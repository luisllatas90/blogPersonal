<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmQuintaEspecial.aspx.vb" Inherits="frmQuintaEspecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="funciones.js"></script>
    <title>Página sin título</title>
    <style type="text/css">
        .contornotabla 
            { 
            	   border: 1px solid #808080; background-color:#FFFFFF ;
             }
        .style9
        {
            font-size: medium;
            background-color: #73B6E1;
            color: #EEF3F6;
        }
        .style12
        {
            font-family: Arial;
        }
        .style25
        {
            font-size: small;
            font-family: "Courier New", Courier, "espacio sencillo";
        }
        .style26
        {
            text-align: left;
            font-size: small;
            font-family: "Courier New", Courier, "espacio sencillo";
        }
        .style32
        {
            background-color: #F2A31B;
        }
        .style52
        {
            color: #003300;
            font-size: x-small;
            background-color: #FFFFFF;
        }
        .style53
        {
            background-color: #731827;
            text-decoration: underline;
            color: #FFFFFF;
        }
        .style84
        {
            background-color: #731827;
            color: #FFFFFF;
            height: 25px;
            font-family: Arial;
            font-size: x-small;
        }
        .style92
        {
            background-color: #D2D2D2;
        }
        .style96
        {
            background-color: #D2D2D2;
        }
        .style100
        {
            font-size: small;
            color: #0033CC;
            font-weight: normal;
            font-family: Arial;
        }
        .style101
        {
            font-size: small;
            color: #0000FF;
            font-family: Arial;
        }
        .style104
        {
            background-color: #FFFFFF;
        }
        .style106
        {
            font-weight: normal;
        }
        .style108
        {
            font-size: 10pt;
            font-family: "Courier New", Courier, "espacio sencillo";
            background-color: #F0F0F0;
        }
        .style109
        {
            color: #0000FF;
            font-family: Arial;
        }
        .style110
        {
            background-color: #D2D2D2;
            width: 37px;
        }
    </style>
