<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmsolicitudcambios.aspx.vb" Inherits="librerianet_cargaacademica_frmsolicitudcambios" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bitácora de solicitudes de acceso</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function MarcarTodasFilas(obj)
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
            
            if (obj.checked==true)
                {form1.dpAcciones.disabled=false}
            else
                {form1.dpAcciones.disabled=true}
        }

        function HabilitarEnvio(idcheck)
        {
            var arrChk = document.getElementsByTagName('input');
            var total=0
            
            if (arrChk.length>0){
                for (var i = 0 ; i < arrChk.length ; i++){
                    var chk = arrChk[i];
                    //verificar si es Check
                    if (chk.type === "checkbox" && chk.id!="grwPermisos_ctl01_chkHeader"){
                        if (chk.checked ==true){
                            total=total+1
                        }
                    }
                }
            }
            else{
	            if (idcheck.checked==true)
		            {total=1}
            }
            //Pintar Fila
		    if (idcheck.parentNode.parentNode.tagName=="TR"){
		        PintarFilaMarcada(idcheck.parentNode.parentNode,idcheck.checked)
		    }	
	        //Habilitar botón
            if (total==0){
                form1.dpAcciones.disabled=true
            }
            else{
                form1.dpAcciones.disabled=false
            }
         }
         
        function PintarFilaMarcada(obj,estado)
        {
            if (estado==true){
                obj.style.backgroundColor="#E6E6FA"//#395ACC
            }
            else{
                obj.style.backgroundColor="white"
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Bitcácora de solicitudes de Acceso</p>
    <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7; " 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Tipo de Acceso:
                <asp:DropDownList ID="dpnombretbl_acr" runat="server">
                    <asp:ListItem Value="cargaacademica">Carga Académica</asp:ListItem>
                    <asp:ListItem Value="lineahorario">Horarios</asp:ListItem>
                </asp:DropDownList>
&nbsp; Mostrar:
                <asp:DropDownList ID="dpFiltro" runat="server">
                    <asp:ListItem Value="0">Solicitudes Pendientes</asp:ListItem>
                    <asp:ListItem Value="1">Solicitudes Aprobadas</asp:ListItem>
                    <asp:ListItem Value="-1">Todas las solicitudes</asp:ListItem>
                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" CssClass="buscar2" Height="22px" 
                                                    Text="   Buscar" Width="60px" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" Font-Size="10pt" 
        ForeColor="Red"></asp:Label>
                <asp:GridView ID="grwPermisos" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" 
        DataKeyNames="OperadorAut_acr,fechareg_acr,codigo_acr" GridLines="Vertical" 
                    Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                        <asp:TemplateField HeaderText="Elegir">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarTodasFilas(this)"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="fechareg_acr" HeaderText="Fecha Reg." >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="personal" HeaderText="Apellidos y Nombres" >
<ItemStyle Font-Size="7pt"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_dac" HeaderText="Departamento Académico" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="obs_acr" HeaderText="Observaciones" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblEstado" runat="server" 
                                    Text='<%# iif(eval("aprobado_acr")=true,"Aprobado","Pendiente") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>&nbsp;No se han registrado "Solicitudes" según los criterios 
                            indicados.</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                </asp:GridView>
    <br />
    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" Enabled="False" 
        Text="    Autorizar Solicitud" Width="120px" />
    </form>
</body>
</html>
