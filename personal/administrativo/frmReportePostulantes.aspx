<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReportePostulantes.aspx.vb" Inherits="administrativo_frmReportePostulantes" Theme="Acero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Lista de Postulantes</title>
 
<style type="text/css">
    
    .ModalBackground {
	background-color: Gray;
	/* filter: alpha(opacity=70); */
	/* opacity: 0.7; */
    }
    .ModalPopup {
	    background-color: #eeeeee;
	    border-width: 1px;
	    border-style: solid;
	    border-color: Gray;
	    padding: 3px;
	    font-size: small	    
    } 
</style>

<%--<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">--%>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link href="../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />        		
<script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
<link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
<script type="text/javascript" src="jspostulantes.js?x=1" ></script>    
    
	<script type="text/javascript" language="javascript">

	    $(document).ready(function() {	        
	        checkuncheckall();
	        $("#cmdDescargar").hide();
	        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(checkuncheckall);
	    });
	    	        	    	    		    	    
	</script>
</head>


<body>
    <form id="form1" runat="server">  
    <div id="diverrores" runat="server" style="color:red; font-size:large"></div>     
            
    <asp:ScriptManager ID="ScriptManager1" runat="server">
     <%--   <Scripts>
            <asp:ScriptReference Path="PopUp.js" />            
        </Scripts>--%>
    </asp:ScriptManager>
    
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(
            function(sender, arg) {
                var modalPopupBehavior = $find('idmpeLoading');
                modalPopupBehavior.hide();
            }
        );

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(
            function(sender, args) {
                var modalPopupBehavior = $find('idmpeLoading');
                modalPopupBehavior.show();
            }
        );    
    </script>
    
                   
   <%--<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">    
    </ajaxtoolkit:toolkitscriptmanager>--%>
    
  <%--<asp:Panel runat="server" ID="warningPopup" style="display: none;" CssClass="ModalPopup">
    <div class="orderLabel">
        Cargando ... espere por favor.
        <br /><br />        
    </div>
    </asp:Panel>

    <ajaxtoolkit:modalpopupextender ID="warningMPE"  runat="server"
    TargetControlID="dummyLink" PopupControlID="warningPopup"
    BehaviorID="warningMPE" BackgroundCssClass="ModalBackground">
    </ajaxtoolkit:modalpopupextender>           
    <asp:HyperLink ID="dummyLink" runat="server" NavigateUrl="#"></asp:HyperLink> --%>                 
        
   <ajaxtoolkit:ModalPopupExtender ID="mpeLoading" runat="server" BehaviorID="idmpeLoading" 
    PopupControlID="pnlLoading" TargetControlID="dummyLink" EnableViewState="false" 
    DropShadow="true" BackgroundCssClass="ModalBackground" />    
    
    <asp:Panel ID="pnlLoading" runat="server" Width="300" Height="50" HorizontalAlign="Center" 
    CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <br />Aguarde un momento...</asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />
    <asp:HyperLink ID="dummyLink" runat="server" NavigateUrl="#"></asp:HyperLink> 
              
    <div id="contenido" runat="server">
    <p class="usatTitulo">Lista de participantes</p>
    <table width="100%">
        <tr>
            <td style="width:10%">Pr. Admisión</td>
            <td style="width:15%">
                <asp:DropDownList ID="ddlProceso" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
            <td style="width:10%">Postulación</td>
            <td style="width:15%">
                 <asp:DropDownList ID="ddlEstPostulacion" runat="server" Width="100%">
                    <asp:ListItem Selected="True" Value="%">&gt;&gt; Seleccione&lt;&lt;</asp:ListItem>
                    <asp:ListItem Value="P">Postulante</asp:ListItem>
                    <asp:ListItem Value="I">Ingresante</asp:ListItem>
                    <asp:ListItem Value="R">Retirado</asp:ListItem>
                </asp:DropDownList>
                
            </td>
            <td style="width:12%">Mod. Ingreso</td>
            <td style="width:38%">
                 <asp:DropDownList ID="ddlModalidad" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Buscar por:</td>
            <td>
                 <asp:DropDownList ID="ddlFiltro" runat="server" AutoPostBack="true" Width="100%">
                     <asp:ListItem Value="D">DNI</asp:ListItem>
                     <asp:ListItem Value="N">Nombres</asp:ListItem>
                     <asp:ListItem Value="CU">Código universitario</asp:ListItem>
                </asp:DropDownList>            
                </td>
            <td colspan="2">
                <asp:TextBox ID="txtCodigoUni" runat="server" Visible="false" Width="98%"></asp:TextBox>
                <asp:TextBox ID="txtDNI" runat="server" Width="98%"></asp:TextBox>
                <asp:TextBox ID="txtNombres" runat="server" Visible="false" Width="98%"></asp:TextBox>
                </td>            
            <td>Centro de Costos</td>
            <td> 
                <asp:DropDownList ID="ddlCeco" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>        
        <tr>
            <td>Categorización</td>
            <td>
                 <asp:DropDownList ID="ddlCategorizado" runat="server" Width="100%">
                     <asp:ListItem Value="1">Si</asp:ListItem>
                     <asp:ListItem Value="0">No</asp:ListItem>
                     <asp:ListItem Selected="True" Value="%">Todos</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>Impr. Carta</td>
            <td>
                <asp:DropDownList ID="ddlImprimio" runat="server" Width="100%">
                    <asp:ListItem Value="1">Impresa</asp:ListItem>
                    <asp:ListItem Value="0">No impresa</asp:ListItem>
                    <asp:ListItem Selected="True" Value="%">Todos</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>Carrera Profesional</td>
            <td>
                <asp:DropDownList ID="ddlEscuela" runat="server" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Letra</td>
            <td>
                <asp:DropDownList ID="cboLetra" runat="server" Width="100%">
                     <asp:ListItem Value="A">A</asp:ListItem>
                     <asp:ListItem Value="B">B</asp:ListItem>
                     <asp:ListItem Value="C">C</asp:ListItem>
                     <asp:ListItem Value="D">D</asp:ListItem>
                     <asp:ListItem Value="E">E</asp:ListItem>
                     <asp:ListItem Value="F">F</asp:ListItem>
                     <asp:ListItem Value="G">G</asp:ListItem>
                     <asp:ListItem Value="H">H</asp:ListItem>
                     <asp:ListItem Value="I">I</asp:ListItem>
                     <asp:ListItem Value="J">J</asp:ListItem>
                     <asp:ListItem Value="K">K</asp:ListItem>
                     <asp:ListItem Value="L">L</asp:ListItem>
                     <asp:ListItem Value="M">M</asp:ListItem>
                     <asp:ListItem Value="N">N</asp:ListItem>
                     <asp:ListItem Value="Ñ">Ñ</asp:ListItem>
                     <asp:ListItem Value="O">O</asp:ListItem>
                     <asp:ListItem Value="P">P</asp:ListItem>
                     <asp:ListItem Value="Q">Q</asp:ListItem>
                     <asp:ListItem Value="R">R</asp:ListItem>
                     <asp:ListItem Value="S">S</asp:ListItem>
                     <asp:ListItem Value="T">T</asp:ListItem>
                     <asp:ListItem Value="U">U</asp:ListItem>                     
                     <asp:ListItem Value="V">V</asp:ListItem>
                     <asp:ListItem Value="W">W</asp:ListItem>
                     <asp:ListItem Value="X">X</asp:ListItem>
                     <asp:ListItem Value="Y">Y</asp:ListItem>
                     <asp:ListItem Value="Z">Z</asp:ListItem>                     
                     <asp:ListItem Value="">TODOS</asp:ListItem>                     
                </asp:DropDownList>
            </td>
            <td>Pag&oacute; matrícula</td>
            <td>
                <asp:CheckBox ID="chkMatCancelada" runat="server"  /></td>
            <td></td>
            <td align="right">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="BotonBuscar"/>
            </td>
        </tr>
    </table>                                    
    <hr />
    
    <div id="accion">
        <asp:Label ID="Label1" runat="server" Text="Acción"></asp:Label>
        <asp:DropDownList ID="ddlAccion" runat="server">
        </asp:DropDownList>
            
      <asp:Button ID="btnAccion" runat="server" Text="Procesar" 
            SkinID="BotonAceptar" />                      
                            
        <input name="cmdDescargar" id="cmdDescargar" type="button" value="Imprimir" 
            onClick="DescargarCartasCategorizacion(); " class="BU_boton_imprimir" 
            style="height:26px; width:70px"  />        
        <input type="hidden" id="txtTest" runat="server" />
        <%--<asp:Button ID="btnExportar" runat="server" SkinID="BotonAExcel" 
                Text="Exportar" />  --%>   
        <input id="btnExportar2" type="button" value="Exportar" onclick="exportar()" runat="server"/>   
    </div>
    
            
    &nbsp;<br />
    <br />
    
     <p>
         (*). Se muestra el resumen de las acciones procesadas para los participantes.       
        </p>
        
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="divresumen" runat="server" style="color:red"></div>   
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grwListaPersonas" 
                EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>  
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="grwListaPersonas" runat="server"
        AutoGenerateColumns="False" 
            DataKeyNames="codigo_Alu,codigo_pso,EstadoPostulacion,imprimiocartacat_Dal,Categorizacion,otroalu,categorizado_Dal" CellPadding="3" 
        SkinID="skinGridViewLineas" EmptyDataText="No se encontraron registros.">
        
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
            </asp:BoundField>
            <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc.">
            </asp:BoundField>
            <asp:BoundField HeaderText="Participante" DataField="participante" />            
            <asp:BoundField DataField="codUniversitario" HeaderText="Cód. Univ." >
            </asp:BoundField>
            <asp:BoundField DataField="carrera" HeaderText="Escuela" />
            <asp:BoundField DataField="nombre_Min" HeaderText="Modalidad Ingreso" />
            <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo" />
            <asp:BoundField DataField="CicloIngreso" HeaderText="Ciclo Ingreso" />
            <asp:BoundField DataField="fechaRegistro_Dal" DataFormatString={0:g}  HeaderText="Fecha Registro" />
            <asp:BoundField DataField="usureg_Dal" HeaderText="Usuario Registro" />
            <asp:BoundField DataField="EstadoPostulacion" HeaderText="Estado" />                                                              
            
            <asp:TemplateField>            
                  <HeaderTemplate>                
                         <asp:CheckBox ID="ChkAll" runat="server" />            
                  </HeaderTemplate>            
                  <ItemTemplate>                
                         <asp:CheckBox ID="chkSel" CssClass="chkSel" runat="server" Style="position: static"/>                                    
                         <asp:HiddenField ID="codigo_AluVal" runat="server" Value='<%#  Format(Second(now()), "0#") & Eval("codigo_Alu") %>' />                          
                  </ItemTemplate>        
            </asp:TemplateField>
                       
            <asp:TemplateField HeaderText="Observación">
                    <ItemTemplate>
                        <asp:TextBox ID="txtObservacion" runat="server" Style="position: static" Text='<%# Bind("observacion_Dal") %>'></asp:TextBox>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nota Ingreso">
                     <ItemTemplate>
                        <asp:TextBox ID="txtNota" runat="server" Style="position: static; width:40px" Text='<%# Bind("notaIngreso_Dal") %>'></asp:TextBox>
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Categorización">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCategorizacion" CssClass="cat" runat="server" Style="position: static; width:40px" Text='<%# Bind("Categorizacion","{0:n}") %>'></asp:TextBox>
                    </ItemTemplate>
            </asp:TemplateField>             
                                        
            <asp:BoundField DataField="imprimiocartacat_Dal" HeaderText="Imprimió Carta" />                                 
            <asp:BoundField DataField="categorizado_Dal" HeaderText="Categorizado" />
            <asp:BoundField DataField="notaIngreso_Dal" HeaderText="notaIngreso_Dal" Visible="false"/>
            <asp:BoundField DataField="observacion_Dal" HeaderText="observacion_Dal" Visible="false"/>
            <asp:BoundField DataField="foto_alu" HeaderText="Foto" Visible="true"/>
            
  
        </Columns>
        <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
    </asp:GridView>
    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>     
                    
    <script>

        $("#ddlAccion").change(function() {
            var str = "";
            MostrarOcultarCheck(str);
                        
        })
        .change();
    </script>
</div>        
</form>
   
</body>
</html>