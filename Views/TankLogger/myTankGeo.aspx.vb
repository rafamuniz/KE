Public Class tankgeo
    Inherits System.Web.UI.Page
    Dim oLog As clsLogdata
    Dim oFormula As clsVolFormula
    Dim oTank As clsTank

    Protected Property lat As Double = 0
    Protected Property lon As Double = 0
    Protected Property tankname As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            ' default to the tank selected if there was one
            Try ' to get the querystring
                lblTankID.Text = Request.QueryString("id")
            Catch ex As Exception
                ' error - fatal
                Response.Redirect("listtanks.aspx")
            End Try


            If CInt(lblTankID.Text) > 0 Then
                ' fill the page with the default tank
                ShowTank(CInt(lblTankID.Text))
            End If

        End If

    End Sub

    Protected Sub ShowTank(ByVal tankid As Integer)
        If oLog Is Nothing Then
            oLog = New clsLogdata
        End If
        If oFormula Is Nothing Then
            oFormula = New clsVolFormula
        End If
        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        ' get the details of this tank
        If oTank.GetTank(tankid) Then
            ' move some tank details to the page
            lblTankName.Text = oTank.oSPOT_Asset.sName

            ' plot it on the map (use address if lat/lon not available)
            ' convert N/S and E/W to +/-
            If IsNumeric(oTank.oSPOT_Asset.fLatitude) And IsNumeric(oTank.oSPOT_Asset.fLongitude) Then
                lat = oTank.oSPOT_Asset.fLatitude
                lon = oTank.oSPOT_Asset.fLongitude
                tankname = oTank.oSPOT_Asset.sName
            End If

        Else
            'error - tank not found
        End If

    End Sub


End Class