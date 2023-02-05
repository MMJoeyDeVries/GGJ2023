using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    private SpriteRenderer _SpriteRenderer;
    private Camera _Camera;
    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Camera = Camera.main;
    }

    void Update()
    {
        float left = transform.position.x - _SpriteRenderer.size.x * transform.lossyScale.x * 0.5f;
        float right = transform.position.x + _SpriteRenderer.size.x * transform.lossyScale.x * 0.5f;

        Vector3 leftPos = new Vector3(left, transform.position.y, transform.position.z);
        Vector3 rightPos = new Vector3(right, transform.position.y, transform.position.z);

        Vector3 leftScreenPos = _Camera.WorldToScreenPoint(leftPos);
        Vector3 rightScreenPos = _Camera.WorldToScreenPoint(rightPos);
        
        // check if we need to spawn a new version
        Debug.Log(rightScreenPos.x + "  | "  + (Screen.width - 100.0f));
        if (-rightScreenPos.x > Screen.width - 100.0f)
        {
            Debug.Log("NEW"); 
            GameObject newSprite = new GameObject("Parallax_Sprite");
            newSprite.transform.position = new Vector3(right - _SpriteRenderer.size.x * transform.lossyScale.x * 0.5f, transform.position.y, transform.position.z);
            newSprite.transform.parent = transform.parent;
            SpriteRenderer sprite = newSprite.AddComponent<SpriteRenderer>();
            sprite.sprite = _SpriteRenderer.sprite;
        }
    }
    
    
}
