
Partial Class CtrlFotoAlumno
    Inherits System.Web.UI.UserControl
    Private CodigoUniver As String
    Dim Obj As New EncriptaCodigos.clsEncripta

    Public Property CodigoUniversitario() As String
        Get
            Return CodigoUniver
        End Get
        Set(ByVal value As String)
            CodigoUniver = value
            Me.LblCodigo.Text = value
            MuestraFoto()
        End Set
    End Property

    Public WriteOnly Property Enlace() As String
        Set(ByVal value As String)
            If value.ToString <> "" Then
                Me.PanFoto.Attributes.Add("OnClick", value)
                Me.PanFoto.Attributes.Add("style", "cursor:hand")
                Me.PanFoto.Attributes.Add("OnMouseOver", "javascript:this.style.backgroundColor='#E4CE74'")
                Me.PanFoto.Attributes.Add("OnMouseOut", "javascript:this.style.backgroundColor='#FFFFFF'")
            End If
        End Set
    End Property

    Public WriteOnly Property Nombre() As String
        Set(ByVal value As String)
            Me.LblNombres.Text = value
        End Set
    End Property

    Private Sub MuestraFoto()
        Me.ImgFoto.ImageUrl = "http://www.usat.edu.pe/imgestudiantes/" & Obj.CodificaWeb("069" & CodigoUniver)
    End Sub

End Class
