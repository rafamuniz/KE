

Option Infer On ' Required for type inference

Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Public Class MyMeter
    Inherits System.Web.UI.Page
    Protected Property flowrate As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            TextBox1.Text = "0"
            TextBox2.Text = "00000000000"
        End If

        lblValue.Text = TextBox2.Text
        Button1_Click(sender, e)

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        flowrate = Convert.ToInt32(TextBox1.Text)

    End Sub


End Class