using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.SharePointUtilities.Test
{
    [TestFixture]
    public class TestSharePointUtilities
    {
        private const string _idContract = "1";
        private const string _idName = "ID"; /* Depende de si es el template en espanol o ingles. */

        [TestFixtureSetUp]
        public void Init()
        { /* Se usa para inicializar partes comunes del Test y no inicializar en c/u ... */ }

        [TestFixtureTearDown]
        public void Cleanup()
        { /* Para liberar partes que se hayan usado en los test comunes ... */ }

        private DTItem CreateItem(ISharePoint sharepoint)
        {
            DTItem issue = new DTItem();
            issue.Attachments = new List<DTAttachment>();
            issue.Fields = sharepoint.GetFieldsIssue(_idContract);
            string name = string.Empty;
            foreach (DTField field in issue.Fields)
            {
                try
                {
                    if (!field.Hidden && !field.IsReadOnly)
                    {
                        switch (field.GetCustomType())
                        {
                            case DTField.Types.Number:
                                (field as DTFieldAtomicNumber).Value = 0;
                                break;
                            case DTField.Types.String:
                                (field as DTFieldAtomicString).Value = "Test Funcional";
                                break;
                            case DTField.Types.Choice:
                                (field as DTFieldChoice).Value = (field as DTFieldChoice).Choices.First();
                                break;
                            case DTField.Types.Boolean:
                                (field as DTFieldAtomicBoolean).Value = false;
                                break;
                            case DTField.Types.DateTime:
                                (field as DTFieldAtomicDateTime).Value = DateTime.Today.AddDays(5);
                                break;
                            case DTField.Types.Note:
                                (field as DTFieldAtomicNote).Value = "Nota";
                                break;
                            case DTField.Types.User:
                                (field as DTFieldChoiceUser).Value = (field as DTFieldChoiceUser).Choices.First();
                                break;
                            case DTField.Types.Counter:
                                break;
                            case DTField.Types.Lookup:
                                break;
                            case DTField.Types.Default:
                                break;
                            default:
                                break;
                        }
                    }
                }
                catch (Exception exc)
                {
                    string msg = exc.Message;
                }
            }
            return issue;
        }

        [Test]
        public void TestAddItem()
        {
            ISharePoint sharepoint = SharePointUtilities.GetInstance().GetISharePoint();
            DTItem issue = CreateItem(sharepoint);
            try
            {
                int originalCount = sharepoint.GetIssues(_idContract, string.Empty).Count;
                sharepoint.AddIssue(_idContract, issue);
                int newCount = sharepoint.GetIssues(_idContract, string.Empty).Count;

                Assert.AreEqual(originalCount + 1, newCount);
            }
            catch (Exception exc)
            {
                Assert.Fail("No se agregó el issue. ", exc.Message);
            }
        }

        [Test]
        public void TestRemoveItem()
        {
            try
            {
                ISharePoint sharepoint = SharePointUtilities.GetInstance().GetISharePoint();
                List<DTItem> items = sharepoint.GetIssues(_idContract, string.Empty);
                bool isIn = false;
                int id = 0;
                foreach (DTItem item in items)
                {
                    foreach (DTField field in item.Fields)
                    {
                        if ((field.GetCustomType() == DTField.Types.String) &&
                            (((DTFieldAtomicString)field).Value.CompareTo("Test Funcional") == 0))
                        {
                            isIn = true;
                            break;
                        }
                    }
                    if (isIn)
                    {
                        id = ((DTFieldCounter)item.GetField(_idName)).Value;
                        break;
                    }
                }
                if (id == 0)
                {
                    DTItem issue = CreateItem(sharepoint);
                    sharepoint.AddIssue(_idContract, issue);
                    List<DTItem> items2 = sharepoint.GetIssues(_idContract, string.Empty);
                    isIn = false;
                    foreach (DTItem item in items2)
                    {
                        foreach (DTField field in item.Fields)
                        {
                            if ((field.GetCustomType() == DTField.Types.String) &&
                                (((DTFieldAtomicString)field).Value.CompareTo("Test Funcional") == 0))
                            {
                                isIn = true;
                                break;
                            }
                        }
                        if (isIn)
                        {
                            id = ((DTFieldCounter)item.GetField(_idName)).Value;
                            break;
                        }
                    }
                }
                if (id == 0)
                {
                    Assert.Fail("No se borro el issue. ");
                }
                else
                {
                    int originalCount = sharepoint.GetIssues(_idContract, string.Empty).Count;
                    sharepoint.DeleteIssue(_idContract, id);
                    int newCount = sharepoint.GetIssues(_idContract, string.Empty).Count;

                    Assert.AreEqual(originalCount - 1, newCount);
                }
            }
            catch (Exception exc)
            {
                Assert.Fail("No se elimino el issue. ", exc.Message);
            }
        }
    }
}
