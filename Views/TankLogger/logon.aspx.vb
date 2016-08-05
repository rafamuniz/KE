Public Class logon
    Inherits System.Web.UI.Page
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
            '' import any log files
            'If oLogdata Is Nothing Then
            '    oLogdata = New clsLogdata
            'End If
            'Dim numlogs = oLogdata.ImportLogFiles
            'If numlogs > 0 Then
            '    ' All (" & numlogs.ToString & ") logs were imported."
            'Else
            '    ' All log files are up-to-date."
            'End If



        End If

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

            ' determine if logged on
            If oUser.Prefs.bAuthStat Then
                ' show dashboard
                Response.Redirect("viewtanks.aspx")
            End If

        Else
            Session("oUser") = Nothing
            lblMessage.Text = oUser.Prefs.sMessage
        End If

    End Sub


End Class