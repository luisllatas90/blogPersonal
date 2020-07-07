<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCambiarEstadoAlumnoPlanEstudio.aspx.vb" Inherits="frmCambiarEstadoAlumnoPlanEstudio" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Egresado: Finalizaron el Plan de Estudio</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function MarcarCursos(obj)
        {
           //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0 ; i < arrChk.length ; i++){
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox"){
                    chk.checked = obj.checked;
                    if (chk.id!=obj.id){
                        PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
            }
        }
      
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#FFE7B3"
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
        
         if(top.location==self.location)
            //location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página   
    </script>
</head>
<body>
    <form id="form1" runat="server">
<p class="usatTitulo">Egresado: Finalizar Plan de Estudios del Estudiante </p>
<asp:Label ID="lblmensaje" runat="server" Text="ADVERTENCIA: Actualizar esta información permitirá obtener informes y 
    estadísticas de los &quot;Egresados&quot; que han finalizado su Plan de Estudios. Por tanto hay que tener mucho cuidado al momento de actualizar 
    esta información
(Sólo alumnos que hayan aprobado más de 120 créditos)" CssClass="azul" 
        Visible="False"></asp:Label>
    <br />
    <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr>
            <td style="height: 30px; ">
                Programa:
                <asp:DropDownList ID="dpCodigo_cpf" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="dpTipo" runat="server">
                    <asp:ListItem Value="0">Mostrar Estudiantes para finalizar su Plan de Estudios</asp:ListItem>
                    <asp:ListItem Value="1">Mostrar estudiantes que han finalizado su Plan de 
                    Estudios</asp:ListItem>
                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Ver" />
            &nbsp;</td>
        </tr>
    </table>
                                                <p>
                                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Visible="False" /></p>
                                                <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
                                                <asp:GridView ID="grwPosiblesEgresados" runat="server" 
                                                    
    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_alu" 
    Width="100%" BorderColor="Silver" 
                                                    
    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkElegir" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>                                                    
                                                        <asp:BoundField HeaderText="#">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                                                            <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:HyperLinkField DataNavigateUrlFields="codigo_alu" 
                                                            DataNavigateUrlFormatString="historial_personal.aspx?id={0}" 
                                                            DataTextField="alumno" HeaderText="Apellidos y Nombres" Target="_blank">
                                                            <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                                        </asp:HyperLinkField>
                                                        <asp:BoundField DataField="cicloing_alu" HeaderText="Ciclo Ingreso">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estadoactual_alu" 
                                                            HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="email_alu" HeaderText="Correo 1">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="email2_alu" HeaderText="Correo 2">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cCreditosAprobados_alu" 
                                                            HeaderText="Condición Final">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                       
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
    </form>
</body>
</html>
