<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Evaluacion.aspx.vb" Inherits="SisSolicitudes_Evaluacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
        <script src="../../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript">
        function validaSeleccionados(source, arguments) {
            var i;
            var fin;
            var bandera;
            bandera = 0;
            fin = parseInt(document.form1.HddTotalSel.value) - 1;

            if (fin == -1) {
                arguments.IsValid = true;
            }
            else {
                //alert(validaAsuntos());
                for (i = 0; i <= fin; i++) {
                    if (eval("document.form1.CblSeleccionar_" + i + ".checked") == true)
                        bandera = 1;
                }
                if (bandera == 0)
                    { arguments.IsValid = false; }
                else
                    { arguments.IsValid = true; }
                }           
        }
    
        $(document).ready(function() {
            jQuery(function($) {
            $("#txtFechaUltimaAsistencia").mask("99/99/9999");
            });

        })
    
    </script>
    <style type="text/css">
        .style1
        {
            font-weight: normal;
        }
        .style3
        {
            text-align: left;
            font-weight: normal;
        }
        .style4
        {
            height: 127px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: small; color: #000080; font-weight: bold;">
    
        <table style="width:100%;">
            <tr>
                                <td align="left">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <b>Area:
                    </b>
                    <asp:Label ID="LblArea" runat="server" Font-Bold="False">Director de Escuela</asp:Label>
                                &nbsp;</td>
                                <td align="right">
                                    Número de Solicitud: <asp:TextBox ID="TxtCodSol" runat="server"></asp:TextBox>
                                    <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" 
                                CssClass="boton"  />
                                </td>
                            </tr>
            <tr>
                                <td align="left" colspan="2">
                                    <b>Usuario:</b>
                    <asp:Label ID="LblUsuario" runat="server" Font-Bold="False">Alonso Pérez, Eduardo </asp:Label>
                                </td>
                            </tr>
            <tr>
                                <td align="left" colspan="2">
                        <table align="center" width="98%" id="TablaContenido" style="visibility:hidden">
                            <tr>
                                <td align="center" class="ContornoTabla" >
                                    <table style="width:100%;" border="0">
                                        <tr>
                                            <td align="center">
                                    <asp:Image ID="ImgFoto" runat="server" Height="100px" Width="80px" 
                                        Visible="False" />
                                            </td>
                                            <td align="left">
                                    <asp:FormView ID="FvDatos" runat="server" 
                                        Width="450px" Visible="False">
                                        <EditItemTemplate>
                                            alumno:
                                            <asp:TextBox ID="alumnoTextBox" runat="server" Text='<%# Bind("alumno") %>' />
                                            <br />
                                            cicloing_alu:
                                            <asp:TextBox ID="cicloing_aluTextBox" runat="server" 
                                                Text='<%# Bind("cicloing_alu") %>' />
                                            <br />
                                            nombre_min:
                                            <asp:TextBox ID="nombre_minTextBox" runat="server" 
                                                Text='<%# Bind("nombre_min") %>' />
                                            <br />
                                            nombre_cpf:
                                            <asp:TextBox ID="nombre_cpfTextBox" runat="server" 
                                                Text='<%# Bind("nombre_cpf") %>' />
                                            <br />
                                            descripcion_pes:
                                            <asp:TextBox ID="descripcion_pesTextBox" runat="server" 
                                                Text='<%# Bind("descripcion_pes") %>' />
                                            <br />
                                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                                CommandName="Update" Text="Actualizar" />
                                            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                            alumno:
                                            <asp:TextBox ID="alumnoTextBox0" runat="server" Text='<%# Bind("alumno") %>' />
                                            <br />
                                            cicloing_alu:
                                            <asp:TextBox ID="cicloing_aluTextBox0" runat="server" 
                                                Text='<%# Bind("cicloing_alu") %>' />
                                            <br />
                                            nombre_min:
                                            <asp:TextBox ID="nombre_minTextBox0" runat="server" 
                                                Text='<%# Bind("nombre_min") %>' />
                                            <br />
                                            nombre_cpf:
                                            <asp:TextBox ID="nombre_cpfTextBox0" runat="server" 
                                                Text='<%# Bind("nombre_cpf") %>' />
                                            <br />
                                            descripcion_pes:
                                            <asp:TextBox ID="descripcion_pesTextBox0" runat="server" 
                                                Text='<%# Bind("descripcion_pes") %>' />
                                            <br />
                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                                CommandName="Insert" Text="Insertar" />
                                            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                                CausesValidation="False" CommandName="Cancel" Text="Cancelar" />
                                        </InsertItemTemplate>
                                        <ItemTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td width="130" class="style1">
                                                        Cod. Universitario</td>
                                                    <td class="style1">
                                                        :</td>
                                                    <td class="style3" >
                                                        <asp:Label ID="alumnoLabel0" runat="server" 
                                                            Text='<%# Bind("codigouniver_alu") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style1" width="130">
                                                        Apellidos y nombres</td>
                                                    <td class="style1">
                                                        :</td>
                                                    <td class="style3">
                                                        <asp:Label ID="alumnoLabel" runat="server" Text='<%# Bind("alumno") %>' />
                                                    </td>
                                                    <span class="style1"></span>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        Ciclo de ingreso</td>
                                                    <td>
                                                        <span class="style1">:</span></td>
                                                    <td class="style3">
                                                        <asp:Label ID="cicloing_aluLabel" runat="server" 
                                                            Text='<%# Bind("cicloing_alu") %>' />
                                                    </td>
                                                    <span class="style1"></span>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        Modalidad de ingreso</td>
                                                    <td>
                                                        <span class="style1">:</span></td>
                                                    <td class="style3">
                                                        <asp:Label ID="nombre_minLabel" runat="server" 
                                                            Text='<%# Bind("nombre_min") %>' />
                                                    </td>
                                                    <span class="style1"></span>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        Carrera profesional</td>
                                                    <td>
                                                        <span class="style1">:</span></td>
                                                    <td class="style3">
                                                        <asp:Label ID="nombre_cpfLabel" runat="server" 
                                                            Text='<%# Bind("nombre_cpf") %>' />
                                                    </td>
                                                    <span class="style1"></span>
                                                </tr>
                                                <tr>
                                                    <td class="style1">
                                                        Plan de estudios</td>
                                                    <td>
                                                        <span class="style1">:</span></td>
                                                    <td class="style3">
                                                        <asp:Label ID="descripcion_pesLabel" runat="server" 
                                                            Text='<%# Bind("descripcion_pes") %>' />
                                                       
                                                    </td>
                                                    <span class="style1"></span>
                                                <tr>
                                                    <td class="style1">
                                                        estado Actual</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="estadoActual_AluLabel" runat="server" 
                                                            Text='<%# Bind("estadoActual_Alu") %>' ForeColor="Blue" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                        </ItemTemplate>
                                    </asp:FormView>
                                            </td>
                                        </tr>
                                    </table>
                                    </td>
                            </tr>
                            <tr >
                                <td align="right" >
                                    <table style="width:100%;">
                                        <tr>
                                <td align="left" valign="top" width="60%" >
                                    <b>Responsable: </b>
                                    <asp:Label ID="LblResponsable" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                <td align="left" valign="top">
                                    &nbsp;</td>
                                <td align="left" valign="top">
                                    <b>Estado:</b>
                                    <asp:Label ID="LblEstado" runat="server" Font-Bold="False"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                <td align="left" valign="top" >
                                    <b>Asunto: </b></br>
                                    <asp:Label ID="LblAsunto" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                <td align="left" valign="top">
                                    &nbsp;</td>
                                <td align="left" valign="top">
                                    <b>Motivo: </b></br>
                                    <asp:Label ID="LblMotivo" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                <td align="left" valign="top" >
                                    <b>Fecha de solicitud: </b>
                                    <asp:Label ID="LblFechaSol" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                <td align="left" valign="top" style="font-weight: 700">
                                    &nbsp;</td>
                                <td align="left" valign="top" style="font-weight: 700">
                                    <b>Fecha de registro:</b>
                                    <asp:Label ID="LblFechaReg" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                <td align="left" valign="top" colspan="3" >
                                    <b>Observaciones de la solicitud:</b>
                                    <asp:Label ID="LblObservaciones" runat="server" Font-Bold="False"></asp:Label>
                                            </td>
                                        </tr>
                                        </table>
                                </td>
                            </tr>

                            <tr >
                                <td align="justify" >
                                    &nbsp;</td>
                            </tr>

                            <tr >
                                <td align="center" class="cajas3" >
                                    <iframe id="frameHistorial" frameborder="0" height="220" name="frameHistorial" src="" 
                                        width="100%"></iframe>

                                    <br />

                                </td>
                            </tr>
                            <tr >
                                <td align="center" >
                                    &nbsp;</td>
                            </tr>
                            <tr >
                                <td align="center" class="cajas3" >
                        <iframe src="Informes.aspx" id="frameInforme" width="100%" frameborder="0" height="600" 
                            name="frameInforme" scrolling="auto"></iframe>

                                </td>
                            </tr>
                            <tr >
                                <td align="center" class="style4" >
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table align="center" width="100%" class="ContornoTabla">
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        :::::::::: EVALUAR LA SOLICITUD ::::::::::</td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        Fecha:
                                                        <asp:Label ID="LblFecha" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        Ha resuelto:
                                                        <asp:DropDownList ID="DdlHaResuelto" runat="server">
                                                        </asp:DropDownList>
                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                                            ControlToValidate="DdlHaResuelto" ErrorMessage="Seleccione un resultado" 
                                                            MaximumValue="2" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
                                                            ValidationGroup="Guardar">*</asp:RangeValidator>
                                                        <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="20" align="left" width="100%" colspan="2">
                                                        Observaciones:<asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                                            runat="server" ControlToValidate="TxtObservaciones" 
                                                            ErrorMessage="Ingrese el motivo de su respuesta" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="color: #FF0000" colspan="2">
                                                        <asp:TextBox ID="TxtObservaciones" runat="server" Rows="4" TextMode="MultiLine" 
                                                            Width="99%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2">
                                                        <asp:Label ID="lblEtiquetaFechaHasta" runat="server" 
                                                            Text="* Ingrese última fecha en la que el alumno asistió a clases:" ForeColor="#FF3300"></asp:Label>
                                                        <asp:TextBox ID="txtFechaUltimaAsistencia" runat="server" Width="93px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtFechaUltimaAsistencia" 
                                                            ErrorMessage="La fecha es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                        <asp:Label ID="lblEtiquetaFormatFecha" runat="server" 
                                                            Text="(Formato: dd/mm/YYYY, Ejemplo: 06/11/2010)"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="justify" colspan="2">
                                                        <asp:Panel ID="pnlCursos" runat="server" BorderColor="#CCCCCC" 
                                                            BorderStyle="Solid" BorderWidth="1px" Height="100px" ScrollBars="Vertical" 
                                                            style="text-align: justify">
                                                            Seleccione los cursos:<asp:CustomValidator ID="CustomValidator6" runat="server" 
                                                                ClientValidationFunction="validaSeleccionados" 
                                                                ErrorMessage="Seleccione los cursos que debe matricularse" 
                                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                                            <asp:CheckBoxList ID="CblSeleccionar" runat="server" RepeatColumns="1" 
                                                                style="font-weight: 400">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                            ValidationGroup="Guardar" />
                                                        <%--<cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                                            ConfirmText="¿Está seguro que desea guardar?" TargetControlID="CmdGuardar">
                                                        </cc1:ConfirmButtonExtender>--%>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Button ID="CmdGuardar" runat="server" CssClass="boton" Text="Guardar" 
                                                            ValidationGroup="Guardar" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            </table>
                                        
                                </td>
                            </tr>
            </table>
    
        <table align="center">
            <tr>
                <td valign="top">
                        &nbsp;</td>
            </tr>
            </table>
    
    </div>
        <br />
        <asp:HiddenField ID="HddCodigoSol" runat="server" />
        <asp:HiddenField ID="HddCodigoCco" runat="server" />
        <asp:HiddenField ID="HddEsDirector" runat="server" Value="0" />
    <asp:HiddenField ID="hddCodigo_tas" runat="server" />

    <asp:HiddenField ID="HddTotalSel" runat="server" />

    </form>
</body>
</html>
