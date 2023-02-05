using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class GameCamera : MonoBehaviour
{
  public Game game;
  public PlayState playState;
  public Spline FollowSpline;
  public float _YOffset = -3.0f;
  public float _Speed = 5.0f;
  public float _Zoom = -15.0f;

  private float _Progress = 0.0f;

  public float CurveXOffset = 15.0f;

  void Start()
  {

  }

  public void SetStartPos()
  {
    CurveSample sample = FollowSpline.GetSampleAtDistance(_Progress + 4.0f);
    transform.position = new Vector3(sample.location.x, sample.location.y + _YOffset, _Zoom);
  }

  void Update()
  {
    // Camera should only track player in PlayState
    if (game.state.GetType() != typeof(PlayState) || playState.Paused)
    {
      return;
    }

    _Progress += Time.deltaTime * _Speed;

    CurveSample sample = FollowSpline.GetSampleAtDistance(_Progress + CurveXOffset);
    transform.position = new Vector3(sample.location.x, sample.location.y + _YOffset, _Zoom);
  }

  public void Reset()
  {
    _Progress = 0.0f;
  }
}
