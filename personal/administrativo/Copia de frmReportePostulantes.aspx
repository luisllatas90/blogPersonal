<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Copia de frmReportePostulantes.aspx.vb" Inherits="administrativo_frmReportePostulantes" Theme="Acero" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Lista de Postulantes</title>
   
<style type="text/css">

    .ModalBackground {
	background-color: Gray;
	filter: alpha(opacity=70);
	opacity: 0.7;
    }
    .ModalPopup {
	    background-color: #eeeeee;
	    border-width: 1px;
	    border-style: solid;
	    border-color: Gray;
	    padding: 3px;
	    font-size: small;
    }
</style>

<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link href="../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />        		
<script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
<link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
<script src="http://code.jquery.com/jquery-latest.js"></script>
    
	<script type="text/javascript" language="javascript">

	    $(document).ready(function() {
	        var chkBox = $("input[id$='ChkAll']");	        

	        chkBox.click(function() {
	        $("#grwListaPersonas INPUT[type='checkbox']")
	                        .attr('checked', chkBox
	                        .is(':checked'));
	    	        });

	    	        // To deselect CheckAll when a GridView CheckBox
	    	        // is unchecked

	    	        $("#grwListaPersonas INPUT[type='checkbox']").click(
	                    function(e) {
	                        if (!$(this)[0].checked) {
	                            chkBox.attr("checked", false);
	                        }
	                    });	             
	    	    });

	           function DescargarCartasCategorizacion() {
	                    var pagina = "doccartacategorizacion.asp";
	                    var DataKeyName = ""; 
	                    var alumnosArray = new Array();	                      

	                    $("#cmdDescargar").click(function() {
	                        var pagina = "doccartacategorizacion.asp";	   
	                        var id;
	                        var i = 0;
	                        //var totalRows = $("#grwListaPersonas tr").length;                    

	                        if ($("#ddlAccion").val() == "I") {
	                            var gridView1Control = $("#grwListaPersonas");
	                            var DataKeyName = "";
	                            $('#grwListaPersonas tr:has(input:checked) input[type=hidden]').each(function(i, item) {
	                                DataKeyName = $(item).val();
	                                alumnosArray[i] = DataKeyName;
	                                i = i + 1;                            
	                            });
	                            window.open(pagina + "?alumnosArray=" + alumnosArray);                        
	                        }
	                    })
	                }                              
	     
	        function DisplayIddleWarning() {
	            $find('warningMPE').show();	            
	        }
	     
	        function HideIddleWarning() {
	            $find('warningMPE').hide();	            
	        }
	        
	    	function PintarFilaElegida(obj) {
	    	        if (obj.style.backgroundColor == "white") {
	    	            obj.style.backgroundColor = "#E6E6FA"//#395ACC
	    	        }
	    	        else {
	    	            obj.style.backgroundColor = "white"
	    	        }
	    	 }	    	    	    		    	    
	</script>
</head>


<body>
    <form id="form1" runat="server">    
       
    <asp:ScriptManager ID="ScriptManager1" runat="server">
     <%--   <Scripts>
            <asp:ScriptReference Path="PopUp.js" />            
        </Scripts>--%>
    </asp:ScriptManager>
    
    <script type="text/javascript">
        var mpeLoading;
        function initializeRequest(sender, args) {
            mpeLoading = $find('idmpeLoading');
            mpeLoading.show();            
        }
        function endRequest(sender, args) {
            $find('idmpeLoading').hide();
        }

        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
    </script>
                
   <%--<ajaxtoolkit:toolkitscriptmanager ID="ToolkitScriptManager1" runat="server">    
    </ajaxtoolkit:toolkitscriptmanager>--%>
    
