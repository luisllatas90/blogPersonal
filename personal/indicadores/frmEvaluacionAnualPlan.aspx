<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluacionAnualPlan.aspx.vb" Inherits="indicadores_frmEvaluacionAnualPlan" %>

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
                    <asp:Label ID="Label4" runat="server" Text="Documentación de Planes"></asp:Label></b></td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                        <tr>
                            <td style="width:25%">
                                <asp:Label ID="Label1" runat="server" Text="Seleccione el Año de la Evaluación">
                                </asp:Label>
                            </td>
                            <td style="width:75%">
                                <asp:DropDownList ID="ddlAnioFiltro" Width="350px" runat="server" 
                                    AutoPostBack="True">
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
                        AutoGenerateColumns="False" DataKeyNames="codigo_eval,plazo">
                        <RowStyle ForeColor="#000066" />
                        <Columns>
                            <asp:BoundField DataField="codigo_eval" HeaderText="CODIGO" Visible="False" >
                            <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_pla" HeaderText="codigo_pla" 
                                Visible="False" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Periodo_pla" HeaderText="DESCRIPCIÓN DEL PLAN" >
                            <ItemStyle Width="600px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="anio_eval" HeaderText="AÑO" >
                            <ItemStyle Width="25px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechaini_eval" HeaderText="DESDE" >
                            <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechafin_eval" HeaderText="HASTA" >
                            <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dias" HeaderText="Nº DIAS" >
                            <ItemStyle Width="20px" />
                            </asp:BoundField>
                            <asp:HyperLinkField 
                                DataNavigateUrlFields="rutadocumento_eval" 
                                DataNavigateUrlFormatString="{0}" 
                                DataTextField="DocumentoPlan" 
                                HeaderText="Documento" 
                                Target="_blank" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ImageUrl="~/Images/fileupload.png" runat="server" ID="image" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:FileUpload ID="FileArchivo" runat="server" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="true" ShowCancelButton="true" 
                                ButtonType="Image" CancelImageUrl="../images/cerrar.gif" 
                                EditImageUrl="../images/editar.gif" UpdateImageUrl="../images/guardar.gif" >
                            <ItemStyle Width="20px" />
                            </asp:CommandField>
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
