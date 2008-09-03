using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.SharePoint;
using System.Net;
using SharepointUtilities;
using Microsoft.SharePoint.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        try
        {
            XmlNode node = Utilities.GetListCollection();
            

        }
        catch (Exception exc)
        {
            this.lblCount.Text = exc.Message;
        }
    }
}
