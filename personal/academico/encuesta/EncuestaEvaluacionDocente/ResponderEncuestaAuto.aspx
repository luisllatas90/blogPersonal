<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResponderEncuestaAuto.aspx.vb" Inherits="academico_encuesta_EncuestaEvaluacionDocente_ResponderEncuestaAuto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="utf-8" />            
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Responder Auto-Evaluación Docente</title>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />    
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>  
    <script type="text/javascript" src="../../../assets/js/jquery.dataTables.min.js?x=1"></script>	
    <link rel='stylesheet' href='../../../assets/css/jquery.dataTables.min.css?z=1'/>
    
    
    <script type="text/javascript" language="javascript">
        var event = jQuery.Event("DefaultPrevented");
        $(document).trigger(event);
        $(document).ready(function() {

        var oTable = $('#gvPreguntas').DataTable({
                //"sPaginationType": "full_numbers",
                "bPaginate": false,
                "bFilter": false,
                "bLengthChange": false,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true
            });
        });

        function ShowMessage(message, messagetype) {

            switch (messagetype) {
                case '1':

                    $('#alert_container').html('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"> <span>' + message + '</span></div><br>');
                    break;
                case '2':
                    cssclass = 'alert-danger';
                    tipo = "danger";
                    $('#alert_container').html('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"> <span>' + message + '</span></div><br>');
                    break;
                case '3':
                    $('#alert_container').html('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-warning"> <span>' + message + '</span></div><br>');
                    break;
                default:
                    $('#alert_container').html('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-info"><span>' + message + '</span></div><br>');
            }

        }
        
        
        
        </script>
        
        
   
    
 <style type="text/css" >
        body
        {
        font-family: Calibri;
        }
        .botonizquierdo{float:left; margin-bottom:5px;}
        .botonderecho{float:right;margin-bottom:5px;}
        
        .radiorespuesta input
        {
         width:50px; 
         height:50px; 
        }
        .radionormal input
        {
         width:25px; 
         height:25px; 
        }     
    </style>
    </head>
    
    
