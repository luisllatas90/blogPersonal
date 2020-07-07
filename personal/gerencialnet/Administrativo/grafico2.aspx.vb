﻿'Clase del Formulario datos correspondiente al Modulo de Reportes Academicos
'Elaborado por  : Wilfredo Aljobin Cumpa
'Fecha          : 24/10/2006
'Observaciones  : Se encuentra el codigo para realizar todas las consultas al CUBO
'del analysis services, se han utilizado funciones dentro de la misma
'clase que ayudan a reducir un tanto el codigo fuente
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Partial Class Graficosave
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            'MuestraCabecera("Ingresantes por Semestre Academico y Escuela Profesional segun modalidad de Ingreso.")
            If Request.QueryString("tipo") = 1 Then
                MostrarReporte()
            Else
                MostrarReporte2()
            End If

        End If

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

    Protected Sub MostrarReporte()
        Try
            Dim objcubo As New cuboGrafAdm
            objcubo.Cubo = Session("cubo")
           
            objcubo._strConsulta = Session("consulta")
            If objcubo.MostrarDatos() IsNot Nothing Then
                objcubo.MostrarGrafNew(100, Response.OutputStream)
            Else
                Response.Write("<br><br><br><br><center><font size='3' face='Arial'>¡¡¡No se encontraron coincidencias de las opciones seleccionadas!!!<br>Intentelo Nuevamente</center>")
            End If
            objcubo = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub MostrarReporte2()
        Try
            Dim objcubo As New cuboGrafAca
            objcubo.Cubo = Session("cubo")

            objcubo._strConsulta = Session("consulta")
            If objcubo.MostrarDatos() IsNot Nothing Then
                objcubo.MostrarGrafNew2(100, Response.OutputStream)
            Else
                Response.Write("<br><br><br><br><center><font size='3' face='Arial'>¡¡¡No se encontraron coincidencias de las opciones seleccionadas!!!<br>Intentelo Nuevamente</center>")
            End If
            objcubo = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

End Class
