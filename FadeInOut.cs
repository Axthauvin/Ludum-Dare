using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public Image blackFade;

    // Start is called before the first frame update
    void Start()
    {
        blackFade.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        FadeOut();
    }

    // Update is called once per frame
    void FadeIn()
    {
        blackFade.CrossFadeAlpha(1, 2, false);
    }

    void FadeOut()
    {
        blackFade.CrossFadeAlpha(0, 2, false);
    }
}
