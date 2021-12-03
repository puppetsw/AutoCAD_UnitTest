using System;
using System.Threading;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using NUnit.Framework;

namespace TestRunnerACAD
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class ExampleTests : TestBase
    {
        [Test]
        public void Test_Pass()
        {
            Assert.Pass("This test should always passes.");
        }

        [Test]
        public void Test_method_new_drawing()
        {
            //Use a new drawing
            long lineId = 0;

            Action<Database, Transaction> action1 = (db, trans) =>
            {
                var line = new Line(new Point3d(0, 0, 0), new Point3d(100, 100, 100));

                var blockTable = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                var modelSpace = (BlockTableRecord)trans.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                var objectId = modelSpace.AppendEntity(line);
                trans.AddNewlyCreatedDBObject(line, true);

                lineId = objectId.Handle.Value;
            };

            Action<Database, Transaction> action2 = (db, trans) =>
            {
                //Check in another transaction if the line was created

                ObjectId objectId;
                if (!db.TryGetObjectId(new Handle(lineId), out objectId))
                {
                    Assert.Fail("Line didn't created");
                }
            };

            ExecuteTestActions(null, action1, action2);
        }
    }
}