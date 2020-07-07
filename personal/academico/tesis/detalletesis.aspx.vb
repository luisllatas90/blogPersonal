﻿
Partial Class personal_academico_tesis_detalletesis
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
        Dim Tbl As New Data.DataTable
        Dim codigo_tes As Integer
        codigo_tes = Request.QueryString("codigo_tes")

        Tbl = obj.TraerDataTable("TES_ConsultarTesis", 1, codigo_tes, 0, 0)

        If Tbl.Rows.Count > 0 Then
            Me.lblcodigoReg.Text = Tbl.Rows(0).Item("codigoreg_tes")
            Me.lblFase.Text = Tbl.Rows(0).Item("nombre_Eti")
            Me.lblTitulo.Text = Tbl.Rows(0).Item("Titulo_Tes")
            Me.lblProblema.Text = Tbl.Rows(0).Item("Problema_Tes")
            Me.lblResumen.Text = Tbl.Rows(0).Item("Resumen_Tes")
            Me.lblFechaInicio.Text = Tbl.Rows(0).Item("fechainicio_Tes")
            Me.lblFechaFin.Text = Tbl.Rows(0).Item("fechafin_Tes")
            'Me.lblEstado.Text = Tbl.Rows(0).Item("descripcion_Ein")
            Me.lblRegistrado.Text = Tbl.Rows(0).Item("OpRegistro") & "&nbsp;&nbsp;" & Tbl.Rows(0).Item("FechaReg_Tes")

            '*******************************************
            'CARGAR AUTORES DE LA TESIS
            '*******************************************
            If IsDBNull(Tbl.Rows(0).Item("codigo_Rtes")) = False Then
                Me.dlAutores.DataSource = Tbl
                Me.dlAutores.DataBind()
            End If

            '*******************************************
            'CARGAR ASESORES
            '*******************************************
            Me.dlAsesores.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 3, codigo_tes, 1, 1)
            Me.dlAsesores.DataBind()

            '*******************************************
            'CARGAR LINEAS DE INVESTIGACIÓN SEGUN AUTOR
            '*******************************************
            Me.lstLineas.DataSource = obj.TraerDataTable("TES_ConsultarAreaInvestigacionTesis", 1, codigo_tes, 0, 0)
            Me.lstLineas.DataBind()

            If Request.QueryString("regresar") = "S" Then
                '*******************************************
                'CARGAR LOS ESTADOS
                '*******************************************                
                Me.gvListaArchivos.DataSource = obj.TraerDataTable("TES_ConsultarAsesoríasTesis", codigo_tes, 1)
                Me.gvListaArchivos.DataBind()
            End If

            'Por  mvillavicencio 23/11/2011
            '*******************************************
            'CARGAR ARCHIVOS 
            '*******************************************
            Me.dlAsesores.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 3, codigo_tes, 1, 1)
            Me.dlAsesores.DataBind()

        End If
        obj = Nothing
        If Request.QueryString("regresar") = "S" Then
            Me.CmdCancelar.Visible = True
            Me.CmdEstados.Visible = True
            Me.lblTituloPagina.Visible = True
            Me.lblEtapas.Visible = True
        End If
    End Sub
    Protected Sub dlAutores_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlAutores.ItemDataBound
        Dim ruta As String
        Dim img As Image
        Dim obEnc As Object
        obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

        ruta = obEnc.CodificaWeb("069" & CType(e.Item.FindControl("lblcodigo"), Label).Text)

        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------

        ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
        img = e.Item.FindControl("FotoAlumno")
        img.ImageUrl = ruta

        obEnc = Nothing
    End Sub

    Protected Sub CmdCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdCancelar.Click
        'If Request.QueryString("pagina") = "vsttesisescuela.aspx" Then
        '    Response.Redirect("vsttesisescuela.aspx?id=" & Session("codigo_usu2"))
        'Else
        '    Response.Redirect("lsttesisasesoria.aspx?id=" & Session("codigo_usu2"))
        'End If
    End Sub


    Protected Sub gvListaArchivos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaArchivos.RowDataBound        
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem

            If fila.Row("Archivo") <> "" Then
                'archivoscv/tesis/
                e.Row.Cells(2).Text = "<a href='..\..\..\archivoscv/tesis/" & fila.Row("Archivo") & "'><img src='../../../images/mover.gif' border=0 /><a/>"
            Else
                e.Row.Cells(2).Text = "No existe archivo."
            End If
        End If
    End Sub
End Class
