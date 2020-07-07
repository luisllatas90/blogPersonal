<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pondescuelaciclo.aspx.vb" Inherits="pondescuelaciclo" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consultar Promedio Ponderado</title>
        <link href="../../private/estilo.css?x=2" rel="stylesheet" type="text/css" />
        <!--<script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>-->
        <script src="../Scripts/js1_12/jquery-1.12.3.min.js" type="text/javascript"></script>
        <!--
        <link href="../Scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
        <script src="../Scripts/js1_12/bootstrap.min.js" type="text/javascript"></script>
        <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
        <script src="private/jquery.loadmask.js" type="text/javascript"></script>        
        -->
        <script type="text/javascript" language="javascript">
            $(document).ready(function() {                
                $("#cmdBuscar").on("click", function() {
                    $('#divLoading').show();
                });                                             
        });

        function Ocultar() {
            $('#divLoading').hide();    
        }
	
	
        </script>
    <style type="text/css">
        span
        {
            cursor: pointer
        }
        
        a:Link
        {
        	color: #000000;
	        text-decoration: underline;
        }
        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 120px;
            height: 120px;
            animation: spin 2s linear infinite;
            position: absolute;
            left: 35%;
            top: 35%;
        }

        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="divLoading" class="loader"></div>
    <p class="usattitulo">Cuadro de Méritos Estudiante por Carrera Profesional</p>
    <table cellpadding="3" cellspacing="0" style="width:100%;" id="tblCriterios">
        <tr>
            <td height="5%" width="20%">Carrera Profesional</td>
            <td height="5%" width="80%">
                <asp:DropDownList ID="dpEscuela" runat="server" AutoPostBack="True" Width="100%">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">Semestre de Ingreso:</td>
            <td height="5%" width="80%">
                <asp:DropDownList ID="dpIngreso" runat="server">
                </asp:DropDownList>
            &nbsp;&nbsp; Semestre Matriculado:
                <asp:DropDownList ID="dpCiclo" runat="server">
                </asp:DropDownList>
            &nbsp;Según:
                <asp:DropDownList ID="dpFiltro" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="C">Cuadro Completo</asp:ListItem>
                    <asp:ListItem Value="T">Tercio Superior</asp:ListItem>
                    <asp:ListItem Value="Q">Quinto Superior</asp:ListItem>
                    <asp:ListItem Value="D">Décimo Superior (sólo PRONABEC)</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">&nbsp;</td>
            <td height="5%" width="80%">
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" CssClass="buscar" />
                <asp:Button ID="cmdExportar" runat="server" CssClass="excel2" 
                    Text="  Exportar" style="width:100px; height: 24px;"
                    onclientclick="document.all.tblmensaje.style.display='none'" />
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%" colspan="2" style="width: 100%">
                <asp:Label ID="Label2" runat="server" ForeColor="Red" 
                    
                    Text="- Para el cálculo del promedio se consideran los créditos efectivamente cursados, se excluyen: Convalidaciones, exámenes extraordinarios.  (Art. 43)" 
                    Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                    
                    Text="- Se excluye del cuadro de mérito: Estudiantes que no se encuentran dentro del periodo regular de acuerdo al plan de estudios. (Art. 45)" 
                    Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" ForeColor="Red" 
                    
                    Text="- Para la ubicación del cuadro de mérito en el caso que dos estudiantes tengan igual promedio ponderado acumulado se preferirá aquel estudiante que no desaprobó ninguna asignatura. (Art. 45)" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="5%" width="100%" colspan="2">
                <!--<div id="listadiv" style="height:400px" class="contornotabla">-->
                <div id="modalBody">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" AllowSorting="True" Width="100%" 
                        DataKeyNames="codigo_alu" GridLines="None">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField HeaderText="ORDEN">
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" />
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigouniver_alu" HeaderText="CODIGO">
                                <HeaderStyle Font-Underline="False" Font-Names="Arial" Font-Size="X-Small" />
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="alumno" HeaderText="APELLIDOS Y NOMBRES">
                                <HeaderStyle Font-Underline="False" Font-Names="Arial" Font-Size="X-Small" 
                                    Height="10px" />
                                <ItemStyle Width="40%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="SEMESTRE INGRESO" DataField="cicloing_alu">
                                <HeaderStyle Font-Underline="False" Font-Names="Arial" Font-Size="X-Small" 
                                    Height="10px" />
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numSem" HeaderText="NUM SEMESTRES">
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="credMat" HeaderText="CRED MATRICULADOS">
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="credApro" HeaderText="CRED APROBADOS">
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DifCred" HeaderText="DIFF CREDITOS">
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" Height="10px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ponderado" HeaderText="PPA" 
                                DataFormatString="{0:F2}">
                                <HeaderStyle Font-Underline="False" Font-Names="Arial" Font-Size="X-Small" 
                                    Height="10px" />
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ModIngreso" HeaderText="MODALIDAD DE INGRESO">
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_alu" HeaderText="codigo_alu" 
                                Visible="False" />
                            <asp:TemplateField HeaderText="Consultas" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistorial" runat="server" Font-Underline="True" 
                                        ForeColor="Blue" Text="Historial"></asp:Label>
                                    &nbsp;|
                                    <asp:Label ID="lblBeca" runat="server" Font-Underline="True" ForeColor="Blue" 
                                        Text="Beneficio"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Names="Arial" Height="10px" />
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado registros con el criterio 
                                seleccionado</p>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
                
                <!--
                <asp:ObjectDataSource ID="objPonderado" runat="server" 
                    SelectMethod="ConsultarPromedioOficial" TypeName="Academico">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="dpEscuela" DefaultValue="0" Name="param1" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="dpIngreso" DefaultValue="0" Name="param2" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="dpCiclo" DefaultValue="0" Name="param3" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="dpFiltro" DefaultValue="C" Name="param4" 
                            PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                -->
                <!--</div>-->
            </td>
        </tr>
        </table>
    </form>
    <!--
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
    -->
</body>
</html>
