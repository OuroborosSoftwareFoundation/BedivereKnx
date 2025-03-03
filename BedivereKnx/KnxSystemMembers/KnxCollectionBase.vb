Imports System.Diagnostics.CodeAnalysis

Public Class KnxCollectionBase(Of TKey, TValue) : Implements IDictionary(Of TKey, TValue)

    Private ReadOnly items As Dictionary(Of TKey, TValue)

    Default Public Overridable Property Item(key As TKey) As TValue Implements IDictionary(Of TKey, TValue).Item
        Get
            Return items(key)
        End Get
        Set(value As TValue)
            items(key) = value
        End Set
    End Property

    Public ReadOnly Property Keys As ICollection(Of TKey) Implements IDictionary(Of TKey, TValue).Keys
        Get
            Return items.Keys
        End Get
    End Property

    Public ReadOnly Property Values As ICollection(Of TValue) Implements IDictionary(Of TKey, TValue).Values
        Get
            Return items.Values
        End Get
    End Property

    Public ReadOnly Property Count As Integer Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Count
        Get
            Return items.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public Sub Add(key As TKey, value As TValue) Implements IDictionary(Of TKey, TValue).Add
        items.Add(key, value)
    End Sub

    Public Sub Add(item As KeyValuePair(Of TKey, TValue)) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Add
        items.Add(item.Key, item.Value)
    End Sub

    Public Sub Clear() Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Clear
        items.Clear()
    End Sub

    Public Sub CopyTo(array() As KeyValuePair(Of TKey, TValue), arrayIndex As Integer) Implements ICollection(Of KeyValuePair(Of TKey, TValue)).CopyTo
        CType(items, ICollection(Of KeyValuePair(Of TKey, TValue))).CopyTo(array, arrayIndex)
    End Sub

    Public Function ContainsKey(key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).ContainsKey
        Return items.ContainsKey(key)
    End Function

    Public Function Remove(key As TKey) As Boolean Implements IDictionary(Of TKey, TValue).Remove
        Return items.Remove(key)
    End Function

    Public Function Remove(item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Remove
        Return items.Remove(item.Key, item.Value)
    End Function

    Public Function TryGetValue(key As TKey, <MaybeNullWhen(False)> ByRef value As TValue) As Boolean Implements IDictionary(Of TKey, TValue).TryGetValue
        Return items.TryGetValue(key, value)
    End Function

    Public Function Contains(item As KeyValuePair(Of TKey, TValue)) As Boolean Implements ICollection(Of KeyValuePair(Of TKey, TValue)).Contains
        Return items.Contains(item)
    End Function

    Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of TKey, TValue)) Implements IEnumerable(Of KeyValuePair(Of TKey, TValue)).GetEnumerator
        Return items.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function
End Class
