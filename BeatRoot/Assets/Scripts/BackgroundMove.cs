using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float _OffsetX;
    private float _OffsetY;
    private Camera _Camera;

    private bool _start = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _Camera = Camera.main;

        
    }

    public void SetOffset()
    {
        _start = true;
        _OffsetX = transform.position.x - _Camera.transform.position.x;
        _OffsetY = transform.position.y - _Camera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(_start)
            transform.position = new Vector3(_Camera.transform.position.x + _OffsetX, transform.position.y, transform.position.z);
    }
}
