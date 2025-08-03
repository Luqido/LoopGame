using UnityEngine;
using UnityEditor;

public class CreateRenderTexture
{
    [MenuItem("Tools/Create Render Texture")]
    static void CreateRT()
    {
        RenderTexture rt = new RenderTexture(512, 512, 16);
        AssetDatabase.CreateAsset(rt, "Assets/MyRenderTexture.renderTexture");
        AssetDatabase.SaveAssets();
    }
}
