using UnityEngine;

[ExecuteInEditMode]
public class AtmosphereSetter : MonoBehaviour
{
    public Transform earth;
    public Transform sun;
    private static readonly int Earth = Shader.PropertyToID("Earth");
    private static readonly int Sun = Shader.PropertyToID("Sun");

    
    private void Update()
    {
        Shader.SetGlobalVector(Earth, earth.position);
        Shader.SetGlobalVector(Sun, sun.position);
    }
}
