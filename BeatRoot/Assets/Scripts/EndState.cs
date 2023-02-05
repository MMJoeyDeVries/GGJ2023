using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class EndState : GameState
{
  public TextMeshProUGUI LeaderBoardTextMesh;

  private bool _isUpdateLocked = false;

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

    LeaderBoardTextMesh.text = "Leaderboard\n\n\n\nLeroy Jenkins: 1000\n\nJohn Doe: 900\n\nJane Doe: 800\n\n";

    yield return Utils.FadeTextFromTo(LeaderBoardTextMesh, LeaderBoardTextMesh.color, Utils.White, 1.0f);

    this._isUpdateLocked = false;
  }

  private IEnumerator LeaveTimeline()
  {
    yield return Utils.FadeTextFromTo(LeaderBoardTextMesh, LeaderBoardTextMesh.color, Utils.WhiteAlpha, 1.0f);
  }
}