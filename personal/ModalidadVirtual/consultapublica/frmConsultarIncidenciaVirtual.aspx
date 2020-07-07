<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultarIncidenciaVirtual.aspx.vb" Inherits="ModalidadVirtual_consultapublica_frmConsultarTramiteVirtual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar de Modalidad Virtual</title>
    <link href="../../../private/estilo.css?z=x" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>     
    <script src='../../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />  
    <link href="../private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <link href="../private/timeline.css" rel="stylesheet" type="text/css" />
    <script src="../private/jquery.loadmask.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../assets/js/jquery.dataTables.min.js?y=1"></script>	
    <link rel='stylesheet' href='../../../assets/css/jquery.dataTables.min.css?z=1'/>
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

     
        function fnDescargar(id_ar) {
            var d = new Date();
            window.open("../../DescargarArchivo.aspx?Id=" + id_ar + "&tk=7ZLQU933BB&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
        </script>
    
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
     <div class="container-fluid">     
        <div class="messagealert" id="alert_container">
        </div>
        
           <div class="panel panel-primary" id="pnlLista" runat="server">
            <div class="panel-heading" style="text-align:center">
                CONSULTAS SOBRE CLASES VIRTUALES
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-8">
                        <div class="form-group">
                            <label for="txtAlumno" class="col-md-5 col-form-label">
                                Cod. Univ./Apellidos y Nombres/DNI:</label>                                
                                <div class="col-md-7">
                                <asp:TextBox ID="txtAlumno" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="txtnroTramite" class="col-md-5 col-form-label">
                            N&deg; de Consulta:
                            </label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtnroTramite" runat="server" CssClass="form-control" ></asp:TextBox>
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
                        
                         <label class="col-sm-5 control-label">
                                                    Fecha Inicio:</label>
                                                <div class="col-sm-7" id="Div2">
                                                    <div class="input-group date" id="FechaInicio">
                                                        <input name="txtfeciniTes" class="form-control" id="txtfeciniTes" style="text-align: right;"
                                                            type="text" placeholder="__/__/____" data-provide="datepicker" autocomplete=off runat="server" >
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                        </i></span>
                                                    </div>
                                                </div>
                                            
                        </div>
                </div>
                 <div class="row">
                 <div class="col-md-8">
                            <div class="btn-group right">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info" />
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" class="btn btn-success" />                       
                        </div>
                                             
                     
                 </div>  
                      <div class="col-md-4">
                                       <label class="col-sm-5 control-label">
                                                    Fecha Fin:</label>
                                                <div class="col-sm-7" id="date-popup-group">
                                                    <div class="input-group date" id="FechaFin">
                                                        <input name="txtfecfinTes" class="form-control" id="txtfecfinTes" style="text-align: right;"
                                                            type="text" placeholder="__/__/____" data-provide="datepicker" autocomplete=off runat="server">
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                        </i></span>
                                                    </div>
                                                </div>
                                            </div>            
                         
                                 
                 </div>   
            </div>
             <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="table-responsive">
                            
                            <asp:GridView ID="gvDatos" runat="server" Width="100%" DataKeyNames="codigo_inc,codigo_Alu,codigoUniver_Alu,asunto_inc,descripcion_Tfu"
                                    CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" ShowHeader="true">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_inc" HeaderText="ID" />
                                        <asp:BoundField DataField="glosaCorrelativo_inc" HeaderText="Nro. CONSULTA" />
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                                        <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />                                        
                                        <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                                        <asp:BoundField DataField="asunto_inc" HeaderText="ASUNTO" />
                                        <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                        <asp:BoundField DataField="descripcion_tfu" HeaderText="INSTANCIA" />
                                        
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPERACION">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEvaluar" runat="server" Text="Seguimiento"  CssClass="btn btn-success btn-sm" CommandName="Evaluar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />                                               
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
                Seguimiento de Consulta sobre Clases Virtuales
            </div>
             <div class="panel-body">
                <div class="row">
                
                       <div class="col-md-6 panel"  runat="server">
                 <div  class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N DEL ESTUDIANTE</div>
                <div class="panel-body">
                   <table style="width:100%" class="table table-bordered bs-table" >
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">ID CONSULTA</th>
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
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Email</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmail" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">EmailUsat</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEmailUsat" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tel&eacute;fono</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTelefono" runat="server"></asp:Label></td>
                   </tr>
                    <tr>
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Estado Actual</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstEstado" runat="server"></asp:Label></td>
                   </tr>
                    <tr style="display:none;">
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Tiene Deuda</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstTieneDeuda" runat="server"></asp:Label></td>
                   </tr>
                   <tr style="display:none">
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Beneficio de Beca</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstBeneficio" runat="server"></asp:Label></td>
                   </tr>
                      <tr style="display:none">
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Responsable de Pago</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstRespPago" runat="server"></asp:Label></td>
                   </tr>
                      <tr style="display:none">
                   <th style="background-color:AliceBlue; font-weight:bold;width:30%;font-size:10pt">Direcci&oacute;n responsable de pago</th>
                   <td style="background-color:Linen; font-size:10pt"><asp:Label ID="lblInfEstDirRespPago" runat="server"></asp:Label></td>
                   </tr>
                   </table>
                </div>             
            </div>
        </div>
        <div class="col-md-6 panel" id="ifrAccion" runat="server">
            <div id="Div1" class="panel panel-default" runat="server">
                <div class="panel-heading" style="font-weight:bold">
                    INFORMACI&Oacute;N DE CONSULTA</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;">
                                    Fecha:</label>
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
                                    Asunto:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblTramite" runat="server" CssClass="form-control" Enabled="false" ></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" for="lblTramiteDescripcion">
                                   Descripci&oacute;n:</label>
                                <div class="col-md-12">
                                    <asp:Label ID="lblTramiteDescripcion" runat="server" CssClass="form-control" Height="120px"  Enabled=false></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label" style="margin-left:10px;" for="lblArchivo">
                                   Archivo Adjunto:</label>
                                <div class="col-md-12">
                                    <asp:LinkButton runat="server" ID="lblArchivo"></asp:LinkButton>
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
                    <div  class="col-md-12 panel"  runat="server">
                        <div   class="panel panel-default" runat="server">
                            <div class="panel-heading" style="font-weight:bold">
                                EVALUADORES DE CONSULTA</div>
                    
                      <div class="panel-body">
                        <div class="form-group">
                            <div class="table-responsive">
                            
                            <asp:GridView ID="gvFlujo" runat="server" Width="100%" DataKeyNames="codigo_inc"
                                    CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" ShowHeader="true" OnRowDataBound = "OnRowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="personal" HeaderText="EVALUADORES" />
                                        <asp:BoundField DataField="descripcion_Tfu" HeaderText="PERFIL DE EVALUADOR" />
                                        <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                        <asp:BoundField DataField="respuesta_din" HeaderText="MENSAJE" />
                                        <asp:BoundField DataField="fecha_timeline" HeaderText="FECHA DE EVALUACIÓN" /> 
                                    
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
