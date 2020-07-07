<html>
    <head>
        <link rel="stylesheet" type="text/css" href="../../private/estilo.css">
        <title> Clonar Personal </title>
        
    </head>
    <body>
        <form id="frmClonar" method="post" action="procesar.asp?Accion=clonarpermisos">                                  
            <%
                Dim rsPersonalDesde
                Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
                obj.AbrirConexion
                'Set rsPersonalDesde = Obj.Consultar("SEG_ListaPersonal","FO")
                Set rsPersonalDesde = Obj.Consultar("SEG_ListaPersonal","ST",NULL)                
                                
                obj.CerrarConexion                
            %>
            <table>
                <tr>
                <td>De:</td>
                <td>
                    <select name="cboPersonal" style="width:280px;">
			        <%If not(rsPersonalDesde.BOF and rsPersonalDesde.EOF) then			    
				        Do While Not rsPersonalDesde.EOF%>
				        <option value="<%=rsPersonalDesde("codigo_per")%>"><%=rsPersonalDesde("trabajador")%></option>
				        <%
				        rsPersonalDesde.movenext
				        Loop
			        end if%>
		            </select>
		        </td>
                </tr>
                <tr>
                <td>A: </td>
                <td>
                    <select name="cboPersonalClonar" style="width:280px;">
			        <% rsPersonalDesde.moveFirst
			           If not(rsPersonalDesde.BOF and rsPersonalDesde.EOF) then
				        Do While Not rsPersonalDesde.EOF%>
				        <option value="<%=rsPersonalDesde("codigo_per")%>"><%=rsPersonalDesde("trabajador")%></option>
				        <%
				        rsPersonalDesde.movenext
				        Loop
			        end if%>
		            </select>
		        </td>
                </tr>  
            </table>
            <br />
            <center><input type="submit" value="Clonar Usuario" class='usatnuevo' /></center>
        </form>
    </body>
</html>