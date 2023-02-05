using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Material material;

    private bool hydrated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fade", 0.0f);
    }

    public void Hydrate()
    {
        if (!hydrated)
         StartCoroutine(HydateRoutine());
    }

    private IEnumerator HydateRoutine()
    {
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            progress += Time.deltaTime / 1.0f;
            material.SetFloat("_Fade", progress);

            yield return null;
        }
        material.SetFloat("_Fade", 1.0f);
        hydrated = true;
    }
}
