﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmtesis.aspx.vb" Inherits="frmtesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Trabajos de Investigación para Titulación.</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>    
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            jQuery(function($) {
                $("#txtFechaInicio").mask("99/99/9999");
                $("#txtFechaFin").mask("99/99/9999");
            });
            $('#txtMeses').keyup(function() {
                this.value = (this.value + '').replace(/[^0-9]/g, '');
            });

            $('#txtPresupuesto').keyup(function() {
                this.value = (this.value + '').replace(/[^0-9\.]/g, '');
                this.value.fixed(2);
            });

        })
    </script>    
</head>
<body>
<form id="form1" runat="server">
    <p class="usatTitulo">Registro de Trabajos de Investigación para Titulación.
    <asp:Label ID="lblcodigo" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="14px" ForeColor="Maroon"></asp:Label><asp:HiddenField ID="hdCodigo_Eti" runat="server" Value="4" />
    </p>
    <table width="100%" cellpadding="3" cellspacing="0">       
        <tr>
            <td><b>Producto Final</b>:</td>
            <td><asp:DropDownList ID="dpTipoTesis" runat="server" 
                    AutoPostBack="True">
                <asp:ListItem Value="11">Tesis</asp:ListItem>
                <asp:ListItem Value="13">Expediente Judicial</asp:ListItem>
                </asp:DropDownList>
                            &nbsp;<b>&nbsp; Enfoque.</b>
                <asp:DropDownList ID="dpenfoque" runat="server">
                    <asp:ListItem Value="NO DEFINIDO">--Seleccione--</asp:ListItem>
                    <asp:ListItem>CUANTITATIVO</asp:ListItem>
                    <asp:ListItem>CUALITATIVO</asp:ListItem>
                    <asp:ListItem>CUANTITATIVO-CUALITATIVO</asp:ListItem>
                </asp:DropDownList>
                            </td>
        </tr>
        <tr>
            <td>
                <b>Título: <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtTitulo" 
                    ErrorMessage="Debe ingresar el título de la tesis">*</asp:RequiredFieldValidator>
                            </b></td>
            <td align="left">
                <asp:Label ID="lblFase" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
                            </td>
        </tr>
        <tr>
            <td colspan="2">
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="cajas2" 
                    MaxLength="500" Rows="3" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td colspan="2">
                                <b>Formulación del Problema: <asp:RequiredFieldValidator ID="RqProblema" runat="server" 
                    ControlToValidate="txtProblema" ErrorMessage="Debe ingresar la Formulación del Problema">*</asp:RequiredFieldValidator>
                                </b>
                            </td>
        </tr>
        <tr>
            <td colspan="2">
                                <asp:TextBox ID="txtProblema" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine" MaxLength="1000" Rows="3" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td colspan="2">
                <b>Resumen: </b>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtResumen" 
                    ErrorMessage="Debe ingresar el resumen de la tesis">*</asp:RequiredFieldValidator>
                            </td>
        </tr>
        <tr>
            <td colspan="2">
                                <asp:TextBox ID="txtResumen" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine" Height="150px" MaxLength="2000" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td colspan="2"><b>Objetivo General</b></td>
        </tr>
        <tr>
            <td colspan="2">
                                <asp:TextBox ID="txtObjetivoGeneral" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine" Height="70px" MaxLength="2000" Width="98%"></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td colspan="2"><b>Objetivo Especifico</b></td>
        </tr>
        <tr>
            <td colspan="2">
                                <asp:TextBox ID="txtObjetivoEspecifico" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine" Height="150px" MaxLength="2000" Width="98%"></asp:TextBox>
                            </td>
        </tr>        
        <tr>
            <td>
                <b>Fecha Inicio</b></td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server" Width="80px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtFechaInicio" 
                    ErrorMessage="Debe especificar la fecha de inicio">*</asp:RequiredFieldValidator>
                <b>&nbsp;&nbsp;&nbsp; Fecha término </b><asp:TextBox ID="txtFechaFin" 
                    runat="server" Width="80px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                    runat="server" ControlToValidate="txtFechaFin" 
                    ErrorMessage="Debe especificar la fecha de término">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator
                                                    ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtFechaFin" ControlToValidate="txtFechaInicio"
                                                    
                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." Operator="LessThan"
                                                    Type="Date">*</asp:CompareValidator>
                </td>
        </tr>
        <tr>
            <td>
                <b>Nro. Resolución </b></td>
            <td>
                <asp:TextBox ID="txtResolucion" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RqResolucion" runat="server" 
                    ControlToValidate="txtResolucion" 
                    ErrorMessage="Debe ingresar el Número de Resolución">*</asp:RequiredFieldValidator>
            &nbsp;
                <asp:Label ID="lblNumeroExp" runat="server" Text="Número de Exp:" 
                    Visible="False"></asp:Label>
