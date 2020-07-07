Imports System.Web.UI.WebControls
Imports System.Globalization
Imports System.Data

Partial Class investigacion_frmRegistrarInvestigaciones
    Inherits System.Web.UI.Page

    Dim operacion As String
    Dim id_investigacion As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-PE")
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
            System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","

            operacion = Request.QueryString("tipo")
            'Para el caso del los nuevos registros.
            If (Request.QueryString("tipo") = "M" Or Request.QueryString("tipo") = "V" Or Request.QueryString("tipo") = "MV") Then
                id_investigacion = Request.QueryString("idINV")
                ddlcomites.Enabled = False
                ddlAreas.Enabled = False
                ddlLineaInv.Enabled = False
            Else
                id_investigacion = 0
            End If

            If Not IsPostBack Then                
                Session.Remove("dtResponsables")
                CargaInstanciaInvestigacion()
                CargaEtapaInvestigacion()  'Problema
                CargaTipoInvestigacion()  'problema
                CargarTipoParticipacion() 'problema
                CargarListaComite()

                'P= Personal / A=Alumno
                If chkTipoPersonal.Enabled Then
                    CargarTipoPersonal("P")
                    ddlTipoPersonal.SelectedValue = Request.QueryString("id")
                Else
                    CargarTipoPersonal("A")
                End If

                txtPresupuesto.Attributes.Add("onKeyPress", "validarnumero()")
                txtTitulo.Focus()

                'Procedimiento para agregar los responsables ------------------------------------
                If Request.QueryString("tipo") = "N" Then
                    CreaDatatableResponsables()
                Else
                    Limpiar_dtResponsables()        '<-----------aqui salia la referencia
                    CreaDatatableResponsables()
                End If
                '--------------------------------------------------------------------------------


                'Tipo M=Modificar - N=Nuevo  - V=Version
                Select Case Request.QueryString("tipo")
                    Case "M"
                        MostrarRegistro(id_investigacion)
                        MostrarListaResponsable(id_investigacion)
                    Case "N"
                        ddlEtapaInv.SelectedValue = 1           'Borrador
                        ddlInstanciaInv.SelectedValue = 1       'Autor
                    Case "V"
                        MostrarRegistro(id_investigacion)
                        MostrarListaResponsable(id_investigacion)
                    Case "MV" 'Modificar una version
                        MostrarRegistro(id_investigacion)
                        MostrarListaResponsable(id_investigacion)
                End Select
                EstdosControlesConstantes(False)
                EstdosControlesDinamicos(False)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub EstdosControlesDinamicos(ByVal vEstado As Boolean)
        Try
            ddlAreas.Enabled = vEstado
            ddlLineaInv.Enabled = vEstado
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub


    Private Sub EstdosControlesConstantes(ByVal vEstado As Boolean)
        Try
            ddlEtapaInv.Enabled = vEstado
            ddlInstanciaInv.Enabled = vEstado
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub CargarListaComite()
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsInvestigacion

            dts = obj.ListaComites()
            If dts.Rows.Count > 0 Then
                ddlcomites.DataSource = dts
                ddlcomites.DataTextField = "descripcion"
                ddlcomites.DataValueField = "codigo"
                ddlcomites.DataBind()
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub MostrarListaResponsable(ByVal id_investigacion As Integer)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsInvestigacion

            dts = obj.ListaResponsableInvestigacion(id_investigacion)
            If dts.Rows.Count > 0 Then
                gvListaResponsables.DataSource = dts
                gvListaResponsables.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CreaDatatableResponsables()
        If Session("dtResponsables") Is Nothing Then
            Dim dtResponsables As New Data.DataTable
            dtResponsables.Columns.Add("codigo_per", GetType(String))
            dtResponsables.Columns.Add("nombre", GetType(String))
            dtResponsables.Columns.Add("id_participacion", GetType(String))
            dtResponsables.Columns.Add("nombre_participacion", GetType(String))
            dtResponsables.Columns.Add("tipo_per", GetType(String))
            Session("dtResponsables") = dtResponsables
        End If
    End Sub

    Private Sub MostrarRegistro(ByVal id_investigacion As Integer)
        Try
            Dim dts As New Data.DataTable
            Dim obj As New clsInvestigacion

            'El tipo depende si va a mostrar el registro de la tabla investigaciones o versiones.
            dts = obj.MostrarRegistroID(id_investigacion, Request.QueryString("tipo"))
            If dts.Rows.Count > 0 Then
                With dts.Rows(0)
                    txtTitulo.Text = .Item("titulo").ToString
                    txtBeneficiarios.Text = .Item("beneficiarios").ToString
                    txtFechaInicio.Text = .Item("fechaInicio").ToString
                    txtFechaFin.Text = .Item("fechaFin").ToString
                    txtPresupuesto.Text = .Item("presupuesto")
                    txtResumen.Text = .Item("resumen").ToString

                    ddlAmbitoInv.SelectedValue = .Item("Ambito").ToString
                    ddlEtapaInv.SelectedValue = .Item("investigacion_etapa_id")
                    ddlTipoFinanciamiento.SelectedValue = .Item("tipoFinanciamiento").ToString
                    ddlInstanciaInv.SelectedValue = .Item("investigacion_instancia_id")
                    ddlLineaInv.SelectedValue = .Item("investigacion_linea_id")
                    ddlTipoInv.SelectedValue = .Item("investigacion_tipo_id")
                    ddlAmbitoInv.SelectedValue = .Item("Ambito").ToString

                    ddlcomites.SelectedValue = .Item("investigacion_comite_id")
                    ListaAreas(.Item("investigacion_comite_id"))
                    ddlAreas.SelectedValue = .Item("investigacion_area_id")
                    ListaLinea(.Item("investigacion_area_id"))


                    '------------------------------------------------------------------------
                    'Creando la tabla Documentos
                    Dim dtDocumentos As New Data.DataTable
                    dtDocumentos.Columns.Add("extension", GetType(String))
                    dtDocumentos.Columns.Add("nombre", GetType(String))
                    dtDocumentos.Columns.Add("ruta", GetType(String))
                    dtDocumentos.Columns.Add("documento", GetType(String))

                    If .Item("rutaInforme").ToString <> "" Then
                        Dim myrow As Data.DataRow
                        myrow = dtDocumentos.NewRow
                        myrow("extension") = .Item("extInf")
                        myrow("nombre") = "Informe"
                        myrow("ruta") = .Item("rutaInforme")
                        myrow("documento") = .Item("docInf")
                        dtDocumentos.Rows.Add(myrow)
                    End If
                    If .Item("rutaProyecto").ToString <> "" Then
                        Dim myrow As Data.DataRow
                        myrow = dtDocumentos.NewRow
                        myrow("extension") = .Item("extPro")
                        myrow("nombre") = "Proyecto"
                        myrow("ruta") = .Item("rutaProyecto")
                        myrow("documento") = .Item("docPro")
                        dtDocumentos.Rows.Add(myrow)
                    End If
                    gvDocumentos.DataSource = dtDocumentos
                    gvDocumentos.DataBind()
                    '--------------------------------------------------------------------------

                End With
            Else
                MostrarAlertaUsuario()
                Exit Sub
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub QuitarResponsable(ByVal codigo_per As String)
        Try
            Dim dtResponsables As New Data.DataTable
            dtResponsables = Session("dtResponsables")
            For Each row As Data.DataRow In dtResponsables.Rows
                If row("codigo_per") = codigo_per Then
                    dtResponsables.Rows.Remove(row)
                    Session("dtResponsables") = dtResponsables
                    gvListaResponsables.DataSource = dtResponsables
                    gvListaResponsables.DataBind()
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub ibtnEliminar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ibtnEliminar As ImageButton
            Dim row As GridViewRow
            ibtnEliminar = sender
            row = ibtnEliminar.NamingContainer
            If id_investigacion = 0 Then
                'Response.Write("Elimina Ejecucion")
                QuitarResponsable(row.Cells.Item(0).Text)
            Else
                'Entra aqui cuando va a modificar, y cambia de estado a los responsables.
                'Response.Write("Elimina de BD")
                Dim obj As New clsInvestigacion
                obj.AbrirTransaccionCnx()
                obj.EliminaResponsablesInvestigacacion(row.Cells.Item(0).Text, id_investigacion, row.Cells.Item(4).Text)
                obj.CerrarTransaccionCnx()
                MostrarListaResponsable(id_investigacion)
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub MostrarAlertaUsuario()
        Try
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('No se encontraron datos, favor de verificar.');", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & ex.Message & "');", True)
        End Try
    End Sub


    Private Sub CargarTipoPersonal(ByVal vTipo As String)
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            dts = obj.ListaTipoPersonal(vTipo)
            If dts.Rows.Count > 0 Then
                ddlTipoPersonal.DataSource = dts
                ddlTipoPersonal.DataTextField = "descripcion"
                ddlTipoPersonal.DataValueField = "codigo"
                ddlTipoPersonal.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargarTipoParticipacion()
        Try

            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            dts = obj.ListaParticipantesinvestigacion
            If dts.Rows.Count > 0 Then
                ddlTipoParticipacion.DataSource = dts
                ddlTipoParticipacion.DataTextField = "descripcion"
                ddlTipoParticipacion.DataValueField = "codigo"
                ddlTipoParticipacion.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Private Sub CargaLineaInvestigacion()
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            dts = obj.ListaLineaInvestigacion
            If dts.Rows.Count > 0 Then
                ddlLineaInv.DataSource = dts
                ddlLineaInv.DataTextField = "descripcion"
                ddlLineaInv.DataValueField = "codigo"
                ddlLineaInv.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaInstanciaInvestigacion()
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            dts = obj.ListaInstancias
            If dts.Rows.Count > 0 Then
                ddlInstanciaInv.DataSource = dts
                ddlInstanciaInv.DataTextField = "descripcion"
                ddlInstanciaInv.DataValueField = "codigo"
                ddlInstanciaInv.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaEtapaInvestigacion()
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            dts = obj.ListaEtapaInvestigacion
            If dts.Rows.Count > 0 Then
                ddlEtapaInv.DataSource = dts
                ddlEtapaInv.DataTextField = "descripcion"
                ddlEtapaInv.DataValueField = "codigo"
                ddlEtapaInv.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub CargaTipoInvestigacion()
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable

            dts = obj.ListaTipoInvestigacion
            If dts.Rows.Count > 0 Then
                ddlTipoInv.DataSource = dts
                ddlTipoInv.DataTextField = "descripcion"
                ddlTipoInv.DataValueField = "codigo"
                ddlTipoInv.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Function validaciones() As Boolean
        Dim obj As New clsInvestigacion
        Dim dts As New Data.DataTable
        Dim sw As Byte = 0

        '----Bloque de validaciones---------
        If txtTitulo.Text.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar el titulo de la investigación');", True)
            txtTitulo.Focus()
            Exit Function
        End If

        If txtFechaInicio.Text.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha de inicio para el proyecto');", True)
            txtFechaInicio.Focus()
            Exit Function
        Else
            If Not IsDate(txtFechaInicio.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha válida para la fecha de inicio');", True)
                txtFechaInicio.Focus()
                Exit Function
            End If
        End If

        If txtFechaFin.Text.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha final para el proyecto');", True)
            txtFechaFin.Focus()
            Exit Function
        Else
            If Not IsDate(txtFechaFin.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar una fecha válida para la fecha final');", True)
                txtFechaFin.Focus()
                Exit Function
            End If
        End If

        If IsDate(txtFechaInicio.Text) And IsDate(txtFechaFin.Text) Then
            If CDate(txtFechaInicio.Text) >= CDate(txtFechaFin.Text) Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('La fecha inicial del proyecto no puede ser mayor o igual a la fecha final, favor de corregir');", True)
                txtFechaInicio.Focus()
                Exit Function
            End If
        End If

        If txtPresupuesto.Text.Trim = "" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar el presupuesto de la investigación');", True)
            txtPresupuesto.Focus()
            Exit Function
        End If

        If IsNumeric(txtPresupuesto.Text) = False Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de ingresar un valor correcto para el presupuesto que tendrá del proyecto de investigación);", True)
            txtPresupuesto.Focus()
            Exit Function
        End If

        If ddlTipoFinanciamiento.SelectedValue = "0" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar un tipo de financiamiento');", True)
            ddlTipoFinanciamiento.Focus()
            Exit Function
        End If

        If ddlEtapaInv.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar la etapa que tendrá el proyecto de Investigación');", True)
            ddlEtapaInv.Focus()
            Exit Function
        End If

        If ddlAmbitoInv.SelectedValue = "0" Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el ámbito que tendrá el proyecto de Investigación');", True)
            ddlAmbitoInv.Focus()
            Exit Function
        End If

        '------------Agregado xDguevara 24.10.2012
        If ddlcomites.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar un comité para el proyecto de Investigación');", True)
            ddlcomites.Focus()
            Exit Function
        End If

        If ddlAreas.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el área para el proyecto de Investigación');", True)
            ddlAreas.Focus()
            Exit Function
        End If
        '---------------------------------------------////////

        If ddlLineaInv.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar la linea que tendrá el proyecto de Investigación');", True)
            ddlLineaInv.Focus()
            Exit Function
        End If

        If ddlTipoInv.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar el tipo que tendrá el proyecto de Investigación');", True)
            ddlTipoInv.Focus()
            Exit Function
        End If

        If ddlInstanciaInv.SelectedValue = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Favor de seleccionar la instancia que tendrá el proyecto de Investigación');", True)
            ddlInstanciaInv.Focus()
            Exit Function
        End If

        If gvListaResponsables.Rows.Count = 0 Then
            ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Agregar los participantes al proyecto de investigación.');", True)
            ddlTipoPersonal.Focus()
            Exit Function
        End If

        If (Request.QueryString("tipo") = "N" Or Request.QueryString("tipo") = "V") Then
            If FileArchivo.HasFile = False Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el archivo referente al proyecto de investigación');", True)
                Exit Function
            End If
        End If

        sw = 1
        If (sw = 1) Then
            Return True
        End If
        Return False
    End Function

    Protected Sub cmdGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Try
            Select Case Request.QueryString("tipo")
                Case "N"
                    If Add() > 0 Then
                        LimpiarControles()
                        Limpiar_dtResponsables()
                        ddlEtapaInv.SelectedValue = 1           'Borrador
                        ddlInstanciaInv.SelectedValue = 1       'Autor
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos fueron registrados correctamente')", True)
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Error al registrar, favor de intentar nuevamente.')", True)
                    End If
                Case "M"
                    Upd()
                    SubirArchivo(id_investigacion, "M")
                    AgregarResponsables(id_investigacion)
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos fueron actualizados correctamente.')", True)
                    MostrarRegistro(id_investigacion)
                    MostrarListaResponsable(id_investigacion)
                Case "V"    'Versionar
                    Upd()
                    SubirArchivo(id_investigacion, "V")
                    AgregarResponsables(id_investigacion)
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos fueron guardados correctamente.')", True)
                    MostrarRegistro(id_investigacion)
                    MostrarListaResponsable(id_investigacion)
                Case "MV"   'Modicar una version
                    Upd()
                    SubirArchivo(id_investigacion, "MV")
                    AgregarResponsables(id_investigacion)
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Los datos fueron guardados correctamente.')", True)
                    MostrarRegistro(id_investigacion)
                    MostrarListaResponsable(id_investigacion)

            End Select
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub Upd()
        Dim obj As New clsInvestigacion
        If validaciones() = False Then
            Exit Sub
        End If

        obj.AbrirTransaccionCnx()
        'M -> Modifica  - V-> Version
        obj.ModificarInvestigaciones(Request.QueryString("tipo"), _
                                     id_investigacion, _
                                     Me.txtTitulo.Text.Trim, _
                                     CType(Me.txtFechaInicio.Text.Trim, Date), _
                                     CType(txtFechaFin.Text.Trim, Date), _
                                     txtPresupuesto.Text, _
                                     Me.ddlTipoFinanciamiento.SelectedValue, _
                                     Me.txtBeneficiarios.Text.Trim, _
                                     Me.txtResumen.Text.Trim, _
                                     Me.ddlAmbitoInv.SelectedValue, _
                                     Me.ddlTipoInv.SelectedValue, _
                                     Request.QueryString("id"))
        obj.CerrarTransaccionCnx()
        obj = Nothing
    End Sub

    Function Add() As Integer
        Try
            Dim obj As New clsInvestigacion
            Dim exec As Integer
            Dim ruta As String = ""

            obj.AbrirTransaccionCnx()
            If validaciones() = False Then
                Exit Function
            End If

            exec = obj.InsertarInvestigaciones(Me.txtTitulo.Text.Trim, _
                                                  CType(Me.txtFechaInicio.Text.Trim, Date), _
                                                  CType(txtFechaFin.Text.Trim, Date), _
                                                  txtPresupuesto.Text, _
                                                  Me.ddlTipoFinanciamiento.SelectedValue, _
                                                  Me.txtBeneficiarios.Text.Trim, _
                                                  Me.txtResumen.Text.Trim, _
                                                  Me.ddlAmbitoInv.SelectedValue, _
                                                  Me.ddlLineaInv.SelectedValue, _
                                                  Me.ddlEtapaInv.SelectedValue, _
                                                  Me.ddlTipoInv.SelectedValue, _
                                                  Me.ddlInstanciaInv.SelectedValue, _
                                                  Request.QueryString("id"), _
                                                  0)
            obj.CerrarTransaccionCnx()
            obj = Nothing
            SubirArchivo(exec, "N")

            '--/ Insertamos todos los responsables de la lista
            '--/ Si la operacion fuera actualiza, el sp validad a los responsables para no duplicar.
            AgregarResponsables(exec)
            Return exec
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
            Return -1
        End Try        
    End Function


    Private Sub SubirArchivo(ByVal investigaciones_id As Integer, ByVal tipo As String)
        Try
            '--/ Despues que inserto correctamente, sube el archivo con el ID de de la investigacion
            If FileArchivo.HasFile Then
                Dim filePath As String
                Dim archivo As String = "\" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(FileArchivo.FileName).ToString
                Dim archivoBD As String = "/" & Now.Day.ToString & Now.Month.ToString & Now.Year.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & System.IO.Path.GetExtension(FileArchivo.FileName).ToString

                filePath = Server.MapPath("../../filesInvestigacion")
                filePath = filePath & "\" & investigaciones_id

                Dim carpeta As New System.IO.DirectoryInfo(filePath)
                If carpeta.Exists = False Then
                    carpeta.Create()
                End If
                FileArchivo.PostedFile.SaveAs(filePath & archivo)
                'Dim ruta As String = filePath & archivo
                Dim ruta As String = archivoBD
                '--/ Actualiza la ruta del archivo
                AgregarDocumento(investigaciones_id, ruta, tipo)
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub AgregarResponsables(ByVal investigacion_id As Integer)
        Try
            Dim obj As New clsInvestigacion
            Dim id As Integer
            id = Request.QueryString("id")
            obj.AbrirTransaccionCnx()
            Dim dt As New Data.DataTable
            dt = Session("dtResponsables")
            For Each row As Data.DataRow In dt.Rows
                If row("tipo_per") = "P" Then
                    obj.AgregaResponsablesInvestigacion(row("codigo_per"), 0, investigacion_id, row("id_participacion"), id, "P")
                Else
                    obj.AgregaResponsablesInvestigacion(0, row("codigo_per"), investigacion_id, row("id_participacion"), id, "A")
                End If
            Next
            obj.CerrarTransaccionCnx()
            obj = Nothing

            'Limpiar_dtResponsables()
            'ClientScript.RegisterStartupScript(Me.GetType, "Alerta", "alert('Se guardaron los datos correctamente.');", True)
            'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmGestionComite.aspx?id=" & Request.QueryString("id") & "';", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub


   


    Private Sub LimpiarControles()
        Try
            txtBeneficiarios.Text = ""
            txtFechaFin.Text = ""
            txtFechaInicio.Text = ""
            txtPresupuesto.Text = ""
            txtResumen.Text = ""
            txtTitulo.Text = ""

            ddlAmbitoInv.SelectedValue = "0"
            ddlTipoFinanciamiento.SelectedValue = "0"

            ddlTipoParticipacion.SelectedValue = 0
            ddlLineaInv.SelectedValue = 0
            ddlEtapaInv.SelectedValue = 0
            ddlInstanciaInv.SelectedValue = 0
            ddlTipoPersonal.SelectedValue = 0


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub Limpiar_dtResponsables()
        Try
            Dim dt As New Data.DataTable
            dt = Session("dtResponsables")
            dt.Rows.Clear()
            Session("dtResponsables") = dt
            gvListaResponsables.DataSource = dt
            gvListaResponsables.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub AgregarDocumento(ByVal id_investigacion As Integer, ByVal ruta As String, ByVal tipo As String)
        Try
            Dim obj As New clsInvestigacion
            obj.AbrirTransaccionCnx()
            obj.AgregaRutaArchivoInvestigacion(id_investigacion, ruta, tipo)
            obj.CerrarTransaccionCnx()
            obj = Nothing
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub chkTipoPersonal_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTipoPersonal.CheckedChanged
        Try
            If chkTipoPersonal.Checked = True Then
                chkTipoPersonal.Text = "Personal"
                chkTipoPersonal.ForeColor = Drawing.Color.Blue
                CargarTipoPersonal("P")
            Else
                chkTipoPersonal.Text = "Alumno"
                chkTipoPersonal.ForeColor = Drawing.Color.Green
                CargarTipoPersonal("A")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Try
            Dim dtResponsables As New Data.DataTable
            dtResponsables = Session("dtResponsables")

            If ddlTipoParticipacion.SelectedValue = 0 Then
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione el tipo de participación.')", True)
                ddlTipoParticipacion.Focus()
                Exit Sub
            End If

            'Validacion
            For Each row As Data.DataRow In dtResponsables.Rows
                If row("codigo_per") = ddlTipoPersonal.SelectedValue Then
                    Dim mensaje As String = "El responsable seleccionado " + ddlTipoPersonal.SelectedItem.ToString + " ya se encuentra en lista."
                    ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('" & mensaje & "')", True)
                    Exit Sub
                End If
            Next

            Dim myrow As Data.DataRow
            myrow = dtResponsables.NewRow
            myrow("codigo_per") = ddlTipoPersonal.SelectedValue
            myrow("nombre") = ddlTipoPersonal.SelectedItem.ToString
            myrow("id_participacion") = ddlTipoParticipacion.SelectedValue
            myrow("nombre_participacion") = ddlTipoParticipacion.SelectedItem.ToString
            If chkTipoPersonal.Checked = True Then
                myrow("tipo_per") = "P"
            Else
                myrow("tipo_per") = "A"
            End If
            dtResponsables.Rows.Add(myrow)
            gvListaResponsables.DataSource = dtResponsables
            gvListaResponsables.DataBind()
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Protected Sub gvListaResponsables_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvListaResponsables.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
                e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            End If
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType, "Error", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub ListaLinea(ByVal area_id As Integer)
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable
            dts = obj.ListaLineasPorArea(area_id)
            If dts.Rows.Count > 0 Then
                ddlLineaInv.Enabled = True
                ddlLineaInv.DataSource = dts
                ddlLineaInv.DataTextField = "descripcion"
                ddlLineaInv.DataValueField = "codigo"
                ddlLineaInv.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub ListaAreas(ByVal comite_id As Integer)
        Try
            Dim obj As New clsInvestigacion
            Dim dts As New Data.DataTable
            dts = obj.ListaAreasPorComite(comite_id)
            If dts.Rows.Count > 0 Then
                ddlAreas.DataSource = dts
                ddlAreas.DataTextField = "descripcion"
                ddlAreas.DataValueField = "codigo"
                ddlAreas.DataBind()

                ddlAreas.Enabled = True
                ddlLineaInv.Enabled = False
                ddlLineaInv.Items.Clear()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub ddlcomites_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcomites.SelectedIndexChanged
        Try
            'Response.Write(Me.ddlcomites.SelectedValue)
            If ddlcomites.SelectedValue <> 0 Then
                ListaAreas(Me.ddlcomites.SelectedValue)
                ddlAreas.Focus()
            Else
                ddlAreas.Items.Clear()
                ddlAreas.Enabled = False
                ddlLineaInv.Enabled = False
                ddlLineaInv.Items.Clear()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub ddlAreas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAreas.SelectedIndexChanged
        Try
            If ddlcomites.SelectedValue <> 0 Then
                If ddlAreas.SelectedValue <> 0 Then
                    ListaLinea(Me.ddlAreas.SelectedValue)
                    ddlInstanciaInv.Focus()
                End If
            Else
                ddlAreas.Enabled = False
                ddlAreas.SelectedValue = 0
                ClientScript.RegisterStartupScript(Me.GetType, "FaltanDatos", "alert('Seleccione un comité de la lista desplegable.')", True)
                ddlcomites.Focus()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub cmdRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRegresar.Click
        Try
            'ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmGestionInvestigacion.aspx?id=" & Request.QueryString("id").ToString & "';", True)
            ClientScript.RegisterStartupScript(Me.GetType, "siguientepagina", "location.href='frmGestionInvestigacion.aspx?id=" & Request.QueryString("id").ToString & "&ctf=" & Request.QueryString("ctf") & "';", True)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class


