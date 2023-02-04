using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayState : GameState
{
  SphereCollider sphereCollider;

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

  public override void Start()
  {
    base.Start();

    Debug.Log("PlayState.Start()");

    sphereCollider = GetComponent<SphereCollider>();
  }

  public override void Update()
  {
    base.Update();

    if (!this.Active)
    {
      return;
    }

    var colliderCenterRight = transform.position + new Vector3(sphereCollider.radius, 0, 0);
    var screenPoint = Camera.main.WorldToScreenPoint(colliderCenterRight);

    if (screenPoint.x < 0)
    {
      this.Next();
    }
  }
}