<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Separacion.aspx.vb" Inherits="academico_estudiante_separacion_Separacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Separación de estudiantes</title>
    <link href="../../../../private/estilo.css" rel="Stylesheet" type ="text/css" />
    <script type="text/javascript" src="../../../../private/funciones.js" language ="javascript"></script>
    <script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <%  response.write(clsfunciones.cargacalendario) %>
    <div>
			<p class="usatTitulo">Separación de Estudiantes</p>
                <table align="center" width="100%" height="100%" border="0" cellpadding="0" 
            cellspacing="0">
                    <tr align="center" >
                        <td align="justify" colspan="4"  >
                                    </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" >
                                                Buscar por
                                                <asp:DropDownList ID="CmbBuscarpor" runat="server" 
                                            AutoPostBack="True">
                                                    <asp:ListItem Value="1">Apellidos y Nombres</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="2">Codigo 
                                            Universitario</asp:ListItem>
                                                </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="TxtBuscar" runat="server" Width="50%"></asp:TextBox>
                                            &nbsp;<asp:Button ID="CmdBuscar" runat="server" Text="Buscar" CssClass="buscar" 
                                            Height="20px" Width="80px"  />
                        </td>
                        </tr>
                    <tr>
                        <td align="left" colspan="4" >
                            &nbsp;</td>
                        </tr>                   
                    <tr align="center">
                        <td align="justify" colspan="4" valign="top" style="width:100%; height:150px" 
                            class="cajas3" >
                         <div id="listadiv" style="height:100%;width:100%" align="left" >
                                <asp:GridView ID="GvAlumnos" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_alu,codigo_pes" 
                                    GridLines="Horizontal" 
                                    style="margin-right: 0px" BorderWidth="0px" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_Alu" HeaderText="Codigo_Alu" 
                                            InsertVisible="False" ReadOnly="True" SortExpression="Codigo_Alu" 
                                            Visible="False" />
                                        <asp:BoundField DataField="Codigouniver_alu" 
                                            SortExpression="Codigouniver_alu" HeaderText="COD. UNIV.">
                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombres" HeaderText="APELLIDOS Y NOMBRES" 
                                            SortExpression="nombres" ReadOnly="True" >
                                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_cpf" SortExpression="nombre_cpf" 
                                            HeaderText="CARRERA PROFESIONAL">
                                            <ItemStyle Width="220px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado_alu" 
                                            SortExpression="estado_alu" HeaderText="ESTADO">
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_pes" 
                                            SortExpression="codigo_pes" HeaderText="codigo_pes" Visible="False">
                                            <ItemStyle Width="80px" />
                                        </asp:BoundField>                                        
                                        <asp:CommandField SelectText=" " ShowSelectButton="True">
                                            <ItemStyle Width="1px" />
                                        </asp:CommandField>                                        
                                    </Columns>                                    
                                    <SelectedRowStyle BackColor="#FFFFD2" />
                                    <HeaderStyle BackColor="#e33439" ForeColor="White" Height="22px" />
                                </asp:GridView>
                                 </div>
                                </td>
                    </tr>
                    <tr align="center" >
                        <td align="left" colspan="4" valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr align="center" >
                        <td align="justify" colspan="4" valign="top">
                            <div>
                                <asp:Panel ID="Panel1" runat="server" Visible="False">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" rowspan="6" valign="top" width="150">
                                                <asp:Image ID="ImgFoto" runat="server" Height="135px" Width="100px" 
                                                    BorderWidth="1px" />
                                            </td>
                                            <td align="left" valign="top">
                                                Código universitario:&nbsp;<asp:Label ID="LblCodigoUniv" runat="server"></asp:Label>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Apellidos y nombres:&nbsp;<asp:Label ID="LblNombres" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Plan de estudios:&nbsp;<asp:Label ID="LblPlanEstudio" runat="server"></asp:Label>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                Estado:
                                                <asp:Label ID="LblEstado" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </div>
                        </td>
                    </tr>
                    <tr align="center" >
                        <td align="justify" colspan="4" valign="top" style="text-align: left">
                            &nbsp; <asp:LinkButton ID="lnkHistorico" runat="server">Histórico</asp:LinkButton>
                            <b>| </b>
                            <asp:LinkButton ID="lnkDatos" runat="server">Registrar </asp:LinkButton>
                        &nbsp;|
                            <asp:LinkButton ID="lnkAdministrar" runat="server">Administrar</asp:LinkButton>
                        </td>
                    </tr>
                    <tr align="center" >
                        <td align="justify" colspan="4" valign="top" style="border: 1px solid #003399;">
                            <asp:Panel ID="pnlHistorico" runat="server" Height="150px" Width="100%" 
                                HorizontalAlign="Justify">
                                <asp:GridView ID="gvHistorico" runat="server" Width="100%" 
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="Ciclo Acad." HeaderText="SEMESTRE" />
                                        <asp:BoundField DataField="Tipo Separación" HeaderText="TIPO SEPARACION" />
                                        <asp:BoundField DataField="Motivo Separación" HeaderText="MOTIVO" />
                                        <asp:BoundField DataField="Fecha Ini" HeaderText="INICIA" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha Fin" HeaderText="FINALIZA" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Vigente" HeaderText="VIGENTE" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="nroResRevocada" HeaderText="Nº REVOC." />
                                        <asp:BoundField DataField="Usuario Reg." HeaderText="USUARIO REG." />
                                        <asp:BoundField DataField="codigo_sep" HeaderText="VER" ItemStyle-HorizontalAlign="Center" />                                                                                
                                    </Columns>                                    
                                    <EmptyDataTemplate>
                                        El estudiante no tiene registro de ningún tipo de separación
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#e33439" ForeColor="White" Height="22px" />
                                    <RowStyle Height="20px" />
                                </asp:GridView>
                                &nbsp;&nbsp;&nbsp;
                            </asp:Panel>
                            <asp:Panel ID="pnlDatos" runat="server" Height="150px" Width="100%">
                                <table width="100%" style="width: 100%; text-align: justify; vertical-align: middle;"  >
                                    <tr>
                                        <td>
                                            <b>
                                            <asp:Label ID="lblNuevo" runat="server" Text="Nuevo: "></asp:Label>
                                            Datos de Separación</b></td>
                                    </tr>
                                    <tr>
                                        <td align="justify" valign="top">
                                            Tipo<asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                ControlToValidate="cboTipo" ErrorMessage="CompareValidator" 
                                                Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                            <asp:DropDownList ID="cboTipo" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>                                            
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nro. Resolución:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="txtNroResolucion" ValidationGroup="Guardar" ErrorMessage="Indique el Nro. de resolución">*</asp:RequiredFieldValidator>
                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNroResolucion" runat="server"></asp:TextBox>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="justify" valign="top">
                                            Fecha final<asp:RequiredFieldValidator ID="rfvFecha" 
                                                runat="server" ControlToValidate="txtFecha" 
                                                ErrorMessage="La fecha final es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            :
                                            <asp:TextBox ID="txtFecha" runat="server" Width="88px"></asp:TextBox>
                                            <input ID="Button1" class="cunia" 
                                                onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFecha,'dd/mm/yyyy')" 
                                                style="height: 22px" type="button" />&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" 
                                                Text="(* Considerando: La separación temporal es por un año)"></asp:Label>
                                            &nbsp; </td>
                                    </tr>
                                    <tr>
                                        <td align="justify" valign="top">
                                            Motivo
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txtMotivo" ValidationGroup="Guardar" ErrorMessage="Indique el motivo de la separación">*</asp:RequiredFieldValidator>
                                            &nbsp;&nbsp; &nbsp; :
                                            <asp:TextBox ID="txtMotivo" runat="server" Width="70%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="justify">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="justify">
                                            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                                ValidationGroup="Guardar" />
                                            </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAdministrar" runat="server">
                                <asp:GridView ID="gvAdministrar" runat="server" Width="100%" 
                                    AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_sep" HeaderText="ID" />
                                        <asp:BoundField DataField="Ciclo Acad." HeaderText="SEMESTRE" 
                                            ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipo Separación" HeaderText="SEPARACION" />
                                        <asp:BoundField DataField="Motivo Separación" HeaderText="MOTIVO" />
                                        <asp:BoundField DataField="Vigente" HeaderText="VIGENTE" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Fecha Ini" HeaderText="INICIA" 
                                            ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Usuario Reg." HeaderText="REGISTRADO POR" />                                        
                                        <asp:CommandField ShowSelectButton="True" />
                                    </Columns>
                                    <HeaderStyle BackColor="#e33439" ForeColor="White" Height="22px" />
                                    <RowStyle Height="20px" />
                                </asp:GridView>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td style="width:10%;" align="left">Acción:</td>
                                        <td align="left">                                            
                                            <asp:RadioButtonList ID="rbLista" runat="server" RepeatDirection="Horizontal" 
                                                AutoPostBack="True" >
                                                <asp:ListItem Value="E" Text="Eliminar separación"></asp:ListItem>
                                                <asp:ListItem Value="D" Text="Revocar"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRevocar" runat="server" Text="No. Resolución:"></asp:Label>
                                            <asp:TextBox ID="txtRevocatoria" runat="server"></asp:TextBox>
                                         </td>                                        
                                    </tr>
                                    <tr>
                                        <td align="left">Observación</td>
                                         <td align="left" colspan="2"><asp:TextBox ID="txtObservacion" runat="server" MaxLength="2000" Width="99%" TextMode="MultiLine"></asp:TextBox></td>                                       
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">     
                                            <br />        
                                            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>&nbsp;&nbsp;
                                            <asp:Button ID="btnActualiza" runat="server" Text="Guardar" />                                        
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr align="center" >
                        <td align="justify" colspan="4" valign="top">
                            <asp:HiddenField ID="hddCodigo_sep" runat="server" />                            
                        </td>
                    </tr>
                    </table>
                
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ValidationGroup="Guardar" />            
    </form>
</body>
</html>
