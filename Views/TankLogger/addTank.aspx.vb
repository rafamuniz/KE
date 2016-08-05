Imports System.IO
Public Class addTank
    Inherits System.Web.UI.Page
    Private oUser As clsUser
    Private oTank As clsTank

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

        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        If Not Page.IsPostBack Then
            If Not Request.QueryString Is Nothing Then
                If Not Request.QueryString("id") Is Nothing Then
                    tbDefID.Text = Request.QueryString("id")
                End If
                If Not Request.QueryString("mdlid") Is Nothing Then
                    tbModel.Text = Request.QueryString("mdlid")
                End If

                FillDDLs()

                ' if tankid = 0 and tankmodel given assume add mode

                If CInt(clsGeneral.IsZed(tbDefID.Text)) = 0 And CInt(clsGeneral.IsZed(tbModel.Text)) > 0 Then
                    LoadModelImage(CInt(clsGeneral.IsZed(tbModel.Text)), True)
                ElseIf CInt(clsGeneral.IsZed(tbDefID.Text)) > 0 Then
                    GetTank(CInt(clsGeneral.IsZed(tbDefID.Text)))
                    LoadModelImage(oTank.Elements.Model, False)
                Else
                    ' error
                End If

            Else
                'error
            End If
        End If
    End Sub
    Protected Sub FillDDLs()

    End Sub
    Protected Sub LoadModelImage(ByVal mdl As Integer, Optional ByVal withdefaults As Boolean = False)

        ' get tank model image and defaults
        oTank.MdlElem.ID = mdl
        If oTank.GetTankModel(oTank.MdlElem.ID) Then
            imgModel.ImageUrl = "images/" & oTank.MdlElem.ImageDim

            If oTank.MdlElem.HtDesc <> "" Then
                lblDimDesc1.Visible = True
                tbDim1.Visible = True
                lblDimDesc1.Text = oTank.MdlElem.HtDesc
                If withdefaults Then
                    tbDim1.Text = oTank.MdlElem.DftHt
                End If
            End If

            If oTank.MdlElem.LenDesc <> "" Then
                lblDimDesc2.Visible = True
                tbDim2.Visible = True
                lblDimDesc2.Text = oTank.MdlElem.LenDesc
                If withdefaults Then
                    tbDim2.Text = oTank.MdlElem.DftLen
                End If
            End If

            If oTank.MdlElem.WidthDesc <> "" Then
                lblDimDesc3.Visible = True
                tbDim3.Visible = True
                lblDimDesc3.Text = oTank.MdlElem.WidthDesc
                If withdefaults Then
                    tbDim3.Text = oTank.MdlElem.DftWidth
                End If
            End If

            If oTank.MdlElem.Dim4Desc <> "" Then
                lblDimDesc4.Visible = True
                tbDim4.Visible = True
                lblDimDesc4.Text = oTank.MdlElem.Dim4Desc
                If withdefaults Then
                    tbDim4.Text = oTank.MdlElem.DftDim4
                End If
            End If

            If oTank.MdlElem.Dim5Desc <> "" Then
                lblDimDesc5.Visible = True
                tbDim5.Visible = True
                lblDimDesc5.Text = oTank.MdlElem.Dim5Desc
                If withdefaults Then
                    tbDim5.Text = oTank.MdlElem.DftDim5
                End If
            End If

            If withdefaults Then


                tbModel.Text = oTank.MdlElem.ID.ToString
            End If

        End If


    End Sub
    Protected Sub GetTank(ByVal id As Integer)
        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        If oTank.GetTank(id) Then
            ' set tank model
            tbModel.Text = oTank.Elements.Model

            ' fill fields
            With oTank.Elements
                tbLogID.Text = .SensorID

                ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(.CustID))
                ddlSite.SelectedIndex = ddlSite.Items.IndexOf(ddlSite.Items.FindByValue(.SiteID))

                tbName.Text = .Name
                tbDesc.Text = .Desc
                tbAddr1.Text = .Addr1
                tbAddr2.Text = .Addr2
                tbCity.Text = .City
                'ddlState.Items.FindByValue(.ST).Selected = True
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(.ST))

                tbZip.Text = .Zip
                tbContact.Text = .SiteContact
                tbemail.Text = ""
                tbPhone.Text = .SitePhone

                tbDim1.Text = .Height
                tbDim2.Text = .Length
                tbDim3.Text = .Width
                tbDim4.Text = .Dim4
                tbDim5.Text = .Dim5

                tbMinDist.Text = .MinDist
                tbMaxDist.Text = .MaxDist

                txtSPOT_ID.Text = oTank.oSPOT_Asset.sID
                lblSPOT_Battery.Text = oTank.oSPOT_Asset.sBattery
                lblSPOT_Last.Text = oTank.oSPOT_Asset.dtDateTime
                lblSPOT_Location.Text = oTank.oSPOT_Asset.fLatitude & " " & oTank.oSPOT_Asset.fLongitude
                lblSPOT_Model.Text = oTank.oSPOT_Asset.sModel
                lblSPOT_Name.Text = oTank.oSPOT_Asset.sName




            End With
        End If
    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If oTank Is Nothing Then
            oTank = New clsTank
        End If
        Response.Redirect("mytank.aspx?id=" & clsGeneral.IsZed(tbDefID.Text))

    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        ' move data to structure
        With oTank.Elements
            .CustID = ddlCompany.SelectedValue
            .SiteID = ddlSite.SelectedValue
            .SensorID = tbLogID.Text
            .Model = tbModel.Text
            .Active = ddlStatus.SelectedValue
            .Name = tbName.Text
            .Desc = tbDesc.Text
            .SiteContact = tbContact.Text
            .Addr1 = tbAddr1.Text
            .Addr2 = tbAddr2.Text
            .City = tbCity.Text
            .ST = ddlState.SelectedValue
            .Zip = tbZip.Text
            .SitePhone = tbPhone.Text
            .sSPOT_ID = txtSPOT_ID.Text

            .Height = tbDim1.Text
            .Length = tbDim2.Text
            .Width = tbDim3.Text
            .Dim4 = tbDim4.Text
            .Dim5 = tbDim5.Text
            .MinDist = tbMinDist.Text
            .MaxDist = tbMaxDist.Text

        End With

        If tbDefID.Text = "" Or tbDefID.Text = "0" Then
            ' write record
            If Not oTank.AddTank(oTank.Elements) Then
                'error
            End If
            Response.Redirect("viewtanks.aspx")
        ElseIf CInt(clsGeneral.IsZed(tbDefID.Text)) > 0 Then
            oTank.Elements.DefID = CInt(clsGeneral.IsZed(tbDefID.Text))
            If Not oTank.UpdTank(oTank.Elements) Then
                ' error
            End If
            Response.Redirect("mytank.aspx?id=" & clsGeneral.IsZed(tbDefID.Text))
        End If



    End Sub
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If oTank Is Nothing Then
            oTank = New clsTank
        End If

        If oTank.PreDeleteTank(CInt(clsGeneral.IsZed(tbDefID.Text))) Then
            Response.Redirect("viewtanks.aspx")
        Else
            ' error
        End If
    End Sub
    Private Sub ddlCompany_Init(sender As Object, e As EventArgs) Handles ddlCompany.Init
        sender.DataSource = clsGeneral.QueryDB("select * from customer").Tables(0)
        sender.DataTextField = "custName"
        sender.DataValueField = "custID"
        sender.DataBind()
    End Sub
    Private Sub ddlState_Init(sender As Object, e As EventArgs) Handles ddlState.Init

        sender.DataSource = clsGeneral.QueryDB("select rtrim(ccode) as ccode, rtrim(cdesc) as cdesc from codevalue where cgroup = 'States'").Tables(0)
        sender.DataTextField = "cDesc"
        sender.DataValueField = "cCode"
        sender.DataBind()
    End Sub
    Private Sub ddlSite_Init(sender As Object, e As EventArgs) Handles ddlSite.Init
        sender.DataSource = clsGeneral.QueryDB("select stID, stName from site where stCustID = " & ddlCompany.SelectedValue).Tables(0)
        sender.DataTextField = "stName"
        sender.DataValueField = "stID"
        sender.DataBind()
    End Sub
    Protected Sub cmdFind_Click(sender As Object, e As EventArgs) Handles cmdFind.Click
        Dim oSpot As New clsSpot
        Dim oSpot_Asset As New clsSpot.clsSpotAsset

        Try

            oSpot_Asset = oSpot.GetCurrentLocation(txtSPOT_ID.Text)

            If Not IsNothing(oSpot_Asset.sID) Then
                lblSPOT_Battery.Text = oSpot_Asset.sBattery
                lblSPOT_Last.Text = oSpot_Asset.dtDateTime
                lblSPOT_Location.Text = oSpot_Asset.fLatitude & " " & oSpot_Asset.fLongitude
                lblSPOT_Model.Text = oSpot_Asset.sModel
                lblSPOT_Name.Text = oSpot_Asset.sName
            Else
                lblSPOT_Battery.Text = ""
                lblSPOT_Last.Text = ""
                lblSPOT_Location.Text = ""
                lblSPOT_Model.Text = ""
                lblSPOT_Name.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class