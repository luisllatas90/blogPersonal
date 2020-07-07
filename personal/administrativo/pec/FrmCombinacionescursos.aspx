<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCombinacionescursos.aspx.vb"
    Inherits="administrativo_pec_FrmCombinacionescursos" %>

<!DOCTYPE html>
<html lang="es">
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Combinaci&oacute;n</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/material.css'/>
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="../../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>
    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../assets/js/materialize.js'></script>    

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {

        $('#txtdetnumero').keypress(function(event) {
            if (event.which == 13) {
                event.preventDefault();
                document.getElementById("<%=btnDetAgregar.ClientID %>").click();                
            }
        });
       
        });

        function openModal(acc, des) {
            $('#modalComite').modal('show');

            if (acc == 'nuevo') {
                $('#txtNombre').val(des);

                $("#spnFile").empty();
                $("#spnFile").text("No se eligió resolución");
            }
        }

        function closeModal(confirm) {
            if (confirm) {
                $('#modalComite').modal('hide');
                $("#modalComite").remove();

                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                $(".modal-dialog").remove();
                $(".modal").remove();
            }
        }

        function mostrarMensaje(mensaje, tipo) {
            var backcolor = "#64C45C";
            var forecolor = "#FFFFFF";

            if (tipo == "danger") {
                backcolor = "#FC7E7E";
                forecolor = "#FFFFFF";
            } else if (tipo == "warning") {
                backcolor = "#FFDC96";
                forecolor = "#000000";
            }

            var box = bootbox.alert({ message: mensaje, backdrop: true });
            box.find('.modal-body').css({ 'background-color': backcolor });
            box.find('.bootbox-body').css({ 'color': forecolor });
            box.find(".btn-primary").removeClass("btn-primary").addClass("btn-" + tipo);
        }

        function ShowMessage(message, messagetype) {
            var cssclss;
            switch (messagetype) {
                case 'Success':
                    cssclss = 'alert-success'
                    break;
                case 'Error':
                    cssclss = 'alert-danger'
                    break;
                case 'Warning':
                    cssclss = 'alert-warning'
                    break;
                default:
                    cssclss = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert ' + cssclss + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }
       
    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
    <asp:HiddenField ID="hf" runat="server" />
    <asp:HiddenField ID="hcomb" value="0" runat="server" />
    <asp:HiddenField ID="hcombdet" value="0" runat="server" />
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    
    <div class="container-fluid row">
    
        <div class="messagealert" id="alert_container">
        </div>
       <div class="col-md-12" id="divListarCombinacion" runat="server">
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>Programaci&oacute;n de Combinaciones para Matrícula Ingresantes</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlEscuela">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList name="ddlEscuela" ID="ddlEscuela" runat="server" CssClass="form-control"
                                    AutoPostBack="true">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                       <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlCiclo">
                                Ciclo Acad&eacute;mico:</label>
                            <div class="col-md-8">
                                <asp:DropDownList name="ddlCiclo" ID="ddlCiclo" runat="server" CssClass="form-control"
                                    AutoPostBack="true">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
				<br>
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<i class="fa fa-search"></i> Buscar'
                                CssClass="btn btn-primary" OnClick="btnConsultar_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btnCrear" runat="server" Text='<i class="fa fa-plus"></i> Nuevo'
                                CssClass="btn btn-success" OnClick="btnCrear_Click"></asp:LinkButton>
                           
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
            <div class="table-responsive">
            <asp:GridView ID="grwResultado" runat="server" AutoGenerateColumns="False" DataKeyNames="id,codigo_cpf,nombre_Cpf,codigo_cac,cicloacademico,nrocombinacion" CssClass="table table-sm table-bordered table-hover" GridLines="None">
                <Columns>          
                    <asp:BoundField DataField="nombre_Cpf" HeaderText="Escuela Profesional" />
                    <asp:BoundField DataField="cicloacademico" HeaderText="Semestre Academico" />                                                                              
                    <asp:BoundField DataField="nrocombinacion" HeaderText="Nro de Combinaciones" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estado" HeaderText="Estado" />   
                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>  
                            <div class="btn-group">
                                <asp:LinkButton ID="BtnDetalle" CssClass="btn btn-warning btn-xs" runat="server" Text="Editar" ToolTip="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"    CommandName="Editar"><span class="fa fa-edit"></span></asp:LinkButton>                                
                                <asp:LinkButton ID="BtnCombDet" CssClass="btn btn-primary btn-xs" runat="server" Text="Ver" ToolTip="Detalle de Combinacion" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"    CommandName="CombDet"><span class="fa fa-list-alt"></span></asp:LinkButton>                                
                            </div> 
                            </ItemTemplate>
                            <HeaderStyle />
                         
                        </asp:TemplateField>
                </Columns>
                 <EmptyDataTemplate>
                    No hay ningún registro
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="12px" />
                <EditRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
            </asp:GridView>
        </div>
            </div>
        </div>        
        
       </div>
       <div class="col-md-12" id="divRegistrarCombinacion" runat="server">
          <div class="panel panel-default" id="pnlRegistro" runat="server">
            <div class="panel panel-heading">
                <h4>Registrar Combinaci&oacute;n</h4>
            </div>
            <div class="panel panel-body">
               <div class="row">
	             <div class="col-md-6">
	                <div class="form-group">
                       
                        <label class="col-md-4" for="ddlEscuelaReg">
                               Escuela Profesional:</label>
				        <div class="col-md-8">
				        <asp:DropDownList ID="ddlEscuelaReg" runat="server"  CssClass="form-control" > </asp:DropDownList>          						
                        </div>    
	                </div>
	                </div>
	                <div class="col-md-6" >
	                    <div class="form-group">                            
                            <label class="col-md-4" for="ddlCicloReg">
                               Ciclo Acad&eacute;mico:</label>
				            <div class="col-md-8">
				            <asp:DropDownList ID="ddlCicloReg" runat="server"  CssClass="form-control" > </asp:DropDownList>
                            </div>    
	                    </div>
	                </div>
	        </div>
	        <div class="row">
	             <div class="col-md-6">
	                <div class="form-group">                        
                        <label class="col-md-4" for="txtnrocomb">Nro Combinaciones:</label>
				        <div class="col-md-8">
				        <asp:TextBox ID="txtnrocomb" runat="server"  CssClass="form-control"  /> 
                        </div>    
	                </div>
	                </div>
	              <div class="col-md-6">
	                          
	                
	               <div class="form-group">
					    <div class="col-md-12">
						    <div class="input-group" style="text-align:justify">
							    <span class="input-group-addon addon-left" style="background-color:White">										
								    <asp:CheckBox ID="chkActivo" name="chkActivo" runat="server" Checked=true />
								    <label for="chkActivo" class="margin-zero"><span></span></label>
							    </span>
							    <label class="form-control" for="chkActivo">Activo</label>								
						    </div>
					    </div>
                    </div>
	                
	                </div>
	         </div>
            </div>
            <div class="panel-footer">
            <div class="row">
                    <div class="col-md-12"><center>
                        <div class="btn-group">
                            
                            <asp:LinkButton ID="btnCancelar" runat="server" Text='<i class="fa fa-arrow-circle-left"></i> Regresar'
                                CssClass="btn btn-danger"></asp:LinkButton>
                                <asp:LinkButton ID="btnGrabar" runat="server" Text='<i class="fa fa-arrow-circle-right"></i> Grabar'
                                CssClass="btn btn-success"></asp:LinkButton>
                        </div></center>
                    </div>
                </div>
            </div>
          </div>
       </div>
       <div class="col-md-12" id="divListarCombinacionDet" runat="server" >
	   <br><br>
       <div class="col-md-6">
       <fieldset>
	                    <legend style="font-size:medium;font-weight:bold;">Detalle combinaciones</legend>
	                     <div class="row">
	                         <div class="col-md-12">
        	                    <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Escuela Profesional</span>                                    
                                        <input type="text" id="lblEscuela" name="lblEscuela" value="" class="form-control" runat="server" readonly="readonly" />                                    
                                    </div>
                                </div>      
	                         </div>
	                     </div>
	                     <div class="row">
	                         <div class="col-md-12">
        	                    <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Semestre Academico</span>
                                        <input type="text" id="lblCiclo" name="lblCiclo" value="" class="form-control" runat="server" readonly="readonly"  />                           
                                    </div>
                                </div>      
	                         </div>
	                     </div>
	                     <div class="row">
	                         <div class="col-md-12">
        	                    <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Plan de Estudio</span>
                                        <asp:DropDownList ID="ddlPlanEstudio" runat="server"  CssClass="form-control" Font-Bold="true" style="background-color:beige">  </asp:DropDownList>                            
                                    </div>
                                </div>      
	                         </div>
	                     </div>
	                     <div class="row">
	                         <div class="col-md-12">
        	                    <div class="form-group">
	                                <div class="btn-group">  
	                                    <asp:LinkButton ID="btnDetRegresar" runat="server" CssClass="btn btn-danger" Text="Consultar"><span class="fa fa-arrow-left"></span>&nbsp;Regresar</asp:LinkButton>
	                                    <asp:LinkButton ID="btnDetConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>
	                                   <%-- <asp:LinkButton ID="btnDetAgregar" runat="server" CssClass="btn btn-green " Text="Consultar"><span class="fa fa-plus"></span>&nbsp;Agregar</asp:LinkButton>	       --%>
	                                </div>
                                </div>      
	                         </div>
	                     </div>
	                     </fieldset>
       </div>
       <div class="col-md-6">
	                    <fieldset>
	                    <legend style="font-size:medium;font-weight:bold;">Curso programado para combinación</legend>
	                    <div class="row">
	                     <div class="col-md-12" style="float:left;">
    	                        <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Curso</span>
                                    
                                    <asp:DropDownList ID="ddlCurso" runat="server"  CssClass="form-control" Font-Bold="true" >  </asp:DropDownList>                                                        
                                    </div>
                                </div>     
	                     </div>
	                    </div>
	                    <div class="row">
	                     <div class="col-md-12" style="float:left;">
	                        <div class="form-group">
                            <div class="input-group demo-group">
                            <span class="input-group-addon addon-left">Combinacion N&deg;</span>
                            <asp:DropDownList ID="ddlCombinacion" runat="server"  CssClass="form-control" Font-Bold="true" >  </asp:DropDownList>                                                        
                            </div>
                        </div>  
	                     </div>
	                     </div>
	                     <div class="row">
	                        <div class="col-md-12" style="float:left;">
	                        <div class="form-group">
                            <div class="input-group demo-group">
                            <span class="input-group-addon addon-left">N&deg; Ingresantes:</span>
                            <asp:TextBox ID="txtdetnumero" runat="server"  CssClass="form-control" style="text-align:left" Font-Bold="true" ForeColor="Black"  ></asp:TextBox>  
                            <span class="input-group-btn">                            
                            <asp:LinkButton ID="btnDetCerrar" runat="server" CssClass="btn btn-danger  btn-md" Text='Cancelar' Visible=false></asp:LinkButton>
                            <asp:LinkButton ID="btnDetAgregar" runat="server" CssClass="btn btn-success  btn-md" Text='Guardar'></asp:LinkButton>	       
                            </span>                      
                            </div>
                            
                        </div>  
	                     </div>
	                     </div>
	                     <div class="row" id="divInfoEdit" runat="server">
	                     <div class="col-md-12" style="float:left; >
	                            <div class="form-group">
                                     <div class="input-group demo-group">                                
                                     <span class="input-group-addon addon-left">Comunicado</span>
                                <asp:TextBox ID="txtinfoeditar" runat="server"  CssClass="form-control" ReadOnly 
                                    Height="53px" TextMode="MultiLine" Font-Bold="True" 
                                BackColor="#FFFFCC" Width="100%" ></asp:TextBox>                          
                         </div>
                            
                        </div>  
	                     </div>
	                     </div>
	                     </fieldset>
	            </div>
	       <div class="col-md-12">
	              <asp:GridView ID="gDataCombDet" runat="server" AutoGenerateColumns="False"                 
                   DataKeyNames="id,faltan,codigo_cup" CssClass="table table-sm table-bordered table-hover" GridLines="None">
                
                <Columns>          
                    <asp:BoundField DataField="nrocombinacion" HeaderText="Combinacion" >
                    <ItemStyle Width="5%" Font-Bold="True" HorizontalAlign="Center" Font-Size=X-Large 
                        VerticalAlign="Middle" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="curso" HeaderText="Curso" >
                    <ItemStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="grupo" HeaderText="Grupo" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cap_amb" HeaderText="Capacidad Ambiente Máx." >
                    <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="vacante" HeaderText="Total Vacantes" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="mat" HeaderText="Total Matriculados" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="nroestudantes" HeaderText="Vacantes para Ingresantes" >
                    <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="cantmatingr" HeaderText="Ingresantes Matriculados" >
                    <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>
                   
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label  ID="lblcup" runat="server" Text='<%# Bind("codigo_cup") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("codigo_cup") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="0px" />
                        <ItemStyle Width="0%" />
                    </asp:TemplateField>
                    
                    
                                         
                    <asp:BoundField DataField="faltan" HeaderText="Vacantes Disponibles Ingresantes" >
                    <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>                      
                    <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
                    HeaderText="" SelectImageUrl="../../Images/menus/edit_s.gif" 
                    UpdateImageUrl="../../Images/menus/edit_s.gif" SelectText="Editar" >
                <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" alternateText="Eliminar" OnClientClick="return confirm('¿Desea Eliminar Registro?.')" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>                      
                </Columns>
                 <EmptyDataTemplate>
                    No hay ningún registro
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="12px" />
                <EditRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
            </asp:GridView>
                </div>	   	 
       </div>
       
    </div> 
    </form>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
            var isOk = $("#validar").val();
            var error = args.get_error();

            if (error) {
                // Manejar el error
            }

            if (controlId == 'btnValidar') {
                if (isOk == "1") {
                    __doPostBack('btnAceptar', '');
                }
            }
        });
    </script>

</body>
</html>