</head>
<body bgcolor="#d2d2d2" style="background-color: #FFFFFF" >
    <form id="form1" runat="server" style="background-color:  white">
    <div class="style25" style="background-color: #FFFFFF">
    
        </div>
    <div class="style26">
        <span class="style12">
        <span class="style53">&nbsp;</span></span><span class="style32"><span 
            class="style12"><span class="style53">Universidad 
        Católica Santo Toribio de Mogrovejo Sistema de Tesorería 
        (v 1.0)</span></span><span class="style52"><span class="style96"><br />
        <br />
        </span>
        </span></span>
    </div>
    <table 
        style="border-width: 0px; padding: 0px; margin: 0px; border-color: #d2d2d2; border-spacing: 0px;  width: 100%" >
        <tr style="border-color: #D2D2D2; border-style:  none">
            <td class="style84" colspan="6">
                Registro de Ingresos por concepto de Quinta</td>
        </tr>
        <tr>
            <td colspan="6">
                <hr style="background-color: #E6805A; color: #FF9900;" />
            </td>
        </tr>
        <tr>
            <td class="style108" colspan="2">
                <span class="style100">Programa :</span><span class="style106"><asp:Label ID="lblprograma" runat="server" Text="Programa"></asp:Label>
                </span>
            </td>
            <td class="style108" colspan="2">
                <span class="style101">Planilla :</span><asp:Label ID="lblplanilla" 
                    runat="server" Text="Planilla" Width="250px"></asp:Label>
            </td>
            <td class="style108">
                <span class="style109">Año :</span><asp:Label ID="lblaño" runat="server" Text="Año"></asp:Label>
            </td>
            <td class="style108">
                <span class="style109">Mes :</span><asp:Label ID="lblmes" runat="server" 
                    Text="Mes" Width="80px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style108" colspan="6">
                <span class="style109">Descripción del informe:</span><asp:Label ID="lbldescripcionprograma" runat="server" 
                    Text="Programa"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style104">
            <div  style="overflow : scroll; height :570px" >
                    <asp:TreeView ID="trvpersonal" runat="server" Height="470px" ShowCheckBoxes="All" 
                        Width="323px" Font-Names="Arial" Font-Size="XX-Small" 
                        
                        style="margin-left: 10px; font-family: 'Courier New', Courier, 'espacio sencillo'; font-size: x-small;" 
                        BackColor="#F9F9F9">
                    </asp:TreeView>
            </div>
                </td>
            <td colspan="5" style="vertical-align: top;" class="style104">
            <div style=" overflow :scroll; height: 550px;" >
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" >
                                <ProgressTemplate>
                                    <img src="iconos/icocargando.gif" style="width: 54px;" />
                                    <asp:Label ID="lblmensaje" runat="server" Font-Names="Arial" Font-Size="Small" 
                                        ForeColor="Blue" Text="Procesando"></asp:Label>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                       
                            
                       
                            <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="codigo_dip" Height="16px" Width="679px" 
                                CssClass="style104">
                                <FooterStyle 
                                    VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkseleccion" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigo_dip" HeaderText="Id" ReadOnly="True">
                                        <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" Font-Names="Arial" 
                                            ForeColor="Blue" />
                                        <ItemStyle Font-Size="X-Small" Font-Names="Arial" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombres" HeaderText="Nombres" ReadOnly="True">
                                        <HeaderStyle Font-Size="X-Small" BackColor="#F0F0F0" Font-Names="Arial" 
                                            ForeColor="Blue"/>
                                        <ItemStyle Font-Size="X-Small" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="importe_dip" HeaderText="Importe">
                                        <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                                        <ItemStyle Font-Size="X-Small" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" text ='<%# Eval("importe_dip") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtimporte_dip" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True">
                                        <ItemStyle Font-Size="X-Small" />
                                    </asp:CommandField>
                                </Columns>
                            </asp:GridView>
                        <asp:Label ID="lblimportetotal" runat="server" Text="Label" BackColor="#993300" 
                                Font-Names="Courier New" ForeColor="White"></asp:Label>
                            <asp:Label ID="lblobservacion" runat="server" Font-Names="Arial" 
                                Font-Size="Small" ForeColor="Blue" Text=".."></asp:Label>
                    </ContentTemplate>
                    
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmdagregar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="lstinformacion" EventName="RowDeleting" />
                        <asp:AsyncPostBackTrigger ControlID="cmdeliminarseleccionados" 
                            EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cmdregistrar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                
                </div> 
                <asp:Button ID="cmdregistrar" runat="server" BorderStyle="Solid" 
                                ForeColor="#0080FF" Text="Guardar cambios" />
                <asp:Button ID="cmdeliminarseleccionados" runat="server" BorderStyle="Solid" 
                                ForeColor="#0080FF" Text="Eliminar Seleccionados" />
                <asp:Button ID="cmdfinalizarEdicion" runat="server" BorderStyle="Solid" 
                                ForeColor="#0080FF" Text="Finalizar Edición del Informe" />
                
                </td>
        </tr>
        <tr>
            <td class="style110" colspan="3">
                &nbsp;</td>
            <td class="style92" colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style92" colspan="6">
                <span class="style9"><span class="style12">
                <asp:Button ID="cmdagregar" runat="server" 
                    ForeColor="Blue" Text="Agregar&gt;&gt;" Width="250px" 
                    BackColor="#EEF3F6" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                    Font-Size="Small" BorderStyle="None" />
                </span></span>
            </td>
        </tr>
        <tr>
            <td class="style12" colspan="6" style="font-size: xx-small">
                Obs. Los datos guardados serán procesados por el Sistema de Tesorería y enviados 
                a Personal para su procesamiento en planillas, la información aca registrada es 
                responsabilidad del Coordinador</td>
        </tr>
    </table>
<p>
    &nbsp;</p>
    </form>
</body>
</html>
