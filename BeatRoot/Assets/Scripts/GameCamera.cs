using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class GameCamera : MonoBehaviour
{
  public Spline _FollowSpline;
  public float _YOffset = -3.0f;
  public float _Speed = 5.0f;
  public float _Zoom = -15.0f;

  private float _Progress = 0.0f;

  void Start()
  {

  }

  void Update()
  {
    _Progress += Time.deltaTime * _Speed;

    CurveSample sample = _FollowSpline.GetSampleAtDistance(_Progress);
    transform.position = new Vector3(sample.location.x, sample.location.y + _YOffset, _Zoom);
  }
}
