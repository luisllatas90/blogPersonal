<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmactivaraulaporfechas.aspx.vb" Inherits="frmCargaPorFecha" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Activar aula virtual</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        function MarcarTodasLasTesis(obj)
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
                {form1.cmdHabilitar.disabled=false}
            else
                {form1.cmdHabilitar.disabled=true}
        }

        
        function OcultarTabla()
        {
            form1.style.display="none"
            document.getElementById("tblmensaje").style.display=""
        }
        
        function HabilitarEnvio(idcheck)
        {
            var arrChk = document.getElementsByTagName('input');
            var total=0
            
            if (arrChk.length>0){
                for (var i = 0 ; i < arrChk.length ; i++){
                    var chk = arrChk[i];
                    //verificar si es Check
                    if (chk.type === "checkbox" && chk.id!="GridView1_ctl01_chkHeader"){
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
                form1.cmdHabilitar.disabled=true
            }
            else{
                form1.cmdHabilitar.disabled=false
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
    <p class="usatTitulo">Habilitar aula virtual por semestre académico y fechas de carga académica</p>
    <table style="width:100%" class="contornotabla" cellpadding="3" cellspacing="0">
        <tr>
            <td style="width: 15%">
                Ciclo Académico</td>
            <td style="width: 85%">
                <asp:DropDownList ID="dpCiclo" runat="server">
                </asp:DropDownList>
            &nbsp;Escuela Profesional
                <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                Carga académica</td>
            <td style="width: 85%">
&nbsp;<asp:DropDownList ID="dpTipo" runat="server">
                    <asp:ListItem Value="0">Registrada HOY</asp:ListItem>
                    <asp:ListItem Value="1">Registrada ESTA SEMANA</asp:ListItem>
                    <asp:ListItem Value="2">Registrada ESTE MES</asp:ListItem>
                    <asp:ListItem Value="3">Todas las fechas</asp:ListItem>
                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />

            &nbsp;<asp:Button ID="cmdHabilitar" runat="server" Text="Habilitar acceso" Enabled="False" />

            </td>
        </tr>
        </table>
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" DataKeyNames="codigo_cup,codigo_per,login_per" 
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Elegir">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarTodasLasTesis(this)"/>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="profesor" HeaderText="Profesor" >
                            <ItemStyle Font-Size="7pt" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="identificador_cur" HeaderText="Código" >
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cur" HeaderText="Curso" >
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" >
                            <ItemStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" >
                            <ItemStyle Font-Size="7pt" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" >
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="matriculados" HeaderText="Matriculados" >
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado carga académica según el criterio especificado</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                </asp:GridView>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
