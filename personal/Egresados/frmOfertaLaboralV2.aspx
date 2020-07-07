<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOfertaLaboralV2.aspx.vb" Inherits="Egresados_frmOfertaLaboralV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>USAT Oferta Laboral</title>
   <%-- Bootstraps--%>
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="js/popper.js" type="text/javascript"></script>
    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>
   <%-- Iconos y fuentes--%>
   <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>   
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" /> 
     <%-- Estilo para fechas --%>
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        $(document).ready(function() {
        $('#txtFechIni').datepicker({
            autoclose: true, orientation: "top"
        });
        $('#txtFechFin').datepicker({
            autoclose: true, orientation: "top"
        });
        $('#btn_calendIni').click(function() {
            $("#txtFechIni").datepicker('show');
        });
        $('#btn_calendFin').click(function() {
            $("#txtFechFin").datepicker('show');
        });


    });  //fin jQuery 
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div></div>
            <br />
            <div class="panel panel-default" id="pnlLista" runat="server">
                <div class="panel panel-heading">
                    <div class="row">
                        <div class="col-md-8">
                            <h4>Agregegar Oferta Laboral</h4>
                        </div>
                    </div>
                </div>
                <div class="panel panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                 <label class="col-md-3">Título:</label>
                                 <div class="col-md-9">
                                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>                                
                            </div>                               
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                 <label class="col-md-3">Empresa:</label>
                                 <div class="col-md-7">
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>
                                 <div class="col-md-1">
                                     <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>                                     
                                 </div>                                
                                 <div class="col-md-1">
                                     <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-sm btn-primary" Text='<i class="fa fa-plus"></i>'></asp:LinkButton>                                     
                                 </div>                                
                            </div>                               
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                 <label class="col-md-3">Descripción:</label>
                                 <div class="col-md-9">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" Rows="3" 
                                         TextMode="MultiLine"></asp:TextBox>
                                 </div>                                
                            </div>                               
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                 <label class="col-md-3">Requisitos:</label>
                                 <div class="col-md-9">
                                    <asp:TextBox ID="txtRequisitos" runat="server" CssClass="form-control" Rows="3" 
                                         TextMode="MultiLine"></asp:TextBox>
                                 </div>                                
                            </div>                               
                        </div>
                    </div>
                    <br />
                    <div class="row">
                       <div class="col-md-4">
                        <div class="form-group">
                            <label class="col-md-5">Departamento:</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>                        
                       </div> 
                         <div class="col-md-4">
                            <div class="form-group">
                                 <label class="col-md-3">Lugar:</label>
                                 <div class="col-md-9">
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>                                
                            </div>                               
                        </div>
                      
                       <div class="col-md-4">
                         <div class="form-group">
                            <label class="col-md-5">Tipo Oferta:</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>          
                       </div> 
                    </div>
                    <br />
                    <div class="row">
                       <div class="col-md-4">
                            <div class="form-group">
                            <label class="col-md-5">Sector:</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            </div>         
                        </div>
                    
                        <div class="col-md-4">
                            <div class="form-group">
                                 <label class="col-md-3">Contacto:</label>
                                 <div class="col-md-9">
                                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>                                
                            </div>                               
                        </div>                        
                        <div class="col-md-4">
                            <div class="form-group">
                                 <label class="col-md-5">Teléfono:</label>
                                 <div class="col-md-7">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                                 </div>                                
                            </div>                               
                        </div>                                                
                    </div>
                    <br />
                    <div class="row">
                         <div class="col-md-4">
                         <div class="form-group">
                            <label class="col-md-5">Tipo Trabajo:</label>
                            <div class="col-md-7">
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>          
                       </div>                        
                        <div class="col-md-4">
                           <div class="form-group">
                                 <label class="col-md-5">Publicación:</label>
                                   <div class="input-group date">
                                     <asp:TextBox ID="txtFechIni" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg" id="btn_calendIni"><i class="fa fa-calendar"></i></span>
                                   </div>
                            </div>                     
                        </div>
                        <div class="col-md-4">
                             <div class="form-group">
                                 <label class="col-md-6">Fin Publica :</label>
                                   <div class="input-group date">
                                     <asp:TextBox ID="txtFechFin" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg" id="btn_calendFin"><i class="fa fa-calendar"></i></span>
                                   </div>
                                   
                            </div>
                        </div>                        
                    </div>
                    
                    <br /> 
                    <div class="row">
                        <div class="col-md-8">
                             <div class="form-group">
                                 <label class="col-md-2">Correo:</label>
                                 <div class="col-md-10">
                                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                 </div>
                                 
                            </div>          
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-md-12">                                    
                                <asp:CheckBox ID="CheckBox1" runat="server" Text=" Mostrar Correo" /> 
                            </div>                                                
                            </div>                            
                        </div>
                    </div>
                    <br />   
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                 <label class="col-md-2">Web: http://</label>
                                 <div class="col-md-10">
                                    <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>                                    
                                 </div>                                 
                            </div>  
                        </div>
                        <div class="col-md-4">
                             <label class="col-md-4">Postular vía:</label>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                 RepeatDirection="Horizontal">
                                <asp:ListItem> Web_</asp:ListItem>
                                <asp:ListItem> Correo_</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>                        
                    </div> 
                                       
                </div>    
                <div class="panel panel-footer">
                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-success" Text="Guardar"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-default" Text="Regresar"></asp:LinkButton>
                </div>          
            </div>
        </div>
    </form>
</body>
</html>