<body>
    <form id="form1" runat="server">
    <br />
    <div class="container-fluid"> 
               
           
        <div class="panel panel-default" id="panelCabecera" runat="server">
                <div class="panel-heading" style="font-weight:bold;">AUTOEVALUACIÓN DEL DESEMPEÑO DOCENTE</div>
                <div class="panel-body">                 
                  <div class="row">                  
                      <div class="col-md-12">
                                    <div class="form-group">                                
                                        
                                       <div class="col-md-1">
                                            <center>
                                            <asp:Image ID="imagePer" runat="server" Height="100px" Width="80px" />
                                            </center>
                                        </div>
                                        
                                        <div class="col-md-11">
                                            
                                            <div class="row">
                                                <div class="col-md-12">
                                                  <center>
                                                   Semestre <asp:Label ID="lblSemestre" runat="server" Text=""></asp:Label>
                                                   </center>
                                                </div>
                                            </div>
                                                
                                            <div class="row">
                                                <div class="col-md-12">
                                                <center>
                                                    <asp:Label ID="lblDocente" runat="server" Text="" Font-Bold=true></asp:Label>
                                                    </center>
                                                </div>
                                            </div>
                                            <div class="row"> 
                                                <div class="col-md-12">
                                                <center>
                                                  <asp:Label ID="lblDedicacion" runat="server" Text=""></asp:Label>
                                                </center>
                                                </div>
                                            </div> 
                                            <div class="row"> 
                                                <div class="col-md-12">
                                                <center>
                                                  <asp:Label ID="lblDpto" runat="server" Text=""></asp:Label>
                                                </center>
                                                </div>
                                            </div>                                                                                                                                                               
                                        </div>                                                                                                         
                                    </div>                                                          
                                    
                      </div>        
      
                  </div>    
               </div>                                
        </div>  
        <div class="messagealert" id="alert_container">
         </div>    
        <div class="panel panel-default"  id="panelCursos" runat="server">
                <div class="panel-heading">CURSOS PARA AUTOEVALUACIÓN</div>
              <div class="panel-body"> 
                  <div class="row">
                          <asp:GridView ID="gvCursos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_cup" 
                                    GridLines="Horizontal" 
                                    CssClass="table table-bordered bs-table datatable">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_cup" 
                                            Visible="False" />
                                        
                                        <asp:BoundField DataField="nombre_cpf" SortExpression="nombre_cpf" 
                                            HeaderText="CARRERA PROFESIONAL">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="nombre_cur" SortExpression="nombre_cur" 
                                            HeaderText="ASIGNATURA">
                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                        </asp:BoundField> 
                                        
                                         <asp:BoundField DataField="estado" SortExpression="estado" 
                                            HeaderText="ESTADO">
                                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                       
                                      
                                        <asp:CommandField SelectText=" Responder " ShowSelectButton="True" HeaderText="ACCIÓN">
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
        </div>              
        <div class="panel panel-default" runat="server" id="PanelRpta" visible="false">
            <div class="panel-heading">RESPONDER AUTOEVALUACIÓN - ASIGNATURA: <b><asp:Label ID="lblNombrecurso" runat="server" Text=""></asp:Label></b></div>                      
             <center>  
              <div class="panel-body"> 
                  <div class="row">
                  <div class="table-responsive">
                          <asp:GridView GridLines="Horizontal" ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" Width="30%">
                          <Columns>
                                <asp:BoundField DataField="1" HeaderText="NUNCA">                            
                                <HeaderStyle BackColor="#FF6666" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="2" HeaderText="LA MAYORÍA DE VECES NO">                            
                                <HeaderStyle BackColor="#FFFF66" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" />
                                </asp:BoundField>
                               <asp:BoundField DataField="3" HeaderText="ALGUNAS VECES SÍ, ALGUNAS VECES NO">                            
                                <HeaderStyle BackColor="#009900" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" ForeColor="White" />
                                </asp:BoundField>
                               <asp:BoundField DataField="4" HeaderText="LA MAYORÍA DE VECES SÍ">                            
                                <HeaderStyle BackColor="#0066FF" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" ForeColor="White" />
                                </asp:BoundField>
                               <asp:BoundField DataField="5" HeaderText="SIEMPRE">                            
                                <HeaderStyle BackColor="#CC3300" HorizontalAlign="Center" 
                                    VerticalAlign="Middle" ForeColor="White" />
                                </asp:BoundField>
                          </Columns>
                          <HeaderStyle BackColor="#C3C3C3" ForeColor="Black" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="11px" />
                          <RowStyle Font-Size="11px"  VerticalAlign="Middle" HorizontalAlign="Center"  />                                            
                          
                      </asp:GridView>
                    
                    </div>
                  </div>
               </div>
             </center>
             <center>     
             <div class="panel-body">
               <div class="row" >
                  <div class="col-md-6" style="float:right">                        
                         <asp:Button ID="btnGuardar" runat="server" Text="Registrar" CssClass="botonderecho btn btn-success" />                                                       
                  </div>
                </div> 
             </div>   
             </center>  
             <div class="row">
                      <div class="col-md-12">
                          <div class="table-responsive">
                         
                                <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="codigo_eva,conrespuesta_eva,orden_eva,codigo_crit,codigo_comp" 
                                HorizontalAlign="Center" CssClass="display">
                                <Columns>
                                <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                                <ItemStyle Width="15px" Font-Bold=true />
                                </asp:BoundField>
                                <asp:BoundField DataField="pregunta_eva" HeaderText="Items " >
                                <ItemStyle HorizontalAlign="Left" Width="90%" />
                                </asp:BoundField>                                                    

                                <asp:TemplateField HeaderText=""> 
                                
                                <ItemTemplate>                                 
                                <center>
                                   
                                    <asp:RadioButton ID="rbUno" GroupName="opciones" runat="server" CssClass="radiorespuesta" />
                                </center>                                     
                               </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <center>
                                       
                                        <asp:RadioButton ID="rbDos" GroupName="opciones" runat="server" CssClass="radiorespuesta" />
                                    </center>
                                </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <center>
                                     
                                        <asp:RadioButton ID="rbTres" GroupName="opciones" runat="server" CssClass="radiorespuesta" />                                     
                                    </center>
                                </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <center>
                                
                                        <asp:RadioButton ID="rbCuatro"  GroupName="opciones" runat="server" CssClass="radiorespuesta" />
                                      
                                    </center>
                                </ItemTemplate> 
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <center>
                                
                                        <asp:RadioButton ID="rbCinco" GroupName="opciones" runat="server" CssClass="radiorespuesta" />
                                      
                                    </center>
                                </ItemTemplate> 
                                </asp:TemplateField>
                                
                                
                                </Columns>
                                <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" 
                                                             Font-Bold="True" ForeColor="Red" />
                                                        <EmptyDataTemplate>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; No hay registros
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle BackColor="#E8EEF7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                            BorderWidth="1px" ForeColor="#3366CC" />
                                                        <SelectedRowStyle BackColor="#6699FF" ForeColor="White" />
                                                        
                                                                                     
                                </asp:GridView>
                                
                                   
                  
                          </div>
                          
                          
                      </div>
                  </div>                                     
             <div class="row">
                        <div class="col-md-6" style="float:right">
                        <br />
                             <asp:Button ID="btnGuardar2" runat="server" Text="Registrar" CssClass="botonderecho btn btn-success" />                                                       
                        </div>
                  </div>  
                
    </div>                                                                        
  </form>
</body>
</html>
