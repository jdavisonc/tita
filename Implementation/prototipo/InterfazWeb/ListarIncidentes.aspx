<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarIncidentes.aspx.cs" Inherits="InterfazWeb.ListarIncidentes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
        <asp:GridView ID="GridView1" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                            PageSize="30" AllowPaging="true"> 
                            <RowStyle ForeColor="#939393" />
                            <Columns>
                                <asp:BoundField DataField="Title" HeaderText="Title" ItemStyle-CssClass="displayNone" HeaderStyle-CssClass="displayNone" />
                                <asp:BoundField DataField="Description" ItemStyle-Width="200px" HeaderText="Description" />
                                <asp:BoundField DataField="Reported by" ItemStyle-Width="200px" HeaderText="Reported by" />                                
                                <asp:BoundField DataField="Reported Date" ItemStyle-Width="200px" HeaderText="Reported Date" />                                
                                <asp:BoundField DataField="WP" ItemStyle-Width="200px" HeaderText="WP" />
                                <asp:BoundField DataField="Assigned To" ItemStyle-Width="200px" HeaderText="Assigned To" />
                                <asp:BoundField DataField="Priority" ItemStyle-Width="200px" HeaderText="Priority" />
                                
                            </Columns>
            </asp:GridView>
    
    
    
    </div>
    </form>
</body>
</html>
