Public Class tanklogi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tbDateTime.Text = Now()
    End Sub

    Private Sub submit_Click(sender As Object, e As EventArgs) Handles submit.Click
        ' assemble a tank log string for one tank
        Dim logstr As String
        logstr = "L" ' log section flag
        logstr = logstr + "S" + tbSiteID.Text + ";" ' site id
        logstr = logstr + "T" + tbTankID.Text + ";" ' tank id
        logstr = logstr + ddRType.SelectedValue + tbDepth.Text + ";" ' R-Range, D-Depth, G-Gallons and value
        logstr = logstr + "W" + tbTemp.Text + ";" ' water temp
        logstr = logstr + "A" + tbTemp.Text + ";" ' ambient temp (for manual logging force same)
        logstr = logstr + "Z" + tbDateTime.Text + ";" ' date and time
        logstr = logstr + "V" + 0.ToString + ";" ' sensor voltage
        logstr = logstr + "P" + tbLoc.Text

        Dim LoggingService As New LocalLoggingService.Service1
        If LoggingService.writetanklog(logstr) Then
            tbSample.Text = "Posting succeeded."
        Else
            ' error
            tbSample.Text = "Posting failed."
        End If
    End Sub
End Class