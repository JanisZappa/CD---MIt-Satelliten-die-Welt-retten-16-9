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
        float range = 1f / (max - min);

        float speed = .45f * max;
        while (t < max)
        {
            t += Time.deltaTime * speed;

            float l = Mathf.Clamp(t, 0, max);
            
            rects[0].localScale = new Vector3(Mathf.Min(a, l), 1, 1);
            rects[1].localScale = new Vector3(Mathf.Min(b, l), 1, 1);
            rects[2].localScale = new Vector3(Mathf.Min(c, l), 1, 1);

            float tMax = Mathf.Min(max, l);
        
            images[0].color = barTex.GetPixelBilinear(Mathf.Clamp01(a / tMax * range - min), 0);
            images[1].color = barTex.GetPixelBilinear(Mathf.Clamp01(b / tMax * range - min), 0);
            images[2].color = barTex.GetPixelBilinear(Mathf.Clamp01(c / tMax * range - min), 0);
            
            yield return null;
        }
    }
}
