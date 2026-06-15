using UnityEngine;
using LucasWritesBadCode.ScreenshotHelper.Runtime;
public class Screenshot : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ScreenshotHandler.TakeScreenshot(prefix: "JPGfile", format: ScreenshotFormat.Jpg);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ScreenshotHandler.TakeScreenshot(prefix: "PNGfile", onComplete: (string s)  =>
            {Debug.Log(s);});
        }
    }
}
