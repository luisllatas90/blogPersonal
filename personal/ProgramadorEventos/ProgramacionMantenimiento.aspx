<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProgramacionMantenimiento.aspx.vb"
    Inherits="ProgramacionMantenimiento" %>

<!DOCTYPE html>
<html lang="es">
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Programación de comunicación</title>
    <link href="libs/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="libs/bootstrap-datepicker-1.8.0/css/bootstrap-datepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="libs/bootstrap-select-1.13.2/css/bootstrap-select.min.css" rel="stylesheet"
        type="text/css" />
    <link href="libs/multiple-select/multiple-select.css" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/smart-wizard/css/smart_wizard.css" rel="stylesheet" type="text/css">
    <link href="../assets/toastr-2.1.4-7/toastr.min.css" rel="stylesheet" type="text/css">
    <link href="css/programacion.css?5" rel="stylesheet" type="text/css" />

    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>

    <script src="libs/popper-1.14.4/js/popper.js" type="text/javascript"></script>

    <script src="libs/bootstrap-4.1/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="libs/bootstrap-datepicker-1.8.0/js/bootstrap-datepicker.min.js" type="text/javascript"></script>

    <script src="libs/bootstrap-datepicker-1.8.0/locales/bootstrap-datepicker.es.min.js"
        type="text/javascript"></script>

    <script src="libs/bootstrap-select-1.13.2/js/bootstrap-select.min.js" type="text/javascript"></script>

    <script src="libs/multiple-select/multiple-select.js" type="text/javascript"></script>

    <script src="libs/fontawesome-5.2/js/fontawesome.min.js" type="text/javascript"></script>

    <script src="libs/iframeresizer/iframeResizer.contentWindow.min.js" type="text/javascript"></script>
    <script src="libs/iframeresizer/iframeResizer.min.js" type="text/javascript"></script>
    <script src="../assets/smart-wizard/js/jquery.smartWizard.js" type="text/javascript"></script>
    <script src="../assets/toastr-2.1.4-7/toastr.min.js" type="text/javascript"></script>

    <script src="js/programacionMantenimiento.js?92" type="text/javascript"></script>

    <script type="text/javascript">
        function setValuesJS(hraIni, hraFin, frec, diaSem, diaMes, ord, sem, frecHra, horaMin) {
            $("#cboTipoMensaje").trigger("change");
            $("#cboTipoProgramacion").trigger("change");
            $("#cboTipoFrecuencia").trigger("change");
            
            //Frecuencia
            if ($("#cboTipoFrecuencia").val() == "D") {
                $("#spnFrecuencia").val(frec);
                $("#cboFrecuenciaTiempo").val(horaMin);
            } else if ($("#cboTipoFrecuencia").val() == "S") {
                var $select;

                $("#txtFrecuenciaDiaSemana").val(diaSem);
                $select = $("#cboFrecuenciaDiaSemana").multipleSelect({
                    placeholder: "Seleccione la semana",
                    position: 'top',
                    width: 200,
                    multiple: true,
                    multipleWidth: 80
                });

                $select.multipleSelect("setSelects", diaSem.split(',').map(Number));
                $select.multipleSelect("refresh");
            } else if ($("#cboTipoFrecuencia").val() == "M") {
                if (ord == null || ord == "0") {
                    //$("#rbtFrecuenciaDiaMes").prop("checked", true);
                    $("#rbtFrecuenciaDiaMes").trigger("click");
                    $("#spnFrecuenciaDiaMes").val(diaMes);
                    $("#spnFrecuenciaMes").val(frec);
                } else {
                    //$("#rbtOrdinal").prop("checked", true);
                    $("#rbtOrdinal").trigger("click");
                    $("#cboOrdinal").val(ord);
                    $("#cboDiaSemana").val(sem);
                    $("#spnMes").val(frec);
                }
            }

            //Frecuencia diaria
            if (hraFin == null || hraFin == "") {
                //$("#rbtFrecuenciaA1").prop("checked", true);
                $("#rbtFrecuenciaA1").trigger("click");
                $("#dtpHoraFrecuencia").val(hraIni);
            } else {
                //$("#rbtFrecuenciaA2").prop("checked", true);
                $("#rbtFrecuenciaA2").trigger("click");
                $("#spnFrecuenciaHora").val(frecHra);
                $("#dtpHoraIniDia").val(hraIni);
                $("#dtpHoraFinDia").val(hraFin);
            }
            
            //Duración
            if ($("#dtpFechaFin").val() !== "") { // || $("#dtpFechaFin").val() !== null
                $("#chkFechaFin").trigger("click"); // $("#chkFechaFin").prop("checked", true);
                $("#dtpFechaFin").removeAttr("disabled");
                $("#dtpHoraInicio").val("");
            } else {
                $("#chkFechaFin").prop("checked", false);
                $("#dtpFechaFin").attr("disabled", "true");
            } 

            if($('#cboTipoProgramacion').val() == 'U') {
                $("#dtpHoraInicio").val(hraIni);
                $("#dtpHoraFrecuencia").val('');
            }

            $("#txtDescripcion").focus();

            $('#spnNumeroDias').val($('#txtNumeroDias').val());
        }
    </script>

