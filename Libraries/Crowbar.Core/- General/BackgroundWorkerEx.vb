﻿Imports System.ComponentModel

Public Class BackgroundWorkerEx
	Inherits BackgroundWorker

	Public Shared DoEvents As Action = Nothing

#Region "Create and Destroy"

	Public Shared Function RunBackgroundWorker(ByVal bw As BackgroundWorkerEx, ByVal bw_DoWork As DoWorkEventHandler, ByVal bw_ProgressChanged As ProgressChangedEventHandler, ByVal bw_RunWorkerCompleted As RunWorkerCompletedEventHandler, ByVal bw_argument As Object) As BackgroundWorkerEx
		If bw Is Nothing Then
			bw = New BackgroundWorkerEx()
			bw.WorkerSupportsCancellation = True
			bw.WorkerReportsProgress = True
		Else
			While bw.IsBusy
				bw.CancelAsync()
				DoEvents()
			End While
		End If

		bw.DoWorkHandler = bw_DoWork
		bw.ProgressChangedHandler = bw_ProgressChanged
		bw.RunWorkerCompletedHandler = bw_RunWorkerCompleted

		bw.RunWorkerAsync(bw_argument)

		Return bw
	End Function

#End Region

#Region "Methods"

	Public Sub Kill()
		RemoveHandler Me.DoWork, Me.theDoWorkHandler
		RemoveHandler Me.ProgressChanged, Me.theProgressChangedHandler
		RemoveHandler Me.RunWorkerCompleted, AddressOf Me.BWE_RunWorkerCompleted
	End Sub

#End Region

#Region "Private Properties"

	Public Property DoWorkHandler() As DoWorkEventHandler
		Get
			Return Me.theDoWorkHandler
		End Get
		Set
			Me.theDoWorkHandler = Value
			AddHandler Me.DoWork, Me.theDoWorkHandler
		End Set
	End Property

	Public Property ProgressChangedHandler() As ProgressChangedEventHandler
		Get
			Return Me.theProgressChangedHandler
		End Get
		Set
			Me.theProgressChangedHandler = Value
			AddHandler Me.ProgressChanged, Me.theProgressChangedHandler
		End Set
	End Property

	Public Property RunWorkerCompletedHandler() As RunWorkerCompletedEventHandler
		Get
			Return Me.theRunWorkerCompletedHandler
		End Get
		Set
			Me.theRunWorkerCompletedHandler = Value
			' Assign BWE_RunWorkerCompleted so can remove all handlers before calling real RunWorkerCompleted.
			AddHandler Me.RunWorkerCompleted, AddressOf Me.BWE_RunWorkerCompleted
		End Set
	End Property

#End Region

#Region "Private Handlers"

	Private Sub BWE_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
		Me.Kill()
		Me.theRunWorkerCompletedHandler(Me, e)
	End Sub

#End Region

#Region "Data"

	Private theDoWorkHandler As DoWorkEventHandler
	Private theProgressChangedHandler As ProgressChangedEventHandler
	Private theRunWorkerCompletedHandler As RunWorkerCompletedEventHandler

#End Region

#Region "Example Usage Class Template"

	'Public Sub New()
	'	MyBase.New()

	'	Me.isRunning = False
	'End Sub

	'Public Sub Run()
	'	If Not Me.isRunning Then
	'		Me.isRunning = True

	'		Dim worker As BackgroundWorkerEx = Nothing
	'		Dim inputInfo As String = ""
	'		worker = BackgroundWorkerEx.RunBackgroundWorker(worker, AddressOf Me.Worker_DoWork, AddressOf Me.Worker_ProgressChanged, AddressOf Me.Worker_RunWorkerCompleted, inputInfo)

	'	End If
	'End Sub

	''NOTE: This is run in a background thread.
	'Private Sub Worker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
	'	Dim bw As BackgroundWorkerEx = CType(sender, BackgroundWorkerEx)
	'	Dim outputInfo As String = ""

	'	e.Result = outputInfo
	'End Sub

	'Private Sub Worker_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
	'	If e.ProgressPercentage = 0 Then
	'	ElseIf e.ProgressPercentage = 1 Then
	'	End If
	'End Sub

	'Private Sub Worker_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
	'	If e.Cancelled Then
	'	Else
	'	End If

	'	Me.isRunning = False
	'End Sub

	'Private isRunning As Boolean

#End Region

End Class
