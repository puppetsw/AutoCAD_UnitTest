// *******************************************
// Simple
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
            string directoryPlugin = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string directoryReportUnit = Path.Combine(directoryPlugin, @"ReportUnit");
            Directory.CreateDirectory(directoryReportUnit);
            string fileInputXML = Path.Combine(directoryReportUnit, @"Report-NUnit.xml");
            string fileOutputHTML = Path.Combine(directoryReportUnit, @"Report-NUnit.html");
            string generatorReportUnit = Path.Combine(directoryPlugin, @"ReportUnit", @"ReportUnit.exe");

            string[] nunitArgs = new List<string>
            {
                // for details of options see  https://github.com/nunit/docs/wiki/Console-Command-Line
                "--trace=verbose" // Tell me everything
                ,"--result=" + fileInputXML
                // , "--work=" + directoryName // save TestResults.xml to the build folder
                //, "--wait" // Wait for input before closing console window (PAUSE). Comment this out for batch operations.
            }.ToArray();

            new AutoRun().Execute(nunitArgs);

            CreateHTMLReport(fileInputXML, fileOutputHTML, generatorReportUnit);

            Process.Start(fileOutputHTML);
        }

        private static void CreateHTMLReport(string pFileInputXML, string pFileOutputHTML, string pGeneratorReportUnit)
        {
            if (!File.Exists(pFileInputXML))
                return;

            if (File.Exists(pFileOutputHTML))
                File.Delete(pFileOutputHTML);

            string output = string.Empty;
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.StartInfo.FileName = pGeneratorReportUnit;

                    StringBuilder param = new StringBuilder();
                    param.AppendFormat(" \"{0}\"", pFileInputXML);
                    param.AppendFormat(" \"{0}\"", pFileOutputHTML);
                    process.StartInfo.Arguments = param.ToString();

                    process.Start();

                    // read the output to return
                    // this will stop this execute until AutoCAD exits
                    StreamReader outputStream = process.StandardOutput;
                    output = outputStream.ReadToEnd();
                    outputStream.Close();
                }

            }
            catch (System.Exception ex)
            {
                output = ex.Message;
            }

        }
    }
}