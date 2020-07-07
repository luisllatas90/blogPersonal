<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEgresadoAlumni_sin PE.aspx.vb" Inherits="academico_frmEgresadoAlumni" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Egresado: Finalizaron el Plan de Estudio</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script> 
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
	        jQuery(function($) {
	            $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
	            //   $("#txttelefono").mask("(999)-9999999");
	            //   $("#txtcelular").mask("(999)-9999999");  
	        });

	    })
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
    <asp:Label ID="lblAviso" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>                                             
    
    <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr>
            <td style="height: 30px; ">
                Escuela Profesional:
                <asp:DropDownList ID="dpCodigo_cpf" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Apellidos y Nombres:"></asp:Label><asp:TextBox
                    ID="txtAlumno" runat="server" Width="200px"></asp:TextBox>
            &nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Ver" />
            </td>
        </tr>
    </table>
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
                                                            DataNavigateUrlFormatString="frmLinkEgresados.aspx?id={0}" 
                                                            DataTextField="alumno" HeaderText="Apellidos y Nombres" Target="_blank">
                                                            <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                                        </asp:HyperLinkField>
                                                        <asp:BoundField DataField="cicloing_alu" HeaderText="Ciclo Ingreso">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estadoactual_alu" 
                                                            HeaderText="Estado Actual">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="email_alu" HeaderText="E-mail 1" Visible="False">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="email2_alu" HeaderText="E-mail 2" Visible="False">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="cCreditosAprobados_alu" 
                                                            HeaderText="Crd. Aprobados">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DebeTesis" HeaderText="Debe Tesis">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalCredElecObl_Pes" 
                                                            HeaderText="Cred. Obl. Electivo">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalCreObl_Pes" HeaderText="Cred. Plan Estudio" >
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_Cac" HeaderText="Ult. Matricula" />
                                                    </Columns>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <p>
                                                    <asp:Label ID="Label2" runat="server" Text="Ciclo Egreso:"></asp:Label>
                                                    <asp:DropDownList ID="cboCiclo" runat="server">
                                                    </asp:DropDownList>
                                                    &nbsp;
                                                    <!-- 
                                                    <asp:Label ID="Label3" runat="server" Text="Fecha:"></asp:Label>
                                                    <asp:TextBox ID="TxtFechaNac" runat="server" Width="120px"></asp:TextBox> -->
                                                    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Visible="False" />
                                                </p>
                                                <br />   
                                                
                                                <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
    </form>
</body>
</html>
