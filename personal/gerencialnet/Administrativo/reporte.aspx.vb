'Clase del Formulario datos correspondiente al Modulo de Reportes Academicos
'Elaborado por  : Wilfredo Aljobin Cumpa
'Fecha          : 24/10/2006
'Observaciones  : Se encuentra el codigo para realizar todas las consultas al CUBO
'del analysis services, se han utilizado funciones dentro de la misma
'clase que ayudan a reducir un tanto el codigo fuente
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Partial Class Academico_datos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If IsPostBack = False Then
        '    MuestraCabecera("Total Recaudado de Ingresos segun Tipo de Documento, Escuelas profesionales y Servicios")
        '    MostrarReporte()
        'End If
        Dim pag As Integer
        Dim MDX As String = Nothing
        pag = Request.QueryString("pag")
        Select Case pag
            Case 1
                MuestraCabecera("Total Recaudado de Ingresos en el Año segun Facultades y Servicios")
                MDX = "SELECT NON EMPTY {tiempo.[2006]} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias],carreraprofesional.[humanidades]} on axis(2)"
                MDX = MDX & ", NON EMPTY {servicio.children} on axis(1)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where estado.ingreso"
                MostrarReporte(MDX)
            Case 2
                MuestraCabecera("Total Recaudado por pensiones en el Año segun escuelas profesionales")
                MDX = "SELECT NON EMPTY {tiempo.[2006]} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[pensiones]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where estado.ingreso"
                MostrarReporte(MDX)
            Case 3
                MuestraCabecera("Total Recaudado por Matricula en el Año segun escuelas profesionales")
                MDX = "SELECT NON EMPTY {tiempo.[2006]} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[matricula]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where estado.ingreso"
                MostrarReporte(MDX)
            Case 4
                MuestraCabecera("Total Recaudado en el Año por Pensiones segun Ciclo Academicos y Escuelas profesionales")
                MDX = "SELECT NON EMPTY {cicloacademico.regular.children} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[pensiones]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where (estado.ingreso, tiempo.[2006])"
                MostrarReporte(MDX)
            Case 5
                MuestraCabecera("Total Recaudado en el Año por Examen de Admision segun Ciclo Academicos y Escuelas profesionales")
                MDX = "SELECT NON EMPTY {cicloacademico.regular.children} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[admision]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where (estado.ingreso, tiempo.[2006])"
                MostrarReporte(MDX)
            Case 6
                MuestraCabecera("Total Recaudado en el Año por Matricula segun Ciclo Academicos y Escuelas profesionales")
                MDX = "SELECT NON EMPTY {cicloacademico.regular.children} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[matricula]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where (estado.ingreso, tiempo.[2006])"
                MostrarReporte(MDX)

            Case 7
                MuestraCabecera("Total de Ingresos en el Año por Otros Servicios segun Ciclo Academicos y Escuelas profesionales")
                MDX = "SELECT NON EMPTY {cicloacademico.regular.children} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[otros servicios]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where (estado.ingreso, tiempo.[2006])"
                MostrarReporte(MDX)
            Case 8
                MuestraCabecera("Total de Ingresos en el Año por Otros Servicios segun Ciclo Academicos y Escuelas profesionales")
                MDX = "SELECT NON EMPTY {cicloacademico.regular.children} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[otros servicios]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where (estado.ingreso, tiempo.[2006])"
                MostrarReporte(MDX)

            Case 8
                MuestraCabecera("Total de Ingresos en el Año por Otros Servicios segun Ciclo Academicos y Escuelas profesionales")
                MDX = "SELECT NON EMPTY {cicloacademico.regular.children} on axis(0) "
                MDX = MDX & ", NON EMPTY {carreraprofesional.[ciencias].children,carreraprofesional.[humanidades].children} on axis(1)"
                MDX = MDX & ", NON EMPTY {servicio.[otros servicios]} on axis(2)"
                MDX = MDX & ", NON EMPTY {measures.recaudado} on axis(3)"
                MDX = MDX & ", NON EMPTY {documento.[recibo de ingreso], documento.[recibo pre]} on axis(4)"
                MDX = MDX & " from cubcajaingreso "
                MDX = MDX & " where (estado.ingreso, tiempo.[2006])"
                MostrarReporte(MDX)


        End Select

    End Sub

    ' Codigo especialmente de diseño de la cabecera de la pagina, no se coloco directaemnte en la pagina
    ' porque le metodo responsewrite se ejecuta primero que el metodo LOAD
    Protected Sub MuestraCabecera(ByVal nombre As String)
        Dim HTML As String
        HTML = "<table width='100%'  align='center' border='0' cellspacing='0' cellpadding='0'>"
        HTML = HTML & "<table width='100%' border='0' cellspacing='0'><tr>"
        HTML = HTML & "<td width='65' height='60' align='right'><img class='NoImprimir' src='../images/cubo_.jpg' width='40' height='40'></td>"
        HTML = HTML & "<td width='651'><strong><font size='3' face='Arial'>" & nombre & "</font></strong></td><td width='263' align='right'>"
        'HTML = HTML & "<a href='javascript:abrir();'><img class='NoImprimir' border='0' alt='Reportes Personalizados' src='../images/softwareD.gif' width='31' height='31'></a>"
        HTML = HTML & "<a href='main.html' target='_self'><img class='NoImprimir' border='0' alt='Pulsa aquí para regresar a la página inicial' src='../images/home.jpg' width='31' height='29'></a>&nbsp;"
        HTML = HTML & "<a href='javascript:print()'><img class='NoImprimir' border='0' alt='Pulsa aquí para imprimir esta página' src='../images/printer.jpg' width='39' height='31'></a>"
        HTML = HTML & "<a href='graCubModalidadIngreso.htm'></a>"
        'HTML = HTML & "<a href='javascript:history.back()'><img class='NoImprimir' border='0' alt='Regresar' src='../images/back.jpg' width='32' height='32'></a></td>"
        HTML = HTML & "</tr></table><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr bgcolor='#990000'>"
        HTML = HTML & "<td  height='2' width='10'></td>"
        HTML = HTML & "<td></td></tr><tr align='center'><td colspan='2'></td></tr></table></td></tr><tr><td align='center'></td></tr></table>"
        Response.Write(HTML)
    End Sub

    ' Muestro la cabecere que me va a permitir realizar la funcion abrir el PopPup pata personalizacion de datos


    Protected Sub MostrarReporte(ByVal consulta As String)

        Dim objcubo As New cuboAdm
        objcubo.Cubo = "cubcajaingreso"
        objcubo._strConsulta = consulta
        If objcubo.MostrarDatos() IsNot Nothing Then
            Response.Write("&nbsp;&nbsp;&nbsp;&nbsp;<font face='arial' color='blue' size='1'>Actualizado al : " & objcubo.MuestraFecha() & "</font>")
            Response.Write(objcubo.MostrarDatos())
        Else
            Response.Write("<br><br><br><br><center><font size='3' face='Arial'>¡¡¡No se encontraron coincidencias de las opciones seleccionadas!!!<br>Intentelo Nuevamente</center>")
        End If
        objcubo = Nothing
    End Sub

End Class
