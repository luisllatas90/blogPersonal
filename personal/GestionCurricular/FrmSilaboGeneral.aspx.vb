Imports System.Collections.Generic
Imports System.IO

Partial Class GestionCurricular_FrmSilaboGeneral
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private cod_user As Integer
    Private cod_ctf As Integer
    Private ruta As String = ConfigurationManager.AppSettings("SharedFiles") '"http://localhost/campusvirtual/ArchivosCompartidos/SharedFiles.asmx"
    Private uni_aux As String = ""
    Private tot_uni As Integer = 1
    Private odCursoProgramado As d_CursoProgramado, oeCursoProgramado As e_CursoProgramado ' 20191230 - ENevado

    Public Enum MessageType
        Success
        [Error]
        Info
        Warning
    End Enum

#End Region

#Region "Eventos"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If (Session("id_per") Is Nothing) Then
                Response.Redirect("../../sinacceso.html")
            End If

            cod_user = Session("id_per")

            If Not String.IsNullOrEmpty(Session("cod_ctf")) Then
                cod_ctf = Session("cod_ctf")
            Else
                cod_ctf = Request.QueryString("ctf")
            End If

            If IsPostBack = False Then
                Dim dtFecha As Data.DataTable = New Data.DataTable("dtFecha")
                dtFecha.Columns.Add("fechas")
                dtFecha.Columns.Add("evento")
                dtFecha.Columns.Add("es_feriado")
                dtFecha.Columns.Add("tipos")
                ViewState("dtFecha") = dtFecha

                Session("dia_fec") = Nothing
                Session("gc_dtResultados") = Nothing
                Call mt_CargarSemestre()
                Call mt_CargarCarreraProf()

                Me.txtBuscar.Attributes.Add("onKeyPress", "txtBuscar_onKeyPress('" & Me.btnBuscar.ClientID & "', event)")
                Me.txtBuscar.Visible = False
                Me.btnBuscar.Visible = False
                Me.btnFuArchivo.Attributes.Add("onClick", "document.getElementById('" + fuArchivo.ClientID + "').click();")

                If Not String.IsNullOrEmpty(Session("codigo_pes")) Then
                    If Me.ddlSemestre.Items.Count > 0 Then Me.ddlSemestre.SelectedValue = Session("codigo_cac") : Call mt_CargarCarreraProf()
                    If Me.ddlCarreraProf.Items.Count > 0 Then Me.ddlCarreraProf.SelectedValue = Session("codigo_cpf") : Call mt_CargarDatos(cod_user)

                    Session.Remove("codigo_cac")
                    Session.Remove("codigo_pes")
                    Session.Remove("codigo_cpf")
                    Session.Remove("curso")
                End If

                divCarrera.Visible = IIf(cod_ctf = 1 Or cod_ctf = 232 Or cod_ctf = 218, True, False)
            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlSemestre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSemestre.SelectedIndexChanged
        Try
            Call mt_CargarDatos(cod_user)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlCarreraProf_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCarreraProf.SelectedIndexChanged
        Try
            Call mt_CargarDatos(cod_user)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlEstado.SelectedIndexChanged
        Try
            Call mt_CargarDatos(cod_user)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub ddlUnidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUnidad.SelectedIndexChanged
        Try
            Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value, ddlUnidad.SelectedValue)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnGuardarActa_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarActa.Click
        'Dim serializer As System.Web.Script.Serialization.JavaScriptSerializer = New System.Web.Script.Serialization.JavaScriptSerializer()
        'Dim JSONresult As String = ""
        'Dim Data As New Dictionary(Of String, Object)()
        'Dim list As New List(Of Dictionary(Of String, Object))()
        Dim obj As New ClsConectarDatos
        Dim idtabla, codigo_cup As Integer
        Dim dt As New Data.DataTable
        Dim respuesta As String = ""
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString

        Try
            If Not (IsDBNull(Session("perlogin"))) And Session("perlogin").ToString <> "" Then
                codigo_cup = CInt(Me.hdCodigoCup.Value)
                If codigo_cup = 0 Then Throw New Exception("¡ Seleccione un Curso !")
                idtabla = 20

                If Me.fuArchivo.HasFile Then
                    Dim Archivos As HttpFileCollection = Request.Files
                    For i As Integer = 0 To Archivos.Count - 1
                        'Data.Add("Step", idtabla & "-" & codigo_dec & "-" & Archivos(i).FileName)
                        Call fc_SubirArchivo(idtabla, codigo_cup, Archivos(i))
                    Next
                End If
                obj.AbrirConexion()
                obj.Ejecutar("CursoProgramado_SubirActa", codigo_cup)
                obj.CerrarConexion()
            Else
                Throw New Exception("Inicie Sesión")
            End If

            Call mt_ShowMessage("¡ Se subió el acta correctamente !", MessageType.Success)

            'Data.Add("Status", "OK")
            'Data.Add("Message", respuesta)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)

            'Data.Add("Status", "Fail")
            'Data.Add("Message", ex.Message & " - " & Session("perlogin").ToString)
            'list.Add(Data)
            'JSONresult = serializer.Serialize(list)
            'Response.Write(JSONresult)
        End Try
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Try
            If Session("gc_dtResultados") Is Nothing Then
                Call mt_CargarDatos(cod_user)
            End If

            Dim dt As New Data.DataTable
            Dim dv As New Data.DataView
            Dim strBuscar As String = ""

            If Me.txtBuscar.Text.Trim <> "" Then
                strBuscar = Me.txtBuscar.Text.Trim.ToUpper.Replace("Á", "A").Replace("É", "E").Replace("Í", "I").Replace("Ó", "O").Replace("Ú", "U")
                dv = New Data.DataView(CType(Session("gc_dtResultados"), Data.DataTable), "nombre_Cur_Aux like '%" & strBuscar & "%'", "", Data.DataViewRowState.CurrentRows)
                dt = dv.ToTable
            Else
                dt = CType(Session("gc_dtResultados"), Data.DataTable)
            End If

            Me.gvResultados.DataSource = dt
            Me.gvResultados.DataBind()

            Me.txtBuscar.Focus()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub btnSalir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.Click, btnSalir2.ServerClick
        Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
        Call mt_CargarDatos(cod_user)
    End Sub

    Protected Sub gvResultados_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim objGridView As GridView = CType(sender, GridView)
                Dim objgridviewrow As GridViewRow = New GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert)
                Dim objtablecell As TableCell = New TableCell()
                mt_AgregarCabecera(objgridviewrow, objtablecell, 5, 1, "Mis Asignaturas")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Evaluaciones")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Sesiones")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 1, 1, "Fechas")
                mt_AgregarCabecera(objgridviewrow, objtablecell, 6, 1, "Acciones")
                objGridView.Controls(0).Controls.AddAt(0, objgridviewrow)
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvResultados.RowCommand
        Try
            Dim index As Integer = CInt(e.CommandArgument)
            If index >= 0 Then
                'codigo_cur,nombre_cur,codigo_dis,fecha_apr,codigo_cup,descripcion_Cac,nombre_Cpf,codigo_Pes,codigo_Cac,codigo_Cpf
                Dim codigo_cac, codigo_dis, codigo_cur, codigo_cup, codigo_pes, codigo_cpf, nombre_cur, nombre_cpf, nombre_gru As String
                Dim modular_pcu As Boolean
                codigo_cur = Me.gvResultados.DataKeys(index).Values("codigo_cur").ToString()
                nombre_cur = Me.gvResultados.DataKeys(index).Values("nombre_cur").ToString()
                codigo_dis = Me.gvResultados.DataKeys(index).Values("codigo_dis").ToString()
                codigo_cup = Me.gvResultados.DataKeys(index).Values("codigo_cup").ToString()
                nombre_cpf = Me.gvResultados.DataKeys(index).Values("nombre_Cpf").ToString()
                codigo_pes = Me.gvResultados.DataKeys(index).Values("codigo_Pes").ToString()
                codigo_cac = Me.gvResultados.DataKeys(index).Values("codigo_Cac").ToString()
                codigo_cpf = Me.gvResultados.DataKeys(index).Values("codigo_Cpf").ToString()
                nombre_gru = Me.gvResultados.DataKeys(index).Values("grupoHor_Cup").ToString()
                modular_pcu = CBool(Me.gvResultados.DataKeys(index).Values("modular_pcu"))

                Me.hdCodigoDis.Value = codigo_dis
                Me.hdCodigoCur.Value = codigo_cur
                Me.hdCodigoCup.Value = codigo_cup

                Session("codigo_cac") = codigo_cac
                Session("codigo_cup") = codigo_cup
                Session("cod_ctf") = cod_ctf

                If e.CommandName.Equals("RegistrarFechas") Then
                    If modular_pcu Then
                        Session("codigo_dis") = codigo_dis
                        Session("codigo_cur") = codigo_cur
                        Session("codigo_pes") = codigo_pes
                        Session("codigo_cpf") = codigo_cpf
                        Session("curso") = nombre_cur
                        Session("grupo") = nombre_gru
                        Session("carrera") = nombre_cpf
                        Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
                        Response.Redirect("~/GestionCurricular/FrmAdicionarFechasSesion.aspx")
                    Else
                        lblCursoA.InnerText = nombre_cur & " (" & nombre_gru & ")"
                        Me.divAlertModal.Visible = False
                        Me.lblMensaje.InnerText = ""
                        updMensaje.Update()

                        Call mt_CargarUnidad(codigo_dis)
                        Call mt_CargarDatosSesion(codigo_dis, codigo_cur, codigo_cup, ddlUnidad.SelectedValue)

                        Page.RegisterStartupScript("Pop", "<script>openModal('registrar','','" & nombre_cur & " (" & nombre_gru & ")" & "');</script>")
                    End If

                ElseIf e.CommandName.Equals("DescargarSilabo") Then
                    Call mt_DescargarArchivo(codigo_cup)
                ElseIf e.CommandName.Equals("BajarActa") Then
                    Dim memory As New System.IO.MemoryStream
                    Dim nombreArchivo As String = "Acta " & Me.gvResultados.DataKeys(index).Values(5).ToString() & " " & Me.gvResultados.DataKeys(index).Values(1).ToString()

                    Call mt_GenerarActa(codigo_cup, CStr(Me.ddlSemestre.SelectedValue), Server.MapPath(".") & "/logo_usat.png", memory, True)
                    Dim bytes() As Byte = memory.ToArray
                    memory.Close()
                    Response.Clear()
                    Response.Buffer = False
                    Response.Charset = ""
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" & nombreArchivo.Replace(",", "") & ".pdf")
                    Response.AddHeader("content-length", bytes.Length.ToString())
                    Response.BinaryWrite(bytes)
                    Response.End()

                ElseIf e.CommandName.Equals("SubirActa") Then
                    Dim obj As New ClsConectarDatos
                    Dim dtActa As Data.DataTable
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

                    obj.AbrirConexion()
                    dtActa = obj.TraerDataTable("COM_ObtenerActaAsignatura", codigo_cup, 20)
                    obj.CerrarConexion()

                    If dtActa.Rows.Count > 0 Then
                        If dtActa.Rows(0).Item("archivo").ToString.Trim.Contains("No se eligió") Then
                            spnFile.InnerText = dtActa.Rows(0).Item("archivo").ToString.Trim
                        Else
                            Call mt_DescargarActa(codigo_cup)
                        End If
                    End If
                    dtActa.Dispose()

                    Page.RegisterStartupScript("Pop", "<script>openModal('','subirActa', '" & nombre_cur & " (" & nombre_gru & ")" & "');</script>")
                ElseIf e.CommandName.Equals("Instrumentos") Then
                    Session("codigo_dis") = codigo_dis
                    Session("codigo_cur") = codigo_cur
                    Session("codigo_pes") = codigo_pes
                    Session("codigo_cpf") = codigo_cpf
                    Session("curso") = nombre_cur
                    Session("grupo") = nombre_gru
                    Session("carrera") = nombre_cpf
                    'Response.Write("<script language='javascript'> window.open('FrmAdicionarInstrumentosContenido.aspx', 'window', 'height=600,width=820,top=50,left=50,toolbar=yes,scrollbars=yes,resizable=yes');</script>")
                    Page.RegisterStartupScript("Pop", "<script>closeModal();</script>")
                    
                    If modular_pcu Then
                        Response.Redirect("~/GestionCurricular/FrmAdicionarInstrumentosContenido_modular.aspx")
                    Else
                        Response.Redirect("~/GestionCurricular/FrmAdicionarInstrumentosContenido.aspx")
                    End If

                ElseIf e.CommandName.Equals("QuitarActa") Then
                    Dim obj As New ClsConectarDatos
                    obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

                    obj.AbrirConexion()
                    obj.TraerDataTable("CursoProgramado_QuitarActa", codigo_cup)
                    obj.CerrarConexion()

                    Call mt_CargarDatos(cod_user)

                ElseIf e.CommandName.Equals("Confirmar") Then
                    ' 20191230 - ENevado -----------------------------------------------------------------------------\
                    Dim _return As Boolean
                    odCursoProgramado = New d_CursoProgramado : oeCursoProgramado = New e_CursoProgramado
                    With oeCursoProgramado
                        .TipoOperacion = "UES" : .codigo_cup = codigo_cup : .estado_sil = "E" : .codigo_per = cod_user
                    End With
                    _return = odCursoProgramado.fc_ActualizarCursoProgramado(oeCursoProgramado)
                    If _return Then
                        mt_CargarDatos(cod_user)
                        mt_ShowMessage("¡Se Confirmo el envio del sílabo correctamente!", MessageType.Success)
                    Else
                        Throw New System.Exception("¡ No se pudo realizar la operación !")
                    End If
                    ' ------------------------------------------------------------------------------------------------/
                End If

            End If
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvResultados.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow AndAlso e.Row.RowIndex >= 0 Then
                Dim i As Integer = e.Row.RowIndex
                Dim ins_total, ins_asign, ins_pend, ses_total, ses_asign, ses_pend, fec_total, fec_asign, fec_pend As String

                ins_total = Me.gvResultados.DataKeys(i).Values("instr_total")
                ins_asign = Me.gvResultados.DataKeys(i).Values("instr_asign")
                ins_pend = Me.gvResultados.DataKeys(i).Values("instr_pend")

                ses_total = Me.gvResultados.DataKeys(i).Values("sesion_total")
                ses_asign = Me.gvResultados.DataKeys(i).Values("sesion_asign")
                ses_pend = Me.gvResultados.DataKeys(i).Values("sesion_pend")

                fec_total = Me.gvResultados.DataKeys(i).Values("fechas_total")
                fec_asign = Me.gvResultados.DataKeys(i).Values("fechas_asign")
                fec_pend = Me.gvResultados.DataKeys(i).Values("fechas_pend")

                e.Row.Cells(6).Text = ins_total & "     |     " & ins_asign & "     |     " & CStr(ins_pend.Length - ins_pend.Replace("|", "").Length)
                e.Row.Cells(6).ToolTip = ins_pend.Replace("|", vbCr).Replace("|", vbLf)
                e.Row.Cells(6).HorizontalAlign = HorizontalAlign.Center

                e.Row.Cells(7).Text = ses_total & "     |     " & ses_asign & "     |     " & CStr(ses_pend.Length - ses_pend.Replace("|", "").Length)
                e.Row.Cells(7).ToolTip = ses_pend.Replace("|", vbCr).Replace("|", vbLf)
                e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
                
                e.Row.Cells(8).Text = fec_total & "     |     " & fec_asign & "     |     " & CStr(CInt(Len(fec_pend) / 11))
                e.Row.Cells(8).ToolTip = fec_pend.Replace("|", vbCr).Replace("|", vbLf)
                e.Row.Cells(8).HorizontalAlign = HorizontalAlign.Center
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvResultados_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResultados.PageIndexChanging
        If Session("gc_dtResultados") IsNot Nothing Then
            Me.gvResultados.DataSource = CType(Session("gc_dtResultados"), Data.DataTable)
            Me.gvResultados.DataBind()
        End If

        Me.gvResultados.PageIndex = e.NewPageIndex
        Me.gvResultados.DataBind()
    End Sub

    Protected Sub gvSesion_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvSesion.RowCancelingEdit
        gvSesion.EditIndex = -1
        Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value, ddlUnidad.SelectedValue)
    End Sub

    Protected Sub gvSesion_OnRowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSesion.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim numero_uni As String = gvSesion.DataKeys(e.Row.RowIndex).Item("numero_uni").ToString
                Dim unidad_des As String = gvSesion.DataKeys(e.Row.RowIndex).Item("descripcion_uni").ToString

                If Not numero_uni.Equals(uni_aux) Then
                    uni_aux = numero_uni
                    Dim objGridView As GridView = CType(sender, GridView)
                    Dim objGridViewRow As GridViewRow = New GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert)
                    Dim objTableCell As TableCell = New TableCell()
                    Dim fila As Integer = objGridView.Rows.Count

                    Call mt_AgregarCabecera(objGridViewRow, objTableCell, gvSesion.Columns.Count, uni_aux & ": " & unidad_des, "#E3DFDF", True)
                    objGridView.Controls(0).Controls.AddAt(fila + tot_uni, objGridViewRow)

                    tot_uni += 1
                End If
            End If
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Protected Sub gvSesion_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvSesion.RowEditing
        gvSesion.EditIndex = e.NewEditIndex
        Session("dia_fec") = gvSesion.DataKeys(gvSesion.EditIndex).Item("dia_fec").ToString()
        Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value, ddlUnidad.SelectedValue)
    End Sub

    Protected Sub gvSesion_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvSesion.RowDeleting
        Call mt_ShowMessage(pMsje, MessageType.Error, True)
    End Sub

    Protected Sub gvSesion_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvSesion.RowUpdating
        If gvSesion.EditIndex > -1 Then
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            Try
                Dim dateValue As String = ""
                Dim dateText As String = ""
                Dim ddl As ListBox 'DropDownList
                ddl = CType(gvSesion.Rows(e.RowIndex).FindControl("ddlFecha"), ListBox)

                For Each item As ListItem In ddl.Items
                    If item.Selected AndAlso item.Value <> "" Then
                        If dateValue.Length > 0 Then dateValue &= "|"
                        If dateText.Length > 0 Then dateText &= "|"
                        dateValue &= item.Value
                        dateText &= item.Text
                    End If
                Next

                'If ddl.SelectedValue <> "" Then
                If Not String.IsNullOrEmpty(dateValue) Then
                    Dim fecha As String = ""
                    Dim flag As Boolean = False
                    Dim motivo As String = ""
                    Dim tipo As String = ""
                    Dim dt As Data.DataTable = TryCast(ViewState("dtFecha"), Data.DataTable)

                    If dt IsNot Nothing Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            For j As Integer = 0 To dateValue.Split("|").Length - 1
                                If dt.Rows(i).Item(2) = True And dt.Rows(i).Item(0).ToString.Contains(dateValue.Split("|")(j)) Then
                                    fecha = dateText.Split("|")(j)
                                    motivo = dt.Rows(i).Item(1).ToString
                                    tipo = dt.Rows(i).Item(3).ToString
                                    tipo = IIf(tipo.Equals("FI"), "Feriado Institucional", IIf(tipo.Equals("FC"), "Feriado Calendario", "Suspensión por Horas"))

                                    flag = True
                                    Exit For
                                End If
                            Next

                            If flag Then Exit For

                            'If dt.Rows(i).Item(2) = True And dt.Rows(i).Item(0).ToString.Contains(ddl.SelectedItem.Value.ToString()) Then
                            '    fecha = ddl.SelectedItem.Text.ToString().Substring(0, 15)
                            '    motivo = dt.Rows(i).Item(1).ToString
                            '    tipo = dt.Rows(i).Item(3).ToString
                            '    tipo = IIf(tipo.Equals("FI"), "Feriado Institucional", IIf(tipo.Equals("FC"), "Feriado Calendario", "Suspensión por Horas"))

                            '    flag = True
                            '    Exit For
                            'End If
                        Next
                        dt.Dispose()
                    End If

                    If flag Then
                        Call mt_ShowMessage("La fecha " & fecha.Substring(0, 15) & ". No puede ser seleccionada porque es " & tipo, MessageType.Info, True)
                        Return
                    End If

                    Dim codigo_ses As String = gvSesion.DataKeys(e.RowIndex).Item("codigo_ses").ToString()
                    Dim codigo_fec As String = gvSesion.DataKeys(e.RowIndex).Item("codigo_fec").ToString()
                    codigo_fec = IIf(String.IsNullOrEmpty(codigo_fec), "0", codigo_fec)

                    'obj.AbrirConexion()
                    'obj.Ejecutar("COM_ActualizarFechaSesion", codigo_fec, codigo_ses, Me.hdCodigoCup.Value, ddl.SelectedValue, ddl.SelectedItem.ToString, cod_user)
                    'obj.CerrarConexion()

                    obj.AbrirConexion()
                    obj.Ejecutar("COM_ActualizarFechaSesion", codigo_fec, codigo_ses, Me.hdCodigoCup.Value, dateValue, dateText, cod_user)
                    obj.CerrarConexion()

                    gvSesion.EditIndex = -1
                    Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value, ddlUnidad.SelectedValue)
                Else
                    Call mt_ShowMessage("Seleccione una fecha válida", MessageType.Info, True)
                End If
            Catch ex As Exception
                Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Info, True)
            End Try
        End If
    End Sub

    Protected Sub gvSesion_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSesion.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As ListBox = CType(e.Row.FindControl("ddlFecha"), ListBox)

            If Not ddl Is Nothing Then
                ddl.SelectionMode = ListSelectionMode.Multiple
                ddl.DataSource = fc_CargarHorario()
                ddl.DataValueField = "fechas"
                ddl.DataTextField = "descripcion"
                ddl.DataBind()

                'Agregar fila en blanco
                ddl.Items.Insert(0, New ListItem("[-- Seleccione una fecha --]", ""))

                Dim fec As String = gvSesion.DataKeys(e.Row.RowIndex).Item("dia_fec").ToString()
                If Not String.IsNullOrEmpty(fec) Then
                    For Each item As ListItem In ddl.Items
                        For i As Integer = 0 To fec.Split("|").Length - 1
                            If item.Value.Trim = fec.Split("|")(i).Trim Then
                                item.Selected = True
                            End If
                        Next
                    Next
                End If

                'ddl.SelectedValue = gvSesion.DataKeys(e.Row.RowIndex).Item("dia_fec").ToString()

                'Seleccionar por defecto el id actual
                'Dim fecha As String = CType(e.Row.FindControl("lblFecha"), Label).Text
                'ddl.Items.FindByValue(fecha).Selected = True
                'dt.Dispose()
            End If
        End If
    End Sub

    Private pMsje As String = ""

    Protected Sub OnDeleteFecha(ByVal sender As Object, ByVal e As EventArgs)
        Dim row As GridViewRow = TryCast((TryCast(sender, LinkButton)).NamingContainer, GridViewRow)
        Dim codigo_ses As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_ses").ToString
        Dim codigo_fec As String = gvSesion.DataKeys(row.RowIndex).Item("codigo_fec").ToString

        If Not String.IsNullOrEmpty(codigo_ses) And Not String.IsNullOrEmpty(codigo_fec) Then
            Dim obj As New ClsConectarDatos
            Dim dt As New Data.DataTable
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_EliminarFechaSesion", codigo_ses, Session("codigo_cup"), codigo_fec) 'dia_fec
            obj.CerrarConexion()

            gvSesion.EditIndex = -1

            If dt.Rows.Count > 0 Then
                Dim rpta As Integer, msje As String
                rpta = CInt(dt.Rows(0).Item(0).ToString)
                msje = dt.Rows(0).Item(1).ToString.Trim

                If rpta = 1 Then
                    Call mt_CargarDatosSesion(Me.hdCodigoDis.Value, Me.hdCodigoCur.Value, Me.hdCodigoCup.Value, ddlUnidad.SelectedValue)
                Else
                    pMsje = msje
                    'Call mt_ShowMessage(msje, MessageType.Info, True)
                End If
            End If
            dt.Dispose()
        End If
    End Sub

