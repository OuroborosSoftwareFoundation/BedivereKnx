Imports Knx.Falcon

Public Class KnxHmiButton : Inherits Button

    Public Property Address As GroupAddress


    Public Property Values As GroupValue()

    Public Property CurrentValue As GroupValue

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        '在此处添加自定义绘制代码
    End Sub

End Class
