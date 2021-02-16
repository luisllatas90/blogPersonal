<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registro.aspx.vb" Inherits="academico_estudiante_separacion_Separacion" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=8; IE=9; " />-->
    <title>Registro de Justificación</title>
     <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>    
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Errors':
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

        function PintarFilaMarcada2(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "#F7F6F3"
            }
        }

        function MarcarTodos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada2(chk.parentNode.parentNode, obj.checked)
                    }
                }
            }
        }
    </script>

   

    </head>
 
<body>
    <form id="form1" class="form form-horizontal"  runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Estamos procesando su solicitud..." Title="Por favor espere" />
    <br />
     <div class="container-fluid">
        <div class="panel panel-default">
              <div class="panel-heading">Trámites de Justificación de Inasistencia</div>
              <div class="panel-body">
                <asp:GridView ID="GvAlumnos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_alu, codigo_pes, codigo_trl,observacion_trl,codigo_dta,fechaIni,fechaFin" 
                                    GridLines="Horizontal" 
                                    CssClass="table table-bordered bs-table datatable">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_Alu" HeaderText="Codigo_Alu" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="Codigo_Alu" 
                                            Visible="False" />
                                        <asp:BoundField DataField="codigo_trl" HeaderText="codigo_trl" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_pes" HeaderText="codigo_pes" Visible="False">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaReg_trl" HeaderText="FECHA REG">
                                             <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="TRÁMITE">
                                             <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>                                        
                                        
                                        <asp:BoundField DataField="Codigouniver_alu" 
                                            SortExpression="Codigouniver_alu" HeaderText="COD. UNIV.">
                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombres" HeaderText="APELLIDOS Y NOMBRES" 
                                            SortExpression="nombres" ReadOnly="True" >
                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_cpf" SortExpression="nombre_cpf" 
                                            HeaderText="CARRERA PROFESIONAL">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="observacion_trl" SortExpression="Obs_trl" 
                                            HeaderText="obs" Visible="false">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_dta" SortExpression="codigo_dta" 
                                            HeaderText="codigo_dta" Visible="false">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="EstadoJusti" SortExpression="EstadoJusti" 
                                            HeaderText="JUSTIFICACIÓN">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                                                              
                                        
                                        <asp:CommandField SelectText=" Seleccionar " ShowSelectButton="True" HeaderText="ACCIÓN">
                                            <ItemStyle Width="1px" />
                                        </asp:CommandField>                                        
                                    </Columns>
                                      <EmptyDataTemplate>0 registros!</EmptyDataTemplate>
                         <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                            Font-Size="11px" />
                        <RowStyle Font-Size="11px" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                            
                                    <SelectedRowStyle BackColor="#FFFFD2" />
                                   
                                </asp:GridView>
              </div>
        </div>
        
          <div class="panel panel-default">
              <div class="panel-heading">Detalle de Trámite</div>
              <div class="panel-body">
             
                <div class="row col-md-12">
              
                    <div class="col-md-6">
                      
                            <div class="col-md-4">
                             <asp:Image ID="ImgFoto" CssClass="form-control" runat="server" Height="150px" Width="120px" BorderWidth="1px" />                            
                            </div>
                            
                             <div class="col-md-8">
                             <div class="form-group">
                            
                                <label for="lblMotivo" class="col-md-5">Nro Trámite</label>
                                <div class="col-md-7">
                                <asp:Label ID="LblEstado" runat="server"></asp:Label>
                                <asp:HiddenField ID="lbldta" runat="server" /></asp:label>
                                </div>
                                </div>
                            </div>
                            
                            <div class="col-md-8">
                             <div class="form-group">
                                <label for="LblCodigoUniv" class="col-md-5"> Código universitario:</label>
                               
                                
                                <div class="col-md-7">
                                <asp:Label ID="LblCodigoUniv" runat="server"  ></asp:Label>
                                </div>
                                </div>
                            </div>
                            <div class="col-md-4"></div>
                            <div class="col-md-8">
                             <div class="form-group">
                                <label for="LblNombres" class="col-md-5">Apellidos y nombres</label>
                                <div class="col-md-7">
                              <asp:Label ID="LblNombres" runat="server"></asp:Label></bR><asp:Label ID="lblPlanEstudio" runat="server"></asp:Label>
                                </div>
                                </div>
                            </div>
                            <div class="col-md-4"></div>
                            
                        
                    </div>
                
                
                <div class="col-md-6" style="display:none" >
                        <div class="form-group"> 
                            <label for="txtDescripcion" class="col-md-4">Descripción</label>
                            <div class="col-md-8"><asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine"  Enabled="false" ReadOnly="True" Width="453px"></asp:TextBox></div>                        
                        </div>
                        <div class="form-group"> 
                            <label for="lblMotivo" class="col-md-4">Motivo</label>
                            <div class="col-md-8"> <asp:Label ID="lblMotivo" runat="server"></asp:Label></div>
                        </div>
                       <div class="form-group"> 
                                <label for="txtObs" class="col-md-4">Observación</label>
                                <div class="col-md-8"><asp:TextBox ID="txtObs" runat="server" TextMode="MultiLine" Enabled="false" ReadOnly="True" 
                                                    Width="536px"></asp:TextBox></div>
                       </div>
                          
                    </div>
                </div>                
              </div>
             
          </div>
           
                        
    <br />
    
    
    <div class="panel panel-default">
              <div class="panel-heading">Sesiones de Asistencia</div>
              <div class="panel-body">
                <asp:TextBox ID="txtDesde" runat="server" Text="01/11/2018"></asp:TextBox>
                 <asp:TextBox ID="txtHasta" runat="server" Text="15/12/2018"></asp:TextBox>
                  <asp:Button ID="btnBuscar" runat="server" Text="Buscar Sesiones"  CssClass="btn btn-info"/>
                  &nbsp;&nbsp;
                  <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Justificación" CssClass=" btn btn-success" />
                 
                  </br>
                    <div id="alert_container" class="messagealert">
                  </div>
                    </br>
                  <asp:GridView ID="gridSesiones" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_cup,codigo_cac,codigo_lho,fechaclase,codigo_justi" CssClass="table table-bordered bs-table datatable">
                            <Columns>
                                <asp:TemplateField HeaderText="">    
                                 <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarTodos(this)" />
                                 </HeaderTemplate>                                       
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkElegir" runat="server" 
                                            onclick="PintarFilaMarcada2(this.parentNode.parentNode,this.checked)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="dia_lho" HeaderText="DÍA" />
                                <asp:BoundField DataField="fechaclase" HeaderText="FECHA SESIÓN" />
                                <asp:BoundField DataField="nombre_hor" HeaderText="HORA INICIO" />
                                <asp:BoundField DataField="horafin_lho" HeaderText="HORA FIN" />
                                <asp:BoundField DataField="curso" HeaderText="CURSO MATRICULADO" />
                                <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" 
                                    visible="false" />
                                <asp:BoundField DataField="codigo_cac" HeaderText="codigo_cac" 
                                    visible="false" />
                                <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" 
                                    visible="false" />
                                <asp:BoundField DataField="codigo_justi" HeaderText="codigo_justi" Visible=false />
                            </Columns>
                            <EmptyDataTemplate>0 registros!</EmptyDataTemplate>
                         <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                            Font-Size="11px" />
                        <RowStyle Font-Size="11px" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                            
                                    <SelectedRowStyle BackColor="#FFFFD2" />
                  </asp:GridView>
                        
              </div>
              
</div>              
    
   </div>
    <asp:HiddenField ID="hdMotivo" runat="server" />
    </form></body></html>