<%@ Page Language="VB" EnableEventValidation="false" AutoEventWireup="false" CodeFile="frmCambioDeGrupo.aspx.vb" Inherits="academico_matricula_administrar_frmCambioDeGrupo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambio de Grupo</title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
    <script src="../../../private/funciones.js" type ="text/javascript" language="javascript"></script>
    <style type="text/css">
   .Borrarlineasuperior
        {
            border-left-width: 0;
	        border-right-width: 0;
	        border-top: 1px solid #FFFFFF;
	        border-bottom-width: 0;
        }
    .FondoBlanco
    {
	    background-color:White;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:Panel ID="pnlDatos" runat="server" BorderColor="#333333" 
                        BorderStyle="Solid" BorderWidth="1px" Visible="False" Height="150px">
                        <table style="width:100%; height: 100%;">
                            <tr>
                                <td align="center">
                                    <asp:Image ID="ImgFoto" runat="server" Height="107px" Width="90px" 
                                        BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
                                </td>
                                <td>
                                    <asp:FormView ID="FvDatos" runat="server" 
                                        Width="600px" 
                CssClass="FondoBlanco">
                                        <EditItemTemplate>
                                            alumno:
                                            <asp:TextBox ID="alumnoTextBox1" runat="server" Text='<%# Bind("alumno") %>' />
                                            <br />
                                            cicloing_alu:
                                            <asp:TextBox ID="cicloing_aluTextBox1" runat="server" 
                                                Text='<%# Bind("cicloing_alu") %>' />
                                            <br />
                                            nombre_min:
                                            <asp:TextBox ID="nombre_minTextBox1" runat="server" 
                                                Text='<%# Bind("nombre_min") %>' />
                                            <br />
                                            nombre_cpf:
                                            <asp:TextBox ID="nombre_cpfTextBox1" runat="server" 
                                                Text='<%# Bind("nombre_cpf") %>' />
                                            <br />
                                            descripcion_pes:
                                            <asp:TextBox ID="descripcion_pesTextBox1" runat="server" 
                                                Text='<%# Bind("descripcion_pes") %>' />
                                            <br />
                                            <asp:LinkButton ID="UpdateButton0" runat="server" CausesValidation="True" 
                                                CommandName="Update" Text="Actualizar" />
                                            &nbsp;<asp:LinkButton ID="UpdateCancelButton0" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            alumno:
                                            <asp:TextBox ID="alumnoTextBox2" runat="server" Text='<%# Bind("alumno") %>' />
                                            <br />
                                            cicloing_alu:
                                            <asp:TextBox ID="cicloing_aluTextBox2" runat="server" 
                                                Text='<%# Bind("cicloing_alu") %>' />
                                            <br />
                                            nombre_min:
                                            <asp:TextBox ID="nombre_minTextBox2" runat="server" 
                                                Text='<%# Bind("nombre_min") %>' />
                                            <br />
                                            nombre_cpf:
                                            <asp:TextBox ID="nombre_cpfTextBox2" runat="server" 
                                                Text='<%# Bind("nombre_cpf") %>' />
                                            <br />
                                            descripcion_pes:
                                            <asp:TextBox ID="descripcion_pesTextBox2" runat="server" 
                                                Text='<%# Bind("descripcion_pes") %>' />
                                            <br />
                                            <asp:LinkButton ID="InsertButton0" runat="server" CausesValidation="True" 
                                                CommandName="Insert" Text="Insertar" />
                                            &nbsp;<asp:LinkButton ID="InsertCancelButton0" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="130">
                                                        Codigo Universitario</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="codigouniver_aluLabel0" runat="server" 
                                                            Text='<%# Bind("codigouniver_alu") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="130">
                                                        Apellidos y nombres</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="alumnoLabel0" runat="server" Text='<%# Bind("alumno") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ciclo de ingreso</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="cicloing_aluLabel0" runat="server" 
                                                            Text='<%# Bind("cicloing_alu") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Modalidad de ingreso</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="nombre_minLabel0" runat="server" 
                                                            Text='<%# Bind("nombre_min") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Carrera profesional</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="nombre_cpfLabel0" runat="server" 
                                                            Text='<%# Bind("nombre_cpf") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Plan de estudios</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="descripcion_pesLabel0" runat="server" 
                                                            Text='<%# Bind("descripcion_pes") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        estado Actual</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="estadoActual_AluLabel0" runat="server" 
                                                            Text='<%# IIF(eval("estadoActual_Alu")=1, "Activo", "Inactivo") %>' 
                                                            ForeColor="Blue" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:FormView>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFCC" id="rest" runat="server">
                    <img alt="" src="../../../images/credito.gif" /> Importante: Sólo puede realizar 
                    <b>2 cambios 
                    de grupo</b> por día</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvDetalleMatricula" runat="server" 
                        AutoGenerateColumns="False" DataKeyNames="codigo_cup,codigo_dma,PermitirRetiro" 
                        Width="100%" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField DataField="tipocurso_dma" HeaderText="Tipo" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grupohor_cup" HeaderText="GH" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="identificador_cur" HeaderText="Código" />
                            <asp:BoundField DataField="nombre_cur" HeaderText="Descripción" />
                            <asp:BoundField DataField="creditoCur_dma" HeaderText="Crd" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="vecescurso_dma" HeaderText="Veces" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estadocurso" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField ShowSelectButton="True" >
                            <ItemStyle ForeColor="#000099" />
                            </asp:CommandField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblMatricula" runat="server" BorderStyle="None" ForeColor="Red" 
                                Text="El estudiante no se ha matriculado en el ciclo seleccionado"></asp:Label>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#ECFFFF" />
                        <HeaderStyle BackColor="#0066CC" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="rblHorarios" ErrorMessage="Grupo sin vacantes o Cerrado" 
                        MaximumValue="999999" MinimumValue="1" SetFocusOnError="True" 
                        Type="Integer">Grupo sin vacantes o Cerrado</asp:RangeValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ValidationGroup="guardar" ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlCambioGrupo" runat="server" BorderColor="Silver" 
                        BorderStyle="Solid" BorderWidth="1px" Height="344px" ScrollBars="Vertical">
                        <table cellpadding="0" cellspacing="0" style="width:100%;">
                            <tr bgcolor="#E1E1FF" style="height: 25px">
                                <td>
                                    &nbsp; Seleccione el grupo horario para realizar el cambio<asp:RequiredFieldValidator 
                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="rblHorarios" 
                                        ErrorMessage="Debe seleccionar el grupo donde se realizará el cambio" 
                                        ValidationGroup="guardar">*</asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                                        ControlToValidate="rblHorarios" ErrorMessage="Grupo sin vacantes o Cerrado" 
                                        MaximumValue="999999" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
                                        ValidationGroup="guardar">*</asp:RangeValidator>
                                </td>
                                <td align="right">
                                    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Height="22px" 
                                        Text="Guardar" ValidationGroup="guardar" Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RadioButtonList ID="rblHorarios" runat="server" CellPadding="1" 
                                        CellSpacing="1" RepeatColumns="3" ValidationGroup="guardar">
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblDatos" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvHorario" runat="server" AutoGenerateColumns="False" 
                            GridLines="Horizontal" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Horario" />
                                <asp:BoundField HeaderText="Docente / Fecha" />
                                <asp:BoundField HeaderText="Vacantes" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <!--<asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />-->
                                    </HeaderTemplate>
                             
                                   <ItemTemplate>
                                        <asp:RadioButton  ID="chkE" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="#CC0000" 
                                    Text="No hay mas grupos para realizar el cambio"></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#CCCCCC" ForeColor="Black" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:HiddenField ID="hddCodigo_pes" runat="server" />
    <asp:HiddenField ID="hddCodigo_alu" runat="server" />
    <asp:HiddenField ID="hdCodUniversitario" runat="server" />
    <asp:HiddenField ID="hddCac" runat="server" />
    <asp:HiddenField ID="hddDisponible" runat="server" />
    </form>
    <script type="text/javascript" language="JavaScript" src="../../../estudiante/private/analyticsEstudiante.js?x=1"></script>
</body>
</html>
