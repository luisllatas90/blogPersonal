<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCategorizarAlumno.aspx.vb" Inherits="administrativo_FrmCategorizarAlumno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Postulantes</title>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="jspostulantes.js" type="text/javascript"></script>        
	<script type="text/javascript" language="javascript">
	    /*
	    $(document).ready(function() {	        
	        checkuncheckall();
	        $("#cmdDescargar").hide();
	        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(checkuncheckall);
	    });	
	    */

	    function exportar() {

	        if ($("#ddlProceso").val() == "-1")
	            var proceso = "%";
	        else
	            var proceso = $("#ddlProceso").val();

	        if ($("#ddlCeco").val() == "-1")
	            var ceco = 0;
	        else
	            var ceco = $("#ddlCeco").val();

	        if ($("#ddlModalidad").val() == "-1")
	            var modalidad = 0;
	        else
	            var modalidad = $("#ddlModalidad").val();

	        if ($("#txtDNI").val() == undefined)
	            var dni = "%"
	        else
	            var dni = $("#txtDNI").val();

	        if ($("#txtCodigoUni").val() == undefined)
	            var coduni = "%"
	        else
	            var coduni = $("#txtCodigoUni").val();

	        if ($("#txtNombres").val() == undefined)
	            var nombres = "%"
	        else
	            var nombres = $("#txtNombres").val();

	        //alert("frmExportaPostulantes.aspx?pro=" + proceso + "&ceco=" + ceco + "&min=" + modalidad + "&dni=" + dni + "&coduni=" + coduni + "&nombres=" + nombres + "&estpos=" + $("#ddlEstPostulacion").val() + "&mod=" + getParameter("mod") + "&alu=0&categor=%&impre=%");
	        window.open("frmExportaPostulantes.aspx?pro=" + proceso + "&ceco=" + ceco + "&min=" + modalidad + "&dni=" + dni + "&coduni=" + coduni + "&nombres=" + nombres + "&estpos=" + $("#ddlEstPostulacion").val() + "&mod=" + getParameter("mod") + "&alu=0&categor=%&impre=%", "", "toolbar=no, location=no, directories=no, status=no, menubar=no, resizable=yes, width=800, height=600, top=50");
	    }
   	        	    	    		    	    
	</script>
	<style>
	    body
	    {
	        font-family: Arial;
	        font-size:small;
	        }
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="width:15%">Proceso de Admisi&oacute;n</td>
                <td style="width:15%">
                    <asp:DropDownList ID="cboCiclo" runat="server" Width="95%">
                    </asp:DropDownList>
                </td>
                <td style="width:15%">C. de Costos</td>
                <td style="width:25%">
                    <asp:DropDownList ID="cboCentroCosto" runat="server" Width="95%">
                    </asp:DropDownList>
                </td>
                <td style="width:15%">Mod. de Ingreso</td>
                <td style="width:15%">
                    <asp:DropDownList ID="cboModalidad" runat="server" Width="95%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Buscar Por</td>
                <td>
                    <asp:DropDownList ID="cboBuscarPor" runat="server" Width="95%">
                        <asp:ListItem Value="D">DNI</asp:ListItem>
                        <asp:ListItem Value="N">Nombres</asp:ListItem>
                        <asp:ListItem Value="CU">Código universitario</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan = "2">
                    <asp:TextBox ID="txtBusqueda" runat="server" Width="96%"></asp:TextBox>
                </td>
                <td>Estado Postulaci&oacute;n</td>
                <td>
                    <asp:DropDownList ID="cboEstadoPostula" runat="server" Width="95%">
                        <asp:ListItem Selected="True" Value="%">&gt;&gt; Seleccione&lt;&lt;</asp:ListItem>
                        <asp:ListItem Value="P">Postulante</asp:ListItem>
                        <asp:ListItem Value="I">Ingresante</asp:ListItem>
                        <asp:ListItem Value="R">Retirado</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Tiene Categorizaci&oacute;n</td>
                <td>
                    <asp:DropDownList ID="cboCategoriza" runat="server">
                        <asp:ListItem Value="1">Si</asp:ListItem>
                        <asp:ListItem Value="0">No</asp:ListItem>
                        <asp:ListItem Selected="True" Value="%">Todos</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>Carta Impresa</td>
                <td>
                    <asp:DropDownList ID="cboImpresion" runat="server" Width="95%">
                        <asp:ListItem Value="1">Impresa</asp:ListItem>
                        <asp:ListItem Value="0">No impresa</asp:ListItem>
                        <asp:ListItem Selected="True" Value="%">Todos</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td><asp:Button ID="btnBuscar" runat="server" Text="Buscar" /></td>
            </tr>
            <tr>
                <td colspan="6">&nbsp;</td>
            </tr>
            <tr>
                <td>Acci&oacute;n:</td>
                <td colspan="5">
                    <asp:DropDownList ID="cboAccion" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnProcesar" runat="server" Text="Procesar" />
                    &nbsp;
                    <input name="cmdDescargar" id="cmdDescargar" type="button" value="Imprimir" 
                        onClick="DescargarCartasCategorizacion(); " class="BU_boton_imprimir" 
                        style="height:26px; width:70px"  />&nbsp;   
                    <input id="btnExportar2" type="button" value="Exportar" onclick="exportar()" runat="server"/>   </td>                
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
        <br />
    </div>
    <div>
        <asp:GridView ID="grwListaPersonas" runat="server" Width="100%" CellPadding="3" 
            AutoGenerateColumns="False" DataKeyNames="codigo_Alu,codigo_pso,EstadoPostulacion,imprimiocartacat_Dal,Categorizacion,otroalu,categorizado_Dal" 
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
                        <asp:TextBox ID="txtCategorizacion" CssClass="cat" runat="server" Style="position: static; width:40px" Text='<%# Bind("Categorizacion","{0:n}") %>'></asp:TextBox>
                    </ItemTemplate>
            </asp:TemplateField>             
                                        
            <asp:BoundField DataField="imprimiocartacat_Dal" HeaderText="Imprimió Carta" />                                 
            <asp:BoundField DataField="categorizado_Dal" HeaderText="Categorizado" />
            <asp:BoundField DataField="notaIngreso_Dal" HeaderText="notaIngreso_Dal" Visible="false"/>
            <asp:BoundField DataField="observacion_Dal" HeaderText="observacion_Dal" Visible="false"/>
            
        </Columns>
        <HeaderStyle BackColor="#e33439" ForeColor="White" />
        </asp:GridView>  
        
    </div>
    <asp:HiddenField ID="HdUsuario" runat="server" />
    <asp:HiddenField ID="HdModulo" runat="server" />
    </form>
</body>
</html>
