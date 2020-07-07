
Partial Class index2020
    Inherits System.Web.UI.Page

#Region "Declaracion de Variables"

    Private oeAccesoDatos As clsaccesodatos

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
            'Dim var As String
            ''var = Session("codigo_apl") & " - " & Session("codigo_tfu") & " - " & Session("descripcion_apl") & " - " & Session("descripcion_tfu")
            'If Request.Cookies("ck_cod_apl") Is Nothing Then
            '    var = "la cookie no existe"
            'Else
            '    var = "var = " & Request.Cookies("ck_cod_apl").Value
            'End If
            'Response.Write(Request("ctfu"))
            'For i As Integer = 0 To Session.Count - 1
            '    Response.Write(Session.Keys(i).ToString() & ": " & Session(i).ToString() & "<br>")
            'Next
            mt_CargarMenu()
        Catch ex As Exception
            mt_ShowMessage(ex.Message.Replace("'", " "), MessageType.Error)
        End Try
    End Sub


#End Region

#Region "Metodos"

    Protected Sub mt_ShowMessage(ByVal Message As String, ByVal type As MessageType)
        Page.RegisterStartupScript("Mensaje", "<script>ShowMessage('" & Message & "','" & type.ToString & "');</script>")
    End Sub

    Private Sub mt_CargarMenu()
        Dim dvMenu As literal
        Dim dtLv1 As New data.datatable, dtLv2 As New data.datatable
        Dim _tmLv1, _tmLv2, _icodigo, _jcodigo As Integer
        Dim _src, _descripcion, _enlace, _inivel, _jnivel As String
        Try

            oeAccesoDatos = New clsaccesodatos
            oeAccesoDatos.abrirconexion()
            dtLv1 = oeAccesoDatos.TraerDataTable("ConsultarAplicacionUsuario", "11", Session("gcodigo_apl"), Session("gcodigo_ctfu"), 0)
            oeAccesoDatos.cerrarconexion()
            oeAccesoDatos = Nothing

            dvMenu = New literal()

            dvMenu.text = "<div class='well' style='padding: 0px;'>"
            '    <!-- Menu -->
            dvMenu.text += "     <div class='side-menu'>"
            dvMenu.text += "         <nav class='navbar navbar-default' role='navigation'>"
            '            <!-- Brand and toggle get grouped for better mobile display -->
            dvMenu.text += "             <div class='navbar-header'>"
            dvMenu.text += "                 <div class='brand-wrapper'>"
            '                    <!-- Hamburger -->
            dvMenu.text += "                    <button type='button' class='navbar-toggle'>"
            'dvMenu.text += "                         <span class='sr-onl'></span>"
            dvMenu.text += "                         <span class='icon-bar'></span>"
            dvMenu.text += "                         <span class='icon-bar'></span>"
            dvMenu.text += "                         <span class='icon-bar'></span>"
            dvMenu.text += "                    </button>"
            '                    <!-- Brand -->
            dvMenu.text += "                   <div class='brand-name-wrapper'>"
            dvMenu.text += "                        <a class='navbar-brand' href='#'>"
            dvMenu.text += "                            " & Session("gdescri_apl")
            dvMenu.text += "                        </a>"
            dvMenu.text += "                    </div>"
            dvMenu.text += "                </div>"
            dvMenu.text += "            </div>"
            '            <!-- Main Menu -->
            dvMenu.text += "            <div class='side-menu-container'>"
            dvMenu.text += "                <ul class='nav navbar-nav'>"

            _tmLv1 = 0
            'Response.Write(Session("gcodigo_apl") & "-" & Session("gcodigo_ctfu"))
            For i As Integer = 0 To dtLv1.rows.count - 1
                _tmLv1 = dtLv1.rows(i).item("total_men")
                _icodigo = dtLv1.rows(i).item("codigo_men")
                _inivel = dtLv1.rows(i).item("nivel_men") & "-" & _icodigo
                If _tmLv1 > 0 Then
                    dvMenu.text += "                <li class='panel panel-default' id='dropdown'>"
                    dvMenu.text += "                    <a data-toggle='collapse' href='#dropdown-lvl" & _inivel & "'>"
                    dvMenu.text += "                        <img src='../images/menus/" & dtLv1.rows(i).item("icono_Men") & "' width='32px' height='32px'> " & dtLv1.rows(i).item("descripcion_Men") & "<span class='caret'></span>"
                    dvMenu.text += "                    </a>"
                    dvMenu.text += "                    <div id='dropdown-lvl" & _inivel & "' class='panel-collapse collapse'>"
                    dvMenu.text += "                        <div class='panel-body'>"
                    dvMenu.text += "                            <ul class='nav navbar-nav'>"

                    oeAccesoDatos = New clsaccesodatos
                    oeAccesoDatos.abrirconexion()
                    dtLv2 = oeAccesoDatos.TraerDataTable("ConsultarAplicacionUsuario", "11", Session("gcodigo_apl"), Session("gcodigo_ctfu"), dtLv1.rows(i).item("codigo_Men"))
                    oeAccesoDatos.cerrarconexion()
                    oeAccesoDatos = Nothing

                    _tmLv2 = 0
                    For j As Integer = 0 To dtLv2.rows.count - 1
                        _tmLv2 = dtLv2.rows(j).item("total_men")
                        _descripcion = dtLv2.rows(j).item("descripcion_Men")
                        _enlace = dtLv2.Rows(j).Item("enlace_men")
                        _jcodigo = dtLv2.rows(j).item("codigo_Men")
                        _jnivel = dtLv2.rows(j).item("nivel_men") & "-" & _jcodigo
                        mt_CargarSubMenu(_tmLv2, dvMenu, _descripcion, _enlace, _jnivel, _jcodigo)
                        'If _tmLv2 > 0 Then
                        '    dvMenu.text += "                            <li class='panel panel-default' id='dropdown'>"
                        '    dvMenu.text += "                                <a data-toggle='collapse' href='#dropdown-lvl2-" & j + 1 & "'>"
                        '    dvMenu.text += "                                    <span class='fa fa-book'></span> " & dtLv2.rows(j).item("descripcion_Men") & " <span class='caret'></span>"
                        '    dvMenu.text += "                                </a>"
                        '    dvMenu.text += "                            </li>"
                        'Else
                        '    _enlace = "fc_AbrirPagina('" & dtLv2.Rows(j).Item("enlace_men") & "?id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                        '    '_enlace = "fc_AbrirPagina('../" & dtLv2.Rows(j).Item("enlace_men") & "')"
                        '    dvMenu.text += "                            <li>"
                        '    dvMenu.text += "                                <a onclick = """ & _enlace & """ href='#'>"
                        '    dvMenu.text += "                                    <span class='fa fa-file-alt'></span> " & dtLv2.rows(j).item("descripcion_Men")
                        '    dvMenu.text += "                                </a>"
                        '    dvMenu.text += "                            </li>"
                        'End If
                    Next
                    dvMenu.text += "                            </ul>"
                    dvMenu.text += "                        </div>"
                    dvMenu.text += "                    </div>"
                    dvMenu.text += "                </li>"
                Else
                    If InStr(dtLv1.Rows(i).Item("enlace_men"), "../rptusat/?/") > 0 Then
                        _src = "fc_AbrirPagina('" & dtLv1.Rows(i).Item("enlace_men") & "&id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                    Else
                        If InStr(dtLv1.Rows(i).Item("enlace_men"), "?") > 0 Then
                            _src = "fc_AbrirPagina('" & dtLv1.Rows(i).Item("enlace_men") & "&id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                        Else
                            _src = "fc_AbrirPagina('" & dtLv1.Rows(i).Item("enlace_men") & "?id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                        End If
                    End If
                    '_enlace = "fc_AbrirPagina('../" & dtLv1.Rows(i).Item("enlace_men") & "')"
                    dvMenu.text += "                <li>"
                    dvMenu.text += "                    <a onclick = """ & _src & """ href='#'>"
                    dvMenu.text += "                        <img src='../images/menus/" & dtLv1.rows(i).item("icono_Men") & "' width='32px' height='32px'> " & dtLv1.rows(i).item("descripcion_Men")
                    dvMenu.text += "                    </a>"
                    dvMenu.text += "                </li>"
                End If
            Next

            dvMenu.text += "                </ul>"
            dvMenu.text += "            </div>" '<!-- /.navbar-collapse -->
            dvMenu.text += "         </nav>"
            dvMenu.text += "     </div>"
            dvMenu.text += "</div>"
            Me.divMenu.controls.add(dvMenu)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub mt_CargarSubMenu(ByVal _tot_men As Integer, ByVal _div As literal, ByVal _descripcion_men As String, ByVal _enlace_men As String, ByVal _item As String, ByVal _codigo_men As Integer)
        Dim dtLv As New data.datatable
        Try
            Dim _src, _descripcion, _enlace, _nivel, _xnivel As String
            Dim _tot, _codigo As Integer
            _xnivel = _item
            If _tot_men > 0 Then
                _div.text += "                            <li class='panel panel-default' id='dropdown'>"
                _div.text += "                                <a data-toggle='collapse' href='#dropdown-lvl" & _xnivel & "'>"
                _div.text += "                                    <span class='fa fa-book'></span> " & _descripcion_men & " <span class='caret'></span>"
                _div.text += "                                </a>"
                _div.text += "                              <div id='dropdown-lvl" & _xnivel & "' class='panel-collapse collapse'>"
                _div.text += "                                  <div class='panel-body'>"
                _div.text += "                                      <ul class='nav navbar-nav'>"

                oeAccesoDatos = New clsaccesodatos
                oeAccesoDatos.abrirconexion()
                dtLv = oeAccesoDatos.TraerDataTable("ConsultarAplicacionUsuario", "11", Session("gcodigo_apl"), Session("gcodigo_ctfu"), _codigo_men)
                oeAccesoDatos.cerrarconexion()
                oeAccesoDatos = Nothing

                For b As Integer = 0 To dtLv.rows.count - 1
                    _tot = dtLv.rows(b).item("total_men")
                    _descripcion = dtLv.rows(b).item("descripcion_Men")
                    _enlace = dtLv.Rows(b).Item("enlace_men")
                    _codigo = dtLv.rows(b).item("codigo_Men")
                    _nivel = dtLv.rows(b).item("nivel_men") & "-" & _codigo
                    mt_CargarSubMenu(_tot, _div, _descripcion, _enlace, _nivel, _codigo)
                Next

                _div.text += "                                      </ul>"
                _div.text += "                                  </div>"
                _div.text += "                              </div>"
                _div.text += "                          </li>"

            Else
                If InStr(_enlace_men, "../rptusat/?/") > 0 Then
                    _src = "fc_AbrirPagina('" & _enlace_men & "&id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                Else
                    If InStr(_enlace_men, "?") > 0 Then
                        _src = "fc_AbrirPagina('" & _enlace_men & "&id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                    Else
                        _src = "fc_AbrirPagina('" & _enlace_men & "?id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                    End If
                End If
                '_src = "fc_AbrirPagina('" & _enlace_men & "?id=" & Session("id_per") & "&ctf=" & Session("gcodigo_ctfu") & "')"
                '_enlace = "fc_AbrirPagina('../" & dtLv2.Rows(j).Item("enlace_men") & "')"
                _div.text += "                            <li>"
                _div.text += "                                <a onclick = """ & _src & """ href='#'>"
                _div.text += "                                    <span class='fa fa-file-alt'></span> " & _descripcion_men
                _div.text += "                                </a>"
                _div.text += "                            </li>"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
