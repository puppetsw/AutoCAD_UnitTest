using System.IO;
using System.Reflection;
using ACADSampleTest;
using Autodesk.AutoCAD.Runtime;
using TestRunnerACAD;

[assembly: CommandClass(typeof(TestRunnerNew))]

namespace ACADSampleTest
{
    public class TestRunnerNew : TestRunnerBase
    {
        [CommandMethod("RunTests", CommandFlags.Session)]
        public void RunTests()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var directoryPlugin = Path.GetDirectoryName(assembly.Location);
            RunTestsBase(assembly, directoryPlugin);
        }
    }
}