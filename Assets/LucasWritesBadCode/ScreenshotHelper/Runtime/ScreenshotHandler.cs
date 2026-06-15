using System;
using System.IO;
using UnityEngine;

namespace LucasWritesBadCode.ScreenshotHelper.Runtime
{
    public static class ScreenshotHandler
    {
        private static ScreenshotRunner runner;

        public static void TakeScreenshot(string folder = "Screenshots", string prefix = "Screenshot", ScreenshotFormat format = ScreenshotFormat.Png, int superscale = 1,
            Action<string> onComplete = null, Action<Exception> onError =null)
        {
            string fullPath;
            try
            {
                fullPath = BuildPath(folder, prefix, format);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                return;
            }

            EnsureRunner().StartCapture(fullPath, format, superscale, onComplete, onError);
        }

        public static string BuildPath(string folder, string prefix, ScreenshotFormat format)
        {
            string ext = format == ScreenshotFormat.Png ? "png" : "jpg";
            string fileName = $"{prefix}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.{ext}";

#if UNITY_EDITOR || UNITY_STANDALONE
            string root = Directory.GetParent(Application.dataPath)?.FullName;
#else
            string root = Application.persistentDataPath;
#endif
            if (root == null) throw new Exception("Could not determine root path");

            string folderPath = Path.Combine(root, folder);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, fileName);
        }

        private static ScreenshotRunner EnsureRunner()
        {
            if (runner && !runner.Equals(null)) return runner;

            GameObject go = new GameObject("ScreenshotRunner")
            {
                hideFlags = HideFlags.HideInHierarchy
            };

            UnityEngine.Object.DontDestroyOnLoad(go);
            runner = go.AddComponent<ScreenshotRunner>();
            return runner;
        }
    }
}