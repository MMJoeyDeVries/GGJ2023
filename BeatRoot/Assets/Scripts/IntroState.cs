using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

class IntroState : GameState
{
  public TextMeshProUGUI InstructionsText;
  public TextMeshProUGUI SkipText;
  public TextMeshProUGUI LeaderBoardText;
  public TextMeshProUGUI ScoreText;

  private IEnumerator coroutine;
  private bool _isUpdateLocked = false;

  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("IntroState.OnEnter()");

    this._isUpdateLocked = true;

    this.coroutine = EnterTimeline();
    StartCoroutine(coroutine);
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("IntroState.OnLeave()");

    StopCoroutine(coroutine);
    StartCoroutine(LeaveTimeline());
  }

  public override void Start()
  {
    base.Start();

    Debug.Log("IntroState.Start()");

    InstructionsText.color = Utils.WhiteAlpha;
    SkipText.color = Utils.WhiteAlpha;
    LeaderBoardText.color = Utils.WhiteAlpha;
    ScoreText.color = Utils.WhiteAlpha;
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
    var fadeDuration = 1.0f;

    yield return new WaitForSeconds(fadeDuration);

    InstructionsText.text = "Once upon a time...\nsomething happened...";

    yield return Utils.FadeTextFromTo(InstructionsText, Utils.WhiteAlpha, Utils.White, fadeDuration);
    yield return new WaitForSeconds(fadeDuration);

    SkipText.text = "Press a key to skip";

    yield return Utils.FadeTextFromTo(SkipText, SkipText.color, Utils.White, fadeDuration);
    yield return new WaitForSeconds(fadeDuration);

    // Unlock update
    this._isUpdateLocked = false;

    yield return new WaitForSeconds(3);

    var routine1 = StartCoroutine(Utils.FadeTextFromTo(InstructionsText, InstructionsText.color, Utils.WhiteAlpha, fadeDuration));
    var routine2 = StartCoroutine(Utils.FadeTextFromTo(SkipText, SkipText.color, Utils.WhiteAlpha, fadeDuration));

    yield return routine1;
    yield return routine2;

    InstructionsText.text = "and then...\nthis text appeared!";
    yield return Utils.FadeTextFromTo(InstructionsText, InstructionsText.color, Utils.White, fadeDuration);


    yield return new WaitForSeconds(4);

    var routine3 = Utils.FadeTextFromTo(InstructionsText, InstructionsText.color, Utils.WhiteAlpha, fadeDuration);

    yield return routine3;

    // Go to next state (Menu)
    this.Next();
  }

  private IEnumerator LeaveTimeline()
  {
    var fadeDuration = 1.0f;

    var routine1 = StartCoroutine(Utils.FadeTextFromTo(InstructionsText, InstructionsText.color, Utils.WhiteAlpha, fadeDuration));
    var routine2 = StartCoroutine(Utils.FadeTextFromTo(SkipText, SkipText.color, Utils.WhiteAlpha, fadeDuration));

    yield return routine1;
    yield return routine2;
  }
}