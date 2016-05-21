using UnityEditor;
using System;
using System.Diagnostics;
using UnityEngine;
#if (UNITY_EDITOR) 


public class ScriptBatch
{
    [MenuItem("Tools/Build and Run")]
    public static void BuildGame()
    {
        if (BuildPipeline.isBuildingPlayer)
            return;
        string path = Application.dataPath + "/../";
        Process[] running = Process.GetProcesses();
                
        foreach (Process process in running)
        {

            if (process.ProcessName == "Munchkin")
            {
                print("Terminating process..." + process.ProcessName);
                process.Kill();
                break;
            }
        }

        // Get filename.

        //string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
        string[] levels = new string[] { "Assets/Scenes/Intro(Network).unity", "Assets/Scenes/Combat(Network).unity" };

        // Build player.
        BuildPipeline.BuildPlayer(levels, path + "/Munchkin.exe", 
            BuildTarget.StandaloneWindows, 
            BuildOptions.AutoRunPlayer | BuildOptions.InstallInBuildFolder |
            BuildOptions.AllowDebugging | BuildOptions.Development );

        // Copy a file from the project folder to the build folder, alongside the built game.
        //FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");

        // Run the game (Process class from System.Diagnostics).
        Process proc = new Process();
        proc.StartInfo.FileName = path + "Munchkin.exe";
        proc.Start();

    }

    public static void print(string a)
    {
        UnityEngine.Debug.Log(a);
    }
}
#endif