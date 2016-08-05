﻿Public Class users
    Inherits System.Web.UI.Page
    Dim oUser As clsUser
    Dim oCust As clsCust
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

        ' fill tree view
        FillCustTree(oUser.Prefs.iusrID)

        If Not FillUserlist() Then
            ' error
            Return False
        End If

        Return True

    End Function
    Private Sub FillCustTree(uid As Integer)

        If oCust Is Nothing Then
            oCust = New clsCust
        End If

        'get a list of customers
        Dim custs As New DataSet
        custs = oCust.getCusts()
        For Each custrow As DataRow In custs.Tables(0).Rows
            ddCustList.Items.Add(New ListItem(custrow("custName"), custrow("custID")))
        Next

        ddCustList.SelectedIndex = 0


    End Sub
    Private Function FillUserlist() As Boolean
        If oUser Is Nothing Then
            oUser = New clsUser
        End If

        Try
            grdUsers.DataSource = oUser.getUsers(ddCustList.SelectedValue)
            grdUsers.DataBind()

        Catch ex As Exception
            'error
            Return False
        End Try

        Return True

    End Function
    Private Sub grdUsers_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grdUsers.PageIndexChanging
        grdUsers.PageIndex = e.NewPageIndex
        If Not LoadPage() Then
            ' error
        End If

    End Sub
    Private Sub grdUsers_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdUsers.RowCommand
        If e.CommandName = "cmdSelected" Then
            grdUsers.SelectedIndex = e.CommandArgument
            Response.Redirect("user.aspx?id=" & grdUsers.SelectedDataKey.Value)

        End If
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("user.aspx")
    End Sub
    Private Sub ddCustList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddCustList.SelectedIndexChanged
        FillUserlist()
    End Sub

End Class