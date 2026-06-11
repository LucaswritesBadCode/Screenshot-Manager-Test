using System;
using System.IO;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    void Update() //i'll just use update for now.
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        string timestamp = DateTime.Now.ToString();

        //changes the formatting to abide by path name rules
        timestamp = timestamp.Replace("/", "-");
        timestamp = timestamp.Replace(" ", "_");
        timestamp = timestamp.Replace(":", "-");


        string fileName = $"Screenshot_{timestamp}.png";

        string fullPath = fileName;

string folderPath = "";

#if UNITY_EDITOR||UNITY_STANDALONE //so it actually shows up
        folderPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Screenshots");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        fullPath = Path.Combine(folderPath, fileName);
#endif

        ScreenCapture.CaptureScreenshot(fullPath);
        Debug.Log($"Screenshot taken and stored in {folderPath}!");
    }
}
