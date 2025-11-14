using UnityEngine;


public class SetFullscreenRes : MonoBehaviour
{
    public Vector2Int res;
    
    private void Start()
    {
        if (res.x == 0 || res.y == 0)
        {
            Resolution resolution = Screen.resolutions[Screen.resolutions.Length - 1];
            Screen.SetResolution(resolution.width, resolution.height, true);
        }
        else
            Screen.SetResolution(res.x, res.y, true);
        
        Destroy(this);
    }
}
