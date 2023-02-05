using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class EndState : GameState
{
  public TextMeshProUGUI HighScoreText;

  private bool _isUpdateLocked = false;
  private PlayState _playState;

  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("EndState.OnEnter()");

    StartCoroutine(EnterTimeline());
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("EndState.OnLeave()");

    StartCoroutine(LeaveTimeline());
  }

  public override void Start()
  {
    base.Start();

    Debug.Log("EndState.Start()");

    _playState = GetComponent<PlayState>();
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active || this._isUpdateLocked)
    {
      return;
    }

    if (Input.anyKey)
    {
      this.Next();
    }
  }

  private IEnumerator EnterTimeline()
  {
    this._isUpdateLocked = true;

    var highScore = Mathf.Max(PlayerPrefs.GetFloat("highScore", 0), _playState.CurrentScore);
    PlayerPrefs.SetFloat("highScore", highScore);

    HighScoreText.text = highScore.ToString();

    yield return Utils.FadeTextFromTo(HighScoreText, HighScoreText.color, Utils.White, 1.0f);

    this._isUpdateLocked = false;
  }

  private IEnumerator LeaveTimeline()
  {
    yield return Utils.FadeTextFromTo(HighScoreText, HighScoreText.color, Utils.WhiteAlpha, 1.0f);
  }
}