<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImportarPlanes.aspx.vb" Inherits="indicadores_frmImportarPlanes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    <link href="../css/MyStyles.css" rel="stylesheet" type="text/css" />

     <script type="text/javascript">
         var timoutID;
         
         function ShowMList() {
             var divRef = document.getElementById("divCheckBoxList");
             divRef.style.display = "block";
             var divRefC = document.getElementById("divCheckBoxListClose");
             divRefC.style.display = "block";
         }

         function ShowMList2() {
             var divRef2 = document.getElementById("divCheckBoxList2");
             divRef2.style.display = "block";
             var divRefC2 = document.getElementById("divCheckBoxListClose2");
             divRefC2.style.display = "block";
         }

         function HideMList() {
             document.getElementById("divCheckBoxList").style.display = "none";
             document.getElementById("divCheckBoxListClose").style.display = "none";
         }

         function HideMList2() {
             document.getElementById("divCheckBoxList2").style.display = "none";
             document.getElementById("divCheckBoxListClose2").style.display = "none";
         }
         
         function FindSelectedItems(sender, textBoxID) {
             var cblstTable = document.getElementById(sender.id);
             var checkBoxPrefix = sender.id + "_";
             var noOfOptions = cblstTable.rows.length;
             var selectedText = "";
             for (i = 0; i < noOfOptions; ++i) {
                 if (document.getElementById(checkBoxPrefix + i).checked) {
                     if (selectedText == "")
                         selectedText = document.getElementById(checkBoxPrefix + i).parentNode.innerText;
                     else
                         selectedText = selectedText + "," + document.getElementById(checkBoxPrefix + i).parentNode.innerText;
                 }
             }
             document.getElementById(textBoxID.id).value = selectedText;
         }
    </script>


    
   <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:5px;        	
        	padding-left:65px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:95px;        	
        }
        
        .usatFormatoCampoOpciones
        {
            float:left; 
        	font-weight:bold;
        	padding-top:5px;        
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:600px;
        	/*border:1px solid red;*/	
        
        	}
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:5px;        
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:500px;
        	/*border:1px solid red;*/
        }
        
        .usatFormatoCampoAnchoDet
        {
        	float:left; 
        	font-weight:bold;
        	padding-top:5px;        
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:265px;
        	/*border:1px solid red;*/
        	
        	}
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;            
            width: 831px;
            height: 20px;
        }
      
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:246px;	        
	        max-height:335PX;
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	  	            
        }
        
        .usatPanel2
        {        
            border: 1px solid #C0C0C0;	        
	        height:150px;	        	        
	        -moz-border-radius: 15px	        
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
        	position:relative;
        	top: -10px;
        	left: 10px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;        	
        	text-align:center;
        }
               
         #lblConsulta
        {
        	position:relative;
        	top: -20px;        	
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;
        	/*width:60px;        */
        	text-align:center;
        }
        
         #lblMetas
        {
        	position:relative;
        	top: -10px;
        	left: 20px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;        	      
        	text-align:center;
        }
        
         
        /***** Para avisos **************/
        .mensajeError
        {
        	border: 1px solid #e99491;
        	background-color: #fed8d5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;  	
        }
        
         .mensajeExito
        {
        	border: 1px solid #488e00;
        	background-color:#c5f4b5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;        	
        }
        
        /********************************/   
                        
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="usatTituloAzul">Importar Planes
    <a href="#" onclick="apprise('Permite importar los objetivos,indicadores y fórmulas.');">
        <img src="../Images/info.png" style="border:0" alt="Ayuda."/>
    </a>
    </div>
    
    <%--<div class="usatDescripcionTitulo">Añade una nueva categoría.</div>--%>
    
    <!-- Para avisos -->
    <div style="clear:both; height:5px"></div>
    
    <div id="avisos" runat="server" style="height:20px; padding-top:2px; width: 750px;">
            <asp:Image ID="Image1" runat="server" ImageUrl="../Images/beforelastchild.GIF"/>
            <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>            
    </div>
    <div style="clear:both; height:10px"></div>
    <!-------------------------------------------->
    
    <div class="usatPanel">
        <asp:Label ID="lblSubtitulo" runat="server" Text="Datos de registro" 
            Width="120px"></asp:Label>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label10" runat="server" Text="Plan"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                <asp:DropDownList ID="ddlPlan" runat="server" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>    
                
                
                            
            </div>            
            <div style="clear:both; height:2px;"></div>                    
        </div>
        
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label11" runat="server" Text="Años de Origen"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
                 
                <div id="divCustomCheckBoxList2" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList2()', 750);">
                    <table>
                        <tr>
                            <td align="right" class="DropDownLook2">
                                <input id="txtSelectedMLValues2" type="text" readonly="readonly" onclick="ShowMList2()" style="width:229px;" runat="server" />
                            </td>
                            <td align="left" class="DropDownLook2">
                                <img id="imgShowHide2" runat="server" src="~/Iconos/drop.gif" onclick="ShowMList2()" align="left" />                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="DropDownLook2">
                                <div>
            	                    <div runat="server" id="divCheckBoxListClose2" class="DivClose2">			                        
		                                <label runat="server" onclick="HideMList2();" class="LabelClose2" id="lblClose2"> x</label>
		                            </div>
                                    <div runat="server" id="divCheckBoxList2" class="DivCheckBoxList2">
		                                <asp:CheckBoxList ID="lstMultipleValues2" runat="server" Width="250px" CssClass="CheckBoxList2"></asp:CheckBoxList>						        			           			        
		                            </div>
		                        </div>
                            </td>
                        </tr>
                    </table>
                </div>
                                
                
                            
            </div>            
            <div style="clear:both; height:2px;"></div>                    
        </div>                  
             

                
        <div>
            <div class="usatFormatoCampo">
            <asp:Label ID="Label3" runat="server" Text="Años de Destino"></asp:Label>
            </div>
            <div class="usatFormatoCampoAncho">
               <div id="divCustomCheckBoxList" runat="server" onmouseover="clearTimeout(timoutID);" onmouseout="timoutID = setTimeout('HideMList()', 750);">
                    <table>
                        <tr>
                            <td align="right" class="DropDownLook">
                                <input id="txtSelectedMLValues" type="text" readonly="readonly" onclick="ShowMList()" style="width:229px;" runat="server" />
                            </td>
                            <td align="left" class="DropDownLook">
                                <img id="imgShowHide" runat="server" src="~/Iconos/drop.gif" onclick="ShowMList()" align="left" />                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="DropDownLook">
                                <div>
            	                    <div runat="server" id="divCheckBoxListClose" class="DivClose">			                        
		                                <label runat="server" onclick="HideMList();" class="LabelClose" id="lblClose"> x</label>
		                            </div>
                                    <div runat="server" id="divCheckBoxList" class="DivCheckBoxList">
		                                <asp:CheckBoxList ID="lstMultipleValues" runat="server" Width="250px" CssClass="CheckBoxList"></asp:CheckBoxList>						        			           			        
		                            </div>
		                        </div>
                            </td>
                        </tr>
                    </table>
                </div> 
            </div>           
            <div style="clear:both; height:15px;"></div>                    
        </div>   
                                         
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAnchoDet">
                <asp:Label ID="Label12" runat="server" Text="Número de Objetivos Importados: " 
                    Font-Bold="False"></asp:Label>
             </div>
             <div class="usatFormatoCampo" style="width:70px">
                 <asp:Label ID="lblcnt_Obj" runat="server" Text="0" ForeColor="#FF0066"></asp:Label>
            </div>
            <div style="clear:both; height:1px;"></div>
        </div>     
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAnchoDet">
                <asp:Label ID="Label1" runat="server" 
                    Text="Número de metas de objetivos Importados: " Font-Bold="False"></asp:Label>
             </div>
             <div class="usatFormatoCampo" style="width:70px">
                 <asp:Label ID="lblcnt_meta_obj" runat="server" Text="0" ForeColor="#FF0066"></asp:Label>
            </div>
             
            <div style="clear:both; height:1px;"></div>
        </div>    
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAnchoDet">
                <asp:Label ID="Label4" runat="server" Text="Número de Indicadores Importados: " 
                    Font-Bold="False"></asp:Label>
             </div>
             <div class="usatFormatoCampo" style="width:70px">
                 <asp:Label ID="lblcnt_ind" runat="server" Text="0" ForeColor="#FF0066"></asp:Label>
            </div>
             
            <div style="clear:both; height:1px;"></div>
        </div>
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAnchoDet">
                <asp:Label ID="Label6" runat="server" 
                    Text="Número de metas de indicadores Importados: " Font-Bold="False"></asp:Label>
             </div>
             <div class="usatFormatoCampo" style="width:70px">
                 <asp:Label ID="lblcnt_meta_ind" runat="server" Text="0" ForeColor="#FF0066"></asp:Label>
            </div>
             
            <div style="clear:both; height:1px;"></div>
        </div> 
        <div>
            <div class="usatFormatoCampo">
            </div>
            <div class="usatFormatoCampoAnchoDet">
                <asp:Label ID="Label2" runat="server" 
                    Text="Número de fórmulas importadas: " Font-Bold="False"></asp:Label>
             </div>
             <div class="usatFormatoCampo" style="width:70px">
                 <asp:Label ID="lblcnt_formula" runat="server" Text="0" ForeColor="#FF0066"></asp:Label>
            </div>
             
            <div style="clear:both; height:1px;"></div>
        </div>
            
                          
    </div>
    <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 
            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                
                    </td>
                        <td align="right" valign="top" style="width:20%">
                                <asp:Button ID="btnSubmit" runat="server" Text="   Importar" 
                                    CssClass="nuevo" ValidationGroup="grupo1" /> &nbsp;<asp:Button 
                                    ID="cmdCancelar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
