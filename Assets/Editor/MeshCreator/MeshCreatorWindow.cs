using UnityEditor;
using UnityEngine;

public class MeshCreatorWindow : EditorWindow
{
    [MenuItem("Window/MeshCreator")]
    public static void ShowWindow()
    {
        GetWindow<MeshCreatorWindow>("Sprite to Mesh Creator");
    }

    private Sprite _sprite;
    private float _thickness = 0.01f;

    private MeshCreator _meshCreator = new MeshCreator();
    private void OnGUI()
    {
        _sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", _sprite, typeof(Sprite), false);
        _thickness = EditorGUILayout.Slider("Thickness", _thickness, 0f, 1f);


        if (GUILayout.Button("Create"))
        {
            if(_sprite == null)
            {
                Debug.LogError("No sprite selected.");
                return;
            }
            CreateMesh();
        }
    }

    private void CreateMesh()
    {
        Mesh mesh = _meshCreator.CreateMesh(_sprite, _thickness);
        Debug.Log($"Mesh creating thickness: {_thickness}");
        SaveMesh(mesh);
        
    }

    private void SaveMesh(Mesh mesh)
    {
        string savePath = EditorUtility.SaveFilePanelInProject("Save Mesh", "NewMesh", "asset", "Select save path for the mesh.", "Assets/Meshes");
        if (string.IsNullOrEmpty(savePath))
        {
            Debug.Log("Cancelled mesh saving.");
            return;
        }

        AssetDatabase.CreateAsset(mesh, savePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Mesh saved at: " + savePath);
    }
}
