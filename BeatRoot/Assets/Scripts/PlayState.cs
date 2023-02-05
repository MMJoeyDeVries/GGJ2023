using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayState : GameState
{
  public TextMeshProUGUI InstructionsText;
  public TextMeshProUGUI SkipText;
  public TextMeshProUGUI LeaderBoardText;
  public TextMeshProUGUI ScoreText;

  private bool _paused = true;
  public bool Paused
  {
    get
    {
      return _paused;
    }
  }

  private SphereCollider _sphereCollider;


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

    _sphereCollider = GetComponent<SphereCollider>();
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active)
    {
      return;
    }

    var colliderCenterRight = transform.position + new Vector3(_sphereCollider.radius, 0, 0);
    var screenPoint = Camera.main.WorldToScreenPoint(colliderCenterRight);

    if (screenPoint.x < 0)
    {
      this.Next();
    }
  }

  private IEnumerator EnterTimeline()
  {
    yield return Utils.FadeTextFromTo(ScoreText, ScoreText.color, Utils.White, 1.0f);

    yield return new WaitForSeconds(1.0f);

    this._paused = false;
  }

  private IEnumerator LeaveTimeline()
  {
    yield return Utils.FadeTextFromTo(ScoreText, ScoreText.color, Utils.WhiteAlpha, 1.0f);
  }
}