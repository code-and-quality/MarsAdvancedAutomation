using OpenQA.Selenium;

namespace MarsAdvancedAutomation.Helpers
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(
            IWebDriver driver,
            string testName)
        {
            string screenshotFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Screenshots");

            Directory.CreateDirectory(screenshotFolder);

            string fileName =
                $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

            string fullPath =
                Path.Combine(screenshotFolder, fileName);

            Screenshot screenshot =
                ((ITakesScreenshot)driver).GetScreenshot();

            screenshot.SaveAsFile(fullPath);

            return fullPath;
        }
    }
}