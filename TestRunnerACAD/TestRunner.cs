// *******************************************
// Simple
// *******************************************

using System.Collections.Generic;
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
            string[] nunitArgs = new List<string>
            {
                // for details of options see  https://github.com/nunit/docs/wiki/Console-Command-Line
                "--trace=verbose" // Tell me everything
                ,"--result=" + "report.xml"
                // , "--work=" + directoryName // save TestResults.xml to the build folder
                //, "--wait" // Wait for input before closing console window (PAUSE). Comment this out for batch operations.
            }.ToArray();

            new AutoRun().Execute(nunitArgs);
        }
    }
}