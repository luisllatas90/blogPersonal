
Partial Class FrmPopAviso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '----------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: yperez
        'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
        '----------------------------------------------------------------------

        Image1.ImageUrl = "https://intranet.usat.edu.pe/campusvirtual/librerianet/inscripcion/afiches/fiestaGala.gif"
    End Sub
End Class
