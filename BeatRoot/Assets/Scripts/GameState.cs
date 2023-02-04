
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{


  public GameState next;
  protected Game game;

  private bool active = false;
  public bool Active
  {
    get
    {
      return this.active;
    }
  }

  public float LockDurationInSeconds = 0;
  private bool isUpdateLocked = false;
  public bool IsUpdateLocked
  {
    get
    {
      return this.isUpdateLocked;
    }
  }

  public virtual void OnEnter()
  {
    active = true;
    isUpdateLocked = true;

    StartCoroutine(UnlockUpdateAfterLockDuration());
  }

  public virtual void OnLeave()
  {
    active = false;
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


  private IEnumerator UnlockUpdateAfterLockDuration()
  {
    yield return new WaitForSeconds(LockDurationInSeconds);

    isUpdateLocked = false;
  }
}
