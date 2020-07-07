<%@ Page Language="VB" AutoEventWireup="false" CodeFile="historial_personal.aspx.vb" Inherits="personal_academico_estudiante_historial" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HISTORIAL ACADÉMICO</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../private/funciones.js"></script>    
    <style type="text/css">
        .D
        {
            color: #FF0000;
        }
        .A
        {
            color: #0000FF;
        }
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="pnlDatos" runat="server">
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
                        Escuela Profesional</td>
                    <td class="usatsubtitulousuario" width="70%">
                        &nbsp;<asp:Label ID="lblescuela" runat="server"></asp:Label>
                        &nbsp;</td>
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
                    <td colspan="2" style="width: 85%" width="15%">
                        <asp:Label 
                                                ID="lblMensaje" runat="server" Font-Bold="True" 
                                    Font-Size="10pt" ForeColor="Red"></asp:Label>
                        <asp:Button ID="cmdVer" runat="server" 
                            Text="Ver Historial por ciclo del curso"  />
                    </td>
                </tr>
            </table>
        </asp:Panel>
            
    <br />
    <asp:DataList ID="dlstCiclos" runat="server" CellPadding="4" 
        DataKeyField="codigo_cac" Width="100%" EnableViewState="False">
        <ItemTemplate>
            <table width="100%" border="1" cellpadding="0" cellspacing="0" style="border: 1px solid #91b4de;border-collapse: collapse">
                <tr bgcolor="#91b4de">
                    <td align="center">
                        <asp:Label ID="lblciclo" runat="server" Text='<%# eval("descripcion_cac") %>' 
                            Font-Bold="True" Font-Size="10pt" ForeColor="Maroon"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            onrowdatabound="VerificarMatricula" Width="100%" CellPadding="3"                              
                            GridLines="Horizontal" BorderStyle="None" BorderWidth="0px" 
                            ShowFooter="True" DataKeyNames="codigo_cur">
                            <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />        
                            <Columns>
                                <asp:BoundField DataField="tipoCurso_Dma" HeaderText="Area" >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="identificador_Cur" HeaderText="Código" >
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombre_cur" HeaderText="Curso" >
                                    <ItemStyle Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="creditocur_dma" HeaderText="Crd." >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" >
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="vecesCurso_DmaUlt" HeaderText="Veces Desap." >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="notafinal_dma" HeaderText="Nota Final" >
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>                                
                                <asp:BoundField HeaderText="Observaciones" >
                                    <ItemStyle HorizontalAlign="Center" CssClass="rojo" Width="15%" />
                                </asp:BoundField>
                                <asp:HyperLinkField HeaderText="Detalles" Text="Ver" >
                                    <ItemStyle HorizontalAlign="Center" Font-Underline="True" ForeColor="#0000CC" 
                                        Width="5%" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="codigo_cur" HeaderText="curso" Visible="false" >
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle 
            HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                            <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <br />
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <table style="width:100%">
            <tr>
                <td style="width:60%">* Matrícula por Convalidación<br />
    ** Matrícula por Examen de Ubicación<br />
    *** Matrícula por Examen de Suficiencia<br />
    </td>
                <td style="width:40%;" align="right">
                <table border="1px" 
                    style="border-color: #C0C0C0; width: 100%; border-collapse: collapse;" 
                    cellpadding="3" cellspacing="0">
                    <tr class="usatEncabezadoTabla">
                        <td width="30%" style=" font-weight: bold">
                            RESUMEN</td>
                        <td width="30%" style="font-weight: bold">
                            Matrícula regular</td>
                        <td width="30%" style="font-weight: bold">
                            Matrícula por Convalidación</td>
                        <td width="10%">TOTAL</td>
                    </tr>
                    <tr>
                        <td width="90%" style=" font-weight: bold">
                            Créditos Aprobados</td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblCrdAprob" runat="server" CssClass="azul" Font-Bold="True" 
                                Text="Label"></asp:Label>
                        </td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblCrdAprobC" runat="server" CssClass="azul" Font-Bold="True" 
                                Text="Label"></asp:Label>
                        </td>
                        <td width="10%">
                               <asp:Label ID="lblCrd" runat="server" CssClass="azul" Font-Bold="True" 
                                Text="Label"></asp:Label>                        
                               &nbsp;</td>
                    </tr>
                    <tr>
                        <td width="90%" style=" font-weight: bold">
                            Asignaturas Aprobadas</td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblAsigAprob" runat="server" CssClass="azul" Font-Bold="True" 
                                Text="Label"></asp:Label>
                        </td>
                        <td width="90%" style=" font-weight: bold" 
                            align="center">
                            <asp:Label ID="lblAsigAprobC" runat="server" CssClass="azul" Font-Bold="True" 
                                Text="Label"></asp:Label>
                        </td>
                        <td width="10%">
                               <asp:Label ID="lblAsig" runat="server" CssClass="azul" Font-Bold="True" 
                                Text="Label"></asp:Label>                        
                        </td>
                    </tr>
                    </table>
                
                </td>
            </tr>
        </table>
        
        </asp:Panel>
    </form>
</body>
</html>
