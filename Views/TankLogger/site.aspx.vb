Public Class site
    Inherits System.Web.UI.Page
    Dim oUSer As clsUser
    Dim oSite As clsSite


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
        If oSite Is Nothing Then
            oSite = New clsSite
        End If

        If oSite.getSite(id) Then
            lblSite.Text = id

            With oSite.oElements
                tbSiteCode.Text = .siteCode
                tbCustID.Text = .siteCustID
                tbName.Text = .siteName
                tbContact.Text = .siteContact
                ddlStatus.Items.FindByValue(.siteStatus).Selected = True
                txtSPOT_ID.Text = .siteSPOT_ID
            End With

            With oSite.oAddr
                tbAddr1.Text = .adAddr1
                tbAddr2.Text = .adAddr2
                tbCity.Text = .adCity
                ddlState.Items.FindByValue(.adSt).Selected = True
                tbZip.Text = .adZip
                tbPhone.Text = .adPh1
                tbemail.Text = .adEmail
            End With

        End If

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If oSite Is Nothing Then
            oSite = New clsSite
        End If

        With oSite.oElements
            .siteName = tbName.Text
            .siteCustID = tbCustID.Text
            .siteCode = tbSiteCode.Text
            .siteContact = tbContact.Text
            .siteStatus = ddlStatus.SelectedValue
            .siteSPOT_ID = txtSPOT_ID.Text
        End With

        With oSite.oAddr
            .adAddr1 = tbAddr1.Text
            .adAddr2 = tbAddr2.Text
            .adCity = tbCity.Text
            .adSt = ddlState.SelectedValue
            .adZip = tbZip.Text
            .adPh1 = tbPhone.Text
            .adEmail = tbemail.Text
        End With

        If lblSite.Text = "" Then
            If Not oSite.AddSite() Then
                ' error
            End If
        Else

            If Not oSite.updSite(CLng(lblSite.Text)) Then
                ' error
            End If
        End If
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub
End Class