Imports System.Data

''' <summary>
''' KNX区域
''' </summary>
Public Class KnxSystemArea

    Private _Table As DataTable
    'Private _MainNode As New TreeNode

    Public ReadOnly Property Table As DataTable
        Get
            Return _Table
        End Get
    End Property

    'Public ReadOnly Property MainNode As TreeNode
    '    Get
    '        Return _MainNode
    '    End Get
    'End Property

    Public Sub New()
        _Table = New DataTable
    End Sub

    Public Sub New(dt As DataTable)
        _Table = dt
        AreaTreeNodesInit()
    End Sub

    Private Sub AreaTreeNodesInit()
        '_MainNode = New TreeNode("全部")
        'Dim SelNode As TreeNode = _MainNode
        'For Each dr As DataRow In _Table.Rows
        '    SelNode = _MainNode '当前节点重置为根节点
        '    Dim ac As String() = dr("AreaCode").ToString.Split("."c) '节点编号的数组
        '    If String.IsNullOrEmpty(dr("MainCode").ToString.Trim) Then Continue For '无视没有主区域的行
        '    If Not SelNode.Nodes.ContainsKey(ac(0)) Then
        '        SelNode.Nodes.Add(ac(0), dr("MainArea")) '如果主区域节点不存在则添加
        '    End If
        '    If Not String.IsNullOrEmpty(dr("MiddleArea").ToString.Trim) Then '存在中区域
        '        Dim acMdl As String = $"{ac(0)}.{ac(1)}"
        '        SelNode = SelNode.Nodes(ac(0)) '选择当前主区域节点
        '        If Not SelNode.Nodes.ContainsKey(acMdl) Then '如果中区域节点不存在则添加
        '            SelNode.Nodes.Add(acMdl, dr("MiddleArea")) '如果中区域节点不存在则添加
        '        End If
        '        If Not String.IsNullOrEmpty(dr("SubArea").ToString.Trim) Then '存在子区域
        '            Dim acSub As String = $"{ac(0)}.{ac(1)}.{ac(2)}"
        '            SelNode = SelNode.Nodes(acMdl) '选择当前中区域节点
        '            If Not SelNode.Nodes.ContainsKey(acSub) Then '如果子区域节点不存在则添加
        '                SelNode.Nodes.Add(acSub, dr("SubArea")) '如果子区域节点不存在则添加
        '            End If
        '        Else
        '            Continue For
        '        End If
        '    Else
        '        Continue For
        '    End If
        'Next
    End Sub

End Class
