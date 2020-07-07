<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmHorarioListaTrabajadores.aspx.vb" Inherits="personal_frmHorarioListaTrabajadores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo.css" rel="stylesheet" type="text/css" />  
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />    
    <script src="script/jquery-1.12.3.min.js" type="text/javascript"></script>    
    <script type="text/javascript">    
    
            $(document).ready(function() {        
            var chkBox = $("input[id$='ChkAll']");        
            chkBox.click(             
                function() {
            $("#gvListaTrabajadores INPUT[type='checkbox']")                 
                    .attr('checked', chkBox                 
                    .is(':checked'));             
                    });        
                    
                    // To deselect CheckAll when a GridView CheckBox
                    // is unchecked

                    $("#gvListaTrabajadores INPUT[type='checkbox']").click(
                    function(e) {
                    if (!$(this)[0].checked) {
                        chkBox.attr("checked", false);
                    }
                });
            });
    
    </script>
       
    
    <style type="text/css">
          
          .usatFormatoCampodd
             {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:80px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:100px;        	
        }
          
          .usatFormatoCampoddl
         {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:10px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:200px;        	
        }
          
       .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:60px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:90px;        	
        }
        
        .usatFormatoCampox
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-right:auto;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:500px;        	
        }
        
        .usatFormatoCampo1
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:200px;        	
        }
        
        .usatFormatoCampoAnchoddl
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:70px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:900px;
        	/*border:1px solid red;*/
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:10px;        	
        	padding-left:70px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:600px;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;
            height: 10px;
            width: 831px;
            height: 30px;
        }
        
        .usatDescripcionTitulo  
        {
        	font-family: Arial;
	        font-size: 10pt;	        
	        color: #27333c;
        }
        
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:220px;	        
	        max-height:300PX;
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:185px;	        
	        max-height:300PX;
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanelConsulta
        {        
            border: 1px solid #C0C0C0;	        	        
	        /*-moz-border-radius: 15px; */
	        padding-top:10px;
	        margin-top:10px; 
	        padding-bottom:10px;	              
        }
        
        #lblSubtitulo
        {
        	position:absolute;
        	top: 35px;
        	left: 18px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        }
        
        #lblSubtitulo1
        {
        	position:absolute;
        	top: 370px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        }
        
         #lblSubtitulo2
        {
        	position:absolute;
        	top: 276px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        }
        
        #chkSubtitulo3
        {
        	position:absolute;
        	top: 482px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        }
        
         #lblSubtitulo3
        {
        	position:absolute;
        	top: 378px;
        	left: 30px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:120px;        
        	text-align:center;
        }
        
        .cuadroMensajes
        {
        	/*background-color:Red;*/
        	height:20px;
        	padding-top:10px;  
        	     	
        }
        
         #lblConsulta
        {
        	position:absolute;
        	top: 460px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	width:150px;        
        	text-align:center;
        }
                        
    </style>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Lista de TrabajaLista de Trabajadores
    <asp:ScriptManager 
            ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       
    </div>
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Filtros"></asp:Label>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label4" runat="server" Text="Estado Horario"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlEstadoHorario" runat="server" Width="80px" 
                    AutoPostBack="True" Font-Size="XX-Small">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkTodos" runat="server" 
                    AutoPostBack="True" />
                <asp:CheckBox ID="chkenvioDirector" runat="server" AutoPostBack="True" 
                    Text="Envio Trabajador" />
                <asp:CheckBox ID="chlenvioDirPersonal" runat="server" AutoPostBack="True" 
                    Text="Envio Director" />
                </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="lblCategoria" runat="server" Text="Tipo Personal"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlTipoPersonal" runat="server" Width="400px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>   
         <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label1" runat="server" Text="Dedicacion"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlDedicacion" runat="server" Width="400px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>  
         <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label2" runat="server" Text="Centro Costos"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlCentroCosto" runat="server" Width="400px" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div> 
             <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label10" runat="server" Text="Estado Planilla"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlEstadoPlanillaF" runat="server" Width="400px" 
                    AutoPostBack="True">
            </asp:DropDownList>
                <asp:Label ID="lblPruebas" runat="server" Text="Label" Visible="False"></asp:Label>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
              <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label11" runat="server" Text="Estado Campus"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
            <asp:DropDownList ID="ddlEstadoCampusF" runat="server" Width="400px" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="lblPruebas2" runat="server" Text="Label" Visible="False"></asp:Label>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
    </div>
    <br />
    <div class="usatPanel2">
        <asp:Label ID="lblSubtitulo2" runat="server" Text="Operaciones"></asp:Label>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label9" runat="server" Text="Calificar Horario"></asp:Label>
            </div>
                <div class="usatFormatoCampoAncho">
                    <asp:DropDownList ID="ddlEvaluacionHorario" runat="server" Width="185px" 
                            AutoPostBack="True">
                        </asp:DropDownList>
                         
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label12" runat="server" Text="Asunto"></asp:Label>
            </div>
                <div class="usatFormatoCampoAncho">
                
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtAsunto" runat="server" Width="600px"></asp:TextBox>        
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEvaluacionHorario" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label3" runat="server" Text="Observación"></asp:Label>
            </div>
                <div class="usatFormatoCampoAncho">
                
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtObservacion" runat="server" Width="600px"></asp:TextBox>        
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEvaluacionHorario" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label5" runat="server" Text="Estado Planilla"></asp:Label>
            </div>
            <div class="usatFormatoCampo">
                <asp:DropDownList ID="ddlEstadoPlanilla" runat="server" Width="150px">
                </asp:DropDownList>
            </div>
                 <div class="usatFormatoCampodd">
                <asp:Label ID="Label6" runat="server" Text="Estado Campus"></asp:Label>
            </div>
                 <div class="usatFormatoCampoddl">
                <asp:DropDownList ID="ddlEstadoCampus" runat="server" Width="150px">
                </asp:DropDownList>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label7" runat="server" Text="Envió a Director"></asp:Label>
            </div>
            <div class="usatFormatoCampo">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEstadoEnvioDirector" runat="server" Width="150px">
                        </asp:DropDownList>    
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlEvaluacionHorario" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </div>
             <div class="usatFormatoCampodd">
                <asp:Label ID="Label8" runat="server" Text="Envió a Personal"></asp:Label>
            </div>
             <div class="usatFormatoCampoddl">
                 <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlEnvioDirPersonal" runat="server" Width="150px">
                        </asp:DropDownList>    
                    </ContentTemplate>
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddlEvaluacionHorario" 
                             EventName="SelectedIndexChanged" />
                     </Triggers>
                 </asp:UpdatePanel>
            </div>
            
            <div style="clear:both; height:1px;"></div>        
        </div>   
    
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
                <asp:Label ID="Label13" runat="server" Text="Acción"></asp:Label>
            </div>
                <div class="usatFormatoCampoAnchoAviso">
                
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblAccion" runat="server" Text=""></asp:Label>   
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlEvaluacionHorario" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
    
            <div style="margin-top:10px;">
            <div style="clear:both; height:1px;"></div>        
        </div>   
    
    </div>
    <div class="cuadroMensajes">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMensaje" runat="server" Visible="False" Font-Size="Large"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdEjecutar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        
        <div style="clear:both; height:10px;"></div>
    </div>
    <br />
    
            <div class="usatPanelConsulta">
                <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
                <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                                <asp:Button ID="cmdEjecutar0" runat="server" Text="   Activar" 
                                    CssClass="guardar2" /> <asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                                <asp:Button ID="cmdExportar" runat="server" CssClass="excel2" Text="Exportar" />
                    </td>
                        <td align="right" style="width:20%" valign="top">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                     <asp:Button ID="cmdEjecutar" runat="server" Text="   Finalizar Enviar" 
                                    CssClass="guardar2" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlEvaluacionHorario" 
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                       
                               
                       </td>
            </tr>
        </table>
            
                    <asp:GridView ID="gvListaTrabajadores" runat="server" Width="100%" CellPadding="4" 
                        ForeColor="#333333" AutoGenerateColumns="False" 
                        DataKeyNames="Codigo" EmptyDataText="No se encontraron registros." 
                    PageSize="15">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <EmptyDataRowStyle ForeColor="#FF0066" />
                        <Columns>
                            <asp:BoundField HeaderText="N°" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            
                           <asp:TemplateField>            
                                <HeaderTemplate>                
                                    <asp:CheckBox ID="ChkAll" runat="server" />            
                                </HeaderTemplate>            
                                <ItemTemplate>                
                                    <asp:CheckBox ID="chkSel" class="chkSel" runat="server" />           
                                    </ItemTemplate>        
                            </asp:TemplateField>
                               
                            <asp:BoundField HeaderText="Codigo" DataField="Codigo" Visible="False" >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Trabajador" DataField="Trabajador" >
                                <ItemStyle HorizontalAlign="Left" Width="320px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TipoPersonal" HeaderText="TipoPersonal" >
                                <ItemStyle Width="155px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Dedicacion" HeaderText="Dedicacion" />
                            <asp:BoundField DataField="EstadoPlla" HeaderText="EstadoPlla" />
                            <asp:BoundField DataField="EstadoCpus" HeaderText="EstadoCpus" />
                            <asp:BoundField DataField="EstadoHorario" HeaderText="EstadoHorario" />
                            
                            <asp:BoundField DataField="EnvioDirector" HeaderText="EnvioTrabajador" />
                            <asp:BoundField DataField="EnvioPersonal" HeaderText="EnvioDirector" />
                            
                            <asp:BoundField DataField="ObservacionHorario" 
                                HeaderText="ObservacionHorario" />
                            <asp:BoundField DataField="descripcion_Cco" HeaderText="C.Costo" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                   
                        
                  
                    
                   
   </div> 
    
    <div>
     <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td style="text-align:center">
                    &nbsp;</td>
            </tr>
        </table>
    </div>
        
   
    
    </form>
</body>
</html>
