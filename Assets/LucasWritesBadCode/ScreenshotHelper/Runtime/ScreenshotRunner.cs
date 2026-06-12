using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace LucasWritesBadCode.ScreenshotHelper.Runtime
{
    public class ScreenshotRunner : MonoBehaviour
    {
        public void StartCapture(string fullPath, ScreenshotFormat format, int superscale,
            Action<string> onComplete, Action<Exception> onError)
        {
            StartCoroutine(CaptureCoroutine(fullPath, format, superscale, onComplete, onError));
        }

        private IEnumerator CaptureCoroutine(string fullPath, ScreenshotFormat format, int superscale,
            Action<string> onComplete, Action<Exception> onError)
        {
            yield return new WaitForEndOfFrame();

            Texture2D texture = null;
            try
            {
                texture = ScreenCapture.CaptureScreenshotAsTexture(superscale);
                byte[] bytes = format == ScreenshotFormat.Png
                    ? texture.EncodeToPNG()
                    : texture.EncodeToJPG();
                File.WriteAllBytes(fullPath, bytes);
                onComplete?.Invoke(fullPath);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
            finally
            {
                if (texture != null)
                    Destroy(texture);
            }
        }
    }
}
