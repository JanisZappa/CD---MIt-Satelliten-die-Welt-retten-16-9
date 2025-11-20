using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUpCFTResult : PopUp
{
    public RectTransform[] rects;
    public Image[] images;
    public Texture2D barTex;
    
    protected override void ButtonInit()
    {
        panelButtons[0].onPointerDown.AddListener(() =>
        {
            callback?.Invoke(0);
            ShowPopup();
        });
        
        panelButtons[1].onPointerDown.AddListener(() =>
        {
            callback?.Invoke(1);
            ShowPopup();
        });
    }

    
    public void OnEnable()
    {
        for (int i = 0; i < 3; i++)
            rects[i].localScale = new Vector3(PopUpCFTQuiz.Score[i] * 1f / 5, 1, 1);

        StartCoroutine(Anim());
    }


    private IEnumerator Anim()
    {
        float t = 0;

        float a = PopUpCFTQuiz.Score[0] * .2f;
        float b = PopUpCFTQuiz.Score[1] * .2f;
        float c = PopUpCFTQuiz.Score[2] * .2f;
        
        float min = Mathf.Min(a, Mathf.Min(b, c));
        float max = Mathf.Max(a, Mathf.Max(b, c));
        
        float a2 = Mathf.Approximately(a, max) ? 1 : Mathf.Approximately(a, min) ? .3f : .65f;
        float b2 = Mathf.Approximately(b, max) ? 1 : Mathf.Approximately(b, min) ? .3f : .65f;
        float c2 = Mathf.Approximately(c, max) ? 1 : Mathf.Approximately(c, min) ? .3f : .65f;

        float speed = .45f * max;
        while (t < max)
        {
            t += Time.deltaTime * speed;

            float l = Mathf.Clamp(t, 0, max);
            rects[0].localScale = new Vector3(Mathf.SmoothStep(0, a, a > 0? Mathf.Min(a, l) / a : 0), 1, 1);
            rects[1].localScale = new Vector3(Mathf.SmoothStep(0, b, b > 0? Mathf.Min(b, l) / b : 0), 1, 1);
            rects[2].localScale = new Vector3(Mathf.SmoothStep(0, c, c > 0? Mathf.Min(c, l) / c : 0), 1, 1);

            float l2 = l / max;
            images[0].color = barTex.GetPixelBilinear(Mathf.Lerp(1, a2, l2), 0);
            images[1].color = barTex.GetPixelBilinear(Mathf.Lerp(1, b2, l2), 0);
            images[2].color = barTex.GetPixelBilinear(Mathf.Lerp(1, c2, l2), 0);
            
            yield return null;
        }
    }
}
