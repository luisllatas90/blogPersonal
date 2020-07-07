﻿'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class historialcc
    Inherits System.Web.UI.Page
    Dim crd As Integer = 0
    Dim notacrd As Double = 0
    Dim crdAR As Int16 = 0
    Dim crdAC As Int16 = 0
    Dim AAR As Int16 = 0
    Dim AAC As Int16 = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("codigo_per") Is Nothing) Then
            Response.Redirect("../ErrorSistema.aspx")
        End If

        If IsPostBack = False Then

            Dim Tbl As New Data.DataTable
            Dim codigo_alu As Integer

            codigo_alu = Request.QueryString("id")
            'codigo_alu = Session("codigo_alu")
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            Tbl = obj.TraerDataTable("consultaracceso", "C", codigo_alu, 0)
            If Tbl.Rows.Count > 0 Then
                Me.lblcodigo.Text = Tbl.Rows(0).Item("codigouniver_alu")
                Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")

                Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
                Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")

                If Request.QueryString("m") = "s" And Tbl.Rows(0).Item("estadodeuda_alu") = 1 Then
                    Me.lblMensaje.Text = "No puede visualizar su historial académico por deudas pendientes"
                    Me.grwHistorial.Visible = False
                    Me.cmdExportar.Visible = False
                Else
                    Me.grwHistorial.Visible = True
                    Me.cmdExportar.Visible = True
                    'Cargar la Foto
                    Dim ruta As String
                    Dim obEnc As Object
                    obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

                    ruta = obEnc.CodificaWeb("069" & Tbl.Rows(0).Item("codigouniver_alu").ToString)
                    'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
                    ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
                    Me.FotoAlumno.ImageUrl = ruta
                    obEnc = Nothing
                    'Cargar escuelas que llevó
                    ClsFunciones.LlenarListas(Me.cboEscuela, obj.TraerDataTable("ConsultarAlumno", 24, codigo_alu), "codigo_cpf", "nombre_cpf", "--Todas las Escuelas que se matriculó--")
                    Me.cboEscuela.SelectedValue = Tbl.Rows(0).Item("codigo_cpf")

                    'Cargar el historial de la Escuela Actual
                    Me.grwHistorial.DataSource = obj.TraerDataTable("ConsultarNotas", 18, Me.cboEscuela.SelectedValue, Request.QueryString("id"), 0)
                    'Response.Write("cbo= " & Me.cboEscuela.SelectedValue & " id= " & Request.QueryString("id"))

                    Me.grwHistorial.DataBind()
                    MostrarResultados(Me.grwHistorial.Rows.Count)
                End If
            Else
                Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
                Me.FotoAlumno.Visible = False
            End If
            Tbl.Dispose()
            obj.CerrarConexion()
            obj = Nothing
        End If
    End Sub
    Private Function PintarNota(ByVal minima As Double, ByVal nota As Double) As String
        If nota >= minima Then
            PintarNota = "<span class='azul'>" & nota & "</span>"
        Else
            PintarNota = "<span class='rojo'>" & nota & "</span>"
        End If
    End Function
    Protected Sub VerificarMatricula(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView = e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(9).Attributes.Add("onclick", "AbrirPopUp('../../personal/academico/estudiante/frmdetalles.asp?codigo_dma=" & fila.Item("codigo_dma").ToString & "&codigouniver_alu=" & Me.lblcodigo.Text & "','450','650')")

            '*******************************************
            'Asignar leyenda según el tipo de Matrícula
            '*******************************************
            Select Case fila.Item("tipomatricula_dma")
                Case "C" : e.Row.Cells(3).Text = fila.Item("nombre_cur") & "*"
                Case "U" : e.Row.Cells(3).Text = fila.Item("nombre_cur") & "**"
                Case "S" : e.Row.Cells(3).Text = fila.Item("nombre_cur") & "***"
            End Select

            '*******************************************
            'No mostrar veces desaprobadas =0
            '*******************************************
            If iif(fila.Item("vecesCurso_DmaUlt") Is dbnull.value, 0, fila.Item("vecesCurso_DmaUlt")) = 0 Then
                e.Row.Cells(8).Text = ""
            End If

            '*******************************************
            'No mostrar notas cuando no se ha llenado el registro
            '*******************************************
            If fila.Item("estadonota_cup") = "P" And fila.Item("estado_dma") <> "R" Then
                e.Row.Cells(6).Text = "-"
                e.Row.Cells(7).Text = "P"
                e.Row.CssClass = "P" 'pintar con el color
            Else
                e.Row.CssClass = fila.Item("condicion_dma") 'pintar con el color
            End If
            If fila.Item("estado_dma") = "R" Then
                e.Row.Cells(2).Text = e.Row.Cells(3).Text & "(Retirado)"
            End If

            
            '*******************************************
            'Almacenar valores Matriculados+Aprobados
            '*******************************************
            If fila.Item("condicion_dma") = "A" And fila.Item("codigo_Pes") <> 593 Then
                If fila.Item("tipomatricula_dma") <> "C" And fila.Item("estado_dma") <> "R" Then
                    crdAR += CDbl(fila.Item("creditocur_dma"))
                    AAR = AAR + 1
                ElseIf fila.Item("tipomatricula_dma") = "C" And fila.Item("estado_dma") <> "R" Then
                    crdAC += CDbl(fila.Item("creditocur_dma"))
                    AAC = AAC + 1
                End If

            End If

			'*******************************************
			'Almacenar valores sin tener en cuenta convalidaciones ni retiros
			'*******************************************
            If fila.Item("estado_dma") <> "R" And (fila.Item("condicion_dma") = "A" Or fila.Item("condicion_dma") = "D" And fila.Item("codigo_Pes") <> 593) Then
                crd += CDbl(fila.Item("creditocur_dma"))
                notacrd += CDbl(fila.Item("notacredito"))
            End If
            'ElseIf e.Row.RowType = DataControlRowType.Footer Then
            '    e.Row.Cells(3).Text = "TOTAL DE APROBADOS"
            '    e.Row.Cells(4).Text = crd
            '    If crd > 0 Then
            '        e.Row.Cells(6).Text = FormatNumber(notacrd / crd, 2)
            '    End If

            '    e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            '    e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center
            '    crd = 0
            '    notacrd = 0
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Me.lblAsigAprob.Text = AAR.ToString
        Me.lblAsigAprobC.Text = AAC.ToString
        Me.lblCrdAprob.Text = crdAR.ToString
        Me.lblCrdAprobC.Text = crdAC.ToString
        Me.lblCrd.Text = Int(Me.lblCrdAprob.Text) + Int(Me.lblCrdAprobC.Text)
        Me.lblAsig.Text = Int(Me.lblAsigAprob.Text) + Int(Me.lblAsigAprobC.Text)
        If crd > 0 Then
            Me.lblPond.Text = FormatNumber(notacrd / crd, 2)
        End If

    End Sub
    Protected Sub cboEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEscuela.SelectedIndexChanged
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        'Cargar Record Académico
        Me.grwHistorial.DataSource = obj.TraerDataTable("ConsultarNotas", 18, Me.cboEscuela.SelectedValue, Request.QueryString("id"), 0)
        Me.grwHistorial.DataBind()
        obj.CerrarConexion()
        obj = Nothing
        MostrarResultados(Me.grwHistorial.Rows.Count)
    End Sub
    Private Sub MostrarResultados(ByVal condicion As Int16)
        Me.Panel1.Visible = condicion > 0
        Me.cmdExportar.Visible = condicion > 0
    End Sub
    Protected Sub cmdExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
        Me.grwHistorial.EnableViewState = False
        Page.EnableEventValidation = False
        Page.DesignerInitialize()
        Page.Controls.Add(form)
        form.Controls.Add(Me.grwHistorial)
        Page.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=HistorialCursosMatriculados.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class