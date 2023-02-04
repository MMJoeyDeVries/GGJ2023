using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using Unity.VisualScripting;

public class MapGeneration : MonoBehaviour
{
    public Transform Player;
    
    public Spline _TopSpline;
    public Spline _BottomSpline;

    public float MinY = -20.0f;
    public float MaxY = 5.0f;

    private Vector3 _CenterLastSplinePosTop = Vector3.zero;
    private Vector3 _CenterLastSplinePosBottom = Vector3.zero;

    private SplineMeshTiling _MeshTilingTop;
    private SplineMeshTiling _MeshTilingBottom;

    private float _splineEndX = 0.0f;
    void Start()
    {
        _TopSpline.Reset();
        // _CenterSpline.Reset();
        _BottomSpline.Reset();

        for (int i = 0; i < _TopSpline.transform.GetChild(0).childCount; i++)
        {
            Destroy(_TopSpline.transform.GetChild(0).GetChild(i).gameObject);
        }
        for (int i = 0; i < _BottomSpline.transform.GetChild(0).childCount; i++)
        {
            Destroy(_BottomSpline.transform.GetChild(0).GetChild(i).gameObject);
        }

        _MeshTilingTop = _TopSpline.GetComponent<SplineMeshTiling>();
        _MeshTilingBottom = _BottomSpline.GetComponent<SplineMeshTiling>();
        
        _splineEndX = CreateNewSegment(25);
    }

    private float CreateNewSegment(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            float maxYDiff = MaxY - _CenterLastSplinePosTop.y;
            float minYDiff = _CenterLastSplinePosTop.y - MinY;

            float random = Random.Range(-5.0f + Mathf.Max(5.0f - minYDiff), 5.0f - Mathf.Max(5.0f - maxYDiff, 0.0f));
            // top spline
            Vector3 prev = _CenterLastSplinePosTop;
            float offset = 3.0f;
            Vector3 pos = new Vector3(prev.x + 4.0f, prev.y + random, 0.0f);
            _TopSpline.AddNode(new SplineNode(pos + new Vector3(0.0f, offset, 0.0f), pos + new Vector3(0.0f, offset, 0.0f) + (pos - prev).normalized * 2.0f));
            _CenterLastSplinePosTop = pos;
            
            // bottom spline
            prev = _CenterLastSplinePosBottom;
            offset = -3.0f;
            pos = new Vector3(prev.x + 4.0f, prev.y + random, 0.0f);
            _BottomSpline.AddNode(new SplineNode(pos + new Vector3(0.0f, offset, 0.0f), pos + new Vector3(0.0f, offset, 0.0f) + (pos - prev).normalized * 2.0f));
            _CenterLastSplinePosBottom = pos;
        }
        
        // generate new mesh
        _MeshTilingTop.CreateMeshes();
        _MeshTilingBottom.CreateMeshes();
        
        // return last generated x coordinate so we know when to generate a new segment later on
        return _TopSpline.GetSampleAtDistance(_TopSpline.Length).location.x;
    }

    public bool IsInTunnel(Vector3 position)
    {
        CurveSample topSample = _TopSpline.GetProjectionSample(position);
        CurveSample bottomSample = _BottomSpline.GetProjectionSample(position);

        Vector3 top = topSample.location - position;
        Vector3 bottom = bottomSample.location - position;

        bool sameDirection = Vector3.Dot(top, bottom) >= 0.0f;
        return !sameDirection;
    }
    
    void Update()
    {
        if (Player != null)
        {
            if (Mathf.Abs(Player.position.x - _splineEndX) < 10.0f)
            {
                Debug.Log("New segment!");
                _splineEndX = CreateNewSegment(10);
            }

            // Debug.Log(IsInTunnel(Player.position).ToString());
        }
    }
}
