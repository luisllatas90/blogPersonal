Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data.SqlClient


Public Class clsPlanCalendario

    Public Function getEvents(ByVal Codigo_pro As String) As List(Of EventosCalendario)
        Dim events As New List(Of EventosCalendario)
        Dim obj As New ClsConectarDatos
        Dim dtEventos As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'Ejecutamos nuestra consulta
            obj.AbrirConexion()
            dtEventos = obj.TraerDataTable("PLAN_BuscaActividadProyecto", 0, Codigo_pro, "")
            obj.CerrarConexion()

            'Asignamos a la Lista
            For i As Integer = 0 To dtEventos.Rows.Count - 1
                Dim cevent As New EventosCalendario

                cevent.id = Integer.Parse(dtEventos.Rows(i).Item("codigo_apr"))
                cevent.title = dtEventos.Rows(i).Item("titulo_apr").ToString
                cevent.description = dtEventos.Rows(i).Item("descripcion_apr").ToString
                cevent.start = Date.Parse(dtEventos.Rows(i).Item("fechaInicio_apr").ToString)
                cevent.end = Date.Parse(dtEventos.Rows(i).Item("fechaFin_apr").ToString)
                cevent.background = dtEventos.Rows(i).Item("color_pro").ToString
                cevent.color = dtEventos.Rows(i).Item("color_pro").ToString
                events.Add(cevent)
            Next

            dtEventos.Dispose()
            obj = Nothing

            Return events
        Catch ex As Exception
            obj.CerrarConexion()
            Return Nothing
        End Try
    End Function

    Public Sub ActualizaDatos(ByVal Codigo As Integer, ByVal Dias As Integer)
        Dim obj As New ClsConectarDatos
        Dim dtActividad As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        Try
            'Capturamos las fechas de la actividad
            obj.AbrirConexion()
            dtActividad = obj.TraerDataTable("PLAN_BuscaActividadProyecto", Codigo, 0, "")
            obj.CerrarConexion()

            If (dtActividad.Rows.Count > 0) Then
                'Calculamos el Numero de Días
                Dim Date1 As Date = Date.Parse(dtActividad.Rows(0).Item("fechaFin_apr"))
                Dim Date2 As Date = Date.Parse(dtActividad.Rows(0).Item("fechaInicio_apr"))
                Dim NewDate As Date = Date.Parse(dtActividad.Rows(0).Item("fechaInicio_apr")).AddDays(Dias)
                Dim numberOfDays As Integer
                numberOfDays = DateDiff(DateInterval.Day, Date2, Date1, Microsoft.VisualBasic.FirstDayOfWeek.Friday, FirstWeekOfYear.System)

                'Asignamos Nueva Fecha
                Dim FechaInicio As Date = NewDate
                Dim FechaFin As Date = NewDate.AddDays(numberOfDays)

                'ActualizamosFecha
                obj.AbrirConexion()
                obj.Ejecutar("PLAN_ActualizaFechaActividad", Codigo, FechaInicio, FechaFin)
                obj.CerrarConexion()                
                obj = Nothing
            End If
        Catch ex As Exception
            obj.CerrarConexion()
        End Try
    End Sub
End Class
