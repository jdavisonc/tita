using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Infocorp.TITA.SharePointUtilities;
using Infocorp.TITA.DataTypes;

namespace Infocorp.TITA.WITLogic.Tests
{
[TestFixture]
    public class WITServices_WP_Test
    {
        [Test]
        public void AddWP()
        {
            string siteUrl = "1";
            ISharePoint sharepoint = SharePointUtilities.SharePointUtilities.GetInstance().GetISharePoint();            
            DTItem issue = new DTItem();
            issue.Attachments = new List<DTAttachment>();
            issue.Fields = sharepoint.GetFieldsWorkPackage(siteUrl);
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
                int originalCount = sharepoint.GetWorkPackages(siteUrl, string.Empty).Count;
                WITFactory.Instance().WITServicesInstance().AddWorkPackage(issue, siteUrl);
                WITFactory.Instance().WITServicesInstance().ApplyChanges(siteUrl, ItemType.WORKPACKAGE);

                int newCount = sharepoint.GetWorkPackages(siteUrl, string.Empty).Count;

                Assert.AreEqual(originalCount + 1, newCount);
            }
            catch (Exception exc)
            {
                Assert.Fail("No se agregó el wp. ", exc.Message);

            }

        }
    }
}
