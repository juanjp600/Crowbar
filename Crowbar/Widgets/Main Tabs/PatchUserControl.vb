Imports System.IO
Imports System.Text

Public Class PatchUserControl

#Region "Creation and Destruction"

	Public Sub New()
		' This call is required by the Windows Form Designer.
		InitializeComponent()
	End Sub

#End Region

#Region "Init and Free"

	Protected Overrides Sub Init()
		Me.MdlPathFileNameTextBox.DataBindings.Add("Text", TheApp.Settings, "PatchMdlPathFileName", False, DataSourceUpdateMode.OnValidation)

		Me.UpdateDataBindings()
		Me.UpdateWidgets(False)

		AddHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
		AddHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManagerEvents.ParsePathFileName
	End Sub

	' Do not need Free() because this widget is destroyed only on program exit.
	'Protected Overrides Sub Free()
	'	RemoveHandler TheApp.Settings.PropertyChanged, AddressOf AppSettings_PropertyChanged
	'	RemoveHandler Me.MdlPathFileNameTextBox.DataBindings("Text").Parse, AddressOf FileManagerEvents.ParsePathFileName

	'	Me.MdlPathFileNameTextBox.DataBindings.Clear()
	'End Sub

#End Region

#Region "Properties"
#End Region

#Region "Methods"

#End Region

#Region "Widget Event Handlers"

#End Region

#Region "Child Widget Event Handlers"

	Private Sub BrowseForMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseForMdlFileButton.Click
		Dim openFileWdw As New OpenFileDialog()

		openFileWdw.Title = "Open the MDL file you want to view"
		openFileWdw.InitialDirectory = FileManager.GetLongestExtantPath(TheApp.Settings.PatchMdlPathFileName)
		If openFileWdw.InitialDirectory = "" Then
			openFileWdw.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		End If
		openFileWdw.FileName = Path.GetFileName(TheApp.Settings.PatchMdlPathFileName)
		openFileWdw.Filter = "Source Engine Model Files (*.mdl) | *.mdl"
		openFileWdw.AddExtension = True
		openFileWdw.Multiselect = False
		openFileWdw.ValidateNames = True

		If openFileWdw.ShowDialog() = Windows.Forms.DialogResult.OK Then
			' Allow dialog window to completely disappear.
			Application.DoEvents()

			TheApp.Settings.PatchMdlPathFileName = openFileWdw.FileName
		End If
	End Sub

	Private Sub GotoMdlFileButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GotoMdlFileButton.Click
		FileManager.OpenWindowsExplorer(TheApp.Settings.PatchMdlPathFileName)
	End Sub

#End Region

#Region "Core Event Handlers"

	Private Sub AppSettings_PropertyChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs)
		'If e.PropertyName = Me.NameOfAppSettingMdlPathFileName Then
		'End If
	End Sub

#End Region

#Region "Private Properties"

#End Region

#Region "Private Methods"

	Private Sub UpdateDataBindings()
	End Sub

	Private Sub UpdateWidgets(ByVal modelViewerIsRunning As Boolean)
	End Sub

#End Region

#Region "Data"

#End Region

End Class