<%--    <asp:Panel runat="server" ID="warningPopup" style="display: none;" CssClass="modalPopup">
    <div class="orderLabel">
        Cargando ... espere por favor.
        <br /><br />        
    </div>
    </asp:Panel>

    <ajaxtoolkit:modalpopupextender ID="warningMPE"  runat="server"
    TargetControlID="dummyLink" PopupControlID="warningPopup"
    BehaviorID="warningMPE" BackgroundCssClass="modalBackground">
    </ajaxtoolkit:modalpopupextender>       
    
    <asp:HyperLink ID="dummyLink" runat="server" NavigateUrl="#"></asp:HyperLink>  --%>  
        
    <ajaxtoolkit:ModalPopupExtender ID="mpeLoading" runat="server" BehaviorID="idmpeLoading" 
    PopupControlID="pnlLoading" TargetControlID="btnBuscar" EnableViewState="false" 
    DropShadow="true" BackgroundCssClass="ModalBackground" />
    
    <asp:Panel ID="pnlLoading" runat="server" Width="300" Height="50" HorizontalAlign="Center" 
    CssClass="ModalPopup" EnableViewState="false" Style="display: none">
        <br />Aguarde un momento...</asp:Panel>
    <asp:Button ID="btnLoading" runat="server" Style="display: none" />
    
    <p class="usatTitulo">Lista de participantes</p>
                                
    <div>
        <div>
            <div style="float:left; width:110px">Proceso de admisión</div>
            <div style="float:left; width:200px">
                <asp:DropDownList ID="ddlProceso" runat="server" Width="200px">
                </asp:DropDownList>
            </div>
             <div style="float:left; width:110px; padding-left:20px">Centro de costos</div>
             <div style="float:left; width:320px">
                 <asp:DropDownList ID="ddlCeco" runat="server" Width="320px">
                </asp:DropDownList>
            </div>
             <div style="float:left; width:110px; padding-left:20px">Modalidad de Ingreso</div>
             <div style="width:260px">
                 <asp:DropDownList ID="ddlModalidad" runat="server" Width="260px">
                </asp:DropDownList>
            </div>
        </div>
        
        <div>
             <div style="float:left; width:110px">Buscar por:</div>
              <div style="float:left; width:650px">
                 <asp:DropDownList ID="ddlFiltro" runat="server" AutoPostBack="true" Width="130px">
                     <asp:ListItem Value="D">DNI</asp:ListItem>
                     <asp:ListItem Value="N">Nombres</asp:ListItem>
                     <asp:ListItem Value="CU">Código universitario</asp:ListItem>
                </asp:DropDownList>            
            
                <asp:TextBox ID="txtNombres" runat="server" Visible="false" Width="510px"></asp:TextBox>
                <asp:TextBox ID="txtDNI" runat="server" Width="510px"></asp:TextBox>
                <asp:TextBox ID="txtCodigoUni" runat="server" Visible="false" Width="510px"></asp:TextBox>
             </div>
             <div style="float:left; width:110px; padding-left:20px">Estado Postulación</div>
             <div style="float:left; width:260px; padding-top:5px">
                <asp:DropDownList ID="ddlEstPostulacion" runat="server" Width="260px">
                    <asp:ListItem Selected="True" Value="%">&gt;&gt; Seleccione&lt;&lt;</asp:ListItem>
                    <asp:ListItem Value="P">Postulante</asp:ListItem>
                    <asp:ListItem Value="I">Ingresante</asp:ListItem>
                    <asp:ListItem Value="R">Retirado</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="width:85px; text-align:center">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="BotonBuscar" />
            </div>
        </div>
    </div>
    
    <hr />
    
    <div id="accion">
        <asp:Label ID="Label1" runat="server" Text="Acción"></asp:Label>
        <asp:DropDownList ID="ddlAccion" runat="server">
        </asp:DropDownList>
            
      <asp:Button ID="btnAccion" runat="server" Text="Procesar" 
            SkinID="BotonAceptar" />
                    
        <input name="cmdDescargar" id="cmdDescargar" type="button" value="Generar" onClick="DescargarCartasCategorizacion(); return false" class="word">
    </div>
    
            <asp:Button ID="cmdExportar" runat="server" SkinID="BotonAExcel" 
        Text="Exportar" />
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
        
     <asp:GridView ID="grwListaPersonas" runat="server"
        AutoGenerateColumns="False" DataKeyNames="codigo_Alu, codigo_pso, EstadoPostulacion, imprimiocartacat_Dal, Categorizacion, otroalu" CellPadding="3" 
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
            
           
            <asp:TemplateField HeaderText="Observacion">
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
                        <asp:TextBox ID="txtCategorizacion" CssClass="cat" runat="server" Style="position: static; width:40px" Text='<%# Bind("Categorizacion") %>'></asp:TextBox>
                    </ItemTemplate>
            </asp:TemplateField>             
                                        
            <asp:BoundField DataField="imprimiocartacat_Dal" HeaderText="Imprimió Carta" />                     
            
        </Columns>
        <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
    </asp:GridView>
       
    <script>

        $("#ddlAccion").change(function() {
            var str = "";
            $("#ddlAccion option:selected").each(function() {
                str = $(this).val();

                if (str == "I") {
                    //Opción de impresión solo cuando tienen categorización y es ingresante, y aun no se imprime
                    var estado, categorizacion, imprimio;
                    $("#grwListaPersonas tbody tr").each(function(index) {
                        $(this).children("td").each(function(index2) {
                            switch (index2) {
                                case 11:
                                    estado = $(this).text();
                                    break;
                                case 15:
                                    categorizacion = $(this).find(".cat").val();
                                    break;
                                case 16:
                                    imprimio = $(this).text();
                                    imprimio = imprimio.replace(/^\s*|\s*$/g, "");
                                    break;
                                case 12:
                                    alert($(this).find(".chkSel input:checkbox").is(":visible"));
                                    $(this).find(".chkSel input:checkbox").attr("visible", true);                                    
                                    break;
                            }
                        })
                        //alert(imprimio);
                        // && imprimio == "No impresa"
                        if (estado == "Ingresante" && parseFloat(categorizacion) > 0) {
                            //alert("q");
                            //$(this).find(".chkSel input:checkbox").is("visible") = true;
                                                        
                        }
                        else {
                            //alert("wa");
                        }
                    })

                }
                else {
                    //alert("c");
                }
            });

        })
        .change();
    </script>
</form>
   
</body>
</html>

