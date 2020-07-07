<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVistaEvaluacionAnual.aspx.vb" Inherits="indicadores_frmVistaEvaluacionAnual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
        <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
        <script src="../../private/jq/jquery.js" type="text/javascript"></script>
        <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" colspan="6" height="30px">
                <b>
                    <asp:Label ID="Label4" runat="server" Text="Informes de la Evaluación Anual - BSC"></asp:Label></b></td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                        <tr>
                            <td style="width:150px">
                                <asp:Label ID="Label1" runat="server" Text="Seleccione el Año"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlAnio" Width="250px" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="70%" >
                    <asp:GridView ID="gvLista" Width="100%" runat="server" BackColor="White" 
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                        AutoGenerateColumns="False">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="codigo_pla" HeaderText="Codigo" Visible="False" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Periodo_pla" HeaderText="Periodo" >
                            <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="anio_eval" HeaderText="Año" >
                            <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Responsable" HeaderText="Responsable" >
                            <ItemStyle Width="500px" />
                            </asp:BoundField>
                            <asp:HyperLinkField 
                                DataNavigateUrlFields="rutadocumento_eval" 
                                DataNavigateUrlFormatString="{0}" 
                                DataTextField="DocumentoPlan" 
                                HeaderText="Documento" 
                                Target="_blank" >
                            <ItemStyle Width="150px" />
                            </asp:HyperLinkField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
