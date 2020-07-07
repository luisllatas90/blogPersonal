<%@ Page Language="VB" AutoEventWireup="false" CodeFile="adminestadocuenta.aspx.vb" Inherits="librerianet_academico_adminestadocuenta" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Estado de cuenta del Estudiante</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       //if(top.location==self.location)
       //{location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
    </script>
    <style type="text/css">

      .style1
      {
        font-weight: bold;
        text-decoration: underline;
        color: #FF0000;
      }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Estado de Cuenta</p>
        <asp:Panel ID="pnlDatos" runat="server">
            <table ID="tblDatos" border="0" bordercolor="#111111" cellpadding="3" 
                        cellspacing="0" class="contornotabla" width="100%">
                <tr>
                    <td rowspan="6" valign="top" width="10%">
                        <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" 
                                    Visible="False" />
                    </td>
                    <td width="15%">
                        Código Universitario</td>
                    <td class="usatsubtitulousuario" width="70%">
                        <asp:Label ID="lblcodigo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        Apellidos y Nombres
                    </td>
                    <td class="usatsubtitulousuario" width="70%">
                        <asp:Label ID="lblalumno" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        Escuela Profesional</td>
                    <td class="usatsubtitulousuario" width="70%">
                        <asp:Label ID="lblescuela" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        Ciclo de Ingreso</td>
                    <td class="usatsubtitulousuario" width="70%">
                        <asp:Label ID="lblcicloingreso" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        Plan de Estudio</td>
                    <td class="usatsubtitulousuario" width="70%">
                        <asp:Label ID="lblPlan" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 85%" width="15%">
                        <asp:Label 
                                                ID="lblMensaje" runat="server" Font-Bold="True" 
                                    Font-Size="10pt" ForeColor="Red"></asp:Label>
                        <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesamiento su información" 
                                    Title="Por favor espere" />
                    </td>
                </tr>
            </table>
    </asp:Panel>
    <table style="width:100%">
        <tr>
            <td style="width: 40%">
             
             
                <b>Nro. de convenios realizados:</b>
                <asp:Label ID="lblconvenios" runat="server" Font-Bold="True" Font-Size="11pt" 
                    ForeColor="Blue" Text="0"></asp:Label>
             
             
            &nbsp;<br />
                |
                <asp:LinkButton ID="lnkDeudas" runat="server" ForeColor="Blue" Font-Bold="True" 
                    Font-Size="X-Small">Ver Deudas</asp:LinkButton>
&nbsp;|
                <asp:LinkButton ID="lnkBanco" runat="server" ForeColor="Red" Font-Bold="True" 
                    Font-Size="X-Small">Deudas en el Banco</asp:LinkButton>
             
             
            &nbsp;|</td>
            <td style="text-align:right; width: 50%">
                <asp:HiddenField ID="hddcodigo_alu" runat="server" />
            <b>Tipo:</b>
        <asp:DropDownList ID="dpEstado" runat="server" AutoPostBack="True">
            <asp:ListItem Value="P">Pendientes</asp:ListItem>
            <asp:ListItem Value="C">Cancelados</asp:ListItem>
            <asp:ListItem Value="D">Doc. Emitidos</asp:ListItem>
            <asp:ListItem Value="A">Cargos vs Pagos</asp:ListItem>
        </asp:DropDownList>

