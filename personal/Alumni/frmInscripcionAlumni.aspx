<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInscripcionAlumni.aspx.vb" Inherits="Alumni_frmInscripcionAlumni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%-- Compatibilidas --%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
        
    <%--<link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="private/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/jquery.js" type="text/javascript"></script>
    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <script src="private/jquery.loadmask.js" type="text/javascript"></script>

--%>

    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="js/popper.js" type="text/javascript"></script>
    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <%--<script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>--%>
    
    <%-- Estilo para fechas --%>
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>   
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" /> 
    
    <script type="text/javascript">
        $(document).ready(function() {

            // activar el submit
            $('body').on('click', '#chkTerminosCondiciones', function(e) {
                activacionBotonesSubmit();
                //alert($("#ddltipoparticipante").val());

            });

            // activar el modal
            $('body').on('click', '#lnkLeerTerminosCondiciones', function(e) {
                MostrarTerminosCondiciones();
                e.preventDefault();
            });

            // para el boton del datePicker
            $('#btnFechaNac').click(function() {
                $("#txtfechaNacimiento").datepicker('show');
            });
             

        })// fin del jQuery

    //*** Validaciones de los controles ***///
    function validar() {
        
        $("#mensaje").removeAttr("class");
        $("#mensaje").html("");

        validamail = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
        var valChkLab = $('#chkLabora').is(':checked')
        
        if ($("#ddltipoparticipante").val() == "0") {
            $("#mensaje").attr("class", "alert alert-danger")            
            $("#mensaje").html("<p>Seleccione el Tipo de Participante.</p>")
            $("#ddltipoparticipante").focus();
            return false;
        }
        if ($("#ddltipodoc").val() == "0") {
            $("#mensaje").attr("class", "alert alert-danger")            
            $("#mensaje").html("<p>Seleccione Tipo de Documento.</p>")
            return false;
        }
        if ($("#txtnrodoc").val() == "") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Ingrese Número de Documento.</p>")
            $("#txtnrodoc").focus();
            return false;
        }
        var valida_enteros = /^\d*$/;
        if ($("#ddltipodoc").val() == "DNI") {
            if ($("#txtnrodoc").val().length != 8 || !valida_enteros.test($("#txtnrodoc").val())) {
                $("#mensaje").attr("class", "alert alert-danger");
                $("#mensaje").html("<p>Número de DNI debe Tener 8 Digitos (Números Enteros).</p>")
                $("#txtnrodoc").focus();
                return false;
            }
        }
        if ($("#txttelefono").val().length != 9 || !valida_enteros.test($("#txttelefono").val())) {
            $("#mensaje").attr("class", "alert alert-danger");
            $("#mensaje").html("<p>Número de celular debe Tener 9 Digitos (Números Enteros).</p>")
            $("#txttelefono").focus();
            return false;
        }
        
        if ($("#ddltipodoc").val() == "RUC") {
            if ($("#txtnrodoc").val().length != 11 || !valida_enteros.test($("#txtnrodoc").val())) {
                $("#mensaje").attr("class", "alert alert-danger");
                $("#mensaje").html("<p>Número de RUC debe Tener 11 Digitos (Números Enteros).</p>")
                $("#txtnrodoc").focus();
                return false;
            }
        }
        var valida_letras = /^[a-zA-ZñÑ\s\W]/;
        if ($("#txtapepat").val() == "") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Ingrese Apellido Paterno.</p>")
            $("#txtapepat").focus();
            return false;
        }
        if (!valida_letras.test($("#txtapepat").val())) {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>No se Aceptan Números en el Apellido Paterno.</p>")
            $("#txtapepat").focus();
            return false;
        }
        if ($("#txtapemat").val() == "") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Ingrese Apellido Materno.</p>")
            $("#txtapemat").focus();
            return false;
        }
        if (!valida_letras.test($("#txtapemat").val())) {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>No se Aceptan Números en el Apellido Materno.</p>")
            $("#txtapemat").focus();
            return false;
        }
        if ($("#txtnombres").val() == "") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Ingrese el Nombre.</p>")
            $("#txtnombres").focus();
            return false;
        }
        if (!valida_letras.test($("#txtnombres").val())) {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>No se Aceptan Números en el Nombre.</p>")
            $("#txtnombres").focus();
            return false;
        }
        if ($("#txtfechaNacimiento").val() == "") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Seleccione Fecha de Nacimiento.</p>")
            $("#txtfechaNacimiento").focus();
            return false;
        }
        if ($("#ddlsexo").val() == "0") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Seleccione Sexo.</p>")
            $("#ddlsexo").focus();
            return false;
        }
        if ($("#txtemail").val() == "") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Ingrese Correo Electrónico.</p>")
            $("#txtemail").focus();
            return false;
        }        
        //Se muestra un texto a modo de ejemplo, luego va a ser un icono
        if (!validamail.test($("#txtemail").val())) {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Ingrese correctamente Correo Electrónico.</p>")
            $("#txtemail").focus();
            return false;
        }
        if ($("#ddlUniversidad").val() == "-1") {
            $("#mensaje").attr("class", "alert alert-danger")
            $("#mensaje").html("<p>Seleccione el Universidad.</p>")
            $("#ddlUniversidad").focus();
            return false;
        }
        // validaciones de la empresa
        if (valChkLab) {
            if ($("#txtEmpAlum").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese Nombre de la empresa.</p>")
                $("#txtEmpAlum").focus();
                return false;
            }
            if ($("#txtCargAlum").val() == "") {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese el cargo o puesto que realiza.</p>")
                $("#txtCargAlum").focus();
                return false;
            }
//            if ($("#txtDirAlum").val() == "") {
//                $("#mensaje").attr("class", "alert alert-danger")
//                $("#mensaje").html("<p>Ingrese dirección de la empresa.</p>")
//                $("#txtDirAlum").focus();
//                return false;
//            }
//            if ($("#txtTelEmp").val() == "") {
//                $("#mensaje").attr("class", "alert alert-danger")
//                $("#mensaje").html("<p>Ingrese teléfono de la empresa.</p>")
//                $("#txtTelEmp").focus();
//                return false;
//            }
            if (!validamail.test($("#txtEmailEmp").val())) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Ingrese correctamente Correo Electrónico de la empresa.</p>")
                $("#txtEmailEmp").focus();
                return false;
            }
            // validar el radio button
            var rb = document.getElementById("<%=rbModLabora.ClientID%>");
            var radio = rb.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }
            if (!isChecked) {
                $("#mensaje").attr("class", "alert alert-danger")
                $("#mensaje").html("<p>Seleccione la oferta laboral en la que ingreso a laborar </p>")
                //$("#txtEmailEmp").focus();
                return false;
            }
            /// fin de validacion del radio button               
        } // fin de validaciones de la empresa
        
        
