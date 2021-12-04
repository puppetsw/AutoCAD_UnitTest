using System.Diagnostics;
using System.IO;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using TestRunnerACAD;

[assembly: ExtensionApplication(typeof(TestPlugin))]
namespace TestRunnerACAD
{
    public class TestPlugin : IExtensionApplication
    {
        public void Initialize()
        {
            // Don't need to do anything here.
        }

        public void Terminate()
        {
            var directoryPlugin = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (directoryPlugin == null)
                return;

            var directoryReportUnit = Path.Combine(directoryPlugin, @"ReportUnit");
            Directory.CreateDirectory(directoryReportUnit);
            var fileInputXml = Path.Combine(directoryReportUnit, @"Report-NUnit.xml");
            var fileOutputHtml = Path.Combine(directoryReportUnit, @"Report-NUnit.html");
            var generatorReportUnit = Path.Combine(directoryPlugin, @"ReportUnit", @"ReportUnit.exe");

            CreateHtmlReport(fileInputXml, fileOutputHtml, generatorReportUnit);
            OpenHtmlReport(fileOutputHtml);
        }

        /// <summary>
        /// Opens a HTML report with the default viewer.
        /// </summary>
        /// <param name="fileName"></param>
        private static void OpenHtmlReport(string fileName)
        {
            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.FileName = fileName;
                process.Start();
            }
        }

        /// <summary>
        /// Creates a HTML report based on the NUnit XML report.
        /// </summary>
        /// <param name="inputFile">The NUnit XML file.</param>
        /// <param name="outputFile">The output HTML report file.</param>
        /// <param name="reportUnitPath">Path to the ReportUnit executable.</param>
        private static void CreateHtmlReport(string inputFile, string outputFile, string reportUnitPath)
        {
            if (!File.Exists(inputFile))
                return;

            if (File.Exists(outputFile))
                File.Delete(outputFile);

            using (var process = new Process())
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = reportUnitPath;
                process.StartInfo.Arguments = $" \"{inputFile}\" \"{outputFile}\"";

                process.Start();
                process.WaitForExit();
            }
        }
    }
}