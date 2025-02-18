Public Class Egg
    Public x, y, r As Integer 'x,y坐标、半径
    Public c As Color   '颜色
    Public rand As Random
    Public Sub New()
        rand = New Random()
    End Sub
    Public Sub RandInfo(ByVal width%, ByVal height%)
        r = rand.Next(6, 15)    ' 生成egg随机半径
        Dim myArray(30) As Integer
        Dim j% = 0
        For i = 10 To 500 Step 20
            myArray(j) = i
            j += 1
        Next
        x = myArray(rand.Next(j))
        y = myArray(rand.Next(j))
        Dim col As Integer
        col = rand.Next(10)
        Select Case col
            Case 0
                c = Color.DarkOrchid
            Case 1
                c = Color.HotPink
            Case 2
                c = Color.YellowGreen
            Case 3
                c = Color.DarkGray
            Case 4
                c = Color.Orange
            Case 5
                c = Color.Gold
            Case 6
                c = Color.Blue
            Case 7
                c = Color.Brown
            Case 8
                c = Color.PaleGreen
            Case 9
                c = Color.Magenta
        End Select
    End Sub
End Class