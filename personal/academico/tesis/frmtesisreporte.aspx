<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmtesisreporte.aspx.vb" Inherits="personal_academico_tesis_frmcambiaretapatesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
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
                {form1.dpAcciones.disabled=false}
            else
                {form1.dpAcciones.disabled=true}
        }

        
        function OcultarTabla()
        {
            form1.style.display="none"
            document.getElementById("tblmensaje").style.display=""
        }
        
        function MostrarVentanaEstado(id)
        {
            var guardar=document.getElementById("imgGuardar")
            var lista=document.getElementById("tblLista")
            var frm=document.getElementById("tblConfirmar")  
            guardar.style.display="none"

            if (id!=2){
                frm.style.display="none"
                lista.style.display=""
            }
            if (id==2){
                if (lista.style.display==""){
                    lista.style.display="none"
                    frm.style.display=""
                }
                else{
                    frm.style.display="none"
                    lista.style.display=""
                }
            }
            if (id==0 || id==1){
                guardar.style.display=""
            }
            
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
    <table width="100%" cellpadding="4" cellspacing="0" 
        style="border: 1px solid #808080; background-color: #F0F0F0;">
        <tr>
            <td class="usatTitulo">Reporte de Tesis</td>
            <td align="right">
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    
    <table width="100%" cellpadding="3" cellspacing="0" id="tblLista">
        <tr>
            <td width="20%">Escuela Profesional:</td>
            <td width="80%">
            <asp:DropDownList ID="dpEscuela" runat="server" Font-Size="9px" AutoPostBack="True"></asp:DropDownList>
            &nbsp;Ciclo Académico:                   
                <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack ="true"
                    Height="16px" Width="156px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="20%">Buscar matriculados:</td>
            <td width="80%">&nbsp;<asp:DropDownList ID="dpCurso" runat="server" Font-Size="9px" 
                    TabIndex="2" AutoPostBack="True">
                            </asp:DropDownList>
                            &nbsp;o
                Título de tesis:
                <asp:TextBox ID="txtTermino" runat="server" Font-Size="8pt" MaxLength="20" 
                    TabIndex="3"></asp:TextBox>
                            <asp:ImageButton ID="imgBuscar" runat="server" 
                    ImageAlign="AbsMiddle" ImageUrl="../../../images/menus/buscar_small12.gif" 
                    Visible="true" EnableViewState="true" />
                <asp:ImageButton ID="cmdExportar" runat="server" ImageAlign="AbsMiddle" 
                    ImageUrl="../../..//images/ext/xls.gif" Visible="False" 
                    EnableViewState="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" DataKeyNames="codigo_tes" GridLines="Vertical" 
                    Width="100%" >
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                     
                        <asp:BoundField DataField="titulo_tes" HeaderText="Título" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="asesor" HeaderText="Asesor" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="autor" HeaderText="Autor" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_cac" HeaderText="Ciclo Matricula" />
                        <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="notafinal_dma" HeaderText="Nota Final" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="obs" HeaderText="Bloq." >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nro.Asesorías" HeaderText="Nro.Asesorías" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado tesis registradas</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
     
<table style="width: 100%;display:none" class="contornotabla" cellpadding="3" id="tblConfirmar">
            <tr>
                            <td width="30%" colspan="2" 
                                style="width: 100%; background-color: #3366CC; color: #FFFFFF; font-weight: bold;">
                                Confirme el cambio de etapa.
                            </td>
                        </tr>
            <tr>
                            <td width="30%">Fecha de Aprobación</td>
                            <td width="70%">
                <asp:TextBox ID="txtFechaAprobacion" runat="server" Width="100px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC" MaxLength="12"></asp:TextBox>
                <asp:Button ID="cmdInicio" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaAprobacion,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" Font-Size="10px" />
                            </td>
                        </tr>
            <tr>
                            <td width="30%">Nueva Fase</td>
                            <td width="70%">
                                <asp:Label ID="lblFase" runat="server" Font-Bold="True" Font-Size="11px" 
                                    ForeColor="Blue"></asp:Label>
                                &nbsp;<asp:HiddenField ID="hdFase" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="2">Comentarios</td>
			</tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:TextBox ID="TxtComentario" runat="server" Height="114px" TextMode="MultiLine"
                                    Width="98%" Font-Size="9pt" MaxLength="1000"></asp:TextBox></td>
                        </tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:CheckBox ID="chkBloquear" runat="server" 
                                    Text="Bloquear registro de asesorías a las tesis seleccionadas, en esta nueva etapa." />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="center" colspan="2">
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>
                                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Text="Guardar" 
                                    OnClientClick="OcultarTabla()"/>
                                <asp:Button ID="cmdCerrar" runat="server" CssClass="salir" Text="Cancelar" 
                                    OnClientClick="MostrarVentanaEstado(2);document.getElementById('dpAcciones').value=-2;return(false)" 
                                    UseSubmitBehavior="False"/>
                            </td>
            </tr>
                        </table>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img src="../../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
