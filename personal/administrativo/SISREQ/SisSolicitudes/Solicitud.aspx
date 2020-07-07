<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Solicitud.aspx.vb" Inherits="SisSolicitudes_Solicitud" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript" src="../private/funciones.js"></script>
    <script language="javascript" type="text/javascript">


        function confirmarSolicitud(mensaje) {
            var answer = confirm(mensaje);
            return answer;            
        }
    
    function HabilitaAnulacion(control)
    {
        if (control.checked == true )
        {   
            //alert ('TRUE') 
            form1.TxtSemAnulacion.style.visibility = 'visible'; 
            LblSemAnulacion.style.visibility = 'visible';
        }
        else
        {    
             //alert ('FALSE')
             form1.TxtSemAnulacion.style.visibility  = 'hidden'; 
             LblSemAnulacion.style.visibility = 'hidden';
        }
    }
    
    function validaAnulacionDeuda(source, arguments)
    {   if (document.form1.RbSi.checked == true )
        {   
            if (document.form1.TxtSemAnulacion.value =="" )
                arguments.IsValid = false
            else 
                arguments.IsValid = true
        }
    }
    
    function validadatos(source, arguments)
    {   
        if (document.form1.CboBuscarPor.value == 1)
        {   if (form1.TxtBuscar.value.length == 10)
                arguments.IsValid=true
            else
                arguments.IsValid=false
        }
    }
    function validaNumSolicitud(source, arguments)
    {
        if (form1.TxtNumSolicitud.value.length == 6)
            if(/^[0-9]+$/.test(form1.TxtNumSolicitud.value))       
                arguments.IsValid=true
            else
                arguments.IsValid=false
        else
            arguments.IsValid=false
    }
    
    function validaMotivo(source, arguments)
    {  
      var i;
      var fin;
      var bandera;
      bandera=0;
      fin = parseInt(document.form1.HddTotalChk.value)- 1;
       for (i=0;i<=fin;i++) {
         if (eval("document.form1.CblMotivo_" + i + ".checked")== true)
               bandera=1;   }
            if (bandera==0)
                arguments.IsValid = false;
            else
                arguments.IsValid = true; 
    }
    function validaSeleccionados(source, arguments)
    {  
      var i;
      var fin;
      var bandera;
      bandera=0;
      fin = parseInt(document.form1.HddTotalSel.value)- 1;

      if (fin==-1)
      { arguments.IsValid = true;
      }
      else
      { 
            // alert(validaAsuntos);
            if (validaAsuntos() == true)
            {   
                for (i=0;i<=fin;i++) 
                {   if (eval("document.form1.CblSeleccionar_" + i + ".checked")== true)
                        bandera=1;   
                }
                if (bandera==0)
                {   arguments.IsValid = false;  }
                else
                {   arguments.IsValid = true;   }
            }
       
      }
    }
    
    function validaSeleccionadosRet(source, arguments)
    {  
      var i;
      var fin;
      var bandera;
      bandera=0;
      fin = parseInt(document.form1.HddTotalSelRet.value)- 1;

      if (fin==-1)
      { arguments.IsValid = true;
      }
      else
      { 
            //alert(validaAsuntos());
            if (validaAsuntos() == true)
            { for (i=0;i<=fin;i++) 
              {     
                if (eval("document.form1.CblSeleccionarRet_" + i + ".checked")== true)
                   bandera=1;   
              }
              if (bandera==0)
              {  arguments.IsValid = false; }
              else
              {  arguments.IsValid = true;  }
            }
     
      }
    }
    
    function validaAsuntos()
    {
      var i;
      var fin;
      var bandera;
      bandera=0;
      fin = parseInt(document.form1.HddTotalRbl.value)- 1;
      for (i=0;i<=fin;i++) 
      {  if (eval("document.form1.RblAsunto_" + i + ".checked")== true)
            bandera=1;   
      }
      if (bandera==0)
         return false;
      else
         return true; 
    }
    </script>
    </head>
    
<body class="FondoPagina">
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
    <table style="width:95%;" align="center">
        <tr>
           
            <td align="center">
                        <table style="width: 750px">
                            <tr>
                                <td align="center" colspan="2">
                                    Buscar Por:
                                    <asp:DropDownList ID="CboBuscarPor" runat="server">
                                        <asp:ListItem Value="1">Código Universitario</asp:ListItem>
                                        <asp:ListItem Value="2">Apellidos y Nombres</asp:ListItem>
                                    </asp:DropDownList>
