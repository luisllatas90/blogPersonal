<%@ Page Language="VB" AutoEventWireup="false" CodeFile="estimarvacantesgw.aspx.vb" Inherits="academico_cargalectiva_consultapublica_estimarvacantesgw" %>

<!DOCTYPE html>

<html lang="en">
<meta charset="utf-8"/>

<head id="Head" runat="server">
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
         var oTableDetElect = $('#gdDetalleElectivo').DataTable({
             //"sPaginationType": "full_numbers",
             "bPaginate": false,
             "bFilter": true,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": true,
             "bAutoWidth": true
         });

         
         
    
//         $('#gData').DataTable().fnSetFilteringDelay(2000);
//         oTable.fnAdjustColumnSizing();


         var oTableAprob = $('#gDataAprobados').DataTable({
             "bJQueryUI": false,
             "sPaginationType": "full_numbers",
             "bFilter": true,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": true,
             "bAutoWidth": true,
             "aaSorting": [[1, "asc"]]
         });

         var oTableAprobReq = $('#gDataAprobadosReq').DataTable({
             "bJQueryUI": false,
             "sPaginationType": "full_numbers",
             "bFilter": true,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": true,
             "bAutoWidth": true,
             "aaSorting": [[1, "asc"]]
         });
         var oTableAprobReq = $('#gDataAprobadosReq2').DataTable({
             "bJQueryUI": false,
             "sPaginationType": "full_numbers",
             "bFilter": true,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": true,
             "bAutoWidth": true,
             "aaSorting": [[1, "asc"]]
         });
         var oTableAptos = $('#gDataAptos').DataTable({
             "bJQueryUI": false,
             "sPaginationType": "full_numbers",
             "bFilter": true,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": true,
             "bAutoWidth": true,
             "aaSorting": [[1, "asc"]]
         });
         var oTableAptos = $('#gDatamatriculados').DataTable({
             "bJQueryUI": false,
             "sPaginationType": "full_numbers",
             "bFilter": true,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": true,
             "bAutoWidth": true,
             "aaSorting": [[1, "asc"]]
         });

         var oTableAprobPE = $('#gDatamatriculadosPE').DataTable({
             "bJQueryUI": false,
             "bPaginate": false,
             "bFilter": false,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": false,
             "bAutoWidth": false,
             "aaSorting": [[1, "asc"]]
         });
         var oTableAprobPE = $('#gDatamatriculadosCRPE').DataTable({
             "bJQueryUI": false,
             "bPaginate": false,
             "bFilter": false,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": false,
             "bAutoWidth": false,
             "aaSorting": [[1, "asc"]]
         });
         var oTableAprobPE = $('#gdElectivo').DataTable({
             "bJQueryUI": false,
             "bPaginate": false,
             "bFilter": false,
             "bLengthChange": false,
             "bSort": true,
             "bInfo": false,
             "bAutoWidth": false,
             "aaSorting": [[1, "desc"]]
         });


     });
     
     
function fnDivLoad(div, time) {

    var $target = $('#' + div);
    $target.mask('<i class="fa fa-refresh fa-spin"></i> Cargando...');
    setTimeout(function() {
        $target.unmask();
    }, time);
}
function openModal() {
    $('#mdDetalleEstimacion').modal('show');
}
function openModalElectivo() {
    $('#mdDetalleElectivo').modal('show');
}


 </script>       
 
        
</head>
<body class="">
<div id="loading" class="piluku-preloader text-center" runat="server">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
<div class="wrapper">
<div class="content">


