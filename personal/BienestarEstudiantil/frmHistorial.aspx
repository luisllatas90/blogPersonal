<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHistorial.aspx.vb" Inherits="academico_frmHistorial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>HISTORIAL ACADÉMICO</title>    
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" /> 
    <script language="JavaScript" src="../../../private/funciones.js"></script>    
    <style type="text/css">
        .D
        {
            color: red;
        }
        .A
        {
            color: blue;
        }
        .P
        {
            color: Green;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divInfo" runat="server">
        <table ID="tblDatos" border="0" bordercolor="#111111" cellpadding="3" 
                        cellspacing="0" class="contornotabla" width="100%">
                        <tr>
                            <td rowspan="6" valign="top" width="10%">
                                <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" />
                            </td>
                            <td width="15%">
                                Código Universitario</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblcodigo" runat="server"></asp:Label>
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Apellidos y Nombres
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblalumno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Ciclo de Ingreso</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblcicloingreso" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Plan de Estudio</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblPlan" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Escuela Profesional</td>
                            <td class="usatsubtitulousuario" width="70%">
                                <asp:DropDownList ID="cboEscuela" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:HiddenField ID="photo" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 85%" width="15%">
                                <asp:Label 
                                                ID="lblMensaje" runat="server" Font-Bold="True" 
                                    Font-Size="10pt" ForeColor="Red"></asp:Label>
                                <asp:Button ID="cmdImprimir" runat="server" Text="Imprimir" Visible="False" 
                                    onclientclick="imprimir('N','','');return(false)" 
                                    UseSubmitBehavior="False" CssClass="imprimir2" />
                            &nbsp;<asp:Button ID="cmdExportar" 
                    runat="server" Text="Exportar" 
                    CssClass="excel2" Height="22px" />
                            </td>
                        </tr>
                    </table>
            
    <br />
                        <asp:GridView ID="grwHistorial" runat="server" AutoGenerateColumns="False" 
                            onrowdatabound="VerificarMatricula" Width="100%" 
            CellPadding="3">
                            <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        
                            <Columns>
                                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="tipoCurso_Dma" HeaderText="Area" >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_cur" HeaderText="Curso" >
                                </asp:BoundField>
                                <asp:BoundField DataField="creditocur_dma" HeaderText="Crd." >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="notafinal_dma" HeaderText="Nota Final" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="condicion_dma" HeaderText="Condición">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="vecesCurso_DmaUlt" HeaderText="Veces Desaprob." >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:HyperLinkField HeaderText="Detalles" Text="Ver" >
                                    <ItemStyle HorizontalAlign="Center" Font-Underline="True" ForeColor="#0000CC" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="escuela" HeaderText="Plan Est - Escuela">
                                    <ItemStyle Font-Size="7pt" />
                                </asp:BoundField>                               
                            </Columns>
                            <FooterStyle 
            HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                            <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
                        </asp:GridView>
                    <br />
        <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <table style="width:100%">
            <tr>
                <td style="width:60%">* Matrícula por Convalidación<br />
    ** Matrícula por Examen de Ubicación<br />
    *** Matrícula por Examen de Suficiencia<br />
                    **** Examen de Recuperación<br />
    </td>
                <td style="width:40%;" align="right">
                <table border="1px" 
                    style="border-color: #C0C0C0; width: 100%; border-collapse: collapse;" 
                    cellpadding="3" cellspacing="0">
                    <tr class="usatEncabezadoTabla">
                        <td width="30%" style=" font-weight: bold">
                            RESUMEN DE MATRICULA</td>
                        <td width="30%" style="font-weight: bold">
                            Regular</td>
                        <td width="30%" style="font-weight: bold">
                            Convalidación</td>
                        <td width="10%">TOTAL</td>
                    </tr>
                    <tr>
                        <td width="90%" style=" font-weight: bold">
                            Créditos Aprobadas</td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblCrdAprob" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblCrdAprobC" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="10%">
                               <asp:Label ID="lblCrd" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>                        
                               &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="90%" style=" font-weight: bold">
                            Asignaturas Aprobadas</td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblAsigAprob" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblAsigAprobC" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>
                        </td>
                        <td width="10%">
                               <asp:Label ID="lblAsig" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>                        
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="3" style=" font-weight: bold; width: 180%;">
                            Promedio Ponderado Acumulado</td>
                        <td width="10%">
                            <asp:Label ID="lblPond" runat="server" CssClass="azul" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    </table>
                
                </td>
            </tr>
        </table>
        
        </asp:Panel>
     </div>
    </form>
</body>
</html>