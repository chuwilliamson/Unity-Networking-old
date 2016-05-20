using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
public class BuildManager : EditorWindow
{
    /// <summary>
    /// Stores The Scenes saved into build settings
    /// </summary>
    static List<string> m_Scenes;
    [MenuItem("Tools/BuildManager")]
    static void Init()
    {
        BuildManager window = GetWindow(typeof(BuildManager)) as BuildManager;
        window.Show();
       
        ///Gets the string name for each of the scens saved into the build settings

    }

    void GatherScenes()
    {
        m_Scenes = new List<string>();
        foreach (EditorBuildSettingsScene a in UnityEditor.EditorBuildSettings.scenes)
        {

            if (a.enabled)
            {

                string tmp;
                tmp = a.path;
                m_Scenes.Add(tmp);
            }


        }
    }
    void OnGUI()
    {
        if (m_Scenes == null)
            GatherScenes();

        GUILayout.Label("Builds game using current platform and scenes set to build.\n Directory Project > Builds > \"Product Name\".exe");
        if(GUILayout.Button("Build"))
        { Build(); }

        GUILayout.Space(10f);


        if (GUILayout.Button("Goto Start Scene"))
            LoadScene();
        
        GUILayout.Space(5f);
        GUILayout.Label(ReturnScenesAsSingleString());
    }

    void Build()
    {
     
        if (!System.IO.Directory.Exists(@".\Builds"))
            System.IO.Directory.CreateDirectory(@".\Builds");

        //UnityEditor.EditorBuildSettings.scenes
        string path = @".\Builds\" + PlayerSettings.productName+".exe";


        
        Process[] Processes = Process.GetProcessesByName(PlayerSettings.productName);
        if(Processes.Length != 0)
        foreach (Process a in Processes)
            a.Kill();

        
        BuildPipeline.BuildPlayer(m_Scenes.ToArray(), path, EditorUserBuildSettings.activeBuildTarget,BuildOptions.Development);

        Process m_Process = new Process();
        m_Process.StartInfo.FileName = path;
        m_Process.Start();

        
    }
    string ReturnScenesAsSingleString()
    {
        
        string tmp = "";
        foreach (string a in m_Scenes)
            tmp += a + "\n";

            return tmp;
    }

    
    void LoadScene()
    {
        if (m_Scenes.Count > 0)
        EditorSceneManager.OpenScene(m_Scenes[0]);
    }
    
}
