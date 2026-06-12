using LucasWritesBadCode.ScreenshotHelper.Runtime;
using UnityEngine;

namespace Runtime
{
    public class GameManager : MonoBehaviour
    {
        [ContextMenu("Screenshot")]
        public void Screenshot()
        {
            ScreenshotHandler.TakeScreenshot("Screenshots", "Test", ScreenshotFormat.Png, 1, 
                null, null);
        }
    }
}