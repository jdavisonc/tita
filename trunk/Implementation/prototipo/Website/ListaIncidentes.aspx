<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaIncidentes.aspx.cs" Inherits="ListaIncidentes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
    <asp:Panel ID="pnlView" runat="server">
                    <div style="text-align: center;">                        
                                              
                        <br />
                        <asp:GridView ID="gvIncidentes" HorizontalAlign="Center" runat="server" 
                            AutoGenerateColumns="False" onrowdatabound="gvIncidentes_RowDataBound" DataKeyNames="ID">
                            <RowStyle ForeColor="#939393" />
                            <Columns>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField>
                                    <ItemStyle Width="10px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" runat="server" CommandName="Select" ImageUrl="../img/view.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="10px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgDelete" OnClientClick="return DeleteItem()" runat="server"
                                            CommandName="delete" ImageUrl="../img/cruz.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
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
