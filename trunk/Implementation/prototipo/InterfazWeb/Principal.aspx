<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="InterfazWeb._Default" %>

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
        <br />
        <br />
        <br />
    
    </div>
    <table style="width:100%;">
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button9" runat="server" onclick="Button9_Click" 
                    Text="Cargar lista de incidentes" Width="221px" />
            </td>
           
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button10" runat="server" onclick="Button10_Click" 
                    Text="Alta incidente" Width="221px" />
            </td>
           
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button11" runat="server" onclick="Button11_Click" 
                    Text="Modificar incidente" Width="221px" />
            </td>
            
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="Button12" runat="server" onclick="Button12_Click" 
                    Text="Eliminar incidente" Width="221px" />
            </td>
            
        </tr>
    </table>
    </form>
</body>
</html>
