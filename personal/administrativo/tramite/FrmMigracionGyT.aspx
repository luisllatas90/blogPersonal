<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMigracionGyT.aspx.vb"
    Inherits="administrativo_tramite_FrmMigracionGyT" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Migraci&oacute;n Tr&acute;mite Virtual GyT</title>
    <link href="../../../private/estilo.css?z=x" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>     
    
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />  
    <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <link href="private/timeline.css" rel="stylesheet" type="text/css" />
    <script src="private/jquery.loadmask.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?y=1"></script>	
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
    
       <script src="../../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../../assets/js/promise.min.js" type="text/javascript"></script>
    
        <script type="text/javascript">
            var oTable;
            $(document).ready(function() {
                oTable = $('#gvDatos').DataTable({
                    //"sPaginationType": "full_numbers",
                    "bPaginate": false,
                    "bFilter": false,
                    "bLengthChange": false,
                    "bSort": true,
                    "bInfo": true,
                    "bAutoWidth": true
                });


            });

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

            function fnConfirmacion(ctrl) {

                var defaultAction = $(ctrl).prop("href");
                Swal.fire({
                    title: '¿Está seguro que desea migrar el tramite al nuevo sistema de GyT?',
                    text: "Luego no podrá revertir",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si',
                    cancelButtonText: 'No'
                }).then(function(result) {
                    if (result.value == true) {
                        eval(defaultAction);
                    }
                })
                /*
                if (confirm('¿Está seguro que desea dar conformidad a tesis?')) {
                return true;
                } else {
                return false;
                }*/
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
                var i = 0, j = 0;

                lp = uniqueGroup.length;
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

            function DescargarArchivo(IdArchivo) {

                window.open("../DescargarArchivo.aspx?Id=" + IdArchivo, 'ta', "");


            }
        </script>
    
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
     <div class="container-fluid">     
        <div class="messagealert" id="alert_container">
        </div>
        
           <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel-heading" style="text-align:center">
                MIGRACION DE TR&Aacute;MITES GYT
            </div>
            <div class="panel-body">
                <div class="row">
                  
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtnroTramite" class="col-md-5 col-form-label">
                            N&deg; de Tr&aacute;mite:
                            </label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtnroTramite" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
             
                      
                        <div class="col-md-6">
                        <div class="btn-group">
                         <asp:HiddenField ID="cpf" runat="server"></asp:HiddenField>
                         <asp:HiddenField ID="ctdg" runat="server" ></asp:HiddenField>
                         
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info" />
                          <%--<asp:Button ID="btnMigrar" runat="server" Text="Migrar" class="btn btn-success" OnClientClick="fnConfirmacion(this); return false;" />--%>
                          <asp:Button ID="btnMigrar" runat="server" Text="Migrar" class="btn btn-success" />
                          
                       
                       </div>
                        </div>
                </div>
                         
                    
            </div>
             <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="table-responsive">
                            
                            <asp:GridView ID="gvDatos" runat="server" Width="100%" DataKeyNames="codigo_dta,codigo_Alu,codigoUniver_Alu,codigo_trl,descripcion_ctr,fecha_cin,fechaReg_trl,usumod,fecmod"
                                    CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" ShowHeader="true">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_dta" HeaderText="ID" />
                                        <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="Nro.TRÁMITE" />
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                                        <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />                                        
                                        <asp:BoundField DataField="fecha_cin" HeaderText="F. PAGO" />
                                        <asp:BoundField DataField="descripcion_ctr" HeaderText="TR&Aacute;MITE" />
                                        <asp:BoundField DataField="estado_trl" HeaderText="ESTADO" />
                                        <asp:BoundField DataField="usumod" HeaderText="USUARIO" />
                                        <asp:BoundField DataField="fecmod" HeaderText="F.ANULADO" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="SEGUIMIENTO">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEvaluar" runat="server" Text="Ver"  CssClass="btn btn-warning btn-sm" CommandName="Evaluar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />                                               
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
           </div>
           <div class="panel panel-info" id="pnlRegistro" runat="server">
            <div class="panel-heading">
                Seguimiento de Tr&aacute;mite
            </div>
             <div class="panel-body">
                <div class="row">
                
                       <div id="Div1" class="col-md-6 panel"  runat="server">
                 <div id="Div2"  class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N DEL ESTUDIANTE</div>
                <div class="panel-body">
                   <table style="width:100%" class="table table-bordered bs-table" >
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">ID del Tr&aacute;mite</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstNumero" runat="server"></asp:Label> </td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Carrera Profesional</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEscuela" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Apellidos y Nombres</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstAlumno" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">DNI</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDocIdent" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Fecha Nacimiento</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstFechaNac" runat="server"></asp:Label></td>
                   </tr>                   
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmail" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tel&eacute;fono</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTelefono" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Estado Actual</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEstado" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tiene Deuda</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTieneDeuda" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Beneficio de Beca</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstBeneficio" runat="server"></asp:Label></td>
                   </tr>
                      <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Responsable de Pago</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstRespPago" runat="server"></asp:Label></td>
                   </tr>
                      <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Direcci&oacute;n responsable de pago</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDirRespPago" runat="server"></asp:Label></td>
                   </tr>
                   </table>
                </div>             
            </div>
        </div>
                       <div class="col-md-6 panel" id="ifrAccion" runat="server">
            <div id="Div3" class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N DEL TR&Aacute;MITE</div>
                <div class="panel-body">
               
                    <div class="row">
                                            <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;">
                                    Tr&aacute;mite:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblTramite" runat="server" CssClass="form-control" Enabled="false" Height="80px" ></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" for="lblTramiteDescripcion">
                                   Descripci&oacute;n Tr&aacute;mite:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblTramiteDescripcion" runat="server" CssClass="form-control" Height=140px  Enabled=false></asp:Label>
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
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;"  id="lblInfoAdicional" runat="server">
                                    Informaci&oacute;n adicional de la solicitud del tr&aacute;mite:</label>
                                <div class="col-md-12">
                                <div class="table-responsive">
                                <asp:GridView ID="gDatosAdicional" runat="server" AutoGenerateColumns="False" DataKeyNames="tabla,valorcampo"
                                    class="table table-bordered bs-table" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="INFORMACIÓN" DataField="tabla" />
                                        <asp:BoundField HeaderText="DETALLE" DataField="valorcampo" />
                                        <asp:BoundField HeaderText="OBSERVACION ADICIONAL" DataField="observacion" />
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
                    <div class="row">
                                <div class="panel" id="ifrRetCiclo" runat="server">
                                        <div id="Div4" class="panel panel-default" runat="server">
                                        <div class="panel-heading">
                                         <i class="glyphicon glyphicon-calendar"></i>  ULTIMA FECHA ASISTENCIA </div> 
                                        <div class="panel-body">
                                            <div class="row">                                        
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                                <label class="control-label" for="txtUltimaFechaAsistencia" style="margin-left:15px">Fecha:  <i class="glyphicon glyphicon-pencil"></i> </label>
                                                                <div class="col-md-12">
                                                                <asp:TextBox ID="txtUltimaFechaAsistencia" runat="server" CssClass="form-control" placeholder="dd/mm/yyyy" Enabled=false></asp:TextBox>
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
                            </div>
                </div>
            </div>
        </div>
                </div>
                <div class="row">
                <div class="panel-body">
                <div class="row">
                    <div id="Div5"  class="col-md-12 panel"  runat="server">
                        <div id="Div6"   class="panel panel-default" runat="server">
                            <div class="panel-heading" style="font-weight:bold">
                                EVALUADORES DEL TRÁMITE</div>
                    
                      <div class="panel-body">
                        <div class="form-group">
                            <div class="table-responsive">
                            
                            <asp:GridView ID="gvFlujo" runat="server" Width="100%" DataKeyNames="codigo_dta"
                                    CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" ShowHeader="true" OnRowDataBound = "OnRowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="personal" HeaderText="EVALUADORES" />
                                        <asp:BoundField DataField="descripcion_Tfu" HeaderText="PERFIL DE EVALUADOR" />
                                        <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                        <asp:BoundField DataField="estadoAprobacion" HeaderText="RESULTADO" />
                                        <asp:BoundField DataField="fechaModestado_dft" HeaderText="FECHA DE EVALUACIÓN" /> 
                                    
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
             </div>
           </div>
                
                </div>
             </div>
             <div class="panel-footer">
             <asp:Button ID="btnCerrar" runat="server" Text="Salir" class="btn btn-danger" />
             </div>
           </div>    
           <asp:HiddenField ID="hdtimelineactive" runat="server" />
                        <asp:HiddenField ID="hdtimelinechk" runat="server" />
                        <asp:HiddenField ID="hddtareq" runat="server" />
     </div>
    </form>
</body>
</html>