<div class="main-content">
<!-- encabezado-->
<form id="form1" runat="server"  >
<div  id="report">

	    <div class="panel panel-piluku">
	    <div class="panel-heading">
	    <h3 class="panel-title">
                        <i class="ion-android-list"></i> Reporte Estimaci&oacute;n de Vacantes
                    </h3>
	    </div>
	    <div class="panel-body">
	      
	    <div class="row">
	    <div class="col-md-6" style="float:left" >
	      <div class="form-group">
						<label class="col-md-4 control-label" ><b>Escuela&nbsp; Profesional:</b></label>
						<div class="col-md-8"><asp:DropDownList ID="ddlEscuela" runat="server" AutoPostBack="True" CssClass="form-control" Font-Bold=true>
                        </asp:DropDownList>            
		  </div>
		</div>
		</div>
		<div class="col-md-6"  style="float:left">
	      <div class="form-group">
						<label class="col-sm-4 control-label"><b>Plan de Estudios:</b></label>
						<div class="col-sm-8">
						<asp:DropDownList ID="ddlPlan" runat="server"  CssClass="form-control" Font-Bold=true> </asp:DropDownList>          						
		                </div>
		    </div>
		</div>
        </div>    
         <div class="row">
         <div class="col-md-6" style="float:left">
	      <div class="form-group">
						<label class="col-sm-4 control-label"><b>Ciclo Académico:</b></label>
                <div class="col-sm-8"> <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control" Font-Bold=true >
                </asp:DropDownList></div>
         
         </div>  
         </div>
         <div class="col-md-3" style="float:left; height: 19px;" >
	      <div class="form-group"><label class="col-sm-4 control-label"> </label>
                <div class="col-sm-8">
               <%-- <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Consultar Cursos" OnClientClick=" fnDivLoad('report', 5000);"  />--%>
             <asp:LinkButton ID="Button1" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>
                </div>  
         </div>  
         
         </div>
         <div class="col-md-3" style="float:left;" >
	      <div class="form-group"><label class="col-sm-4 control-label"> </label>
                <div class="col-sm-8">               
                <%--<a href="#" class="btn btn-green"><i class="fa fa-file-excel-o"></i></a>--%>
                <%--<asp:Button ID="btnExportar" runat="server" CssClass="btn btn-green" Text="Exportar" Visible="false"  />--%>
                <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-green" Text="Exportar"><span class="fa fa-file-excel-o"></span>&nbsp;Exportar</asp:LinkButton>
                </div>  
         </div>  
         
         </div>
         </div> 
         
         <div class="row">
         
          <!-- <script type="text/javascript" src='../private/dataTable/estimarvacantes.js'></script>-->
           <div role="tabpanel">
             
          <!-- Nav tabs --><ul class="nav nav-tabs piluku-tabs" role="tablist">
								<li id="liReporte" runat="server" role="presentation" class="active" style="width:50%;" onserverclick="Tab1_Click"><a href="#reporte" aria-controls="reporte" role="tab" data-toggle="tab">Reporte Estimaci&oacute;n de Vacantes</a></li>
								<li id="liElectivo" runat="server" role="presentation" style="width:50%;" onserverclick="Tab2_Click"><a href="#electivo" aria-controls="electivo" role="tab" data-toggle="tab">Reporte Cursos Electivos Aprobados</a></li>
                           </ul>
                           <div class="tab-content piluku-tab-content">
							                    <div role="tabpanel" class="tab-pane active" id="reporte" runat="server">
							                        <asp:GridView ID="gData" runat="server" AutoGenerateColumns="False" 
            CellPadding="4"  BorderStyle="None" 
             AlternatingRowStyle-BackColor="#F7F6F4"  DataKeyNames="codigo_cur,codigo_cpf,codigo_cac,codigo_pes,nombre_cur" CssClass="display" Font-Size=12px Width="100%">
            <RowStyle BorderColor="#C2CFF1" />
            <Columns>          
                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                <asp:BoundField DataField="nombre_cur" HeaderText="Curso" >
                </asp:BoundField>
                <asp:BoundField DataField="creditos_cur" HeaderText="Cred" />
                 <asp:BoundField DataField="nmat" HeaderText="Mat" >
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="necca" HeaderText="Aprob" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="tdca" HeaderText="% Tasa Desaprobados" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Estimación Desaprobados">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="nmatpr2" HeaderText="Mat y Aprob Pre Req" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="neccapr" HeaderText="Mat Pre Req" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="tdcapr" HeaderText="% Tasa Aprob Pre Req" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Estimación Aprobado Pre Req">
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Proyección aprobados / desaprobados" >
                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="nenmap" 
                    HeaderText="Aptos para Cursos Matriculados en el Semestre" >
                    <ItemStyle HorizontalAlign="Right"  Width="10px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Total de Proyeccion de Vacantes a Programar" >
                    <ControlStyle BackColor="#FF6666" Font-Bold="True" />
                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="nenmin" 
                    HeaderText="Aptos para Cursos No Mat en Semestre" >
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Num Estudiante Estimado Máximo" >
                    <ControlStyle BackColor="#FFFFCC" Font-Bold="True" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="electivo_Cur" HeaderText="electivo">
                </asp:BoundField>
                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" >
                        <ItemTemplate>                             
                            
                            <asp:LinkButton ID="BtnDetalle" CssClass="btn btn-primary" runat="server" Text="Ver" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" OnClientClick=" fnDivLoad('report', 7000);"   CommandName="Ver"><span class="fa fa-list-alt"></span></asp:LinkButton>
                            
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
							                    </div>
							                    <div role="tabpanel" class="tab-pane" id="electivo" runat="server">
                                                    <asp:GridView ID="gdElectivo" runat="server" 
                                                    DataKeyNames="codigo_pes,CICLO"
                                                    CssClass="display cell-border" Width=100% cellpadding=0  >
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("OPCION") %>'></asp:Label>--%>
                                                                    <asp:LinkButton ID="BtnDetalle2" CssClass="btn btn-primary" runat="server" Text="Ver" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" OnClientClick=" fnDivLoad('report', 7000);"   CommandName="Ver"><span class="fa fa-list-alt"></span></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
                                                        <AlternatingRowStyle BackColor="#F7F6F4" />
                                                    </asp:GridView>							                    
							                    </div>
	                        </div>
           
           </div>
      
          
  
        </div>       
                 
	    </div>
	    </div>
       
        <div class="modal fade modal-full-pad" id="mdDetalleEstimacion" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog modal-full">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="btn btn-white" data-dismiss="modal" aria-label="Close" style="background-color:red;font-weight:bold;float:right;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White" id="hTituloDet" runat="server">&nbsp;</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12">
					 <div class="row">
                               <div class="panel-group piluku-accordion" id="accordion" role="tablist" aria-multiselectable="true">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="heading0">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse0" aria-expanded="true" aria-controls="heading0">
                                        Matriculados.&nbsp;<label class="label-info" id="lblmat" runat="server"></label>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse0" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading0">
                                <div class="panel-body">                                        
                                    <div class="row">
                                    <label id="lbl" runat="server"></label>
                                    
                                    <table class="display cell-border" id="gDatamatriculadosPE" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th colspan="2" style="text-align:center">CANTIDAD DE ALUMNOS POR PLAN</th>
                                    </tr>
                                    <tr>
                                    <th>PLAN DE ESTUDIOS</th>
                                    <th>Nro DE ESTUDIANTES</th>
                                    </tr>
                                    </thead>
                                    <tbody id="tbdMatriculadoPE" runat="server">
                                    </tbody>
                                    </table>
                                    </div>
                                <div class="row">
                                    <table class="display cell-border" id="gDatamatriculados" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th>C&oacute;digo Uni</th>
                                    <th>Estudiante</th>
                                    <th>Ciclo Ingreso</th>
                                    <th>Plan de Estudio</th>
                                    </tr>
                                    </thead> 
                                    <tbody id="tbdMatriculado" runat="server">
                                    </tbody>   
                                    <tfoot>
                                    <tr style="border-top:1 black solid">
                                    <th colspan=4>                                    
                                    </th>
                                    </tr>
                                    </tfoot>                                 
                                    </table>
                                 </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="heading1">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="true" aria-controls="heading1">
                                        Aprobados.&nbsp;<label class="label-info" id="lblap" runat="server"></label>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse1" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading1">
                                <div class="panel-body">                                        
                                    <table class="display cell-border" id="gDataAprobados" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th>C&oacute;digo Uni</th>
                                    <th>Estudiante</th>
                                    <th>Ciclo Ingreso</th>
                                    </tr>
                                    </thead> 
                                    <tbody id="tbdAprobado" runat="server">
                                    </tbody>   
                                    <tfoot>
                                    <tr style="border-top:1 black solid">
                                    <th colspan=3>                                    
                                    </th>
                                    </tr>
                                    </tfoot>                                 
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="heading12">
                                <h4 class="panel-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#collapse12" aria-expanded="true" aria-controls="heading12">
                                        Prerequisito Aprobados y Matriculados&nbsp;<label class="label-info" id="lblrap2" runat="server"></label>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse12" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading1">
                                <div class="panel-body">                                        
                                    <table class="display cell-border" id="gDataAprobadosReq2" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th>C&oacute;digo Uni</th>
                                    <th>Estudiante</th>
                                    <th>Ciclo Ingreso</th>
                                    <th>Plan de Estudios</th>
                                    </tr>
                                    </thead> 
                                    <tbody id="tbdAprobadoReq2" runat="server">
                                    </tbody> 
                                    <tfoot>
                                    <tr style="border-top:1 black solid">
                                    <th colspan=4>                                    
                                    </th>
                                    </tr>
                                    </tfoot>                                  
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="heading2">
                                <h4 class="panel-title">
                                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse2" aria-expanded="false" aria-controls="heading2">
                                        Prerequisito Matriculados.&nbsp;<label class="label-info" id="lblrap" runat="server"></label>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse2" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading2">
                                <div class="panel-body">
                                <div class="row">
                                    <table class="display cell-border" id="gDatamatriculadosCRPE" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th colspan="2" style="text-align:center">CANTIDAD DE ALUMNOS POR PLAN</th>
                                    </tr>
                                    <tr>
                                    <th>PLAN DE ESTUDIOS</th>
                                    <th>Nro DE ESTUDIANTES</th>
                                    </tr>
                                    </thead>
                                    <tbody id="tbdMatriculadoCRPE" runat="server">
                                    </tbody>
                                    </table>
                                    </div>
                                <div class="row">
                                    <table class="display cell-border" id="gDataAprobadosReq" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th>C&oacute;digo Uni</th>
                                    <th>Estudiante</th>
                                    <th>Ciclo Ingreso</th>
                                    <th>Plan de Estudios</th>
                                    </tr>
                                    </thead> 
                                    <tbody id="tbdAprobadoReq" runat="server">
                                    </tbody> 
                                    <tfoot>
                                    <tr style="border-top:1 black solid">
                                    <th colspan=4>                                    
                                    </th>
                                    </tr>
                                    </tfoot>                                  
                                    </table>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="heading3">
                                <h4 class="panel-title">
                                    <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse3" aria-expanded="false" aria-controls="heading3">
                                        Aptos Para llevar.&nbsp;<label class="label-info" id="lblapto" runat="server"></label>
                                    </a>
                                </h4>
                            </div>
                            <div id="collapse3" class="panel-collapse collapse" role="tabpanel" aria-labelledby="heading3">
                                <div class="panel-body">
                                    <table class="display cell-border" id="gDataAptos" cellpadding=0 cellspacing=0>
                                    <thead>
                                    <tr>
                                    <th>C&oacute;digo Uni</th>
                                    <th>Estudiante</th>
                                    <th>Ciclo Ingreso</th>
                                    </tr>
                                    </thead> 
                                    <tbody id="tbdAptos" runat="server">
                                    </tbody> 
                                    <tfoot>
                                    <tr style="border-top:1 black solid">
                                    <th colspan=3>                                    
                                    </th>
                                    </tr>
                                    </tfoot>                                  
                                    </table>
                                </div>
                            </div>
                        </div>                        
                    </div>
                            </div>
					</div>
			        </div>			                
					</div>
				  
			        
			        <div class="modal-footer">
				    
				</div>
				</div>
				
			</div>
		
	</div>
	<div class="modal fade modal-full-pad" id="mdDetalleElectivo" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog modal-full">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;" >
					<button type="button" class="btn btn-white" data-dismiss="modal" aria-label="Close" style="background-color:red;font-weight:bold;float:right;"><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title"  style="color:White" id="hTituloElectivoDet" runat="server">&nbsp;</h4>
				</div>
				<div class="modal-body">
                    <div class="row">	
					<div class="col-md-12">
					 <div class="row">
                                <center><asp:GridView ID="gdDetalleElectivo" runat="server" 
                                                    DataKeyNames="ESTUDIANTE"
                                                    CssClass="display cell-border" Width="99%" cellpadding="0" CellSpacing="0" Font-Size=X-Small  >
                                                      
                                                        <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
                                                        <AlternatingRowStyle BackColor="#F7F6F4" />
                                                    </asp:GridView>	
                                                    <asp:Label ID ="lbl2" runat="server"></asp:Label>	
                                                    </center>
                            </div>
					</div>
			        </div>			                
					</div>
				  
			        
			        <div class="modal-footer">
				    
				</div>
				</div>
				
			</div>
		
	</div>
	
</div>	
</form>
</div>
	</div>
	</div>
</body>
</html>