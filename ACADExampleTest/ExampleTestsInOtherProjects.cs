using System.Threading;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using NUnit.Framework;
using TestRunnerACAD;

namespace ACADExampleTest
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class ExampleTestsInOtherProjects : TestBase
    {
        [Test]
        public void OtherTest_Pass()
        {
            Assert.Pass("This test should always passes.");
        }

        [Test]
        public void OtherTest_Fail()
        {
            Assert.Fail("This test should always fail.");
        }

        [Test]
        public void OtherTest_Add_Line()
        {
            //Use a new drawing
            long lineId = 0;

            void Action1(Database db, Transaction tr)
            {
                var line = new Line(new Point3d(0, 0, 0), new Point3d(100, 100, 100));

                var blockTable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                var modelSpace =
                    (BlockTableRecord)tr.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                var objectId = modelSpace.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                lineId = objectId.Handle.Value;
            }

            void Action2(Database db, Transaction tr)
            {
                //Check in another transaction if the line was created

                ObjectId objectId;
                if (!db.TryGetObjectId(new Handle(lineId), out objectId)) Assert.Fail("Line didn't created");
            }

            // Run the tests
            ExecuteTestActions(null, Action1, Action2);
        }
    }
}