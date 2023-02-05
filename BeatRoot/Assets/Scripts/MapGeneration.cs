using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SplineMesh;
using Unity.VisualScripting;

public class MapGeneration : MonoBehaviour
{
    public Transform Player;
    
    public Spline _TopSpline;
    public Spline _CenterSpline;
    public Spline _BottomSpline;

    [Header("Config")] 
    public float Width = 3.0f;
    public float Curvature = 1.0f;
    public float MinY = -20.0f;
    public float MaxY = 5.0f;
    public GameObject TopObstaclePrefab;
    public GameObject BottomObstaclePrefab;
    public Vector3 ObstacleOffset;
    public float ObstacleChance = 0.4f;

    private Vector3 _CenterLastSplinePosTop = Vector3.zero;
    private Vector3 _CenterLastSplinePosBottom = Vector3.zero;
    private Vector3 _CenterLastSplinePos = Vector3.zero;

    private SplineMeshTiling _MeshTilingTop;
    private SplineMeshTiling _MeshTilingBottom;
    private SplineMeshTiling _MeshTilingCenter;

    private float _splineEndX = 0.0f;
    void Start()
    {
        // _TopSpline.Reset();
        // _BottomSpline.Reset();

        // for (int i = 0; i < _TopSpline.transform.GetChild(0).childCount; i++)
        // {
        //     Destroy(_TopSpline.transform.GetChild(0).GetChild(i).gameObject);
        // }
        // for (int i = 0; i < _BottomSpline.transform.GetChild(0).childCount; i++)
        // {
        //     Destroy(_BottomSpline.transform.GetChild(0).GetChild(i).gameObject);
        // }

        _MeshTilingTop = _TopSpline.GetComponent<SplineMeshTiling>();
        _MeshTilingBottom = _BottomSpline.GetComponent<SplineMeshTiling>();
        _MeshTilingCenter = _CenterSpline.GetComponent<SplineMeshTiling>();
        
        // create 4 segments to override base segments, help fix some issues
        // _TopSpline.nodes[0].Position = new Vector3(0.0f, Width, 0.0f);
        // _BottomSpline.nodes[0].Position = new Vector3(0.0f, -Width, 0.0f);
        // _TopSpline.nodes[1].Position = new Vector3(4.0f, Width, 0.0f);
        // _BottomSpline.nodes[1].Position = new Vector3(4.0f, -Width, 0.0f);

        
        _TopSpline.nodes[0].Position = new Vector3(-11.0f, 15.0f, 0.0f);
        _TopSpline.nodes[0].Direction = new Vector3(-11.0f, 14.0f, 0.0f);
        _TopSpline.nodes[1].Position = new Vector3(0.0f, 3.0f, 0.0f);
        _TopSpline.nodes[1].Direction = new Vector3(1.0f, 3.0f, 0.0f);
        _TopSpline.nodes[2].Position = new Vector3(4.0f, 3.0f, 0.0f);
        _TopSpline.nodes[2].Direction = new Vector3(5.0f, 3.0f, 0.0f);
        _TopSpline.nodes[3].Position = new Vector3(8.0f, 3.0f, 0.0f);
        _TopSpline.nodes[3].Direction = new Vector3(9.0f, 3.0f, 0.0f);
        
        _BottomSpline.nodes[0].Position = new Vector3(-17.0f, 15.0f, 0.0f);
        _BottomSpline.nodes[0].Direction = new Vector3(-17.0f, 14.0f, 0.0f);
        _BottomSpline.nodes[1].Position = new Vector3(0.0f, -3.0f, 0.0f);
        _BottomSpline.nodes[1].Direction = new Vector3(1.0f, -3.0f, 0.0f);
        _BottomSpline.nodes[2].Position = new Vector3(4.0f, -3.0f, 0.0f);
        _BottomSpline.nodes[2].Direction = new Vector3(5.0f, -3.0f, 0.0f);
        _BottomSpline.nodes[3].Position = new Vector3(8.0f, -3.0f, 0.0f);
        _BottomSpline.nodes[3].Direction = new Vector3(9.0f, -3.0f, 0.0f);
        
        _CenterSpline.nodes[0].Position = new Vector3(-14.0f, 15.0f, 0.0f);
        _CenterSpline.nodes[0].Direction = new Vector3(-14.0f, 14.0f, 0.0f);
        _CenterSpline.nodes[1].Position = new Vector3(0.0f, 0.0f, 0.0f);
        _CenterSpline.nodes[1].Direction = new Vector3(1.0f, 0.0f, 0.0f);
        _CenterSpline.nodes[2].Position = new Vector3(4.0f, 0.0f, 0.0f);
        _CenterSpline.nodes[2].Direction = new Vector3(5.0f,0.0f, 0.0f);
        _CenterSpline.nodes[3].Position = new Vector3(8.0f, 0.0f, 0.0f);
        _CenterSpline.nodes[3].Direction = new Vector3(9.0f, 0.0f, 0.0f);

        _CenterLastSplinePosTop = _TopSpline.nodes[3].Position;
        _CenterLastSplinePosBottom = _BottomSpline.nodes[3].Position;
        _CenterLastSplinePos = _CenterSpline.nodes[3].Position;
        
        _MeshTilingTop.CreateMeshes();
        _MeshTilingBottom.CreateMeshes();
        _MeshTilingCenter.CreateMeshes();

        _splineEndX = CreateNewSegment(25);
    }

