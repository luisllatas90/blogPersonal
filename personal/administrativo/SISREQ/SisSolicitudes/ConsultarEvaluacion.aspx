<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultarEvaluacion.aspx.vb" Inherits="SisSolicitudes_ConsultarEvaluacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
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
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        <table style="width:100%;">
            <tr>
                                <td align="left">
                                    <b>Area:
                    </b>
                    <asp:Label ID="LblArea" runat="server">Director de Escuela</asp:Label>
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
                    <asp:Label ID="LblUsuario" runat="server">Alonso Pérez, Eduardo </asp:Label>
                                </td>
                            </tr>
            <tr>
                                <td align="left" colspan="2">
                        <table align="center" width="95%" id="TablaContenido" style="visibility:hidden">
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
                                                </tr>
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
                                    <asp:Label ID="LblResponsable" runat="server"></asp:Label>
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
                                    <asp:Label ID="LblAsunto" runat="server"></asp:Label>
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
                                    <asp:Label ID="LblObservaciones" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        </table>
                                </td>
                            </tr>

                            <tr >
                                <td align="center" >
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
                        <iframe src="ConsultarInformes.aspx" id="frameInforme" width="100%" frameborder="0" height="600" 
                            name="frameInforme"></iframe>

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
    </form>
</body>
</html>
