<%@ Page Language="VB" AutoEventWireup="false" CodeFile="aulavirtual.aspx.vb" Inherits="CampusVirtualEstudiante_aulavirtual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
<meta name="tipo_contenido" content="text/html;" http-equiv="content-type" charset="utf-8">
<script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>
<link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css'/>
<link rel='stylesheet' href='../assets/css/bootstrap.min.css'/>
<link rel='stylesheet' href='../assets/css/material.css'/>
<link rel='stylesheet' href='../assets/css/style.css?x=1'/>
<link rel='stylesheet' href='../assets/css/sweet-alerts/sweetalert.css'/>

</head>

<body>
    <form id="form1"  runat="server" action="https://intranet.usat.edu.pe/aulavirtual/login/index.php" target=_blank method="post">
  
  
   <%--<form    action="http://10.10.14.25/aulavirtual2.6_06_12/login/index.php" target=_blank method="POST" runat="server">--%>
  
       
    <div class="row grid">   
    		<div class="col-md-12">
    			<div class="panel panel-piluku">
					<div class="panel-heading" style="text-align: center;">
						<h3 class="panel-title">Accede al Aula Virtual</h3>
					</div>
    		         <div class="panel-body">
				       <asp:HiddenField ID="avm7"  runat="server" />
        <asp:HiddenField ID="avm8"  runat="server" />
			
				
					<div class="table-responsive">
					
					<h1 class="heading" style="text-align: center;">
                        <img alt="Logo USAT" src="../assets/images/logousat.png" style="width:110px; height: 110px">
                    </h1>
					
					
					
					<%--<script type="text/javascript" src='../assets/js/datatables/moodletablacursosmatriculados.js'></script>--%>
								<table class="display dataTable" id="moodletablacursosmatriculados" style="width:100%">
									
									<tbody id="tbCursos" >
									</tbody>
									<tfoot>
									<tr>
									<td colspan="9" style="text-align:center">
									 <input type="submit" class="btn btn-primary" id="btnSubmit" value="Ingresar"  />
									 </td>
									</tr>
									</tfoot>
								</table>
							
							<!-- /.table-responsive -->
                    </div>
				
					<!-- /panel -->
				
				</div>
				</div>
			</div>
		</div>
    </form>
</body>
</html>
