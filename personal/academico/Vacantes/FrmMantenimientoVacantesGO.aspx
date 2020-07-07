<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoVacantesGO.aspx.vb" Inherits="academico_Vacantes_FrmMantenimientoVacantesGO" %>
	
	
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mantenimiento de Vacantes</title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
 <script type="text/javascript" language='javascript'>
     function confirma_nrovac() {
         var vac = 0
         var ing = 0
     vac = document.getElementById("txtvacantes").value
     ing = document.getElementById("txtnroingresantes").value
         if (parseInt(vac) < parseInt(ing)) {
             alert("N° de Vacantes ( " + vac + " ) Menor a N° de Ingresantes ( " + ing + " )")
         }
     }
 </script>
 <style type="text/css">
        
        /***** Para avisos **************/
        .mensajeError
        {
        	border: 1px solid #e99491;
        	background-color: #fed8d5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;  	
        }
        
         .mensajeExito
        {
        	border: 1px solid #488e00;
        	background-color:#c5f4b5;        	
        	padding-top:2px;        	
        	-moz-border-radius: 15px;
        	padding-left:25px;        	
        }
        
        /********************************/                        
                        
    </style>  
</head>
<body>

     <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" colspan="2" height="30px">
                <b>Mantenimiento de Vacantes</b></td>
            </tr>
        <tr>
        <td width="20%">
            Ciclo Academico:</td>
        <td width="75%">
            
            <asp:DropDownList ID="cboCac" runat="server" AutoPostBack="True" Width="125px" ValidationGroup="grupo1">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                runat="server" 
                                ControlToValidate="cboCac" 
                                initialvalue="-1"
                                ErrorMessage="Seleccione Ciclo Academico" 
                                ValidationGroup="grupo1" 
                                SetFocusOnError="true"
                                Display="Dynamic"
                                text="*"
                                > </asp:RequiredFieldValidator> 
        </td>
       
      </tr>
      <tr>
        <td width="20%">
            Carrera Profesional:</td>
        <td width="75%">
            <asp:DropDownList ID="cboCpf" runat="server" AutoPostBack="True" Width="300px">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                runat="server" 
                                ControlToValidate="cboCpf" 
                                initialvalue="-1"
                                ErrorMessage="Seleccione Escuela Profesional" 
                                ValidationGroup="grupo1" 
                                SetFocusOnError="true"
                                Display="Dynamic"
                                text="*"
                                > </asp:RequiredFieldValidator> 
        </td>
      </tr>
      <tr>
        <td width="20%">
            Modalidad de Ingreso:</td>
        <td width="75%">
            <asp:DropDownList ID="cboMin" runat="server" AutoPostBack="True" Width="300px">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                runat="server" 
                                ControlToValidate="cboMin" 
                                initialvalue="-1"
                                ErrorMessage="Seleccione Modalidad de Ingreso" 
                                ValidationGroup="grupo1" 
                                SetFocusOnError="true"
                                Display="Dynamic"
                                text="*"
                                > </asp:RequiredFieldValidator>             
        </td>
      </tr>
      <tr>
        <td width="20%" class="style1">
          N° de Vacantes:</td>
        <td width="75%" class="style1">
         <asp:TextBox ID="txtvacantes" runat="server" 
            Width="50px" ValidationGroup="grupo1"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                              runat="server" 
                              ControlToValidate="txtvacantes" 
                              Display="Dynamic" 
                              ErrorMessage="Ingrese solo números en el Campo de Vacantes." 
                              ValidationExpression="^(\-)?(?=.*[0-9].*$)\d*(?:\.\d+)?$"
                              ValidationGroup="grupo1" 
                              SetFocusOnError="true" 
                              EnableTheming="True"
                              Text="*"
                              > </asp:RegularExpressionValidator>
                              
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                                runat="server" 
                                ControlToValidate="txtvacantes" 
                                ErrorMessage="Debe ingresar el Número de Vacantes" 
                                ValidationGroup="grupo1" 
                                Display="Dynamic"
                                Text="*"
                                ></asp:RequiredFieldValidator> 
                                
                    <asp:CustomValidator ID="CustomValidator1" 
                                runat="server" 
                                ControlToValidate="txtvacantes" 
                                ErrorMessage="CustomValidator"
                                ClientValidationFunction="confirma_nrovac"
                                Text="*">
                                </asp:CustomValidator>
                                <asp:textbox ID="txtnroingresantes"  runat="server" ValidationGroup="grupo1" BorderColor="Transparent" Width="0"></asp:textbox>
         
        </td>
       </tr>
       <tr>
        <td width="20%">
          Estado:</td>
        <td width="75%">
          <asp:Checkbox ID="chkestado" runat="server"></asp:Checkbox>
        </td>
      </tr>
    </table>
    
    </div>   
            <div id="avisos" runat="server" style="height:25px; padding-top:2px; width: 50%;">
            <asp:Image ID="Image1" runat="server" ImageUrl="../../Images/beforelastchild.GIF"/>
            <asp:Label ID="lblmensaje" runat="server" Visible="False"></asp:Label> 
                   
            <asp:Label ID="lblCodigoVac" runat="server" Visible="false"></asp:Label>    
            </div> 
