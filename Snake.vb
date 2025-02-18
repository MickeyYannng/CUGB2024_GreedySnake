Public Class Snake
    '蛇由一个头和很多个球球身体组成
    Class SnakeBody
        Public x, y, r As Integer 'x,y坐标、半径
        Public c As Color   '颜色
        Public Sub New(ByVal x%, ByVal y%, ByVal r%, ByVal c As Color)
            Me.x = x
            Me.y = y
            Me.r = r
            Me.c = c
        End Sub
    End Class
    Public head, body() As SnakeBody
    Public bodyNum As Integer
    Public Sub New(ByVal x%, ByVal y%, ByVal r%, ByVal c As Color)
        head = New SnakeBody(x, y, r, c)
    End Sub
    Public Sub Move(ByVal Direction As Char, ByVal cellmove As Integer)
        'body移动
        Dim i As Integer
        If bodyNum >= 1 Then
            For i = bodyNum - 1 To 1 Step -1
                body(i).x = body(i - 1).x
                body(i).y = body(i - 1).y
            Next
            body(0).x = head.x
            body(0).y = head.y
        End If
        'head 移动
        Select Case Direction
            Case "w"
                head.y -= cellmove - 4
            Case "s"
                head.y += cellmove - 4
            Case "a"
                head.x -= cellmove - 4
            Case "d"
                head.x += cellmove - 4
        End Select

    End Sub
End Class
