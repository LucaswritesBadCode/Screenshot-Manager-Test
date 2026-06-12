using System.IO;
using LucasWritesBadCode.ScreenshotHelper.Runtime;
using NUnit.Framework;

namespace LucasWritesBadCode.ScreenshotHelper.Tests.Editor
{
    public class ScreenshotHandlerBuildPathTests
    {
        [Test]
        public void When_PngFormat_Expect_DotPngExtension()
        {
            string path = ScreenshotHandler.BuildPath("Screenshots", "Test", ScreenshotFormat.Png);
            Assert.That(Path.GetExtension(path), Is.EqualTo(".png"));
        }
        
        [Test]
        public void When_JpgFormat_Expect_DotJpgExtension()
        {
            string path = ScreenshotHandler.BuildPath("Screenshots", "Test", ScreenshotFormat.Jpg);
            Assert.That(Path.GetExtension(path), Is.EqualTo(".jpg"));
        } 
        
        [Test]
        public void When_CustomPrefixProvided_Expect_PrefixInFilename()
        {
            const string prefix = "MyCustomPrefix";
            string path = ScreenshotHandler.BuildPath("Screenshots", prefix, ScreenshotFormat.Png);
            Assert.That(Path.GetFileName(path), Does.StartWith(prefix + "_"));
        }
    }
}
