Imports System.Media
Imports System.Numerics
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports 杨美琪1004226104贪吃蛇大作战.Snake
Public Class Game
    Public mySnake As Snake
    Public myMap As Map
    Public myBomb As Bomb
    Public myEgg As Egg
    Public Score As Integer
    Public Direction As Char = "w" '初始化方向
    Public Sub New(ByVal width%, ByVal height%, ByVal cellmove%)
        myMap = New Map(width, height, cellmove) ' 初始化地图
        mySnake = New Snake(myMap.width \ 2, myMap.height \ 2, 8, Color.Black) ' 初始化蛇
        myEgg = New Egg() ' 初始化蛋
        If Form1.TextBox1.Text = "极限" Then
            myBomb = New Bomb()
            myBomb.RandInfo(myMap.width, myMap.height)
        End If
        myEgg.RandInfo(myMap.width, myMap.height)
    End Sub
    Public Function JudgeDie() As Boolean
        If Form1.TextBox1.Text = "极限" Then
            Dim d As Single
            d = Math.Sqrt((mySnake.head.x - myBomb.x) ^ 2 + (mySnake.head.y - myBomb.y) ^ 2)
            Dim r1 As Integer = mySnake.head.r
            Dim r2 As Integer = myBomb.r
            If (d < r1 + r2) Then 'head 与bomb 撞了
                Return True
            End If
        End If
        If mySnake.head.x - mySnake.head.r < 0 Or mySnake.head.x + mySnake.head.r > myMap.width Or
           mySnake.head.y - mySnake.head.r < 0 Or mySnake.head.y + mySnake.head.r > myMap.height Then '是否超出map或是否撞了自己
            Return True
        ElseIf mySnake.bodyNum > 2 Then '从body(2)开始检测是否与head撞了
            Dim i As Integer
            For i = 2 To mySnake.bodyNum - 1
                Dim d As Single
                d = Math.Sqrt((mySnake.head.x - mySnake.body(i).x) ^ 2 + (mySnake.head.y - mySnake.body(i).y) ^ 2)
                Dim r1 As Integer = mySnake.head.r
                Dim r2 As Integer = mySnake.body(i).r
                If (d < r1 + r2) Then 'head 与body 撞了
                    Return True
                End If
            Next
            Return False
        Else
            Return False
        End If
    End Function
    Public Function JudgeScore() As Boolean '是否需要加分
        Dim d As Single = Math.Sqrt((mySnake.head.x - myEgg.x) ^ 2 + (mySnake.head.y - myEgg.y) ^ 2)
        Dim r1 As Integer = mySnake.head.r
        Dim r2 As Integer = myEgg.r
        If (d < r1 + r2) Then '检查蛇头和蛋的距离
            Score += 16 - myEgg.r
            Dim x, y As Integer
            Select Case Direction
                Case "w"
                    x = mySnake.head.x
                    y = mySnake.head.y - myEgg.r
                Case "s"
                    x = mySnake.head.x
                    y = mySnake.head.y + myEgg.r
                Case "a"
                    x = mySnake.head.x + myEgg.r
                    y = mySnake.head.y
                Case "d"
                    x = mySnake.head.x - myEgg.r
                    y = mySnake.head.y
            End Select
            ReDim Preserve mySnake.body(mySnake.bodyNum + 1)
            mySnake.body(mySnake.bodyNum) = New Snake.SnakeBody(x, y, mySnake.head.r, myEgg.c)
            mySnake.bodyNum += 1
            myGame.myEgg.RandInfo(myMap.width, myMap.height) '吃完了，该蛋换位置
            Return True
        End If
        Return False
    End Function
    Public Sub WriteToFile()
        Dim path As String = Application.StartupPath & "\record.txt"
        If (Not IO.File.Exists(path)) Then
            Dim sw As IO.StreamWriter = IO.File.CreateText(path)
            Using (sw)
                sw.WriteLine(Now() & " " & "Score= " & Score)
            End Using
            MessageBox.Show("创建记录文件")
            Exit Sub
        End If
        Dim sw1 = IO.File.AppendText(path)
        Using (sw1)
            sw1.WriteLine(Now() & " " & "Score= " & Score)
        End Using
    End Sub
    Public Sub ReadFile()
        Dim path As String = Application.StartupPath & "\record.txt"
        If (Not IO.File.Exists(path)) Then
            MessageBox.Show("记录文件不存在")
            Exit Sub
        End If
        Dim txt As String = IO.File.ReadAllText(path)
        MessageBox.Show(txt)
    End Sub

End Class