&nbsp;<asp:TextBox ID="txtExpediente" runat="server" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><b>Semestre Académico</b></td></td>
            <td>
                <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>Carrera Profesional</b></td>
            <td>
                <asp:DropDownList ID="dpEscuela" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                            </td>
       
        </tr>
        <tr>
            <td>
                                            <b>Investigador Principal</b></td>
            <td>
                <asp:DropDownList ID="cboAutor" runat="server" DataTextField="alumno" 
                    DataValueField="codigo_alu" Enabled="False" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Button ID="cmdAgregarAutor" runat="server" CssClass="agregar2" 
                    Text="   Agregar" ValidationGroup="Cancelar" Visible="False" />
                            </td>
       
        </tr>        
        <tr>
            <td>
                &nbsp;</td>
            <td>
                                        <asp:GridView ID="grwAutor" runat="server" AutoGenerateColumns="False" 
                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                        ForeColor="#333333" ShowHeader="False" 
                                        DataKeyNames="codigo_RTes">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        <asp:BoundField DataField="alumno" HeaderText="Apellidos y Nombres">
                                        </asp:BoundField>
                                            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" />
                                            <asp:BoundField DataField="cicloing_alu" HeaderText="Ingreso" />
                                        <asp:CommandField ShowDeleteButton="True">
                                        </asp:CommandField>
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                            </td>
       
        </tr>
        <tr>
            <td>
                <b>Cronograna</b>
            </td>
            <td>
                <asp:TextBox ID="txtMeses" runat="server" Width="50px"></asp:TextBox> &nbsp;Meses
            </td>
        </tr>
        <tr>
            <td>
                <b>Presupuesto</b>
            </td>
            <td>
                <asp:TextBox ID="txtPresupuesto" runat="server" Width="80px"></asp:TextBox>
            &nbsp;Soles</td>
        </tr>
        <tr>
            <td><b>Financiamiento:</b></td>
            <td>
                <asp:DropDownList ID="ddlFinanciado" runat="server" AutoPostBack="True">
<%--                    <asp:ListItem Value="A">Autofinanciado</asp:ListItem>--%>
                    <asp:ListItem Value="A">Interno</asp:ListItem>
                    <asp:ListItem Value="E">Externo</asp:ListItem>
                                        

                </asp:DropDownList>
                <asp:Label ID="lblFinanciado" runat="server" Text="Financiado por:"></asp:Label>
                <asp:TextBox ID="txtFinanciado" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b>Adjuntar Proyecto Tesis</b></td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;&nbsp;&nbsp; <span id="texto" runat="server"></span></td>
       
        </tr>
        <tr>
            <td>
                <b>Lineas Inv.:</b></td>
            <td>
                                            <asp:DropDownList ID="cboLineas" 
                                                runat="server" DataTextField="nombre_are" DataValueField="codigo_are" 
                                                Width="50%" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:Button ID="cmdAgregarLineas" runat="server" CssClass="agregar2" Text="   Agregar" ValidationGroup="Cancelar" Visible="False" />
                            </td>
       
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                                        <asp:GridView ID="grdLineas" runat="server" AutoGenerateColumns="False" 
                                            BorderColor="#666666" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                            DataKeyNames="codigo_are,guardado" ForeColor="#333333" ShowHeader="False">
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField DataField="nombre_are" HeaderText="Descripción">
                                                    <ItemStyle Width="95%" />
                                                </asp:BoundField>
                                                <asp:CommandField ShowDeleteButton="True">
                                                    <ControlStyle Font-Underline="True" />
                                                    <ItemStyle Font-Overline="False" Font-Size="X-Small" Font-Underline="True" 
                                                        ForeColor="#0066FF" Width="5%" />
                                                </asp:CommandField>
                                            </Columns>
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                            </td>
       
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEtiquetaRegistrado" runat="server" Text="Registrado" 
                    Visible="False" style="font-weight: 700"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRegistrado" runat="server" Font-Bold="True"></asp:Label>
                            </td>
       
        </tr>
        </table>          
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
        <p style="text-align:right">
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" CssClass="guardar" 
                 />
&nbsp;<input id="cmdCancelar" type="button" value="Cerrar" 
                onclick="self.parent.tb_remove()" class="salir" />&nbsp;</p>
    </form>
    </body>
</html>
