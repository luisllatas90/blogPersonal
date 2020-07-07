<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EgresadoActualizar.aspx.vb" Inherits="frmpersona" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .style5
        {
            width: 120px;            
        }
        .style6
        {
            font-size:10px;
            font-weight:normal;
         }
         .style7
        {
            font-size:12px;
            font-weight:bold;
         }
          
        .style8
        {
            width: 400px;
        }
          
    </style>
</head>
<body>
      
       <table width="100%">
        <tr>
            <td style="width: 20%">
            <img src="../../../librerianet/Egresado/archivos/logousat.png" />
            </td>            
            <td>
                <span class="usatTitulo">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ACTUALIZACIÓN DE 
                FICHA DE EGRESADO</span>
            </td>
        </tr>
    </table> 
    <form id="frmPersona" runat="server"> 
    
    <table style="width: 100%" class="contornotabla" id="tablaNombre" runat="server">                 
    <tr>
        <td class="style5">
            <br />
            <asp:Image ID="FotoAlumno" runat="server" Height="107px" Width="89px" /></td>
        <td><asp:HiddenField ID="hdcodigo_pso" runat="server" />
            <span class="style6">Apellidos</span><br />
            <asp:Label ID="lblApellidoPat" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;&nbsp;
            <asp:Label ID="lblApellidoMat" runat="server" Text="Label" CssClass="style7"></asp:Label>&nbsp;&nbsp;</br>
                <br /><span class="style6">Nombres</span><br />
            <asp:Label ID="lblNombres" runat="server" Text="Label" CssClass="style7"></asp:Label></br>
        </td>  </tr>
        <tr>
        <td>
            <span class="style6">Sexo</span><br />
            
            <asp:Label ID="lblSexo" runat="server" Text="Label" CssClass="style7"></asp:Label> 
        </td>
        
        <td>
            <span class="style6">Fecha Nac.</span><br />
            <asp:Label ID="lblFechaNac" runat="server" Text="Label" CssClass="style7"></asp:Label>
        </td>
        </tr>
        <tr>
        <td>
            <span class="style6">DNI</span><br />
            <asp:Label ID="lblDNI" runat="server" Text="Label" CssClass="style7"></asp:Label></br>
        </td>
         <td>
            <span class="style6">Carrera Profesional</span><br />
            <asp:Label ID="lblCarrera" runat="server" Text="Label" CssClass="style7"></asp:Label></br>
        </td>
        </tr>                     
    
    </table>
    <br />
     <table style="width: 100%" class="contornotabla" id="Table1" runat="server">                         
        <tr><td colspan="2"><span class="style7">Datos Personales</br></span></td></tr>                
        <tr>
         <td class="style8">
            <span class="style6">Email Principal</span><br />
            <asp:TextBox ID="txtEmailP" runat="server" Width="294px"></asp:TextBox>
         </td>
         <td>
            <span class="style6">Email Alternativo</span><br />
            <asp:TextBox ID="txtEmailA" runat="server" Width="294px"></asp:TextBox>
         </td>
        </tr>
        <tr>
         <td class="style8" colspan="3">
            <span class="style6">Dirección</span><br />
            <asp:TextBox ID="txtDir" runat="server" Width="696px"></asp:TextBox>
         </td>         
        </tr>
        <tr>
            <td>
            <span class="style6">Departamento</span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="style6">Provincia</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="style6">Distrito</span>
            </td>         
        </tr>
        <tr>
            <td>
            <asp:DropDownList ID="dpdepartamento" runat="server" AutoPostBack="True" Width="150px" Height="18px"></asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dpprovincia" runat="server" AutoPostBack="True" Width="150px" Height="18px"></asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dpdistrito" runat="server" AutoPostBack="True" Width="150px" Height="18px"></asp:DropDownList>      
            </td>               
        </tr>
        
        <tr>        
         <td colspan="2">
            <span class="style6">Teléfono Fijo</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="style6">Teléfono Celular</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="style6">&nbsp;&nbsp;&nbsp;&nbsp; RUC</span>
            <span class="style6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Estado Civil</span>
         </td>
         </tr>
         <tr>
         <td colspan="2">
            <asp:TextBox ID="txtFijo" runat="server" Width="100px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtCelular" runat="server" Width="100px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtruc" runat="server" Width="100px" MaxLength="11"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dpEstadoCivil" runat="server" SkinID="ComboObligatorio" Width="30%">
                <asp:ListItem Value="-1">&gt;&gt;Seleccione&lt;&lt;</asp:ListItem>
                <asp:ListItem Value="SOLTERO">SOLTERO</asp:ListItem>
                <asp:ListItem Value="CASADO">CASADO</asp:ListItem>
                <asp:ListItem Value="VIUDO">VIUDO</asp:ListItem>
                <asp:ListItem Value="DIVORCIADO">DIVORCIADO</asp:ListItem>
           </asp:DropDownList>         
         </td>
        </tr>
        <tr>
        <td>
            
        <span class="style6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<!-- Conyuge--></span>&nbsp;&nbsp;</td>
        
        <td>
        <!--    &nbsp;&nbsp;<span class="style6">Fecha Matrimonio</span>--></td>
        </tr>
        <tr>
        <td>
                       
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtConyuge" runat="server" Width="267px" Visible="False"></asp:TextBox>                      
        </td>
        <td>
            &nbsp;
        <asp:TextBox ID="txtFechaMat" runat="server" Width="100px" Visible="False">--/--/----</asp:TextBox>
        </td>                   
        </tr>
        
        <tr>
             
        <td>       
            <span class="style6">Actualizar Foto</span></br>
                             
            <asp:FileUpload ID="fileFoto" runat="server" Width="70%" />
            <br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"    ErrorMessage="Tipo de archivo no permitido (*.jpeg, *.gif, *.png)" ControlToValidate="fileFoto"  ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$">
            </asp:RegularExpressionValidator>                            
            </td>
        
        </tr>
     </table>
     <br />
     <table style="width: 100%" class="contornotabla" id="Table2" runat="server">                 
        <tr><td><span class="style7">Datos Profesionales</br></span></td></tr>
        <tr>
                        <td class="style5">Se encuentra laborando: </td>
                        <td>                           
                            <asp:RadioButtonList ID="rblSituacionLaboral" runat="server" Width="140px" 
                                RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Text="Si" Value="S"></asp:ListItem><asp:ListItem Text="No" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>                                                   
                        </td>
                    </tr>                                        
                    <tr>
                        <td class="style5">Tipo de Empresa</td>
                        <td>
                            <asp:RadioButtonList ID="rblTipoEmpresa" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Privada" Value="P"></asp:ListItem>
                                <asp:ListItem Text="Pública" Value="N"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="style5">Sector:</td>
                        <td>
                            <asp:DropDownList ID="dpSector" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>
                        <td class="style5">Empresa donde labora:</td>
                        <td>
                            <asp:TextBox ID="txtEmpresaLabora" runat="server" Width="32%"></asp:TextBox>
                        &nbsp; Direccion Empresa:<asp:TextBox ID="txtDireccionEmpresa" runat="server" Width="32%"></asp:TextBox>
                        </td>
                    </tr>                                          
                    <tr>
                        <td class="style5">Telefono</td>
                        <td>
                            <asp:TextBox ID="txtTelefonoProfesional" runat="server" Width="32%"></asp:TextBox>
                        &nbsp; Correo Profesional:<asp:TextBox ID="txtCorreoProfesional" runat="server" 
                                Width="32%"></asp:TextBox>
                        </td>
                    </tr>  
                    <tr>
                        <td class="style5">Cargo Actual:</td>
                        <td>
                            <asp:TextBox ID="txtCargoActual" runat="server" Width="32%"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr> 
                    <tr>
                        <td colspan="2"><b>Currriculum Vitae</b></td> 
                    </tr>                    
                    <tr>
                        <td class="style5">Nivel</td>
                        <td>
                            <asp:DropDownList ID="dpNivel" runat="server" Enabled="true">                             
                                <asp:ListItem Value="BACHILLER">BACHILLER</asp:ListItem>
                                <asp:ListItem Value="EGRESADO">EGRESADO</asp:ListItem>
                                <asp:ListItem Value="TITULADO">TITULADO</asp:ListItem>
                            </asp:DropDownList>                            
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style5">
                        Formacion Académica:</td>
                        <td> 
                            <asp:TextBox ID="txtFormacion" runat="server" TextMode="MultiLine" 
                                Width="77.3%"></asp:TextBox>                  
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                        Experiencia Laboral:</td>
                        <td>   
                            <asp:TextBox ID="txtExperiencia" runat="server" TextMode="MultiLine" 
                                Width="77.3%"></asp:TextBox>                    
                        </td>
                    </tr>                    
                    <tr>
                        <td class="style5">
                        Otros Estudios:</td>
                        <td>   
                            <asp:TextBox ID="txtOtrosEstudios" runat="server" TextMode="MultiLine" 
                                Width="77.3%"></asp:TextBox>                  
                        </td>
                    </tr>                                    
                    <tr>
                        <td class="style5">
                        Curriculum:</td>
                        <td>                        
                            
                            <asp:FileUpload ID="fileCV" runat="server" Width="42%" />                               
                            
                               <br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ErrorMessage="Solo documentos (*.doc, *.docx, *.pdf) " ControlToValidate="fileCV" 
                                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.doc|.DOC|.docx|.DOCX|.pdf|.PDF)$">
                            </asp:RegularExpressionValidator>                            
                            <br />                            
                            <a href="#" runat="server" name = "lnkDescarga" id="lnkDescarga">Descargar Curriculum</a>
                        </td>
                        
                    </tr>                                        
                    <tr>
                        <td colspan="2"><b>Ultima actualizacion al <asp:Label ID="lblActualizacion" runat="server"></b></asp:Label>
                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="2" class="style1"><br />En promedio, cuantos meses le llevó conseguir un puesto de trabajo acorde a la formación que recibió después de titulado <br />
                            <asp:CheckBox ID="chkTresMeses" runat="server" Text="Antes de los 3 meses" 
                                AutoPostBack="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Indique el Número de meses
                            <asp:TextBox ID="txtNumMeses" runat="server"></asp:TextBox>
                        </td>                        
                    </tr> 
                    <tr>
                        <td colspan="2">                        
                            <br /><asp:CheckBox ID="chkMostrar" runat="server" Text="Mostrar Perfil" />
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" 
                                Text="[Permite que su perfil sea visible por las empresas]"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br /><br /><asp:Button ID="cmdGuardar" runat="server" Enabled="true" 
                                Text="Actualizar" SkinID="BotonGuardar" />
                            &nbsp;<asp:Button ID="cmdLimpiar" runat="server" SkinID="BotonLimpiar" 
                                Text="Limpiar" ValidationGroup="Limpiar" Width="86px" />
                            &nbsp;
                        </td>
                    </tr>        
     </table>
     <asp:HiddenField ID="HdFileCV" runat="server" />
    <asp:HiddenField ID="HdFileFoto" runat="server" />
    </form>
    
</body>
</html>

