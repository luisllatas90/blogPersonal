<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmprogramacioncademicacombinacion.aspx.vb" Inherits="administrativo_pec_frmprogramacioncademicacombinacion" %>

<!DOCTYPE html>

<html lang="en">
<meta charset="utf-8"/>

<head id="Head" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
<meta http-equiv='X-UA-Compatible' content='IE=8' />
<meta http-equiv='X-UA-Compatible' content='IE=10' />
<meta http-equiv='X-UA-Compatible' content='IE=11' />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
<link rel='stylesheet' href='../../assets/css/bootstrap.min.css?a=1'/>
<link rel='stylesheet' href='../../assets/css/material.css?a=1'/>
<link rel='stylesheet' href='../../assets/css/style.css?a=1'/>
<script type="text/javascript" src='../../assets/js/jquery.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/app.js?a=1'></script>

<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/bootstrap.min.js?a=1'></script>

<script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/wow.min.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.accordion.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/materialize.js?a=1'></script>
<script type="text/javascript" src='../../assets/js/funciones.js?a=1'></script>
<script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
<script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>
<script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
<link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>

    <title>Programar Combinaciones</title>
    <script type="text/javascript">
    var event = jQuery.Event("DefaultPrevented");
     $(document).trigger(event);
     $(document).ready(function() {

     var oTable = $('#gDataComb').DataTable({
         //"sPaginationType": "full_numbers",
         "bPaginate": false,
         "bFilter": true,
         "bLengthChange": false,
         "bSort": true,
         "bInfo": true,
         "bAutoWidth": true
     });
/*
     var oTableDet = $('#gDataCombDet').DataTable({
         //"sPaginationType": "full_numbers",
         "bPaginate": false,
         "bFilter": true,
         "bLengthChange": false,
         "bSort": false,
         "bInfo": true,
         "bAutoWidth": true
     });*/
     
     //gDataCombDet


     
         $('#txtnrocomb').keypress(function(tecla) {
             if (tecla.charCode < 48 || tecla.charCode > 57) return false;
         });
         $('#txtdetnumero').keypress(function(tecla) {
             if (tecla.charCode < 48 || tecla.charCode > 57) return false;
         });
         
         
     });

</script>    
</head>
<body>

