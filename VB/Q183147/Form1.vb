Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors

Namespace Q183147
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' TODO: This line of code loads data into the 'carsDBDataSet.Cars' table. You can move, or remove it, as needed.
			Me.carsTableAdapter.Fill(Me.carsDBDataSet.Cars)

		End Sub

		Private Sub gridView1_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gridView1.FocusedRowChanged
			Dim focusedRowHandle As Integer = -1
			If e.FocusedRowHandle = GridControl.NewItemRowHandle OrElse e.FocusedRowHandle = GridControl.AutoFilterRowHandle Then
				Return
			End If
			Dim view As GridView = CType(sender, GridView)
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
				If focusedRowHandle < 0 Then
					view.FocusedRowHandle = 0
				Else
					view.FocusedRowHandle = focusedRowHandle
				End If
			End If
		End Sub
	End Class
End Namespace