using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayState : GameState
{
  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("PlayState.OnEnter()");
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("PlayState.OnLeave()");
  }

  void Start()
  {
    Debug.Log("PlayState.Start()");
  }

  void Update()
  {
    if (!this.Active)
    {
      return;
    }
  }
}