<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCartaCompromiso.aspx.vb" Inherits="academico_matricula_FrmCartaCompromiso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Cartas de Compromiso</title>
    
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        
        /*if(top.location==self.location)
            {location.href='../../tiempofinalizado.asp'}
       */
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Impresión de Cartas de Compromiso</p>
    
    <asp:HiddenField ID="hdCodigo_alu" runat="server" />
    <asp:HiddenField ID="hdCodigoUniver" runat="server" />
    <table style="width:100%" runat="server" id="fradatos" >
        <tr>
            <td style="width: 15%" >
                Apellidos y Nombres</td>
                            <td class="usatsubtitulousuario" style="width: 75%">
                                &nbsp;<asp:Label ID="lblalumno" runat="server"></asp:Label>
                            </td>
        </tr>
        <tr>
            <td style="width: 15%" >
                Escuela Profesional</td>
                            <td class="usatsubtitulousuario" style="width: 75%">
                                &nbsp;<asp:Label ID="lblescuela" runat="server"></asp:Label>
                                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 15%" >
                Ciclo de Ingreso</td>
                            <td class="usatsubtitulousuario" style="width: 75%">
                                &nbsp;<asp:Label ID="lblcicloingreso" runat="server"></asp:Label>
                            </td>
        </tr>
        <tr style="width: 15%">
            <td >
                Plan de Estudios</td>
                            <td class="usatsubtitulousuario" style="width: 75%">
                                &nbsp;<asp:Label ID="lblPlan" runat="server"></asp:Label>
                            </td>
        </tr>
        <tr>
            <td style="width: 15%" >&nbsp;</td>
                            <td class="usatsubtitulousuario" style="width: 75%">
                                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 15%" >&nbsp;</td>
                            <td class="usatsubtitulousuario" style="width: 75%">
                                Indicar en qué semestre se compromete APROBAR:
                                <asp:DropDownList ID="dpcodigo_cac" runat="server" Visible="False">
                                </asp:DropDownList>
                                <asp:Button ID="cmdGenerar" runat="server" Text="   Generar Carta" 
                                    Visible="False" CssClass="word" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%" >&nbsp;</td>
                            <td class="rojo" style="width: 75%">
                                <b>Solamente puede &quot;Generar carta&quot; una sola vez. Tener cuidado al momento de 
                                marcar las asignaturas. Esta acción bloqueará la matrícula del estudiante vía 
                                campus virtual.</b></td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="grwCursosDesaprobados" runat="server" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_dma" 
        Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" 
        CellPadding="2" AutoGenerateColumns="False">
    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="chkElegir" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre" />
            <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura" />
            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
            <asp:BoundField DataField="creditoCur_Dma" HeaderText="Crd." />
            <asp:BoundField DataField="vecesCurso_Dma" HeaderText="Veces Desap." />
            <asp:BoundField DataField="notafinal_dma" HeaderText="Nota" />
            <asp:BoundField DataField="firmocarta" HeaderText="Firmó Carta" />
            <asp:BoundField DataField="ciclocompromiso" HeaderText="Ciclo Compromiso" />
        </Columns>
        <EmptyDataTemplate>
            No se han encontrado asignaturas desaprobadas según los criterios establecidos.
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
    </asp:GridView>
    <br />
       <asp:GridView ID="grwMarcados" runat="server" 
            AutoGenerateColumns="False" BorderColor="Silver" BorderStyle="Solid" 
            CaptionAlign="Top" CellPadding="3"
            EnableModelValidation="True" Width="100%">
            <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
            <Columns>
                <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre" />
                <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura" />
                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                <asp:BoundField DataField="creditoCur_Dma" HeaderText="Crd." />
                <asp:BoundField DataField="vecesCurso_Dma" HeaderText="Veces Desap." />
                <asp:BoundField DataField="notafinal_dma" HeaderText="Nota" />
            </Columns>
            <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                BorderWidth="1px" ForeColor="#3366CC" />
        </asp:GridView>
    <asp:HiddenField ID="HdFacultad" runat="server" />
    </form>
</body>
</html>
