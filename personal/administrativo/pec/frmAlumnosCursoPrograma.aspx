<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAlumnosCursoPrograma.aspx.vb" Inherits="administrativo_pec_frmAlumnosCursoPrograma" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consolidad de Alumnos por Curos y Programa</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/PopCalendar.js"></script>
    
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
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <p class="usatTitulo">Número de alumnos por Curo-Programa</p>
         <table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
            <tr>
                <td>
                    <asp:GridView ID="gvLista" Width="100%" runat="server">
                     <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
                    </asp:GridView>
                </td>
            </tr>
         </table>
    </div>
    </form>
</body>
</html>
