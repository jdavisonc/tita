<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaIncidentes.aspx.cs"
    Inherits="ListaIncidentes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">

    <script type="text/javascript" language="javascript">
    function DeleteItem()
    {
        return confirm("Desea eliminar este item?");
    }
    </script>

    <div>
        <asp:Panel ID="pnlView" runat="server">
            <div style="text-align: center;">
                <br />
                <asp:GridView ID="gvIncidentes" HorizontalAlign="Center" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="ID" OnRowCommand="gvIncidentes_RowCommand" OnRowDeleting="gvIncidentes_RowDeleting">
                    <RowStyle ForeColor="#939393" />
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Titulo" />
                        <asp:BoundField DataField="ReportedBy" HeaderText="Reported by" />
                        <asp:BoundField DataField="ReportedDate" HeaderText="Reported Date" />
                        <asp:BoundField DataField="WP" HeaderText="WP" />
                        <asp:BoundField DataField="Ord" HeaderText="Priority Order" />
                        <asp:BoundField DataField="Priority" HeaderText="Priority" />
                        <asp:BoundField DataField="Category" HeaderText="Category" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="Resolution" HeaderText="Resolution" />
                        <asp:BoundField DataField="LinkIssueIdNoMenu" HeaderText="Related Issue" />
                        <asp:TemplateField>
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:Button ID="imgEdit" runat="server" CommandName="Select" ImageUrl="../img/view.gif"
                                    CommandArgument='<%# Eval("ID") %>' Text="Modificar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="imgDelete" OnClientClick="return DeleteItem():" runat="server" CommandArgument='<%# Eval("ID") %>'
                                    Text="Borrar" CommandName="Delete" ImageUrl="../img/cruz.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle ForeColor="Black" />
                </asp:GridView>
                <br />
                <br />
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
