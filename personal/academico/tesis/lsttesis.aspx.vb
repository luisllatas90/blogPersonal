Imports System.IO

Partial Class lsttesis
    Inherits System.Web.UI.Page
    Dim xCar As Integer


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'div.innerHTML = "<a href='frmtesis.aspx?accion=A&codigo_tes=0&mod=" & Request.QueryString("mod") & "&ctf=" & Request.QueryString("ctf") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=450&width=650&modal=true' title='Registrar Trabajos de Investigación' class='thickbox'>Registrar Tesis<a/>"
        If (Session("id_per") Is Nothing) Then
            Response.Redirect("../../../sinacceso.html")
        End If

        If IsPostBack = False Then
            Dim tblescuela As Data.DataTable
            Dim tbCicloAcademico As Data.DataTable      'agregado 15/11/2011

            Dim modulo, ctf, codigo_usu As Integer
            modulo = Request.QueryString("mod")
            ctf = Request.QueryString("ctf")
            codigo_usu = Request.QueryString("id")

            'ClsFunciones.LlenarListas(Me.dpEstado, obj.TraerDataTable("TES_ConsultarEtapaInvestigacionTesis", 1, 0, 0, 0), "codigo_Ein", "descripcion_Ein")
            Dim obj As New ClsConectarDatos
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            '### comentado por hreyes 13/01/2011 ###
            'If Request.QueryString("ctf") = 1 Then
            '    tblescuela = obj.TraerDataTable("ConsultarCarreraProfesional", "MA", 0)
            '    ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf")
            'Else
            '    'tblescuela = obj.TraerDataTable("consultaracceso", "ESC", "", Request.QueryString("id"))
            '    tblescuela = obj.TraerDataTable("eve_ConsultarCarreraProfesional", modulo, ctf, codigo_usu)
            '    ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf")
            'End If
            ' ######################################
            tblescuela = obj.TraerDataTable("eve_ConsultarCarreraProfesional", modulo, ctf, codigo_usu)
            ClsFunciones.LlenarListas(Me.dpEscuela, tblescuela, "codigo_cpf", "nombre_cpf")

            '### Cargar ciclo académico ###
            'Modificado 15/12/2011 para cargar también la opción Todos
            tbCicloAcademico = obj.TraerDataTable("ConsultarCicloAcademico", "TO2", 0)
            ClsFunciones.LlenarListas(Me.ddlCiclo, tbCicloAcademico, "codigo_cac", "descripcion_cac")

            obj.CerrarConexion()
            cmdReporte.Enabled = False
            Me.dpEscuela.SelectedIndex = 0
        End If


    End Sub
    Protected Sub grwListaTesis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grwListaTesis.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim fila As Data.DataRowView
            fila = e.Row.DataItem
            'e.Row.Attributes.Add("id", "" & fila.Row("codigo_tes").ToString & "")
            'e.Row.Attributes.Add("Class", "Sel")
            'e.Row.Attributes.Add("Typ", "Sel")
            'e.Row.Attributes.Add("OnClick", "HabilitarBoton('M',this)")
            e.Row.Attributes.Add("OnMouseOver", "Resaltar(1,this,'S')")
            e.Row.Attributes.Add("OnMouseOut", "Resaltar(0,this,'S')")
            e.Row.Cells(4).Text = "<a href='detalleetapatesis.aspx?codigo_tes=" & fila.Row("codigo_tes") & "&codigo_per=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=400&width=650&modal=true' title='Cambiar estado' class='thickbox'>" & fila.Row("nombre_eti") & "&nbsp;<img src='../../../images/menu6.gif' border=0 /><a/>"
            e.Row.Cells(6).Text = "<a href='asignarinvolucrado.aspx?codigo_tes=" & fila.Row("codigo_tes") & "&id=" & Request.QueryString("id") & "&KeepThis=true&TB_iframe=true&height=400&width=650&modal=true' title='Asignar Asesores / Jurado' class='thickbox'><img src='../../../images/contargrupo.gif' border=0 /><a/>"
            e.Row.Cells(7).Text = "<a href='frmtesis.aspx?accion=M&cac=" & fila.Row("codigo_cac") & "&cpf=" & fila.Row("codigo_cpf") & "&codigo_tes=" & fila.Row("codigo_tes") & "&id=" & Request.QueryString("id") & "&ctf=" & Request.QueryString("ctf") & "&KeepThis=true&TB_iframe=true&height=500&width=650&modal=true' title='Modificar Trabajos de Investigación' class='thickbox'><img src='../../../images/editar.gif' border=0 /><a/>"
            e.Row.Cells(0).Text = e.Row.RowIndex + 1


            Dim curFile As String = Server.MapPath("../../../filesTesis/" & fila.Row("codigo_tes") & ".pdf")
            Dim texto As String = "No"
            If (File.Exists(curFile)) Then
                texto = "<a href='../../../filesTesis/" & fila.Row("codigo_tes") & ".pdf' target='_blank'>Ver</a>"
            End If
            e.Row.Cells(10).Text = texto

            'Cargar Autores de la Tesis
            Dim obj As New ClsConectarDatos
            Dim grw As BulletedList
            obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
            obj.AbrirConexion()
            grw = CType(e.Row.FindControl("bAutores"), BulletedList)

            grw.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 9, fila.Row("codigo_tes"), 0, 0)
            grw.DataBind()
            'Cargar Autores de la Tesis
            grw = CType(e.Row.FindControl("bAsesores"), BulletedList)
            grw.DataSource = obj.TraerDataTable("TES_ConsultarResponsableTesis", 10, fila.Row("codigo_tes"), 4, 7)
            grw.DataBind()
            obj.TerminarTransaccion()
            obj = Nothing


        End If
    End Sub
    Protected Sub CmdBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Dim obj As New ClsConectarDatos
        obj.CadenaConexion = ConfigurationManager.ConnectionStrings("CNXBDUSAT").ConnectionString
        obj.AbrirConexion()

        'Response.Write(Me.cboPor.SelectedValue)
        'Response.Write(ddlCiclo.SelectedValue)

        'Me.grwListaTesis.DataSource = obj.TraerDataTable("TES_ConsultarTesis", Me.cboPor.SelectedValue, Me.txtbusqueda.Text.Trim, Me.cboPor.SelectedValue, Me.dpEscuela.SelectedValue)


        Me.grwListaTesis.DataSource = obj.TraerDataTable("TES_ConsultarTesis_v3", Me.cboPor.SelectedValue, Me.txtbusqueda.Text.Trim, Me.cboPor.SelectedValue, Me.dpEscuela.SelectedValue, ddlCiclo.SelectedValue)

        'Response.Write("Por " & Me.cboPor.SelectedValue & "</br>")
        ' Response.Write("txtbus " & Me.txtbusqueda.Text.Trim & "</br>")
        ' Response.Write("cbo por" & Me.cboPor.SelectedValue & "</br>")
        'Response.Write("escuela " & Me.dpEscuela.SelectedValue & "</br>")
        ' Response.Write("ciclo " & Me.ddlCiclo.SelectedValue & "</br>")

        'Por titulo  = 9
        'Por autor = 10
        'Por asesor = 11
        'Por Jurado = 12

        Me.cmdReporte.Enabled = True
        Me.cmdReporte.OnClientClick = "AbrirPopUp('rptListaTesisExcel.aspx?mod=" & Me.dpEscuela.SelectedValue & "&Opt=" & ddlCiclo.SelectedValue & "&Por=" & Me.cboPor.SelectedValue & "&text=" & Me.txtbusqueda.Text.Trim & "  ','600','800','yes','yes','yes','yes')"
        Me.grwListaTesis.DataBind()
        obj.CerrarConexion()
        obj = Nothing
    End Sub

    Protected Sub cmdReporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdReporte.Click
        Me.cmdReporte.OnClientClick = "AbrirPopUp('rptListaTesisExcel.aspx?mod=" & Me.dpEscuela.SelectedValue & "&Opt=" & ddlCiclo.SelectedValue & "&Por=" & Me.cboPor.SelectedValue & "&text=" & Me.txtbusqueda.Text.Trim & "  ','600','800','yes','yes','yes','yes')"
    End Sub

    Protected Sub dpEscuela_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpEscuela.SelectedIndexChanged
        Me.cmdReporte.OnClientClick = "AbrirPopUp('rptListaTesisExcel.aspx?mod=" & Me.dpEscuela.SelectedValue & "&Opt=" & ddlCiclo.SelectedValue & "&Por=" & Me.cboPor.SelectedValue & "&text=" & Me.txtbusqueda.Text.Trim & "  ','600','800','yes','yes','yes','yes')"
        cmdReporte.Enabled = False
    End Sub

    Protected Sub ddlCiclo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCiclo.SelectedIndexChanged
        Me.cmdReporte.OnClientClick = "AbrirPopUp('rptListaTesisExcel.aspx?mod=" & Me.dpEscuela.SelectedValue & "&Opt=" & ddlCiclo.SelectedValue & "&Por=" & Me.cboPor.SelectedValue & "&text=" & Me.txtbusqueda.Text.Trim & "  ','600','800','yes','yes','yes','yes')"
    End Sub


End Class
