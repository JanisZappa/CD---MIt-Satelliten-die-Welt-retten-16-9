using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpCFTQuiz : PopUp
{
    public TextMeshProUGUI[] texts;
    public Image[] images;
    public Sprite[] spr;
    public Image weiterImg;
    public GameObject backButton;
    private int question;
    private readonly bool[] answered = new bool[5];

    private readonly int[] qOrder = new int[5];
    private readonly int[][] aOrder = new int[5][];
    
    public static readonly int[] Score = new int[3];

    private readonly int[] selection = new int[5];
    
    public static int SelectedScenario =>
        Score[0] >= Score[1] ? (Score[0] >= Score[2] ? 0 : Score[1] >= Score[2] ? 1 : 2) : Score[1] >= Score[2] ? 1 : 2;
    
    
    
    protected override void ButtonInit()
    {
        panelButtons[0].onPointerDown.AddListener( () =>
        {
            callback?.Invoke(0);
            ShowPopup();
        });
        panelButtons[1].onPointerDown.AddListener( () =>
        {
            if (answered[question])
            {
                question++;
                if(question < 5)
                    SetQuestion();
                else
                {
                    for (int i = 0; i < 3; i++)
                        Score[i] = 0;
                    for (int i = 0; i < 5; i++)
                        Score[selection[i]]++;
                    
                    callback?.Invoke(1);
                    ShowPopup();
                }
            }
        });
        
        panelButtons[3].onPointerDown.AddListener(() =>
        {
            //if(!answered)
                Selected(0);
        });
        panelButtons[2].onPointerDown.AddListener( () =>
        {
            //if(!answered)
                Selected(1);
        });
        panelButtons[4].onPointerDown.AddListener( () =>
        {
            //if(!answered)
                Selected(2);
        });
        panelButtons[5].onPointerDown.AddListener( () =>
        {
            if (question > 0)
            {
                question--;
                SetQuestion();
            } 
        });
        panelButtons[6].onPointerDown.AddListener( () =>
        {
            //Debug.Log("Überspringen");
            Score[0] = 5;
            Score[1] = 0;
            Score[2] = 0;
            callback?.Invoke(2);
            ShowPopup();
        });
    }


    private void RandomOrder(int[] list)
    {
        int count = list.Length;
        for (int i = 0; i < count; i++)
            list[i] = i;

        int count2 = count * 32;
        for (int i = 0; i < count2; i++)
        {
            int pA = Random.Range(0, count);
            int pB = Random.Range(0, count);
            int vA = list[pA];
            int vB = list[pB];

            list[pA] = vB;
            list[pB] = vA;
        }
    }


    private void OnEnable()
    {
        question = 0;
        for (int i = 0; i < 5; i++)
            answered[i] = false;
        
        for (int i = 0; i < 5; i++)
            selection[i] = -1;
        
        RandomOrder(qOrder);
        
        if(aOrder[0] == null)
            for (int i = 0; i < 5; i++)
                aOrder[i] = new int[3];
        
        for (int i = 0; i < 5; i++)
            RandomOrder(aOrder[i]);
        
        SetQuestion();
    }


    private void Selected(int id)
    {
        for (int i = 0; i < 3; i++)
            images[i].sprite = spr[i == id? 1 : 0];

        int v = CFTGame.GetQuestion(qOrder[question]).Answers[aOrder[question][id]].Value;
        selection[question] = v;
        
        answered[question] = true;
        
        weiterImg.color = new Color(1, 1, 1, 1);
        weiterImg.raycastTarget = true;
    }


    private void SetQuestion()
    {
        //Debug.LogFormat("Question {0}: {1}", question, answered[question]? selection[question].ToString() : "X");
        //Debug.Log(aOrder[question][0] + ":" + aOrder[question][1] + ":" + aOrder[question][2]);
        backButton.SetActive(question > 0);
        
        weiterImg.color = new Color(1, 1, 1, answered[question]? 1 : .35f);
        weiterImg.raycastTarget = answered[question];
        Question q = CFTGame.GetQuestion(qOrder[question]);

        if (LanguageSwitch.English)
        {
            texts[0].text = "Question " + (1 + question) + " of 5";
            texts[1].text = q.QText_EN;
        
            for (int i = 0; i < 3; i++)
                texts[2 + i].text = q.Answers[aOrder[question][i]].AText_EN;
            
            texts[5].text = "Your answer?";
        }
        else
        {
            texts[0].text = "Frage " + (1 + question) + " von 5";
            texts[1].text = q.QText;
        
            for (int i = 0; i < 3; i++)
                texts[2 + i].text = q.Answers[aOrder[question][i]].AText;

            texts[5].text = "Ihre Antwort?";
        }

        int sel = selection[question];
        for (int i = 0; i < 3; i++)
            images[i].sprite = spr[aOrder[question][i] == sel? 1 : 0];
    }
}
