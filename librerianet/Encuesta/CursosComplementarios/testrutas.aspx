<%@ Page Language="VB" AutoEventWireup="false" CodeFile="testrutas.aspx.vb" Inherits="Encuesta_CursosComplementarios_testrutas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script language="javascript" type="text/javascript">
    function validarOtroHorario(source, arguments)
    {   total =  parseInt(document.form1.hddTotal.value)
        for(i=0;i<total;i++)
        { 
            if (eval("document.form1.rblrpta2_" + i + ".checked")== true)
            {   valor = eval("document.form1.rblrpta2_" + i + ".value")
            }
        }
          
        if (valor == 7)
            if(form1.txtOtroHorario.value.length > 3)       
                arguments.IsValid=true
            else
                arguments.IsValid=false
        else
            arguments.IsValid=true   
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <%-- <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Text="  Guardar" ValidationGroup="Guardar" />--%>
       <input type="button" value="ir a librerianet" id="cmdGuardar" runat="server" onserverclick ="cmdGuardar_Click" />
    </div>
    </form>
</body>
</html>