    private float CreateNewSegment(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            float maxYDiff = MaxY - _CenterLastSplinePosTop.y;
            float minYDiff = _CenterLastSplinePosTop.y - MinY;

            float random = Random.Range(-2.0f + Mathf.Max(2.0f - minYDiff, 0.0f), 2.0f - Mathf.Max(2.0f - maxYDiff, 0.0f));
            // top spline
            Vector3 prev = _CenterLastSplinePosTop;
            Vector3 topPos = new Vector3(prev.x + 4.0f, prev.y + random, 0.0f);
            _TopSpline.AddNode(new SplineNode(topPos, topPos + (topPos - prev).normalized * 2.0f));
            _CenterLastSplinePosTop = topPos;
            
            // bottom spline
            prev = _CenterLastSplinePosBottom;
            var bottomPos = new Vector3(prev.x + 4.0f, prev.y + random, 0.0f);
            _BottomSpline.AddNode(new SplineNode(bottomPos, bottomPos + (bottomPos - prev).normalized * 2.0f));
            _CenterLastSplinePosBottom = bottomPos;
            
            // center spline
            prev = _CenterLastSplinePos;
            var centerPos = new Vector3(prev.x + 4.0f, prev.y + random, 0.0f);
            _CenterSpline.AddNode(new SplineNode(centerPos, centerPos + (centerPos - prev).normalized * 2.0f));
            _CenterLastSplinePos = centerPos;

            var addToTop = i % 2 == 0;
            AddObstacle(addToTop, addToTop ? topPos : bottomPos);
        }
        
        // generate new mesh
        _MeshTilingTop.CreateMeshes();
        _MeshTilingBottom.CreateMeshes();
        _MeshTilingCenter
            .CreateMeshes();
        
        // return last generated x coordinate so we know when to generate a new segment later on
        return _TopSpline.GetSampleAtDistance(_TopSpline.Length).location.x;
    }

    private void AddObstacle(bool isTop, Vector3 position)
    {
        if ((Random.value - this.ObstacleChance) > 0) {
            return;
        }

        var obstacle = Instantiate(isTop ? TopObstaclePrefab : BottomObstaclePrefab);
        obstacle.transform.position = position + (isTop ? -ObstacleOffset : ObstacleOffset);

        var scale = Random.Range(1.5f, 2.5f);

        obstacle.transform.localScale = new Vector3(scale, scale, 1);
        obstacle.transform.parent = this.transform;
    }

    public bool TunnelIntersect(Vector3 position, float radius)
    {
        CurveSample topSample = _TopSpline.GetProjectionSample(position);
        CurveSample bottomSample = _BottomSpline.GetProjectionSample(position);

        Vector3 top = topSample.location - position;
        Vector3 bottom = bottomSample.location - position;

        bool sameDirection = Vector3.Dot(top, bottom) >= 0.0f;

        if (sameDirection)
        {
            
        }
        else
        {
            // position is in tunnel, 
        }

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
