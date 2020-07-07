<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFichaEgresado.aspx.vb" Inherits="Egresado_frmFichaEgresado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>            
        <table width="100%">
            <tr>
                <td colspan="3" align="right">                
                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" Width="100px" Height="22px" CssClass="imprimir2" /> 
                    &nbsp;&nbsp;<input name="button" type="button" onclick="window.close();" value="Salir" class="salir" style="width:100px; height:22px" />                
                </td>
            </tr>
        </table>        
        <table style="width: 100%" class="contornotabla">
            <tr>
                <td colspan="3" align="center">
                    <img src="../../librerianet/Egresado/archivos/logousat.png" /> <br />
                    <span class="usatTitulo">ALUMNI - USAT</span>
                </td>                
            </tr>
                    <tr style="height:20px">
                        <td><b><u>Datos Personales</u></b></td>                        
                        <td></td>
                        <td rowspan=6>
                            <asp:Image ID="ImgEgresado" runat="server" Width="100px" Height="120px" />
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td>
                            <asp:Label ID="lblDocumento" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblNumDocumento" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                        
                    </tr>
                    
                    <tr style="height:20px">
                        <td> <b>Apellido Paterno: </b></td>
                        <td>
                            <asp:Label ID="lblAPaterno" runat="server" Text="" Width="250px"></asp:Label>
                        &nbsp;&nbsp; <b>Apellido Materno: </b>
                            <asp:Label ID="lblAMaterno" runat="server" Text="" Width="250px"></asp:Label>                            
                        </td>                        
                    </tr>                    
                    <tr style="height:20px">
                        <td><b>Nombres :</b></td>
                        <td><asp:Label ID="lblNombres" runat="server" Text="" Width="250px" ></asp:Label>                            
                            &nbsp;</td>                            
                    </tr>                    
                    <tr style="height:20px">
                        <td><b>Fecha Nac.: </b></td>
                        <td>
                            <asp:Label ID="lblFechaNac" runat="server" Text="" Width="20%"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                            <b>Sexo:</b>&nbsp;<asp:Label ID="lblSexo" runat="server" Text="" Width="20%"></asp:Label>                                
                            &nbsp;<b>Estado Civil:</b>&nbsp;<asp:Label ID="lblEstadoCivil" runat="server" Text="" Width="20%"></asp:Label>                            
                            
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td><b>Conyugue: </b></td>
                        <td>
                            <asp:Label ID="lblConyugue" runat="server" Text="" Width="38.5%"></asp:Label>
                        &nbsp;&nbsp;
                            <b>Fecha Matrimonio:</b>&nbsp;
                            <asp:Label ID="lblFecha" runat="server" Text=""></asp:Label>                            
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td><b>Email Principal</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblemail1" runat="server" Text="" Width="33%" ></asp:Label>                            
                         &nbsp; <b>Email Alternativo:</b>&nbsp;&nbsp;
                            <asp:Label ID="lblemail2" runat="server" Text="" Width="31.5%"></asp:Label>                            
                        </td>
                    </tr>                    
                    <tr style="height:20px">
                        <td><b>Dirección:</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblDireccion" runat="server" Text="" Width="77.3%"></asp:Label>                            
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td><b>Departamento:</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblDepartamento" runat="server" Text="" Width="20%"></asp:Label>                            
                            &nbsp;<b>Provincia:</b>
                            <asp:Label ID="lblProvincia" runat="server" Text="" Width="20%"></asp:Label>                            
                            &nbsp;<b>Distrito:</b>
                            <asp:Label ID="lblDistrito" runat="server" Text="" Width="20%"></asp:Label>                                                   
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td><b>Teléfono Fijo:</b></td>
                        <td colspan="2">      
                            <asp:Label ID="lblTelefono" runat="server" Text="" Width="20%"></asp:Label>                                              
                            &nbsp;<b> Celular:</b>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblCelular" runat="server" Width="20%"></asp:Label>                            
                        </td>
                    </tr>
                    <tr style="height:20px">
                        <td><b>Modalidad Ingreso</b></td>
                        <td colspan="2"><asp:Label ID="lblModalidad" runat="server" Text="" Width="20%"></asp:Label>
                        &nbsp;<b>RUC:</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblruc" runat="server" Text=""></asp:Label>                        
                        </td>
                    </tr>                                                                            
                    <tr style="height:20px">
                        <td colspan="3">&nbsp;</td>
                    </tr> 
                    <tr style="height:20px">
                        <td colspan="3"><b><u>Datos Profesionales</u></b></td>                        
                    </tr> 
                    <tr style="height:20px">
                        <td><b>Se encuentra laborando: </b></td>
                        <td colspan="2"> 
                            <asp:Label ID="lblSituacion" runat="server" Text="" Width="20%"></asp:Label>                                                      
                        &nbsp;<b>Tipo de Empresa:</b>
                            <asp:Label ID="lblTipoEmpresa" runat="server" Text="" Width="20%"></asp:Label>
                        &nbsp;<b>Sector: </b>
                            <asp:Label ID="lblSector" runat="server" Text="" Width="20%"></asp:Label>
                        </td>
                    </tr>                                                                                
                    <tr style="height:20px">
                        <td><b>Empresa donde labora:</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblEmpresaLabora" runat="server" Text="" Width="32%"></asp:Label>                            
                        &nbsp; <b>Direccion Empresa:</b>
                            <asp:Label ID="lblDireccionEmpresa" runat="server" Text="" Width="32%"></asp:Label>                        
                        </td>
                    </tr>                                          
                    <tr style="height:20px">
                        <td><b>Teléfono:</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblTelefonoProfesional" runat="server" Text="" Width="32%"></asp:Label>                            
                        &nbsp;&nbsp; <b>Correo Profesional: </b>
                            <asp:Label ID="lblCorreoProfesional" runat="server" Text="" Width="32%"></asp:Label>                        
                        </td>
                    </tr>  
                    <tr style="height:20px">
                        <td><b>Cargo Actual:</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblCargoActual" runat="server" Text="" Width="32%"></asp:Label>
                        </td>
                    </tr> 
                    <tr style="height:20px">
                        <td colspan="3">&nbsp;</td>
                    </tr> 
                    <tr>
                        <td colspan="3"><b><u>Currriculum Vitae</u></b></td> 
                    </tr>                    
                    <tr style="height:20px">
                        <td><b>Nivel</b></td>
                        <td colspan="2">
                            <asp:Label ID="lblNivel" runat="server" Text=""></asp:Label>                             
                        </td>
                    </tr>                    
                    <tr>
                        <td><b>Formacion Académica:</b></td>
                        <td colspan="2"> 
                            <asp:TextBox ID="txtFormacion" runat="server" TextMode="MultiLine" 
                                Width="77.3%" Enabled="False"></asp:TextBox>                  
                        </td>
                    </tr>
                    <tr>
                        <td><b>Experiencia Laboral:</b></td>
                        <td colspan="2">   
                            <asp:TextBox ID="txtExperiencia" runat="server" TextMode="MultiLine" 
                                Width="77.3%" Enabled="False"></asp:TextBox>                    
                        </td>
                    </tr>                    
                    <tr>
                        <td><b>Otros Estudios:</b></td>
                        <td colspan="2">   
                            <asp:TextBox ID="txtOtrosEstudios" runat="server" TextMode="MultiLine" 
                                Width="77.3%" Enabled="False"></asp:TextBox>                  
                        </td>
                    </tr>                                    
                    <tr style="height:20px">
                        <td><b>Curriculum:</b></td>
                        <td colspan="2">                        
                            <asp:LinkButton ID="lnkDescarga" runat="server" Font-Bold="True" 
                                Font-Overline="False" Font-Underline="True" ForeColor="#0683FF">Descargar CV</asp:LinkButton>
                               <br />
                        </td>
                    </tr>                                        
                    <tr style="height:20px">
                        <td colspan="3"><b>Ultima actualizacion al <asp:Label ID="lblActualizacion" runat="server"></b></asp:Label>
                        </td>                        
                    </tr>
                    <tr >
                        <td colspan="3" class="style1"><br />Promedio en meses que llevó conseguir un puesto de trabajo acorde a la formación que recibió después de titulado <br />
                            <asp:CheckBox ID="chkTresMeses" runat="server" Text="Antes de los 3 meses" 
                                Enabled="False" Font-Bold="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Indique el Número de meses
                            <asp:TextBox ID="txtNumMeses" runat="server" Enabled="False" Font-Bold="True"></asp:TextBox>
                        </td>                        
                    </tr> 
                    <tr>
                        <td colspan="3">                        
                            <br />
                            <asp:CheckBox ID="chkMostrar" runat="server" Text="Mostrar Perfil" 
                                Enabled="False" Font-Bold="True" />
                        &nbsp;&nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">        
                            <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>                                                                     
                </table>
            <asp:HiddenField ID="hdcodigo_pso" runat="server" Value="0" />
            <asp:HiddenField ID="HdFileCV" runat="server" />
            <asp:HiddenField ID="HdFileFoto" runat="server" />
    </div>
    </form>
</body>
</html>
