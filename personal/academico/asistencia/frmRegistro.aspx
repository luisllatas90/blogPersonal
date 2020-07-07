<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistro.aspx.vb" Inherits="academico_asistencia_frmRegistro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8"/>            
  <meta name="viewport" content="width=device-width, initial-scale=1" />
  <title></title>
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="https://jqueryui.com/resources/demos/style.css">
<%--
     <script src="jquery/jquery-1.10.2.js" type="text/javascript"></script>
  <link rel="stylesheet" href="jquery/jquery-ui.css"> 

  <script src="jquery/jquery-ui.js" type="text/javascript"></script>
  <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>  --%>
    
   
    
   <script type="text/javascript" language="javascript">
        
        var event = jQuery.Event("DefaultPrevented");
        $(document).trigger(event);
        $(document).ready(function() {
            console.log($('#gvData'));

            var $gvData = $('#gvData');
            if ($gvData.find('thead').length>0){
                var oTable = $('#gvData').DataTable({
                    //"sPaginationType": "full_numbers",
                    "bPaginate": true,
                    "bFilter": true,
                    "bLengthChange": false,
                    "bSort": true,
                    "bInfo": true,
                    "bAutoWidth": true
                });   
            }

            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtFechaAsistencia").datepicker({
                firstDay: 0
            });
        });

        function MarcarCursos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                    }
                }
            }
        }


        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
      
    </script>
    <style type="text/css">
       
        #txtFechaAsistencia
        {
          
            background-color: #CCCCFF;
        }

        #gvData .btn {
            background-color: #ff2f2f;
            color: white;
            margin: 5px 0px;
        }

        #gvData .btn:hover {
            background: #e40202;
        }

        #gvData .btn:focus {
            outline: none;
            background: #ce0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div class="container-fluid">     
 
        <div class="panel panel-default" id="pnlLista" runat="server">
                        
              <div class="panel-heading" style="font-weight:bold;">Lista de Asistencia del Proceso de Admisión</div>
                        
                <div class="panel-body"> 
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">            
                                <label for="ddlTipoAsistencia" class="col-md-4 col-form-label">Tipo de Asistencia</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlTipoAsistencia" runat="server" AutoPostBack="True"></asp:DropDownList>&nbsp;
                                    <asp:DropDownList ID="ddlEstado" runat="server">
                                    <asp:ListItem Value="%">Todos</asp:ListItem>
                                    <asp:ListItem Selected="True">Pendientes</asp:ListItem>
                                    <asp:ListItem Value="R">Registrados</asp:ListItem>
                                    </asp:DropDownList>
                                </div>                                                                                                                              
                            </div>                                                          
                        </div>                  
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">            
                                <label for="ddlSemestre" class="col-md-4 col-form-label">Semestre de Ingreso</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSemestre" runat="server"></asp:DropDownList>
                                </div>                                                                                                                              
                            </div>                                                          
                        </div>                  
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">            
                                <label for="ddlCarrera" class="col-md-4 col-form-label"> Carrera Profesional</label>
                                <div class="col-md-8">
                                   <asp:DropDownList ID="ddlCarrera" runat="server"></asp:DropDownList>
                                </div>                                                                                                                              
                            </div>                                                          
                        </div>                  
                    </div>  
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">            
                                <label for="ddlModalidad" class="col-md-4 col-form-label"> Modalidad de Ingreso</label>
                                <div class="col-md-8">
                                   <asp:DropDownList ID="ddlModalidad" runat="server"></asp:DropDownList>
                                </div>                                                                                                                              
                            </div>                                                          
                        </div>                  
                    </div> 
 
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">            
                                <label for="txtTextoBuscar" class="col-md-4 col-form-label"> Nombre / Apellidos /D.N.I.</label>
                                <div class="col-md-8">
                                  <asp:TextBox ID="txtTextoBuscar" runat="server" Width="295px"></asp:TextBox>
                                  <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" />
                                </div>                                                                                                                              
                            </div>                                                          
                        </div>                  
                    </div>
                                                         
                </div>
                <div class="panel-footer"> 
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">            
                                <label for="txtFechaAsistencia" class="col-md-4 col-form-label">Ingresar Fecha de Asistencia</label>
                                <div class="col-md-8">
                                    <input ID="txtFechaAsistencia" runat="server" type="text" />
                                     <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-success" Visible="False" />
                                     <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-success" Visible="False"   />
                    
                                </div>                                                                                                                              
                            </div>                                                          
                        </div>                  
                    </div>
                </div>
                
                <div class="panel-body">
                  <div class="row">
                  <div class="col-md-12">
                  <div class="table-responsive">
                
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="No se encontraron registros" DataKeyNames="codigo_alu"  BorderStyle="None"
             AlternatingRowStyle-BackColor="#F7F6F4" >
                <RowStyle BorderColor="#C2CFF1" />
                <Columns>

                <asp:TemplateField>
                    <HeaderTemplate>
                    <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                    </HeaderTemplate>

                    <ItemTemplate>
                    <asp:CheckBox ID="chkElegir" runat="server" />
                    </ItemTemplate> 
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>  

                <asp:BoundField HeaderText="Nro" />
                <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" Visible="False" />
                <asp:BoundField DataField="codigouniver_alu" HeaderText="Cód. Univ" />
                <asp:BoundField DataField="alumno" HeaderText="Estudiante" />
                <asp:BoundField DataField="nroDocIdent_alu" HeaderText="DNI" />
                <asp:BoundField DataField="cicloing_alu" HeaderText="Semestre Ing." />
                <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" />
                <asp:BoundField DataField="nombre_min" HeaderText="Modalidad Ingreso" />
                <asp:BoundField DataField="fecha_asi" HeaderText="Fecha Asistencia" />
                <asp:BoundField ItemStyle-HorizontalAlign="Center" HeaderText="Opciones" />
                </Columns>
                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#E7EFF7" />
                </asp:GridView>
                  </div>
                  </div>
                  </div>
              </div>
                  
               
        </div>



         
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>  
    <script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
</body>
</html>
