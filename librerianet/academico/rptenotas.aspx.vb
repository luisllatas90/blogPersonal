﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class librerianet_academico_rptenotas
    Inherits System.Web.UI.Page
    Dim Creditos As Int16
    Dim Cursos As Int16
    Dim Promedio As Double
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub cmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        'If IsPostBack = False Then
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable

        Tbl = obj.TraerDataTable("consultaracceso", "E", Me.txtcodigo.Text.Trim, 0)
        Me.lblMensaje.Visible = False
        If Tbl.Rows.Count > 0 Then
            Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")
            Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
            Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
            Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")

            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            ruta = obEnc.CodificaWeb("069" & Me.txtcodigo.Text.Trim)
            'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
            ruta = "..\..\imgestudiantes\" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing

            'Cargar Record Académico si no tiene deuda o según la función
            If Tbl.Rows(0).Item("estadodeuda_alu").ToString = "1" And _
                Request.QueryString("ctf") <> 1 And Request.QueryString("ctf") <> 25 And _
                Request.QueryString("ctf") <> 9 And Request.QueryString("ctf") <> 26 And _
                Request.QueryString("ctf") <> 30 And Request.QueryString("ctf") <> 35 And _
                Request.QueryString("ctf") <> 16 And Request.QueryString("ctf") <> 7 Then

                Me.lblMensaje.Text = "Lo sentimos no se puede mostrar su historial académico del Estudiante, por favor <br>comuníquese con la Oficina de Pensiones para que se le indique el motivo."
            Else
                Me.GridView1.DataSource = obj.TraerDataTable("GenerarCertificadoEstudios", 10, Tbl.Rows(0).Item("codigo_alu").ToString, Tbl.Rows(0).Item("codigo_pes").ToString)
                Me.GridView1.DataBind()

                Me.cmdExportar.Visible = True
		Me.FotoAlumno.Visible=true
            End If
        Else
            Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
	    Me.FotoAlumno.Visible=false
        End If
        Tbl.Dispose()
        obj = Nothing
        'End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            'e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            Creditos += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "creditocur_dma"))
            Cursos = e.Row.RowIndex + 1
            Promedio += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "notacredito"))
        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            'e.Row.Cells(1).Text = "TOTAL:"
            e.Row.Cells(2).Text = "Total de asignaturas: " & Cursos.ToString
            e.Row.Cells(3).Text = Creditos.ToString
            e.Row.Cells(4).Text = FormatNumber(Promedio / Creditos, 2)
            e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Right
        End If

    End Sub

 
End Class
