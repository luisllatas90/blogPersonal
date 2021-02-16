<%@ Page Language="VB" AutoEventWireup="false" CodeFile="historialacademico.aspx.vb" Inherits="historialacademico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src='assets/js/jquery.dataTables.min.js'></script>    
    <link rel='stylesheet' href='assets/css/jquery.dataTables.min.css'/>
   <%-- <script type="text/javascript" src='assets/js/datatables/detallehistorialcurso.js'></script> --%>  
   
        <script  type="text/javascript" >
            jQuery(document).ready(function() {
                $("#cbocpf").change(function() {
                var eleccioncbo = this.value;
                  //  alert(eleccioncbo);                             
               
            });
        });
                            
        </script>
   
</head>
<body>
    <div class="col-md-12">
        <!-- *** Responsive Tables *** -->
        <!-- panel -->
        <div class='panel panel-piluku'>
            <div class='panel-heading'>
                <h3 class='panel-title' id="h3Titulo"  runat="server"></h3>
            </div>
	        <div class='table-responsive'>
	            
                <div class='panel-body'>
                <div id="pCabecera" class="row" runat ="server">                
                </div>
                <div class="row">
                <script type="text/javascript" src='assets/js/datatables/historial.js?bc=1&fg=3'></script>
                <table id="tbHistorial" style="width:100%"  class='display dataTable cell-border'>
                <thead>
                <tr>
                <th>Ciclo</th>
                <th>Semestre</th>
                <th>&Aacute;rea</th>
                <th>Curso</th>
                <th>Crd.</th>
                <th>Grupo</th>
                <th>Veces Desaprob.</th>                
                <th>Nota Final</th>
                <th>Condición</th>
                <th>Detalles</th>
                <th>Plan Est - Escuela</th>
                </tr>
                </thead>  
                <tbody id="tbdHistorial" runat="server">
                </tbody>    
                <thead>
                <tr>
                <th colspan="11"></th>
                </tr></thead>       
                </table>
                </div>
                <br />
                <div class="row">
                <div class="col-sm-4">
                * Matrícula por Convalidación <br />
                ** Matrícula por Examen de Ubicación<br /> 
                *** Matrícula por Examen de Suficiencia <br />
                </div>
                <div class="col-sm-8">
               <table class='table table-bordered' id="tbresumen">
               <tr><th class='text-center' style ='line-height:1;'>Resumen</th><th class='text-center' style ='line-height:1;'>Matrícula Regular</th><th class='text-center' style ='line-height:1;'>Matrícula por Convalidación</th><th class='text-center' style ='line-height:1;'>Total</th></tr>
               <tr><td style ='line-height:1;'>Créditos Aprobados</td><td class='text-right' style ='line-height:1;'><span id="CAR"></span></td><td class='text-right' style ='line-height:1;'><span id="CAC"></span></td><td class='text-right' style ='line-height:1;'><span id="CARCAC"></span></td></tr>
               <tr><td style ='line-height:1;' style ='line-height:1;'>Asignaturas Aprobadas</td><td class='text-right' style ='line-height:1;'><span id="AAR"></span></td><td class='text-right' style ='line-height:1;'><span id="AAC"></span></td><td class='text-right' style ='line-height:1;'><span id="AARAAC"></span></td></tr>
               </table>
                </div>
                </div>
                <div id="pDatos"  runat ="server">
                </div>
                    <div class='modal fade' id='largemodal' tabindex='-1' role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>  
                    <div class='modal-dialog-lg'>   
                    <div class='modal-content'>     
                    <div class='modal-header'>  
                   <button type='button' class='close' data-dismiss='modal' aria-label='Close'><span aria-hidden='true' class='ti-close'></span></button>   
                   
                    </div>
                                <div class='modal-body'> <br /><br />  
                                 <h4 class='modal-title' id='titulo'></h4>    
                                  <div class='table-responsive'>
                                <table class='table table-bordered' id='tablaDetalleCurso'> 
                                </table>
                                </div>
                                </div>
                                </div>
                            </div>
                            </div>
                </div>
                <!-- 20200814-ENevado ------------------------------------------------------------ -->
                <div class='panel-body' id="divFormComp">
                    <div id="pCabecera2" class="row" runat ="server">                
                    </div>
                    <div class="row">
                        <table id="tbHistoria2" style="width:100%"  class='display dataTable cell-border'>
                            <thead>
                                <tr>
                                    <th>Ciclo</th>
                                    <th>Semestre</th>
                                    <th>&Aacute;rea</th>
                                    <th>Actividad Complementaria</th>
                                    <th>Crd.</th>
                                    <th>Grupo</th>
                                    <th>Veces No Acredita</th>                
                                    <th>Condición</th>
                                    <th>Plan Estudio</th>
                                </tr>
                            </thead>  
                            <tbody id="tbdHistorial2" runat="server">
                            </tbody>    
                            <thead>
                                <tr>
                                    <th colspan="11"></th>
                                </tr>
                            </thead>       
                        </table>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                        </div>
                        <div class="col-sm-8">
                           <table class='table table-bordered' id="tbresumen2">
                            <tr>
                                <th class='text-center' style ='line-height:1;'>Resumen</th>
                                <th class='text-center' style ='line-height:1;'>Inscripción Regular</th>
                            <tr>
                                <td style ='line-height:1;'>Créditos Aprobados</td>
                                <td class='text-right' style ='line-height:1;'><span id="FCCA"></span></td>
                            </tr>
                            <tr>
                                <td style ='line-height:1;' style ='line-height:1;'>Actividades Acreditadas</td>
                                <td class='text-right' style ='line-height:1;'><span id="FCAA"></span></td>
                            </tr>
                           </table>
                        </div>
                    </div>
                </div>
                <!-- ----------------------------------------------------------------------------- -->
            </div>
            
        </div>
    </div> 
</body>
</html>
