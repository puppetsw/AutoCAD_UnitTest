using System.IO;
using System.Reflection;
using ACADExampleTest;
using Autodesk.AutoCAD.Runtime;
using TestRunnerACAD;

[assembly: CommandClass(typeof(TestRunner))]

namespace ACADExampleTest
{
    public class TestRunner : TestRunnerBase
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