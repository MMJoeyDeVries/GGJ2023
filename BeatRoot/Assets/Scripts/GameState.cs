
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
  public GameState next;

  private bool active = false;
  public bool Active
  {
    get
    {
      return this.active;
    }
  }

  public virtual void OnEnter()
  {
    this.active = true;
  }

  public virtual void OnLeave()
  {
    this.active = false;
  }

  protected void Next()
  {
    this.OnLeave();
    next.OnEnter();
  }
}
