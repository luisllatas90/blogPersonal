
Partial Class Administrativo_ganancias
    Inherits System.Web.UI.Page

    Protected Sub ConsultaLoad()
        Dim StrSQL As String
        StrSQL = "SELECT NON EMPTY {measures.[monto est]} on axis(3),"
        StrSQL = StrSQL & " NON EMPTY {[codigo est].[Todas codigo est]} on axis(2),"
        StrSQL = StrSQL & " NON EMPTY {[estado].children} on axis(1),"
        StrSQL = StrSQL & " NON EMPTY {[codproceso].children} on axis(0) "
        StrSQL = StrSQL & " from cubGanancias"
        Session("consulta") = StrSQL
    End Sub

    Protected Sub MostrarReporte()
        Try
            Dim objcubo As New cuboGrafEPG
            objcubo.Cubo = Session("cubo")
            objcubo._strConsulta = Session("consulta")

            If objcubo.MostrarDatos() IsNot Nothing Then
                Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;<font face='arial' color='blue' size='1'>Actualizado al : " & objcubo.MuestraFecha() & "</font>")
                Response.Write(objcubo.MostrarDatos())
            Else
                Response.Write("<br><br><br><br><center><font size='3' face='Arial'>¡¡¡No se encontraron coincidencias de las opciones seleccionadas!!!<br>Intentelo Nuevamente</center>")
            End If
            objcubo = Nothing
        Catch ex As Exception
            Response.Write("<br><br><br><br><center><font size='3' face='Arial'>¡¡¡Ocurrio un error generar los datos!!!<br>Posiblemente no ha seleccionado ningun tipo de informacion. <br> !!Intentelo Nuevamente¡¡.</center>")
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Session("Titulo") = "Estado de Ganancias y Pérdidas por Naturaleza"
            Session("cubo") = "cubCajaIngreso"
            Session("cubo") = "cubGanancias"
            MuestraCabecera("Estado de Ganancias y Pérdidas por Naturaleza")
            ConsultaLoad()
            MostrarReporte()
        End If
    End Sub

    Protected Sub MuestraCabecera(ByVal nombre As String)
        Dim HTML As String
        HTML = "<table width='100%'  align='center' border='0' cellspacing='0' cellpadding='0'>"
        HTML = HTML & "<table width='100%' border='0' cellspacing='0'><tr>"
        HTML = HTML & "<td width='65' height='60' align='right'><img class='NoImprimir' src='../images/cubo_.jpg' width='40' height='40'></td>"
        HTML = HTML & "<td width='700'><strong><font size='3' face='Arial'>" & nombre & "</font></strong><font size='1' face='Arial'><br>Expresado en Nuevos Soles</font></td><td width='263' align='right'>"
        'HTML = HTML & "<a href='javascript:abrir();'><img class='NoImprimir' border='0' alt='Reportes Personalizados' src='../images/softwareD.gif' width='31' height='31'></a>"
        'HTML = HTML & "<a href='main.html' target='_self'><img class='NoImprimir' border='0' alt='Pulsa aquí para regresar a la página inicial' src='../images/home.jpg' width='31' height='29'></a>&nbsp;"
        HTML = HTML & "<a href='javascript:print()'><img class='NoImprimir' border='0' alt='Pulsa aquí para imprimir esta página' src='../images/printer.jpg' width='39' height='31'></a>"
        HTML = HTML & "<a href='graCubModalidadIngreso.htm'></a>"
        'HTML = HTML & "<a href='javascript:history.back()'><img class='NoImprimir' border='0' alt='Regresar' src='../images/back.jpg' width='32' height='32'></a></td>"
        HTML = HTML & "</tr></table><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr bgcolor='#990000'>"
        HTML = HTML & "<td  height='2' width='10'></td>"
        HTML = HTML & "<td></td></tr><tr align='center'><td colspan='2'></td></tr></table></td></tr><tr><td align='center'></td></tr></table>"
        Response.Write(HTML)
    End Sub

End Class
