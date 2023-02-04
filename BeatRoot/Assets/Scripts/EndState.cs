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

  public override void Start()
  {
    base.Start();

    Debug.Log("EndState.Start()");
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active || IsUpdateLocked)
    {
      return;
    }

    if (Input.anyKey)
    {
      this.Next();
    }
  }
}