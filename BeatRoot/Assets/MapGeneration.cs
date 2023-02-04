using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using Unity.VisualScripting;

public class MapGeneration : MonoBehaviour
{
    public Transform TunnelTester;
    
    public Spline _TopSpline;
    public Spline _CenterSpline;
    public Spline _BottomSpline;

    private Vector3 _CenterLastSplinePosTop = Vector3.zero;
    private Vector3 _CenterLastSplinePosBottom = Vector3.zero;

    private SplineMeshTiling _MeshTilingTop;
    private SplineMeshTiling _MeshTilingBottom;
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
        
        CreateNewSegment(true, 25);
        CreateNewSegment(false, 25);
        
        _MeshTilingTop.CreateMeshes();
        _MeshTilingBottom.CreateMeshes();
    }

    private void CreateNewSegment(bool top, int count)
    {
        Random.InitState(0);
        Spline spline = top ? _TopSpline : _BottomSpline;
        for (int i = 0; i < count; ++i)
        {
            Vector3 prev = top ? _CenterLastSplinePosTop : _CenterLastSplinePosBottom;
            float offset = top ? 3.0f : -3.0f;
            Vector3 pos = new Vector3(prev.x + 4.0f, prev.y + Random.Range(-3.0f, 3.0f), 0.0f);
            // Vector3 pos = new Vector3(prev.x + 4.0f, prev.y, 0.0f);
            spline.AddNode(new SplineNode(pos + new Vector3(0.0f, offset, 0.0f), pos + new Vector3(0.0f, offset, 0.0f) + (pos - prev).normalized * 2.0f));
            if (top)
            {
                _CenterLastSplinePosTop = pos;
            }
            else
            {
                _CenterLastSplinePosBottom = pos;
            }
        }
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
        Debug.Log(IsInTunnel(TunnelTester.position).ToString());
    }
}
