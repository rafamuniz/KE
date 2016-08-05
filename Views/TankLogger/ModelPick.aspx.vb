Public Class ModelPick
    Inherits System.Web.UI.Page
    Dim otank As clsTank

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillGallery()

        End If

    End Sub
    Private Sub FillGallery()
        If otank Is Nothing Then
            otank = New clsTank
        End If

        ' add the datasource to the gallery gridview
        dlModels.DataSource = otank.GetModels()
        dlModels.DataBind()

    End Sub

    Protected Sub dlModels_ItemCommand(ByVal sender As Object, ByVal e As DataListCommandEventArgs)
        If e.CommandSource.CommandName = "cmdImgChoice" Then
            ' Add code here to add the item to the shopping cart.
            ' Use the value of e.Item.ItemIndex to retrieve the data 
            ' item in the control.
            Dim sID As Label = dlModels.Items(e.Item.ItemIndex).FindControl("lblModelID")
            Dim sDesc As Label = dlModels.Items(e.Item.ItemIndex).FindControl("lblModelDesc")

            Response.Redirect("addtank.aspx?id=0&mdlid=" & sID.Text.ToString)
        End If

    End Sub


End Class