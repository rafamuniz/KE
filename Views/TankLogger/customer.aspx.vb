Public Class customer
    Inherits System.Web.UI.Page
    Dim oUSer As clsUser
    Dim oCust As clsCust

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

        'if site id passed, fill fields
        If Request.QueryString.HasKeys AndAlso Request.QueryString("id") IsNot Nothing Then

            'do something
            fillfields(CLng(Request.QueryString("id")))
        End If

        Return True
    End Function
    Private Sub fillfields(ByVal id As String)
        ' get the site record
        If oCust Is Nothing Then
            oCust = New clsCust
        End If

        If oCust.getCust(id) Then
            lblCust.Text = id

            With oCust.oElements
                tbName.Text = .custName
                tbContact.Text = .custContact
                ddlStatus.Items.FindByValue(.custStatus).Selected = True
            End With

            Try

                With oCust.oAddr
                    tbAddr1.Text = .adAddr1
                    tbAddr2.Text = .adAddr2
                    tbCity.Text = .adCity
                    ddlState.Items.FindByValue(.adSt).Selected = True
                    tbZip.Text = .adZip
                    tbPhone.Text = .adPh1
                    tbemail.Text = .adEmail
                End With
            Catch ex As Exception
                ' error couldn't find an address

            End Try

        End If

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If oCust Is Nothing Then
            oCust = New clsCust
        End If

        With oCust.oElements
            .custName = tbName.Text
            .custContact = tbContact.Text
            .custStatus = ddlStatus.SelectedValue
        End With

        With oCust.oAddr
            .adAddr1 = tbAddr1.Text
            .adAddr2 = tbAddr2.Text
            .adCity = tbCity.Text
            .adSt = ddlState.SelectedValue
            .adZip = tbZip.Text
            .adPh1 = tbPhone.Text
            .adEmail = tbemail.Text
        End With

        If lblCust.Text = "" Then
            If Not oCust.addCust() Then
                ' error
            End If
        Else

            If Not oCust.updCust(CLng(lblCust.Text)) Then
                ' error
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

End Class