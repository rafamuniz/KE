Public Class tanklogger
    Inherits System.Web.UI.MasterPage
    Dim oLogdata As clsLogdata
    Dim oUser As clsUser

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try ' see if user object is instatiated
            oUser = Session("oUser")

            If oUser Is Nothing Then ' do so if not
                oUser = New clsUser
                Session("oUser") = oUser
            End If

        Catch ex As Exception
            ' failed to instantiate user object
        End Try


        If Not Page.IsPostBack Then

            ' determine if logged on
            If oUser.Prefs.bAuthStat Then
                divLoggedOut.Visible = False
                divLoggedIn.Visible = True
                lbuserid.Text = "Hello " + oUser.Prefs.sUsrFName
            Else
                divLoggedOut.Visible = True
                divLoggedIn.Visible = False
                lbuserid.Text = "Not Logged in"
            End If

            ' display appropriate menu
            divadmin.Visible = False
            Dim sUsrType As String = "A"
            Select Case sUsrType
                Case "A"
                    divadmin.Visible = True
                Case Else

            End Select

        End If

    End Sub

    Protected Sub btnLogtank_Click(sender As Object, e As EventArgs) Handles btnLogtank.Click
        Response.Redirect("tanklogi.aspx")
    End Sub

    Protected Sub btnViewTanks_Click(sender As Object, e As EventArgs) Handles btnViewTanks.Click
        Response.Redirect("viewtanks.aspx")
    End Sub
    Private Sub btnViewReport_Click(sender As Object, e As EventArgs) Handles btnViewReport.Click
        Response.Redirect("listtanks.aspx")
    End Sub



    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If oUser Is Nothing Then
            oUser = New clsUser
        End If

        oUser.Prefs.sUID = tbEmail.Text
        oUser.Prefs.sPWD = tbPwd.Text
        oUser.Prefs.iusrID = 0
        oUser.Prefs.sRole = ""
        oUser.Prefs.sUsrFName = ""
        oUser.Prefs.bAuthStat = False

        If oUser.Logon() Then
            Session("oUSer") = oUser
            lbuserid.Text = "Hello " + oUser.Prefs.sUsrFName
            divLoggedIn.Visible = True
            divLoggedOut.Visible = False
        Else
            btnLogoff_Click(sender, e)
        End If

        lblMsg.Text = oUser.Prefs.sMessage

    End Sub


    Private Sub btnLogoff_Click(sender As Object, e As EventArgs) Handles btnLogoff.Click, lbLogoff.Click
        Session("oUser") = Nothing
        divLoggedIn.Visible = False
        divLoggedOut.Visible = True
        divadmin.Visible = False
        divuser.Visible = False
        Response.Redirect("logon.aspx")
    End Sub

    Private Sub btnSettings_Click(sender As Object, e As ImageClickEventArgs) Handles btnSettings.Click

    End Sub

    Private Sub btnSites_Click(sender As Object, e As EventArgs) Handles btnSites.Click
        Response.Redirect("sites.aspx")

    End Sub

    Private Sub btnCusts_Click(sender As Object, e As EventArgs) Handles btnCusts.Click
        Response.Redirect("customers.aspx")
    End Sub

    Private Sub btnUsers_Click(sender As Object, e As EventArgs) Handles btnUsers.Click
        Response.Redirect("users.aspx")
    End Sub

    Private Sub btnFastTracker_Click(sender As Object, e As EventArgs) Handles btnFastTracker.Click
        Response.Redirect("sitedashboard.aspx")
    End Sub
End Class