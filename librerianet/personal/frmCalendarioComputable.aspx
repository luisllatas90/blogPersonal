<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalendarioComputable.aspx.vb" Inherits="personal_frmCalendarioComputable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title></title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    
    <link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    
   
        <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
        <script src="../../private/PopCalendar.js" language="text/javascript" type="text/javascript"></script>
        <script src="../../private/jq/jquery.js" type="text/javascript"></script>
        <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
        
        
          <script language="javascript" type="text/javascript">
              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtdesde").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txthasta").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtFechaCierre").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtfechanolab").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtFInicio_Pem").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtFFin_Pem").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtFInicio_Pap").mask("99/99/9999");
                  });
              })

              $(document).ready(function() {
                  jQuery(function($) {
                  $("#txtFFin_Pap").mask("99/99/9999");
                  });
              })
    
         </script>
         
         <script type="text/javascript" language="javascript">

             function MarcarCursos(obj) {
                 //asignar todos los controles en array
                 var arrChk = document.getElementsByTagName('input');
                 for (var i = 0; i < arrChk.length; i++) {
                     var chk = arrChk[i];
                     //verificar si es Check
                     if (chk.type == "checkbox") {
                         chk.checked = obj.checked;
                         if (chk.id != obj.id) {
                             PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                         }
                     }
                 }
             }

             function PintarFilaMarcada(obj, estado) {
                 if (estado == true) {
                     obj.style.backgroundColor = "#FFE7B3"
                 } else {
                     obj.style.backgroundColor = "white"
                 }
             }

	   
    </script>
    
          <style type="text/css">
        TBODY {
	display: table-row-group;
}
a:Link {
	color: #000000;
	text-decoration: none;
}
              .style1
              {
                  width: 534px;
              }
        </style>
    
