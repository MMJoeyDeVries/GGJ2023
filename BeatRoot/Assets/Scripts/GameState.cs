
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{
  public GameState next;
  protected Game game;

  private bool _active = false;
  public bool Active
  {
    get
    {
      return this._active;
    }
  }

  public virtual void OnEnter()
  {
    this._active = true;
  }

  public virtual void OnLeave()
  {
    _active = false;
  }

  public virtual void Start()
  {
    game = GetComponent<Game>();
  }

  public virtual void Update() { }

  protected void Next()
  {
    OnLeave();
    next.OnEnter();

    game.state = next;
  }


}
