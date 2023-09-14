using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnitLite;

namespace TestRunnerACAD
{
    public class TestRunnerBase
    {
        public void RunTestsBase(Assembly assembly, string directoryPlugin)
        {
            if (directoryPlugin == null)
                return;

            var directoryReportUnit = Path.Combine(directoryPlugin, TestRunnerConsts.ReportToolFolderName);
            Directory.CreateDirectory(directoryReportUnit);
            var fileInputXml = Path.Combine(directoryReportUnit, TestRunnerConsts.ReportNunitXml);
            if (File.Exists(fileInputXml))
                File.Delete(fileInputXml);
            var nunitArgs = new List<string>
            {
                "--trace=verbose", "--result=" + fileInputXml
            }.ToArray();

            new AutoRun(assembly).Execute(nunitArgs);
        }
    }
}