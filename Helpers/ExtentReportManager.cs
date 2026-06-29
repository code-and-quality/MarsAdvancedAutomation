using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Diagnostics;

namespace MarsAdvancedAutomation.Helpers
{
    public static class ExtentReportManager
    {
        public static ExtentReports Extent;
        public static ExtentTest Test;
        public static string reportPath;

        public static void InitializeReport()
        {
            string reportFolder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Reports");

            Directory.CreateDirectory(reportFolder);

            string reportPath = Path.Combine(
                reportFolder,
                $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");

            var sparkReporter = new ExtentSparkReporter(reportPath);

            sparkReporter.Config.DocumentTitle = "Mars Automation Report";
            sparkReporter.Config.ReportName = "Automation Test Results";

            Extent = new ExtentReports();
            Extent.AttachReporter(sparkReporter);

            Extent.AddSystemInfo("Project", "Mars Advanced Automation");
            Extent.AddSystemInfo("Framework", "Selenium NUnit");
            Extent.AddSystemInfo("Tester", "Susmitha");
        }

        public static void FlushReport()
        {
            Extent?.Flush();
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = reportPath,
                UseShellExecute = true
            });
        }
    }
}