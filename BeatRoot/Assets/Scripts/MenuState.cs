using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MenuState : GameState
{
  public MapGeneration mapGeneration;

  public override void OnEnter()
  {
    base.OnEnter();

    mapGeneration.Reset();

    // Reset player position
    transform.position = new Vector3(0, 0, 0);

    Debug.Log("MenuState.OnEnter()");
  }

  public override void OnLeave()
  {
    base.OnLeave();

    Debug.Log("MenuState.OnLeave()");
  }

  public override void Start()
  {
    base.Start();

    Debug.Log("MenuState.Start()");
  }

  public override void Update()
  {
    base.Update();

    if (!Active || IsUpdateLocked)
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