using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Infocorp.TITA.SharePointUtilities;
using Rhino.Mocks;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic.Tests
{
    [TestFixture]
    public class WITServices_Tasks_Test
    {
        IWITServices witServices;
        MockRepository mocks;
        DataBaseAccess.DataBaseAccess dbMock;
        ISharePoint suMock;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {

        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            dbMock = mocks.CreateMock<DataBaseAccess.DataBaseAccess>();
            suMock = mocks.CreateMock<ISharePoint>();
            witServices = WITFactory.Instance().WITServicesInstance(dbMock, suMock);
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        #region Tests Unitarios

        [Test]
        public void MustGetTaskTemplate()
        {
            string contractId = "1";
            List<DTField> fields = new List<DTField>() { new DTFieldAtomicString("Test", "Test", true, false, true) };
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.GetFieldsTask(contractId)).Return(fields);
            }

            try
            {
                DTItem taskTemplate = witServices.GetTaskTemplate(contractId);

                Assert.AreEqual(fields.Count, taskTemplate.Fields.Count);
                foreach (DTField field in taskTemplate.Fields)
                {
                    Assert.Contains(field, fields);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustGetTasks()
        {
            string contractId = "1";
            List<DTItem> tasks = new List<DTItem>() { new DTItem(new List<DTField>(), new List<DTAttachment>()) };
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.GetTasks(contractId, string.Empty)).Return(tasks);
            }

            try
            {
                List<DTItem> result = witServices.GetTasks(contractId);

                Assert.AreEqual(tasks.Count, result.Count);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustAddNewTask()
        {
            DTItem task = new DTItem(new List<DTField>(), new List<DTAttachment>());
            string contractId = "1";
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.AddTask(contractId, task)).Return(true);
            }

            try
            {
                witServices.AddTask(task, contractId);
                witServices.ApplyChanges(contractId, ItemType.TASK);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustModifyTask()
        {
            DTItem task = new DTItem(new List<DTField>(), new List<DTAttachment>());
            task.Fields.Add(new DTFieldCounter("ID", "ID", true, true, true, 1));
            string contractId = "1";
            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.UpdateTask(contractId, task)).Return(true);
            }

            try
            {
                witServices.UpdateTask(task, contractId);
                witServices.ApplyChanges(contractId, ItemType.TASK);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        [Test]
        public void MustDeleteTask()
        {
            string contractId = "1";
            int taskId = 1;

            using (mocks.Record())
            {
                Expect.On(suMock).Call(suMock.DeleteTask(contractId, taskId)).Return(true);
            }

            try
            {
                witServices.DeleteTask(taskId, contractId);
                witServices.ApplyChanges(contractId, ItemType.TASK);
            }
            catch (Exception exc)
            {
                Assert.Fail(exc.Message);
            }
        }

        #endregion
    }
}
