<%@ Page Language="VB" ValidateRequest="false" AutoEventWireup="false" CodeFile="frmportadawebcurso.aspx.vb" Inherits="frmportadawebcurso" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Registro de páginas web</title>
     <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <link rel="STYLESHEET"  href="../../private/estilo.css"/>
    <script type="text/javascript">
        function OcultarTabla()
        {
            if (form1.TxtComentario.value!=""){
                document.all.form1.style.display="none"
                document.all.tblmensaje.style.display=""
            }
        }
        
        /*HTML a aplicar*/
        function Negrita() {
        var tr=document.getElementById("TxtComentario").document.selection.createRange()
        tr.text = '<b>' + tr.text + '</b>'
        tr.select()
        }
        
        function FormatoTexto(v) {
            var str = document.selection.createRange().text;
            document.getElementById("TxtComentario").focus();
            var sel = document.selection.createRange();
            if (str.length>0){
                sel.text = "<" + v + ">" + str + "</" + v + ">";
                sel.select()//return;
            }
        }
        
        function FormatoAlineacion(v) {
            var str = document.selection.createRange().text;
            document.getElementById("TxtComentario").focus();
            var sel = document.selection.createRange();
            if (str.length>0){
                sel.text = "<p style='text-align=" + v + "'>" + str + "</p>";
                sel.select()//return;
            }
        }
        
        function Hipervinculo() {
            var str = document.selection.createRange().text;
            document.getElementById("TxtComentario").focus();
            if (str.length>0){
                var url = prompt("Ingresar la Dirección Web o URL:","http://");
                if (url != null) {
                    var sel = document.selection.createRange();
                    sel.text = "<a target='_blank' href=\"" + url + "\">" + str + "</a>";
                }
                sel.select()//return;
            }
        }
        
        function CambiarEncabezado(obj){
            if (obj.value!="h0"){
                var str = document.selection.createRange().text;
                document.getElementById("TxtComentario").focus();
                if (str.length>0){
                    var sel = document.selection.createRange();
                    sel.text = "<" + obj.value + ">" + sel.text + "</" + obj.value + ">"
                    sel.select()
                }
                else{
                    obj.value="h0"//return;
                }
            }
        }
        
       function IncluirImagen() {
            var str = document.selection.createRange().text;
            document.getElementById("TxtComentario").focus();
            var url = prompt("Pegar aquí la Dirección Web de la imágen externa:","http://");
            if (url != null) {
                var sel = document.selection.createRange();
                sel.text = "<img alt='Fuente: " + url + "' src='" + url + "' />";
		sel.select()//return;
            }
        }
     </script>
</head>
<body style="margin-top: 5px;background-color: #F2F2F2">
<form id="form1" runat="server">
<asp:HiddenField ID="hdidcursovirtual" runat="server" />
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
<table style="width: 100%; height: 95%;" cellpadding="3" id="tblDatos">
                       <tr>
                            <td style="width: 100%; height: 5%;">
                            <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar3" 
    OnClientClick="OcultarTabla()" Text="Guardar" />

                            
                            <img src="../../images/negrita.gif" alt="Negrita" onClick="FormatoTexto('b')" 
                                    align="absmiddle" /><img src="../../images/cursiva.gif" alt="Cursiva" 
                                    onClick="FormatoTexto('i')" align="absmiddle" /><img 
                                    src="../../images/subrayado.gif" alt="Subrayado" onClick="FormatoTexto('u')" 
                                    align="absmiddle" />&nbsp; | <img src="../../images/izquierda.gif" 
                                    onClick="FormatoAlineacion('left')" align="middle" alt="Alinear a la izquierda" /><img 
                                    src="../../images/centrado.gif" onClick="FormatoAlineacion('center')"
                                    align="middle" alt="Alinear al centro" /><img src="../../images/derecha.gif" 
                                    onClick="FormatoAlineacion('right')" align="middle" alt="Alinear a la derecha" />|
                                <select id="cboh" name="cboh" onchange="CambiarEncabezado(this)">
                                    <option value="h0">--Aplicar Tamaño predeterminado--</option>
                                    <option value="h1">Encabezado 1</option>
                                    <option value="h2">Encabezado 2</option>
                                    <option value="h3">Encabezado 3</option>
                                    <option value="h4">Encabezado 4</option>
                                    <option value="h5">Encabezado 5</option>
                                    <option value="h6">Encabezado 6</option>
                                </select>
                                <img src="../../images/vinculo.gif" 
                                    onClick="Hipervinculo()" align="middle" alt="Añadir hipervínculo" />&nbsp;
                                <img src="../../images/imagen.gif" 
                                    onClick="IncluirImagen()" align="middle" alt="Añadir imágen externa" />&nbsp;   
                                |  <span id="lblcontador">Máximo 8000 caracteres</span>
                                |
                                <img alt="" src="../../images/ayuda.gif" /><a target="_blank" href="../../ayudas/crearpaginaweb.html" 
                                    style="color: #0000FF; text-decoration: underline">Ayuda</a></b>&nbsp;
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtComentario"
                                    
                                    ErrorMessage="Escriba el contenido o copie el código HTML de la página web">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                       <tr>
                            <td style="width: 100%; height: 90%;">
                                <asp:TextBox ID="TxtComentario" runat="server" 
                                    Height="100%" TextMode="MultiLine"
                                    Width="100%" Font-Size="9pt" MaxLength="1000" Rows="5000" 
                                    BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 100%; height: 5%">
<asp:Label ID="LblMensaje" runat="server" Font-Size="12pt" ForeColor="Red" Font-Bold="True"></asp:Label>
			    </td>
                        </tr>
</table>
</form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    ProcesandoPor favor espere un momento...doPor favor espere un momento...<br/>
	    <img src="../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
