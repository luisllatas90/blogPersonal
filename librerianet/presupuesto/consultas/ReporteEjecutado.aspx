<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteEjecutado.aspx.vb" Inherits="librerianet_presupuesto_consultas_ReporteEjecutado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script>

/*colocar esto en un bloque script
ref: http://digitalbush.com/projects/masked-input-plugin/
*/

$(document).ready(function(){
jQuery(function($){
   $("#TxtFechaD").mask("99/99/9999");
   $("#TxtFechaH").mask("99/99/9999");//.mask("(999)-999999");
//   $("#txttelefono").mask("(999)-9999999");
//   $("#txtcelular").mask("(999)-9999999");  
});

})

    function MarcarTodosCC()
    { 
      var i;
      var finCC;
      finCC = parseInt(document.form1.hddCC.value)- 1;
      valor = document.form1.chkTodosCC.checked;
      for (i=0;i<=finCC;i++)
      {  eval("document.form1.chklCecos_" + i + ".checked=" + valor );
      }
    }
    
    function MarcarTodosPP()
    { 
      var i;
      var finPP;
      finPP = parseInt(document.form1.hddPP.value)- 1;
      //if (document.form1.chkTodosPP.checked == true)
      valor = document.form1.chkTodosPP.checked;
      for (i=0;i<=finPP;i++)
      {  eval("document.form1.chklProgPresupuestal_" + i + ".checked=" + valor );
      }
    }
    
    function VerificarMarcadosCC()
    { 
      var i;
      var finCC;
      var marcados;
      var cant;
      finCC = parseInt(document.form1.hddCC.value);
      i=0;
      cant = 0;
      marcados = true;
     
      do 
      //for (i=0;i<=finCC;i++)
      {  if (eval("document.form1.chklCecos_" + i + ".checked")== true )
         {  marcados = true;
            cant++;
         }
         else
         {  marcados = false;
         }
         i++;
      }while(marcados == true && cant <= finCC-1 );
      if (cant == finCC)
         document.form1.chkTodosCC.checked = true;
      else
         document.form1.chkTodosCC.checked = marcados;
    }
    
    function VerificarMarcadosPP()
    { var i;
      var finPP;
      var marcados;
      var cant;  
      finPP = parseInt(document.form1.hddPP.value);
      cant =0;
      i=0;
      marcados = true;
      do 
      //for (i=0;i<=finPP;i++)
      {  if (eval("document.form1.chklProgPresupuestal_"  + i + ".checked") == true )
         {  marcados = true;
            cant++;
         }
         else
         {  marcados = false;
         }   
         i++;
      }while(marcados == true && cant <= finPP-1 );
      if (cant == finPP)
         document.form1.chkTodosPP.checked = true;
      else
         document.form1.chkTodosPP.checked = marcados;
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
        <table class="ContornoTabla1" width="100%">
            <tr>
                <td>
                    Desde: 
                                <asp:TextBox ID="TxtFechaD" runat="server" Width="80px"></asp:TextBox>
                              <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtFechaD" 
                                    ErrorMessage="Fecha de nacimiento es obligatorio" 
                        ValidationGroup="Consultar">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                        ControlToValidate="TxtFechaD" 
                        ErrorMessage="El valor de la fecha inicio debe ser entre el 01/01/2010 al 30/09/2014" 
                        MaximumValue="30/09/2030" MinimumValue="01/01/2010" 
                        ValidationGroup="Consultar">*</asp:RangeValidator>
                &nbsp;Hasta: 
                                <asp:TextBox ID="TxtFechaH" runat="server" Width="80px"></asp:TextBox>
                              <asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtFechaH" 
                                    ErrorMessage="Fecha de nacimiento es obligatorio" 
                        ValidationGroup="Consultar">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" 
                        ControlToValidate="TxtFechaH" 
                        ErrorMessage="El valor de la fecha final debe ser entre el 01/01/2010 al 30/09/2014" 
                        MaximumValue="30/09/2030" MinimumValue="01/01/2010" 
                        ValidationGroup="Consultar">*</asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBoxList ID="chklEstado" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="EJECUTADO">Ejecutado</asp:ListItem>
                        <asp:ListItem Value="COMPROMETIDO">Comprometido</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td>
                    Centro de costos
                    <asp:CheckBox ID="chkTodosCC" runat="server" Text="Todos" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel1" runat="server" Width="100%" 
                        BorderWidth="1px" ScrollBars="Vertical" Height="100px">
                        <asp:CheckBoxList ID="chklCecos" runat="server" >
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    Programa presupuestal                  <asp:CheckBox ID="chkTodosPP" runat="server" Text="Todos" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <asp:Panel ID="Panel2" runat="server"  Width="100%" 
                        ScrollBars="Vertical" BorderWidth="1px" Height="100px">
                        <asp:CheckBoxList ID="chklProgPresupuestal" runat="server">
                        </asp:CheckBoxList>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <asp:Button ID="cmdConsultar" runat="server" Text="Consultar" Width="75px" 
                        ValidationGroup="Consultar" />
                &nbsp;<asp:Button ID="cmdExportar" runat="server" Text="Exportar" Width="75px" />
                </td>
            </tr>
        </table>
    
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalEjecutado" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvEjecutadoConsolidado" runat="server">
                    <EmptyDataTemplate>
                        <asp:Label ID="Label1" runat="server" BorderStyle="None" ForeColor="Red" 
                            Text="No se encontraron registros para esta consulta"></asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#FF9900" />
                </asp:GridView>
            </td>
        </tr>
    </table>
        <asp:HiddenField ID="hddPP" runat="server" />
        <asp:HiddenField ID="hddCC" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="Consultar" />
    </form>
</body>
</html>
