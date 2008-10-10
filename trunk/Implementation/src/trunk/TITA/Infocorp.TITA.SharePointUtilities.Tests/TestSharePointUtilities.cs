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

        [TestFixtureSetUp]
        public void Init()
        { /* Se usa para inicializar partes comunes del Test y no inicializar en c/u ... */ }

        [TestFixtureTearDown]
        public void Cleanup()
        { /* Para liberar partes que se hayan usado en los test comunes ... */ }

        [Test]
        public void UnTest()
        {
            string siteUrl = "http://localhost/infocorp";
            ISharePoint sharepoint = SharePointUtilities.GetInstance().GetISharePoint();
            DTItem issue = new DTItem();
            issue.Attachments = new List<DTAttachment>();
            issue.Fields = sharepoint.GetFieldsIssue(siteUrl);
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
                                (field as DTFieldAtomicString).Value = "0";
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
            try
            {
                int originalCount = sharepoint.GetIssues(siteUrl, string.Empty).Count;
                sharepoint.AddIssue(siteUrl, issue);
                int newCount = sharepoint.GetIssues(siteUrl, string.Empty).Count;

                Assert.AreEqual(originalCount + 1, newCount);
            }
            catch (Exception exc)
            {
                Assert.Fail("No se agregó el issue. ", exc.Message);

            }


        }

        [Test]
        public void UnTestOK()
        {
          /*Suma oSuma = new Suma(2, 3);
            Assert.AreEqual(5, oSuma.SumaElem());
            Suma oSuma2 = new Suma(3, 3);
            Assert.AreEqual(6, oSuma2.SumaElem());*/

        }
    }
}
