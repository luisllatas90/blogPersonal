<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaTramite.aspx.vb"
    Inherits="administrativo_tramite_FrmListaTramite" EnableEventValidation="false" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Lista de trámites</title>
    
<%--    <link href="../../../private/estilo.css?z=x" rel="stylesheet" type="text/css" />--%>
  <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <%--<script src="../../assets/js/jquery.min.1.7.2.js" type="text/javascript"></script>--%>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
     
    <link href="private/bootstrap-datepicker3.standalone.css" rel="stylesheet" type="text/css" />
    <script src="private/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
  
    <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <link href="private/timeline.css" rel="stylesheet" type="text/css" />

    <script src="private/jquery.loadmask.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
    
    <script type="text/javascript">
        $(document).ready(function() {

            $('#_tfFlujoAct').hide();
            $('#_tfFlujoUlt').hide();
            $('#_tieneEmailAprobacion').hide();
            $('#_tieneEmailRechazo').hide();
            $('#_tieneMsjTextoRechazo').hide();
            $('#_tieneMsjTextoAprobacion').hide();
            
            $('#_nFlujoUlt').hide();
            $('#_nFlujoAct').hide();

            $('#_FlujoCheck').hide();

            $('#_tieneEmailPlanEstudio').hide();

            $('#_tieneEmailPrecioCredito').hide();
            $('#_tieneEmailGyT').hide();

            $('#_tEmail_send').hide();
            $('#_tEmail_config').hide();
            $('#_tEmail_cabecera').hide();
            $('#_tEmail_cuerpo').hide();
            $('#_tEmail_pie').hide();

            $("#fechaTramite").datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                todayHighlight: true,
                clearBtn: true,
                orientation: 'bottom',
                pickTime: false
            });
            $("#rblEstado").change(fnEnvioEmail);

            $("#btnGuardaFecha").click(function() {
                var ope = $('#_FechaEmail').val();
                // alert(ope);

                if (ope == 'F') {
                    var texto = $("#txtFecha").val();
                    if (texto == "") {
                        alert("Debe fecha de entrega");
                        return false;
                    }
                } else if (ope == 'E') {

                    var texto = $("#txtObservacionAlumno").val();
                    if (texto == "") {
                        alert("Debe ingresar una observación");
                        return false;
                    }
                }
                MascaraEspera('1');
            });

            $("#btnGuardar").click(function() {
                MascaraEsperaModal('1');
            });

            var oTable = $('#gvDatos').DataTable({
                //"sPaginationType": "full_numbers",
                "bPaginate": true,
                "bFilter": false,
                "bLengthChange": false,
                //   "aLengthMenu": [[100, 200, -1], [100, 200, "All"]],
                //  "iDisplayLength": 100,
                "bSort": false,
                "bInfo": true,
                "bAutoWidth": true
            });

            fnMensajeReq();

        });

        /*function fnResetOpcion() {

            $("table[id$=rblEstado] input:radio:checked").removeAttr("checked");
        }*/

        function fnMensajeReq() {
            var ecr= $('#_tEmail_send').val();
            if (ecr == 1) {
                fnCorreoReq(true);
            } else {
                fnCorreoReq(false);
            }
        }
        
        function fnEnvioEmail() {
            //alert($('#rblEstado input:checked').val());
            var est = $('#rblEstado input:checked').val();
            var acttf = $('#_tfFlujoAct').val();
            var ulttf = $('#_tfFlujoUlt').val();
            var teap = $('#_tieneEmailAprobacion').val();
            var tere = $('#_tieneEmailRechazo').val();
            
            var head= $('#_tEmail_cabecera').val();
            var body= $('#_tEmail_cuerpo').val();
            var foot= $('#_tEmail_pie').val();
            
            $('#pnlMensajeEmail').hide();

            //alert(acttf + ' ' + ulttf + ' ' + est + ' ' + teap + ' ' + tere);

/*
            if (acttf == ulttf) {

                if (est == 'A') {
                    if (teap == 1) {
                        $('#pnlMensajeEmail').show();
                    } else {
                        $('#pnlMensajeEmail').hide();
                    }

                } else if (est == 'R') {
                    if (tere == 1) {
                        $('#pnlMensajeEmail').show();
                    } else {
                        $('#pnlMensajeEmail').hide();
                    }
                }
            } else {
            
                    if (est == 'R') {
                                if (tere == 1) {
                                    $('#pnlMensajeEmail').show();
                                } else {
                                    $('#pnlMensajeEmail').hide();
                                }

                                if (head != "") {
                                    $('#txtmensajeemail').val("");
                                }


                            } else {

                                if (teap == 1) {
                                    $('#pnlMensajeEmail').show();
                                    if (head != "") {
                                        $('#txtmensajeemail').val(head + ':' + body + '\n' + foot);
                                    } else {
                                        $('#txtmensajeemail').val("");
                                    }
                                }
                                else {
                                    $('#pnlMensajeEmail').hide();
                                }
                           }
                       }
            */        
        }
        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }
        
        function openModalEstudiante() {
            $('#divDatosEstudiante').modal('show');
        }

        function closeModalEstudiante() {
            $('#divDatosEstudiante').modal('hide');
        }

        
        function openModalFecha() {
            $('#myModalFecha').modal('show');
        }

        function closeModalFecha() {
            $('#myModalFecha').modal('hide');
        }

        function fnCorreoReq(sw) {
            if (sw) {
                $('#pnlMensajeEmailReq').show();
            } else {
                $('#pnlMensajeEmailReq').hide();
            }
        
        }
        
        function ShowMessage(message, messagetype) {
           
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case '1':
                    cssclass = 'alert-danger'
                    break;
                    
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }

            if (cssclass != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }

        function ShowMessage2(message, messagetype) {
            $('#alert_container').html('<div id="alert_div"  class="alert ' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }

        function ShowMessageEmail( message, messagetype) {
            //console.log(capa + ',' + message + '' + messagetype);
            //$('div#alert_container_email').html('<div id="alert_div_email" class="alert alert-' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            $('div#alert_container_email').html('<div id="alert_div_email" class="alert alert-' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }

        function ShowMessageEmailDestino( message, messagetype) {
            console.log( message + '' + messagetype);
            //$('div#alert_container_email').html('<div id="alert_div_email" class="alert alert-' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            $('div#alert_container_email_destino').html('<div id="alert_div_email_destino"  class="alert alert-' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }

        function ShowMessageSMS(message, messagetype) {
            console.log(message + '' + messagetype);
            //$('div#alert_container_email').html('<div id="alert_div_email" class="alert alert-' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            $('div#alert_container_sms').html('<div id="alert_div_sms"  class="alert alert-' + messagetype + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }
        
        
        function fnMostrarEvaluar(sw) {
            if (sw == 0) {
                $('#pnlLista').show();
                $('#pnlRegistro').hide();
            }
            else {
                $('#pnlLista').hide();
                $('#pnlRegistro').show();
            }
        }

        function MascaraEspera(sw) {
            if (sw == "1")
                $("#modalFechaBody").mask("Espere...");
            if (sw == "0")
                $("#modalFechaBody").unmask();
        }

        function MascaraEsperaModal(sw) {
            if (sw == "1")
                $("#modalFinalizaBody").mask("Espere...");
            if (sw == "0")
                $("#modalFinalizaBody").unmask();
        }
        function GroupDropdownlist() {

            var id = 'ddlEscuela';

            var lp = 0;
            var lh = 0;
            var selectControl = $('#' + id);
            var groups = [];
            var groups2 = [];
            var groups3 = [];
            $(selectControl).find('option').each(function() {
                groups.push($(this).attr('data-group'));
            });
            $(selectControl).find('option').each(function() {
                groups2.push($(this).val());
            });
            $(selectControl).find('option').each(function() {
                groups3.push($(this).text());
            });
           /* console.log(groups);
            console.log(groups2);
            console.log(groups3);*/
            var uniqueGroup = groups.filter(function(itm, i, a) {
                return i == a.indexOf(itm);
            });

          //  console.log(uniqueGroup);
            var s = '';
            var i=0,j=0;

            lp=uniqueGroup.length;
            lh = groups3.length;
            for (i = 0; i < lp; i++) {
                s += '<optgroup label="' + uniqueGroup[i] + '">';
                for (j = 0; j < lh; j++) {
                    if (uniqueGroup[i] == groups[j]) {
                        s += '<option value="' + groups2[j] + '">' + groups3[j] + '</option>';
                    }
                }
                    
                s += '</optgroup>';
            }

            //console.log(s);
            selectControl.html(s);

            var cpf = $('#cpf').val();
          
            $('#' + id).val(cpf);
        }
        function GroupDropdownlist2() {

           

            var selectControl = $('#ddlEscuela')

            var EmployeesGRP = jQuery('<optgroup/>', {
                label: 'Employees'
            }).appendTo(selectControl);
            var GroupsGRP = jQuery('<optgroup/>', {
                label: 'Groups'
            }).appendTo(selectControl);

            jQuery('option', selectControl).each(function(i) {
                var item = jQuery(this);

                if (item.hasClass("Employees")) {
                    item.appendTo(EmployeesGRP);
                } else {
                    item.appendTo(GroupsGRP);
                }
            });

            var cpf = $('#cpf').val();

            $('#' + id).val(cpf);
        }
        function DescargarArchivo(IdArchivo) {              
                            window.open("DescargarArchivo.aspx?Id=" + IdArchivo, 'ta', "");                     
        }
    </script>

<style>
    ul {
      list-style: none;
    }

    ul li:before {
      content: '✓';
    }
    ul li:before {
      content: '✓';
    }

</style>
<style type="text/css">
    .radioButtonList { list-style:none; margin: 0; padding: 0;}
    .radioButtonList.horizontal li { display: inline;}

    .radioButtonList label{
        display:inline;
    }
</style>
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdTfu" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdTfuDesc" runat="server"></asp:HiddenField>                       
    <asp:HiddenField ID="hdOrden" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdNombrectr" runat="server"></asp:HiddenField>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container"></div>
        <div class="messagealert" id="alert_container_email"></div>       
        <div class="messagealert" id="alert_container_email_destino"></div> 
        <div class="messagealert" id="alert_container_sms">
                     
                </div> 
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel-heading">
                Listado de Tr&aacute;mites
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-8">
                        <div class="form-group">
                            <label for="txtAlumno" class="col-md-5 col-form-label">
                                C&oacute;d. Univ./Apellidos y Nombres/DNI:</label>                                
                                <div class="col-md-7">
                                <asp:TextBox ID="txtAlumno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="cboEstado" class="col-md-3 col-form-label">
                                Estado:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="%">TODOS</asp:ListItem>
                                    <asp:ListItem Value="P">PENDIENTE</asp:ListItem>
                                    <asp:ListItem Value="T">FINALIZADO</asp:ListItem>
                                    <asp:ListItem Value="E">ENTREGADO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    </div>
                      <div class="row">
                        <div class="col-md-8">
                        <div class="form-group">
                        <label for="ddlEscuela" class="col-md-3 col-form-label">
                                Carrera Profesional:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlEscuela" runat="server"  CssClass="form-control" >                                  
                                </asp:DropDownList>
                                <asp:HiddenField ID="cpf" runat="server"></asp:HiddenField>
                            </div>
                        </div>
                        </div>
                        <div class="col-md-4">
                         <div class="form-group">
                        <label for="ddlEscuela" class="col-md-3 col-form-label">
                                Tr&aacute;mite:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlconceptotramite" runat="server"  CssClass="form-control" >                                  
                                </asp:DropDownList>         
                                 <asp:HiddenField ID="ctr" runat="server"></asp:HiddenField>                       
                            </div>
                        </div>
                        <div class="btn-group right">
                        
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info" />
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" class="btn btn-success" />
                       
                       </div>
                        </div>
                      </div>
                    
                </div>
             
                    <div class="panel-body">
                <div class="row">
                    <div class="col-md-12"> <!--datos-->
                        
                            <div class="table-responsive">
                                <asp:GridView ID="gvExportar" runat="server" AutoGenerateColumns="False" Visible="false">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_dta" HeaderText="ID" />
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="CÓD. UNIV." />
                                        <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />
                                        <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="No. DOC." />
                                        <asp:BoundField DataField="fecha_cin" HeaderText="F. PAGO" />
                                        <asp:BoundField DataField="descripcion_Sco" HeaderText="TRÁMITE" />
                                        <asp:BoundField DataField="estado_trl" HeaderText="ESTADO" />
                                        <asp:BoundField DataField="observacion_trl" HeaderText="DESCRIPCIÓN SOLICITUD" />
                                        <asp:BoundField DataField="fechaFin_dta" HeaderText="F. ENT." />
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdFecha_Mail" runat="server"  />
                                <asp:HiddenField ID="nroTramite" runat="server"  />
                                <asp:GridView ID="gvDatos" runat="server" Width="100%" DataKeyNames="codigo_dta,tieneRequisito,nInstancia,codigo_tfu,_admin,sco,descripcion_Sco,glosaCorrelativo_trl,nTieneEvaluacionPersonal,valor,fecha_Cin,fecharegistro"
                                    CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" ShowHeader="true">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_dta" HeaderText="ID"  ItemStyle-Width=10 />
                                        <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="Nro.TRÁMITE"  ItemStyle-Width=40 />
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL"  ItemStyle-Width=80 ItemStyle-HorizontalAlign=Justify />
                                        
                                        <%--<asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV."  ItemStyle-Width=40   />--%>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderText="CÓD. UNIV."  ItemStyle-Width=80  >                                  
                                        <ItemTemplate>
                                        
                                            <asp:LinkButton ID="lnkButton3"  runat="server"  OnClick="Image3_Click" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" ForeColor=Orange Font-Bold=true>
                                            <%#Eval("codigoUniver_Alu")%>
                                            <asp:Image ID="Image3" runat="server" ImageUrl="img/busca.gif" Width="10%" Height="50%"  />                                            
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" ItemStyle-Width=80 ItemStyle-HorizontalAlign=Justify  />                                        
                                        <asp:BoundField DataField="fecha_cin" HeaderText="F. PAGO"  ItemStyle-Width=20 />
                                        <asp:BoundField DataField="descripcion_Sco" HeaderText="TRÁMITE" ItemStyle-Width=80 ItemStyle-HorizontalAlign=Justify  />
                                        <asp:BoundField DataField="estado_trl" HeaderText="ESTADO"  ItemStyle-Width=20 />
                                        <asp:BoundField DataField="observacion_trl" HeaderText="DESCRIPCIÓN SOLICITUD"  ItemStyle-Width=150 ItemStyle-HorizontalAlign=Justify />
                                        <asp:BoundField DataField="fechaFin_dta" HeaderText="F. ENT."  ItemStyle-Width=20/>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ACTUALIZAR FECHA"  ItemStyle-Width=40  >
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkButton" runat="server"  OnClick="Image1_Click" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="img/Calendar.png"  />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="ENVIAR MAIL"  ItemStyle-Width=40 >                                        
                                            <ItemTemplate>
                                               <asp:LinkButton ID="lnkButton2" runat=server  OnClick="Image2_Click"  CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>">
                                               <asp:Image ID="Image2" runat="server" ImageUrl="img/envio.gif" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPERACIÓN"  ItemStyle-Width=60 ItemStyle-VerticalAlign="Middle" >
                                            <ItemTemplate>
                                                <%--Botones de eliminar y editar --%>
                                                <asp:Button ID="btnEvaluar" runat="server" Text="Evaluar" OnClick="btnEvaluar_Click"
                                                    CssClass="btn btn-warning btn-sm" CommandName="Evaluar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Finalizar" OnClick="btnAtender_Click"
                                                    CssClass="btn btn-success btn-sm" CommandName="Delete" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                                <asp:Button ID="btnEdit" runat="server" Text="Entregar" OnClick="btnEntregar_Click"
                                                    CssClass="btn btn-info btn-sm" CommandName="Edit" OnClientClick="return confirm('¿Desea confirmar la entrega?');"
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                            </ItemTemplate>
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron registros!
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="11px" />
                                    <RowStyle Font-Size="11px" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </div>
                        
                    </div>
                </div>
            </div>
        </div>
    
       <div class="row col-lg-12 panel" id="pnlRegistro" runat="server">
        <div class="panel panel-default" id="Div1" runat="server">
            <div class="panel-heading">
                Evaluar Requisitos
            </div>
         <%--   <div class="panel-body">
                <div class="row">
                
                    <div class="col-md-12">
                      <table style="width:100%" class="table table-bordered bs-table" >
                       <tr>
                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">C&oacute;digo Tr&aacute;mite</th>
                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblReqNroTramite" runat="server"></asp:Label> </td>
                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Alumno</th>
                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblReqAlumno" runat="server"></asp:Label> </td>
                       </tr>
                      </table>
                    </div>    
               
                    <div class="col-md-6">
                        <label class="col-md-4" for="" style="color:White;background-color:#D9534F;font-size:12px;">
                        C&oacute;digo Tr&aacute;mite
                        </label>                        
                        <div class="col-md-8">
                            <span id="spNroTramite" runat="server" style="background-color: Khaki;"></span>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label class="col-md-4" for="" style="color:White;background-color:#D9534F;font-size:12px;">
                        Alumno
                        </label>                        
                        <div class="col-md-8">
                            <span id="spAlumno" runat="server" style="background-color: Khaki;"></span>
                        </div>
                    </div>                          
                </div>
            </div>           --%>         
                        
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            Proceso de Evaluaci&oacute;n de Requisitos</h4>
                        <div style="display: inline-block; width: 100%; overflow-y: auto;" id="divTimeline"
                            runat="server">
                        </div>
                        <%--Botones de eliminar y editar --%>
                        <asp:HiddenField ID="hdtimelineactive" runat="server" />
                        <asp:HiddenField ID="hdtimelinechk" runat="server" />
                        <asp:HiddenField ID="hddtareq" runat="server" />
						<asp:HiddenField ID="hddtaindex" runat="server" />
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-7">
                        
                        <asp:GridView ID="gvRequisitos" runat="server" AutoGenerateColumns="False" DataKeyNames="cumple_dre,codigo_dre,codigo_tfu,codigo_dft,nombre_tre,codigo_tfu_req"
                            class="table table-bordered bs-table" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="REQUISITOS CORRESPONDIENTES A SU PERFIL" DataField="nombre_tre" />
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <!--<asp:CheckBox ID="chkHeader" runat="server" /> -->
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkElegir" runat="server" CssClass="form-control" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cumple_dre" Visible="False" />
                                <asp:BoundField DataField="codigo_tfu" Visible="false" />
                                <asp:BoundField DataField="codigo_dre" HeaderText="codigo_dre" Visible="False" />
                                <asp:BoundField DataField="codigo_dft" Visible="false" />
                            </Columns>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                Font-Size="12px" />
                        </asp:GridView>
                        
                        <div id="Div5" class="panel"  runat="server">
                            <div id="Div7" class="panel panel-default" runat="server">
                                <div class="panel-heading" style="font-weight:bold">
                                    INFORMACI&Oacute;N DEL ESTUDIANTE</div>
                                    <div class="panel-body">
                                       <table style="width:100%" class="table table-bordered bs-table" >
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">ID del Tr&aacute;mite</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstNumero3" runat="server"></asp:Label> </td>
                                       </tr>
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tr&aacute;mite</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTramite3" runat="server"></asp:Label></td>
                                       </tr>
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Descripci&oacute;n</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDescripcion3" runat="server"></asp:Label></td>
                                       </tr>
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Carrera Profesional</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEscuela3" runat="server"></asp:Label></td>
                                       </tr>
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Apellidos y Nombres</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstAlumno3" runat="server"></asp:Label></td>
                                       </tr>
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">C&oacute;digo Universitario</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstCodUni3" runat="server"></asp:Label></td>
                                       </tr>
                                        <tr>
                                        <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Nro. Doc. Identidad</th>
                                        <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDocIden3" runat="server"></asp:Label></td>
                                        </tr>                                        
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Fecha Nacimiento</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstFecNac3" runat="server"></asp:Label></td>
                                       </tr>                                       
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email Estudiante</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmail3" runat="server"></asp:Label></td>
                                       </tr>
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email Personal</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmailp3" runat="server"></asp:Label></td>
                                       </tr>                                        
                                       <tr>
                                       <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tel&eacute;fono</th>
                                       <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTelefono3" runat="server"></asp:Label></td>
                                       </tr>
                                       </table>
                                    </div>             
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                       <div class="row">
                            <div id="pnlMensajeEmailReq" class="panel panel-priimary" runat="server" style="display:none">
                            <div class="panel-heading" style="font-weight:bold">
                               <i class="glyphicon glyphicon-send"></i> ENVIAR MENSAJE 
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                <asp:TextBox ID="txtmensajemailreqcab" runat="server" TextMode=SingleLine CssClass="form-control" placeholder="Escribir titulo"  BackColor=Khaki Font-Bold=true ></asp:TextBox>
                                </div>
                                <div class="row">
                                <asp:TextBox ID="txtmensajeemailreq" runat="server" TextMode=MultiLine CssClass="form-control" placeholder="Escribir cuerpo del mensaje..."  Columns="50" Rows="5" BackColor=Khaki  ></asp:TextBox>
                                </div>
                                <div class="row">
                                <asp:TextBox ID="txtmensajemailreqpie" runat="server" TextMode=SingleLine CssClass="form-control" placeholder="Escribir pie"  BackColor=Khaki Font-Bold=true  ></asp:TextBox>
                                </div>
                                
                            </div>
                            </div>                                         
                          </div>
                       <div class="row" id="colBotonReq" runat="server">                                              
                        <center>                        
                            <asp:LinkButton ID="lnkAnt" runat="server" CssClass="btn btn-danger" Width="100%" Text="Exportar"><span class="glyphicon glyphicon-fast-backward"></span> Regresar</asp:LinkButton>
                            <asp:LinkButton ID="lnkSgt" runat="server" CssClass="btn btn-info" Width="100%"  Text="Exportar">Aprobar <span class="glyphicon glyphicon-fast-forward"></span> </asp:LinkButton>
                            
                        </center>
                        </div>
                          
                         <div class="row" id="colConfirmaReq" runat="server">
                         <!-- Confirma Req -->
                           <center>
                            <asp:Panel ID="pnlPreguntaReq" runat="server" BorderColor="#5D7B9D" 
                                BorderStyle="Solid" BorderWidth="1px" style="text-align: center; padding:5px;" 
                                Width="100%" Height="100%" BackColor="#F7F6F4">
                                 <table style="width: 100%" class="contornotabla"  >
                                     <tr>
                                        <td style="color: white; font-size: 12px; background-color:orange;" height="35px">                   
                                          <i class="glyphicon glyphicon-warning-sign"></i>  <asp:Label ID="Label2" runat="server" Text="APROBACION DE REQUISITOS"></asp:Label></b>
                                     </td>                
                                     </tr>
                                     </table>
                                     <div class="row">
                                        <span class="style1" style="font-weight:bold">
                                          <div class="col-md-12">
                                           ¿Estas seguro de aprobar los siguientes requisitos del tramite?
                                           </div>
                                        </span>
                                         <div class="col-md-12">
                                            <div class="row">
                                            <div class="col-md-12">
                                            <div id="ulselreq" runat="server">
                                            </div>
                                            </div>
                                        </div>
                                      
                                        <div class="row">                        
                                        <asp:Button ID="btnSiReq" runat="server" CssClass="btn btn-primary" Text="SI" Width="50px" ></asp:Button >
                                        &nbsp;&nbsp;
                                        <asp:Button ID="btnNoReq" runat="server" CssClass="btn btn-danger" Text="NO" Width="50px" ></asp:Button >
                                        </div>
                                        </div>
                              </div>
                            </asp:Panel>
    </center> 
                         <!-- Confirma Req -->
                         </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
 
    
    <div class="row col-lg-12 panel" id="pnlRegistro2" runat="server">
        <div class="panel panel-default" runat="server">
            <div class="panel-heading" style="font-weight:bold">
                EVALUACI&Oacute;N DEL TR&Aacute;MITE VIRTUAL
             </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4>
                            Proceso de Evaluaci&oacute;n</h4>
                        <div style="display: inline-block; width: 100%; overflow-y: auto;" id="divTimelineFlujo"
                            runat="server">
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-6">
                    <asp:HiddenField ID="txtcodalu" runat="server" Value=""></asp:HiddenField>
                        <asp:GridView ID="gvFlujoTramite" runat="server" AutoGenerateColumns="False" DataKeyNames="cumple_dft,codigo_dft,codigo_ftr,codigo_tfu,verDetAcad_ftr,verDetProyectotesis,verDetAdm_ftr,accionURL_ftr,codigoUniver_Alu,codigo_Alu,tipo,codigo_trl,proceso,codigo_dta,descripcion_ctr,observacion_trl,tieneEntrega,tieneEmailRechazo,nFlujoUlt,tfFlujoUlt,tieneEmailAprobacion,codigo_ftr_email,config_email,cabecera_email,cuerpo_email,pie_email,tieneEmailPlanEstudio,tieneEmailPrecioCredito,tieneEmailGyT,tieneMsjTextoRechazo,tieneMsjTextoAprobacion,orden_ftr,procesoEmailAprobacion,procesoEmailRechazo"
                            class="table table-bordered bs-table" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="EVALUADORES" DataField="descripcion_Tfu" />
                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <!--<asp:CheckBox ID="chkHeader" runat="server" /> -->
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkElegir" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="cumple_dft" Visible="False" />
                                <asp:BoundField DataField="codigo_tfu" Visible="false" />
                                <asp:BoundField DataField="codigo_Alu" Visible="false" />
                            </Columns>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                Font-Size="12px" />
                        </asp:GridView>
                    </div>
                    <div class="col-md-6">
                    
                     <div class="panel panel-default" runat="server">
                            <div class="panel-heading" style="font-weight:bold">
                               <i class="glyphicon glyphicon-list"></i> EVALUACI&Oacute;N DEL TR&Aacute;MITE</div>
                            <div class="panel-body">
                                                    
                            <div class="row">
                            <asp:Panel ID="pnlEvaluacionFlujo" runat="server">
                                <div class="col-md-12">
                                    <div class="form-group">
                                       <%-- <label class="control-label col-md-4" for="txtobservacionaprobacion">Observacion:  </label>--%>
                                       
                                       <div class="col-md-12" style="margin-left:10px;">
                                       <asp:RadioButtonList ID="rblEstado" runat="server" Width="100px" RepeatDirection=Horizontal CssClass="radioButtonList" >
                                                <asp:ListItem Text="Aprobar" Value="A" >&nbsp;&nbsp;<i class="glyphicon glyphicon-thumbs-up"></i>Aprobar&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Text="Rechazar" Value="R">&nbsp;&nbsp;<i class="glyphicon glyphicon-thumbs-down"></i>Rechazar&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                <asp:ListItem Text="Observar" Value="O">&nbsp;&nbsp;<i class="glyphicon glyphicon glyphicon-eye-open"></i>Observar</asp:ListItem>
                                            </asp:RadioButtonList>
                                       </div>
                                       <hr />
                                       <div class="col-md-12" >
                                        <label class="control-label" style="margin-left:10px;">
                                    AÑADIR OBSERVACI&Oacute;N:</label>
                                         <div class="col-md-12" >
                                        <asp:TextBox ID="txtobservacionaprobacion" runat="server" TextMode=MultiLine CssClass="form-control" placeholder="Observación del proceso"  Columns="50" Rows="5"></asp:TextBox>
                                        </div>
                                        </div>
                                        
                                         <div class="col-md-12" >
                                            <div id="pnlMensajeEmail" class="panel panel-priimary" runat="server" style="display:none">
                                            <div class="panel-heading" style="font-weight:bold">
                                               <i class="glyphicon glyphicon-send"></i> ENVIAR MENSAJE DE APROBACIÓN <i class="glyphicon glyphicon-thumbs-up"></i> O RECHAZO <i class="glyphicon glyphicon-thumbs-down"></i>
                                            </div>
                                            <div class="panel-body">
                                                <asp:TextBox ID="txtmensajeemail" runat="server" TextMode=MultiLine CssClass="form-control" placeholder="Escribir mensaje..."  Columns="50" Rows="5" BackColor=Khaki Font-Bold=true ></asp:TextBox>
                                            </div>
                                            </div>                                         
                                         </div>
                                    </div>
                                </div>
                            </asp:Panel>
                            </div>
                          
                             <div class="row">
                                <div class="panel" id="ifrRetCiclo" runat="server">
                                        <div id="Div2" class="panel panel-default" runat="server">
                                        <div class="panel-heading">
                                         <i class="glyphicon glyphicon-calendar"></i>  ULTIMA FECHA ASISTENCIA </div> 
                                        <div class="panel-body">
                                            <div class="row">                                        
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                                <label class="control-label" for="txtUltimaFechaAsistencia" style="margin-left:15px">Ingrese Fecha:  <i class="glyphicon glyphicon-pencil"></i> </label>
                                                                <div class="col-md-12">
                                                                <asp:TextBox ID="txtUltimaFechaAsistencia" runat="server" CssClass="form-control" placeholder="dd/mm/yyyy"></asp:TextBox>
                                                                </div>
                                                        </div>
                                                     </div>                                       
                                             </div>
                                             <div class="row">                                        
                                                    <div class="col-md-12">
                                                        <!--Lista de cursos matriculados y ultima asistencia -->
                                                        <asp:GridView ID="gvCursosMatriculadosAsistencia" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_Cup"
                                                            class="table table-bordered bs-table" Width="100%" Font-Size=X-Small>
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Curso" DataField="nombre_Cur" />
                                                                <asp:BoundField HeaderText="Grupo" DataField="grupoHor_Cup" />
                                                                <asp:BoundField HeaderText="Ultima Fecha Asistencia" DataField="ultimafechaasi" />
                                                            </Columns>
                                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                                                        </asp:GridView>
                                                    </div>
                                             </div>
                                         </div>
                                       </div>
                                </div>
                               
                                 <div class="panel" id="ifrDocEspecialista" runat="server">
                                     <div  class="panel panel-default">
                                      <div class="panel-heading">
                                        <i class="glyphicon glyphicon-education"></i> DOCENTE ESPECIALISTA
                                      </div>
                                      <div class="panel-body">
                                          <div class="row">                                        
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                <label class="control-label" for="cboDocente" class="form-control">Docente:  <i class="glyphicon glyphicon-pencil"></i> </label>
                                                <asp:DropDownList ID="cboDocente" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                          </div>    
                                      </div>
                                     </div>
                                 </div>
                            </div>
                             <div class="row">
                                
                                 <div class="col-md-6">
                                    <asp:LinkButton ID="lnkAnt2" runat="server" CssClass="btn btn-danger" Width="100%" Text="Exportar"><span class="glyphicon glyphicon-fast-backward"></span> Regresar</asp:LinkButton>
                                 </div>
                                  <div class="col-md-6">
                                    <asp:LinkButton ID="lnkSgt2" runat="server" CssClass="btn btn-info" Width="100%"  Text="Exportar">Procesar <span class="glyphicon glyphicon-fast-forward"></span> </asp:LinkButton>
                                  </div>
                                
                                </div>
                            </div>
                     </div>
                    
                    </div>
                </div>
            </div>
        </div>
         <div class="row">
        <div class="col-md-6 panel" id="ifrAccion" runat="server">
            <div class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N DEL TR&Aacute;MITE</div>
                <div class="panel-body">
                    <div class="row" style="display:none">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Acciones:</label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlAccion" runat="server" CssClass="form-control" Font-Bold="true"
                                        Enabled="False">
                                        <asp:ListItem Text="Retiro de Ciclo Academico" Value="T" />
                                        <asp:ListItem Text="Retiro definitivo de la Universidad" Value="D" />
                                        <asp:ListItem Text="Retiro de ciclo académico sin complementario" Value="C" />
                                        <asp:ListItem Text="Retiro por devolución de matrícula" Value="E" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                                            <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;">
                                    Tr&aacute;mite:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblTramite" runat="server" CssClass="form-control" Enabled=false Height="80px"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;">
                                   Descripci&oacute;n Tr&aacute;mite:</label>
                                <div class="col-md-12">
                                    <%--<asp:Label ID="lblTramiteObservacion" runat="server" CssClass="form-control" Enabled=false></asp:Label>--%>
                                    <asp:TextBox ID="txtTramiteObservacion" runat="server" TextMode="MultiLine" CssClass="form-control" Enabled=false  Rows=6></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;">
                                    Fecha Tr&aacute;mite:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblFechaTramite" runat="server" CssClass="form-control" Enabled=false></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                         <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;">
                                    Fecha Pago Tr&aacute;mite:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblFechaPago" runat="server" CssClass="form-control" Enabled=false></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="trcicloacad" runat="server" >
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" id="lblSemestreMatriculado" runat="server">
                                    &Uacute;ltimo semestre matriculado:</label>
                                <div class="col-md-12">
                                    <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control" Font-Bold="true" Enabled=false>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>                          
                          
                                 
                     <div class="row">
                             <div class="col-md-12" id="divInfoProyectoTesis" runat="server" visible=false >                             
                             <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" id="lblInfoProyectoTesis" runat="server">
                                    Informaci&oacute;n Proyecto de Tesis:</label>
                                <table style="width:100%" class="table table-bordered bs-table" >                             
                                <tr>
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt; text-align: justify">Solicitante</th>
                                <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblPry_Solicitud" runat="server"></asp:Label></td>
                                </tr>
                                <tr>
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Facultad</th>
                                <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblPry_Facultad" runat="server"></asp:Label></td>
                                </tr> 
                                <tr>
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Programa de estudios </th>
                                <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblPry_Programa" runat="server"></asp:Label></td>
                                </tr>
                                                                 
                                <tr>
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Título de tesis</th>
                                <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblPry_tesis" runat="server"></asp:Label></td>
                                </tr> 
                                <tr>
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Presupuesto, Financiamiento</th>
                                <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblPry_presupuesto" runat="server"></asp:Label></td>
                                </tr>                                    
                                <tr>
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Objetivo General</th>
                                <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblPry_objetivog" runat="server"></asp:Label></td>
                                </tr> 
                                <tr>    
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Objetivo Especifico</th>
                                <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblPry_objetivoe" runat="server"></asp:Label></td>
                                </tr>   
                                <tr>    
                                <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Fecha de aprobación</th>
                                <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblPry_fechaaprobacion" runat="server"></asp:Label></td>
                                </tr>     
                                </table>
                                 </div>
                                 </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" id="lblInfoAdicional" runat="server">
                                    Informaci&oacute;n adicional de la solicitud del tr&aacute;mite:</label>
                                <div class="col-md-12">
                                <div class="table-responsive">
                                <asp:GridView ID="gDatosAdicional" runat="server" AutoGenerateColumns="False" DataKeyNames="tabla,valorcampo"
                                    class="table table-bordered bs-table" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="INFORMACIÓN" DataField="tabla" />
                                        <asp:BoundField HeaderText="DETALLE" DataField="valorcampo" />
                                        <asp:BoundField HeaderText="OBSERVACIÓN ADICIONAL" DataField="observacion" />
                                    </Columns>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                </asp:GridView>
                                </div>
                                </div>
                             </div>
                         </div>
                         <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" id="lblTotalSemestre" runat="server">
                                    Total de Semestres:</label>
                                <div class="col-md-12">
                                <asp:Label ID="lblNumSemestre" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                     </div>
                    
                    <div class="row" id="rowObservacion" runat="server">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Observaci&oacute;n:</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtobs" runat="server" CssClass="form-control" TextMode="multiline"
                                        Columns="50" Rows="5" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
         <div class="col-md-6 panel"  runat="server">
            <div id="Div6" class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N DEL ESTUDIANTE</div>
                <div class="panel-body">
                   <table style="width:100%" class="table table-bordered bs-table" >
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">ID del Tr&aacute;mite</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstNumero" runat="server"></asp:Label> </td>
                   </tr>
                    <tr>
                   <!-- <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tr&aacute;mite</th>
                    <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTramite" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Descripci&oacute;n</th>
                    <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDescripcion" runat="server"></asp:Label></td>
                    </tr>  -->                
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Carrera Profesional</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEscuela" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Apellidos y Nombres</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstAlumno" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">C&oacute;digo Universitario</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstCodUni" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                    <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Nro. Doc. Identidad</th>
                    <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDocIden" runat="server"></asp:Label></td>
                    </tr>                    
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Fecha Nacimiento</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstFecNac" runat="server"></asp:Label></td>
                   </tr>                    
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email Estudiante</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmail" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email Personal</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmailP" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tel&eacute;fono</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTelefono" runat="server"></asp:Label></td>
                   </tr>
                   </table>
                </div>             
            </div>
        </div>
                                
        </div>
        
        <div class="row">
        <div class="col-md-12 panel" id="ifrGeneraDeudaPorSemestre" runat="server">
            <div id="Div4" class="panel panel-default" runat="server">
                <div class="panel-heading">
                    Deudas a generar por trámite solicitados</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvDeudaPorSemestre" runat="server" AutoGenerateColumns="False" 
                                Width="100%" class="table table-bordered bs-table" HeaderStyle-Font-Size="10px">
                                <Columns>
                                
                                   <asp:BoundField DataField="concepto" HeaderText="Observación Deuda" ReadOnly="True" SortExpression="concepto"
                                        ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="50%" />
                                   </asp:BoundField>
                                    <asp:BoundField DataField="precio" HeaderText="Precio" ReadOnly="True" SortExpression="precio" DataFormatString="S/ {0:###,###,###.00}"
                                        ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="10%" />
                                   </asp:BoundField>
                                    <asp:BoundField DataField="fecha_deu" HeaderText="Fecha Deuda" SortExpression="fecha_deu"
                                        DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundField>
                                   
                                 <asp:BoundField DataField="fecha_vence" HeaderText="Fecha Vence" SortExpression="fecha_vence"
                                        DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="20%" />
                                    </asp:BoundField>                                   
                                  
                                </Columns>
                                <HeaderStyle BackColor="#3366CC" ForeColor="#FFFFFF" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="10px" Height="10px" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="No se han realizado calculo de deuda por de reincorporacion de semestres seleccionados"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="usatCeldaHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        
        <div class="row">
        <div class="col-md-12">
        <div class="col-md-12 panel" id="ifrHistorial" runat="server">
            <div class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N ACAD&Eacute;MICA</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <iframe id="frameHistorial" frameborder="0" height="220" name="frameHistorial" src=""
                                width="100%"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        <div class="col-md-12">
        <div id="ifrInformes" class="col-md-12 panel" runat="server">
            <div id="Div3" class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N ADMINISTRATIVA</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GvEstadoCue" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_Deu"
                                Width="100%" class="table table-bordered bs-table" HeaderStyle-Font-Size="10px">
                                <Columns>
                                    <asp:BoundField DataField="fechaVencimiento_Sco" HeaderText="Fecha de Venc." SortExpression="fechaVencimiento_Sco"
                                        DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="120px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SERVICIO" HeaderText="Servicio" ReadOnly="True" SortExpression="SERVICIO"
                                        ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="estado_Deu" HeaderText="Estado" SortExpression="estado_Deu"
                                        ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CARGO" HeaderText="Cargo" ReadOnly="True" SortExpression="CARGO"
                                        ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PAGOS" HeaderText="Pago" SortExpression="PAGOS" ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SALDO" HeaderText="Saldo" SortExpression="SALDO" ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mora_deu" HeaderText="Mora" ReadOnly="True" SortExpression="Mora_deu"
                                        ItemStyle-Font-Size="10px">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" SortExpression="DOCUMENTO"
                                        Visible="False" ItemStyle-Font-Size="10px" />
                                    <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" Visible="False" />
                                    <asp:BoundField DataField="CODIGO_RESP" HeaderText="CODIGO_RESP" ReadOnly="True"
                                        SortExpression="CODIGO_RESP" Visible="False" />
                                    <asp:BoundField DataField="RESPONSABLE" HeaderText="RESPONSABLE" ReadOnly="True"
                                        SortExpression="RESPONSABLE" Visible="False" />
                                    <asp:BoundField DataField="OBSERVACIÓN" HeaderText="OBSERVACIÓN" SortExpression="OBSERVACIÓN"
                                        Visible="False" />
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" Visible="False" />
                                    <asp:CheckBoxField DataField="generaMora_Sco" HeaderText="generaMora_Sco" SortExpression="generaMora_Sco"
                                        Visible="False" />
                                    <asp:BoundField DataField="codigo_Deu" HeaderText="codigo_Deu" InsertVisible="False"
                                        ReadOnly="True" SortExpression="codigo_Deu" Visible="False" />
                                    <asp:BoundField DataField="codigo_Sco" HeaderText="codigo_Sco" SortExpression="codigo_Sco"
                                        Visible="False" />
                                    <asp:BoundField DataField="Est" HeaderText="Est" SortExpression="Est" Visible="False" />
                                    <asp:BoundField HeaderText="Sub total" Visible="false">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle BackColor="#3366CC" ForeColor="#FFFFFF" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="10px" Height="10px" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="El estudiante no tiene deuda"></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="usatCeldaHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
        </div>
    </div>            
 
    </div>
    
    
    
    <asp:TextBox ID="_tieneEmailPlanEstudio"  runat="server" ReadOnly=true></asp:TextBox>
    <asp:TextBox ID="_tieneEmailPrecioCredito"  runat="server" ReadOnly=true></asp:TextBox>
    <asp:TextBox ID="_tieneEmailGyT"  runat="server" ReadOnly=true></asp:TextBox>
    
    <asp:TextBox ID="_tieneMsjTextoRechazo"  runat="server" ReadOnly=true></asp:TextBox>
    <asp:TextBox ID="_tieneMsjTextoAprobacion"  runat="server" ReadOnly=true></asp:TextBox>
    
    <asp:TextBox ID="_tieneEmailRechazo"  runat="server" ReadOnly=true></asp:TextBox>
    <asp:TextBox ID="_tieneEmailAprobacion"  runat="server"  ReadOnly=true></asp:TextBox>
    <asp:TextBox ID="_nFlujoUlt"  runat="server"  ReadOnly=true></asp:TextBox>
    <asp:TextBox ID="_nFlujoAct"  runat="server"  ReadOnly=true></asp:TextBox>
    
    <asp:TextBox ID="_tfFlujoUlt"  runat="server" ReadOnly=true></asp:TextBox>    
    <asp:TextBox ID="_tfFlujoAct"  runat="server" ReadOnly=true ></asp:TextBox>
    
    <asp:TextBox ID="_tEmail_send"  runat="server" ReadOnly=true ></asp:TextBox>
    <asp:TextBox ID="_tEmail_config"  runat="server" ReadOnly=true ></asp:TextBox>
    <asp:TextBox ID="_tEmail_cabecera"  runat="server" ReadOnly=true ></asp:TextBox>
    <asp:TextBox ID="_tEmail_cuerpo"  runat="server" ReadOnly=true ></asp:TextBox>
    <asp:TextBox ID="_tEmail_pie"  runat="server" ReadOnly=true ></asp:TextBox>
    
    <asp:TextBox ID="_FlujoCheck"  runat="server" ReadOnly=true Text=0 ></asp:TextBox>
    
    <asp:HiddenField ID="_FechaEmail" runat="server" />
    
    <!-- Modal -->
    <div id="divDatosEstudiante" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="background-color:orange; font-weight:bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">Datos del Estudiante</h4>
                </div>
                <div class="modal-body">
                <div class="row">
                <div class="col-md-12">
                   <table style="width:100%" class="table table-bordered bs-table" >
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">ID del Tr&aacute;mite</th>
                   <td style="background-color:Linen; font-size:10pt; width:50%" ><asp:Label ID="lblInfEstNumero2" runat="server"></asp:Label> </td>
                   <td rowspan=3 style="width:20% id="tdFotoAlu" runat="server"></td>
                   </tr>
                    <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tr&aacute;mite</th>
                    <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTramite2" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Descripci&oacute;n</th>
                    <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDescripcion2" runat="server"></asp:Label></td>
                    </tr>                                   
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt; text-align:justify">Carrera Profesional</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstEscuela2" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt; text-align: justify">Apellidos y Nombres</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstAlumno2" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">C&oacute;digo Universitario</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstCodUni2" runat="server"></asp:Label></td>
                   </tr> 
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Nro. Doc. Identidad</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstDocIden2" runat="server"></asp:Label></td>
                   </tr>                                    
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Fecha Nacimiento</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstFecNac2" runat="server"></asp:Label></td>
                   </tr>                   
                  <%-- 
                   <tr style="display:none">
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstEmail2" runat="server"></asp:Label></td>
                   </tr>                   
                   --%>                                     
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email Estudiante</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstEmail2E" runat="server"></asp:Label></td>
                   </tr>
                   
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email Personal</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstEmail2P" runat="server"></asp:Label></td>
                   </tr>                   
                   
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tel&eacute;fono</th>
                   <td style="background-color:Linen; font-size:10pt"  colspan =2><asp:Label ID="lblInfEstTelefono2" runat="server"></asp:Label></td>
                   </tr>

                   </table>
                </div>
                </div>
                <div class="row">
                <div class="col-md-12">
                 <asp:GridView ID="gdDeudaTramite" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_dta"
                                                            class="table table-bordered bs-table" Width="100%" Font-Size=X-Small>
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Trámite" DataField="descripcion_ctr" />
                                                                <asp:BoundField HeaderText="Cantidad" DataField="cantidad_dta" />
                                                                <asp:BoundField HeaderText="Precio" DataField="preciounitario" />
                                                                <asp:BoundField HeaderText="Importe" DataField="subtotal" />
                                                            </Columns>
                                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                                                        </asp:GridView>
                </div>
                </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="Button2" class="btn btn-danger" data-dismiss="modal">
                        Salir</button>
                </div>
                
            </div>
        </div>
    </div>
    <!-- Modal -->
    
    
    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        ATENCI&Oacute;N DE TR&Aacute;MITE</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <div class="form-group">
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" Height="100px"
                                MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">
                        Salir</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-info" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <div id="myModalFecha" class="modal fade" role="dialog">
        <div class="modal-dialog" id="modalFecha">
            <!-- Modal content-->
            <div class="modal-content" id="modalFechaBody">
                <div class="modal-header" id="divHeadFecha_Mail" runat="server">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title" id="h4Fecha_Mail" runat="server">
                       ACTUALIZAR FECHA DE ENTREGA</h4>
                </div>
                <div class="modal-body">                   
                 <div class="col-md-12">   
                    <div class="row" id="divModalFecha" runat="server" >
                        <div class="form-group">
                            <label for="fechaTramite" class="form-group-sm" >&nbsp;&nbsp;Fecha de Entrega:</label>
                            <div class='input-group date' id='fechaTramite'>
                                <input type='text' class="form-control" runat="server" id="txtFecha" />
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                </span>
                            </div>
                         </div>
                    
                    </div >
                      <div class="row"  id="divModalMensaje" runat="server">
                        <div class="form-group">
                        <label for="txtObservacionAlumno" class="form-group-sm" >&nbsp;&nbsp;Mensaje:</label>
                            <asp:TextBox ID="txtObservacionAlumno" runat="server" CssClass="form-control" Height="100px"
                                MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                            <br />
                        </div>
                        </div>
                   </div>                  
                </div>
                <div class="modal-footer">
                    <button type="button" id="Button1" class="btn btn-danger" data-dismiss="modal">
                        Salir</button>
                    <asp:Button ID="btnGuardaFecha" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
                <div class="modal-footer">
                <fieldset><legend>Historial</legend>
                                <asp:GridView ID="gvHistorialFecha" runat="server"
                                    Width="99%" DataKeyNames="codigo_dta" CssClass="table table-bordered bs-table"
                                    AutoGenerateColumns="False" >
                                    <Columns>                                        
                                        <asp:BoundField DataField="fechaFin_dta" HeaderText="FECHA FIN" ItemStyle-Width="80%"  />
                                        <asp:BoundField DataField="usuario_per" HeaderText="HECHO POR"  ItemStyle-Width="20%" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                    No se encuentra historial de fechas de entrega de este trámite
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered"  />
                                </asp:GridView>
                                 <asp:GridView ID="gvHistorialEmail" runat="server"
                                    Width="99%" DataKeyNames="codigo_dta" CssClass="table table-bordered bs-table"
                                    AutoGenerateColumns="False" >
                                    <Columns>
                                        <asp:BoundField DataField="observacionAlumno_dft" HeaderText="OBSERVACION"  ItemStyle-HorizontalAlign=Justify  ItemStyle-Width="80%" />                                        
                                        <asp:BoundField DataField="usuario_per" HeaderText="HECHO POR" ItemStyle-Width="20%" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                    No se encuentra historial de mensajes de este trámite
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                </fieldset>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="HdTramite" runat="server" />
    <asp:HiddenField ID="HdAccion" runat="server" />
    <asp:HiddenField ID="HdEmailErroneo" runat="server" />    
    <div id="divEmailErroneo" runat=server></div>
    </form>
</body>
</html>