<div>    
        
        <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0"> 

            <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                <td style="width:80%">
                
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        DisplayMode="BulletList" ShowSummary="true" HeaderText="Errores encontrados:" 
                        BorderWidth="0" ValidationGroup="grupo1" Font-Bold="False" 
                        ForeColor="#FF0066"/>
                    </td>
                        <td align="right" valign ="top"  style="width:20%">
                                <asp:Button ID="btnguardar" runat="server" Text="   Guardar" 
                                    CssClass="guardar2" ValidationGroup="grupo1"  /> &nbsp;<asp:Button 
                                    ID="btnlimpiar" runat="server" Text="Limpiar" CssClass="regresar2" 
                                    Width="70px" />
                        </td>
            </tr>
        </table>
        <br />       
    </div> 
    <div>
      <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" colspan="2" height="30px">
                <b>Datos de Busqueda</b></td>
            </tr>
        <tr>
        <td width="20%">
            Ciclo Academico:</td>
        <td width="75%">
            
            <asp:DropDownList ID="cboCac2" runat="server" AutoPostBack="True" Width="125px" ValidationGroup="grupo2">
            </asp:DropDownList>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                                runat="server" 
                                ControlToValidate="cboCac2" 
                                initialvalue="-1"
                                ErrorMessage="Seleccione Ciclo Academico" 
                                ValidationGroup="grupo2" 
                                SetFocusOnError="true"
                                Display="Dynamic"
                                text="*"
                                > </asp:RequiredFieldValidator> 
        </td>
       
      </tr>
      <tr>
        <td width="20%">
            Carrera Profesional:</td>
        <td width="75%">
            <asp:DropDownList ID="cboCpf2" runat="server" AutoPostBack="True" Width="300px">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td width="20%">
            Modalidad de Ingreso:</td>
        <td width="75%">
            <asp:DropDownList ID="cbomin2" runat="server" AutoPostBack="True" Width="300px">
            </asp:DropDownList>        
        </td>
      </tr>
       <tr>
        <td colspan="2" align="right">
              <asp:Button ID="btnbuscar" runat="server"  Text="   Buscar" CssClass="buscar"/>
        </td>
      </tr>
    </table>  
    <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr>
           <td style="text-align:center" align="center">
                <asp:GridView ID="gvVacantes" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="codigo_Vac,codigo_cac,codigo_cpf,codigo_min,estado_vac"
                            EmptyDataText="No se encontraron registros" 
                            CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                            BorderWidth="1px">
                            <RowStyle ForeColor="#000066" />
                            <EmptyDataRowStyle ForeColor="#FF0066" />
                            <Columns>
                                <asp:BoundField HeaderText="Codigo Vacante" DataField="codigo_Vac" Visible="false" />
                                <asp:BoundField HeaderText="Ciclo Academico" DataField="descripcion_Cac" >
                                    <ItemStyle HorizontalAlign="Left" Width="12%" />
                                </asp:BoundField>                                              
                                <asp:BoundField HeaderText="Carrera Profesional" DataField="nombre_cpf" >
                                    <ItemStyle HorizontalAlign="Left" Width="40%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Modalidad de Ingreso" DataField="nombre_min" >
                                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                                </asp:BoundField>      
                                <asp:BoundField HeaderText="Vacantes" DataField="numero_vac" >
                                    <ItemStyle Width="15%" />
                                </asp:BoundField>                                                                                                                            
                                <asp:CommandField ShowSelectButton="True" 
                                    SelectImageUrl="../../images/editar.gif" ButtonType="Image" 
                                    SelectText="Modificar" >
                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" 
                                    DeleteImageUrl="../../images/eliminar.gif" ButtonType="Image" >
                                <HeaderStyle Width="15px" />
                                </asp:CommandField>
                            </Columns>
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#D1DDEF" Font-Bold="True" ForeColor="Black" />
                        </asp:GridView>
            </td>
        </tr>    
        </table>  
        </div>
    </form>                  
</body>
</html>
