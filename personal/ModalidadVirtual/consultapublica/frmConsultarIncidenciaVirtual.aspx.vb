Imports System.IO
Imports System.Security.Cryptography
Imports System.Drawing

Partial Class ModalidadVirtual_consultapublica_frmConsultarTramiteVirtual
    Inherits System.Web.UI.Page

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        cpf.Value = Me.ddlEscuela.SelectedValue

        If cpf.Value = "" Then
            cpf.Value = 0
        End If
        fnMostrarEvaluar(False)
        Me.txtnroTramite.Attributes("placeholder") = "INC-" & Date.Now.Year.ToString & "-####"

        Dim obj As New ClsConectarDatos
        Dim objFun As New ClsFunciones
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()
        Me.ddlEscuela.Items.Clear()
        Me.ddlEscuela.DataBind()
        'objFun.CargarListas(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "3", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf")
        objFun.CargarListasGrupo(Me.ddlEscuela, obj.TraerDataTable("ConsultarEscuelaPorPersonal", "3", CInt(Session("id_per").ToString()), 2), "codigo_Cpf", "nombre_Cpf", "TipoEstudio")
        Page.RegisterStartupScript("Combo", "<script> GroupDropdownlist();</script>")
        obj.CerrarConexion()
        obj = Nothing
        objFun = Nothing

    End Sub

    Protected Sub ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type & "');</script>")
    End Sub


#Region "Eventos"


    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(2).Text.ToString.Trim.Contains("PENDIENTE") Then

                e.Row.Cells(0).ForeColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.Red
                e.Row.Cells(2).ForeColor = Drawing.Color.Red
                e.Row.Cells(3).ForeColor = Drawing.Color.Red
            Else


            End If
        End If
    End Sub

    Protected Sub gvDatos_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvDatos.PreRender
        If gvDatos.Rows.Count > 0 Then
            gvDatos.UseAccessibleHeader = True
            gvDatos.HeaderRow.TableSection = TableRowSection.TableHeader
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        CargaDatos()
    End Sub


    Protected Sub gvDatos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDatos.RowCommand
        Try

            ' Response.Write(e.CommandArgument)
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            ' Response.Write(e.CommandName)
            If (e.CommandName = "Evaluar") Then
                Dim codigo_inc As Integer = 0
                hddtareq.Value = Encriptar(gvDatos.DataKeys(index).Values("codigo_inc").ToString)
                codigo_inc = gvDatos.DataKeys(index).Values("codigo_inc")
                'Dim fechaPago As String = ""
                'If Not IsDBNull(gvDatos.DataKeys(index).Values("fecha_cin")) Then
                '    fechaPago = gvDatos.DataKeys(index).Values("fecha_cin")
                'End If
                'Me.lblSemestreMatriculado.Visible = False
                'Me.lblTotalSemestre.Visible = False
                'Me.lblInfoAdicional.Visible = False


                Dim codigoAlu As Integer = 0
                codigoAlu = gvDatos.DataKeys(index).Values("codigo_Alu")

                Dim codigoUniver As String = ""
                codigoUniver = gvDatos.DataKeys(index).Values("codigoUniver_Alu")

                'Dim codigo_inc As Integer = 0
                'codigo_inc = gvDatos.DataKeys(index).Values("codigo_inc")

                Dim tramite As String = ""
                'tramite = gvDatos.DataKeys(index).Values("descripcion_ctr")

                Dim fechaTramite As String = ""
                'fechaTramite = gvDatos.DataKeys(index).Values("fechaReg_trl").ToString

                fnMostrarEvaluar(True)
                fnInformacionTramite(CInt(codigo_inc))
                'fnInformacionTramite(CInt(gvDatos.DataKeys(index).Values("codigo_dta").ToString))
                'accionURL(codigoUniver, codigoAlu, "", codigo_trl, codigo_dta, tramite, fechaPago, fechaTramite)
                MostrarInformacionEvaluadores(codigo_inc)
            End If
        Catch ex As Exception
            Response.Write("Error gvDatos_RowCommand: " & ex.Message & " - " & ex.StackTrace)
        End Try
    End Sub



    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        fnMostrarEvaluar(False)
    End Sub


#End Region


