Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Q183147
    Partial Public Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim rand As New Random()
            Dim list = New BindingList(Of Item)()
            For i As Integer = 0 To 49
                list.Add(New Item() With { _
                    .ID = i, _
                    .Name = "Name" & i, _
                    .Category = rand.Next(0, 5) _
                })
            Next i
            gridControl1.DataSource = list
        End Sub

        Private Sub gridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gridView1.FocusedRowChanged
            Dim focusedRowHandle As Integer = -1
            If e.FocusedRowHandle = GridControl.NewItemRowHandle OrElse e.FocusedRowHandle = GridControl.AutoFilterRowHandle Then
                Return
            End If
            Dim view As GridView = DirectCast(sender, GridView)
            If e.FocusedRowHandle < 0 Then
                If e.PrevFocusedRowHandle = GridControl.InvalidRowHandle Then
                    focusedRowHandle = 0
                ElseIf Control.MouseButtons = MouseButtons.Left OrElse Control.MouseButtons = MouseButtons.Right Then
                    focusedRowHandle = e.PrevFocusedRowHandle
                Else
                    Dim prevRow As Integer = view.GetVisibleIndex(e.PrevFocusedRowHandle)
                    Dim currRow As Integer = view.GetVisibleIndex(e.FocusedRowHandle)
                    If prevRow > currRow Then
                        focusedRowHandle = e.PrevFocusedRowHandle - 1
                    Else
                        focusedRowHandle = e.PrevFocusedRowHandle + 1
                    End If
                    If focusedRowHandle < 0 Then
                        focusedRowHandle = 0
                    End If
                    If focusedRowHandle >= view.DataRowCount Then
                        focusedRowHandle = view.DataRowCount - 1
                    End If
                End If
                view.FocusedRowHandle = If(focusedRowHandle < 0, 0, focusedRowHandle)
            End If
        End Sub
        Public Class Item
            Public Property ID() As Integer
            Public Property Name() As String
            Public Property Category() As Integer
        End Class
    End Class
End Namespace