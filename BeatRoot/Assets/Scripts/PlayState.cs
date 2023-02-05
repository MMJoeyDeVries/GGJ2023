using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayState : GameState
{
  public TextMeshProUGUI InstructionsText;
  public TextMeshProUGUI SkipText;
  public TextMeshProUGUI HighScoreText;
  public TextMeshProUGUI ScoreText;

  public BackgroundMove _Mover1;
  public BackgroundMove _Mover2;

  private bool _paused = true;
  public bool Paused
  {
    get
    {
      return _paused;
    }
  }

  private float _currentScore;
  public float CurrentScore
  {
    get
    {
      return _currentScore;
    }
  }

  private SphereCollider _sphereCollider;
  private float _startTime;


  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("PlayState.OnEnter()");

    this._paused = true;

    StartCoroutine(EnterTimeline());
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("PlayState.OnLeave()");

    this._paused = true;

    StartCoroutine(LeaveTimeline());
  }

  public override void Start()
  {
    base.Start();

    Debug.Log("PlayState.Start()");
    
    // _Mover1.SetOffset();
    // _Mover2.SetOffset();

    _sphereCollider = GetComponent<SphereCollider>();
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active || this.Paused)
    {
      return;
    }

    var colliderCenterRight = transform.position + new Vector3(_sphereCollider.radius, 0, 0);
    var screenPoint = Camera.main.WorldToScreenPoint(colliderCenterRight);

    if (screenPoint.x < 0)
    {
      this.Next();
      return;
    }

    this._currentScore = Mathf.Round((Time.time - this._startTime) * 10);
    ScoreText.text = this._currentScore.ToString();
  }

  private IEnumerator EnterTimeline()
  {
    ScoreText.text = "0";

    var highScore = PlayerPrefs.GetFloat("highScore", 0);
    HighScoreText.text = highScore.ToString();

    yield return Utils.FadeTextFromTo(ScoreText, ScoreText.color, Utils.White, 1.0f);
    yield return Utils.FadeTextFromTo(HighScoreText, HighScoreText.color, Utils.White, 1.0f);

    yield return new WaitForSeconds(1.0f);

    this._paused = false;
    this._startTime = Time.time;
  }

  private IEnumerator LeaveTimeline()
  {
    yield return Utils.FadeTextFromTo(ScoreText, ScoreText.color, Utils.WhiteAlpha, 1.0f);
  }
}