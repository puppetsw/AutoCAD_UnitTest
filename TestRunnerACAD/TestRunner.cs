// *******************************************
// Simple TestRunner for AutoCAD 2017.
// *******************************************

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using NUnitLite;
using TestRunnerACAD;

[assembly: CommandClass(typeof(TestRunner))]
namespace TestRunnerACAD
{
    public static class TestRunner
    {
        [CommandMethod("RunTests", CommandFlags.Session)]
        public static void RunTests()
        {
            var directoryPlugin = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryReportUnit = Path.Combine(directoryPlugin, @"ReportUnit");
            Directory.CreateDirectory(directoryReportUnit);
            var fileInputXML = Path.Combine(directoryReportUnit, @"Report-NUnit.xml");
            var fileOutputHTML = Path.Combine(directoryReportUnit, @"Report-NUnit.html");
            var generatorReportUnit = Path.Combine(directoryPlugin, @"ReportUnit", @"ReportUnit.exe");

            var nunitArgs = new List<string>
            {
                "--trace=verbose"
                ,"--result=" + fileInputXML
            }.ToArray();

            new AutoRun().Execute(nunitArgs);

            CreateHtmlReport(fileInputXML, fileOutputHTML, generatorReportUnit);

            Process.Start(fileOutputHTML);
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

                var param = new StringBuilder();
                param.AppendFormat($" \"{inputFile}\"");
                param.AppendFormat($" \"{outputFile}\"");
                process.StartInfo.Arguments = param.ToString();

                process.Start();

                // Read the output stream to end.
                // This will make this process wait until AutoCAD finishes.
                // Thank you CADBloke.
                using (var outputStream = process.StandardOutput)
                {
                    outputStream.ReadToEnd();
                }
            }
        }
    }
}