//        var captcha = grecaptcha.getResponse();
//        if (captcha == "" || captcha == undefined) {
//            $("#mensaje").attr("class", "alert alert-danger")
//            $("#mensaje").html("<p>Resolver Captcha.</p>")
//            return false;
//        }
        if (confirm("¿Esta Seguro que desea Registrar sus Datos?") == true) {
            MascaraEspera("1");
            return true;
        }
        return false;
    }
    //*** fin de validaciones**///
    
    //*** activar el submit
    function activacionBotonesSubmit() {
        var validacion = $('#chkTerminosCondiciones').is(':checked') // && flagCaptchaValidated; //DESARROLLO
        $('#btnInscribir').prop('disabled', !validacion);
    }
    /// fin de activar el submit
    // Activar el modal de terminos y condiciones
    function MostrarTerminosCondiciones() {
        var $mdlGenerico = $('#mdlGenerico');
        $mdlGenerico.find('#mensajeGenerico').html($('#terminosCondiciones').html());
        $mdlGenerico.modal('show');
    }    
        
        
    </script>
    
    <style type="text/css">
         .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .panel-heading
        {  
        	 padding:3px;
        }
        .panel-body
        {
        	margin-top: 0px;
        	padding-top: 0px; 
        	
        	}
        .form-control
        {
            border-color: #d9d9d9;
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
        }
    </style>
        
