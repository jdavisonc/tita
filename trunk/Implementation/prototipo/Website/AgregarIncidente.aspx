<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgregarIncidente.aspx.cs" Inherits="AgregarIncidente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table style="width:57%; height: 384px; margin-top: 121px;" align="center">
            <tr>
                <td class="style9" style="text-align: left">
                    <asp:Label ID="Label1" runat="server" Text="Titulo:"></asp:Label>
                </td>
                <td class="style10">
                    <asp:TextBox ID="txtTitle" runat="server" style="text-align: left" 
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
                    <asp:TextBox ID="txtDescription" runat="server" Height="112px" Width="312px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label3" runat="server" Text="Reportado por:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlReported" runat="server" Height="25px" 
                        style="text-align: left" Width="197px" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label4" runat="server" Text="Fecha de reporte:"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="text-align: left">
                    <asp:UpdatePanel ID="up1" runat="server"><ContentTemplate>    
                    <asp:Calendar ID="calendarReportDate" runat="server"></asp:Calendar>
                    </ContentTemplate> </asp:UpdatePanel>                
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label14" runat="server" Text="Work Package:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlWp" runat="server" Height="26px" 
                        style="text-align: left" Width="107px">
                    </asp:DropDownList>
                </td>
                
            </tr>
            
            
            
            
            
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label5" runat="server" Text="Asignado a:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlAsignedTo" runat="server" Height="23px" 
                        style="text-align: left" Width="160px">
                    </asp:DropDownList>
                
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label6" runat="server" Text="Prioridad:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlPriority" runat="server" Height="28px" 
                        style="text-align: left" Width="120px">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label7" runat="server" Text="Orden de prioridad:"></asp:Label>
                </td>
                <td class="style4">
                    <asp:TextBox ID="txtPriorityOrder" runat="server" Height="24px" Width="139px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label8" runat="server" Text="Categoria:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlCategory" runat="server" Height="26px" 
                        style="text-align: left" Width="120px">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label9" runat="server" Text="Estado:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="ddlStatus" runat="server" Height="26px" 
                        style="text-align: left" Width="120px">
                    </asp:DropDownList>
                </td>
                
            </tr>
            <tr>
                <td class="style11" style="text-align: left">
                    <asp:Label ID="Label10" runat="server" Text="Comentarios:"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="text-align: left" class="style12">
                    <asp:TextBox ID="txtComment" runat="server" Height="105px" Width="312px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label11" runat="server" Text="Fecha de fin:"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                    <asp:Calendar ID="CalendarDueDate" runat="server" BackColor="White" 
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
                    </ContentTemplate> </asp:UpdatePanel>
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
                    <asp:TextBox ID="txtResolution" runat="server" Height="76px" Width="312px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    <asp:Label ID="Label13" runat="server" Text="Asociado al incidente:"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtAsignedRelatedIssue" runat="server" Height="24px" 
                        Width="139px" ontextchanged="txtAsignedRelatedIssue_TextChanged"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" Text="*" ErrorMessage="Debe ingresar un entero" ControlToValidate="txtAsignedRelatedIssue" Type="Integer"></asp:RangeValidator>
                </td>
                
            </tr>
            <tr>
                <td class="style1" style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">
                    <br />
&nbsp;<asp:Button ID="buttonAcept" runat="server" Text="Aceptar" onclick="Button1_Click" 
                        Width="70px" />
                &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Cancelar" 
                        Width="70px" />
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

    </form>

</body>
</html>
