<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHorasInvestigacion.aspx.vb" Inherits="personal_frmHorasInvestigacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
     <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D9534F" colspan="2" height="30px">
                    <b>
                        <asp:Label ID="lblTitulo" runat="server" Text="Lista de horas de investigaci&oacute;n" 
                        ForeColor="White"></asp:Label></b>
                </td>
            </tr>
            </table>
    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="PERIODO"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPeriodoLaboral" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            
                            <td>
                                
                                <asp:CheckBox ID="chkFomrato" runat="server" AutoPostBack="True" Checked="True" 
                                    ForeColor="Red" Text="Sin Formato HH:MM" />
                            </td>
                            <td>
                                
                                <asp:Button ID="btnExportar" CssClass="excel" runat="server" Text="Exportar" 
                                    Width="132px" />
                                
                            </td>
                        </tr>
                        <tr>
                        <td >
                                <asp:Label ID="Label2" runat="server" Text="DEP.ACAD / ÁREA"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlDptAcad" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
    </div>
    <asp:GridView ID="gvActividad" runat="server" Width="100%" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="DepAcadArea" HeaderText="DEPARTAMENTO ACADÉMICO" />
            <asp:BoundField DataField="descripcion_Tpe" HeaderText="TIPO PERSONAL" />
            <asp:BoundField DataField="Descripcion_Ded" HeaderText="DEDICACION" />
            <asp:BoundField DataField="trabajador" HeaderText="TRABAJADOR" />
            <asp:BoundField DataField="Investigacion" HeaderText="HRS. INVEST." />
            <asp:BoundField DataField="Tesis" HeaderText="Nº TESIS" />
        </Columns>
        <EmptyDataTemplate>
            No se encontraron registros!
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
        <RowStyle Font-Size="11px" Height="20px" />
        <EditRowStyle BackColor="#ffffcc" />
        <EmptyDataRowStyle ForeColor="Red" />
    </asp:GridView>
    </form>
</body>
</html>
