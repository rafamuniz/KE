Public Class user
    Inherits System.Web.UI.Page
    Dim oCust As clsCust
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

            If oUser.Prefs.bAuthStat = True Then
                If oUser.IsAuthorized() Then
                    ' show mgr page
                    If Not LoadPage() Then
                        ' error occured loading page
                    End If
                Else
                    Response.Redirect("notallowed.aspx")
                End If
            Else
                Response.Redirect("notallowed.aspx")
            End If
        End If

    End Sub
    Private Function LoadPage() As Boolean

        ' fill dropdowns
        clsGeneral.FillDD(ddlState, "States")
        ddlState.Items.Insert(0, New ListItem("select", "select"))

        'if  id passed, fill fields
        If Request.QueryString.HasKeys AndAlso Request.QueryString("id") IsNot Nothing Then

            'do something
            fillfields(CLng(Request.QueryString("id")))
        End If

        Return True
    End Function
    Private Sub fillfields(ByVal id As String)
        ' get the user record
        If oUser Is Nothing Then
            oUser = New clsUser
        End If

        If oUser.getUser(id) Then
            lblUser.Text = id

            With oUser.oElements
                tbFName.Text = .usrFName
                tbLName.Text = .usrLName
                tbEmail.Text = .usrEmail
                tbPwd.Text = .usrPwd
                tbCustID.Text = .usrDefaultCustID
                ddlRole.Items.FindByValue(.usrRole).Selected = True
                ddlStatus.Items.FindByValue(.usrStatus).Selected = True
            End With

            With oUser.oAddr
                tbAddr1.Text = .adAddr1
                tbAddr2.Text = .adAddr2
                tbCity.Text = .adCity
                ddlState.Items.FindByValue(.adSt).Selected = True
                tbZip.Text = .adZip
                tbPhone.Text = .adPh1
                tbEmail.Text = .adEmail
            End With

        End If

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If oUser Is Nothing Then
            oUser = New clsUser
        End If

        With oUser.oElements
            .usrFName = tbFName.Text
            .usrLName = tbLName.Text
            .usrDefaultCustID = tbCustID.Text
            .usrEmail = tbEmail.Text
            .usrPwd = tbPwd.Text
            .usrStatus = ddlStatus.SelectedValue
            .usrRole = ddlRole.SelectedValue
        End With

        With oUser.oAddr
            .adAddr1 = tbAddr1.Text
            .adAddr2 = tbAddr2.Text
            .adCity = tbCity.Text
            .adSt = ddlState.SelectedValue
            .adZip = tbZip.Text
            .adPh1 = tbPhone.Text
            .adEmail = tbEmail.Text
        End With

        If lblUser.Text = "" Then
            If Not oUser.AddUser() Then
                ' error
            End If
        Else

            If Not oUser.updUser(CLng(lblUser.Text)) Then
                ' error
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

End Class