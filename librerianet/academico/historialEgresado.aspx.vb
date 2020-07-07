'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
Partial Class personal_academico_estudiante_historial
    Inherits System.Web.UI.Page
    Dim crd As Integer = 0
    Dim notacrd As Double = 0
    Dim crdAR As Int16 = 0
    Dim crdAC As Int16 = 0
    Dim AAR As Int16 = 0
    Dim AAC As Int16 = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim Obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString)
        If (Session("codigo_alu") Is Nothing) Then
            Response.Redirect("../ErrorSistema.aspx")
        End If

        Dim Obj As New ClsConectarDatos
        Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
        Dim Tbl As New Data.DataTable
        Dim codigo_alu As Integer

        'codigo_alu = Request.QueryString("id")
        codigo_alu = Session("codigo_alu")
        If Request.QueryString("VerDatos") = "" Or Request.QueryString("VerDatos") <> 0 Then
            Me.pnlDatos.Visible = True
        Else
            Me.pnlDatos.Visible = False
        End If
        Obj.AbrirConexion()
        Tbl = Obj.TraerDataTable("consultaracceso", "C", codigo_alu, 0)
        Obj.CerrarConexion()
        If Tbl.Rows.Count > 0 Then
            Me.lblcodigo.Text = Tbl.Rows(0).Item("codigouniver_alu")
            Me.lblalumno.Text = Tbl.Rows(0).Item("alumno")
            Me.lblescuela.Text = Tbl.Rows(0).Item("nombre_cpf")
            Me.lblcicloingreso.Text = Tbl.Rows(0).Item("cicloing_alu")
            Me.lblPlan.Text = Tbl.Rows(0).Item("descripcion_pes")            

            'Cargar la Foto
            Dim ruta As String
            Dim obEnc As Object
            obEnc = Server.CreateObject("EncriptaCodigos.clsEncripta")

            ruta = obEnc.CodificaWeb("069" & Tbl.Rows(0).Item("codigouniver_alu").ToString)
            'ruta = "http://www.usat.edu.pe/imgestudiantes/" & ruta
            ruta = "//intranet.usat.edu.pe/imgestudiantes/" & ruta
            Me.FotoAlumno.ImageUrl = ruta
            obEnc = Nothing

            'Cargar Record Académico si no tiene deuda o según la función
            'If Tbl.Rows(0).Item("estadodeuda_alu").ToString = "1" And _
            If "1" <> "1" And _
                Request.QueryString("ctf") <> 1 And Request.QueryString("ctf") <> 25 And _
                Request.QueryString("ctf") <> 9 And Request.QueryString("ctf") <> 26 And _
                Request.QueryString("ctf") <> 30 And Request.QueryString("ctf") <> 35 And _
                Request.QueryString("ctf") <> 16 And Request.QueryString("ctf") <> 7 And _
                Request.QueryString("ctf") <> 19 And Request.QueryString("ctf") <> 20 Then

                'Si es Profesionalizacion a partir de la segunda deuda
                If (Tbl.Rows(0).Item("codigo_test") = 3) Then

                Else
                    Me.lblMensaje.Text = "Lo sentimos no se puede mostrar su historial académico del Estudiante, por favor <br>comuníquese con la Oficina de Pensiones para que se le indique el motivo."
                    'cmdVer.Visible = False
                End If
            Else
                'Cargar la lista de ciclos Matriculados
                Obj.AbrirConexion()
                Me.dlstCiclos.DataSource = Obj.TraerDataTable("ConsultarHistorialAlumnoReservas", 1, codigo_alu, 0)
                Obj.CerrarConexion()
                Me.dlstCiclos.DataBind()
                If Me.dlstCiclos.Items.Count > 0 Then
                    'Me.cmdVer.Visible = True
                    Me.Panel1.Visible = True
                Else
                    Me.lblMensaje.Text = "No se ha registrado Historial Académico para el estudiante."
                End If
            End If
        Else
            Me.lblMensaje.Text = "El estudiante no existe en la Base de datos"
            Me.FotoAlumno.Visible = False
        End If
        Tbl.Dispose()
        Obj = Nothing

    End Sub
    Private Function PintarNota(ByVal minima As Double, ByVal nota As Double) As String
        If nota >= minima Then
            PintarNota = "<span class='azul'>" & nota & "</span>"
        Else
            PintarNota = "<span class='rojo'>" & nota & "</span>"
        End If
    End Function

    Protected Sub dlstCiclos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlstCiclos.ItemDataBound
        Dim EsReserva As Boolean = False
        If e.Item.DataItem("tipo_cac").ToString = "N" Then
            If e.Item.DataItem("codigo_sco") = 103 Then 'Reserva: NO HA ESTUDIADO EN EL CICLO Y PAGÓ
                CType(e.Item.FindControl("lblciclo"), Label).Text = e.Item.DataItem("descripcion_cac") & " - RESERVA DE MATRICULA (Cancelado)"
                EsReserva = True
            ElseIf e.Item.DataItem("estadoMatriculado").ToString = "R" Then 'servicio nulo indica no pagado
                CType(e.Item.FindControl("lblciclo"), Label).Text = e.Item.DataItem("descripcion_cac") & " - RESERVA DE MATRICULA (No Cancelado)"
                EsReserva = True
            End If

            'Si paga Matrícula o el servicio 124 y se inscribe en cursos (opc.) se considera como RETIRO DE CICLO
            If e.Item.DataItem("estadoMatriculado").ToString <> "N" And (e.Item.DataItem("codigo_sco") = 124 Or e.Item.DataItem("codigo_sco") = 30) Then

                If e.Item.DataItem("descripcion_cac") = "2011-I" Then
                    CType(e.Item.FindControl("lblciclo"), Label).Text = e.Item.DataItem("descripcion_cac") & " - MATRICULA (Cancelado)"
                    EsReserva = False
                Else
                    CType(e.Item.FindControl("lblciclo"), Label).Text = e.Item.DataItem("descripcion_cac") & " - RETIRO DE CICLO (Cancelado)"
                    EsReserva = False
                End If
            End If
        End If

        If EsReserva = False Then
            Dim gr As GridView
            gr = CType(e.Item.FindControl("gridView1"), GridView)

            'Dim obj As New ClsSqlServer(ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString)
            Dim Obj As New ClsConectarDatos
            Obj.CadenaConexion = ConfigurationManager.ConnectionStrings("cnxBDUSAT").ConnectionString
            Obj.AbrirConexion()
            gr.DataSource = Obj.TraerDataTable("ConsultarHistorialAlumnoReservas", 2, Session("codigo_alu"), Me.dlstCiclos.DataKeys(e.Item.ItemIndex))
            Obj.CerrarConexion()
            gr.DataBind()
            Obj = Nothing
        End If
    End Sub
    Protected Sub VerificarMatricula(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView=e.Row.DataItem

            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(9).Attributes.Add("onclick", "AbrirPopUp('../../personal/academico/estudiante/frmdetalles.asp?codigo_dma=" & fila.Item("codigo_dma").ToString & "&codigouniver_alu=" & Me.lblcodigo.Text & "','450','650')")

            '*******************************************
            'Asignar leyenda según el tipo de Matrícula
            '*******************************************
            Select Case fila.Item("tipomatricula_dma")
                Case "C" : e.Row.Cells(2).Text = fila.Item("nombre_cur") & "*"
                Case "U" : e.Row.Cells(2).Text = fila.Item("nombre_cur") & "**"
                Case "S" : e.Row.Cells(2).Text = fila.Item("nombre_cur") & "***"
                Case "R" : e.Row.Cells(2).Text = fila.Item("nombre_cur") & "****"
            End Select

            '*******************************************
            'No mostrar veces desaprobadas =0
            '*******************************************
            If iif(fila.Item("vecesCurso_DmaUlt") Is dbnull.value, 0, fila.Item("vecesCurso_DmaUlt")) = 0 Then
                e.Row.Cells(6).Text = ""
            End If

            '*******************************************
            'No mostrar notas cuando no se ha llenado el registro
            '*******************************************
            If fila.Item("estadonota_cup") = "P" And fila.Item("estado_dma") = "R" Then
                e.Row.Cells(6).Text = "-"
            End If

            e.Row.Cells(7).CssClass = fila.Item("condicion_dma")

            If fila.Item("estado_dma") = "R" Then
                e.Row.Cells(8).Text = "Retirado"
            End If

            '*******************************************
            'Almacenar valores sin tener en cuenta convalidaciones ni retiros
            '*******************************************
            ' If fila.Item("tipomatricula_dma") <> "C" And fila.Item("estado_dma") <> "R" Then  ** Modificado para tomar en cuenta cursos convalidados
            'If fila.Item("estado_dma") <> "R" Then
            If fila.Item("estado_dma") <> "R" And _
                (fila.Item("tipomatricula_dma") = "N" Or fila.Item("tipomatricula_dma") = "A") Then
                crd += CDbl(fila.Item("creditocur_dma"))
                notacrd += CDbl(fila.Item("notacredito"))
            End If

            '*******************************************
            'Almacenar valores Matriculados+Aprobados
            '*******************************************
            If fila.Item("condicion_dma") = "A" Then
                If fila.Item("tipomatricula_dma") <> "C" And fila.Item("estado_dma") <> "R" Then
                    crdAR += CDbl(fila.Item("creditocur_dma"))
                    AAR = AAR + 1
                ElseIf fila.Item("tipomatricula_dma") = "C" And fila.Item("estado_dma") <> "R" Then
                    crdAC += CDbl(fila.Item("creditocur_dma"))
                    AAC = AAC + 1
                End If
            End If

        ElseIf e.Row.RowType = DataControlRowType.Footer Then
            e.Row.Cells(3).Text = "TOTAL"
            e.Row.Cells(4).Text = crd
            If crd > 0 Then
                e.Row.Cells(7).Text = FormatNumber(notacrd / crd, 2)
            End If

            e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Center
            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Center
            crd = 0
            notacrd = 0
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Me.lblAsigAprob.Text = AAR.ToString
        Me.lblAsigAprobC.Text = AAC.ToString
        Me.lblCrdAprob.Text = crdAR.ToString
        Me.lblCrdAprobC.Text = crdAC.ToString
        Me.lblCrd.Text = Int(Me.lblCrdAprob.Text) + Int(Me.lblCrdAprobC.Text)
        Me.lblAsig.Text = Int(Me.lblAsigAprob.Text) + Int(Me.lblAsigAprobC.Text)
    End Sub
    'Protected Sub cmdVer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdVer.Click
    'Response.Redirect("historialcc.aspx?id=" & Request.QueryString("id"))
    'End Sub
End Class