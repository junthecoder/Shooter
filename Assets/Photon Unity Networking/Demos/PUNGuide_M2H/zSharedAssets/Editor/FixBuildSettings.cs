using System.IO;
using UnityEngine;
using UnityEditor;

using System.Collections;

public class FixBuildSettings : MonoBehaviour
{

    [MenuItem("PUN Guide/Reset build settings")]
    static void FixBSet()
    {
        //
        //  SET SCENES
        //

        if (!EditorUtility.DisplayDialog("Resetting build settings", "Can the current build settings be overwritten with the scenes for the PUN guide?", "OK", "No, cancel"))
            return;

        // find path of pun guide
        string[] tempPaths = Directory.GetDirectories(Application.dataPath, "PUNGuide_M2H", SearchOption.AllDirectories);
        if (tempPaths == null || tempPaths.Length != 1)
        {
            return;
        }

        // find scenes of guide
        string guidePath = tempPaths[0];
        tempPaths = Directory.GetFiles(guidePath, "*.unity", SearchOption.AllDirectories);

        if (tempPaths == null || tempPaths.Length == 0)
        {
            return;
        }

        // add found guide scenes to build settings
        EditorBuildSettingsScene[] sceneAr = new EditorBuildSettingsScene[tempPaths.Length];
        for (int i = 0; i < tempPaths.Length; i++)
        {
            string path = tempPaths[i].Substring(Application.dataPath.Length - "Assets".Length);
            sceneAr[i] = new EditorBuildSettingsScene(path, true);
        }
        
        EditorBuildSettings.scenes = sceneAr;
        Debug.Log("PUN Guide: reset project build settings.");


        /*
        
        //Output current build settings
        string bl = "";
        int i = 0;
        foreach (EditorBuildSettingsScene sc in EditorBuildSettings.scenes)
        {
            bl += "sceneAr[i++] = new EditorBuildSettingsScene(\"" + sc.path + "\", true);\n";

            i++;
        }
        Debug.Log(bl);
          
        */

    }
}