using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MenuState : GameState
{
  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("MenuState.OnEnter()");
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("MenuState.OnLeave()");
  }

  void Start()
  {
    Debug.Log("MenuState.Start()");
  }

  void Update()
  {
    if (!this.Active)
    {
      return;
    }

    // Enter the next state when any key is pressed
    if (Input.anyKey)
    {
      this.Next();
    }
  }
}