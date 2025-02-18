Option Explicit On
Imports System.Reflection.Emit
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
'Imports System.Security.Cryptography.X509Certificates
Public Class Form1
    Dim player1 As New System.Media.SoundPlayer()
    Dim player2 As New System.Media.SoundPlayer()
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(250, 150)
        AxWindowsMediaPlayer1.Visible = False
        Dim musicFilePath1 As String = "" '"D:\resourse\die.wav"
        Dim musicFilePath2 As String = "" '"D:\resourse\Score.wav"
        player1.SoundLocation = musicFilePath1
        player2.SoundLocation = musicFilePath2
        Me.Width = 900
        Me.Height = 600
        If TextBox1.Text = "极限" Then
            myGame = New Game(500, 500, 20)
        Else
            myGame = New Game(500, 500, 12)
        End If
        Picshow.Width = myGame.myMap.width + 1
        Picshow.Height = myGame.myMap.height + 1
        Picshow.Left = 30
        Picshow.Top = 30
        Timer1.Enabled = False
    End Sub
    Private Sub PicShow_Paint(sender As Object, e As PaintEventArgs) Handles Picshow.Paint
        Dim x, y, i, sc, w, h As Integer
        sc = myGame.myMap.cellmove
        w = myGame.myMap.width
        h = myGame.myMap.height
        Dim mygraphics As Graphics
        mygraphics = e.Graphics
        '画边框
        mygraphics.DrawLine(Pens.Black, 0, 0, 0, h)
        mygraphics.DrawLine(Pens.Black, w, 0, w, h)
        mygraphics.DrawLine(Pens.Black, 0, 0, w, 0)
        mygraphics.DrawLine(Pens.Black, 0, h, w, h)
        '画网格
        For i = 0 To w Step 20
            mygraphics.DrawLine(Pens.Black, i, 0, i, h)
        Next
        For i = 0 To h Step 20
            mygraphics.DrawLine(Pens.Black, 0, i, w, i)
        Next
        '画蛇头
        Dim mybrush As New SolidBrush(Color.Black)
        Dim r As Integer
        x = myGame.mySnake.head.x
        y = myGame.mySnake.head.y
        r = myGame.mySnake.head.r
        Dim rect1 As Rectangle = New Rectangle(x - r, y - r, 2 * r, 2 * r)
        mygraphics.DrawEllipse(Pens.Black, rect1)
        mygraphics.FillEllipse(mybrush, rect1)
        mybrush = Nothing
        '同理，画蛇的身体
        If (myGame.mySnake.bodyNum > 0) Then
            For i = 0 To myGame.mySnake.bodyNum - 1
                x = myGame.mySnake.body(i).x
                y = myGame.mySnake.body(i).y
                r = myGame.mySnake.body(i).r
                Dim mybrush1 As New SolidBrush(Color.SkyBlue)
                Dim mypen1 As New Pen(Color.SkyBlue)
                mygraphics.DrawEllipse(mypen1, x - r, y - r, 2 * r, 2 * r)
                mygraphics.FillEllipse(mybrush1, x - r, y - r, 2 * r, 2 * r)
            Next
        End If
        '画蛋
        x = myGame.myEgg.x
        y = myGame.myEgg.y
        r = myGame.myEgg.r
        Dim mybrush2 As New SolidBrush(myGame.myEgg.c)
        Dim mypen2 As New Pen(myGame.myEgg.c)
        mygraphics.DrawEllipse(mypen2, x - r, y - r, 2 * r, 2 * r)
        mygraphics.FillEllipse(mybrush2, x - r, y - r, 2 * r, 2 * r)
        '画炸弹
        If TextBox1.Text = "极限" Then
            x = myGame.myBomb.x
            y = myGame.myBomb.y
            r = myGame.myBomb.r
            Dim mybrush3 As New SolidBrush(Color.Red)
            Dim mypen3 As New Pen(Color.Red)
            mygraphics.DrawEllipse(mypen3, x - r, y - r, 2 * r, 2 * r)
            mygraphics.FillEllipse(mybrush3, x - r, y - r, 2 * r, 2 * r)
        End If
    End Sub
    Private Sub MnuStart_Click(sender As Object, e As EventArgs) Handles MnuStart.Click
        Select Case MnuStart.Text
            Case "开始游戏(Enter)"
                Dim musicFilePath As String = "" '"D:\resourse\bg.wav"
                AxWindowsMediaPlayer1.URL = musicFilePath
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                If TextBox1.Text = "极限" Then
                    myGame = New Game(500, 500, 20)
                Else
                    myGame = New Game(500, 500, 12)
                End If
                Dim gr As Graphics
                gr = Picshow.CreateGraphics()
                Dim mybrush As New SolidBrush(myGame.mySnake.head.c)
                Dim x, y, r As Integer
                x = myGame.mySnake.head.x
                y = myGame.mySnake.head.y
                r = myGame.mySnake.head.r
                Dim rect As Rectangle = New Rectangle(x - r, y - r, 2 * r, 2 * r)
                gr.DrawEllipse(Pens.Black, rect)
                gr.FillEllipse(mybrush, rect)
                Timer1.Enabled = True
                MnuStart.Text = "暂停游戏(Enter)"
            Case "暂停游戏(Enter)"
                Timer1.Enabled = False
                AxWindowsMediaPlayer1.Ctlcontrols.pause()
                MnuStart.Text = "继续游戏(Enter)"
            Case "继续游戏(Enter)"
                AxWindowsMediaPlayer1.Ctlcontrols.play()
                Timer1.Enabled = True
                MnuStart.Text = "暂停游戏(Enter)"
        End Select
    End Sub
    Private Sub MnuQuit_Click(sender As Object, e As EventArgs) Handles MnuQuit.Click
        Application.Exit() ' 退出应用程序
    End Sub
    Dim count% = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        count += 1
        If TextBox1.Text = "极限" Then
            If (count Mod 100 = 0) Then
                myGame.myBomb.RandInfo(500, 500)
            End If
        End If
        Dim sc As Integer
        sc = myGame.myMap.cellmove
        myGame.mySnake.Move(myGame.Direction, sc)
        Picshow.Refresh()
        '死亡检测
        If myGame.JudgeDie Then
            player1.Play()
            Timer1.Enabled = False
            AxWindowsMediaPlayer1.Ctlcontrols.stop()
            MessageBox.Show("撞死了！您的分数为： " & myGame.Score, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
            myGame.WriteToFile()
            MnuStart.Text = "开始游戏(Enter)"
            MnuScore.Text = "分数： 0"
            Exit Sub
        End If
        '吃蛋检测
        If myGame.JudgeScore Then
            player2.Play()
            MnuScore.Text = "分数： " & myGame.Score
            Exit Sub
        End If

    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.W
                If (myGame.Direction = "s") Then Exit Sub
                myGame.Direction = "w"
            Case Keys.S
                If (myGame.Direction = "w") Then Exit Sub
                myGame.Direction = "s"
            Case Keys.A
                If (myGame.Direction = "d") Then Exit Sub
                myGame.Direction = "a"
            Case Keys.D
                If (myGame.Direction = "a") Then Exit Sub
                myGame.Direction = "d"
            Case Keys.Enter
                MnuStart_Click(sender, e)
        End Select
    End Sub

    Private Sub MnuRecord_Click(sender As Object, e As EventArgs) Handles MnuRecord.Click
        myGame.ReadFile()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Me.Close()
        Form2.Visible = True
    End Sub
End Class