</head>
<body>
     <div class="container-fluid">
     <div class="row">
        <div class="col-md-2">
        </div>
        <div class="col-md-8">
             <div class="panel panel-default">
                <div class="panel panel-heading" style="background-color:#e33439; color:White;" >
                    <div class="row">
                        <div class="col-md-12">
                            <h4>INSCRIPCION AL EVENTO: <span> <label id="divEvento" runat="server"></label>   </span></h4>                                                               
                        </div>
                    </div>                
                </div>
                <div class="panel panel-body">
                <div id="mensaje" runat="server">
                </div>
                <div id="shenlong">
                     <form id="form1" runat="server">          
                        <div class="row">
                            <div>                        
                                <asp:HiddenField ID="hdtoken" runat="server" />
                                <asp:HiddenField ID="hdevento" runat="server" />
                                <asp:HiddenField ID="hdmonto" runat="server" />
                                <asp:HiddenField ID="hdfechavenc" runat="server" />
                            </div>
                        </div>
                        <%--Tipo de participante--%>
                        <div class="row">                    
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="ddltipoparticipante">
                                    Tipo Inscripción:</label>
                                <div class="col-sm-4">
                                    <select runat="server" name="ddltipoparticipante" id="ddltipoparticipante" class="form-control" onserverchange="ddltipoparticipante_ServerChange" onchange="javascript:form1.submit();">
                                    </select>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:label class="col-sm-2 control-label" for="ddlUniversidad" ID="lblUniversidad" runat="server" style="font-weight:bold;">
                                    Universidad:</asp:label>
                                 <div class="col-sm-6" id="divSelec" runat="server">
                                    <%--<asp:DropDownList runat="server" name="ddlUniversidad" ID="ddlUniversidad" class="form-control">
                                    </asp:DropDownList>--%>
                                    <select runat="server" name="ddlUniversidad" id="ddlUniversidad" class="form-control">
                                    </select>
                                    
                                </div>
                                <div class="col-sm-6" id="divText" runat="server" visible="false">
                                    <asp:TextBox runat="server" name="txtUniversidad" ID="txtUniversidad" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>                
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="ddltipodoc">Tipo Documento:</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" name="ddltipodoc" ID="ddltipodoc" class="form-control">
                                        <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                                        <asp:ListItem Value="DNI">DNI</asp:ListItem>
                                        <asp:ListItem Value="RUC">RUC</asp:ListItem>
                                        <asp:ListItem Value="PASAPORTE">PASAPORTE</asp:ListItem>
                                        <asp:ListItem Value="CARNÉ DE EXTRANJERÍA">CARNÉ DE EXTRANJERÍA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <label class="col-sm-2 control-label" for="txttitulo">Nro. Documento:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" name="txtnrodoc" ID="txtnrodoc" class="form-control"></asp:TextBox>
                                </div>             
                            </div>                    
                        </div>                
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtapemat">
                                    Apellido Paterno:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" name="txtapepat" ID="txtapepat" class="form-control"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 control-label" for="txttitulo">
                                    Apellido Materno:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" name="txtapemat" ID="txtapemat" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>                
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtnombres">
                                    Nombres:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" runat="server" name="txtnombres" ID="txtnombres" class="form-control"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 control-label" for="txtfechaNacimiento">
                                    Fech. Nac.:</label>
                                <div class="col-sm-4">
                                    <%--<asp:TextBox runat="server" name="txtfechaNacimiento" ID="txtfechaNacimiento" class="form-control"></asp:TextBox>--%>
                                    <div class="input-group date" id="FechaNacimiento" runat="server">
                                        <input name="txtfechaNacimiento" class="form-control" id="txtfechaNacimiento" style="text-align: right;"
                                            runat="server" type="text" placeholder="dd/mm/aaaa" data-provide="datepicker"/>
                                        <span class="input-group-addon sm" id="btnFechaNac"><i class="fa fa-calendar" id="DivFechaNacimiento">
                                        </i></span>
                                    </div>
                                </div>
                            </div>
                        </div>               
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="ddlsexo">
                                    Sexo:</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList runat="server" name="ddlsexo" ID="ddlsexo" class="form-control">
                                        <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                                        <asp:ListItem Value="M">MASCULINO</asp:ListItem>
                                        <asp:ListItem Value="F">FEMENINO</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                  <label class="col-sm-2 control-label" for="txttelefono">
                                    Celular:</label>
                                <div class="col-sm-4">
                                    <asp:TextBox runat="server" name="txttelefono" ID="txttelefono" class="form-control" onkeypress="return validarnumeros(event);"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label" for="txtemail">
                                    Email:</label>
                                <div class="col-sm-6">
                                    <asp:TextBox runat="server" name="txtemail" ID="txtemail" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>                      
                        <div class="row" id="rowLabora" runat="server" >
                             <div class="panel panel-default">
                                <div class="panel panel-heading" style="padding-top:0px; padding-bottom:0px">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h5>Información Laboral</h5>
                                        </div>                                
                                    </div>                            
                                </div>                                
                                <div class="panel panel-body">
                                      <div class="row">
                                        <div class="form-group">
                                             <label class="col-sm-3 control-label" for="txtemail">
                                                Labora Actualmente</label>
                                                <div id="divCheckLabora" class="col-sm-6" runat="server">
                                                    <asp:CheckBox ID="chkLabora" runat="server" AutoPostBack="true" />
                                                </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="lblEmpresa">
                                                Empresa:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtEmpAlum" ID="txtEmpAlum" class="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-2 control-label" for="txttitulo">
                                                Cargo:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtCargAlum" ID="txtCargAlum" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="lblDireccion">
                                                Dirección:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtDirAlum" ID="txtDirAlum" class="form-control"></asp:TextBox>
                                            </div>
                                            <label class="col-sm-2 control-label" for="lblTelefono">
                                                Teléfono:</label>
                                            <div class="col-sm-4">
                                                <asp:TextBox runat="server" name="txtTelEmp" ID="txtTelEmp" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label" for="lblEmailEmp">
                                                Email:</label>
                                            <div class="col-sm-6">
                                                <asp:TextBox runat="server" name="txtEmailEmp" ID="txtEmailEmp" class="form-control"></asp:TextBox>
                                            </div>                                            
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-3 control-label" for="lblModLab">
                                                Ingresé a laborar por: </label>
                                        <asp:RadioButtonList ID="rbModLabora" runat="server" 
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1">&nbsp;Oferta Laboral Alumni&nbsp;&nbsp;</asp:ListItem> 
                                            <asp:ListItem Value="0">&nbsp;&nbsp;Oferta Laboral Externa&nbsp;&nbsp;</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                             </div>
                            
                        </div>
                        <div class="modal fade" id="mdlGenerico" tabindex="-1" role="dialog" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-body">
                                        <div id="mensajeGenerico"></div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnModalAceptar" class="btn btn-sm btn-danger" data-dismiss="modal">Aceptar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="terminosCondiciones" style="display:none">
                            <div class="terminos-condiciones">
                                <h1>Términos y Condiciones</h1>
                                <p>
                                    De conformidad con la Ley N° 29733, Ley de Protección de Datos Personales, autorizo a la USAT a utilizar los datos personales proporcionados o que proporcione a futuro, para la gestión administrativa y comercial que realice. 
                                </p>
                                <p>
                                    Asimismo, de conformidad con las Leyes N° 28493 y N° 29571 brindo mi consentimiento para que la USAT me envíe información, 
                                    publicidad, encuestas y estadísticas de sus servicios educativos; teniendo pleno conocimiento que puedo acceder, rectificar, 
                                    oponerme y cancelar mis datos personales, así como revocar mi consentimiento enviando un correo a informacion@usat.edu.pe. 
                                </p>
                            </div>
                        </div>
                        
                       <div class="form-group row">
                            <center>
                                <div class="g-recaptcha" data-sitekey="6LemTGAUAAAAAC4qhRnTPNDqY1XyNS35KSG6WxJo">
                                </div>
                                <br />
                                <div class="contenedor-terminos-condiciones">
                                    <div class="custom-control custom-checkbox">    
                                        <input type="checkbox" id="chkTerminosCondiciones" runat="server" class="custom-control-input ignore" onclick="return chkTerminosCondiciones_onclick()">
                                        <label for="chkTerminosCondiciones" class="custom-control-label">
                                            He leído y acepto los términos y condiciones: &nbsp;&nbsp;&nbsp; <a href="#" id="lnkLeerTerminosCondiciones" class="btn btn-xs btn-info" >Leer aquí</a>
                                        </label>
                                    </div>
                                </div>
                                <br />
                                <asp:Button runat="server" Text="Inscribir" name="btnInscribir" ID="btnInscribir" disabled
                                    OnClientClick="return validar();" class="btn btn-primary" />
                            </center>
                            
                        </div>
            </form>
                </div>                
                </div> 
            </div>       
        </div>
        <div class="col-md-2">
        </div>
     </div>      
    </div>
</body>
</html>
