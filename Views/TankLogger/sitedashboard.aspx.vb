Imports System.Web.UI.DataVisualization.Charting
Imports System.Web.Configuration

Public Class sitedashboard
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

        If Not FillTanklist() Then
            ' error
            Return False
        End If

        Return True

    End Function
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

    Private Sub dlistTanks_ItemDataBound(sender As Object, e As DataListItemEventArgs) Handles dlistTanks.ItemDataBound
        ' add the graph data for this item
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rangechart As Chart = e.Item.FindControl("RangeChart")
            Dim ds As New DataSet

            Dim hfKey As HiddenField = e.Item.FindControl("hfTankID")

            ds = oLog.getGraphData(hfKey.Value, "R")

            If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                ' range
                rangechart.DataSource = ds
                rangechart.DataBind()

                'rangechart.Titles.Add("RangeTitle").Text = "Tank Volume Chart (gals.)"
                rangechart.Series("RangeSeries").YValueMembers = "wVol"
                rangechart.Series("RangeSeries").XValueMember = "SampleTime"
                rangechart.Series("RangeSeries").BorderWidth = 3

            End If
        End If
    End Sub

    Private Sub ddMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddMenu.SelectedIndexChanged
        Select Case ddMenu.SelectedValue
            Case "Main"
                Response.Redirect("viewtanks.aspx")

            Case "Logoff"
                Session("oUser") = Nothing
                Response.Redirect("logon.aspx")

            Case "Settings"

            Case Else

        End Select
    End Sub
End Class