using System.IO;
using NUnit.Framework;

namespace LucaswritesBadCode.ScreenShotManager.Tests
{
    public class ScreenshotHandlerBuildPathTests
    {
        [Test]
        public void When_PngFormat_Expect_DotPngExtension()
        {
            string path = ScreenshotHandler.BuildPath("Screenshots", "Test", ScreenshotFormat.PNG);
            Assert.That(Path.GetExtension(path), Is.EqualTo(".png"));
        }

        [Test]
        public void When_JpgFormat_Expect_DotJpgExtension()
        {
            string path = ScreenshotHandler.BuildPath("Screenshots", "Test", ScreenshotFormat.JPG);
            Assert.That(Path.GetExtension(path), Is.EqualTo(".jpg"));
        }

        [Test]
        public void When_CustomPrefixProvided_Expect_PrefixInFilename()
        {
            const string prefix = "MyCustomPrefix";
            string path = ScreenshotHandler.BuildPath("Screenshots", prefix, ScreenshotFormat.PNG);
            Assert.That(Path.GetFileName(path), Does.StartWith(prefix + "_"));
        }
    }
}
