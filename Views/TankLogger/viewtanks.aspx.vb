Public Class viewtanks
    Inherits System.Web.UI.Page
    Dim oUser As clsUser
    Dim oSite As clsSite
    Dim oTank As clsTank
    Dim oFormula As clsVolFormula
    Dim oLog As clsLogdata


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
        ' set page refresh rate
        'Dim meta As New HtmlMeta
        'meta.HttpEquiv = "Refresh"
        'meta.Content = ConfigurationManager.AppSettings("tankpagerefresh")
        'MetaPlaceHolder.Controls.Add(meta)

        ' fill tree view
        FillSiteTree(oUser.Prefs.iusrID)

        If Not FillTanklist() Then
            ' error
            Return False
        End If

        Return True

    End Function
    Private Sub FillSiteTree(uid As Integer)

        If oSite Is Nothing Then
            oSite = New clsSite
        End If
        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        ddSiteList.Items.Add(New ListItem("My Sites", 0))

        'get each site for this user
        Dim sites As New DataSet
        sites = oSite.GetUserSites(uid)
        For Each siterow As DataRow In sites.Tables(0).Rows
            ''   add it to the dropdown level 2
            Dim sitemnunode As New ListItem
            sitemnunode.Text = siterow.Item("stName")
            sitemnunode.Value = siterow.Item("stID")
            ddSiteList.Items.Add(sitemnunode)

            '   get each tank for this site
            Dim tanks As New DataSet
            tanks = oTank.GetSiteTanks(siterow.Item("stID"), "S")
            For Each tankrow As DataRow In tanks.Tables(0).Rows
                ''       add it to the tree level 2
                Dim tankmnunode As New ListItem
                tankmnunode.Text = "-- " + tankrow("tdefName")
                tankmnunode.Value = tankrow("tdefID")
                ddSiteList.Items.Add(tankmnunode)
            Next

        Next

    End Sub
    Private Function FillTanklist() As Boolean
        If oUser Is Nothing Then
            oUser = New clsUser
        End If
        If oTank Is Nothing Then
            oTank = New clsTank
        End If
        If oLog Is Nothing Then
            oLog = New clsLogdata
        End If
        If oFormula Is Nothing Then
            oFormula = New clsVolFormula
        End If

        Try
            Dim tds As New DataSet
            tds = oTank.GetSiteTanks(oUser.Prefs.iusrID, "U")

            If oFormula.CurrStateToTanklist(tds.Tables(0)) Then
                dlistTanks.DataSource = tds
                dlistTanks.DataBind()
                'grdTanks.Columns(1).Visible = False
            Else
                ' error
                Return False
            End If

        Catch ex As Exception
            'error
            Return False
        End Try

        Return True

    End Function
    Private Sub dlistTanks_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles dlistTanks.ItemCommand
        If e.CommandName = "tankdetail" Then
            Response.Redirect("mytank.aspx?id=" & e.CommandArgument)
        End If
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("modelpick.aspx")
    End Sub

    Private Sub ddSiteList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddSiteList.SelectedIndexChanged
        ' the list is expected to have a label at item(0)
        ' a list of sites and site tanks should follow with tanks preceeded by "-- "

        If ddSiteList.SelectedIndex > 0 Then
            If Left(ddSiteList.Items(ddSiteList.SelectedIndex).Text, 3) = "-- " Then
                ' assume a tank was selected
                Response.Redirect("mytank.aspx?id=" & ddSiteList.SelectedValue)
            Else
                ' assume a site was selected

            End If
        End If
    End Sub
End Class