Option Explicit On
Imports System.Reflection.Emit
Imports AxWMPLib
Public Class Form2
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Form1.Visible = True
        Me.Visible = False
        AxWindowsMediaPlayer1.Ctlcontrols.stop()
        Dim rule As String
        rule = MsgBox("使用w,a,s,d来控制方向，按回车键 开始/暂停，极限模式中红色为炸弹，如果触碰到自身、边界或炸弹则游戏结束！", vbQuestion, "游戏规则说明：")
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        Select Case Label2.Text
            Case "极限模式 关"
                Form1.TextBox1.Text = "极限"
                Label2.Text = "极限模式 开"
            Case "极限模式 开"
                Form1.TextBox1.Text = "普通"
                Label2.Text = "极限模式 关"
        End Select
    End Sub
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Application.Exit()
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(250, 150)
        Me.Width = 900
        Me.Height = 600
        Dim musicFilePath As String = "" '"D:\resourse\fm.wav"
        AxWindowsMediaPlayer1.URL = musicFilePath
        AxWindowsMediaPlayer1.Ctlcontrols.play()
        AxWindowsMediaPlayer1.Visible = False
    End Sub
End Class