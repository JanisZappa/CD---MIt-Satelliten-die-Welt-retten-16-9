using UnityEngine;


public class SplitSafariGame : SplitGame
{
    public Camera main, map;
    public SafariGame game;
    public GameObject[] toggleObjects;
    
    public override void Init()
    {
        base.Init();
        SetActive(false);
    }
    
    
    public override SplitGame GameStart(bool activeWindow = true)
    {
        Debug.Log("Safari Start");
        
        SetActive(true);
        game.StartGame(SafariGame.GameType.None);
        
        return base.GameStart(activeWindow);
    }


    public override void GameStop()
    {
        base.GameStop();
        
        Debug.Log("Safari Stop");
        game.ForceGameOver(true);
        
        SetActive(false);
    }


    private void SetActive(bool value)
    {
        main.enabled = value;
        map.enabled = value;

        int l = toggleObjects.Length;
        for (int i = 0; i < l; i++)
            toggleObjects[i].SetActive(value);
    }


    public int Score => game.Score;
}
