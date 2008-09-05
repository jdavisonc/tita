<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModificarIncidente.aspx.cs" Inherits="ModificarIncidente"  ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width:57%; height: 384px; margin-top: 121px;" align="center">
            <tr>
                <td class="style2" style="text-align: left">
                    <asp:Label ID="Label15" runat="server" Text="Lista:"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="dropDownLista" runat="server" Height="25px" 
                        style="text-align: left" Width="197px" Enabled="false">
                        <asp:ListItem Text="Issues" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style9" style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="Titulo:"></asp:Label>
                </td>
                <td class="style10">
                    <asp:TextBox ID="txtTitulo" runat="server" style="text-align: left" 
                        Width="315px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style5" style="text-align: left">
                    <asp:Label ID="Label2" runat="server" Text="Descripcion:"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="text-align: left" class="style6">
                    <asp:TextBox ID="txtDescripcion" runat="server" Height="112px" Width="312px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="Reportado por:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="dropDownReportado" runat="server" Height="25px" 
                        style="text-align: left" Width="197px">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Text="Fecha de reporte:"></asp:Label>
                </td>
                <td style="text-align: left">
                <asp:UpdatePanel ID="upCalendario" runat="server">
                <ContentTemplate>
                <asp:Calendar ID="calendar2" runat="server" BackColor="White" 
                        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
                        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
                        Width="200px">
                        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" />
                        <WeekendDayStyle BackColor="#FFFFCC" />
                        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <OtherMonthDayStyle ForeColor="#808080" />
                        <NextPrevStyle VerticalAlign="Bottom" />
                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    </asp:Calendar>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                
            </tr>
            
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Text="Prioridad:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="dropDownPrioridad" runat="server" Height="28px" 
                        style="text-align: left" Width="120px">
                        <asp:ListItem Text="Normal"></asp:ListItem>
                        <asp:ListItem Text="High"></asp:ListItem>
                        <asp:ListItem Text="Low"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label7" runat="server" Text="Orden de prioridad:"></asp:Label>
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtOrden" runat="server" Height="24px" Width="139px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label8" runat="server" Text="Categoria:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="dropDownCategoria" runat="server" Height="26px" 
                        style="text-align: left" Width="120px">
                        <asp:ListItem Text="Change"></asp:ListItem>
                        <asp:ListItem Text="Error"></asp:ListItem>
                        <asp:ListItem Text="Query"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label9" runat="server" Text="Estado:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="dropDownEstado" runat="server" Height="26px" 
                        style="text-align: left" Width="120px">
                        <asp:ListItem Text="Reported"></asp:ListItem>
                        <asp:ListItem Text="Active"></asp:ListItem>
                        <asp:ListItem Text="Resolved"></asp:ListItem>
                        <asp:ListItem Text="Closed"></asp:ListItem>
                        <asp:ListItem Text="Canceled"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
           
            <tr>
                <td class="style7" style="text-align: left">
                    <asp:Label ID="Label12" runat="server" Text="Resolucion:"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td class="style8" style="text-align: left">
                    <asp:TextBox ID="txtResolucion" runat="server" Height="76px" Width="312px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label13" runat="server" Text="Asociado al incidente:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtAsociado" runat="server" Height="24px" Width="139px"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" Text="*" ErrorMessage="Debe ingresar un entero" ControlToValidate="txtAsociado" Type="Integer"></asp:RangeValidator>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    &nbsp;</td>
                <td >
                    <br />
                    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" onclick="Button1_Click" />&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" onclick="ButtonCancel_Click" />
                </td>
                
            </tr>
        </table>
    
    </div>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    </form>
   
</body>
</html>