<asp:Button ID="cmdExportar" runat="server" CssClass="imprimir2" 
                    Text="    Imprimir" onclientclick="imprimir('N','','');return(false)" 
                    UseSubmitBehavior="False" Height="22px" />
            &nbsp;<asp:Button ID="cmdRegresar" runat="server" CssClass="regresar2" 
                    Height="22px" Text="Regresar" />
                <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="excel" Height="22px" 
                    SkinID="BotonSinTextoAExcel" UseSubmitBehavior="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2" id="divObs" >
             
             
                <asp:Panel ID="Panel1" runat="server">
                    * Para la <b>matrícula 2013-I</b> el <b>Monto de matrícula</b> tendrá como fecha 
                de <b>vencimiento 10-03-2013 </b>y <span class="style1">variará gradualmente</span> de acuerdo al cronograma establecido por la oficina de Pensiones.
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" id="divObs" >
             
             
                &nbsp;</td>
        </tr>
    </table>
    <asp:Panel ID="pnlDeudas" runat="server">
        <asp:GridView ID="grwPagos" runat="server" AutoGenerateColumns="False" 
            BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
            EmptyDataText="&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;No se encontraron deudas pendientes por realizar." 
            EnableViewState="False" ShowFooter="True" Width="100%">
            <EmptyDataRowStyle CssClass="usatSugerencia" />
            <Columns>
                <asp:BoundField DataField="fechaVencimiento_sco" DataFormatString="{0:d}" 
                    HeaderText="Fecha Vencimiento">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="servicio" HeaderText="Concepto" />
                <asp:BoundField DataField="estado_deu" HeaderText="Estado" />
                <asp:BoundField DataField="cargo" HeaderText="Cargo (S/.)">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="pagos" HeaderText="Pago (S/.)">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="saldo" HeaderText="Saldo (S/.)">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="mora_deu" HeaderText="Mora (S/.)">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="SubTotal (S/.)">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="OBSERVACION" HeaderText="Observacion" />
                <asp:BoundField DataField="codigo_pod" HeaderText="codigo_pod" />
            </Columns>
            <FooterStyle BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" 
                HorizontalAlign="Center" />
            <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                BorderWidth="1px" ForeColor="#3366CC" />
        </asp:GridView>
        <p>
        
        <asp:GridView ID="gvPagoVsDeuda" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Fec. Venc." DataField="fechaVencimiento_Deu">
                        <ItemStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Concepto" DataField="Concepto" />
                    <asp:BoundField HeaderText="Estado" DataField="estado_Deu">
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cargo" DataField="montoTotal_Deu">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Pago" DataField="Pago_Deu" >
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Saldo" DataField="saldo_Deu">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Documento" DataField="descripcion_Tdo" >
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fec. Doc." DataField="fecha_Cin" >                    
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" HeaderText="Importe" >
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>  
                <RowStyle Height="20px"  />              
                <FooterStyle BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" 
                    HorizontalAlign="Center" />
                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                    BorderWidth="1px" ForeColor="#3366CC" />
        </asp:GridView>
        </p>
        <p>
            (*) Genera mora por día.
            <asp:Label ID="lblVencidas" runat="server" Font-Bold="True" Font-Size="11pt" 
                ForeColor="Blue" Text="0"></asp:Label>
            <asp:GridView ID="DocEmitidos" runat="server" AutoGenerateColumns="False" 
                BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                EmptyDataText="&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;No se encontraron documentos emitidos." 
                EnableViewState="False" ShowFooter="True" visible="false" Width="100%">
                <EmptyDataRowStyle CssClass="usatSugerencia" />
                <Columns>
                    <asp:BoundField DataField="fecha_Cin" HeaderText="Fecha" />
                    <asp:BoundField DataField="descripcion_Tdo" HeaderText="Tipo Doc." />
                    <asp:BoundField DataField="nroDocumento_Cin" HeaderText="Nro. Doc." />
                    <asp:BoundField DataField="total_Cin" HeaderText="Monto (S/.)">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="usuario_Cin" HeaderText="Operador">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Observacion_Cin" HeaderText="Observación" />
                    <asp:BoundField DataField="nombre_Ban" HeaderText="Lugar pago" />
                </Columns>
                <FooterStyle BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" 
                    HorizontalAlign="Center" />
                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                    BorderWidth="1px" ForeColor="#3366CC" />
            </asp:GridView>
        </p>        
    </asp:Panel>    
    <asp:Panel ID="pnlBanco" runat="server">
        <b style="font-family: Arial, Helvetica, sans-serif; text-decoration: underline;">
        DEUDAS ENVIADAS AL BANCO<br />
        </b> 
        &nbsp;<asp:GridView ID="gvDatosBanco" runat="server" 
            AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="descripcion_Sco" HeaderText="Servicio" />
                <asp:BoundField DataField="fechaVencimiento_Gen" 
                    DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha Vencimiento" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="UltimoProceso" DataFormatString="{0:dd/MM/yyyy}" 
                    HeaderText="Último Proceso" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CuotaMes" HeaderText="Cuota Mes" Visible="False" />
                <asp:BoundField DataField="CuotaAnio" HeaderText="Cuota Año" Visible="False" />
                <asp:BoundField DataField="Total" HeaderText="Cargos" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="mora" HeaderText="Mora">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="total_deuda" HeaderText="Total Pagar">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" BorderStyle="None" BorderWidth="0px" 
                    ForeColor="Red" Text="No se encontraron registros "></asp:Label>
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#E8EEF7" BorderColor="#99BAE2" ForeColor="#3366CC" />
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlAviso" runat="server">
        <p class="etiqueta">
            <u>Aviso:</u></p>
        <li><b><font color="#FF0000">Si tiene deudas vencidas en el presente ciclo o si debe alguna de ciclos pasados su Aula Virtual será bloqueada.</font></b></li>
        <li><b><font color="#FF0000">Deberá acudir a cancelar en cualquier agencia del Banco 
        de Crédito a nivel nacional, indicando su número de DNI.</font></b> </li>
        <li><b><font color="#FF0000">Los cursos complementarios, se cancelará de acuerdo al 
            número de cuotas seleccionadas en su matricula y se agrupará en su pensión.</font></b></li>
    </asp:Panel>
</form>
</body>
</html>