</head>
<body>
    <form id="frmProgramacion" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode=Release>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="panForm" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <input type="hidden" id="hddFiltros" runat="server">
            <div class="row">
                <div class="col-xs-12 col-sm-12">
                    <h6 class="page-header" id="pageHeader" runat="server">
                        <span runat="server" id="lblEvento"></span>
                    </h6>
                    <div id="smartwizard">
                        <ul>
                            <li id="liProgramacion" runat="server">
                                <a href="#divProgramacion" data-step="2">
                                    <small>PROGRAMACIÓN</small>
                                </a>
                            </li>
                            <li id="liSeleccionarInteresados" runat="server">
                                <a href="#divSeleccionarInteresados" data-step="1">
                                    <small>FILTRAR INTERESADOS</small>
                                </a>
                            </li>
                            <li id="liInteresados" runat="server">
                                <a href="#divInteresados" data-step="1">
                                    <small>INTERESADOS</small>
                                </a>
                            </li>
                        </ul>
                        <div> 
                            <div id="divProgramacion" class="tab-pane step-content" runat="server">
                                <div class="form-group row">
                                    <div class="col-xs-12 col-sm-8">
                                        <label for="txtDescripcion" class="form-control-sm">
                                            Descripción</label>
                                        <input type="text" name="txtDescripcion" id="txtDescripcion" class="form-control form-control-sm"
                                            style="text-transform: uppercase" runat="server" />
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="cboTipoMensaje" class="form-control-sm">
                                            Enviar por</label>
                                        <select id="cboTipoMensaje" class="form-control form-control-sm" onchange="elegirTipoMensaje(this);"
                                            runat="server">
                                            <option value="M" selected="selected">MAILING</option>
                                            <option value="S">SMS</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-xs-12 col-sm-8">
                                        <label id="lblID" for="txtID" class="form-control-sm">ID del Template</label>
                                        <input type="text" name="txtID" id="txtID" class="form-control form-control-sm" runat="server"
                                            required />
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="cboTipoProgramacion" class="form-control-sm">
                                            Tipo</label>
                                        <select id="cboTipoProgramacion" class="form-control form-control-sm" onchange="elegirTipoProgramacion(this);"
                                            runat="server">
                                            <option value="P">Periódica</option>
                                            <option value="U">Una vez</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-xs-12 col-sm-12">
                                        <label for="cboVariablesProgramacion" class="form-control-sm">Variables</label>
                                        <asp:ListBox ID="cboVariablesProgramacion" runat="server" AutoPostBack="true" SelectionMode="Multiple" 
                                            CssClass="form-control form-control-sm selectpicker">
                                            <asp:ListItem value="apellidoPaterno">Ape. Paterno</asp:ListItem>
                                            <asp:ListItem value="apellidoMaterno">Ape. Materno</asp:ListItem>
                                            <asp:ListItem value="nombres">Nombres</asp:ListItem>
                                        </asp:ListBox>
                                    </div>
                                </div>
                                <fieldset class="border p-1" id="divMsgPrueba" runat="server">
                                    <legend class="w-auto">Mensaje de prueba</legend>
                                    <div class="form-group row">
                                        <label id="lblDestinatarioPrueba" for="txtDestinatarioPrueba" class="col-sm-2 form-control-sm col-form-sm">Correo Electrónico</label>
                                        <div class="col-xs-12 col-sm-4">
                                            <input type="text" name="txtDestinatarioPrueba" id="txtDestinatarioPrueba" class="form-control form-control-sm"
                                                runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-3">
                                            <button id="btnMensajePrueba" runat="server" class="btn btn-sm btn-success">Enviar mensaje de prueba</button>
                                        </div>
                                    </div>
                                </fieldset>
                                <hr>
                                <fieldset class="border p-1" id="divDiasFechaRegistro" runat="server">
                                    <legend class="w-auto">Días con respecto a la fecha de registro</legend>
                                    <div class="form-group row">
                                        <label for="spnNumeroDias" class="col-sm-2 form-control-sm col-form-sm">Día / Fecha Registro</label>
                                        <div class="col-xs-12 col-sm-2">
                                            <input id="spnNumeroDias" class="form-control form-control-sm" type="number" min="1"
                                                max="100" step="1" value="1" />
                                            <asp:HiddenField ID="txtNumeroDias" runat="server" />
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset class="border p-1" id="divFrecuenciaGeneral" runat="server">
                                    <legend class="w-auto">Frecuencia</legend>
                                    <div class="form-group row">
                                        <div class="col-xs-12 col-sm-2">
                                            <label for="cboTipoFrecuencia" class="form-control-sm">
                                                Se ejecuta</label>
                                            <select id="cboTipoFrecuencia" class="form-control form-control-sm" runat="server"
                                                onchange="elegirTipoFrecuencia(this);">
                                                <option value="D">Diaria</option>
                                                <option value="S">Semanal</option>
                                                <option value="M">Mensual</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-12 col-sm-2" id="divFrecuenciaA">
                                            <label for="spnFrecuencia" class="form-control-sm">
                                                Repetir cada</label>
                                            <div class="row no-gutters" id="divFrecuencia">
                                                <div class="col-xs-12 col-sm-6">
                                                    <input id="spnFrecuencia" class="form-control form-control-sm" type="number" min="1"
                                                        max="100" step="1" value="1" />
                                                    <asp:HiddenField ID="txtFrecuencia" runat="server" />
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <label id="lblFrecuencia" class="form-control-sm" runat="server">
                                                        día(s)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-2" id="divFrecuenciaDiaSemana">
                                            <label for="cboFrecuenciaDiaSemana" class="form-control-sm">
                                                El
                                            </label>
                                            <select multiple="multiple" id="cboFrecuenciaDiaSemana" name="cboFrecuenciaDiaSemana"
                                                onchange="elegirFrecuenciaDiaSemana('S');">
                                                <option value="1">Domingo</option>
                                                <option value="2">Lunes</option>
                                                <option value="4">Martes</option>
                                                <option value="8">Miércoles</option>
                                                <option value="16">Jueves</option>
                                                <option value="32">Viernes</option>
                                                <option value="64">Sábado</option>
                                            </select>
                                            <asp:HiddenField ID="txtFrecuenciaDiaSemana" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group row" id="divFrecuenciaB">
                                        <div class="col-xs-12 col-sm-2" id="divFrecuenciaC">
                                            <input type="radio" runat="server" id="rbtFrecuenciaDiaMes" name="rbtFrecuenciaOrdinal"
                                                onclick="elegirFrecuenciaDiaMes(this);" checked />
                                            <label for="rbtFrecuenciaDiaMes" class="form-control-sm">
                                                El día</label>
                                            <input id="spnFrecuenciaDiaMes" class="form-control form-control-sm" type="number"
                                                min="1" max="31" step="1" value="1" />
                                            <asp:HiddenField ID="txtFrecuenciaDiaMes" runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-2" id="divFrecuenciaD">
                                            <label for="divFrecuenciaMes" class="form-control-sm">
                                                De cada</label>
                                            <div class="row no-gutters" id="divFrecuenciaMes">
                                                <div class="col-xs-12 col-sm-6">
                                                    <input id="spnFrecuenciaMes" class="form-control form-control-sm" type="number" min="1"
                                                        max="99" step="1" value="1" />
                                                    <asp:HiddenField ID="txtFrecuenciaMes" runat="server" />
                                                </div>
                                                <div class="col-xs-12 col-sm-6">
                                                    <label for="spnFrecuenciaMes" class="form-control-sm">
                                                        mes(es)</label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-8" id="divFrecuenciaE">
                                            <input type="radio" runat="server" id="rbtOrdinal" name="rbtFrecuenciaOrdinal" onclick="elegirFrecuenciaDiaMes(this);" />
                                            <label for="rbtOrdinal" class="form-control-sm">
                                                El
                                            </label>
                                            <div class="row no-gutters">
                                                <div class="col-xs-12 col-sm-3">
                                                    <select id="cboOrdinal" class="form-control form-control-sm" runat="server">
                                                        <option value="1" selected="selected">Primer</option>
                                                        <option value="2">Segundo</option>
                                                        <option value="4">Tercer</option>
                                                        <option value="8">Cuarto</option>
                                                        <option value="16">Último</option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-12 col-sm-3">
                                                    <select id="cboDiaSemana" class="form-control form-control-sm" runat="server">
                                                        <option value="1">Domingo</option>
                                                        <option value="2" selected="selected">Lunes</option>
                                                        <option value="3">Martes</option>
                                                        <option value="4">Miércoles</option>
                                                        <option value="5">Jueves</option>
                                                        <option value="6">Viernes</option>
                                                        <option value="7">Sábado</option>
                                                        <option value="8">Día</option>
                                                        <option value="9">Día de la semana</option>
                                                        <option value="10">Día fin de semana</option>
                                                    </select>
                                                </div>
                                                <div class="col-xs-12 col-sm-2">
                                                    <label class="form-control-sm">
                                                        de cada</label>
                                                </div>
                                                <div class="col-xs-12 col-sm-2">
                                                    <input id="spnMes" class="form-control form-control-sm" type="number" min="1" max="99"
                                                        step="1" value="1" />
                                                    <asp:HiddenField id="txtMes" runat="server" />
                                                </div>
                                                <div class="col-xs-12 col-sm-2">
                                                    <label class="form-control-sm">
                                                        mes(es)</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset class="border p-1" id="divFrecuenciaDiaria">
                                    <legend class="w-auto">Frecuencia diaria</legend>
                                    <div class="form-group row">
                                        <div class="col-xs-12 col-sm-2">
                                            <input type="radio" runat="server" id="rbtFrecuenciaA1" name="FrecuenciaDiaria" onclick="elegirFrecuenciaDiaria(this);"
                                                checked />
                                            <label for="rbtFrecuenciaA1" class="form-control-sm">
                                                Sucede a las</label>
                                            <input type='time' id='dtpHoraFrecuencia' name='dtpHoraFrecuencia' min='0:00' max='23:59'
                                                class='form-control form-control-sm' />
                                            <asp:HiddenField ID="txtHoraFrecuencia" runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-2">
                                            <input type="radio" runat="server" id="rbtFrecuenciaA2" name="FrecuenciaDiaria" onclick="elegirFrecuenciaDiaria(this);" />
                                            <label for="rbtFrecuenciaA2" class="form-control-sm">
                                                Cada</label>
                                            <input id="spnFrecuenciaHora" class="form-control form-control-sm" type="number"
                                                min="1" max="24" step="1" value="1" />
                                            <asp:HiddenField ID="txtFrecuenciaHora" runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-2">
                                            <label for="cboFrecuenciaTiempo" class="form-control-sm" style="color: white; visibility: hidden;"
                                                visible="false">
                                                .</label>
                                            <select id="cboFrecuenciaTiempo" class="form-control form-control-sm" runat="server">
                                                <option value="Hrs" selected="selected">Horas</option>
                                                <option value="Min">Minutos</option>
                                            </select>
                                        </div>
                                        <div class="col-xs-12 col-sm-2">
                                            <label for="dtpHoraIniDia" class="form-control-sm">
                                                Comienza</label>
                                            <input type='time' id='dtpHoraIniDia' name='dtpHoraIniDia' min='0:00' max='23:59'
                                                class='form-control form-control-sm' />
                                            <asp:HiddenField ID="txtHoraIniDia" runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-2">
                                            <label for="dtpHoraFinDia" class="form-control-sm">
                                                Finaliza</label>
                                            <input type='time' id='dtpHoraFinDia' name='dtpHoraFinDia' min='0:00' max='23:59'
                                                class='form-control form-control-sm' />
                                            <asp:HiddenField ID="txtHoraFinDia" runat="server" />
                                        </div>
                                    </div>
                                </fieldset>
                                <fieldset class="border p-1">
                                    <legend class="w-auto">Duración</legend>
                                    <div class="form-group row">
                                        <div class="col-xs-12 col-sm-3">
                                            <label id="lblFechaInicio" for="dtpFechaInicio" class="form-control-sm">
                                                Fecha de inicio</label>
                                            <input type="text" name="dtpFechaInicio" id="dtpFechaInicio" class="form-control form-control-sm"
                                                runat="server" required />
                                            <asp:HiddenField ID="txtFechaInicio" runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-3">
                                            <input type="checkbox" id="chkFechaFin" name="chkFechaFin" runat="server" onclick="elegirFechaFinalización(this);" />
                                            <label for="chkFechaFin" class="form-control-sm">
                                                Fecha de finalización</label>
                                            <input type="text" name="dtpFechaFin" id="dtpFechaFin" class="form-control form-control-sm"
                                                runat="server" />
                                            <asp:HiddenField ID="txtFechaFin" runat="server" />
                                        </div>
                                        <div class="col-xs-12 col-sm-2">
                                            <label for="dtpHoraInicio" class="form-control-sm">
                                                Hora</label>
                                            <input type='time' id='dtpHoraInicio' name='dtpHoraInicio' min='0:00' max='23:59'
                                                class='form-control form-control-sm' />
                                            <asp:HiddenField ID="txtHoraInicio" runat="server" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div> 
                            <div id="divSeleccionarInteresados" class="tab-pane step-content" runat="server">
                                <div class="tab-pane fade show" id="seleccionar-interesados" role="tabpanel" aria-labelledby="seleccionar-interesados-tab">
                                    <asp:UpdatePanel ID="udpSeleccinarInteresados" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <iframe src="" id="ifrmSeleccionarInteresados" runat="server" frameborder="0" width="100%" scrolling="no"></iframe>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>    
                            </div>
                            <div id="divInteresados" class="tab-pane step-content" runat="server">
                                <div class="tab-pane fade show" id="interesados" role="tabpanel" aria-labelledby="interesados-tab">
                                    <asp:UpdatePanel ID="udpInteresados" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:GridView ID="grwInteresados" runat="server" AutoGenerateColumns="false" CssClass="table table-sm" GridLines="None" DataKeyNames="codigo_int">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Nro" />
                                                    <asp:BoundField DataField="numerodoc_int" HeaderText="Doc. Identidad" />
                                                    <asp:BoundField DataField="apepaterno_int" HeaderText="Ape. Paterno" />
                                                    <asp:BoundField DataField="apematerno_int" HeaderText="Ape. Materno" />
                                                    <asp:BoundField DataField="nombres_int" HeaderText="Nombres" />
                                                    <asp:BoundField DataField="numero_tei" HeaderText="Celular" />
                                                    <asp:BoundField DataField="descripcion_emi" HeaderText="Email" />
                                                </Columns>
                                                <HeaderStyle CssClass="thead-dark alt" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div> 
                            </div>      
                        </div>
                    </div>
                    <div class="tab-content" id="myTabContent">
                        
                        <div class="tab-pane fade" id="programacion" role="tabpanel" aria-labelledby="programacion-tab">
                            
                        </div>
                    </div>
                </div>
            </div>
            <div id="botonesAccion" runat="server" class="collapse">
                <div class="form-group row">
                    <div class="col-sm-10">
                        <button id="btnCancelar" runat="server" class="btn btn-sm btn-default">
                            Cancelar</button>
                        <asp:Button ID="btnRegistrar" runat="server" UseSubmitBehavior="false" CssClass="btn btn-sm btn-primary"
                            Text="Registrar" OnClientClick="guardar(this);" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="mdlMensajesServidor" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <asp:UpdatePanel ID="udpMensajeServidorParametros" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div id="divMdlMenServParametros" runat="server" data-mostrar="false"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-header">
                    <asp:UpdatePanel ID="udpMensajeServidorHeader" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Respuesta del
                                Servidor</span>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpMensajeServidorBody" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divRespuestaPostback" runat="server" class="alert" data-ispostback="false"
                                data-rpta="" data-msg="" data-control=""></div>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="d-none" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:UpdatePanel ID="udpMensajeServidorFooter" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <button type="button" id="btnMensajeCerrar" runat="server" class="btn btn-default"
                                data-dismiss="modal">Cerrar</button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="txtCodigo" runat="server" />
    <asp:HiddenField ID="txtCategoria" runat="server" />
    </form>

    <script type="text/javascript">
        var controlId = '';

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch(controlId) {
                case 'btnMensajePrueba':
                    AtenuarBoton(controlId, false);
                    break;
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                AtenuarBoton(controlId, true);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            /*for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;
                switch (udpPanelId) {

                }
            }*/
        });

        Sys.Application.add_load(function() {
            switch(controlId){
                case 'btnMensajePrueba':
                    AtenuarBoton(controlId, true);
                    break;

                case 'btnRegistrar':
                    SubmitPostBack();
                    break;
            }

            RevisarMensajePostback();
        });
    </script>

</body>
</html>
