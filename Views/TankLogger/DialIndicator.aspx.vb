Option Infer On ' Required for type inference

Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Public Class DialIndicator
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim flowrate As Integer = 0
        Dim indicatormax As Integer = 0
        Dim anglemax As Integer = 0
        Dim angle As Integer = 0
        Dim needleimagepath As String = ""

        Try ' to get and prepare the input values
            ' get the base image for the dial's needle
            needleimagepath = Server.MapPath(Request("image"))

            ' get the flowrate value and convert to appropriate degrees of rotation for the needle
            ' the base dial image needle range is 0 indicated = 0 degrees to 140 indicated = 252 degrees
            ' convert the flowrate value to appropriate degrees of rotation
            flowrate = Convert.ToInt32(Request("flowrate").ToString)
            indicatormax = Convert.ToInt32(Request("indicatormax").ToString)
            anglemax = Convert.ToInt32(Request("anglemax").ToString)
            angle = Convert.ToInt32(flowrate * (anglemax / indicatormax))

        Catch ex As Exception
            ' error getting input values
            Return

        End Try

        Try 'to stream the image
            Dim originalbitmap As Bitmap = Bitmap.FromFile(needleimagepath)

            Dim ms As New MemoryStream
            Dim rotatedbitmap As Bitmap = RotateImage(originalbitmap, angle)
            rotatedbitmap.Save(ms, ImageFormat.Png)

            ms.WriteTo(Response.OutputStream)
        Catch ex As Exception

        End Try

    End Sub
    Private Function RotateImage(bmp As Bitmap, angle As Double) As Bitmap
        Dim rotatedimage As New Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppPArgb)
        Dim g As Graphics = Graphics.FromImage(rotatedimage)
        With g
            .TranslateTransform(bmp.Width / 2, bmp.Height / 2)
            .RotateTransform(angle)
            .TranslateTransform(-bmp.Width / 2, -bmp.Height / 2)
            .DrawImage(bmp, New Point(0, 0))
        End With
        Return rotatedimage
    End Function

End Class