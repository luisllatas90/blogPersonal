<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaDocentesTesis.aspx.vb" Inherits="personal_frmListaDocentesTesis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/estilo.css" rel="stylesheet" type="text/css" />  
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />
    
    <script type='text/javascript'src='http://jqueryjs.googlecode.com/files/jquery-1.3.2.min.js'></script>
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
	        height:155px;	        
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
        	top: 50px;
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
    <div class="usatTituloAzul">Lista de Docentes Registro de Horas Tesis
       
                            <asp:Button ID="btnCancelar" runat="server" Text="Regresar" Width="100px" Height="22px" CssClass="salir" 
                            onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
    &nbsp;</div>
    <div style="clear:both; height:15px;"></div>        
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Enviar Email"></asp:Label>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label4" runat="server" Text="Asunto"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtAsunto" runat="server" Width="550px">Actualizar el Registro de Horario </asp:TextBox>
                </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            <asp:Label ID="Label1" runat="server" Text="Mensaje"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtMensaje" runat="server" Width="550px">Debe actualizar las horas dedicadas a la asesoría de tesis.</asp:TextBox>
                </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:1px;">
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAncho">
                &nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="img_EnviarCorreo" runat="server" 
                    ImageUrl="~/images/Enviar_email.png" ToolTip="Enviar Correo." />
                HT : Horas Tesis Reales. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; / HTR : Horas Tesis 
                Registradas.</div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        <div style="margin-top:15px;">
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
            <div style="clear:both; height:1px;"></div>        
        </div>
        
    </div>
    
    <div class="usatPanelConsulta">
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
                                    <asp:CheckBox ID="chkSel" class="chkSel" runat="server" Checked="True" />           
                                    </ItemTemplate>        
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                               
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                            <asp:BoundField DataField="Docente" HeaderText="Docente" >
                            <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HrsTesis" HeaderText="HT" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HrsTesisReg" HeaderText="HTR" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tesis del Docente">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlListaTesis" runat="server" Width="530px" 
                                        BackColor="#CCFFFF">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Width="520px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="estado_hop" HeaderText="ES" Visible="False" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EnvioDirector" HeaderText="ED" Visible="False" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EnvioPersonal" HeaderText="EP" Visible="False" >
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observacion" HeaderText="observación" 
                                Visible="False" />
                            <asp:BoundField DataField="EnvioEmail_tesis" HeaderText="Email" />
                            <asp:BoundField DataField="FechaEnvioEmail_tesis" HeaderText="FechaEnvio" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                   
                        
                  
                    
                   
   </div>
   
    </form>
</body>
</html>
