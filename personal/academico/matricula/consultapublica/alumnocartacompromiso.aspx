<%@ Page Language="VB" AutoEventWireup="false" CodeFile="alumnocartacompromiso.aspx.vb" Inherits="academico_matricula_consultapublica_alumnocartacompromiso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
  <meta http-equiv='X-UA-Compatible' content='IE=8' />
  <meta http-equiv='X-UA-Compatible' content='IE=10' />
  <meta http-equiv='X-UA-Compatible' content='IE=11' />
 <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
<link rel='stylesheet' href='../../../assets/css/bootstrap.min.css?x=1'/>
<link rel='stylesheet' href='../../../assets/css/material.css?x=1'/>
<link rel='stylesheet' href='../../../assets/css/style.css?y=1'/>
<script type="text/javascript" src='../../../assets/js/jquery.js?x=1'></script>
<script type="text/javascript" src='../../../assets/js/app.js?x=1'></script>

<script type="text/javascript" src='../../../assets/js/jquery-ui-1.10.3.custom.min.js?x=1'></script>
<script type="text/javascript" src='../../../assets/js/bootstrap.min.js?x=1'></script>

<script type="text/javascript" src='../../../assets/js/jquery.nicescroll.min.js?x=1'></script>
<script type="text/javascript" src='../../../assets/js/wow.min.js?x=1'></script>
<script type="text/javascript" src='../../../assets/js/jquery.loadmask.min.js?x=1'></script>
<script type="text/javascript" src='../../../assets/js/jquery.accordion.js?x=1'></script>
<script type="text/javascript" src='../../../assets/js/materialize.js?x=1'></script>
<%--<script type="text/javascript" src='../../../assets/js/funciones.js?x=1'></script>--%>

<script type="text/javascript" src="../../../assets/js/jquery.dataTables.min.js?x=1"></script>	
<link rel='stylesheet' href='../../../assets/css/jquery.dataTables.min.css?z=1'/>
    <title></title>
    <script type="text/javascript">
     var event = jQuery.Event("DefaultPrevented");
     $(document).trigger(event);
     $(document).ready(function() {

         // $('#divInfoDetails').hide();
         var oTable = $('#gData').DataTable({
         //"sPaginationType": "full_numbers",
            "bPaginate": false,
             "bFilter": true,
             "bLengthChange": false,
             "bSort": false,
             "bInfo": true,
             "bAutoWidth": true
         });

     });
     </script>         
</head>
<body class="">
<form runat=server>
<div id="loading" class="piluku-preloader text-center" runat="server">         
        <div class="loader" id="loader" runat="server" style="display:block">Loading...</div>
</div>
<div class="wrapper">
<div class="content">

<div class="main-content">
        <div class="panel panel-piluku">
	        <div class="panel-heading">
	            <h3 class="panel-title">
                        <i class="ion-android-list"></i> Reporte matr&iacute;cula con carta de compromiso
                </h3>
	        </div>
	        <div class="panel-body">
	        <div class="row">
	            <fieldset>	        
	            <legend>&nbsp;Filtro de B&uacute;squeda</legend>	        
    	               <div class="row">
    	                    <div class="col-md-6">
	                              <div class="form-group">
						              <label for="ddlCiclo" class="col-sm-4 control-label"><b>Ciclo Académico:</b></label>
                                      <div class="col-sm-8"> <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control" Font-Bold=true >
                                      </asp:DropDownList>
                                  </div>
                              </div>  
                             </div>
                             <div class="col-md-6">
                              <div class="form-group">
                                 <div class="btn-group btn-group-justified">
                                    <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>
                                    <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-green" Text="Exportar"><span class="fa fa-file-excel-o"></span>&nbsp;Exportar</asp:LinkButton>
                                 </div>
                              </div>
                             </div>
	                    </div>
    	                
	            </fieldset>
	        </div>
	        <div class="row">
	            <fieldset>	        
	                <legend>&nbsp;Listado de Alumnos</legend>
	                  <div class="row">
	                  <div class="col-md-12">
                          <asp:GridView ID="gData" runat="server" AutoGenerateColumns="False"   DataKeyNames="codigo_cac,codigo_alu"
                              CellPadding="4"  BorderStyle="None" AlternatingRowStyle-BackColor="#F7F6F4" 
                              CssClass="display" Font-Size=12px Width="100%" 
                              EmptyDataText="No se encontraron datos">
                          <RowStyle BorderColor="#C2CFF1" />
                           <Columns> 
                           <asp:BoundField DataField="codigo_cac" HeaderText="Ciclo Acad&eacute;mico" 
                                   Visible="False" />
                           <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código Universitario" /> 
                           <asp:BoundField DataField="estudiante" HeaderText="Estudiante" />                
                           <asp:BoundField DataField="carreraprofesional" HeaderText="Carrera Profesional" />
                           <asp:BoundField DataField="asignaturas" HeaderText="Asignatura Condicionada" />
                           <asp:BoundField DataField="vecesdesaprobada" HeaderText="N&uacute;mero de veces desaprobada" />
                           <asp:BoundField DataField="matriculado" HeaderText="Matriculado" />
                           <asp:BoundField DataField="cursosmatriculados" HeaderText="Cursos Matriculados" />
                         <%--  <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Carta de Compromiso" >
                            <ItemTemplate>--%>
                               <%-- <asp:LinkButton ID="BtnDetalle" CssClass="btn btn-primary" runat="server" Text="Ver" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  CommandName="Ver"><span class="fa fa-list-alt"></span></asp:LinkButton>--%>

<asp:HyperLinkField 
      DataNavigateUrlFields="codigo_alu, codigo_cac" runat="server"  Text="Descargar"
      DataNavigateUrlFormatString="ImprimirCartaCompromiso.aspx?alumno={0}&ciclo={1}"
      HeaderText="Carta de Compromiso" 
      Target="_blank"
      ItemStyle-Font-Bold="true"     
      />
                           <%-- </ItemTemplate>--%>

<%--<ItemStyle HorizontalAlign="Center">
</ItemStyle>
                            </asp:TemplateField>--%>
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
                          </div>
	                  </div>
	            </fieldset>    
	        </div>
	        
	        
	        
            </div>

</div>
</div>
</form>
</body>
</html>
