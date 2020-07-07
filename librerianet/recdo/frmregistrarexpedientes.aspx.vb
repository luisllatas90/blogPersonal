
Partial Class recdo_frmregistrarexpedientes
    Inherits System.Web.UI.Page
    Private tblTmpExp As Data.DataTable
    Private tblTmpExpView As Data.DataView
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("tblTmpExpData") Is Nothing Then
            'Movimientos
            tblTmpExp = New Data.DataTable()
            tblTmpExp.Columns.Add(New Data.DataColumn("idmovimiento", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("fechamovimiento", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("horamovimiento", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("idareaarchivo", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("idareaarchivo2", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("areaorigen", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("areadestino", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("numcargo", GetType(String)))
            tblTmpExp.Columns.Add(New Data.DataColumn("motivo", GetType(String)))

            Session("tblTmpExpData") = tblTmpExp
        Else
            tblTmpExp = Session("tblTmpExpData")
        End If

        tblTmpExpView = New Data.DataView(tblTmpExp)

        If IsPostBack = False Then
            Dim anio As String = 7 'Request.QueryString("idanio") 
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString
            obj.AbrirConexion()
            ClsFunciones.LlenarListas(lstExpedientes, obj.TraerDataTable("ConsultarExpedienteArea", 0, 230, anio), "idarchivo", "NumeroExpediente")
            ClsFunciones.LlenarListas(dpTipo, obj.TraerDataTable("ConsultarExpedienteArea", 1, 230, anio), "idtipoarchivo", "descripciontipoarchivo")
            ClsFunciones.LlenarListas(dpprocedencia, obj.TraerDataTable("ConsultarExpedienteArea", 2, 230, anio), "idprocedencia", "nombreprocedencia")
            ClsFunciones.LlenarListas(dpdirigido, obj.TraerDataTable("ConsultarExpedienteArea", 3, 230, anio), "idareaarchivo", "nombreareaarchivo")
            obj.CerrarConexion()
            obj = Nothing

            'Tipo NUEVO
            dptipo.selectedvalue = 1
            dpdirigido.selectedvalue = 60
            Me.txtfecha.text = Date.Now.Date
            Me.txthora.text = Date.Now.ToString("hh:mm")

            Me.CargarMovimientosTemporal()
        End If
    End Sub
    Private Sub CargarDatos(ByVal idarchivo As String)
        Dim obj As New ClsConectarDatos
        Dim tbl As data.datatable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString
        obj.AbrirConexion()
        tbl = obj.TraerDataTable("ConsultarArhivoDocumentario", 4, idarchivo, 0, 0)

        If tbl.rows.count > 0 Then
            'Limpiar Datos
            Me.LimpiarTablasTemporales()
            'Mostrar Datos
            Me.txtnumeroexpediente.text = tbl.rows(0).item("numeroexpediente")
            Me.dptipo.selectedvalue = tbl.rows(0).item("IdTipoarchivo")
            Me.txtnumeroTipo.text = tbl.rows(0).item("NumeroTipo")
            Me.txtFecha.text = tbl.rows(0).item("Fechaarchivo")
            Me.txtHora.text = tbl.rows(0).item("Horaarchivo")
            Me.dpprocedencia.selectedvalue = tbl.rows(0).item("idProcedencia")
            'idDestinatario = tbl.rows(0).item("idDestinatario")
            Me.txtAsunto.text = tbl.rows(0).item("asunto")
            Me.txtObs.text = tbl.rows(0).item("obs")

            'Mostrar movimientos
            Dim tblmov As data.datatable
            tblmov = obj.TraerDataTable("ConsultarArhivoDocumentario", 3, idarchivo, 0, 0)

            If tblmov.rows.count > 0 Then
                For i As int16 = 0 To tblmov.rows.count - 1
                    AgregarMovimiento(tblmov.rows(i).item("idmovimiento"), tblmov.rows(i).item("fechamovimiento"), tblmov.rows(i).item("horamovimiento"), tblmov.rows(i).item("idareaarchivo"), tblmov.rows(i).item("idareaarchivo2"), tblmov.rows(i).item("areaorigen"), tblmov.rows(i).item("areadestino"), tblmov.rows(i).item("numcargo"), tblmov.rows(i).item("motivo"))
                Next
            End If
            tblmov.dispose()
            Me.CargarMovimientosTemporal()
        End If
        tbl.dispose()
        obj.CerrarConexion()
        obj = Nothing
    End Sub
    Private Sub CargarMovimientosTemporal()
        Me.grwMovimientos.DataSource = tblTmpExpView
        Me.grwMovimientos.DataBind()
    End Sub
    Private Sub AgregarMovimiento(ByVal idmovimiento As Int32, ByVal fechamovimiento As String, ByVal horamovimiento As String, ByVal idareaarchivo As int32, ByVal idareaarchivo2 As int32, ByVal areaorigen As String, ByVal areadestino As String, ByVal numcargo As String, ByVal motivo As String)
        Dim fila As Data.DataRow

        fila = tblTmpExp.NewRow()

        fila("idmovimiento") = idmovimiento
        fila("fechamovimiento") = fechamovimiento
        fila("horamovimiento") = horamovimiento
        fila("idareaarchivo") = idareaarchivo
        fila("idareaarchivo2") = idareaarchivo2
        fila("areaorigen") = areaorigen
        fila("areadestino") = areadestino
        fila("numcargo") = numcargo
        fila("motivo") = motivo

        'Añadir fila
        tblTmpExp.Rows.Add(fila)
    End Sub
    Private Sub LimpiarTablasTemporales()
        Session("tblTmpExpData") = Nothing
        tblTmpExp.Dispose()

        'Limpiar controles
        Me.txtnumeroexpediente.text = ""
        'Me.dptipo.selectedvalue = ""
        Me.txtnumeroTipo.text = ""
        Me.txtfecha.text = Date.Now.Date
        Me.txthora.text = Date.Now.ToString("hh:mm")
        'Me.dpprocedencia.selectedvalue = ""
        'idDestinatario = tbl.rows(0).item("idDestinatario")
        Me.txtAsunto.text = ""
        Me.txtObs.text = ""
        Me.grwMovimientos.DataBind()
        Me.hdIdArchivo.value = 0
    End Sub
    Protected Sub imgAgregar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAgregar.Click
        Dim idareaarchivo, areaorigen As String

        idareaarchivo = 98 'Mesa de partes
        areaorigen = "MESA DE PARTES"

        AgregarMovimiento(0, Date.Now.Date, Date.Now.ToString("hh:mm"), idareaarchivo, dpdirigido.selectedvalue, areaorigen, dpdirigido.selecteditem.text, Me.txtcargo.text.tostring.trim, Me.txtobscargo.text.tostring.trim)
        Me.CargarMovimientosTemporal()
        'Me.dpDirigido.selectedvalue = 0
        Me.txtcargo.text = ""
        Me.txtobscargo.text = ""
    End Sub
    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim anio As String = Request.QueryString("idanio")
        Dim idusuario As String = Request.QueryString("idusuario")
        Dim idarchivo As String

        Dim obj As New ClsConectarDatos

        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString
        Try
            obj.IniciarTransaccion()
            'Grabando datos del expediente
            If Me.hdIdArchivo.value = 0 Then
                Dim valoresdevueltos(1) As Integer
                obj.Ejecutar("Agregararchivo", anio, CDate(Me.txtFecha.text), Me.txtHora.text, Me.txtnumeroexpediente.text.trim, Me.txtnumerotipo.text.trim, Me.dptipo.selectedvalue, Me.dpprocedencia.selectedvalue, 230, Me.txtasunto.text.trim, Me.txtobs.text.trim, idusuario, 0, 0).copyto(valoresdevueltos, 0)
                idarchivo = valoresdevueltos(0)
            Else
                idarchivo = Me.hdIdArchivo.value
                obj.Ejecutar("modificararchivo", idarchivo, anio, CDate(Me.txtFecha.text), Me.txtHora.text, Me.txtnumeroexpediente.text.trim, Me.txtnumerotipo.text.trim, Me.dptipo.selectedvalue, Me.dpprocedencia.selectedvalue, 230, Me.txtasunto.text.trim, Me.txtobs.text.trim, 0, idusuario)
            End If
            'Grabar sus movimientos
            If idarchivo = 0 Then
                obj.AbortarTransaccion()
                Page.RegisterStartupScript("error", "<script>alert('Ha ocurrido un error al grabar\n Intente denuevo o contáctese con desarrollosistemas@usat.edu.pe')</script>")
                Exit Sub
            End If

            For i As int16 = 0 To tblTmpExp.rows.count - 1
                obj.Ejecutar("Agregarmovimientoarchivo", Date.Now.Date, tblTmpExp.rows(i).item("horamovimiento"), idarchivo, tblTmpExp.rows(i).item("idareaarchivo"), tblTmpExp.rows(i).item("idareaarchivo2"), "", Me.txtcargo.text.trim, Me.txtobscargo.text.trim, idusuario, "PC", 0)
            Next

            'Cargar lista expedientes
            ClsFunciones.LlenarListas(lstExpedientes, obj.TraerDataTable("ConsultarExpedienteArea", 0, 230, anio), "idarchivo", "NumeroExpediente")

            obj.TerminarTransaccion()
            obj = Nothing
            LimpiarTablasTemporales()
            Page.RegisterStartupScript("error", "<script>alert('Los datos se han guardado correctamente')</script>")
        Catch ex As Exception
            Me.LimpiarTablasTemporales()
            obj.AbortarTransaccion()
            Page.RegisterStartupScript("error", "<script>alert('Ha ocurrido un error al grabar\n Intente denuevo o contáctese con desarrollosistemas@usat.edu.pe')</script>")
            obj = Nothing
        End Try
    End Sub
    Protected Sub grwMovimientos_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwMovimientos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(4).Attributes.Add("onclick", "return confirm('¿Esta seguro que desea quitar el movimiento?');")
        End If
    End Sub
    Protected Sub grwMovimientos_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grwMovimientos.RowDeleting
        If grwMovimientos.DataKeys(e.RowIndex).Value <> 0 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxCMUSAT").ConnectionString
            obj.AbrirConexion()
            obj.Ejecutar("Eliminarmovimientoarchivo", grwMovimientos.DataKeys(e.RowIndex).Value)
            obj.TerminarTransaccion()
            obj = Nothing
            Me.tblTmpExp.Rows.RemoveAt(e.RowIndex)
        Else
            'Eliminar fila temporal
            Me.tblTmpExp.Rows.RemoveAt(e.RowIndex)
        End If
        Me.CargarMovimientosTemporal()
        e.Cancel = True
    End Sub
    Protected Sub imgBuscar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBuscar.Click
        Me.hdIdArchivo.value = Me.lstExpedientes.selectedvalue
        Me.CargarDatos(Me.lstExpedientes.selectedvalue)
    End Sub
End Class