&nbsp;<asp:TextBox ID="TxtBuscar" runat="server" 
                                        MaxLength="10" Width="250px"></asp:TextBox>
                                    &nbsp;<asp:CustomValidator ID="CustomValidator1" runat="server" 
                                        ClientValidationFunction="validadatos" 
                                        ErrorMessage="El código universitario debe contener 10 dígitos" 
                                        ValidationGroup="Buscar">*</asp:CustomValidator>
                                    <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" 
                                CssClass="boton" ValidationGroup="Buscar"  />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    &nbsp;</td>
                                    
                            </tr>
                            
                            <tr >
                                <td align="center" width="130px" >
                                    <asp:Image ID="ImgFoto" runat="server" Height="100px" Width="80px" 
                                        Visible="False" />
                                </td>
                                <td class="ContornoTabla" align="center">
                                    <asp:FormView ID="FvDatos" runat="server" 
                                        Width="600px" Visible="False" CssClass="FondoBlanco">
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
                                                    <td width="130">
                                                        Codigo Universitario</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="codigouniver_aluLabel" runat="server" Text='<%# Bind("codigouniver_alu") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="130">
                                                        Apellidos y nombres</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="alumnoLabel" runat="server" Text='<%# Bind("alumno") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ciclo de ingreso</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="cicloing_aluLabel" runat="server" 
                                                            Text='<%# Bind("cicloing_alu") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Modalidad de ingreso</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="nombre_minLabel" runat="server" 
                                                            Text='<%# Bind("nombre_min") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Carrera profesional</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="nombre_cpfLabel" runat="server" 
                                                            Text='<%# Bind("nombre_cpf") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Plan de estudios</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="descripcion_pesLabel" runat="server" 
                                                            Text='<%# Bind("descripcion_pes") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Benefico de Beca</td>
                                                    <td>
                                                        :</td>
                                                    <td>
                                                        <asp:Label ID="beneficio_AluLabel" runat="server" 
                                                            Text='<%# Bind("beneficio_Alu") %>' ForeColor="Blue" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
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
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:Panel ID="Panel1" runat="server" BorderStyle="Solid" BorderWidth="1px"  style="visibility:hidden"
                    Width="750px" CssClass="FondoBlanco">
                    <table width="700px" >
                        <tr>
                            <td>
                                <table align="center" style="width:100%;">
                                    <tr>
                                        <td>
                                            <b>Estado de Cuenta:</b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width:100%;">
                                                <tr>
                                                    <th>
                                                        &nbsp;</th>
                                                </tr>
                                                <tr>
                                                    <th align="right">
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="LblTotal" runat="server" style="text-align:center" Width="80px" 
                                                            Visible="False"></asp:Label>
                                                    </th>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Número de Solicitud: </b>
                                <asp:TextBox ID="TxtNumSolicitud" runat="server" MaxLength="6"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TxtNumSolicitud" 
                                    ErrorMessage="Debe ingresar el número de la solicitud" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CustomValidator ID="CustomValidator4" runat="server" 
                                    ClientValidationFunction="validaNumSolicitud" 
                                    ErrorMessage="El número de la solicitud debe tener 6 dígitos" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <b>Seleccione asunto de la solicitud:</b></td>
                        </tr>
                        <tr>
                            <td >
                                <table width="100%">
                                    <tr>
                                        <td   colspan="2" style="border: 1px solid #C0C0C0">
                                            <asp:RadioButtonList ID="RblAsunto" runat="server" AutoPostBack="True" 
                                                RepeatColumns="2" Width="100%">
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="RblAsunto" ErrorMessage="Seleccione un asunto">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="Width:50%" valign="top">
                                            <asp:Label ID="LblIndicar" runat="server" Text="Indicar"></asp:Label>
                                            &nbsp;<asp:DropDownList ID="cboEscuela" runat="server">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="cvEscuela" runat="server" 
                                                ControlToValidate="cboEscuela" 
                                                ErrorMessage="Seleccione la Escuela Profesional a donde se trasladará" 
                                                Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                            <asp:CustomValidator ID="CustomValidator3" runat="server" 
                                                ClientValidationFunction="validaSeleccionados" 
                                                ErrorMessage="Seleccione una opcion relacionada al asunto" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                        </td>
                                        <td style="Width:50%" valign="top">
                                            <asp:Label ID="LblIndicarRet" runat="server" Text="Indicar"></asp:Label>
                                            <asp:CustomValidator ID="CustomValidator5" runat="server" 
                                                ClientValidationFunction="validaSeleccionadosRet" 
                                                ErrorMessage="Seleccione una opcion para retiro cursos" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="Width:50%" valign="top">
                                            <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Height="100px" 
                                                ScrollBars="Vertical" Width="100%" BorderColor="#999999" 
                                                BorderStyle="Solid">                                                
                                                <asp:CheckBoxList ID="CblSeleccionar" runat="server" AutoPostBack="True">
                                                </asp:CheckBoxList>                                                  
                                                
                                            </asp:Panel>
                                        </td>
                                        <td style="Width:50%" valign="top">
                                            <asp:Panel ID="PanelRet" runat="server" BorderWidth="1px" Height="100px" 
                                                ScrollBars="Vertical" Width="100%" BorderColor="#999999" 
                                                BorderStyle="Solid">
                                                <asp:CheckBoxList ID="CblSeleccionarRet" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                        
                            <td>
                            <div style="DISPLAY:none">
                                &nbsp;Anulación de Deuda:
                                <asp:RadioButton ID="RbSi" runat="server" GroupName="Anulacion" 
                                    onClick="HabilitaAnulacion(this)" Text="Si" />
                                &nbsp;<asp:RadioButton ID="RbNo" runat="server" Checked="True" GroupName="Anulacion" 
                                    onClick="HabilitaAnulacion(form1.RbSi)" Text="No" />
                                &nbsp;
                                <asp:Label ID="LblSemAnulacion" runat="server" style="visibility:hidden" 
                                    Text="Indique semestre(s) académico(s):"></asp:Label>
                                &nbsp;<asp:TextBox ID="TxtSemAnulacion" runat="server" style="visibility:hidden" 
                                    width="212px"></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator6" runat="server" 
                                    ClientValidationFunction="validaAnulacionDeuda" 
                                    ErrorMessage="Indique la deuda a anular" ValidationGroup="Guardar">*</asp:CustomValidator>
                            </div>
                            </td>
                           
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <b>Señores:<br />
                                </b>
                                <br />
                                Universidad Católica Santo Toribio de Mogrovejo</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify">
                                Yo
                                <asp:TextBox ID="TxtResponsable" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;" Width="480px"></asp:TextBox>
                                , identificado(a) con Código de Matrícula N°
                                <asp:TextBox ID="TxtCodMatricula" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;"></asp:TextBox>
                                , del semestre académico:
                                <asp:TextBox ID="TxtSemestre" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;" Width="70px"></asp:TextBox>
                                , estudiante de la Escuela Profesional de:
                                <asp:TextBox ID="TxtEscuela" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;" Width="350px"></asp:TextBox>
                                , con domicilo en:
                                <asp:TextBox ID="TxtDireccion" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;" Width="230px"></asp:TextBox>
                                <asp:TextBox ID="TxtUrbDis" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;" Width="290px"></asp:TextBox>
                                , teléfonos:&nbsp;
                                <asp:TextBox ID="TxtTelefonos" runat="server" CssClass="EscribirTexto" 
                                    style="text-align: center;" Width="210px"></asp:TextBox>
                                , me presento ante
                                <br />
                                <br />
                                ustedes para expresarle los motivos de mi solicitud:<br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CblMotivo" runat="server" CellPadding="3" CellSpacing="2" 
                                    RepeatColumns="2">
                                </asp:CheckBoxList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                    ClientValidationFunction="validaMotivo" 
                                    ErrorMessage="Debe Seleccionar por lo menos un motivo" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
                                <asp:TextBox ID="TxtOtros" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Observaciones:</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:TextBox ID="TxtObservaciones" runat="server" Height="56px" 
                                    TextMode="MultiLine" Width="690px"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <b>Fecha: </b>
                                <asp:TextBox ID="TxtFecha" runat="server" style="text-align: center;" 
                                    Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TxtFecha" 
                                    ErrorMessage="Debe ingresar la fecha de la solicitud">*</asp:RequiredFieldValidator>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="CmdGuardar" runat="server" CssClass="boton" Text="Guardar" 
                                    ValidationGroup="Guardar"  />
                                &nbsp; </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HiddenField ID="HddCodigocpf" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:GridView ID="gvEstadoCue" runat="server">
                </asp:GridView>
            </td>
          
        </tr>
    </table>

    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
    ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" />

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
    ValidationGroup="Buscar" ShowMessageBox="True" ShowSummary="False" />

    <asp:HiddenField ID="HddTotalChk" runat="server" />

    <asp:HiddenField ID="HddTotalSel" runat="server" />

    <asp:HiddenField ID="HddTotalRbl" runat="server" />

    <asp:HiddenField ID="HddSelecionado" runat="server" />

    <asp:HiddenField ID="HddTotalSelRet" runat="server" />

    <asp:HiddenField ID="hfMensaje" runat="server" />

    </form>


</body>
</html>