<div class="main-content">
    <form id="form1" runat="server">
    <div id ="divEscuelaComb">
        <div class="panel panel-piluku">
	    <div class="panel-heading">
	    <h3 class="panel-title"><i class="ion-android-list"></i> Programaci&oacute;n de combinaciones.</h3>	    
	    </div>
	    <div class="panel-body" style="padding:0px;">
	    <div class="row">
	        <div class="col-md-12" id="divBuscarCombinacion" runat="server">
	    <fieldset>
	    <legend style="font-size:medium;font-weight:bold;">Filtros de B&uacute;squeda</legend>
	    
	    <div class="row">
	    <div class="col-md-6" style="float:left;width:50%">
	        <div class="form-group">
                <label class="col-md-4 control-label"><b>Escuela Profesional</b></label>
				<div class="col-md-8">
				<asp:DropDownList ID="ddlEscuela" runat="server"  CssClass="form-control" Font-Bold=true> </asp:DropDownList>          						
                </div>    
	        </div>
	    </div>
	    <div class="col-md-6" style="float:left;width:50%">
	        <div class="form-group">
                <label class="col-md-4 control-label"><b>Ciclo Acad&eacute;mico</b></label>
				<div class="col-md-8">
				<asp:DropDownList ID="ddlCiclo" runat="server"  CssClass="form-control" Font-Bold=true> </asp:DropDownList>          						
                </div>    
	        </div>
	    </div>
	    </div>
	    <div class="row">
	    <center>
	    <div class="btn-group">  
	        <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>
	        <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-green" Text="Consultar"><span class="fa fa-plus"></span>&nbsp;Agregar</asp:LinkButton>
	    </div>
	    </center>
	    </div>
	    </fieldset>
	    </div>
	        <div class="col-md-12" id="divListarCombinacion" runat="server">
	        <fieldset>
	        <legend style="font-size:medium;font-weight:bold;">Listado Combinaciones</legend>
	        <asp:GridView ID="gDataComb" runat="server" AutoGenerateColumns="False" 
                CellPadding="4"  BorderStyle="None" 
                 AlternatingRowStyle-BackColor="#F7F6F4"  DataKeyNames="id,codigo_cpf,nombre_Cpf,codigo_cac,cicloacademico,nrocombinacion" CssClass="display" Font-Size=12px Width="100%">
                <RowStyle BorderColor="#C2CFF1" />
                <Columns>          
                    <asp:BoundField DataField="nombre_Cpf" HeaderText="Escuela Profesional" />
                    <asp:BoundField DataField="cicloacademico" HeaderText="Semestre Academico" />                                                                              
                    <asp:BoundField DataField="nrocombinacion" HeaderText="Nro de Combinaciones" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estado" HeaderText="Estado" />   
                      <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>   
                                <asp:LinkButton ID="BtnDetalle" CssClass="btn btn-orange btn-xs" runat="server" Text="Editar" ToolTip="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"    CommandName="Editar"><span class="ion-edit"></span></asp:LinkButton>                                
                                <asp:LinkButton ID="BtnCombDet" CssClass="btn btn-primary btn-xs" runat="server" Text="Ver" ToolTip="Detalle de Combinacion" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"    CommandName="CombDet"><span class="fa fa-list-alt"></span></asp:LinkButton>                                
                            </ItemTemplate>
                            <HeaderStyle />
                         
                        </asp:TemplateField>
                </Columns>
                  <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
               <EmptyDataTemplate>
                     <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                         No se encontraron registros.
                     </div>
                 </EmptyDataTemplate>
                 <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
                 <AlternatingRowStyle BackColor="#F7F6F4" />
            </asp:GridView>
	        </fieldset>
	        </div>
	        <div class="col-md-12" id="divRegistrarCombinacion" runat="server">
	        <fieldset>
	        <legend style="font-size:medium;font-weight:bold;">Registro Combinaciones</legend>
	        <div class="row">
	             <div class="col-md-6" style="float:left;width:50%">
	                <div class="form-group">
                        <label class="col-md-4 control-label"><b>Escuela Profesional</b></label>
				        <div class="col-md-8">
				        <asp:DropDownList ID="ddlEscuelaReg" runat="server"  CssClass="form-control" Font-Bold=true> </asp:DropDownList>          						
                        </div>    
	                </div>
	                </div>
	                <div class="col-md-6" style="float:left;width:50%">
	                    <div class="form-group">
                            <label class="col-md-4 control-label"><b>Ciclo Acad&eacute;mico</b></label>
				            <div class="col-md-8">
				            <asp:DropDownList ID="ddlCicloReg" runat="server"  CssClass="form-control" Font-Bold=true> </asp:DropDownList>
                            </div>    
	                    </div>
	                </div>
	        </div>
	        <div class="row">
	             <div class="col-md-6" style="float:left;width:50%">
	                <div class="form-group">
                        <label class="col-md-4 control-label"><b>Nro Combinaciones</b></label>
				        <div class="col-md-8">
				        <asp:TextBox ID="txtnrocomb" runat="server"  CssClass="form-control"  /> 
                        </div>    
	                </div>
	                </div>
	              <div class="col-md-6" style="float:left;width:50%">
	                <div class="form-group">
                        <label class="col-md-4 control-label"><b>Activo</b></label>
				        <div class="col-md-8">
				        <asp:CheckBox ID="chkActivo" runat="server"  Checked="true"/>
                        </div>    
	                </div>
	                </div>
	         </div>
	        <div class="row">
	        <center>
	        <div class="btn-group">  
	            <asp:LinkButton ID="btnCombGrabar" runat="server" CssClass="btn btn-green" Text="Consultar"><span class="fa fa-check"></span>&nbsp;Guardar</asp:LinkButton>
	            <asp:LinkButton ID="btnCombCancelar" runat="server" CssClass="btn btn-red" Text="Consultar"><span class="fa fa-times"></span>&nbsp;Cancelar</asp:LinkButton>
	        </div>
	        </center>
	        </div>	        
	        </fieldset>
	        </div>
	        <div class="col-md-12" id="divListarCombinacionDet" runat="server" >
	            <div class="col-md-6" style="float:left;">
	                    <fieldset>
	                    <legend style="font-size:medium;font-weight:bold;">Detalle combinaciones</legend>
	                     <div class="row">
	                         <div class="col-md-12" style="float:left;">
        	                    <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Escuela Profesional</span>
                                    
                                        <input type="text" id="lblEscuela" name="lblEscuela" value="" class="form-control" runat="server" readonly="readonly" />                                    
                                    </div>
                                </div>      
	                         </div>
	                     </div>
	                     <div class="row">
	                         <div class="col-md-12" style="float:left;">
        	                    <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Semestre Academico</span>
                                        <input type="text" id="lblCiclo" name="lblCiclo" value="" class="form-control" runat="server" readonly="readonly"  />                           
                                    </div>
                                </div>      
	                         </div>
	                     </div>
	                     <div class="row">
	                         <div class="col-md-12" style="float:left;">
        	                    <div class="form-group">
                                    <div class="input-group demo-group">
                                    <span class="input-group-addon addon-left">Plan de Estudio</span>
                                        <asp:DropDownList ID="ddlPlanEstudio" runat="server"  CssClass="form-control" Font-Bold="true" style="background-color:beige">  </asp:DropDownList>                            
                                    </div>
                                </div>      
	                         </div>
	                     </div>
	                     <div class="row">
	                         <div class="col-md-12" style="float:left;">
        	                    <div class="form-group">
	                                <div class="btn-group">  
	                                    <asp:LinkButton ID="btnDetRegresar" runat="server" CssClass="btn btn-red" Text="Consultar"><span class="ion-android-arrow-back"></span>&nbsp;Regresar</asp:LinkButton>
	                                    <asp:LinkButton ID="btnDetConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>
	                                   <%-- <asp:LinkButton ID="btnDetAgregar" runat="server" CssClass="btn btn-green " Text="Consultar"><span class="fa fa-plus"></span>&nbsp;Agregar</asp:LinkButton>	       --%>
	                                </div>
                                </div>      
	                         </div>
	                     </div>
	                     </fieldset>
	            </div>
	            <div class="col-md-6" style="float:left;">
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
                            <span class="input-group-addon addon-left">N&deg; Ingresantes (Vacantes-Mat-Vacantes Asig) :</span>
                            <asp:TextBox ID="txtdetnumero" runat="server"  CssClass="form-control" style="text-align:left" Font-Bold="true" ForeColor="Black"  ></asp:TextBox>  
                            <span class="input-group-btn">
                            <asp:LinkButton ID="btnDetAgregar" runat="server" CssClass="btn btn-green  btn-xs" Text="Consultar"><span class="ion-android-done"></span>&nbsp;Guardar</asp:LinkButton>	       
                            <asp:LinkButton ID="btnDetCerrar" runat="server" CssClass="btn btn-red  btn-xs" Text="Consultar"><span class="ion-android-close"></span>&nbsp;Cancelar</asp:LinkButton>	       
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
	            <hr />
	            <div class="col-md-12">
	              <asp:GridView ID="gDataCombDet" runat="server" AutoGenerateColumns="False" 
                CellPadding="4"  BorderStyle="None" 
                   DataKeyNames="id,faltan,codigo_cup" CssClass="display" Font-Size=12px Width="100%">
                
                <Columns>          
                    <asp:BoundField DataField="nrocombinacion" HeaderText="Combinacion" >
                    <ItemStyle Width="10%" Font-Bold="True" HorizontalAlign="Center" Font-Size=X-Large 
                        VerticalAlign="Middle" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="curso" HeaderText="Curso" >
                    <ItemStyle Width="40%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="grupo" HeaderText="Grupo" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cap_amb" HeaderText="Cap Amb Min" >
                    <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="vacante" HeaderText="Vacantes" >
                    <ItemStyle HorizontalAlign="center" Width="10%" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="mat" HeaderText="Mat" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="" HeaderText="Total Ingresantes" >
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
                    <asp:BoundField DataField="nroestudantes" HeaderText="Ingresante" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>                      
                    <asp:BoundField DataField="faltan" HeaderText="Faltan" >
                    <ItemStyle HorizontalAlign="center" Width="5%" />
                    </asp:BoundField>                      
                    <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
                    HeaderText="" SelectImageUrl="../../../images/editar_poa.png" 
                    UpdateImageUrl="../../images/editar.gif" SelectText="Editar" >
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
                  <FooterStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
               <EmptyDataTemplate>
                     <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                         No se encontraron registros.
                     </div>
                 </EmptyDataTemplate>
                 <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
                 
            </asp:GridView>
                </div>	              
	        </div>		        
	        
	    </div>
	    
	    </div>
        </div> 
    </div> 
    </form>
    </div>
</body>
</html>