</head>
<body>
<form id="form1" runat="server">
<% Response.Write(ClsFunciones.CargaCalendario())%>
    <div>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:64%">
    <tr>
        <td bgcolor="#D1DDEF" height="30px" style="width:124px">
            <asp:Label ID="Label1" runat="server" Text="Periodo Laborable"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPeriodoLaborable" runat="server" 
            AutoPostBack="True" Width="100%" style="height: 22px">
            </asp:DropDownList>
        </td>
     </tr>                    
     
    </table>  
      
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:64%"> 
    <tr>
        <td >
            <table width="100%">  
                <tr>
                    <td colspan = "4"> <hr /></td>
                </tr>      
                <tr>
                    <td>
                        <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                            <tr>
                                <td bgcolor="#D1DDEF" colspan="4" height="30px">
                                <b>
                                    <asp:Label ID="Label5" runat="server" 
                                        Text="REGISTRO DEL CALENDARIO COMPUTABLE" 
                                        Font-Bold="True"></asp:Label>
                                </b>
                    </td>                                               
                </tr>
                <tr>
                    <td>
                              
                    </td>
                    </td>
                    <td >
                    </td>
                    <td>
                    </td>
                </tr>                                                      
                    <tr bgcolor="#F5F9FC">
                        <td style="width:124px">
                            <asp:Label ID="Label7" runat="server" Text="Nº Semana"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlSemana" runat="server" Width="130px" 
                                AutoPostBack="True">
                                <asp:ListItem Value="0">Semestral</asp:ListItem>
                                <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                <asp:ListItem Value="--SELECCIONE--">--SELECCIONE--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblRangoFechas" runat="server" Font-Size="Smaller" 
                                ForeColor="Blue"></asp:Label>
                        </td>                        
                    </tr>                   
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Mes"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMes" runat="server" Width="130px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="width:124px">
                            <asp:Label ID="Label8" runat="server" Text="Desde"></asp:Label>
                        </td>
                        <td align="right">
                              <asp:TextBox ID="txtdesde" runat="server" Font-Names="Arial" Font-Size="X-Small" Width="103px">
                              </asp:TextBox>
                              <input id="btnFechaInicio" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtdesde,'dd/mm/yyyy')" class="cunia" type="button" />
                        </td>                                                
                    </tr>                                        
                    <tr bgcolor="#F5F9FC">
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Año"></asp:Label>
                        </td>
                        <td style="width:20%">
                            <asp:DropDownList ID="ddlAño" runat="server" Width="130px">
                            </asp:DropDownList>
                        </td>
                        <td style="width:124px" align="left">
                            <asp:Label ID="Label3" runat="server" Text="Hasta"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:TextBox ID="txthasta" runat="server" Font-Names="Arial" Font-Size="X-Small" 
                        Width="103px"></asp:TextBox>
                            <input id="Button1" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txthasta,'dd/mm/yyyy')" class="cunia" type="button" />
                        </td>                                                
                     </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnAñadir" Width="124px" runat="server" Text="Agregar" CssClass="agregar2" />
                        </td>                                                
                    </tr> 
                    <tr>
                        <td colspan="4">
                         <table width="100%">
                <tr bgcolor="#F5F9FC">
                    <td align="center">                        
                        <asp:GridView ID="gvCalendarioComputable" runat="server" CellPadding="3"  Width="100%"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderStyle="None" 
                            DataKeyNames="Codigo" BackColor="White" BorderColor="#CCCCCC" 
                            BorderWidth="1px">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="Semana" HeaderText="Semana" />
                                    <asp:BoundField DataField="Mes" HeaderText="Mes" />
                                    <asp:BoundField DataField="Año" HeaderText="Año" />
                                    <asp:BoundField DataField="Desde" HeaderText="Desde" />
                                    <asp:BoundField DataField="Hasta" HeaderText="Hasta" />
                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" 
                                        ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>                         
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>                
                </table>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#D1DDEF" colspan="4" height="30px">
                            <asp:Label ID="Label22" runat="server" Font-Bold="True" 
                                Text="MES VIGENTE - FECHA DE CIERRE"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label21" runat="server" Text="Mes Vigente"></asp:Label>
                         </td>
                         <td>
                             <asp:DropDownList ID="ddlMesVigente"  Width="130px" runat="server" 
                                 Enabled="False" AutoPostBack="True">
                             </asp:DropDownList>
                        </td>
                        <td>
                        
                            <asp:CheckBox ID="chkMesVigente" runat="server" Text="Actualizar" 
                                AutoPostBack="True" />
                        
                        </td>
                        <td align="right">
                        
                            <asp:Button ID="btnMesVigente" runat="server" Text="Guardar" Width="124px" CssClass="agregar2" />
                        
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4"  bgcolor="#FFFFCC"  >                        
                            <asp:Label ID="lblMesVigente" runat="server" Font-Bold="True" 
                                Font-Size="Smaller" ForeColor="#990000"  ></asp:Label>                        
                        </td>
                    </tr> 
                    <tr>
                        <td>
                            Fecha Cierre</td>
                        <td>
                            <asp:TextBox ID="txtFechaCierre" runat="server" Width="100px" Enabled="False"></asp:TextBox>
                                <input id="btnFechaCierre" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaCierre,'dd/mm/yyyy')" class="cunia" runat="server" disabled="disabled" type="button" />
                                </td>
                        <td>
                             <asp:CheckBox ID="chkFechaCierre" runat="server" Text="Actualizar" 
                                AutoPostBack="True" />                        
                        </td>
                        <td align="right">
                            <asp:Button ID="btnGuardarFechaCierre" runat="server" Text="Guardar" CssClass="agregar2" Width="124px" />
                        </td>
                    </tr>
                     <tr>
                        <td colspan="4"  bgcolor="#FFFFCC">                        
                            <asp:Label ID="lblFechaCierre" runat="server" Font-Bold="True" 
                                Font-Size="Smaller" ForeColor="#990000" ></asp:Label>                        
                        </td>
                    </tr> 
                                                                        
                </table>
                <br />
                <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                    <tr>
                        <td bgcolor="#D1DDEF" colspan="4" height="30px">
                            <asp:Label ID="Label23" runat="server" Font-Bold="True" 
                                Text="REGISTRO DE DIAS NO LABORABLES"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" bgcolor="#FFFFCC" >
                            <asp:Label ID="Label24" runat="server" ForeColor="#990000" Text="Usted puede importar los dias de un periodo anterior si lo desea."></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        
                        <td>
                            
                            <asp:Label ID="Label25" runat="server" Text="Periodo Laboral"></asp:Label>
                            
                        </td>         
                        <td align="right" colspan="2">
                            <asp:DropDownList ID="ddlPeriodo1" runat="server" Width="100%">
                            </asp:DropDownList>
                       </td>
                       <td  align="right" >
                       
                            <asp:Button ID="btnImportar1" runat="server" Text="      Importar" 
                                CssClass="agregar2" Width="124px" />
                       
                       </td>                                       
                    </tr>
                     <tr>
                        <td colspan="4" bgcolor="#FFFFCC" >
                            <asp:Label ID="Label9" runat="server" ForeColor="#990000" Text="Registre manualmente los dias no laborables"></asp:Label>
                        </td>
                    </tr>
                    <tr bgcolor="#F5F9FC">
                        <td style="width:124px">
                            <asp:Label ID="Label10" runat="server" Text="Descripción"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescripcion" runat="server" Width="98%"></asp:TextBox>
                        </td>                        
                    </tr>
                    <tr>
                        <td style="width:124px">
                            <asp:Label ID="Label11" runat="server" Text="Tipo"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoDiaNoLaborable" runat="server" Width="130px">
                                <asp:ListItem Value="--SELECCIONE--">--SELECCIONE--</asp:ListItem>
                                <asp:ListItem Value="FC">Feriado Calendario</asp:ListItem>
                                <asp:ListItem Value="FI">Feriado Institucional</asp:ListItem>
                                <asp:ListItem Value="FG">Feriado Gobierno</asp:ListItem>
                                <asp:ListItem Value="SL">Suspensión Labores</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width:124px">
                            <asp:Label ID="Label12" runat="server" Text="Fecha"></asp:Label>
                        </td>
                        <td align="right">
                              <asp:TextBox ID="txtfechanolab" runat="server" Font-Names="Arial" Font-Size="X-Small" Width="130px"></asp:TextBox>
                        <input id="Button2" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtfechanolab,'dd/mm/yyyy')" class="cunia" type="button" />
                        </td>
                    </tr>
                    <tr bgcolor="#F5F9FC">
                        <td>
                            &nbsp;</td>          
                        <td>
                            &nbsp;</td>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnAgregarDiaNoLab" runat="server" Text="Agregar" 
                                CssClass="agregar2" Width="124px" /></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        <asp:GridView ID="gvDiaNoLab" runat="server" CellPadding="3"  Width="100%" 
                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderStyle="None" 
                            DataKeyNames="Codigo" Font-Bold="False" BackColor="White" 
                                BorderColor="#CCCCCC" BorderWidth="1px">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                                    <asp:BoundField DataField="TipoDia" HeaderText="TipoDia" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" 
                                        ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>                         
                        </td>
                    </tr>
                    <tr>
                    <td>&nbsp;</td>
                    </tr>  
                </table>
              </td>
        </tr>
    </table>
    <br />
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
              <td colspan = "4"> &nbsp;</td>
        </tr>
              <tr>
                        <td bgcolor="#D1DDEF" colspan="4" height="30px">
                            <asp:Label ID="Label26" runat="server" Font-Bold="True" 
                                Text="REGISTRAR EL PERSONAL EXCEPTUADO DE MARCACIÓN"></asp:Label>
                        </td>
               </tr>
            <tr>
                <td >
                    <asp:Label ID="Label27" runat="server" Text="Periodo Laboral"></asp:Label>
                </td>
                <td colspan="2" >
                        <asp:DropDownList ID="ddlPeriodo2" runat="server" Width="100%">
                        </asp:DropDownList>
                        </td>
                        
                <td colspan="2" align="right">
                        <asp:Button ID="btnImportar2" runat="server" Text="Importar" 
                        CssClass="agregar2" Width="124px" />
                </td>
            </tr>
        <tr bgcolor="#F5F9FC">
            <td style="width:124px">
                            <asp:Label ID="Label13" runat="server" Text="Trabajador" 
                                Font-Bold="True"></asp:Label>
                        </td>
            <td colspan="3">
                <asp:DropDownList ID="ddlPersonal" runat="server" Width="100%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td style="width:124px">
                <asp:Label ID="Label15" runat="server" Text="Fecha Inicio" Font-Bold="True"></asp:Label>
            </td>
            <td>
                
                <asp:TextBox ID="txtFInicio_Pem" runat="server" Width="103px"></asp:TextBox>
                <input id="Button3" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFInicio_Pem,'dd/mm/yyyy')" class="cunia" type="button" />
                
            </td>
            <td style="width:124px">
                <asp:Label ID="Label14" runat="server" Text="Fecha Fin" Font-Bold="True"></asp:Label>
            </td>
            <td align="right">
                <asp:TextBox ID="txtFFin_Pem" runat="server" Width="103px"></asp:TextBox>
                <input id="Button4" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFFin_Pem,'dd/mm/yyyy')" class="cunia" type="button" />
            </td>
        </tr>
        <tr bgcolor="#F5F9FC">
            <td></td>
            <td></td>
            <td></td>
            <td colspan="2" align="right">
                <asp:Button ID="btnAñadirPem" runat="server" Text="Agregar" Width="124px" 
                    CssClass="agregar2" />
            </td>            
        </tr>
        <tr>
            <td colspan="4">
                
                        <asp:GridView ID="gvPersonalExceptuado" runat="server" CellPadding="3"  Width="100%" 
                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderStyle="None" 
                            DataKeyNames="Codigo" BackColor="White" BorderColor="#CCCCCC" 
                            BorderWidth="1px">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="Personal" HeaderText="Personal" />
                                    <asp:BoundField DataField="FechaInicio" HeaderText="FechaInicio" />
                                    <asp:BoundField DataField="FechaFin" HeaderText="FechaFin" />
                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                    <asp:BoundField DataField="Vigencia" HeaderText="Vigencia" />
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" 
                                        ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>                         
                
                </td>
        </tr>
    </table>
    <br />
    <br />
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        
         <tr>
                        <td bgcolor="#D1DDEF" colspan="4" height="30px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                Text="CONFIGURACIÓN DE TOLERANCIAS"></asp:Label>
                        </td>
       </tr>
       <tr>
                        <td colspan="4" bgcolor="#FFFFCC" >
                            <asp:Label ID="Label16" runat="server" ForeColor="#990000" Text="Usted puede importar los parámetros de tolerancia de un periodo anterior."></asp:Label>
                        </td>
        </tr>
        <tr>
            <td style="width:20%">
                            <asp:Label ID="Label29" runat="server" Text="Periodo Laboral"></asp:Label>    
            </td>
            <td style="width:50%">
                            <asp:DropDownList ID="ddlPeriodo3" runat="server" Width="100%"> </asp:DropDownList>
            </td>
            <td colspan="2" align="right" style="width:20%">
                <asp:Button ID="btnImportar3" runat="server" Text="Importar" CssClass="agregar2" Width="100%" />
            </td>
        </tr>
         <tr>
                        <td colspan="4" bgcolor="#FFFFCC" >
                            <asp:Label ID="Label28" runat="server" ForeColor="#990000" Text="Registre manualmente los parámetros de tolerancia para el control de horarios."></asp:Label>
                        </td>
           </tr>     
        <tr>
            <td colspan="4" >
                <table width="100%">
                <tr bgcolor="#F5F9FC">
                    <td  style="width:124px">
                            <asp:Label ID="Label17" runat="server" Text="Parámetro" 
                                Font-Bold="True"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:DropDownList ID="ddlParametro" runat="server" Width="100%" AutoPostBack="True">
                        </asp:DropDownList>                                        
                    </td>
                </tr>
                    <tr>
                        <td  style="width:124px">
                <asp:Label ID="Label19" runat="server" Text="Fecha inicio" 
                 Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtFInicio_Pap" runat="server" Width="103px"></asp:TextBox>
                <input id="Button5" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFInicio_Pap,'dd/mm/yyyy')" class="cunia" type="button" />
                        </td>
                        <td>
                            <asp:Label ID="Label20" runat="server" Text="Fecha fin" 
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                <asp:TextBox ID="txtFFin_Pap" runat="server" Width="103px"></asp:TextBox>
                <input id="Button6" onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFFin_Pap,'dd/mm/yyyy')" class="cunia" type="button" />
                        </td>
                        <td>
                            <asp:Label ID="Label18" runat="server" Text="Valor" 
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td><asp:TextBox ID="txtValor" runat="server" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            
            </td>
        </tr>
        <tr bgcolor="#F5F9FC">
            <td style="width:124px" align="right">
                 &nbsp;</td>
            <td></td>
            <td align="right">
                 <asp:Button ID="btnAñadirPap" runat="server" Text="Agregar" CssClass="agregar2" Width="124px" />
            </td>
        </tr>
        <tr>
            <td colspan="4">                
                        <asp:GridView ID="gvParametroTolerancia" runat="server" CellPadding="3"  Width="100%" 
                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderStyle="None" 
                            DataKeyNames="Codigo" BackColor="White" BorderColor="#CCCCCC" 
                            BorderWidth="1px">
                                <RowStyle ForeColor="#000066" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                                    <asp:BoundField DataField="Parametro" HeaderText="Parametro" />
                                    <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" />
                                    <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                    <asp:BoundField DataField="FechaInicio" HeaderText="FechaInicio" />
                                    <asp:BoundField DataField="FechaFin" HeaderText="FechaFin" />                                    
                                    
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="../../images/eliminar.gif" 
                                        ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>                         
                
                </td>
        </tr>
    </table>
    </td>
    </tr>
    </table>    
    </div>
    </form>
</body>
</html>
