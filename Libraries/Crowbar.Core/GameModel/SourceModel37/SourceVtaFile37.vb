﻿Imports System.IO

Public Class SourceVtaFile37

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData37, ByVal bodyPart As SourceMdlBodyPart37)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.theBodyPart = bodyPart
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Common.WriteHeaderComment(Me.theOutputFileStreamWriter)
	End Sub

	Public Sub WriteHeaderSection()
		Dim line As String = ""

		'version 1
		line = "version 1"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteNodesSection()
		Dim line As String = ""
		Dim name As String

		'nodes
		line = "nodes"
		Me.theOutputFileStreamWriter.WriteLine(line)

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			name = Me.theMdlFileData.theBones(boneIndex).theName

			line = "  "
			line += boneIndex.ToString(AppConstants.InternalNumberFormat)
			line += " """
			line += name
			line += """ "
			line += Me.theMdlFileData.theBones(boneIndex).parentBoneIndex.ToString(AppConstants.InternalNumberFormat)
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForVertexAnimation()
		Dim line As String = ""

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If AppSettings.Instance.DecompileStricterFormatIsChecked Then
			line = "time 0 # basis shape key"
		Else
			line = "  time 0 # basis shape key"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame37

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To Me.theBodyPart.theFlexFrames.Count - 1
			aFlexFrame = Me.theBodyPart.theFlexFrames(flexTimeIndex)

			If AppSettings.Instance.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += timeIndex.ToString(AppConstants.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			Me.theOutputFileStreamWriter.WriteLine(line)

			timeIndex += 1
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteVertexAnimationSection()
		Dim line As String = ""
		Dim aVertex As SourceMdlVertex37

		'vertexanimation
		line = "vertexanimation"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If AppSettings.Instance.DecompileStricterFormatIsChecked Then
			line = "time 0 # basis shape key"
		Else
			line = "  time 0 # basis shape key"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim beginVertexIndex As Integer = 0
		Dim endVertexIndex As Integer = 0
		Dim bodyVertexCount As Integer = 0
		Dim aBodyPart As SourceMdlBodyPart37
		Dim aModel As SourceMdlModel37
		For bodyPartIndex As Integer = 0 To Me.theMdlFileData.theBodyParts.Count - 1
			aBodyPart = Me.theMdlFileData.theBodyParts(bodyPartIndex)

			If Me.theBodyPart Is aBodyPart Then
				beginVertexIndex = bodyVertexCount
				endVertexIndex = bodyVertexCount
			End If

			If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
				For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
					aModel = aBodyPart.theModels(modelIndex)
					bodyVertexCount += aModel.vertexCount
				Next
			End If

			If Me.theBodyPart Is aBodyPart Then
				endVertexIndex = bodyVertexCount - 1
			End If
		Next

		Try
			For vertexIndex As Integer = 0 To Me.theMdlFileData.theVertexes.Count - 1
				aVertex = Me.theMdlFileData.theVertexes(vertexIndex)

				line = "    "
				line += vertexIndex.ToString(AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.position.x.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.position.z.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.normal.x.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.normal.y.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.normal.z.ToString("0.000000", AppConstants.InternalNumberFormat)
				Me.theOutputFileStreamWriter.WriteLine(line)
			Next
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame37

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To Me.theBodyPart.theFlexFrames.Count - 1
			aFlexFrame = Me.theBodyPart.theFlexFrames(flexTimeIndex)

			If AppSettings.Instance.DecompileStricterFormatIsChecked Then
				line = "time "
			Else
				line = "  time "
			End If
			line += timeIndex.ToString(AppConstants.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			Me.theOutputFileStreamWriter.WriteLine(line)

			For x As Integer = 0 To aFlexFrame.flexes.Count - 1
				Me.WriteVertexAnimLines(aFlexFrame.flexes(x), aFlexFrame.bodyAndMeshVertexIndexStarts(x))
			Next

			timeIndex += 1
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub WriteVertexAnimLines(ByVal aFlex As SourceMdlFlex37, ByVal bodyAndMeshVertexIndexStart As Integer)
		Dim line As String
		Dim aVertex As SourceMdlVertex37
		Dim vertexIndex As Integer
		Dim positionX As Double
		Dim positionY As Double
		Dim positionZ As Double
		Dim normalX As Double
		Dim normalY As Double
		Dim normalZ As Double

		For i As Integer = 0 To aFlex.theVertAnims.Count - 1
			Dim aVertAnim As SourceMdlVertAnim37
			aVertAnim = aFlex.theVertAnims(i)

			vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart
			aVertex = Me.theMdlFileData.theVertexes(vertexIndex)

			positionX = aVertex.position.x + aVertAnim.delta.x
			positionY = aVertex.position.y + aVertAnim.delta.y
			positionZ = aVertex.position.z + aVertAnim.delta.z
			normalX = aVertex.normal.x + aVertAnim.nDelta.x
			normalY = aVertex.normal.y + aVertAnim.nDelta.y
			normalZ = aVertex.normal.z + aVertAnim.nDelta.z
			line = "    "
			line += vertexIndex.ToString(AppConstants.InternalNumberFormat)
			line += " "
			line += positionX.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += positionY.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += positionZ.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += normalX.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += normalY.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += normalZ.ToString("0.000000", AppConstants.InternalNumberFormat)

			' For debugging.
			'line += " // "
			'line += aVertAnim.flDelta(0).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flDelta(1).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flDelta(2).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flNDelta(0).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flNDelta(1).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flNDelta(2).the16BitValue.ToString()

			Me.theOutputFileStreamWriter.WriteLine(line)
		Next
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData37
	Private theBodyPart As SourceMdlBodyPart37

#End Region

End Class
