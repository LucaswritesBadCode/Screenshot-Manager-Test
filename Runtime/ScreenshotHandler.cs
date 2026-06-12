using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("LucaswritesBadCode.ScreenShotManager.Tests")]

namespace LucaswritesBadCode.ScreenShotManager
{
    public static class ScreenshotHandler
    {
        private static ScreenshotRunner _runner;

        public static void TakeScreenshot(
            string folder = "Screenshots",
            string prefix = "Screenshot",
            ScreenshotFormat format = ScreenshotFormat.PNG,
            int superscale = 1,
            Action<string> onComplete = null,
            Action<Exception> onError = null)
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

        internal static string BuildPath(string folder, string prefix, ScreenshotFormat format)
        {
            string ext = format == ScreenshotFormat.PNG ? "png" : "jpg";
            string fileName = $"{prefix}_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.{ext}";

#if UNITY_EDITOR || UNITY_STANDALONE
            string root = Directory.GetParent(Application.dataPath).FullName;
#else
            string root = Application.persistentDataPath;
#endif
            string folderPath = Path.Combine(root, folder);
            Directory.CreateDirectory(folderPath);
            return Path.Combine(folderPath, fileName);
        }

        private static ScreenshotRunner EnsureRunner()
        {
            if (_runner == null || _runner.Equals(null))
            {
                var go = new GameObject("ScreenshotRunner")
                {
                    hideFlags = HideFlags.HideInHierarchy
                };
                UnityEngine.Object.DontDestroyOnLoad(go);
                _runner = go.AddComponent<ScreenshotRunner>();
            }
            return _runner;
        }
    }
}
