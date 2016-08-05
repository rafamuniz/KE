Public Class myTank
    Inherits System.Web.UI.Page
    Dim oLog As clsLogdata
    Dim oFormula As clsVolFormula
    Dim oTank As clsTank
    Public oSite As clsSite
    Dim oAlarm As clsAlarm
    Public lat As Double
    Public lon As Double
    Shared tankname As String
    Shared tankdefid As Integer
    Shared tankdefsensorid As String
    Shared tankgalrem As Integer
    Shared alarmid As Integer
    Shared alarmstate As String = ""
    Shared alarmguid As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If oSite Is Nothing Then
            oSite = New clsSite
        End If

        If Not oSite.OnSite Then
            ' hide the WX and MAP buttons if onsite mode
            btnMap.Visible = True
            btnWX.Visible = True

            ' remove the realtime data option if onsite mode
            rbChartMode.Items.FindByValue("H").Selected = True ' set a new default
            Dim removeListItem As ListItem = rbChartMode.Items.FindByValue("R")
            rbChartMode.Items.Remove(removeListItem)
        End If

        If Not Page.IsPostBack Then
            ' set page refresh rate
            'Dim meta As New HtmlMeta
            'meta.HttpEquiv = "Refresh"
            'meta.Content = ConfigurationManager.AppSettings("tankpagerefresh")
            'MetaPlaceHolder.Controls.Add(meta)


            ' default to the tank selected if there was one
            Try ' to get the querystring
                lblTankID.Text = Request.QueryString("id")
            Catch ex As Exception

            End Try

            ' fill page elements
            'clsGeneral.FillDD(rbChartMode, "chtmode")
            'rbChartMode.SelectedIndex = 0


            If CInt(lblTankID.Text) > 0 Then
                ' fill the page with the default tank
                ShowTank(CInt(lblTankID.Text))

            End If

        End If

    End Sub

    Protected Sub ShowTank(ByVal tankid As Integer)
        If oLog Is Nothing Then
            oLog = New clsLogdata
        End If
        If oFormula Is Nothing Then
            oFormula = New clsVolFormula
        End If
        If oTank Is Nothing Then
            oTank = New clsTank
        End If
        If oAlarm Is Nothing Then
            oAlarm = New clsAlarm
        End If

        ' get the details of this tank
        If oTank.GetTank(tankid) Then
            ' move some tank details to the page
            lblTankName.Text = oTank.Elements.Name
            lblSPOT_NAME.Text = oTank.oSPOT_Asset.sName
            btnMap.Text = oTank.oSPOT_Asset.fLatitude & " " & oTank.oSPOT_Asset.fLongitude

            ' set default tank image
            If oTank.GetTankModel(oTank.Elements.Model) Then
                imgTank.ImageUrl = "images/" & oTank.MdlElem.ImageDim.Replace(".png", "_NoData.png")
            End If

            ' graph the temp logs
            lblnoVdata.Visible = True
            lblnoTdata.Visible = True
            lblnoPdata.Visible = True

            If chart_bind2(oTank.Elements.SensorID, rbChartMode.SelectedValue) Then

                ' get the last Range value
                Dim logrecord As DataRow = oLog.get_latest_data(tankid)
                If Not logrecord Is Nothing Then

                    ' round the pctfull to the closest 10% and ma to a tank image
                    Dim tenspct As Integer = Math.Round(logrecord.Item("PctFull") / 10, 0) * 10
                    ' get tank model rec for image path
                    imgTank.ImageUrl = "images/" & oTank.MdlElem.ImageDim.Replace(".png", "_" & tenspct.ToString & ".png")

                    ' fill the page fields
                    lblPctfull.Text = logrecord.Item("PctFull").ToString & "%"
                    tankgalrem = logrecord.Item("Volume_Remaining_Gal")
                    lblRemvol.Text = tankgalrem.ToString
                    lblTotvol.Text = logrecord.Item("Volume_Gal").ToString & " gallons"
                    lblAge.Text = clsGeneral.IsADate(logrecord.Item("tdDateTime")).ToString

                    lblnoVdata.Visible = False
                    lblnoTdata.Visible = False
                    lblnoPdata.Visible = False

                    ' show the alarms setting
                    tankdefid = oTank.Elements.DefID
                    tankdefsensorid = oTank.Elements.SensorID
                    FillAlarms(tankdefsensorid)

                End If
            End If

        Else
            'error - tank not found
        End If

    End Sub
    Private Function FillAlarms(ByVal id As String)
        Dim alds As DataSet
        Try

            alds = oAlarm.GetLimits(id)
            alds.Tables(0).Columns.Add("warnimage")
            For Each row In alds.Tables(0).Rows
                If (row.item("slSensType") = "Low" And tankgalrem < row.item("slSensValue")) Or
                    (row.item("slSensType") = "High" And tankgalrem > row.item("slSensValue")) Then
                    row.item("warnimage") = "images/warning_tiny.png"
                End If
            Next
            grdAlarms.DataSource = alds
            grdAlarms.DataBind()
        Catch ex As Exception
            ' unable to load alarm panel
        End Try

    End Function
    Private Function chart_bind2(ByVal tankid As String, ByVal mode As String) As Boolean
        Dim ds As New DataSet
        ds = oLog.getGraphData(tankid, mode)

        If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then

            ' range
            RangeChart.DataSource = ds
            RangeChart.DataBind()

            RangeChart.Titles.Add("RangeTitle").Text = "Tank Volume Chart (gals.)"

            RangeChart.Series("RangeSeries").YValueMembers = "wVol"
            RangeChart.Series("RangeSeries").XValueMember = "SampleTime"
            RangeChart.Series("RangeSeries").BorderWidth = 3


            If mode <> "R" Then
                RangeChart.Series("RangeSeries").ChartType = DataVisualization.Charting.SeriesChartType.Point
            End If


            ' temps
            TempChart.DataSource = ds
            TempChart.DataBind()

            TempChart.Titles.Add("TempTitle").Text = "Tank Temp Chart (" + Chr(176) + "F)"

            TempChart.Series("TempSeries1").YValueMembers = "wTemp"
            TempChart.Series("TempSeries1").XValueMember = "SampleTime"
            TempChart.Series("TempSeries1").BorderWidth = 3

            TempChart.Series("TempSeries2").YValueMembers = "aTemp"
            TempChart.Series("TempSeries2").XValueMember = "SampleTime"
            TempChart.Series("TempSeries2").BorderWidth = 3

            If mode <> "R" Then
                TempChart.Series("TempSeries1").ChartType = DataVisualization.Charting.SeriesChartType.Point
                TempChart.Series("TempSeries2").ChartType = DataVisualization.Charting.SeriesChartType.Point
            End If


            ' volts
            VoltChart.DataSource = ds
            VoltChart.DataBind()


            VoltChart.Titles.Add("SensorTitle").Text = "Sensor Voltage Chart (mV)"

            VoltChart.Series("VoltSeries").YValueMembers = "mvolts"
            VoltChart.Series("VoltSeries").XValueMember = "SampleTime"
            VoltChart.Series("VoltSeries").BorderWidth = 3

            If mode <> "R" Then
                VoltChart.Series("VoltSeries").ChartType = DataVisualization.Charting.SeriesChartType.Point
            End If

        End If

        Return True
    End Function
    Private Sub rbChartMode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbChartMode.SelectedIndexChanged
        ShowTank(CInt(lblTankID.Text))
    End Sub
    Protected Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Response.Redirect("addtank.aspx?id=" & CInt(lblTankID.Text))
    End Sub
    Private Sub grdAlarms_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdAlarms.RowCommand
        ' get id of alarm limit record clicked
        Select Case e.CommandName
            Case "cmdSelected"
                grdAlarms.SelectedIndex = e.CommandArgument

                ' move info into text fields
                alarmid = grdAlarms.SelectedDataKey.Value
                lblAlID.Text = alarmid.ToString
                Dim lb As LinkButton = grdAlarms.SelectedRow.Cells(1).Controls(0)
                tbAlDesc.Text = lb.Text
                ddAlType.Items.FindByValue(grdAlarms.SelectedRow.Cells(2).Text).Selected = True
                tbAlLimit.Text = grdAlarms.SelectedRow.Cells(3).Text
                tbAlEmail.Text = grdAlarms.SelectedRow.Cells(4).Text

                alarmstate = grdAlarms.SelectedRow.Cells(5).Text
                alarmguid = grdAlarms.SelectedRow.Cells(6).Text

            Case "cmdDelete"
                If oAlarm Is Nothing Then
                    oAlarm = New clsAlarm
                End If
                grdAlarms.SelectedIndex = e.CommandArgument
                If oAlarm.DeleteLimit(grdAlarms.SelectedDataKey.Value.ToString) < 0 Then
                    ' error - deleting alarm limit record
                End If

                FillAlarms(tankdefsensorid)

            Case Else

        End Select


    End Sub
    Private Sub btnMap_Click(sender As Object, e As EventArgs) Handles btnMap.Click
        Response.Redirect("myTankGeo.aspx?id=" & tankdefid)
        'Response.Write("<script>")
        'Response.Write("window.open('" & "myTankGeo.aspx?id=" & tankdefid & "','_blank')")
        'Response.Write("</script>")
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ' add / update the record in the table
        If oAlarm Is Nothing Then
            oAlarm = New clsAlarm
        End If

        oAlarm.Elements.slID = alarmid
        oAlarm.Elements.slSensorID = tankdefsensorid
        oAlarm.Elements.slDescription = tbAlDesc.Text
        oAlarm.Elements.slSensType = ddAlType.SelectedValue
        oAlarm.Elements.slSensValue = tbAlLimit.Text
        oAlarm.Elements.slSensEmail = tbAlEmail.Text
        oAlarm.Elements.slActive = alarmstate
        oAlarm.Elements.slModFlag = "U"
        oAlarm.Elements.slGUID = alarmguid
        oAlarm.Elements.slLastUpdate = "1/1/1990"

        If oAlarm.UpdSensorLimit() < 0 Then
            'error

        End If
        alarmid = 0
        tbAlDesc.Text = ""
        tbAlEmail.Text = ""
        tbAlLimit.Text = ""
        ddAlType.SelectedIndex = 0

        FillAlarms(tankdefsensorid)
    End Sub
    Private Sub btnWX_Click(sender As Object, e As EventArgs) Handles btnWX.Click
        Response.Redirect("myTankWX.aspx?id=" & tankdefid)
        'Response.Write("<script>")
        'Response.Write("window.open('" & "myTankWX.aspx?id=" & tankdefid & "','_blank')")
        'Response.Write("</script>")

    End Sub
End Class