Imports System.Data.SqlClient

Partial Class Default3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim pagina As String = ""
            pagina = "<html>"
            pagina = pagina & "<head runat='server'>"
            pagina = pagina & "<title></title>"
            pagina = pagina & "<link href='css/estilos.css' rel='stylesheet' type='text/css' />"
            pagina = pagina & "</head>"
            pagina = pagina & "<body>"            
            pagina = pagina & "<form id='form1' runat='server'>"
            If Request.QueryString("m") IsNot Nothing Then
                pagina = pagina & "<div id='conte-cuerpo'>" & fn_Tabla(Request.QueryString("m")) & "</div>"
            Else
                pagina = pagina & "<div id='conte-cuerpo'>" & fn_Tabla(Date.Today.Month) & "</div>"
            End If

            pagina = pagina & "</form>"
            pagina = pagina & "</body>"
            pagina = pagina & "</html>"

            Response.Write(pagina)
        Catch ex As Exception
            Response.Write("Problemas al generar calendario: " & ex.Message)
        End Try
    End Sub

    Private Function fn_Tabla(ByVal mes As Integer) As String
        Dim strTablaPrincipal As String = ""
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        dt = obj.TraerDataTable("PLAN_CalendarioExterno", mes, Date.Today.Year)
        obj.CerrarConexion()

        strTablaPrincipal = "<table id='calendario-de-eventos' border='0' cellpadding='0' cellspacing='0' runat='server'>"
        strTablaPrincipal = strTablaPrincipal & fn_cabecera()
        strTablaPrincipal = strTablaPrincipal & "<tr><td id='contenedor-cal' colspan='14'>"
        For i As Integer = 0 To dt.Rows.Count - 1
            strTablaPrincipal = strTablaPrincipal & fn_cuerpo(dt.Rows(i).Item("titulo_apr"), _
                                                            dt.Rows(i).Item("descripcion_apr"), _
                                                            dt.Rows(i).Item("DiaInicio"), _
                                                            dt.Rows(i).Item("DiaNombreInicio"), _
                                                            dt.Rows(i).Item("MesInicio"), _
                                                            dt.Rows(i).Item("DiaFin"), _
                                                            dt.Rows(i).Item("DiaNombreFin"), _
                                                            dt.Rows(i).Item("MesFin"))
        Next
        strTablaPrincipal = strTablaPrincipal & "</td></tr>"
        strTablaPrincipal = strTablaPrincipal & "</table>"
        dt.Dispose()
        obj = Nothing
        Return strTablaPrincipal
    End Function

    Private Function fn_cabecera() As String
        Dim strCabecera As String = ""
        '"<th style='color: rgb(231, 65, 65);' id='year'>" & Date.Today.Year & "</th>" & _
        strCabecera = strCabecera & "<thead><tr> " & _
                    "<th style='color: gray;' id='enero-cal'><a href='frmCalendario.aspx?m=1'>Enero</a></th>" & _
                    "<th style='color: gray;' id='febrero-cal'><a href='frmCalendario.aspx?m=2'>Febrero</a></th>" & _
                    "<th style='color: gray;' id='marzo-cal'><a href='frmCalendario.aspx?m=3'>Marzo</a></th>" & _
                    "<th style='color: gray' id='abril-cal'><a href='frmCalendario.aspx?m=4'>Abril</a></th>" & _
                    "<th style='color: gray;' id='mayo-cal'><a href='frmCalendario.aspx?m=5'>Mayo</a></th>" & _
                    "<th style='color: gray;' id='junio-cal'><a href='frmCalendario.aspx?m=6'>Junio</a></th>" & _
                    "<th style='color: gray;' id='julio-cal'><a href='frmCalendario.aspx?m=7'>Julio</a></th>" & _
                    "<th style='color: gray;' id='agosto-cal'><a href='frmCalendario.aspx?m=8'>Agosto</a></th>" & _
                    "<th style='color: gray;' id='septiembre-cal'><a href='frmCalendario.aspx?m=9'>Septiembre</a></th>" & _
                    "<th style='color: gray;' id='octubre-cal'><a href='frmCalendario.aspx?m=10'>Octubre</a></th>" & _
                    "<th style='color: gray;' id='noviembre-cal'><a href='frmCalendario.aspx?m=11'>Noviembre</a></th>" & _
                    "<th style='color: gray;' id='diciembre-cal'><a href='frmCalendario.aspx?m=12'>Diciembre</a></th>" & _
                    "<th style='color: rgb(231, 65, 65);' class='borde-izq' id='year'>" & (Date.Today.Year) & "</th>" & _
                    "</tr></thead>"
        '
        Return strCabecera
    End Function

    Private Function fn_cuerpo(ByVal Titulo As String, ByVal descripcion As String, _
                                ByVal diaInicio As Integer, ByVal nombreDiaIni As String, _
                                ByVal nombreMesIni As String, ByVal diaFin As Integer, _
                                ByVal nombreDiaFin As String, ByVal nombreMesFin As String) As String
        Dim strCuerpo As String = ""
        strCuerpo = "<table class='rango-fechas-calendario' border='0' cellpadding='0' cellspacing='0' width='930'>"
        strCuerpo = strCuerpo & "<tbody><tr>" & _
                    "<td class='contenedor-bloque' width='780'>" & _
                    "<span class='fecha'>" & nombreMesIni & "<br/>" & _
                    "<span style='font-size: 30px;'>" & diaInicio & "</span><br/>" & _
                    nombreDiaIni & "</span>"
        strCuerpo = strCuerpo & "<span class='contenido'>" & _
                    "<span class='titulo'>" & Titulo & "</span><br/> " & _
                    "<span class='campus'>Campus: USAT</span><br/><br/> " & _
                    "<span class='descripcion'>" & descripcion & "</span></span>" & _
                    "</td>"
        strCuerpo = strCuerpo & "<td bgcolor='white' width='10'>" & _
                    "<div id='flecha-calendario'><div id='flecha'></div></div></td>" & _
                    "<td class='contenedor-bloque' width='80'>" & _
                    "<span style='margin-right: 0px ! important;' class='fecha'> " & _
                    nombreMesFin & "<br/><span style='font-size: 30px;'>" & _
                    diaFin & "</span><br/>" & nombreDiaFin & "</span></td>" & _
                    "</tr></tbody>"
        strCuerpo = strCuerpo & "</td></tr></tbody>"
        strCuerpo = strCuerpo & "</table>"
        Return strCuerpo
    End Function
End Class
