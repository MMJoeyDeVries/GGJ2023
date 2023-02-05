using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Movement : MonoBehaviour
{
  private Game _game;
  private Rigidbody _rigidBody;
  private PlayState _playState;

  public float forceScale = 10;
  public float pointerScale = 1;

  // Start is called before the first frame update
  void Start()
  {
    _game = GetComponent<Game>();
    _rigidBody = GetComponent<Rigidbody>();
    _playState = GetComponent<PlayState>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void FixedUpdate()
  {
    if (_playState.Paused)
    {
      _rigidBody.velocity = Vector3.zero;
      return;
    }

    var force = Vector3.zero;

    if (Input.GetKey(KeyCode.W))
    {
      force += Vector3.up;
    }

    if (Input.GetKey(KeyCode.S))
    {
      force += Vector3.down;
    }

    if (Input.GetKey(KeyCode.A))
    {
      force += Vector3.left;
    }

    if (Input.GetKey(KeyCode.D))
    {
      force += Vector3.right;
    }

    if (Input.GetMouseButton(0))
    {
      var mousePositionRelativeToObject = Camera.main.ScreenToViewportPoint(
        Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)
      );

      force = mousePositionRelativeToObject * 2f * pointerScale;
    }

    if (Input.touchCount > 0)
    {
      var firstTouch = Input.touches[0];

      var touchPositionRelativeToObject = Camera.main.ScreenToViewportPoint(
        new Vector3(
          firstTouch.position.x,
          firstTouch.position.y,
          0
        ) - Camera.main.WorldToScreenPoint(transform.position)
      );

      force = touchPositionRelativeToObject * 2f * pointerScale;
    }

    _rigidBody.AddForce(force * forceScale);
  }
}
