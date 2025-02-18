Public Class Bomb
    Public x, y, r As Integer 'x,y坐标、半径
    Public c As Color = Color.Red
    Public rand As Random
    Public Sub New()
        rand = New Random()
    End Sub
    Public Sub RandInfo(ByVal width%, ByVal height%)
        r = 10  ' bomb的半径
        Dim myArray(30) As Integer
        Dim j% = 0
        For i = 10 To 500 Step 20
            myArray(j) = i
            j += 1
        Next
        x = myArray(rand.Next(j))
        y = myArray(rand.Next(j))
    End Sub
End Class
