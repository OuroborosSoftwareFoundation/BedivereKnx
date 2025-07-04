Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Class frmReference

    Dim RefAssy As AssemblyName() = Assembly.GetExecutingAssembly.GetReferencedAssemblies
    'Dim RefAssy As Assembly() = AppDomain.CurrentDomain.GetAssemblies()

    Private Sub frmReference_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShowRef()
    End Sub

    Private Sub ShowRef()
        lvRef.Items.Clear()
        For Each assyName As AssemblyName In RefAssy
            Dim assy As Assembly = Assembly.Load(assyName)
            Dim company As AssemblyCompanyAttribute = assy.GetCustomAttribute(GetType(AssemblyCompanyAttribute))
            If Not company.Company.StartsWith("Microsoft") Then
                If Not lvRef.ContainsGroup(company.Company) Then
                    lvRef.Groups.Add(company.Company, company.Company)
                End If
                Dim lvi As New ListViewItem(assy.GetName.Name)
                'lvi.SubItems.Add(assy.GetName.Name)
                lvi.SubItems.Add(assy.GetName.Version.ToString)
                lvi.Group = lvRef.Groups(company.Company)
                lvRef.Items.Add(lvi)
            End If
        Next
        lvRef.Sorting = SortOrder.Ascending
        lvRef.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

End Class
