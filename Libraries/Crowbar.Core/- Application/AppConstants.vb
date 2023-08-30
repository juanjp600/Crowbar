Imports System.Globalization
Imports System.Reflection

Public Module AppConstants

	Public Const WorkshopLinkStart As String = "https://steamcommunity.com/sharedfiles/filedetails/?id="
	Public Const ChangedMarker As String = "*"

    Public HelpTutorialLink As String = "tutorial.html"
    Public HelpContentsLink As String = "contents.html"
    Public HelpIndexLink As String = "index.html"
    Public HelpTipsLink As String = "tips.html"
	
	Public HeaderComment As String = "Created by Crowbar"

	Public Readonly InternalCultureInfo As CultureInfo = CultureInfo.InvariantCulture
	Public Readonly InternalNumberFormat As NumberFormatInfo = NumberFormatInfo.InvariantInfo

	Public ReadOnly EntryAssembly As Assembly = Assembly.GetEntryAssembly()

	Public Const CrowbarSteamPipeFileName As String = "CrowbarSteamPipe.exe"
	
	
	Public Function GetHeaderComment() As String
		Dim line As String

		line = "Created by "
		line += GetProductNameAndVersion()

		Return line
	End Function

	Public Function GetProductNameAndVersion() As String
		Dim result As String

		result = EntryAssembly.GetName().Name
		result += " "
		result += EntryAssembly.GetName().Version.ToString(2)

		Return result
	End Function
End Module
