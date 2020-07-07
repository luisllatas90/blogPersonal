<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCursosFaltantes.aspx.vb" Inherits="academico_frmCursosFaltantes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cursos Faltantes</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
            <td align="left">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="imgmdl/bola_roja.gif" />
                                                &nbsp;Faltantes&nbsp;&nbsp;&nbsp;<asp:Image ID="Image2" runat="server" 
                                                    ImageUrl="imgmdl/bola_verde.gif" />
                                                &nbsp;Aprobados&nbsp;&nbsp;&nbsp;<asp:Image ID="Image3" runat="server" 
                                                    ImageUrl="imgmdl/bola_amar.jpg" />
                                                &nbsp;Convalidados&nbsp;
                                                <asp:Image ID="Image4" runat="server" ImageUrl="imgmdl/bola_naranja.gif" />
                                                &nbsp;Electivos no llevados&nbsp;
                                                <asp:Image ID="Image5" runat="server" ImageUrl="imgmdl/bola_azul.gif" />
                                                Matriculados</td>
            </tr>
            <tr align="center" >
                <td align="justify" colspan="4" valign="top">
                    <asp:GridView ID="GvPlanMatricula" runat="server" 
                        Width="100%" EnableViewState="False" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="codigo_alu" HeaderText="Nro" />
                            <asp:BoundField DataField="descripcion_cac" HeaderText="Ciclo Acad." />
                            <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />
                            <asp:BoundField DataField="creditos_cur" HeaderText="Creditos" />
                            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                            <asp:BoundField DataField="electivo_cur" HeaderText="Electivo" />
                            <asp:BoundField DataField="notafinal_dma" HeaderText="Nota Final" />
                            <asp:BoundField HeaderText="Estado" />
                        </Columns>
                        <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
