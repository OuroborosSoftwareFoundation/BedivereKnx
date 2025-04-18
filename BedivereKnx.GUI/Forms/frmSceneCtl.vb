Imports BedivereKnx

Public Class frmSceneCtl

    Friend Scn As KnxScene
    Friend CtlVal As Byte '传出去的场景控制值

    Private Sub frmSceneCtl_Load(sender As Object, e As EventArgs) Handles Me.Load
        lblScnName.Text = $"{Scn.Name}" '场景名称
        Dim grp As KnxGroup = Scn(KnxObjectPart.SceneControl)
        lblScnAddr.Text = $"({grp.Address})" '场景地址 
        lvScn.Clear() '清空场景列表
        lvScn.Columns.Add("ScnAddr", "场景地址")
        lvScn.Columns.Add("ScnName", "场景名称")
        For i = 0 To Scn.Names.Length - 1
            Dim str As String = Scn.Names(i)
            If String.IsNullOrEmpty(str) Then Continue For '跳过空项
            Dim lvi As New ListViewItem(i)
            lvi.Tag = i
            lvi.SubItems.Add(str)
            lvScn.Items.Add(lvi)
        Next
        lvScn.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Sub btnOk_Click(sender As Object, ByVal e As EventArgs) Handles btnOk.Click
        If lvScn.SelectedItems.Count > 0 Then
            CtlVal = lvScn.SelectedItems(0).Tag
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class
