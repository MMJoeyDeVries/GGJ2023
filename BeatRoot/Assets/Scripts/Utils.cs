using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

static class Utils
{
  public static Color32 WhiteAlpha = new Color32(255, 255, 255, 0);
  public static Color32 White = new Color32(255, 255, 255, 255);

  public static IEnumerator FadeTextFromTo(TextMeshProUGUI textMesh, Color32 from, Color32 to, float fadeTime = 1)
  {
    float waitTime = 0;

    while (waitTime < fadeTime)
    {
      textMesh.color = Color32.Lerp(from, to, waitTime);

      yield return null;

      waitTime += Time.deltaTime / fadeTime;
    }

    textMesh.color = to;
  }
}