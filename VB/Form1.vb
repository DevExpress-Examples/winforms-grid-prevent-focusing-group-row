Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Q183147

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            Dim rand As Random = New Random()
            Dim list = New BindingList(Of Item)()
            For i As Integer = 0 To 50 - 1
                list.Add(New Item() With {.ID = i, .Name = "Name" & i, .Category = rand.Next(0, 5)})
            Next

            gridControl1.DataSource = list
        End Sub

        Private Sub gridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As Views.Base.FocusedRowChangedEventArgs)
            Dim focusedRowHandle As Integer = -1
            If e.FocusedRowHandle = GridControl.NewItemRowHandle OrElse e.FocusedRowHandle = GridControl.AutoFilterRowHandle Then Return
            Dim view As GridView = CType(sender, GridView)
            If e.FocusedRowHandle < 0 Then
                If e.PrevFocusedRowHandle = GridControl.InvalidRowHandle Then
                    focusedRowHandle = 0
                ElseIf MouseButtons = MouseButtons.Left OrElse MouseButtons = MouseButtons.Right Then
                    focusedRowHandle = e.PrevFocusedRowHandle
                Else
                    Dim prevRow As Integer = view.GetVisibleIndex(e.PrevFocusedRowHandle)
                    Dim currRow As Integer = view.GetVisibleIndex(e.FocusedRowHandle)
                    If prevRow > currRow Then
                        focusedRowHandle = e.PrevFocusedRowHandle - 1
                    Else
                        focusedRowHandle = e.PrevFocusedRowHandle + 1
                    End If

                    If focusedRowHandle < 0 Then focusedRowHandle = 0
                    If focusedRowHandle >= view.DataRowCount Then focusedRowHandle = view.DataRowCount - 1
                End If

                view.FocusedRowHandle = If(focusedRowHandle < 0, 0, focusedRowHandle)
            End If
        End Sub

        Public Class Item

            Public Property ID As Integer

            Public Property Name As String

            Public Property Category As Integer
        End Class
    End Class
End Namespace
