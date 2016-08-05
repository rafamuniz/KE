Public Class customers
    Inherits System.Web.UI.Page
    Dim oUser As clsUser
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

        If Not FillCustlist() Then
            ' error
            Return False
        End If

        Return True

    End Function
    Private Function FillCustlist() As Boolean
        If oUser Is Nothing Then
            oUser = New clsUser
        End If
        If oCust Is Nothing Then
            oCust = New clsCust
        End If

        Try
            grdCustomers.DataSource = oCust.getCusts()
            grdCustomers.DataBind()

        Catch ex As Exception
            'error
            Return False
        End Try

        Return True

    End Function

    Private Sub grdCustomers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdCustomers.PageIndexChanging
        grdCustomers.PageIndex = e.NewPageIndex
        If Not LoadPage() Then
            ' error
        End If

    End Sub

    Private Sub grdCustomers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCustomers.RowCommand
        If e.CommandName = "cmdSelected" Then
            grdCustomers.SelectedIndex = e.CommandArgument
            Response.Redirect("customer.aspx?id=" & grdCustomers.SelectedDataKey.Value)

        End If
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("customer.aspx")
    End Sub

End Class