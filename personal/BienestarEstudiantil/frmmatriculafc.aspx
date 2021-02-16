<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmmatriculafc.aspx.vb"
    Inherits="frmmatriculafc" %>
    
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title>Matrícula Formación Complementaria</title>
    <link href="../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />  

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>

    <%-- ======================= Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Sweet Alert =============================================--%>

    <script src="../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../assets/js/promise.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            fnLoading(false);
        });
        function fnLoading(sw) {
            console.log(sw);
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            //console.log(sw);
        }
        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 5000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }
        function fnConfirmacion(ctrl, texto, adicional) {
            var defaultAction = $(ctrl).prop("href");
            Swal.fire({
                title: texto,
                text: adicional,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si',
                cancelButtonText: 'No'
            }).then(function(result) {
                if (result.value == true) {
                    fnLoading(true);
                    eval(defaultAction);
                }
            })
        }
        function Validar(ctrl, texto, adicional) {
            if ($("#txtDescripcion").val() == "") {
                fnMensaje('error', 'Debe ingresar la descripción del agregado')
                return false
            } else if ($("#txtDetalle").val() == "") {
                fnMensaje('error', 'Debe ingresar el detalle de la clasificación')
                return false
            } else {
                fnConfirmacion(ctrl, texto, adicional);
            }         
        }
    </script>

    <style type="text/css">
        body
        {
            padding-right: 0 !important;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
        }
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="updLoading" UpdateMode="Conditional">
            <contenttemplate>
                <div class="piluku-preloader text-center">
                    <div class="loader">
                        Loading...</div>
                </div>
            </contenttemplate>
            <triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lbCancelar1" />
                <asp:AsyncPostBackTrigger ControlID="lbRetiro" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="gvLista1" EventName="RowCommand" />--%>
                <asp:AsyncPostBackTrigger ControlID="lbCancelar" />
                <asp:AsyncPostBackTrigger ControlID="lbNuevo" />
                <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
            </triggers>
        </asp:UpdatePanel>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 16px;">
                Matrícula y Retiro de Estudiantes
            </div>
            <div class="panel-body"> 
             <div runat="server" class="col-12" id="Div1">                               
              <table id="tbldatos" border="0" bordercolor="#111111" cellpadding="3" cellspacing="0"
                    >
                    <tr>
                        <td class="style5">
                            Apellidos y Nombres/DNI/Cód.Univ.
                        </td>
                        <td class="style4">
                            <asp:TextBox ID="txtcodigo" runat="server" MaxLength="30" Width ="250"></asp:TextBox>
                            <%--<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" Width="71px" CssClass="btn btn-sm btn-primary btn-radius" />--%>
                            <asp:LinkButton runat="server" ID="cmdBuscar" CssClass="btn btn-sm btn-primary btn-radius"
                                            OnClientClick="fnLoading(true);" Text="<i class='fa fa-search'></i> Buscar">
                                        </asp:LinkButton>
                            <%--<asp:LinkButton runat="server" ID="cmdBuscar" CssClass="btn btn-sm btn-success btn-radius"
                                        
                                        Text='Buscar'></asp:LinkButton>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcodigo"
                                Display="Dynamic" ErrorMessage="Debe ingresar el código universitario, apellidos o dni">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                    <td align="left">
                        <br/> </td>
                    </tr>
                    <%--<tr align="center" style="background-color: #E33439; color: #FFFFFF;" >
                        <td align="center" class="style6">
                            Cód. Universitario</td>
                        <td align="center" class="style2">
                            Apellidos y Nombres</td>
                        <td align="center" class="style3">
                            Carrera Profesional</td>
                        <td align="center" style="height:20px">
                            Estado </td>
                    </tr>--%>

                    <tr align="center">
                        <td align="justify" colspan="12" valign="top" style="width:100%; height:150px">
                         <div id="listadiv" style="height:150px;width:100%;overflow:auto" align="left" >
                                <asp:HiddenField runat="server" ID="hdcod_univ" Value="0" Visible="true" />
                                <asp:GridView ID="GvAlumnos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_alu,Codigouniver_alu" ShowHeader="True"
                                    style="margin-right: 0px" Width="100%" CssClass="table table-condensed">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_Alu" HeaderText="Codigo_Alu" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="Codigo_Alu" 
                                            Visible="False" />
                                        <asp:BoundField DataField="Codigouniver_alu" 
                                            SortExpression="Codigouniver_alu" HeaderText="CÓD. UNIVERSITARIO">
                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="alumno" HeaderText="APELLIDOS Y NOMBRES" 
                                            SortExpression="nombres" ReadOnly="True" >
                                            <ItemStyle Width="140px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_cpf" SortExpression="nombre_cpf" 
                                            HeaderText="ESCUELA">
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estadoactual" 
                                            SortExpression="estado_alu" HeaderText="ESTADO">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_pes" 
                                            SortExpression="codigo_pes" HeaderText="codigo_pes" Visible="False">
                                            <ItemStyle Width="10px" />
                                        </asp:BoundField>
                                        <asp:CommandField SelectText=" " ShowSelectButton="True">
                                            <ItemStyle Width="1px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#FFFFD2" />
                                </asp:GridView>
                                 </div>
                                </td>
                    </tr>
                    <tr>
                        <td rowspan="5" valign="top" align="center" class="style5">
                            <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" />
                        </td>
                        <td class="style7">
                            <asp:Label ID="lbltitcodigo" runat="server">Código Universitario</asp:Label>
                        </td>
                        <td class="style4">
                            &nbsp;<asp:Label ID="lblcodigo" runat="server"></asp:Label>
                        </td>
                       
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lbltitAlumno" runat="server">Apellidos y Nombres</asp:Label>
                        </td>
                        <td class="style4">
                            &nbsp;<asp:Label ID="lblalumno" runat="server"></asp:Label>
                        </td>
                       
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lbltitEscuela" runat="server">Carrera Profesional</asp:Label>
                        </td>
                        <td class="style4">
                            &nbsp;<asp:Label ID="lblescuela" runat="server"></asp:Label>
                            &nbsp;
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lbltitCicloIngreso" runat="server">Semestre de Ingreso</asp:Label>
                        </td>
                        <td class="style4">
                            &nbsp;<asp:Label ID="lblcicloingreso" runat="server"></asp:Label>
                        </td>
                       
                    </tr>
                    <tr>                      
                        <td class="style7">
                            <asp:Label ID="lbltitPlan" runat="server">Plan de Estudio</asp:Label>
                        </td>
                        <td class="style4">
                            &nbsp;<asp:Label ID="lblPlan" runat="server"></asp:Label>
                        </td>
                       
                    </tr>
                    <tr>
                        <td colspan="12">
                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="10pt" ForeColor="Red"></asp:Label>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            </br>
                        </td>
                    </tr>
                    
                    </table>
                     </div>            
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>
                        <div runat="server" id="Lista">
                            <div class="tab-pane" style="background-color: #FFFFFF; color: black; font-weight: bold;
                                font-size: 15px; text-align :center;">
                                Lista de Talleres Disponibles
                            </div>
                            <asp:LinkButton runat="server" ID="lbNuevo" CssClass="btn btn-sm btn-success btn-radius"
                                Text='<span class="fa fa-plus-circle"></span>&nbsp;Agregar' OnClientClick="fnLoading(true);"></asp:LinkButton>
                            <div class="form-group">
                                <asp:GridView runat="server" ID="gvLista" CssClass="table table-condensed" DataKeyNames="codigo_cup,codigo_cur"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nombre_cur" HeaderText="TALLER" HeaderStyle-Width="25%" />                                        
                                        <asp:BoundField DataField="creditos_cur" HeaderText="CRÉ" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="grupohor_cup" HeaderText="GRUPO" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="horario" HeaderText="HORARIO" HeaderStyle-Width="20%" />
                                        <asp:BoundField DataField="docente" HeaderText="DOCENTE" HeaderStyle-Width="30%" />
                                        <asp:BoundField DataField="condicion" HeaderText="ESTADO" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="vac_disp" HeaderText="VACANTES" HeaderStyle-Width="5%" />
                                        
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <%--<ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-edit"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Editar" CommandName="Editar"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" ItemStyle-HorizontalAlign="left" CssClass="form-control"
                                                    Width="5px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron registros</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbNuevo" />
                        <asp:AsyncPostBackTrigger ControlID="lbCancelar" />
                        <asp:AsyncPostBackTrigger ControlID="lbGuardar" />
                        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
                    </triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="form-group" id="DivMantenimiento" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="HiddenField2" Value="0" Visible="false" />                                    
                                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-2 col-md-1 control-label"
                                        For="txtActividad">Actividad</asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:TextBox runat="server" ID="txtActividad" CssClass="form-control" Text=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hdc" Value="0" Visible="false" />                                    
                                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-2 col-md-1 control-label"
                                        For="txtDescripcion">Motivo de Agregado</asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" Text="" TextMode="MultiLine"
                                            Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="form-group text-center">
                                    <asp:LinkButton runat="server" ID="lbGuardar" CssClass="btn btn-sm btn-success btn-radius"
                                        OnClientClick="Validar(this,'¿Está seguro que desea guardar los datos?',''); return false;"
                                        Text='<span class="fa fa-save"></span>&nbsp;Guardar'></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbCancelar" CssClass="btn btn-sm btn-danger btn-radius"
                                        OnClientClick="fnLoading(true);" Text='<span class="fa fa-close"></span>&nbsp;Cancelar'></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbNuevo" />
                        <asp:AsyncPostBackTrigger ControlID="lbCancelar" />
                        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
                    </triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="updLista1" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>
                        <div runat="server" id="Lista1">
                            <div class="tab-pane" style="background-color: #FFFFFF; color: black; font-weight: bold;
                                font-size: 15px; text-align :center;">
                                Talleres Inscritos
                            </div>
                            <asp:LinkButton runat="server" ID="lbRetiro" CssClass="btn btn-sm btn-success btn-radius"
                                Text='<span class="fa fa-less-circle"></span>&nbsp;Retirar' OnClientClick="fnLoading(true);"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lblGrupo" CssClass="btn btn-sm btn-success btn-radius"
                                Text='<span class="fa fa-less-circle"></span>&nbsp;Cambiar Grupo' OnClientClick="fnLoading(true);"></asp:LinkButton>
                            <div class="form-group">
                                <asp:GridView runat="server" ID="gvLista1" CssClass="table table-condensed" DataKeyNames="codigo_cup,codigo_cur"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nombre_cur" HeaderText="TALLER" HeaderStyle-Width="25%" />
                                        <asp:BoundField DataField="creditos_cur" HeaderText="CRÉ" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="grupohor_cup" HeaderText="GRUPO" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="horario" HeaderText="HORARIO" HeaderStyle-Width="20%" />
                                        <asp:BoundField DataField="docente" HeaderText="DOCENTE" HeaderStyle-Width="30%" />
                                        <asp:BoundField DataField="condicion" HeaderText="ESTADO" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="vac_disp" HeaderText="VACANTES" HeaderStyle-Width="5%" />
                                        
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <%--<ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-edit"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Editar" CommandName="Editar"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>--%>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" ItemStyle-HorizontalAlign="left" CssClass="form-control"
                                                    Width="5px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron registros</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </contenttemplate>
                   <triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbRetiro" />
                        <asp:AsyncPostBackTrigger ControlID="lbCancelar1" />
                        <asp:AsyncPostBackTrigger ControlID="lbGuardar1" />
                        <asp:AsyncPostBackTrigger ControlID="gvLista1" EventName="RowCommand" />
                    </triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="form-group" id="DivMantenimiento1" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="HiddenField1" Value="0" Visible="false" />                                    
                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 col-md-1 control-label"
                                        For="txtActividad1">Actividad</asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:TextBox runat="server" ID="txtActividad1" CssClass="form-control" Text=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hdc1" Value="0" Visible="false" />                                    
                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-2 col-md-1 control-label"
                                        For="txtDescripcion1">Motivo de Retiro</asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:TextBox runat="server" ID="txtDescripcion1" CssClass="form-control" Text="" TextMode="MultiLine"
                                            Rows="4"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                            
                            <div class="row">
                                <div class="form-group text-center">
                                    <asp:LinkButton runat="server" ID="lbGuardar1" CssClass="btn btn-sm btn-success btn-radius"
                                        OnClientClick="Validar1(this,'¿Está seguro que desea guardar los datos?',''); return false;"
                                        Text='<span class="fa fa-save"></span>&nbsp;Guardar'></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbCancelar1" CssClass="btn btn-sm btn-danger btn-radius"
                                        OnClientClick="fnLoading(true);" Text='<span class="fa fa-close"></span>&nbsp;Cancelar'></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </contenttemplate>
                   <triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbRetiro" />
                        <asp:AsyncPostBackTrigger ControlID="lbCancelar1" />
                        <asp:AsyncPostBackTrigger ControlID="gvLista1" EventName="RowCommand" />
                    </triggers>
                </asp:UpdatePanel>
              </div>
        </div>                           
      
        </form>
    </div>
</body>
</html>
