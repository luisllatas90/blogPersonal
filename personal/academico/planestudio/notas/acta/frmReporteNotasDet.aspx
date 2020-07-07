<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReporteNotasDet.aspx.vb" Inherits="academico_notas_frmReporteNotasDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />    
    <script src="../../../../private/calendario.js"></script>
    <script src="../private/validarnotas.js" type="text/javascript"></script>
    <script type="text/javascript">
        function imprimir(modo, panel, titulo) {
            if (modo == "N") {
                window.document.title = titulo
                window.print()
            }
            else {
                window.parent.frames[panel].document.title = titulo
                window.parent.frames[panel].focus()
                window.parent.frames[panel].print()
            }
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
        ForeColor="Red"></asp:Label>
        <br />
    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" />
    <br/>
    <asp:HiddenField ID="hdCup" runat="server" />
        <asp:HiddenField ID="hdper" runat="server" />
        <asp:HiddenField ID="hdCac" runat="server" />
        <asp:HiddenField ID="hdIdentificador" runat="server" />
    <br />
        <asp:Label ID="Label1" runat="server" Text="Curso: " Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:Label ID="lblCurso" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
&nbsp;&nbsp;
        
    <br /> 
    <asp:Label ID="Label2" runat="server" Text="Grupo: " Font-Bold="True" Font-Size="Small"></asp:Label>   
    <asp:Label ID="lblGrupo" runat="server" Text="" Font-Bold="True" Font-Size="Small"></asp:Label>
    &nbsp;&nbsp;&nbsp;    
    <asp:Label ID="Label5" runat="server" Text="Ciclo: " Font-Bold="True" Font-Size="Small"></asp:Label>   
    <asp:Label ID="lblNCiclo" runat="server" Text="" Font-Bold="True" Font-Size="Small"></asp:Label>
    &nbsp;&nbsp;&nbsp;    
    <asp:Label ID="Label6" runat="server" Text="Impreso: " Font-Bold="True" Font-Size="Small"></asp:Label>   
    <asp:Label ID="lblEstado" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>    
    <br />
    <asp:Label ID="Label3" runat="server" Text="Docente: " Font-Bold="True" Font-Size="Small"></asp:Label>   
    <asp:Label ID="lblDocente" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>    
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" Text="Ciclo Académico: " Font-Bold="True" Font-Size="Small"></asp:Label>   
    <asp:Label ID="lblCiclo" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
    <asp:GridView ID="gvAlumnos" runat="server" Width="100%" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Cod. Univ." />
            <asp:BoundField DataField="alumno" HeaderText="Alumno" />
            <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" />
            <asp:BoundField DataField="notafinal_dma" HeaderText="Nota" />
            <asp:BoundField DataField="condicion_dma" HeaderText="Condición" />
        </Columns>
        <HeaderStyle BackColor="#dfdba4" ForeColor="Black" Height="25px" />                
        <RowStyle Height="20px" />
    </asp:GridView>
    </div>
    </form>
</body>
</html>
