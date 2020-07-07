<%@ Page Language="VB" AutoEventWireup="false" CodeFile="loginpersona.aspx.vb" Inherits="login_loginpersona" %>

<html>
<head runat="server">
    <title>Acceso de Personal</title>

    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">

    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>

    <style type="text/css">
        .ocultar
        {
            display: none;
        }

        .filter-option {
            position: relative !important;            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel-group">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4 class="panel-title">
                        <a data-toggle="collapse" href="#collapse1">Instrucciones:</a>
                    </h4>
                </div>
                <div id="collapse1" class="panel-collapse collapse in">
                    <ul class="list-group">
                        <li class="list-group-item">Solo podrán acceder al campus virtual las personas que tengan
                            activo ACCESO CAMPUS (ACTIVO)</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="panel panel-danger" id="pnlLista" runat="server">
            <!--<div class="panel-heading">                                
                <label class="control-label">ACTUALIZAR LOGIN PERSONAL</label>
            </div>-->
            <div class="panel-body">
                <div class="form-group">
                    <div class="row">                        
                        <asp:Label ID="Label1" class="col-md-2" runat="server" Text="Label">Apellidos y Nombres:</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <%--<asp:CheckBox ID="chkClave" runat="server" Text="Expiró clave" TextAlign="Right" Checked="true" />--%>
                            <%--
                            <asp:DropDownList ID="cboClave" runat="server" class="form-control">
                                <asp:ListItem Text="TODAS LAS CLAVES" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="CLAVE CADUCADA" Value="1"></asp:ListItem>
                                <asp:ListItem Text="CLAVE VIGENTE" Value="2"></asp:ListItem>
                                <asp:ListItem Text="SIN USUARIO AD" Value="3"></asp:ListItem>                                
                            </asp:DropDownList>
                            --%>
                            <asp:ListBox ID="cboClave" runat="server" AutoPostBack="true" SelectionMode="Multiple" 
                                CssClass="form-control form-control-sm selectpicker">
                                <asp:ListItem Text="TODAS LAS CLAVES" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="CLAVE CADUCADA" Value="1"></asp:ListItem>
                                <asp:ListItem Text="CLAVE NO CADUCA" Value="4"></asp:ListItem>  
                                <asp:ListItem Text="CLAVE VIGENTE" Value="2"></asp:ListItem>
                                <asp:ListItem Text="SIN USUARIO AD" Value="3"></asp:ListItem>                                                              
                            </asp:ListBox>                            
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="Label">Centro Costos:</asp:Label>
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList ID="cboCentroCosto" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control">
                                <asp:ListItem Text="TODOS LOS ESTADOS" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="HABILITADOS CAMPUS" Value="1"></asp:ListItem>
                                <asp:ListItem Text="DESHABILITADOS CAMPUS" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">                        
                        <asp:Label ID="Label2" class="col-md-2" runat="server" Text="Label">Tipo:</asp:Label>
                        <div class="col-md-3">
                            <%--
                            <asp:DropDownList ID="cboTipo" runat="server" CssClass="btn btn-default dropdown-toggle" SelectionMode="Multiple">
                            </asp:DropDownList>
                            --%>
                            <asp:ListBox ID="cboTipo" runat="server" AutoPostBack="true" SelectionMode="Multiple" 
                                CssClass="form-control form-control-sm selectpicker">
                            </asp:ListBox>                        
                        </div>
                        <asp:Label ID="Label3" class="col-md-1" runat="server" Text="Label">Dedicación:</asp:Label>
                        <div class="col-md-3">                            
                            <%--
                            <asp:DropDownList ID="cboDedicacion" runat="server" CssClass="btn btn-default dropdown-toggle">
                            </asp:DropDownList>
                            --%>
                            <asp:ListBox ID="cboDedicacion" runat="server" AutoPostBack="true" SelectionMode="Multiple" 
                                CssClass="form-control form-control-sm selectpicker">
                            </asp:ListBox>   
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info" />
                            <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn btn-success" />
                        </div>
                    </div>
                    <br />
                    <div class="row">                        
                        <asp:Label ID="Label5" class="col-md-2" runat="server" Text="Label">Ordenar por:</asp:Label>
                        <div class="col-md-2">
                            <asp:DropDownList ID="cboNombres" runat="server" CssClass="btn btn-default dropdown-toggle">
                                <asp:ListItem Value="1" Text="Apellidos y Nombres"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Expira Clave"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-9">
                                <asp:Label ID="LblError" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-9" id="divGrid">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                            DataKeyNames="codigo_per" ForeColor="#333333" GridLines="None" PageSize="100"
                                            CssClass="table table-bordered bs-table">
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="codigo_per" HeaderText="COD." InsertVisible="False" ReadOnly="True"
                                                    SortExpression="codigo_per" />
                                                <asp:BoundField DataField="Apellidos" HeaderText="APELLIDOS" SortExpression="Apellidos" />
                                                <asp:BoundField DataField="nombres_per" HeaderText="NOMBRES" SortExpression="nombres_per" />
                                                <asp:BoundField DataField="usuario_per" HeaderText="USUARIO" SortExpression="usuario_per" />
                                                <asp:BoundField DataField="descripcion_tpe" HeaderText="TIPO" SortExpression="descripcion_tpe" />
                                                <asp:BoundField DataField="descripcion_ded" HeaderText="DEDICACION" SortExpression="descripcion_ded" />
                                                <asp:CheckBoxField DataField="estado" HeaderText="EST. PERS." ReadOnly="True" SortExpression="estado"
                                                    Visible="False" />
                                                <asp:BoundField DataField="descripcion_Cco" ReadOnly="True" SortExpression="estado"
                                                    HeaderText="CENTRO COSTO" />
                                                <asp:BoundField DataField="activo_campus" ReadOnly="True" SortExpression="Campus"
                                                    HeaderText="ACCESO CAMPUS" />
                                                <asp:BoundField DataField="estado_Per" ReadOnly="True" SortExpression="estpersonal"
                                                    HeaderText="ESTADO PERSONAL" />
                                                <asp:BoundField DataField="vigenciaPassword" ReadOnly="True" SortExpression="vencedias"
                                                    HeaderText="EXPIRA CLAVE" />
                                                <asp:BoundField DataField="vigenciaCuenta" ReadOnly="True" SortExpression="vencecta"
                                                    HeaderText="EXPIRA CUENTA" />
                                                <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                                            </Columns>
                                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <EmptyDataTemplate>
                                                No se encontraron registros!
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                Font-Size="12px" />
                                            <RowStyle Font-Size="11px" />
                                            <EditRowStyle BackColor="#ffffcc" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divFormEdit">
                                    <asp:FormView ID="FormView1" runat="server" DataKeyNames="codigo_per" DataSourceID="LoginDetalle"
                                        class="form-horizontal" DefaultMode="Edit">
                                        <EditItemTemplate>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Codigo</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="TextBox2" runat="server" BorderStyle="Solid" BorderWidth="1px" Enabled="False"
                                                        Font-Names="Verdana" Font-Size="8pt" ForeColor="#000040" Text='<%# Bind("codigo_per") %>'
                                                        class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Paterno</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="apellidopat_perTextBox" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        class="form-control" Enabled="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#000040"
                                                        Text='<%# Bind("apellidopat_per") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Materno</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="apellidomat_perTextBox" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        class="form-control" Enabled="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#000040"
                                                        Text='<%# Bind("apellidomat_per") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Nombres</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="nombres_perTextBox" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        class="form-control" Enabled="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#000040"
                                                        Text='<%# Bind("nombres_per") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Sexo</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="sexo_perTextBox" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        class="form-control" Enabled="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#000040"
                                                        Text='<%# Bind("sexo_per") %>' Width="44px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Usuario</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="usuario_perTextBox" runat="server" Text='<%# Bind("usuario_per") %>'
                                                        class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Login</label>
                                                <div class="col-md-8">
                                                    <asp:TextBox ID="login_perTextBox" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                        class="form-control" Enabled="False" Font-Names="Verdana" Font-Size="8pt" ForeColor="#000040"
                                                        Text='<%# Bind("login_per") %>'></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">
                                                    Estado</label>
                                                <div class="col-md-8">
                                                    <asp:CheckBox ID="estadoCheckBox" runat="server" Checked='<%# Bind("estado") %>'
                                                        Text="Habilitado" TextAlign="Right" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Actualizar" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:LinkButton>
                                                <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancelar" Font-Bold="True" Font-Names="Verdana" ForeColor="Black"></asp:LinkButton>
                                            </div>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            apellidopat_per:
                                            <asp:TextBox ID="apellidopat_perTextBox" runat="server" Text='<%# Bind("apellidopat_per") %>'>
                                            </asp:TextBox><br />
                                            apellidomat_per:
                                            <asp:TextBox ID="apellidomat_perTextBox" runat="server" Text='<%# Bind("apellidomat_per") %>'>
                                            </asp:TextBox><br />
                                            nombres_per:
                                            <asp:TextBox ID="nombres_perTextBox" runat="server" Text='<%# Bind("nombres_per") %>'>
                                            </asp:TextBox><br />
                                            sexo_per:
                                            <asp:TextBox ID="sexo_perTextBox" runat="server" Text='<%# Bind("sexo_per") %>'>
                                            </asp:TextBox><br />
                                            login_per:
                                            <asp:TextBox ID="login_perTextBox" runat="server" Text='<%# Bind("login_per") %>'>
                                            </asp:TextBox><br />
                                            usuario_per:
                                            <asp:TextBox ID="usuario_perTextBox" runat="server" Text='<%# Bind("usuario_per") %>'>
                                            </asp:TextBox><br />
                                            estado:
                                            <asp:CheckBox ID="estadoCheckBox" runat="server" Checked='<%# Bind("estado") %>' /><br />
                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                Text="Insertar">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                Text="Cancelar">
                                            </asp:LinkButton>
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            codigo_per:
                                            <asp:Label ID="codigo_perLabel" runat="server" Text='<%# Eval("codigo_per") %>'></asp:Label><br />
                                            apellidopat_per:
                                            <asp:Label ID="apellidopat_perLabel" runat="server" Text='<%# Bind("apellidopat_per") %>'></asp:Label><br />
                                            apellidomat_per:
                                            <asp:Label ID="apellidomat_perLabel" runat="server" Text='<%# Bind("apellidomat_per") %>'></asp:Label><br />
                                            nombres_per:
                                            <asp:Label ID="nombres_perLabel" runat="server" Text='<%# Bind("nombres_per") %>'></asp:Label><br />
                                            sexo_per:
                                            <asp:Label ID="sexo_perLabel" runat="server" Text='<%# Bind("sexo_per") %>'></asp:Label><br />
                                            login_per:
                                            <asp:Label ID="login_perLabel" runat="server" Text='<%# Bind("login_per") %>'></asp:Label><br />
                                            usuario_per:
                                            <asp:Label ID="usuario_perLabel" runat="server" Text='<%# Bind("usuario_per") %>'></asp:Label><br />
                                            estado:
                                            <asp:CheckBox ID="estadoCheckBox" runat="server" Checked='<%# Bind("estado") %>'
                                                Enabled="false" /><br />
                                        </ItemTemplate>
                                    </asp:FormView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel-body">
                        <span id="spNroRegistros"></span>
                    </div>
                </div>
            </div>
        </div>
        <table style="width: 100%">
            <tr>
                <td valign="top" colspan="2">
                </td>
                <td valign="top">
                    <asp:ObjectDataSource ID="LoginDetalle" runat="server" SelectMethod="ObtieneLoginDetalle"
                        TypeName="Personal">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="GridView1" DefaultValue="1" Name="param1" PropertyName="SelectedValue"
                                Type="String" />
                            <asp:Parameter DefaultValue="1" Name="param2" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    &nbsp;
                    <br />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvExportar" runat="server" AutoGenerateColumns="False" CellPadding="3"
            DataKeyNames="codigo_per" Visible="false" ForeColor="#333333" GridLines="None"
            CssClass="table table-bordered bs-table">
            <Columns>
                <asp:BoundField DataField="codigo_per" HeaderText="COD." InsertVisible="False" ReadOnly="True" />
                <%-- <asp:BoundField DataField="apellidopat_per" HeaderText="AP. PAT." /> --%>
                <%-- <asp:BoundField DataField="apellidomat_per" HeaderText="AP. MAT." /> --%>
                <asp:BoundField DataField="Apellidos" HeaderText="APELLIDOS" />
                <asp:BoundField DataField="nombres_per" HeaderText="NOMBRES" />
                <%-- <asp:BoundField DataField="sexo_per" HeaderText="SEXO" /> --%>
                <%-- <asp:BoundField DataField="login_per" HeaderText="LOGIN" /> --%>
                <asp:BoundField DataField="usuario_per" HeaderText="USUARIO" />
                <asp:BoundField DataField="descripcion_tpe" HeaderText="TIPO" />
                <asp:BoundField DataField="descripcion_ded" HeaderText="DEDICACION" />
                <%-- <asp:CheckBoxField DataField="estado" HeaderText="EST. PERS." ReadOnly="True" Visible="False" />  --%>
                <asp:BoundField DataField="descripcion_Cco" ReadOnly="True" HeaderText="CENTRO COSTO" />
                <asp:BoundField DataField="activo_campus" ReadOnly="True" HeaderText="ACCESO CAMPUS" />
                <asp:BoundField DataField="estado_Per" ReadOnly="True" HeaderText="ESTADO PERSONAL" />
                <asp:BoundField DataField="vigenciaPassword" ReadOnly="True" HeaderText="EXPIRA CLAVE" />
                <asp:BoundField DataField="vigenciaCuenta" ReadOnly="True" HeaderText="EXPIRA CUENTA" />
            </Columns>
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataTemplate>
                No se encontraron registros!
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                Font-Size="12px" />
            <RowStyle Font-Size="11px" />
            <EditRowStyle BackColor="#ffffcc" />
            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
        </asp:GridView>
    </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#cboTipo').selectpicker({
                noneSelectedText: '-- SELECCIONE --',
            });

            $('#cboDedicacion').selectpicker({
                noneSelectedText: '-- SELECCIONE --',
            });

            $('#cboClave').selectpicker({
                noneSelectedText: '-- SELECCIONE --',
            });            
        });    
    </script>
</body>
</html>
