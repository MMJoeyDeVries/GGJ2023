using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class MenuState : GameState
{
  public TextMeshProUGUI InstructionsText;
  public GameCamera gameCamera;
  private bool _isUpdateLocked = false;

  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("MenuState.OnEnter()");

    this.gameCamera.Reset();
    this.transform.position = new Vector3(11.3f, 0.0f, 0f);
    this._isUpdateLocked = true;

    StartCoroutine(EnterTimeline());
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("MenuState.OnLeave()");

    StartCoroutine(LeaveTimeline());
  }

  public override void Start()
  {
    base.Start();

    Debug.Log("MenuState.Start()");
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active || this._isUpdateLocked)
    {
      return;
    }

    // Enter the next state when any key is pressed
    if (Input.anyKey)
    {
      this.Next();
    }
  }

  private IEnumerator EnterTimeline()
  {
    InstructionsText.text = "Press any key to start";

    yield return Utils.FadeTextFromTo(InstructionsText, InstructionsText.color, Utils.White, 1.0f);
    // yield return new WaitForSeconds(1);

    this._isUpdateLocked = false;
  }

  private IEnumerator LeaveTimeline()
  {
    yield return Utils.FadeTextFromTo(InstructionsText, InstructionsText.color, Utils.WhiteAlpha, 1.0f);
  }
}