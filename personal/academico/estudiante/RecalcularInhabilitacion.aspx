<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RecalcularInhabilitacion.aspx.vb" Inherits="academico_estudiante_RecalcularInhabilitacion" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=8; IE=9; " />-->
    <title>Alumnos Inhabilitados</title>    
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
    <script type="text/javascript">
        function MarcarCursos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');

            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        // PintarFilaMarcada(chk.parentNode.parentNode,obj.checked)
                    }
                }
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

       

        
    </script>
</head>
<body>    
<br />
    <form id="form1" class="form form-horizontal" runat="server"> 
    
       <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
  
    <div class="container-fluid">
   
    <asp:Panel CssClass="panel panel-primary" id="pnlLista"  runat="server" style="padding:0px;">
            <div class="panel panel-heading"  >
                <h5>Re-Procesar Inhabilitaciones</h5>
            </div>
            <div class="panel panel-body"  style="padding:3px;">            
            <div class="row">
                    <div class="col-md-12">
                     <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlTipoTramiteBsq">
                                Semestre Acad.</label>
                                <div class="col-md-8">                            
                                <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                     </div>
                     <div class="col-md-6">
                         <div class="form-group">
                                <label class="col-md-4" for="cboCarrera">
                                    Carrera Profesional</label>
                                    <div class="col-md-8">  
                                    <asp:DropDownList ID="cboCarrera" runat="server"  CssClass="form-control" AutoPostBack=true>
                                    </asp:DropDownList>
                                </div>
                            </div>
                     </div>
                     <div class="col-md-12">
                       <div class="form-group">
                                <label class="col-md-2" for="cboCursos">
                                   Asignatura</label>
                                    <div class="col-md-10">  
                                     <asp:DropDownList ID="cboCursos" runat="server"  CssClass="form-control" AutoPostBack="True">
                                        </asp:DropDownList>
                                </div>
                            </div>
                     </div>
                     
                    </div>
                    <div class="col-md-12">
                    <div class="col-md-6">
                       <div class="form-group">
                                <label class="col-md-4" for="cboEstado">
                                    Estudiantes</label>
                                    <div class="col-md-8">  
               <asp:DropDownList ID="cboEstado" runat="server"  CssClass="form-control" AutoPostBack="True">
               <asp:ListItem Value="%">Todos</asp:ListItem>
               <asp:ListItem Selected="True" Value="1">Inhabilitados</asp:ListItem>
               <asp:ListItem Value="0">No Inhabilitados</asp:ListItem>
           </asp:DropDownList>
                                </div>
                            </div>
                     </div>
                        <div class="col-md-6">
                        
                        <div class="btn-group" role="group" aria-label="Basic example">                           
                           
                           
                               <asp:LinkButton ID="btnRecalcular" runat="server" Text='<i class="fa fa-plus"></i> Re - Procesar'
                            CssClass="btn btn-success btn-sm" ></asp:LinkButton>
                      
                        </div>
                        </div>
                    </div>
            </div>  
                    </div>
                    <div class="panel panel-body" style="padding:3px;">
                     <div class="messagealert" id="alert_container"></div>
                     <br />
                    <div class="table-responsive">
                    
                    
           <asp:GridView ID="gridAlumnos" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_dma,codigo_alu, inh"  CssClass="table table-sm table-bordered table-hover" GridLines="None" RowStyle-Font-Size=X-Small  >
               <Columns>
                   
                  <asp:TemplateField HeaderText="Sel" ItemStyle-Width=3% >
                  
                   <HeaderTemplate>
                        <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                    </HeaderTemplate>
                    <ItemTemplate>                                             
                        <asp:CheckBox ID="chkElegir" runat="server" onclick="PintarFilaMarcada2(this.parentNode.parentNode,this.checked)" />                                                
                    </ItemTemplate>
                  </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="#" ItemStyle-Width="30"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                   <asp:BoundField DataField="codigo_dma" HeaderText="codigo_dma" 
                       Visible="False" />
                   <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" 
                       Visible="False" />
                   <asp:BoundField DataField="codigoUniver_alu" HeaderText="CÓDIGO UNIVERSITARIO" />
                   <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" />
                   <asp:BoundField DataField="faltas" HeaderText="FALTAS INH" ItemStyle-Width="30" HeaderStyle-BackColor="SlateGray" ItemStyle-HorizontalAlign="Center"/>
                   <asp:BoundField DataField="justi" HeaderText="JUST. INH" ItemStyle-Width="30" HeaderStyle-BackColor="SlateGray" ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="sesiones" HeaderText="SE.EFE. INH" ItemStyle-Width="30" HeaderStyle-BackColor="SlateGray"  ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="porcentaje" HeaderText="%INAS. INH" ItemStyle-Width="30" HeaderStyle-BackColor="SlateGray"  ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="faltasA" HeaderText="FALTAS ACTUAL" ItemStyle-Width="30"  HeaderStyle-BackColor="SandyBrown"  ItemStyle-HorizontalAlign="Center"/>
                   <asp:BoundField DataField="justiA" HeaderText="JUST. ACTUAL " ItemStyle-Width="30" HeaderStyle-BackColor="SandyBrown" ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="sesionesA" HeaderText="SE.EFEC. ACTUAL" ItemStyle-Width="30" HeaderStyle-BackColor="SandyBrown" ItemStyle-HorizontalAlign="Center" />
                   <asp:BoundField DataField="porcentajeA" HeaderText="%INAS. ACTUAL" ItemStyle-Width="30" HeaderStyle-BackColor="SandyBrown" ItemStyle-HorizontalAlign="Center"/>
               </Columns>
                 <EmptyDataTemplate>
                    No se encontró ningún registro
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="10px" />
                <EditRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
           </asp:GridView>
                    </div>
                    </div>
    </asp:Panel>
    </div>
       <div>
         
        
           <br />
           
          
         
        
     
    
        
   
    </form>
</body>
</html>
