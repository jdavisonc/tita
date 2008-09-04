<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eliminarIncidente.aspx.cs" Inherits="InterfazWeb.eliminarIncidente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <br />
    
    </div>
    <table style="width: 46%; height: 92px;">
        <tr>
            <td class="style3">
                Lista:</td>
            <td class="style2">
                <asp:DropDownList ID="DropDownList1" runat="server" Width="227px">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td class="style3">
                Incidente:</td>
            <td class="style2">
                <asp:DropDownList ID="DropDownList2" runat="server" Width="136px" Height="23px" 
                    style="text-align: left">
                </asp:DropDownList>
            </td>
            
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style2">
                <br />
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Eliminar" />
                <br />
                <br />
            </td>
           
        </tr>
    </table>
    </form>
</body>
</html>
