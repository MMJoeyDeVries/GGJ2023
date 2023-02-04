using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class IntroState : GameState
{

  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("IntroState.OnEnter()");
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("IntroState.OnLeave()");
  }

  public override void Start()
  {
    base.Start();

    Debug.Log("IntroState.Start()");
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active)
    {
      return;
    }

    if (Input.anyKey)
    {
      this.Next();
    }
  }
}