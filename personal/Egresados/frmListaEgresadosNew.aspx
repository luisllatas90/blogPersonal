<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaEgresadosNew.aspx.vb" Inherits="Egresados_frmListaEgresadosNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Comunicación Egresado</title> 
    <!-- custom scrollbar stylesheet -->   
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <link href="css/paginacion.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
        <div class="container-fluid">
            <div></div>
            <br />
            <div class="panel panel-default" id="pnlLista" runat="server">
                 <div class="panel panel-heading">
                    <h4>
                        Comunicación Egresados</h4>
                </div>
                <div class="panel panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Nivel:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlNivel" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                             <div class="form-group">
                                <label class="col-md-4">
                                    Facultad:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlFacultad" runat="server" CssClass="form-control input-sm" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>                        
                        </div>
                        <div class="col-md-4">
                             <div class="form-group">
                                <label class="col-md-4">
                                    Carrera Profesional :</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlEscuela" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>                        
                        </div>
                    </div> 
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Modalidad:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlModalidad" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>    
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Año Egreso:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlEgreso" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>        
                            </div>                            
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Año Bachiller:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlBachiller" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>                                        
                            </div>                            
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                 <label class="col-md-4">
                                    Año Título:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlTitulo" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>                   
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Género:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="%">TODOS</asp:ListItem>
                                        <asp:ListItem>M</asp:ListItem>
                                        <asp:ListItem>F</asp:ListItem>
                                    </asp:DropDownList>
                                </div>                   
                            </div>                            
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Nombres:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtApellidoNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>                   
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        
                    </div>                    
                </div>
            </div>        
        </div>
    </form>
</body>
</html>