#Region "Procedimiento"



    Private Sub CargaDatos()
        If fnValidarFiltroBuscar() Then


            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            Dim filtro As String = ""
            Dim fechaI As Date = "#01/01/1901#"
            Dim fi As String = ""
            Dim fechaF As Date = "#01/01/1901#"
            Dim ff As String = ""
            Try
                If Me.txtfeciniTes.value <> "" Then
                    fechaI = Me.txtfeciniTes.value
                    fi = Me.txtfeciniTes.value

                End If

                If Me.txtfecfinTes.value <> "" Then
                    fechaF = Me.txtfecfinTes.value
                    ff = Me.txtfecfinTes.value
         
                End If
                If fi <> "" And ff <> "" Then
                    filtro = "F"
                Else
                    filtro = ""
                    Me.txtfeciniTes.value = ""
                    Me.txtfecfinTes.value = ""
                End If


                obj.AbrirConexion()


                'dt = obj.TraerDataTable("TRL_BuscaTramiteLinea", Me.txtAlumno.Text.Replace(" ", "%"), Me.cboEstado.SelectedValue, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"))
                dt = obj.TraerDataTable("MV_rpteconsultarIncidencia", "1", Me.txtAlumno.Text.Replace(" ", "%"), Me.txtnroTramite.Text, Me.Request.QueryString("ctf"), Me.Request.QueryString("id"), cpf.Value, filtro, fechaI, fechaF)

                obj.CerrarConexion()
                If dt.Rows.Count > 0 Then
                    btnExportar.Visible = True
                Else
                    btnExportar.Visible = False
                End If


                Me.gvDatos.DataSource = dt
                Me.gvDatos.DataBind()

            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Private Function fnValidarFiltroBuscar() As Boolean
        Try
            If Me.txtAlumno.Text.Trim <> "" Or Me.txtnroTramite.Text.Trim <> "" Then
                Return True
            Else
                'ShowMessage("Ingrese algun dato en los filtros de búsqueda: ", MessageType.Warning)
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Sub fnMostrarEvaluar(ByVal sw As Boolean)

        If sw Then
            Me.pnlLista.Visible = False
            Me.pnlRegistro.Visible = True
        Else
            Me.pnlLista.Visible = True
            Me.pnlRegistro.Visible = False
        End If

    End Sub

    Private Sub fnInformacionTramite(ByVal codigo_inc As Integer)
        'Response.Write(codigo_inc)

        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim str As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try

            obj.AbrirConexion()
            dt = obj.TraerDataTable("MV_informacionincidencia", "1", codigo_inc)
            obj.CerrarConexion()

            If dt.Rows.Count > 0 Then

                Me.lblInfEstNumero.Text = dt.Rows(0).Item("numero").ToString()
                Me.lblInfEstEscuela.Text = dt.Rows(0).Item("escuela").ToString()
                Me.lblInfEstAlumno.Text = dt.Rows(0).Item("estudiante").ToString()
                Me.lblInfEstEmail.Text = dt.Rows(0).Item("eMail_Alu").ToString()
                Me.lblInfEstEmailUsat.Text = dt.Rows(0).Item("UserPrincipalName").ToString()
                Me.lblInfEstTelefono.Text = dt.Rows(0).Item("telefonoMovil_Dal").ToString()

                Me.lblInfEstDocIdent.Text = dt.Rows(0).Item("nroDocIdent_Alu").ToString()
                If dt.Rows(0).Item("estadoActual_Alu") = 1 Then
                    Me.lblInfEstEstado.Text = "Activo"
                Else
                    Me.lblInfEstEstado.Text = "Inactivo"
                End If

                If dt.Rows(0).Item("estadoDeuda_Alu").ToString() = "1" Then
                    Me.lblInfEstTieneDeuda.Text = "Si"
                Else
                    Me.lblInfEstTieneDeuda.Text = "No"
                End If

                If dt.Rows(0).Item("beneficio_Alu").ToString() = "1" Then
                    Me.lblInfEstBeneficio.Text = "Si"
                Else
                    Me.lblInfEstBeneficio.Text = "No"
                End If


                Me.lblInfEstRespPago.Text = dt.Rows(0).Item("PersonaApod_Dal").ToString()
                Me.lblInfEstDirRespPago.Text = dt.Rows(0).Item("direccionApod_Dal").ToString()
                Me.lblTramite.Text = dt.Rows(0).Item("asunto_inc").ToString()
                Me.lblTramiteDescripcion.Text = dt.Rows(0).Item("descripcion_inc").ToString()
                Me.lblFechaTramite.text = dt.Rows(0).Item("fecha").ToString()

                If dt.Rows(0).Item("archivo").ToString <> "" Then
                    lblArchivo.Text = "Descargar"
                    lblArchivo.Attributes.Add("onclick", "fnDescargar('" + dt.Rows(0).Item("archivo") + "'); return false")
                    lblArchivo.ForeColor = Drawing.Color.Blue
                Else
                    lblArchivo.Text = "No tiene archivo adjunto"
                    lblArchivo.Attributes.Remove("onclick")
                    lblArchivo.ForeColor = Drawing.Color.Red
                End If
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            obj = Nothing
        End Try

    End Sub

    Private Sub MostrarInformacionEvaluadores(ByVal codigo_inc As Integer)
        Try
            Dim dt As New Data.DataTable
            Dim obj As New ClsConectarDatos

            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("MV_InstanciaIncidencia_Listar", "1", codigo_inc)
            obj.CerrarConexion()

            Me.gvFlujo.DataSource = dt
            Me.gvFlujo.DataBind()


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Dim sb As StringBuilder = New StringBuilder()
        Dim SW As System.IO.StringWriter = New System.IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(SW)
        Dim Page As Page = New Page()
        Dim form As HtmlForm = New HtmlForm()
		
		If gvDatos.Rows.Count > 0 Then
			Me.gvDatos.EnableViewState = False
			Page.EnableEventValidation = False
			Page.DesignerInitialize()
			Page.Controls.Add(form)
			form.Controls.Add(Me.gvDatos)
			Page.RenderControl(htw)
			Response.Clear()
			Response.Buffer = True
			Response.ContentType = "application/vnd.ms-excel"
			Response.AddHeader("Content-Disposition", "attachment;filename=ConsultasCalsesVirtuales.xls")
			'Response.Charset = "UTF-8"
			'Response.ContentEncoding = Encoding.Default        
			Response.Cache.SetCacheability(HttpCacheability.NoCache)
			Response.Write(sb.ToString())
			Response.End()
		End If
    End Sub
#End Region


#Region "Funciones"
    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function

    Public Function Desencriptar(ByVal Input As String) As String
        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function
#End Region



End Class
