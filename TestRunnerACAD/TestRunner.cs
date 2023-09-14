using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using NUnitLite;
using TestRunnerACAD;

[assembly: CommandClass(typeof(TestRunner))]

namespace TestRunnerACAD
{
    public class TestRunner : TestRunnerBase
    {
        [CommandMethod("RunTests", CommandFlags.Session)]
        public void RunTests()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string directoryPlugin = Path.GetDirectoryName(assembly.Location);
            RunTestsBase(assembly, directoryPlugin);
        }
    }
}