#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType, Optional ByVal modal As Boolean = False)
        If modal Then
            Me.divAlertModal.Visible = True
            Me.lblMensaje.InnerText = Message
            Me.divAlertModal.Focus()
            Me.lblMensaje.Focus()
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.divAlertModal)
            ScriptManager.GetCurrent(Me.Page).SetFocus(Me.lblMensaje)
            updMensaje.Update()
        Else
            Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
        End If
    End Sub

    Private Sub mt_CargarSemestre()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dt = obj.TraerDataTable("ConsultarCicloAcademico", "DA", "")
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlSemestre, dt, "codigo_Cac", "descripcion_Cac")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCarreraProf()
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            If cod_ctf = 1 Or cod_ctf = 232 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "CF", "2", cod_user)
            ElseIf cod_ctf = 218 Then
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UX", "2", cod_user)
            Else
                dt = obj.TraerDataTable("ConsultarCarreraProfesionalV2", "UC", "2", cod_user)
            End If
            obj.CerrarConexion()

            Call mt_CargarCombo(Me.ddlCarreraProf, dt, "codigo_Cpf", "nombre_Cpf")
            dt.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarCombo(ByVal cbo As DropDownList, ByVal dt As Data.DataTable, ByVal datavalue As String, ByVal datatext As String)
        cbo.DataSource = dt
        cbo.DataTextField = datatext
        cbo.DataValueField = datavalue
        cbo.DataBind()
    End Sub

    Private Sub mt_CargarDatos(ByVal user As Integer)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        Dim semestre, carrera As String
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            semestre = IIf(String.IsNullOrEmpty(Me.ddlSemestre.SelectedValue), "0", Me.ddlSemestre.SelectedValue)
            carrera = IIf(String.IsNullOrEmpty(Me.ddlCarreraProf.SelectedValue), "0", Me.ddlCarreraProf.SelectedValue)

            If cod_ctf = 1 Or cod_ctf = 232 Or cod_ctf = 218 Then user = -1

            obj.AbrirConexion()
            dt = obj.TraerDataTable("CursoProgramado_Listar_V2", "PS", -1, carrera, IIf((carrera = -2 And user = -1), -1, cod_user), semestre, user, ddlEstado.SelectedValue, cod_ctf)
            obj.CerrarConexion()

            Session("gc_dtResultados") = dt
            Me.gvResultados.DataSource = dt 'CType(Session("gc_dtResultados"), Data.DataTable)
            Me.gvResultados.DataBind()
            dt.Dispose()

            Me.txtBuscar.Visible = IIf(Me.gvResultados.Rows.Count > 0, True, False)
            Me.btnBuscar.Visible = IIf(Me.gvResultados.Rows.Count > 0, True, False)
        Catch ex As Exception
            Call mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub

    Private Sub mt_CargarDatosSesion(ByVal codigo_dis As String, ByVal codigo_cur As String, ByVal codigo_cup As String, Optional ByVal codigo_uni As Integer = -1)
        Dim dtSesion As Data.DataTable = fc_GetSesion(codigo_dis, codigo_cur, codigo_cup, codigo_uni)
        Try
            Me.gvSesion.DataSource = dtSesion
            Me.gvSesion.DataBind()

            If Me.gvSesion.Rows.Count > 0 Then
                Call mt_AgruparFilas(Me.gvSesion.Rows, 0, 4)
            End If

            Me.udpSesion.Update()

            Me.divAlertModal.Visible = False
            Me.lblMensaje.InnerText = ""
            updMensaje.Update()
        Catch ex As Exception
            Throw New Exception(ex.Message.ToString(), ex)
        End Try
    End Sub

    Private Sub mt_CargarUnidad(ByVal codigo_dis As String)
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString
        Try
            obj.AbrirConexion()
            codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)

            dt = obj.TraerDataTable("COM_ListarUnidades", codigo_dis, "S")
            obj.CerrarConexion()
            Call mt_CargarCombo(Me.ddlUnidad, dt, "codigo_uni", "descripcion")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para generar silabo en pdf
    ''' </summary>
    ''' <param name="codigo_cup"></param>
    ''' <param name="codigo_cac"></param>
    ''' <param name="sourceIcon"></param>
    ''' <param name="memory"></param>
    ''' <param name="vista"></param>
    ''' <remarks></remarks>
    Public Sub mt_GenerarActa(ByVal codigo_cup As Integer, ByVal codigo_cac As String, ByVal sourceIcon As String, ByVal memory As System.IO.Stream, Optional ByVal vista As Boolean = False)
        Dim obj As New ClsConectarDatos
        Dim dtCab As New Data.DataTable
        Dim dtDet As New Data.DataTable
        Dim TEst As String
        Dim imageURL As String = Server.MapPath(".") & "/logo_usat.png"
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            obj.AbrirConexion()
            dtCab = obj.TraerDataTable("COM_EstructuraActaSilabo", codigo_cup, codigo_cac, "C")
            dtDet = obj.TraerDataTable("COM_EstructuraActaSilabo", codigo_cup, codigo_cac, "D")
            TEst = dtCab.Rows(0).Item("codigo_test").ToString()

            Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4)
            Dim pdfWrite As iTextSharp.text.pdf.PdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, memory)

            pdfDoc.Open()

            Dim pdfTable As iTextSharp.text.pdf.PdfPTable = New iTextSharp.text.pdf.PdfPTable(6)
            pdfTable.SetWidths(New Single() {5.0F, 10.0F, 5.0F, 30.0F, 30.0F, 20.0F})
            pdfTable.WidthPercentage = 100.0F
            pdfTable.DefaultCell.Border = 0
            pdfTable.DefaultCell.Padding = 0

            ' 0: Cabecera del Acta ----------------------------------------------------------------------

            Dim srcIcon As String = sourceIcon
            Dim usatIcon As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(srcIcon)
            usatIcon.ScalePercent(30.0F)
            usatIcon.Alignment = iTextSharp.text.Element.ALIGN_LEFT

            Dim cellIcon As iTextSharp.text.pdf.PdfPCell = New iTextSharp.text.pdf.PdfPCell(usatIcon)
            cellIcon.HorizontalAlignment = 1
            cellIcon.VerticalAlignment = 5
            cellIcon.Border = 15
            cellIcon.Colspan = 2
            cellIcon.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName("WhiteSmoke"))

            pdfTable.AddCell(cellIcon)
            pdfTable.AddCell(fc_CeldaTexto("ACTA DE EXPOSICIÓN", 14.0F, 1, 15, 3, 1, 1, 5, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto("SISTEMA DE GESTIÓN DE LA CALIDAD" & Chr(13) & "CÓDIGO: USAT-EA-R-06" & Chr(13) & "VERSIÓN 02", 6.0F, 1, 15, 1, 1, 1, 5, "WhiteSmoke"))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            pdfTable.AddCell(fc_CeldaTexto("DOCUMENTO EXPUESTO", 7.0F, 1, 15, 2, 2, 1, 5, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("Protocolo de Seguridad del " & dtCab.Rows(0).Item("ambiente").ToString, 7.0F, 0, 15, 3, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("X", 7.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("Sílabo", 7.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FECHA DE EXPOSICIÓN", 7.0F, 1, 15, 1, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("fecha").ToString, 7.0F, 0, 15, 1, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 1: Datos Generales ------------------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("I. DATOS GENERALES", 7.0F, 1, 15, 6, 1, 1, 1, "WhiteSmoke"))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("FACULTAD", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("nombre_Fac").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("PROGRAMA DE ESTUDIOS", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("nombre_Cpf").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("SEMESTRE ACADÉMICO", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("descripcion_Cac").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("TIPO DE SERVICIO", 7.0F, 1, 15, 2, 5, 1, 5, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("2"), "X", ""), 7.0F, 0, 15, 1, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("Pregrado", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("10"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Pregrado: Programa para gente que trabaja (GO)", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("8"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Segunda Especialidad", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("5"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Postgrado", 7.0F, 0, 15, 3, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto(IIf(TEst.Equals("6"), "X", ""), 7.0F, 0, 15, 1, 1, 0))
            pdfTable.AddCell(fc_CeldaTexto("Educación continua", 7.0F, 0, 15, 3, 1, 0))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 1: Datos de Asignatura --------------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("II. DATOS DE ASIGNATURA", 7.0F, 1, 15, 6, 1, 1, 1, "WhiteSmoke"))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("ASIGNATURA", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("nombre_Cur").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("CICLO DE LA ASIGNATURA", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("ciclo_Cur").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("GRUPO HORARIO", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("grupoHor_Cup").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto("DOCENTE", 7.0F, 1, 15, 2, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto(dtCab.Rows(0).Item("docente").ToString, 7.0F, 0, 15, 4, 1, 1))

            pdfTable.AddCell(fc_CeldaTexto("", 1.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 2: Listado de estudiantes -----------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("III. LISTADO DE ESTUDIANTES", 7.0F, 1, 15, 6, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto("N°", 7.0F, 1, 15, 1, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto("APELLIDOS Y NOMBRES", 7.0F, 1, 15, 3, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto("CÓDIGO", 7.0F, 1, 15, 1, 1, 1, 1, "WhiteSmoke"))
            pdfTable.AddCell(fc_CeldaTexto("FIRMA", 7.0F, 1, 15, 1, 1, 1, 1, "WhiteSmoke"))

            For index As Integer = 0 To dtDet.Rows.Count - 1
                pdfTable.AddCell(fc_CeldaTexto((index + 1).ToString, 7.0F, 0, 15, 1, 1, 0))
                pdfTable.AddCell(fc_CeldaTexto(dtDet.Rows(index).Item("estudiante").ToString, 7.0F, 0, 15, 3, 1, 0))
                pdfTable.AddCell(fc_CeldaTexto(dtDet.Rows(index).Item("codigoUniver_Alu").ToString, 7.0F, 0, 15, 1, 1, 1))
                pdfTable.AddCell(fc_CeldaTexto("", 7.0F, 0, 15, 1, 1, 0))
            Next

            pdfTable.AddCell(fc_CeldaTexto(Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine, 7.0F, 0, 0, 6, 1, 1)) 'Salto de línea
            pdfTable.AddCell(fc_CeldaTexto(Environment.NewLine & " " & Environment.NewLine & " " & Environment.NewLine, 7.0F, 0, 0, 6, 1, 1)) 'Salto de línea

            ' 3: Firma ----------------------------------------------------------------------------------

            pdfTable.AddCell(fc_CeldaTexto("______________________________________", 7.0F, 1, 0, 4, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("______________________________________", 7.0F, 1, 0, 2, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FIRMA DOCENTE", 7.0F, 1, 0, 4, 1, 1))
            pdfTable.AddCell(fc_CeldaTexto("FIRMA COORDINADOR LABORATORIO", 7.0F, 1, 0, 2, 1, 1))

            pdfDoc.Add(pdfTable)

            pdfDoc.Close()
            dtCab.Dispose()
            dtDet.Dispose()
        Catch ex As Exception
            Throw ex
        Finally
            obj.CerrarConexion()
        End Try
    End Sub

    Private Sub mt_DescargarArchivo(ByVal codigo_cup As Long)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb, dt As New Data.DataTable

            Dim usuario As String = Session("perlogin")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            dt = obj.TraerDataTable("SilaboCurso_listar", -1, codigo_cup, 1)
            If dt.Rows.Count = 0 Then Throw New Exception("¡ No existe silabo para este curso !")
            'tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, 20, dt.Rows(0).Item("codigo_sil"), "YAXVXFQACX") ' Desarrollo
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, 22, dt.Rows(0).Item("codigo_sil"), "YAXVXFQACX") ' Produccion
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontró el archivo !")
            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", "YAXVXFQACX") ' Desarrollo
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)
            Dim bytes As Byte() = Convert.FromBase64String(imagen)
            Response.Clear()
            Response.Buffer = False
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
            Response.AppendHeader("Content-Length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.End()
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_DescargarActa(ByVal codigo_cup As Long)
        Try
            Dim wsCloud As New ClsArchivosCompartidosV2
            Dim list As New Dictionary(Of String, String)
            Dim obj As New ClsConectarDatos
            Dim tb As New Data.DataTable
            Dim usuario As String = Session("perlogin")
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            tb = obj.TraerDataTable("ArchivosCompartidos_Listar2", 1, 20, Session("codigo_cup"), "IFNGVE8H9Q")
            obj.CerrarConexion()
            If tb.Rows.Count = 0 Then Throw New Exception("¡ No se encontró el archivo !")
            list.Add("IdArchivo", tb.Rows(0).Item("IdArchivo").ToString)
            list.Add("Usuario", usuario)
            list.Add("Token", "IFNGVE8H9Q")
            Dim envelope As String = wsCloud.SoapEnvelopeDescarga(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/DownloadFile", usuario)
            Dim imagen As String = fc_ResultFile(result)
            Dim bytes As Byte() = Convert.FromBase64String(imagen)

            Response.Clear()
            Response.Buffer = False
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Response.ContentType = "application/pdf"
            Response.AddHeader("content-disposition", "attachment;filename=" & tb.Rows(0).Item("NombreArchivo").ToString.Replace(",", ""))
            Response.AppendHeader("Content-Length", bytes.Length.ToString())
            Response.BinaryWrite(bytes)
            Response.End()
            'Response.Write(envelope)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_AgruparFilas(ByVal gridViewRows As GridViewRowCollection, ByVal startIndex As Integer, ByVal totalColumns As Integer)
        If totalColumns = 0 Then Return
        Dim i As Integer, count As Integer = 1
        Dim lst As ArrayList = New ArrayList()
        Dim ctrl As TableCell

        lst.Add(gridViewRows(0))
        ctrl = gridViewRows(0).Cells(startIndex)

        For i = 1 To gridViewRows.Count - 1
            Dim nextTbCell As TableCell = gridViewRows(i).Cells(startIndex)

            If ctrl.Text = nextTbCell.Text Then
                count += 1
                nextTbCell.Visible = False
                lst.Add(gridViewRows(i))
            Else
                If count > 1 Then
                    ctrl.RowSpan = count
                    Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
                End If
                count = 1
                lst.Clear()
                ctrl = gridViewRows(i).Cells(startIndex)
                lst.Add(gridViewRows(i))
            End If
        Next
        If count > 1 Then
            ctrl.RowSpan = count
            Call mt_AgruparFilas(New GridViewRowCollection(lst), startIndex + 1, totalColumns - 1)
        End If
        count = 1
        lst.Clear()
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        'objtablecell.Style.Add("background-color", backcolor)
        'objtablecell.Style.Add("BackColor", backcolor)
        'objtablecell.Style.Add("Font-Bold", "true")
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal celltext As String, ByVal backcolor As String, Optional ByVal paint As Boolean = False)
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan

        If paint Then
            objtablecell.Style.Add("background-color", backcolor)
            objtablecell.Style.Add("font-weight", "600")
        End If

        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

    Protected Sub mt_AgregarCabecera(ByVal objgridviewrow As GridViewRow, ByVal objtablecell As TableCell, ByVal colspan As Integer, ByVal rowspan As Integer, ByVal celltext As String, Optional ByVal tooltip As String = "")
        objtablecell = New TableCell()
        objtablecell.Text = celltext
        objtablecell.ColumnSpan = colspan
        objtablecell.RowSpan = rowspan
        objtablecell.ToolTip = tooltip
        objtablecell.VerticalAlign = VerticalAlign.Middle
        objtablecell.HorizontalAlign = HorizontalAlign.Center
        objgridviewrow.Cells.Add(objtablecell)
    End Sub

#End Region

#Region "Funciones"

    Private Function fc_CargarHorario() As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable("data")
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        Try
            Dim dia_fec As String = Session("dia_fec")
            dia_fec = IIf(String.IsNullOrEmpty(dia_fec), "", dia_fec)

            obj.AbrirConexion()
            dt = obj.TraerDataTable("COM_ListarHorarioDocente", Session("codigo_cup"), Session("codigo_cac"), dia_fec, "1")

            Dim columns As String() = {"fechas", "evento", "es_feriado", "tipo"}
            Dim dtFecha As Data.DataTable = New Data.DataView(dt).ToTable(False, columns)

            ViewState("dtFecha") = dtFecha

            obj.CerrarConexion()
        Catch ex As Exception
            Throw ex
        End Try

        Return dt
    End Function

    Private Function fc_GetSesion(ByVal codigo_dis As String, ByVal codigo_cur As String, ByVal codigo_cup As String, Optional ByVal codigo_uni As Integer = -1) As Data.DataTable
        Dim obj As New ClsConectarDatos
        Dim dt As New Data.DataTable
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ToString

        uni_aux = ""
        tot_uni = 1

        obj.AbrirConexion()
        codigo_dis = IIf(String.IsNullOrEmpty(codigo_dis), "-1", codigo_dis)
        codigo_cur = IIf(String.IsNullOrEmpty(codigo_cur), "-1", codigo_cur)
        codigo_cup = IIf(String.IsNullOrEmpty(codigo_cup), "-1", codigo_cup)
        codigo_uni = IIf(String.IsNullOrEmpty(codigo_uni), "-1", codigo_uni)

        dt = obj.TraerDataTable("COM_ListarFechaSesion", codigo_dis, codigo_cur, codigo_cup, codigo_uni)
        obj.CerrarConexion()

        Return dt
    End Function

    Private Function fc_SubirArchivo(ByVal idTabla As String, ByVal codigo As String, ByVal file As HttpPostedFile) As String
        Dim list As New Dictionary(Of String, String)
        Try
            Dim _TablaId As String = idTabla
            Dim _Archivo As HttpPostedFile = file
            Dim _TransaccionId As String = codigo
            Dim _Fecha As String = Date.Now.ToString("dd/MM/yyyy")
            Dim _Usuario As String = Session("perlogin").ToString
            Dim Input(_Archivo.ContentLength) As Byte

            Dim br As New BinaryReader(_Archivo.InputStream)
            Dim binData As Byte() = br.ReadBytes(_Archivo.InputStream.Length)
            Dim base64 As Object = System.Convert.ToBase64String(binData)
            Dim _Nombre As String = Path.GetFileName(_Archivo.FileName.Replace("&", "_").Replace("'", "_").Replace("*", "_"))

            Dim wsCloud As New ClsArchivosCompartidos

            list.Add("Fecha", _Fecha)
            list.Add("Extencion", System.IO.Path.GetExtension(_Archivo.FileName))
            list.Add("Nombre", _Nombre)
            list.Add("TransaccionId", _TransaccionId)
            list.Add("TablaId", _TablaId)
            list.Add("NroOperacion", "")
            list.Add("Archivo", System.Convert.ToBase64String(binData, 0, binData.Length))
            list.Add("Usuario", _Usuario)
            list.Add("Equipo", "")
            list.Add("Ip", "")
            list.Add("param8", _Usuario)

            Dim envelope As String = wsCloud.SoapEnvelope(list)
            Dim result As String = wsCloud.PeticionRequestSoap(ruta, envelope, "http://usat.edu.pe/UploadFile", Session("perlogin").ToString)

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function fc_ResultFile(ByVal cadXml As String) As String
        Try
            Dim xError As String()
            Dim nsMgr As System.Xml.XmlNamespaceManager
            Dim xml As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            xml.LoadXml(cadXml)
            nsMgr = New System.Xml.XmlNamespaceManager(xml.NameTable)
            nsMgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/")
            Dim res As System.Xml.XmlNode = xml.DocumentElement.SelectSingleNode("/soap:Envelope/soap:Body", nsMgr)
            xError = res.InnerText.Split(":")
            If xError.Length = 2 Then
                Throw New Exception(res.InnerText)
            End If
            Return res.InnerText
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Funciones2"

    ''' <summary>
    ''' Función para crear una celda tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamaño de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan"></param>
    ''' <param name="_rowspan"></param>
    ''' <param name="_haligment">Alineación horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <param name="_valigment">Alineación vertical del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, Optional ByVal _valigment As Integer = 1, Optional ByVal _backgroundcolor As String = "") As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.VerticalAlignment = _valigment

        If Not String.IsNullOrEmpty(_text) Then
            celdaITC.Padding = 6
        End If

        If _backgroundcolor <> "" Then celdaITC.BackgroundColor = New iTextSharp.text.BaseColor(System.Drawing.Color.FromName(_backgroundcolor))

        Return celdaITC
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto con más atributos
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC, 4: UNDERLINE</param>
    ''' <param name="_border">Borde de la Celda. 0: NO_BORDER, 1: TOP_BORDER , 2: BOTTON_BORDER, 4: LEFT_BORDER, 8: RIGHT_BORDER, 15: FULL_BORDER </param>
    ''' <param name="_colspan"></param>
    ''' <param name="_rowspan"></param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <param name="_fontcolor"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _border As Integer, _
                                 ByVal _colspan As Integer, ByVal _rowspan As Integer, ByVal _haligment As Integer, _
                                 ByVal _fontcolor As iTextSharp.text.BaseColor) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC As iTextSharp.text.pdf.PdfPCell
        Dim fontITC As iTextSharp.text.Font
        fontITC = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style, _fontcolor)
        celdaITC = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC))
        celdaITC.Border = _border
        celdaITC.Colspan = _colspan
        celdaITC.Rowspan = _rowspan
        celdaITC.HorizontalAlignment = _haligment
        celdaITC.Padding = 6
        Return celdaITC
    End Function

    ''' <summary>
    ''' Función para crear una celta tipo texto
    ''' </summary>
    ''' <param name="_text">Contenido de la Celda</param>
    ''' <param name="_size">Tamano de letra del contenido de la celda</param>
    ''' <param name="_style">Estilo del Contenido de la Celda. 0: NORMAL, 1: BOLD, 2: ITALIC, 3: BOLDITALIC</param>
    ''' <param name="_haligment">Alineacion horizontal del contenido de la celda. 0: LEFT, 1: CENTER, 2: RIGHT, 4: TOP, 5: MIDDLE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function fc_CeldaTexto2(ByVal _text As String, ByVal _size As Single, ByVal _style As Integer, ByVal _haligment As Integer) As iTextSharp.text.pdf.PdfPCell
        Dim celdaITC2 As iTextSharp.text.pdf.PdfPCell
        Dim fontITC2 As iTextSharp.text.Font
        fontITC2 = New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, _size, _style)
        celdaITC2 = New iTextSharp.text.pdf.PdfPCell(New iTextSharp.text.Phrase(_text, fontITC2))
        celdaITC2.HorizontalAlignment = _haligment
        Return celdaITC2
    End Function

#End Region

End Class
