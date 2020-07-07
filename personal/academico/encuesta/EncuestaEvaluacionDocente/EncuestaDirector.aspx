<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaDirector.aspx.vb" Inherits="academico_encuesta_EncuestaEvaluacionDocente_EncuestaDirector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>            
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Evaluación Docente</title>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />    
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>  
    <script type="text/javascript" src="../../../assets/js/jquery.dataTables.min.js?x=1"></script>	
    <link rel='stylesheet' href='../../../assets/css/jquery.dataTables.min.css?z=1'/>
   
    <script type="text/javascript" language="javascript">
         var event = jQuery.Event("DefaultPrevented");
             $(document).trigger(event);
             $(document).ready(function() {

             var oTable = $('#gvData').DataTable({
                 //"sPaginationType": "full_numbers",
                 "bPaginate": false,
                 "bFilter": true,
                 "bLengthChange": false,
                 "bSort": false,
                 "bInfo": true,
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
   
    <style type="text/css">
       body
       {
           font-family: Calibri;
           }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div class="container-fluid">       
     
     <div class="messagealert" id="alert_container">
     </div>
     
     <div class="panel panel-default" id="pnlLista" runat="server">
                <div class="panel-heading" style="font-weight:bold;">Evaluación del Desempeño Docente - Guía del <asp:Label ID="lblTipo" Font-Bold=true runat="server" Text="Label"></asp:Label></div>
                <div class="panel-body"> 
                
                  <div class="row">
                  
                  <div class="col-md-12">
                                <div class="form-group">
                                
                                <label for="lblSemestreAcademico" class="col-md-4 col-form-label">
                                    Semestre Académico
                                </label>
                                
                                <div class="col-md-8">
                                 <asp:Label ID="lblSemestreAcademico" runat="server" Text=""></asp:Label>
                                </div>
                                
                                </div>                                                          
                  </div>
                  
                  </div>
                  
                  <div class="row">
                  <div class="col-md-12">
                                <div class="form-group">
                                
                                <label for="lblCarrera" class="col-md-4 col-form-label">
                                   <asp:Label ID="lblTituloTipo" runat="server" Text=""></asp:Label>
                                </label>
                                
                                <div class="col-md-8">
                                 <!--<asp:Label ID="lblCarrera" runat="server" Text=""></asp:Label>-->
                                  <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                
                                </div>                                                          
                  </div>
                  
                  </div>
                  
                  <div class="row">
                  <div class="col-md-12">
                                <div class="form-group">
                                
                                <label for="ddlEstado" class="col-md-4 col-form-label">
                                  Filtrar
                                </label>
                                
                                <div class="col-md-8">
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                                <asp:ListItem Value="%">TODOS</asp:ListItem>
                                <asp:ListItem Value="E">EVALUADOS</asp:ListItem>
                                <asp:ListItem Value="P" Selected="True">POR EVALUAR</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                
                                </div>                                                          
                  </div>
                  
                  </div>
                  
                
                   <div class="row" style="display:none">
                  <div class="col-md-12">
                                <div class="form-group">
                                
                                <label for="txtDocente" class="col-md-4 col-form-label">
                                  Buscar Docente
                                </label>
                                
                                <div class="col-md-8">
                                 <asp:TextBox ID="txtDocente" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                
                                </div>                                                          
                  </div>
                  
                  </div>
                  
                
                </div>
                <div class="panel-footer">
                
                    
                  <div class="row">
                  <div class="col-md-12">
                        <center>
                            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-primary" />                        
                        </center>
                  </div>
                  </div>
                
                </div>
                
                <div class="panel-body">
                  <div class="row">
                  <div class="col-md-12">
                  <div class="table-responsive">
                     <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_per, docente, descripcion_ded" CssClass="display">
                        <Columns>
                            <asp:BoundField DataField="codigo_per" HeaderText="codigo_per" Visible=false />
                            <asp:BoundField DataField="nrocursos" HeaderText="Nro" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="docente" HeaderText="Docente" />
                            <asp:BoundField DataField="estado" HeaderText="Estado" />
                            <asp:TemplateField HeaderText="Acción" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="btnEvaluar" runat="server" Text="Evaluar" cssclass="btn btn-success "
                                     CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                     CommandName="Evaluar" ImageUrl="~/administrativo/pec/image/Asol.png" 
                                     
                                     />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" 
                                                            CssClass="usatsugerencia" Font-Bold="True" ForeColor="Red" />
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
                  
                </div>
    </div>
                
    
    
        
    
    </div>
    </form>
</body>
</html>
