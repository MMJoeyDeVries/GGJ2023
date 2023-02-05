using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public SpriteRenderer _Sprite;
    [Range(0.0f, 1.0f)]
    public float _FadeOverride = 0.0f;

    private bool _isAnimating = false;
    void Start()
    {
        _Sprite.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (!_isAnimating)
        {
            _Sprite.color = new Color(0.0f, 0.0f, 0.0f, _FadeOverride);
        }
    }

    
    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutRoutine(duration));
    }
    public void FadeIn(float duration)
    {
        StartCoroutine(FadeInRoutine(duration));
    }
    public IEnumerator FadeOutRoutine(float duration)
    {
        _isAnimating = true;
        
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            progress += Time.deltaTime / duration;
            _Sprite.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - progress);
            yield return null;
        }

        progress = 1.0f;
        _FadeOverride = 1.0f - progress;
        _Sprite.color = new Color(0.0f, 0.0f, 0.0f, 1.0f - progress);
        
        _isAnimating = false;
    }

    public IEnumerator FadeInRoutine(float duration)
    {
        _isAnimating = true;
        
        float progress = 0.0f;
        while (progress < 1.0f)
        {
            progress += Time.deltaTime / duration;
            _Sprite.color = new Color(0.0f, 0.0f, 0.0f, progress);
            Debug.Log(progress);
            yield return null;
        }

        progress = 1.0f;
        _FadeOverride = 1.0f - progress;
        _Sprite.color = new Color(0.0f, 0.0f, 0.0f, progress);

        _isAnimating = false;
    }
    
    
}
