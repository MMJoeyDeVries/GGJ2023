using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EndState : GameState
{
  public override void OnEnter()
  {
    base.OnEnter();

    Debug.Log("EndState.OnEnter()");
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("EndState.OnLeave()");
  }

  void Start()
  {

    Debug.Log("EndState.Start()");
  }

  void Update()
  {
    if (!this.Active)
    {
      return;
    }
  }
}