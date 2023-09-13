using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using NUnitLite;
using TestRunnerACAD;

[assembly: CommandClass(typeof(TestRunner))]

namespace TestRunnerACAD
{
    public static class TestRunner
    {
        public const string ReportToolFolderName = @"ExtentReports";
        public const string ReportNunitXml = @"Report-NUnit.xml";

        [CommandMethod("RunTests", CommandFlags.Session)]
        public static void RunTests()
        {
            var directoryPlugin = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (directoryPlugin == null)
                return;

            var directoryReportUnit = Path.Combine(directoryPlugin, ReportToolFolderName);
            Directory.CreateDirectory(directoryReportUnit);
            var fileInputXml = Path.Combine(directoryReportUnit, ReportNunitXml);

            var nunitArgs = new List<string>
            {
                "--trace=verbose", "--result=" + fileInputXml
            }.ToArray();

            new AutoRun().Execute(nunitArgs);
        }
    }
}