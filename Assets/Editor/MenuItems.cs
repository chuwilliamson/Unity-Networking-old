using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
public class MenuItems
{
    [MenuItem("Tools/Start Network")]
    private static void NewMenuOption()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/Intro(Network).unity");
        Debug.Log(EditorSceneManager.sceneCountInBuildSettings);
        EditorApplication.isPlaying = true;
    }
}