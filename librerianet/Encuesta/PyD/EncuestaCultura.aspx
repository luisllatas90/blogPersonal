<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaCultura.aspx.vb" Inherits="librerianet_Encuesta_PyD_EncuestaCultura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Encuesta Cultura</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" type="text/javascript" src="../../../private/funciones.js"></script>
     <script type="text/javascript" language="javascript">
       
    function ValidaActividadesRecreacion(source, arguments)
    { var sw;
      var i;
      sw = 0;
      for (i=0; i<=7; i++)
      {
        if (eval("document.form1.cblTiemposLibres_" + i + ".checked")== true)
            sw=sw+1;   
      }
      if (sw==0)
        arguments.IsValid = false;
      else
        arguments.IsValid = true; 
    }
    
    function ValidaTipoMusica(source, arguments)
    { var sw;
      var i;
      sw = 0;
      for (i=0; i<=8; i++)
      {
        if (eval("document.form1.cblTipoMusica_" + i + ".checked")== true)
            sw=sw+1;   
      }
      if (sw==0)
        arguments.IsValid = false;
      else
        arguments.IsValid = true; 
    }
    
    function ValidaTipoTeatro(source, arguments)
    { var sw;
      var i;
      sw = 0;
      for (i=0; i<=7; i++)
      {
        if (eval("document.form1.cblTipoTeatro_" + i + ".checked")== true)
            sw=sw+1;   
      }
      if (sw==0)
        arguments.IsValid = false;
      else
        arguments.IsValid = true; 
    }
    
    function ValidaExpresionesCulturales(source, arguments)
    { var sw;
      var i;
      sw = 0;
      for (i=0; i<7; i++)
      {
        if (eval("document.form1.cblExpCulturales_" + i + ".checked")== true)
            sw=sw+1;   
      }
      if (sw==0)
        arguments.IsValid = false;
      else
        arguments.IsValid = true; 
    }
    
    function HabilitaEspecificar(combo, cajaTexto, indice)
    { 
      if (combo.options[indice].selected == true )
      { //alert(combo.options[6].value);
        cajaTexto.disabled = false;
        cajaTexto.style.backgroundColor = '#FFFFFF'
      }
      else
      { cajaTexto.value = "";
        cajaTexto.disabled = true;
        cajaTexto.style.backgroundColor = '#E4E4E4'
      }
    }
    
    function HabilitaEspecificaTexto(cajaTexto, control)
    { 
      if (eval("document.form1." + control + ".checked")== true)
      { //alert(combo.options[6].value);
        cajaTexto.disabled = false;
        cajaTexto.style.backgroundColor = '#FFFFFF'
      }
      else
      { cajaTexto.value = "";
        cajaTexto.disabled = true;
        cajaTexto.style.backgroundColor = '#E4E4E4'
      }
    }
    
    function HabilitaEspecificaMultiple(control)
    { for(i=0; i<7; i++)
      {
          if (eval("document.form1." + control + i + ".checked")== true)
          { cajaTexto = DevolverNombreCajaTexto(i)
            eval("document.form1." + cajaTexto + ".disabled = false");
            eval("document.form1." + cajaTexto + ".style.backgroundColor = '#FFFFFF'");
          }
          else
          { cajaTexto = DevolverNombreCajaTexto(i)
            eval("document.form1." + cajaTexto + ".value=''");
            eval("document.form1." + cajaTexto + ".disabled = true");
            eval("document.form1." + cajaTexto + ".style.backgroundColor = '#E4E4E4'");
          }
      }
    }
    
    function DevolverNombreCajaTexto(valor)
    {
        switch (valor)
            {   case 0 : cajaTexto = "txtExpMusica";
                         break;
                case 1 : cajaTexto = "txtExpDanza";
                         break;                    
                case 2 : cajaTexto = "txtExpLiteratura";
                         break;
                case 3 : cajaTexto = "txtExpArtesania";
                         break;
                case 4 : cajaTexto = "txtExpGastronomia";
                         break;
                case 5 : cajaTexto = "txtExpArqueologia";
                         break;
                case 6 : cajaTexto = "txtExpOtros";
                         break;
            }
            return cajaTexto;
    }
    
    function ValidaEspecifiqueMusica(source, arguments)
    {
      if (document.form1.cblTipoMusica_8.checked==true)
      { if (document.form1.txtEspecifiqueMusica.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }  
    }
        
    function ValidaP7(source, arguments)
    { 
      if (document.form1.cboIdentificacion.value == 6)
      { if (document.form1.txtIdentificacion.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }
    }
    
    function ValidaP8(source, arguments)
    { 
      if (document.form1.cboGrupoCultural.value == 1)
      { if (document.form1.txtGrupoCultural.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }
    }

    function ValidaP10(source, arguments)
    { 
      if (document.form1.cboArtesPlasticas.value == 1)
      { if (document.form1.txtArtesPlasticas.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }
    }
    function ValidaP11(source, arguments)
    { 
      if (document.form1.cboIncursionarArte.value == 2)
      { if (document.form1.txtIncursionarArte.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }
    }
    function ValidaP18(source, arguments)
    {
      if (document.form1.cboPreferenciaDanzas.value == 1)
      { if (document.form1.txtPreferenciaDanzas.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }    
    }    
    function ValidaP19(source, arguments)
    { 
      if (document.form1.cboConocesDanzas.value == 1)
      { if (document.form1.txtConocesDanzas.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }    
    } 
    function ValidaExpMusica(source, arguments)
    {
      if (document.form1.cblExpCulturales_0.checked==true)
      { if (document.form1.txtExpMusica.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      } 
    }    
    function ValidaExpDanza(source, arguments)
    {
      if (document.form1.cblExpCulturales_1.checked==true)
      { if (document.form1.txtExpDanza.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }     
    }
    function ValidaExpLiteratura(source, arguments)
    {
      if (document.form1.cblExpCulturales_2.checked==true)
      { if (document.form1.txtExpLiteratura.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }     
    }
    function ValidaExpArtesania(source, arguments)
    {
      if (document.form1.cblExpCulturales_3.checked==true)
      { if (document.form1.txtExpArtesania.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }      
    }
    function ValidaExpGastronomia(source, arguments)
    {
      if (document.form1.cblExpCulturales_4.checked==true)
      { if (document.form1.txtExpGastronomia.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }  
    }
    function ValidaExpArqueologia(source, arguments)
    {
      if (document.form1.cblExpCulturales_5.checked==true)
      { if (document.form1.txtExpArqueologia.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }  
    }
    function ValidaExpOtros(source, arguments)
    {
      if (document.form1.cblExpCulturales_6.checked==true)
      { if (document.form1.txtExpOtros.value != "")
            arguments.IsValid = true;
         else
            arguments.IsValid = false;
      }
    }
    </script>
</head>
<body style="margin-top:0px">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" cellspacing="0">
            <tr>
                <td align="center" style="font-weight: bold; color: #FFFFFF;" bgcolor="#0066CC">
                    ENCUESTA</td>
            </tr>
            <tr>
                <td bgcolor="#3366CC" style="color: #FFFFFF">
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3">
                    Carrera Profesional:<asp:RequiredFieldValidator ID="RequiredFieldValidator8" 
                        runat="server" ControlToValidate="txtEscuela" 
                        ErrorMessage="La carrera profesional es obligatoria" 
                        ValidationGroup="guardar">*</asp:RequiredFieldValidator>
&nbsp;<asp:TextBox ID="txtEscuela" runat="server" Width="50%"></asp:TextBox>
                    &nbsp;</td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3">
                    Ciclo:<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="cboCiclo" ErrorMessage="El ciclo es obligatorio" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;<asp:DropDownList ID="cboCiclo" runat="server" Width="56px">
                    </asp:DropDownList>
&nbsp;Edad:<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="txtEdad" 
                        ErrorMessage="La carrera profesional es obligatoria" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
&nbsp;<asp:TextBox ID="txtEdad" runat="server" Width="49px"></asp:TextBox>
                &nbsp;Sexo:<asp:CompareValidator ID="CompareValidator17" runat="server" 
                                    ControlToValidate="cboSexo" ErrorMessage="El campo sexo es obligatorio" 
                                    Operator="GreaterThan" ValueToCompare="0" 
                        ValidationGroup="Guardar">*</asp:CompareValidator>
                            &nbsp;<asp:DropDownList ID="cboSexo" runat="server">
                        <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                        <asp:ListItem Value="2">Mujer</asp:ListItem>
                        <asp:ListItem Value="1">Varón</asp:ListItem>
                    </asp:DropDownList>
&nbsp;Tipo de colegio de origen:<asp:CompareValidator ID="CompareValidator16" runat="server" 
                                    ControlToValidate="cboColegio" ErrorMessage="El tipo de colegio es obligatorio" 
                                    Operator="GreaterThan" ValueToCompare="0" 
                        ValidationGroup="Guardar">*</asp:CompareValidator>
                            &nbsp;<asp:DropDownList ID="cboColegio" runat="server">
                        <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                        <asp:ListItem Value="1">Público</asp:ListItem>
                        <asp:ListItem Value="2">Privado</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3">
                    Lugar de residencia:
                                <asp:CompareValidator ID="CompareValidator18" runat="server" 
                                    ControlToValidate="cboProvincia" ErrorMessage="La provincia de residencia es obligatoria" 
                                    Operator="GreaterThan" ValueToCompare="0" 
                        ValidationGroup="Guardar">*</asp:CompareValidator>
                    <asp:DropDownList ID="cboProvincia" runat="server" AutoPostBack="True" 
                        ToolTip="Provincia" >
                    </asp:DropDownList>
&nbsp;<asp:CompareValidator ID="CompareValidator19" runat="server" 
                                    ControlToValidate="cboDistrito" ErrorMessage="El distrito de residencia es obligatorio" 
                                    Operator="GreaterThan" ValueToCompare="0" 
                        ValidationGroup="Guardar">*</asp:CompareValidator>
                            <asp:DropDownList ID="cboDistrito" runat="server" ToolTip="Distrito" Width="210px">
                    </asp:DropDownList>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                        ControlToValidate="txtUrbanizacion" 
                        ErrorMessage="La urbanización de residencia es obligatoria" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtUrbanizacion" runat="server" ToolTip="Urbanización"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td bgcolor="#F3F3F3" style="font-weight: bold">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Provincia&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Distrito&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; Urbanización</td>
            </tr>
            <tr>
                <td bgcolor="#666666" height="1px">
                    </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td valign="top" width="40%" align="justify">
                                &nbsp;</td>
                            <td width="2%" valign="top">
                                &nbsp;</td>
                            <td valign="top" width="58%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" width="40%" align="justify">
                                1. ¿Que actividad o actividades de recreación y ocio realizas en tus tiempos 
                                libres?</td>
                            <td width="2%" valign="top">
                                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                    ClientValidationFunction="ValidaActividadesRecreacion" 
                                    ErrorMessage="La pregunta 1 es obligatoria" ValidationGroup="Guardar">*</asp:CustomValidator>
                            </td>
                            <td valign="top" width="58%">
                                <asp:CheckBoxList ID="cblTiemposLibres" runat="server" CellPadding="1" 
                                    CellSpacing="1" RepeatColumns="4" Width="409px">
                                    <asp:ListItem Value="1">Deporte</asp:ListItem>
                                    <asp:ListItem Value="2">Leer</asp:ListItem>
                                    <asp:ListItem Value="3">Ir al cine</asp:ListItem>
                                    <asp:ListItem Value="4">Fiestas</asp:ListItem>
                                    <asp:ListItem Value="5">Salir de compras</asp:ListItem>
                                    <asp:ListItem Value="6">Utilizar el chat</asp:ListItem>
                                    <asp:ListItem Value="7">Caminatas</asp:ListItem>
                                    <asp:ListItem Value="8">Ciclismo</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                2. ¿Qué valor tiene para ti la literatura? (Entiéndase: ensayo, narrativa, 
                                poesía, etc)</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="cboLiteratura" ErrorMessage="La pregunta 2 es obligatoria" 
                                    Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboLiteratura" runat="server">
                                    <asp:ListItem Value="0">-- seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1">Ninguno</asp:ListItem>
                                    <asp:ListItem Value="2">Muy bajo</asp:ListItem>
                                    <asp:ListItem Value="3">Bajo</asp:ListItem>
                                    <asp:ListItem Value="4">Medio</asp:ListItem>
                                    <asp:ListItem Value="5">Alto</asp:ListItem>
                                    <asp:ListItem Value="6">Muy Alto</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                3. ¿Qué tipo de lectura realizas con más frecuencia?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                    ControlToValidate="cboLectura" ErrorMessage="La pregunta 3 es obligatoria" 
                                    Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboLectura" runat="server" Font-Names="Arial" 
                                    Font-Size="Small">
                                    <asp:ListItem Value="0">-- seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1">Narrativo (Cuento, novela, epopeya, leyenda, balada, 
                                    romance, fábula, etc.)</asp:ListItem>
                                    <asp:ListItem Value="2">Dramático (Tragedia, comedia, farsa, contenido 
                                    religioso)</asp:ListItem>
                                    <asp:ListItem Value="3">Lírico (Oda, poesía, elegía, canción, copla, epístola)</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                4. ¿Cuántos libros has leído en el último año?
                            </td>
                            <td valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txtNroLibrosLeidos" 
                                    ErrorMessage="La pregunta 4 es obligatoria" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNroLibrosLeidos" runat="server" Width="82px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                5. ¿Cuáles fueron los 2 últimos libros que ha leído?</td>
                            <td valign="top">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtLibroLeido1" 
                                    ErrorMessage="La pregunta 5 es obligatoria, especifique dos libros" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                a).
                                <asp:TextBox ID="txtLibroLeido1" runat="server" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="txtLibroLeido2" 
                                    ErrorMessage="La pregunta 5 es obligatoria, especifique dos libros" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                b).
                                <asp:TextBox ID="txtLibroLeido2" runat="server" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                6. ¿Cuánto conoces de la cultura en nuestro departamento?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                    ControlToValidate="cboCulturaDep" ErrorMessage="La pregunta 6 es obligatoria" 
                                    Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboCulturaDep" runat="server">
                                    <asp:ListItem Value="0">-- seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1">Casi nada</asp:ListItem>
                                    <asp:ListItem Value="2">Nada</asp:ListItem>
                                    <asp:ListItem Value="3">Poco</asp:ListItem>
                                    <asp:ListItem Value="4">Muy Poco</asp:ListItem>
                                    <asp:ListItem Value="5">Mucho</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                7. ¿De qué manera te identificas con nuestra cultura?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator4" runat="server" 
                                    ControlToValidate="cboIdentificacion" 
                                    ErrorMessage="La pregunta 7 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboIdentificacion" runat="server">
                                    <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1">Respetando valores culturales</asp:ListItem>
                                    <asp:ListItem Value="2">Fomentando la cultura de nuestra región</asp:ListItem>
                                    <asp:ListItem Value="3">Elaboración de proyectos</asp:ListItem>
                                    <asp:ListItem Value="4">Asistiendo a eventos netamente culturales</asp:ListItem>
                                    <asp:ListItem Value="5">Ninguna</asp:ListItem>
                                    <asp:ListItem Value="6">Otra</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidaP7" 
                                    ErrorMessage="La pregunta 7 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
                                <asp:TextBox ID="txtIdentificacion" runat="server" Width="210px" 
                                    Enabled="False" BackColor="#E4E4E4"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                8. ¿Participas activamente en algún grupo cultural?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                    ControlToValidate="cboGrupoCultural" 
                                    ErrorMessage="La pregunta 8 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboGrupoCultural" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label2" runat="server" Text="Especifíque: "></asp:Label>
                                <asp:CustomValidator ID="CustomValidator3" runat="server" 
                                    ClientValidationFunction="ValidaP8" ControlToValidate="txtGrupoCultural" 
                                    ErrorMessage="La pregunta 8 es obligatoria, especifique">*</asp:CustomValidator>
                                <asp:TextBox ID="txtGrupoCultural" runat="server" BackColor="#E4E4E4" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="justify" valign="top">
                                9. ¿Crees que nuestra comunidad tiene conciencia del valor de la cultura? 
                                (Entiéndase esto como cultura de valores, cultura de arte, cultura de cultura).</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator6" runat="server" 
                                    ControlToValidate="cboConciencia" ErrorMessage="La pregunta 9 es obligatoria" 
                                    Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="cboConciencia" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                10. ¿Tienes alguna preferencia por las artes plásticas? (Entiéndase: pintura, 
                                escultura, grabado, graffiti)</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator7" runat="server" 
                                    ControlToValidate="cboArtesPlasticas" 
                                    ErrorMessage="La pregunta 10 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboArtesPlasticas" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label1" runat="server" Text="Especifíque: "></asp:Label>
                                <asp:CustomValidator ID="CustomValidator4" runat="server" 
                                    ClientValidationFunction="ValidaP10" 
                                    ErrorMessage="La pregunta 10 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
                                <asp:TextBox ID="txtArtesPlasticas" runat="server" BackColor="#E4E4E4" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                11. ¿Te gustaría incursionar en alguna manifestación de este arte?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                    ControlToValidate="cboIncursionarArte" 
                                    ErrorMessage="La pregunta 11 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboIncursionarArte" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label3" runat="server" Text="Porqué:"></asp:Label>
                                <asp:CustomValidator ID="CustomValidator5" runat="server" 
                                    ClientValidationFunction="ValidaP11" 
                                    ErrorMessage="La pregunta 11 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
&nbsp;<asp:TextBox ID="txtIncursionarArte" runat="server" BackColor="#E4E4E4" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                12. ¿Tienes alguna preferencia por la música? (Entiéndase: en la amplitud de los 
                                géneros musicales)</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                    ControlToValidate="cboPreferenciaMusica" 
                                    ErrorMessage="La pregunta 12 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboPreferenciaMusica" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" valign="top" align="justify">
                                13. ¿Qué tipo de música te gusta más?</td>
                            <td valign="top">
                                <asp:CustomValidator ID="CustomValidator6" runat="server" 
                                    ClientValidationFunction="ValidaTipoMusica" 
                                    ErrorMessage="La pregunta 13 es obligatoria" ValidationGroup="Guardar">*</asp:CustomValidator>
                            </td>
                            <td valign="top">
                                <asp:CheckBoxList ID="cblTipoMusica" runat="server" CellPadding="1" 
                                    CellSpacing="1" RepeatColumns="3" Width="306px">
                                    <asp:ListItem Value="1">Clásica</asp:ListItem>
                                    <asp:ListItem Value="2">Rock</asp:ListItem>
                                    <asp:ListItem Value="3">Baladas</asp:ListItem>
                                    <asp:ListItem Value="4">Salsa</asp:ListItem>
                                    <asp:ListItem Value="5">Rap</asp:ListItem>
                                    <asp:ListItem Value="6">Reggae</asp:ListItem>
                                    <asp:ListItem Value="7">Cumbia</asp:ListItem>
                                    <asp:ListItem Value="8">Folklórica</asp:ListItem>
                                    <asp:ListItem Value="9">Otra</asp:ListItem>
                                </asp:CheckBoxList>
                                <asp:CustomValidator ID="CustomValidator22" runat="server" 
                                    ClientValidationFunction="ValidaEspecifiqueMusica" 
                                    ErrorMessage="La pregunta 13 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
                                <asp:TextBox ID="txtEspecifiqueMusica" runat="server" BackColor="#E4E4E4" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                14. ¿Te gustaría incursionar en la música?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator10" runat="server" 
                                    ControlToValidate="cboIncursionarMusica" 
                                    ErrorMessage="La pregunta 14 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboIncursionarMusica" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label4" runat="server" Text="Porqué:"></asp:Label>
&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="txtIncursionarMusica" 
                                    ErrorMessage="La pregunta 14 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtIncursionarMusica" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                15. ¿Tienes alguna preferencia por el teatro?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator11" runat="server" 
                                    ControlToValidate="cboPreferenciaTeatro" 
                                    ErrorMessage="La pregunta 15 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboPreferenciaTeatro" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label5" runat="server" Text="Porqué:"></asp:Label>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                    ControlToValidate="txtPreferenciaTeatro" 
                                    ErrorMessage="La pregunta 15 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtPreferenciaTeatro" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                16. ¿Qué tipo de teatro te gustaría hacer?</td>
                            <td valign="top">
                                <asp:CustomValidator ID="CustomValidator9" runat="server" 
                                    ClientValidationFunction="ValidaTipoTeatro" 
                                    ErrorMessage="La pregunta 16 es obligatoria" ValidationGroup="Guardar">*</asp:CustomValidator>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="cblTipoTeatro" runat="server" CellPadding="1" 
                                    CellSpacing="1" RepeatColumns="4" Width="341px">
                                    <asp:ListItem Value="1">Comedia</asp:ListItem>
                                    <asp:ListItem Value="2">Drama</asp:ListItem>
                                    <asp:ListItem Value="3">Tragedia</asp:ListItem>
                                    <asp:ListItem Value="4">Claun</asp:ListItem>
                                    <asp:ListItem Value="5">Mimo</asp:ListItem>
                                    <asp:ListItem Value="6">Pantomima</asp:ListItem>
                                    <asp:ListItem Value="7">danza teatro</asp:ListItem>
                                    <asp:ListItem Value="8">Otra</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                17. ¿Te gustaría incursionar en el teatro?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator12" runat="server" 
                                    ControlToValidate="cboIncursionarTeatro" 
                                    ErrorMessage="La pregunta 17 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboIncursionarTeatro" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label6" runat="server" Text="Porqué:"></asp:Label>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="txtIncursionarTeatro" 
                                    ErrorMessage="La pregunta 17 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtIncursionarTeatro" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                18. ¿Tienes alguna preferencia por las danzas? (Entiéndase: danza moderna, danza 
                                contemporánea, danzas folkloricas, bailes modernos)</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator13" runat="server" 
                                    ControlToValidate="cboPreferenciaDanzas" 
                                    ErrorMessage="La pregunta 18 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboPreferenciaDanzas" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label7" runat="server" Text="Porqué:"></asp:Label>
                                &nbsp;<asp:CustomValidator ID="CustomValidator11" runat="server" 
                                    ClientValidationFunction="ValidaP18" 
                                    ErrorMessage="La pregunta 18 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
                                <asp:TextBox ID="txtPreferenciaDanzas" runat="server" BackColor="#E4E4E4" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                19. ¿Conoces las danzas de nuestro Departamento?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator14" runat="server" 
                                    ControlToValidate="cboConocesDanzas" 
                                    ErrorMessage="La pregunta 19 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboConocesDanzas" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label8" runat="server" Text="Porqué:"></asp:Label>
                                &nbsp;<asp:CustomValidator ID="CustomValidator12" runat="server" 
                                    ClientValidationFunction="ValidaP19" 
                                    ErrorMessage="La pregunta 19 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:CustomValidator>
                                <asp:TextBox ID="txtConocesDanzas" runat="server" BackColor="#E4E4E4" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                20. ¿Te gustaría incursionar en algún tipo de danza?</td>
                            <td valign="top">
                                <asp:CompareValidator ID="CompareValidator15" runat="server" 
                                    ControlToValidate="cboIncursionarDanza" 
                                    ErrorMessage="La pregunta 20 es obligatoria" Operator="GreaterThan" 
                                    ValueToCompare="0" ValidationGroup="Guardar">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboIncursionarDanza" runat="server">
                                    <asp:ListItem Value="0">---</asp:ListItem>
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="2">No</asp:ListItem>
                                </asp:DropDownList>
&nbsp;<asp:Label ID="Label9" runat="server" Text="Porqué:"></asp:Label>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                    ControlToValidate="txtIncursionarDanza" 
                                    ErrorMessage="La pregunta 20 es obligatoria, especifique" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtIncursionarDanza" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="margin-left: 120px" align="justify" valign="top">
                                21. ¿Qué expresiones culturales de nuestro departamento conoces?</td>
                            <td valign="top">
                                <asp:CustomValidator ID="CustomValidator14" runat="server" 
                                    ClientValidationFunction="ValidaExpresionesCulturales" 
                                    ErrorMessage="La pregunta 21 es obligatoria" ValidationGroup="Guardar">*</asp:CustomValidator>
                            </td>
                            <td>
                                <table style="width:100%;">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBoxList ID="cblExpCulturales" runat="server">
                                                <asp:ListItem Value="1">Música</asp:ListItem>
                                                <asp:ListItem Value="2">Danza</asp:ListItem>
                                                <asp:ListItem Value="3">Literatura</asp:ListItem>
                                                <asp:ListItem Value="4">Artesanía</asp:ListItem>
                                                <asp:ListItem Value="5">Gastronomía</asp:ListItem>
                                                <asp:ListItem Value="6">Arqueología</asp:ListItem>
                                                <asp:ListItem Value="7">Otros</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                        <td>
                                            <asp:CustomValidator ID="CustomValidator15" runat="server" 
                                                ClientValidationFunction="ValidaExpMusica" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique la expresion cultural de música" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpMusica" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustomValidator16" runat="server" 
                                                ClientValidationFunction="ValidaExpDanza" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique la expresion cultural de danza" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpDanza" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustomValidator17" runat="server" 
                                                ClientValidationFunction="ValidaExpLiteratura" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique la expresion cultural de Literatura" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpLiteratura" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustomValidator18" runat="server" 
                                                ClientValidationFunction="ValidaExpArtesania" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique la expresion cultural de artesanía" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpArtesania" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustomValidator19" runat="server" 
                                                ClientValidationFunction="ValidaExpGastronomia" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique la expresion cultural de gastronomía" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpGastronomia" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustomValidator20" runat="server" 
                                                ClientValidationFunction="ValidaExpArqueologia" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique la expresion cultural de arqueologia" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpArqueologia" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                            <br />
                                            <asp:CustomValidator ID="CustomValidator21" runat="server" 
                                                ClientValidationFunction="ValidaExpOtros" 
                                                
                                                ErrorMessage="En la pregunta 21 especifique otra expresion cultural" 
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                            <asp:TextBox ID="txtExpOtros" runat="server" BackColor="#E4E4E4" 
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr bgcolor="#F3F3F3">
                            <td style="margin-left: 120px" align="center" valign="top" colspan="3">
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                    ValidationGroup="Guardar" />
                            &nbsp;<asp:Button ID="cmdCerrar" runat="server" onclientclick="window.close()" 
                                    Text="Cerrar" Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ValidationGroup="Guardar" ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
