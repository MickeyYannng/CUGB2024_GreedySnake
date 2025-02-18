Public Class Map
    Public width, height As Integer  '地图的长宽
    Public cellmove As Integer  '蛇每次移动一个单位长
    Public Sub New(ByVal width%, ByVal height%, ByVal cellmove%)
        Me.width = width
        Me.height = height
        Me.cellmove = cellmove
    End Sub
End Class
