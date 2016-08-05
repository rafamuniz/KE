Public Class myTankWX
    Inherits System.Web.UI.Page
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
        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        ' get the details of this tank
        If oTank.GetTank(tankid) Then
            ' move some tank details to the page
            lblTankName.Text = oTank.Elements.Name

            ' get the weather at the tank location
            GetWeather(oTank.Elements.Zip)

        Else
            'error - tank not found
        End If

    End Sub
    Private Sub GetWeather(ByVal sZip As String)
        Try ' to get event weather

            Weather1.LocationId = sZip

        Catch ex As Exception

        End Try
    End Sub


End Class