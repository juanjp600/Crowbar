﻿Imports System.IO

Public Class SourceSmdFile49

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData49)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData49, ByVal vvdFileData As SourceVvdFileData04)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.theVvdFileData = vvdFileData
	End Sub

	Public Sub New(ByVal outputFileStream As StreamWriter, ByVal mdlFileData As SourceMdlFileData49, ByVal phyFileData As SourcePhyFileData)
		Me.theOutputFileStreamWriter = outputFileStream
		Me.theMdlFileData = mdlFileData
		Me.thePhyFileData = phyFileData
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
		Dim boneCount As Integer

		'nodes
		line = "nodes"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If Me.theMdlFileData.theBones Is Nothing Then
			boneCount = 0
		Else
			boneCount = Me.theMdlFileData.theBones.Count
		End If
		For boneIndex As Integer = 0 To boneCount - 1
			'Dim aBone As SourceMdlBone
			'aBone = Me.theMdlFileData.theBones(boneIndex)
			'If lodIndex = -2 AndAlso aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
			'	Continue For
			'End If

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

	Public Sub WriteSkeletonSection(ByVal lodIndex As Integer)
		Dim line As String = ""
		Dim aBone As SourceMdlBone

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If AppSettings.Instance.DecompileStricterFormatIsChecked Then
			line = "time 0"
		Else
			line = "  time 0"
		End If
		Me.theOutputFileStreamWriter.WriteLine(line)
		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)

			line = "    "
			line += boneIndex.ToString(AppConstants.InternalNumberFormat)
			'======
			line += " "
			line += aBone.position.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += aBone.position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += aBone.position.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += aBone.rotation.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += aBone.rotation.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			line += " "
			line += aBone.rotation.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'------
			''TEST: This messes up some of the hair jigglebones in all sequences of L4D2 survivor_teenangst_light.
			'If aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
			'	line += " "
			'	line += aBone.rotation.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'Else
			'	line += " "
			'	line += aBone.rotation.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'End If
			'------
			''TEST: This messes up L4D2 survivor_teenangst_light.
			'If aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
			'	line += " "
			'	line += aBone.position.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	'line += aBone.position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	'line += " "
			'	'line += (-aBone.position.x).ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.position.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	'line += " "
			'	'line += aBone.position.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	'line += " "
			'	'line += aBone.position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	'line += " "
			'	'line += "0.000000 0.000000 0.000000"
			'	'line += " "
			'	'line += aBone.rotation.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += (aBone.rotation.x + MathModule.DegreesToRadians(-90)).ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	''line += " "
			'	''line += (aBone.rotation.y + MathModule.DegreesToRadians(90)).ToString("0.000000", AppConstants.InternalNumberFormat)
			'	''line += " "
			'	''line += (aBone.rotation.z + MathModule.DegreesToRadians(-90)).ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	'line += " "
			'	'line += "0.000000 0.000000 0.000000"
			'Else
			'	If lodIndex = 0 AndAlso Me.theWeaponBoneIndex = boneIndex Then
			'		line += " "
			'		line += "0.000000 0.000000 0.000000"
			'	Else
			'		line += " "
			'		line += aBone.position.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'		line += " "
			'		line += aBone.position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'		line += " "
			'		line += aBone.position.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	End If
			'	line += " "
			'	line += aBone.rotation.x.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.y.ToString("0.000000", AppConstants.InternalNumberFormat)
			'	line += " "
			'	line += aBone.rotation.z.ToString("0.000000", AppConstants.InternalNumberFormat)
			'End If
			'======
			Me.theOutputFileStreamWriter.WriteLine(line)
		Next

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSection(ByVal aVtxModel As SourceVtxModel07, ByVal lodIndex As Integer, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer, ByVal extraUvChannelIndex As Integer)
		Dim line As String = ""
		Dim materialLine As String = ""
		Dim vertex1Line As String = ""
		Dim vertex2Line As String = ""
		Dim vertex3Line As String = ""

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim aVtxLod As SourceVtxModelLod07
		Dim aVtxMesh As SourceVtxMesh07
		Dim aStripGroup As SourceVtxStripGroup07
		'Dim cumulativeVertexCount As Integer
		'Dim maxIndexForMesh As Integer
		'Dim cumulativeMaxIndex As Integer
		Dim materialIndex As Integer
		Dim materialPathFileName As String
		Dim materialFileName As String
		Dim meshVertexIndexStart As Integer

		Try
			aVtxLod = aVtxModel.theVtxModelLods(lodIndex)

			If aVtxLod.theVtxMeshes IsNot Nothing Then
				'cumulativeVertexCount = 0
				'maxIndexForMesh = 0
				'cumulativeMaxIndex = 0
				For meshIndex As Integer = 0 To aVtxLod.theVtxMeshes.Count - 1
					aVtxMesh = aVtxLod.theVtxMeshes(meshIndex)
					materialIndex = aModel.theMeshes(meshIndex).materialIndex
					materialPathFileName = Me.theMdlFileData.theTextures(materialIndex).thePathFileName
					materialFileName = Me.theMdlFileData.theModifiedTextureFileNames(materialIndex)

					meshVertexIndexStart = aModel.theMeshes(meshIndex).vertexIndexStart

					If aVtxMesh.theVtxStripGroups IsNot Nothing Then
						If AppSettings.Instance.DecompileDebugInfoFilesIsChecked AndAlso materialPathFileName <> materialFileName Then
							materialLine = "// In the MDL file as: " + materialPathFileName
							Me.theOutputFileStreamWriter.WriteLine(materialLine)
						End If

						For groupIndex As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
							aStripGroup = aVtxMesh.theVtxStripGroups(groupIndex)

							If aStripGroup.theVtxStrips IsNot Nothing AndAlso aStripGroup.theVtxIndexes IsNot Nothing AndAlso aStripGroup.theVtxVertexes IsNot Nothing Then
								For vtxIndexIndex As Integer = 0 To aStripGroup.theVtxIndexes.Count - 3 Step 3
									''NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
									'line = materialName
									'Me.theOutputFileStreamWriter.WriteLine(line)
									'Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									'Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									'Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									'------
									'NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
									materialLine = materialFileName
									vertex1Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart, extraUvChannelIndex)
									vertex2Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart, extraUvChannelIndex)
									vertex3Line = Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart, extraUvChannelIndex)
									If vertex1Line.StartsWith("// ") OrElse vertex2Line.StartsWith("// ") OrElse vertex3Line.StartsWith("// ") Then
										materialLine = "// " + materialLine
										If Not vertex1Line.StartsWith("// ") Then
											vertex1Line = "// " + vertex1Line
										End If
										If Not vertex2Line.StartsWith("// ") Then
											vertex2Line = "// " + vertex2Line
										End If
										If Not vertex3Line.StartsWith("// ") Then
											vertex3Line = "// " + vertex3Line
										End If
									End If
									Me.theOutputFileStreamWriter.WriteLine(materialLine)
									Me.theOutputFileStreamWriter.WriteLine(vertex1Line)
									Me.theOutputFileStreamWriter.WriteLine(vertex2Line)
									Me.theOutputFileStreamWriter.WriteLine(vertex3Line)
								Next
							End If
						Next
					End If
				Next
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteTrianglesSectionForPhysics()
		Dim line As String = ""

		'triangles
		line = "triangles"
		Me.theOutputFileStreamWriter.WriteLine(line)

		Dim collisionData As SourcePhyCollisionData
		Dim aBone As SourceMdlBone
		Dim boneIndex As Integer
		Dim aTriangle As SourcePhyFace
		Dim convexMesh As SourcePhyConvexMesh
		Dim phyVertex As SourcePhyVertex
		Dim aVectorTransformed As SourceVector
		Dim aSourcePhysCollisionModel As SourcePhyPhysCollisionModel

		Try
			If Me.thePhyFileData.theSourcePhyCollisionDatas IsNot Nothing Then
				Me.ProcessTransformsForPhysics()

				For collisionDataIndex As Integer = 0 To Me.thePhyFileData.theSourcePhyCollisionDatas.Count - 1
					collisionData = Me.thePhyFileData.theSourcePhyCollisionDatas(collisionDataIndex)

					If collisionDataIndex < Me.thePhyFileData.theSourcePhyPhysCollisionModels.Count Then
						aSourcePhysCollisionModel = Me.thePhyFileData.theSourcePhyPhysCollisionModels(collisionDataIndex)
					Else
						aSourcePhysCollisionModel = Nothing
					End If

					For convexMeshIndex As Integer = 0 To collisionData.theConvexMeshes.Count - 1
						convexMesh = collisionData.theConvexMeshes(convexMeshIndex)

						' [12-Apr-2022] From RED_EYE. (He is using someone else's set of data strutures for PHY file.)
						'     flags: has_children: (self.flags >> 0) & 3  ' 0 = false; > 0 true
						'     This seems to be correct way rather than checking Me.thePhyFileData.theSourcePhyIsCollisionModel.
						'     Example where checking Me.thePhyFileData.theSourcePhyIsCollisionModel is incorrect (because the gib meshes are compiled in): 
						'         "SourceFilmmaker\game\hl2\models\combine_strider.mdl"
						If (convexMesh.flags And 3) > 0 Then
							Continue For
						End If

						If Me.theMdlFileData.theBones.Count = 1 Then
							boneIndex = 0
						Else
							boneIndex = convexMesh.theBoneIndex
							' MDL36 and MDL37 need this because their PHY does not store bone index.
							' Model versions above MDL37 can have multiple bones with same name, so this check needs to be last.
							If boneIndex < 0 Then
								If aSourcePhysCollisionModel IsNot Nothing AndAlso Me.theMdlFileData.theBoneNameToBoneIndexMap.ContainsKey(aSourcePhysCollisionModel.theName) Then
									boneIndex = Me.theMdlFileData.theBoneNameToBoneIndexMap(aSourcePhysCollisionModel.theName)
								Else
									' Not expected to reach here, but just in case, write a mesh connected to first bone instead of writing an empty mesh.
									boneIndex = 0
								End If
							End If
						End If
						aBone = Me.theMdlFileData.theBones(boneIndex)

						For triangleIndex As Integer = 0 To convexMesh.theFaces.Count - 1
							aTriangle = convexMesh.theFaces(triangleIndex)

							line = "  phy"
							Me.theOutputFileStreamWriter.WriteLine(line)

							'  19 -0.000009 0.000001 0.999953 0.0 0.0 0.0 1 0
							'  19 -0.000005 1.000002 -0.000043 0.0 0.0 0.0 1 0
							'  19 -0.008333 0.997005 1.003710 0.0 0.0 0.0 1 0
							For vertexIndex As Integer = 0 To aTriangle.vertexIndex.Length - 1
								'phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))
								phyVertex = convexMesh.theVertices(aTriangle.vertexIndex(vertexIndex))

								aVectorTransformed = Me.TransformPhyVertex(aBone, phyVertex.vertex, aSourcePhysCollisionModel)

								''DEBUG: Move different face sections away from each other.
								'aVectorTransformed.x += faceSectionIndex * 20
								'aVectorTransformed.y += faceSectionIndex * 20

								line = "    "
								line += boneIndex.ToString(AppConstants.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.x.ToString("0.000000", AppConstants.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.y.ToString("0.000000", AppConstants.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.z.ToString("0.000000", AppConstants.InternalNumberFormat)

								'line += " 0 0 0"
								'------
								line += " "
								line += phyVertex.Normal.x.ToString("0.000000", AppConstants.InternalNumberFormat)
								line += " "
								line += phyVertex.Normal.y.ToString("0.000000", AppConstants.InternalNumberFormat)
								line += " "
								line += phyVertex.Normal.z.ToString("0.000000", AppConstants.InternalNumberFormat)

								line += " 0 0"
								'NOTE: The studiomdl.exe doesn't need the integer values at end.
								'line += " 1 0"
								Me.theOutputFileStreamWriter.WriteLine(line)
							Next
						Next
					Next
				Next
			End If
		Catch ex As Exception
			Dim debug As Integer = 4242
		End Try

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDescBase As SourceMdlSequenceDescBase, ByVal anAnimationDescBase As SourceMdlAnimationDescBase, Optional onlyWriteCorrectiveAnimationRootBones As Boolean = False)
		Dim line As String = ""
		Dim aBone As SourceMdlBone
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim position As New SourceVector()
		Dim rotation As New SourceVector()
		Dim tempRotation As New SourceVector()
		Dim aSequenceDesc As SourceMdlSequenceDesc
		Dim anAnimationDesc As SourceMdlAnimationDesc49
		Dim thisIsForFirstSequence As Boolean
		Dim beforeMovementFrameRotation As New SourceVector()
		Dim previousFrameRotation As New SourceVector()
		Dim beforeMovementFramePosition As New SourceVector()
		Dim previousFramePosition As New SourceVector()

		aSequenceDesc = CType(aSequenceDescBase, SourceMdlSequenceDesc)
		anAnimationDesc = CType(anAnimationDescBase, SourceMdlAnimationDesc49)

		thisIsForFirstSequence = (anAnimationDesc.theName = "" OrElse anAnimationDesc.theName(0) = "@") AndAlso anAnimationDesc.theAnimIsLinkedToSequence AndAlso (anAnimationDesc.theLinkedSequences(0) Is Me.theMdlFileData.theSequenceDescs(0))

		'skeleton
		line = "skeleton"
		Me.theOutputFileStreamWriter.WriteLine(line)

		If Me.theMdlFileData.theBones IsNot Nothing Then
			Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()

			Dim frameCount As Integer = anAnimationDesc.frameCount
			If onlyWriteCorrectiveAnimationRootBones Then
				frameCount = 1
			End If
			For frameIndex As Integer = 0 To frameCount - 1
				Me.theAnimationFrameLines.Clear()

				If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_FRAMEANIM) <> 0) Then
					Dim sectionFrameIndex As Integer
					Dim sectionIndex As Integer
					Dim aSectionOfAnimation As SourceAniFrameAnim49
					If anAnimationDesc.sectionFrameCount = 0 Then
						sectionIndex = 0
						sectionFrameIndex = frameIndex
					Else
						sectionIndex = CInt(Math.Truncate(frameIndex / anAnimationDesc.sectionFrameCount))
						sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount)
					End If
					aSectionOfAnimation = anAnimationDesc.theSectionsOfFrameAnim(sectionIndex)

					Dim aBoneConstantInfo As BoneConstantInfo49
					Dim aBoneFrameDataInfo As BoneFrameDataInfo49

					For boneIndex = 0 To Me.theMdlFileData.theBones.Count - 1
						aBone = Me.theMdlFileData.theBones(boneIndex)

						If onlyWriteCorrectiveAnimationRootBones AndAlso aBone.parentBoneIndex <> -1 Then
							Continue For
						End If

						aFrameLine = New AnimationFrameLine()
						Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)

						aFrameLine.position = New SourceVector()
						aFrameLine.rotation = New SourceVector()

						'TODO: How to determine when to use ZERO and when to use BONE? Maybe the STUDIO_DELTA check works. Need to test it.
						'NOTE: The ZERO path works for L4D2 v_huntingrifle, v_snip_awp, v_snip_scout.
						'aFrameLine.position.x = 0
						'aFrameLine.position.y = 0
						'aFrameLine.position.z = 0
						'aFrameLine.position.debug_text = "ZERO"
						'aFrameLine.rotation.x = 0
						'aFrameLine.rotation.y = 0
						'aFrameLine.rotation.z = 0
						'aFrameLine.rotation.debug_text = "ZERO"
						'------
						'NOTE: The BONE path works for L4D2 common_male_carexit.
						'aFrameLine.position.x = aBone.position.x
						'aFrameLine.position.y = aBone.position.y
						'aFrameLine.position.z = aBone.position.z
						'aFrameLine.position.debug_text = "BONE"
						'aFrameLine.rotation.x = aBone.rotation.x
						'aFrameLine.rotation.y = aBone.rotation.y
						'aFrameLine.rotation.z = aBone.rotation.z
						'aFrameLine.rotation.debug_text = "BONE"
						'------
						'NOTE: This path works for L4D2 v_huntingrifle.
						If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
							aFrameLine.position.x = 0
							aFrameLine.position.y = 0
							aFrameLine.position.z = 0
							aFrameLine.position.debug_text = "ZERO"
							aFrameLine.rotation.x = 0
							aFrameLine.rotation.y = 0
							aFrameLine.rotation.z = 0
							aFrameLine.rotation.debug_text = "ZERO"
						Else
							aFrameLine.position.x = aBone.position.x
							aFrameLine.position.y = aBone.position.y
							aFrameLine.position.z = aBone.position.z
							aFrameLine.position.debug_text = "BONE"
							aFrameLine.rotation.x = aBone.rotation.x
							aFrameLine.rotation.y = aBone.rotation.y
							aFrameLine.rotation.z = aBone.rotation.z
							aFrameLine.rotation.debug_text = "BONE"
						End If
						'------
						''TEST: This code path did not fix the issue of the jigglebones+delta problem with L4D2 survivor_teenangst_light.
						'If aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
						'	aFrameLine.position.x = aBone.position.y
						'	aFrameLine.position.y = -aBone.position.x
						'	aFrameLine.position.z = aBone.position.z
						'	aFrameLine.position.debug_text = "JIGGLE"
						'	aFrameLine.rotation.x = aBone.rotation.x
						'	'aFrameLine.rotation.y = aBone.rotation.y
						'	'aFrameLine.rotation.z = aBone.rotation.z + MathModule.DegreesToRadians(-90)
						'	aFrameLine.rotation.y = aBone.rotation.z
						'	aFrameLine.rotation.z = aBone.rotation.y
						'	aFrameLine.rotation.debug_text = "JIGGLE"
						'ElseIf (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
						'	aFrameLine.position.x = 0
						'	aFrameLine.position.y = 0
						'	aFrameLine.position.z = 0
						'	aFrameLine.position.debug_text = "ZERO"
						'	aFrameLine.rotation.x = 0
						'	aFrameLine.rotation.y = 0
						'	aFrameLine.rotation.z = 0
						'	aFrameLine.rotation.debug_text = "ZERO"
						'Else
						'	aFrameLine.position.x = aBone.position.x
						'	aFrameLine.position.y = aBone.position.y
						'	aFrameLine.position.z = aBone.position.z
						'	aFrameLine.position.debug_text = "BONE"
						'	aFrameLine.rotation.x = aBone.rotation.x
						'	aFrameLine.rotation.y = aBone.rotation.y
						'	aFrameLine.rotation.z = aBone.rotation.z
						'	aFrameLine.rotation.debug_text = "BONE"
						'End If
						'------
						''TEST: This code path did not fix the issue of the jigglebones+delta problem with L4D2 survivor_teenangst_light.
						'If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
						'	If aBone.proceduralRuleOffset <> 0 AndAlso aBone.proceduralRuleType = SourceMdlBone.STUDIO_PROC_JIGGLE Then
						'		aFrameLine.position.x = -aBone.position.x
						'		aFrameLine.position.y = -aBone.position.y
						'		aFrameLine.position.z = -aBone.position.z
						'		aFrameLine.position.debug_text = "DELTA+JIGGLE"
						'		aFrameLine.rotation.x = aBone.rotation.x
						'		aFrameLine.rotation.y = aBone.rotation.y
						'		'aFrameLine.rotation.z = aBone.rotation.z + MathModule.DegreesToRadians(-90)
						'		aFrameLine.rotation.z = aBone.rotation.z
						'		'aFrameLine.rotation.z = aBone.rotation.y
						'		aFrameLine.rotation.debug_text = "DELTA+JIGGLE"
						'	Else
						'		aFrameLine.position.x = 0
						'		aFrameLine.position.y = 0
						'		aFrameLine.position.z = 0
						'		aFrameLine.position.debug_text = "ZERO"
						'		aFrameLine.rotation.x = 0
						'		aFrameLine.rotation.y = 0
						'		aFrameLine.rotation.z = 0
						'		aFrameLine.rotation.debug_text = "ZERO"
						'	End If
						'Else
						'	aFrameLine.position.x = aBone.position.x
						'	aFrameLine.position.y = aBone.position.y
						'	aFrameLine.position.z = aBone.position.z
						'	aFrameLine.position.debug_text = "BONE"
						'	aFrameLine.rotation.x = aBone.rotation.x
						'	aFrameLine.rotation.y = aBone.rotation.y
						'	aFrameLine.rotation.z = aBone.rotation.z
						'	aFrameLine.rotation.debug_text = "BONE"
						'End If

						Dim boneFlag As Byte
						boneFlag = aSectionOfAnimation.theBoneFlags(boneIndex)
						If aSectionOfAnimation.theBoneConstantInfos IsNot Nothing Then
							aBoneConstantInfo = aSectionOfAnimation.theBoneConstantInfos(boneIndex)
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_RAWROT) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneConstantInfo.theConstantRawRot.quaternion)
								aFrameLine.rotation.debug_text = "RAWROT"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_RAWPOS) > 0 Then
								aFrameLine.position.x = aBoneConstantInfo.theConstantRawPos.x
								aFrameLine.position.y = aBoneConstantInfo.theConstantRawPos.y
								aFrameLine.position.z = aBoneConstantInfo.theConstantRawPos.z
								aFrameLine.position.debug_text = "RAWPOS"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_CONST_POS2) > 0 Then
								aFrameLine.position.x = aBoneConstantInfo.theConstantPosition2.x
								aFrameLine.position.y = aBoneConstantInfo.theConstantPosition2.y
								aFrameLine.position.z = aBoneConstantInfo.theConstantPosition2.z
								aFrameLine.position.debug_text = "FRAME_CONST_POS2"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_CONST_ROT2) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneConstantInfo.theConstantRotation2.quaternion)
								aFrameLine.rotation.debug_text = "FRAME_CONST_ROT2"
							End If
						End If
						If aSectionOfAnimation.theBoneFrameDataInfos IsNot Nothing Then
							aBoneFrameDataInfo = aSectionOfAnimation.theBoneFrameDataInfos(sectionFrameIndex)(boneIndex)
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIMROT) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneFrameDataInfo.theAnimRotation.quaternion)
								aFrameLine.rotation.debug_text = "ANIMROT"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIMPOS) > 0 Then
								aFrameLine.position.x = aBoneFrameDataInfo.theAnimPosition.x
								aFrameLine.position.y = aBoneFrameDataInfo.theAnimPosition.y
								aFrameLine.position.z = aBoneFrameDataInfo.theAnimPosition.z
								aFrameLine.position.debug_text = "ANIMPOS"
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_FULLANIMPOS) > 0 Then
								aFrameLine.position.x = aBoneFrameDataInfo.theFullAnimPosition.x
								aFrameLine.position.y = aBoneFrameDataInfo.theFullAnimPosition.y
								aFrameLine.position.z = aBoneFrameDataInfo.theFullAnimPosition.z
								aFrameLine.position.debug_text = "FULLANIMPOS"
							End If
							If (boneFlag And &H20) > 0 Then
								Dim unknownFlagIsUsed As Integer = 4242
							End If
							If (boneFlag And SourceAniFrameAnim49.STUDIO_FRAME_ANIM_ROT2) > 0 Then
								aFrameLine.rotation = MathModule.ToEulerAngles(aBoneFrameDataInfo.theAnimRotationUnknown.quaternion)
								aFrameLine.rotation.debug_text = "FRAME_ANIM_ROT2"
							End If
						End If
					Next
				Else
					Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex, onlyWriteCorrectiveAnimationRootBones)
				End If

				If AppSettings.Instance.DecompileStricterFormatIsChecked Then
					line = "time "
				Else
					line = "  time "
				End If
				line += CStr(frameIndex)
				Me.theOutputFileStreamWriter.WriteLine(line)

				For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
					boneIndex = Me.theAnimationFrameLines.Keys(i)
					aFrameLine = Me.theAnimationFrameLines.Values(i)

					''TODO: Decompile blended anims.
					'' Doesn't seem to be direct way to get the animDesc's subtractFrameIndex.
					'' For now, do what MDL Decompiler seems to do; use zero for the animDesc's subtractFrameIndex.
					'If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0) AndAlso frameIndex = 0 AndAlso Me.theMdlFileData.theFirstAnimationDescFrameLines IsNot Nothing AndAlso Me.theMdlFileData.theFirstAnimationDescFrameLines.ContainsKey(boneIndex) Then
					'	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
					'	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

					'	If aFrameLine.position.debug_text = "desc_delta" OrElse aFrameLine.position.debug_text.StartsWith("raw") Then
					'		position.x = aFrameLine.position.x + aFirstAnimationDescFrameLine.position.x
					'		position.y = aFrameLine.position.y + aFirstAnimationDescFrameLine.position.y
					'		position.z = aFrameLine.position.z + aFirstAnimationDescFrameLine.position.z
					'		'If aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.position.debug_text = "anim+bone" Then
					'		'	'TEST: Try this version, because of "sequence_blend from Game Zombie" model.
					'		'	position.x = aFrameLine.position.y + aFirstAnimationDescFrameLine.position.x
					'		'	position.y = -aFrameLine.position.x + aFirstAnimationDescFrameLine.position.y
					'		'	position.z = aFrameLine.position.z + aFirstAnimationDescFrameLine.position.z
					'		'ElseIf aFrameLine.position.debug_text = "delta" Then
					'		'	position.x = aFrameLine.position.x + aFirstAnimationDescFrameLine.position.x
					'		'	position.y = aFrameLine.position.y + aFirstAnimationDescFrameLine.position.y
					'		'	position.z = aFrameLine.position.z + aFirstAnimationDescFrameLine.position.z
					'	Else
					'		position.x = aFrameLine.position.x
					'		position.y = aFrameLine.position.y
					'		position.z = aFrameLine.position.z
					'	End If

					'	If aFrameLine.rotation.debug_text = "desc_delta" OrElse aFrameLine.rotation.debug_text.StartsWith("raw") Then
					'		rotation.x = aFrameLine.rotation.x
					'		rotation.y = aFrameLine.rotation.y
					'		rotation.z = aFrameLine.rotation.z

					'		Dim quat As New SourceQuaternion()
					'		Dim quat2 As New SourceQuaternion()
					'		Dim quatResult As New SourceQuaternion()
					'		Dim magnitude As Double
					'		quat = MathModule.EulerAnglesToQuaternion(rotation)
					'		quat2 = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)

					'		quat.x *= -1
					'		quat.y *= -1
					'		quat.z *= -1
					'		quatResult.x = quat.w * quat2.x + quat.x * quat2.w + quat.y * quat2.z - quat.z * quat2.y
					'		quatResult.y = quat.w * quat2.y - quat.x * quat2.z + quat.y * quat2.w + quat.z * quat2.x
					'		quatResult.z = quat.w * quat2.z + quat.x * quat2.y - quat.y * quat2.x + quat.z * quat2.w
					'		quatResult.w = quat.w * quat2.w + quat.x * quat2.x + quat.y * quat2.y - quat.z * quat2.z

					'		magnitude = Math.Sqrt(quatResult.w * quatResult.w + quatResult.x * quatResult.x + quatResult.y * quatResult.y + quatResult.z * quatResult.z)
					'		quatResult.x /= magnitude
					'		quatResult.y /= magnitude
					'		quatResult.z /= magnitude
					'		quatResult.w /= magnitude

					'		'rotation = MathModule.ToEulerAngles(quatResult)
					'		tempRotation = MathModule.ToEulerAngles(quatResult)
					'		rotation.x = tempRotation.y
					'		rotation.y = tempRotation.z
					'		rotation.z = tempRotation.x
					'		'If aFrameLine.rotation.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone" Then
					'		'	rotation.x = aFrameLine.rotation.x
					'		'	rotation.y = aFrameLine.rotation.y
					'		'	rotation.z = aFrameLine.rotation.z
					'		'	'rotation.z = aFrameLine.rotation.z + MathModule.DegreesToRadians(-90)

					'		'	'TODO: Reverse this, which is probably the same as doing it forward, because of scale by -1.
					'		'	'QuaternionScale( p, s, p1 );
					'		'	'QuaternionMult( p1, q, q1 );
					'		'	'	(Q1 * Q2).w = (w1w2 - x1x2 - y1y2 - z1z2)
					'		'	'	(Q1 * Q2).x = (w1x2 + x1w2 + y1z2 - z1y2)
					'		'	'	(Q1 * Q2).y = (w1y2 - x1z2 + y1w2 + z1x2)
					'		'	'	(Q1 * Q2).z = (w1z2 + x1y2 - y1x2 + z1w2
					'		'	'QuaternionNormalize( q1 );
					'		'	'	magnitude = sqrt(w^2 + x^2 + y^2 + z^2)
					'		'	'	w = w / magnitude
					'		'	'	x = x / magnitude
					'		'	'	y = y / magnitude
					'		'	'	z = z / magnitude
					'		'	Dim quat As New SourceQuaternion()
					'		'	Dim quat2 As New SourceQuaternion()
					'		'	Dim quatResult As New SourceQuaternion()
					'		'	Dim magnitude As Double
					'		'	quat = MathModule.EulerAnglesToQuaternion(rotation)
					'		'	quat2 = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)
					'		'	'quat2 = MathModule.EulerAnglesToQuaternion(rotation)
					'		'	'quat = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)

					'		'	quat.x *= -1
					'		'	quat.y *= -1
					'		'	quat.z *= -1
					'		'	'quat.w *= -1
					'		'	quatResult.x = quat.w * quat2.x + quat.x * quat2.w + quat.y * quat2.z - quat.z * quat2.y
					'		'	quatResult.y = quat.w * quat2.y - quat.x * quat2.z + quat.y * quat2.w + quat.z * quat2.x
					'		'	quatResult.z = quat.w * quat2.z + quat.x * quat2.y - quat.y * quat2.x + quat.z * quat2.w
					'		'	quatResult.w = quat.w * quat2.w + quat.x * quat2.x + quat.y * quat2.y - quat.z * quat2.z

					'		'	magnitude = Math.Sqrt(quatResult.w * quatResult.w + quatResult.x * quatResult.x + quatResult.y * quatResult.y + quatResult.z * quatResult.z)
					'		'	quatResult.x /= magnitude
					'		'	quatResult.y /= magnitude
					'		'	quatResult.z /= magnitude
					'		'	quatResult.w /= magnitude

					'		'	rotation = MathModule.ToEulerAngles(quatResult)
					'		'ElseIf aFrameLine.rotation.debug_text = "desc_bone" Then
					'		'	rotation.x = aFrameLine.rotation.x
					'		'	rotation.y = aFrameLine.rotation.y
					'		'	rotation.z = aFrameLine.rotation.z

					'		'	Dim quat As New SourceQuaternion()
					'		'	Dim quat2 As New SourceQuaternion()
					'		'	Dim quatResult As New SourceQuaternion()
					'		'	Dim magnitude As Double
					'		'	quat = MathModule.EulerAnglesToQuaternion(rotation)
					'		'	quat2 = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)

					'		'	quat.x *= -1
					'		'	quat.y *= -1
					'		'	quat.z *= -1
					'		'	quatResult.x = quat.w * quat2.x + quat.x * quat2.w + quat.y * quat2.z - quat.z * quat2.y
					'		'	quatResult.y = quat.w * quat2.y - quat.x * quat2.z + quat.y * quat2.w + quat.z * quat2.x
					'		'	quatResult.z = quat.w * quat2.z + quat.x * quat2.y - quat.y * quat2.x + quat.z * quat2.w
					'		'	quatResult.w = quat.w * quat2.w + quat.x * quat2.x + quat.y * quat2.y - quat.z * quat2.z

					'		'	magnitude = Math.Sqrt(quatResult.w * quatResult.w + quatResult.x * quatResult.x + quatResult.y * quatResult.y + quatResult.z * quatResult.z)
					'		'	quatResult.x /= magnitude
					'		'	quatResult.y /= magnitude
					'		'	quatResult.z /= magnitude
					'		'	quatResult.w /= magnitude

					'		'	'NOTE: This gives closer match to values in the source SMD file than trying to use a different ToEulerAngles().
					'		'	tempRotation = MathModule.ToEulerAngles(quatResult)
					'		'	rotation.x = tempRotation.y
					'		'	rotation.y = tempRotation.z
					'		'	rotation.z = tempRotation.x
					'	Else
					'		rotation.x = aFrameLine.rotation.x
					'		rotation.y = aFrameLine.rotation.y
					'		rotation.z = aFrameLine.rotation.z
					'	End If
					'ElseIf Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
					'	'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
					'	'NOTE: Only adjust position if bone is a root bone. Do not know why.

					'	If aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.position.debug_text = "anim+bone" Then
					'		'TEST: Try this because of "sequence_blend from Game Zombie" model.
					'		position.x = aFrameLine.position.y
					'		position.y = -aFrameLine.position.x
					'		position.z = aFrameLine.position.z
					'	Else
					'		position.x = aFrameLine.position.x
					'		position.y = aFrameLine.position.y
					'		position.z = aFrameLine.position.z
					'	End If

					'	rotation.x = aFrameLine.rotation.x
					'	rotation.y = aFrameLine.rotation.y
					'	If aFrameLine.rotation.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone" Then
					'		'TEST: Try this because of "sequence_blend from Game Zombie" model.
					'		rotation.z = aFrameLine.rotation.z + MathModule.DegreesToRadians(-90)
					'	Else
					'		rotation.z = aFrameLine.rotation.z
					'	End If
					'Else
					'	position.x = aFrameLine.position.x
					'	position.y = aFrameLine.position.y
					'	position.z = aFrameLine.position.z

					'	rotation.x = aFrameLine.rotation.x
					'	rotation.y = aFrameLine.rotation.y
					'	rotation.z = aFrameLine.rotation.z
					'End If

					'If anAnimationDesc.theMovements IsNot Nothing AndAlso Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
					'	beforeMovementFramePosition.x = position.x
					'	beforeMovementFramePosition.y = position.y
					'	beforeMovementFramePosition.z = position.z

					'	Dim perFrameMovement As Double
					'	Dim startFrameIndex As Integer = 0
					'	For Each aMovement As SourceMdlMovement In anAnimationDesc.theMovements
					'		If frameIndex <= aMovement.endframeIndex Then
					'			beforeMovementFrameRotation.x = rotation.x
					'			beforeMovementFrameRotation.y = rotation.y
					'			beforeMovementFrameRotation.z = rotation.z

					'			''NOTE: LY means x position in the SMD.
					'			'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
					'			'	perFrameMovement = aMovement.position.y / aMovement.endframeIndex
					'			'	position.x = position.x + (perFrameMovement * frameIndex)
					'			'End If
					'			''NOTE: LX means -y position in the SMD.
					'			'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
					'			'	perFrameMovement = aMovement.position.x / aMovement.endframeIndex
					'			'	position.y = position.y - (perFrameMovement * frameIndex)
					'			'End If
					'			'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
					'			'	perFrameMovement = aMovement.position.z / aMovement.endframeIndex
					'			'	position.z = position.z + (perFrameMovement * frameIndex)
					'			'End If
					'			'------
					'			'TEST: See if motion is based on positions between two frames rather than evenly spread through all frames.
					'			'TODO: Reverse this scaling: v0 * t + 0.5 * (v1 - v0) * t * t
					'			'      		t = frameIndex / (aMovement.endframeIndex - startFrameIndex);
					'			Dim t As Double
					'			Dim scale As Double
					'			t = frameIndex / (aMovement.endframeIndex - startFrameIndex)
					'			scale = aMovement.v0 * t + 0.5 * (aMovement.v1 - aMovement.v0) * t * t
					'			If scale <> 0 Then
					'				scale = 1 / scale
					'			End If
					'			'NOTE: LY means x position in the SMD.
					'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
					'				position.x = position.x + scale * aMovement.position.y
					'				aFrameLine.position.debug_text += " [x]"
					'			End If
					'			'NOTE: LX means -y position in the SMD.
					'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
					'				'position.y = position.y - scale * aMovement.position.x
					'				'position.y = position.y + scale * aMovement.position.x
					'				'position.y = position.y - scale
					'				'position.y = position.y - scale * aMovement.position.x + previousFramePosition.y
					'				'position.y = -(scale * aMovement.position.x * position.y)
					'				'position.y = -(scale * aMovement.position.x / position.y)
					'				'position.y = scale * (position.y - aMovement.position.x)
					'				'position.y = position.y * scale
					'				'position.y = scale * aMovement.position.x
					'				'position.y = position.y * scale * aMovement.position.x
					'				'position.y = position.y * scale - aMovement.position.x
					'				'position.y = scale * aMovement.position.x
					'				'position.y = position.y - scale * aMovement.position.x - aMovement.position.x
					'				perFrameMovement = aMovement.position.x / (aMovement.endframeIndex + 1)
					'				position.y = position.y - (perFrameMovement * frameIndex)
					'				''position.y = position.y - (perFrameMovement * scale)
					'				''position.y = position.y - (perFrameMovement * scale * aMovement.position.x)
					'				''position.y = (position.y - perFrameMovement) * scale * aMovement.position.x
					'				'position.y = (position.y - perFrameMovement) * scale
					'				'position.y = position.y - (perFrameMovement * frameIndex * scale)
					'				'position.y = position.y - (perFrameMovement * frameIndex) - scale * aMovement.position.x
					'				'position.y = -(perFrameMovement * frameIndex) - scale * position.y
					'				'position.y = -(perFrameMovement * frameIndex)
					'				'position.y = -(perFrameMovement * frameIndex) + scale * position.y
					'				aFrameLine.position.debug_text += " [y]"
					'			End If
					'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
					'				position.z = position.z + scale * aMovement.position.z
					'				aFrameLine.position.debug_text += " [z]"
					'			End If

					'			'NOTE: Even if the original QC had an LXR, LYR, or LZR that was not actually used,
					'			' the compiler still stores the flag. 
					'			'------
					'			'TEST: [WORKS, except with the situation described in note above.] 
					'			'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LXR) > 0 Then
					'			'	perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / aMovement.endframeIndex
					'			'	rotation.x = rotation.x + (perFrameMovement * frameIndex)
					'			'End If
					'			'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LYR) > 0 Then
					'			'	perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / aMovement.endframeIndex
					'			'	rotation.y = rotation.y + (perFrameMovement * frameIndex)
					'			'End If
					'			'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZR) > 0 Then
					'			'	perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / aMovement.endframeIndex
					'			'	rotation.z = rotation.z + (perFrameMovement * frameIndex)
					'			'End If
					'			'TEST: [WORKS] Maybe only change x, y, or z if this frame's value changed from previous frame's value.
					'			'------
					'			'				pmove[j].angle			= RAD2DEG( anim->piecewisemove[j].rot[2] );
					'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LXR) > 0 AndAlso (frameIndex = 0 OrElse previousFrameRotation.x <> rotation.x) Then
					'				perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / aMovement.endframeIndex
					'				rotation.x = rotation.x + (perFrameMovement * frameIndex)
					'				aFrameLine.rotation.debug_text += " [x]"
					'			End If
					'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LYR) > 0 AndAlso (frameIndex = 0 OrElse previousFrameRotation.y <> rotation.y) Then
					'				perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / aMovement.endframeIndex
					'				rotation.y = rotation.y + (perFrameMovement * frameIndex)
					'				aFrameLine.rotation.debug_text += " [y]"
					'			End If
					'			If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZR) > 0 AndAlso (frameIndex = 0 OrElse previousFrameRotation.z <> rotation.z) Then
					'				perFrameMovement = MathModule.DegreesToRadians(aMovement.angle) / aMovement.endframeIndex
					'				rotation.z = rotation.z + (perFrameMovement * frameIndex)
					'				aFrameLine.rotation.debug_text += " [z]"
					'			End If

					'			previousFrameRotation.x = beforeMovementFrameRotation.x
					'			previousFrameRotation.y = beforeMovementFrameRotation.y
					'			previousFrameRotation.z = beforeMovementFrameRotation.z
					'		End If
					'	Next

					'	'previousFramePosition.x = beforeMovementFramePosition.x
					'	'previousFramePosition.y = beforeMovementFramePosition.y
					'	'previousFramePosition.z = beforeMovementFramePosition.z
					'	previousFramePosition.x = position.x
					'	previousFramePosition.y = position.y
					'	previousFramePosition.z = position.z
					'End If
					'------
					'Me.AdjustPositionAndRotation(boneIndex, aFrameLine.position, aFrameLine.rotation, position, rotation)
					'------
					'Dim adjustedPosition As New SourceVector()
					'Dim adjustedRotation As New SourceVector()
					'Me.AdjustPositionAndRotation(boneIndex, aFrameLine.position, aFrameLine.rotation, adjustedPosition, adjustedRotation)
					'Me.AdjustPositionAndRotationByPiecewiseMovement(frameIndex, boneIndex, anAnimationDesc.theMovements, adjustedPosition, adjustedRotation, position, rotation)
					'------
					Dim adjustedPosition As New SourceVector()
					Dim adjustedRotation As New SourceVector()
					Me.AdjustPositionAndRotationByPiecewiseMovement(frameIndex, boneIndex, anAnimationDesc.theMovements, aFrameLine.position, aFrameLine.rotation, adjustedPosition, adjustedRotation)
					Me.AdjustPositionAndRotation(boneIndex, adjustedPosition, adjustedRotation, thisIsForFirstSequence, position, rotation)

					If onlyWriteCorrectiveAnimationRootBones Then
						position.x = 0
						position.y = 0
						position.z = 0
					End If

					line = "    "
					line += boneIndex.ToString(AppConstants.InternalNumberFormat)

					line += " "
					line += position.x.ToString("0.000000", AppConstants.InternalNumberFormat)
					line += " "
					line += position.y.ToString("0.000000", AppConstants.InternalNumberFormat)
					line += " "
					line += position.z.ToString("0.000000", AppConstants.InternalNumberFormat)

					line += " "
					line += rotation.x.ToString("0.000000", AppConstants.InternalNumberFormat)
					line += " "
					line += rotation.y.ToString("0.000000", AppConstants.InternalNumberFormat)
					line += " "
					line += rotation.z.ToString("0.000000", AppConstants.InternalNumberFormat)

					If AppSettings.Instance.DecompileDebugInfoFilesIsChecked Then
						line += "   # "
						line += "pos: "
						line += aFrameLine.position.debug_text
						line += "   "
						line += "rot: "
						line += aFrameLine.rotation.debug_text
					End If

					Me.theOutputFileStreamWriter.WriteLine(line)
				Next
			Next
		End If

		line = "end"
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

#End Region

#Region "Private Delegates"

#End Region

#Region "Private Methods"

	Private Sub AdjustPositionAndRotation(ByVal boneIndex As Integer, ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByVal thisIsForFirstSequence As Boolean, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone
		aBone = Me.theMdlFileData.theBones(boneIndex)

		'If iPosition.debug_text = "desc_delta" OrElse iPosition.debug_text.StartsWith("delta") Then
		'	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
		'	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

		'	oPosition.x = iPosition.x + aFirstAnimationDescFrameLine.position.x
		'	oPosition.y = iPosition.y + aFirstAnimationDescFrameLine.position.y
		'	oPosition.z = iPosition.z + aFirstAnimationDescFrameLine.position.z
		'Else
		If aBone.parentBoneIndex = -1 Then
			If thisIsForFirstSequence AndAlso Me.theMdlFileData.theUpAxisYCommandWasUsed Then
				oPosition.x = iPosition.y
				oPosition.y = iPosition.z
				oPosition.z = iPosition.x
			Else
				oPosition.x = iPosition.y
				oPosition.y = -iPosition.x
				oPosition.z = iPosition.z
			End If
		Else
			oPosition.x = iPosition.x
			oPosition.y = iPosition.y
			oPosition.z = iPosition.z
		End If

		'If iRotation.debug_text = "desc_delta" OrElse iRotation.debug_text.StartsWith("delta") Then
		'	Dim aFirstAnimationDescFrameLine As AnimationFrameLine
		'	aFirstAnimationDescFrameLine = Me.theMdlFileData.theFirstAnimationDescFrameLines(boneIndex)

		'	Dim quat As New SourceQuaternion()
		'	Dim quat2 As New SourceQuaternion()
		'	Dim quatResult As New SourceQuaternion()
		'	Dim magnitude As Double
		'	quat = MathModule.EulerAnglesToQuaternion(iRotation)
		'	quat2 = MathModule.EulerAnglesToQuaternion(aFirstAnimationDescFrameLine.rotation)

		'	quat.x *= -1
		'	quat.y *= -1
		'	quat.z *= -1
		'	quatResult.x = quat.w * quat2.x + quat.x * quat2.w + quat.y * quat2.z - quat.z * quat2.y
		'	quatResult.y = quat.w * quat2.y - quat.x * quat2.z + quat.y * quat2.w + quat.z * quat2.x
		'	quatResult.z = quat.w * quat2.z + quat.x * quat2.y - quat.y * quat2.x + quat.z * quat2.w
		'	quatResult.w = quat.w * quat2.w + quat.x * quat2.x + quat.y * quat2.y - quat.z * quat2.z

		'	magnitude = Math.Sqrt(quatResult.w * quatResult.w + quatResult.x * quatResult.x + quatResult.y * quatResult.y + quatResult.z * quatResult.z)
		'	quatResult.x /= magnitude
		'	quatResult.y /= magnitude
		'	quatResult.z /= magnitude
		'	quatResult.w /= magnitude

		'	Dim tempRotation As New SourceVector()
		'	tempRotation = MathModule.ToEulerAngles(quatResult)
		'	oRotation.x = tempRotation.y
		'	oRotation.y = tempRotation.z
		'	oRotation.z = tempRotation.x
		'Else
		If aBone.parentBoneIndex = -1 Then
			If thisIsForFirstSequence AndAlso Me.theMdlFileData.theUpAxisYCommandWasUsed Then
				oRotation.x = iRotation.x + MathModule.DegreesToRadians(-90)
				oRotation.y = iRotation.y
				oRotation.z = iRotation.z + MathModule.DegreesToRadians(-90)
			Else
				oRotation.x = iRotation.x
				oRotation.y = iRotation.y
				oRotation.z = iRotation.z + MathModule.DegreesToRadians(-90)
			End If

			''TEST: 
			'If oRotation.z > Math.PI Then
			'	oRotation.z -= 2 * Math.PI
			'End If
			'If oRotation.z < -Math.PI Then
			'	oRotation.z += 2 * Math.PI
			'End If
			'ElseIf iRotation.debug_text.StartsWith("raw") OrElse iRotation.debug_text = "anim+bone" Then
			'	oRotation.x = iRotation.y
			'	oRotation.y = iRotation.x
			'	oRotation.z = iRotation.z
			'ElseIf iRotation.debug_text = "RAWROT" Then
			'	oRotation.x = iRotation.y
			'	oRotation.y = iRotation.x
			'	oRotation.z = iRotation.z
		Else
			oRotation.x = iRotation.x
			oRotation.y = iRotation.y
			oRotation.z = iRotation.z
		End If
		'------
		'If aBone.parentBoneIndex = -1 Then
		'	oPosition.x = iPosition.y
		'	oPosition.y = -iPosition.x
		'	oPosition.z = iPosition.z

		'	oRotation.x = iRotation.x
		'	oRotation.y = iRotation.y
		'	oRotation.z = iRotation.z + MathModule.DegreesToRadians(-90)
		'Else
		'	oPosition.x = iPosition.x
		'	oPosition.y = iPosition.y
		'	oPosition.z = iPosition.z

		'	oRotation.x = iRotation.x
		'	oRotation.y = iRotation.y
		'	oRotation.z = iRotation.z
		'End If
	End Sub

	'Private Sub AdjustPositionAndRotationByPiecewiseMovement(ByVal frameIndex As Integer, ByVal boneIndex As Integer, ByVal movements As List(Of SourceMdlMovement), ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
	'	Dim aBone As SourceMdlBone
	'	aBone = Me.theMdlFileData.theBones(boneIndex)

	'	oPosition.x = iPosition.x
	'	oPosition.y = iPosition.y
	'	oPosition.z = iPosition.z
	'	oPosition.debug_text = iPosition.debug_text
	'	oRotation.x = iRotation.x
	'	oRotation.y = iRotation.y
	'	oRotation.z = iRotation.z
	'	oRotation.debug_text = iRotation.debug_text

	'	If aBone.parentBoneIndex = -1 Then
	'		If frameIndex = 0 Then
	'			previousFrameRotation.x = oRotation.x
	'			previousFrameRotation.y = oRotation.y
	'			previousFrameRotation.z = oRotation.z

	'			frame0Position = iPosition
	'		End If

	'		If movements IsNot Nothing AndAlso frameIndex > 0 Then
	'			Dim beforeMovementFrameRotation As New SourceVector()
	'			Dim startFrameIndex As Integer = 0

	'			For Each aMovement As SourceMdlMovement In movements
	'				If frameIndex <= aMovement.endframeIndex Then
	'					'TEST: Using this scale works only if one axis is changed.
	'					'Dim t As Double
	'					'Dim scale As Double
	'					't = frameIndex / (aMovement.endframeIndex - startFrameIndex)
	'					'scale = aMovement.v0 * t + 0.5 * (aMovement.v1 - aMovement.v0) * t * t
	'					' ''NOTE: LY means x position in the SMD.
	'					''If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
	'					''	oPosition.x += aMovement.position.y / scale
	'					''End If
	'					' ''NOTE: LX means -y position in the SMD.
	'					''If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
	'					''	oPosition.y += aMovement.position.x / scale
	'					''End If
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
	'					'	oPosition.x += scale
	'					'End If
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
	'					'	oPosition.y += scale
	'					'End If
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
	'					'	oPosition.z += scale
	'					'End If
	'					'------
	'					'TEST: This works for all 3 axes on 3 test cases: ani_straight_line, ani_straight_line_diagonal, ani_zigzag
	'					'Dim t As Double
	'					't = frameIndex / (aMovement.endframeIndex - startFrameIndex)
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
	'					'	oPosition.x += t * aMovement.position.x
	'					'End If
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
	'					'	oPosition.y += t * aMovement.position.y
	'					'End If
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
	'					'	oPosition.z += t * aMovement.position.z
	'					'End If
	'					'------
	'					'TEST: This works for all 3 axes on these test cases: ani_bouncy, ani_rotate, ani_straight_line, ani_straight_line_diagonal, ani_swirl, ani_zigzag
	'					'Dim t As Double
	'					'Dim scale As Double
	'					'Dim differencePosition As New SourceVector()
	'					't = (frameIndex - startFrameIndex) / (aMovement.endframeIndex - startFrameIndex)
	'					'scale = aMovement.v0 + 0.5 * (aMovement.v1 - aMovement.v0)
	'					'If scale <> 0 Then
	'					'	differencePosition.x = aMovement.position.x / scale
	'					'	differencePosition.y = aMovement.position.y / scale
	'					'	differencePosition.z = aMovement.position.z / scale
	'					'	scale = aMovement.v0 * t + 0.5 * (aMovement.v1 - aMovement.v0) * t * t
	'					'	If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LX) > 0 Then
	'					'		oPosition.x += scale * differencePosition.x
	'					'	End If
	'					'	If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LY) > 0 Then
	'					'		oPosition.y += scale * differencePosition.y
	'					'	End If
	'					'	If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZ) > 0 Then
	'					'		oPosition.z += scale * differencePosition.z
	'					'	End If
	'					'End If
	'					'------
	'					'TEST: Works for same cases as above TEST.
	'					'Dim t As Double
	'					'Dim scale As Double
	'					'Dim differencePosition As New SourceVector()
	'					't = (frameIndex - startFrameIndex) / (aMovement.endframeIndex - startFrameIndex)
	'					'scale = aMovement.v0 + 0.5 * (aMovement.v1 - aMovement.v0)
	'					'If scale <> 0 Then
	'					'	differencePosition.x = aMovement.position.x / scale
	'					'	differencePosition.y = aMovement.position.y / scale
	'					'	differencePosition.z = aMovement.position.z / scale

	'					'	scale = aMovement.v0 * t + 0.5 * (aMovement.v1 - aMovement.v0) * t * t

	'					'	Dim adjmatrixColumn0 As New SourceVector()
	'					'	Dim adjmatrixColumn1 As New SourceVector()
	'					'	Dim adjmatrixColumn2 As New SourceVector()
	'					'	Dim adjmatrixColumn3 As New SourceVector()
	'					'	MathModule.AngleMatrix(0, 0, 0, adjmatrixColumn0, adjmatrixColumn1, adjmatrixColumn2, adjmatrixColumn3)
	'					'	adjmatrixColumn3.x = scale * differencePosition.x
	'					'	adjmatrixColumn3.y = scale * differencePosition.y
	'					'	adjmatrixColumn3.z = scale * differencePosition.z

	'					'	'AngleMatrix( panim->sanim[j+iStartFrame][k].rot, panim->sanim[j+iStartFrame][k].pos, bonematrix );
	'					'	Dim boneMatrixColumn0 As New SourceVector()
	'					'	Dim boneMatrixColumn1 As New SourceVector()
	'					'	Dim boneMatrixColumn2 As New SourceVector()
	'					'	Dim boneMatrixColumn3 As New SourceVector()
	'					'	MathModule.AngleMatrix(0, 0, 0, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
	'					'	boneMatrixColumn3.x = oPosition.x
	'					'	boneMatrixColumn3.y = oPosition.y
	'					'	boneMatrixColumn3.z = oPosition.z
	'					'	'ConcatTransforms( adjmatrix, bonematrix, bonematrix );
	'					'	Dim newBoneMatrixColumn0 As New SourceVector()
	'					'	Dim newBoneMatrixColumn1 As New SourceVector()
	'					'	Dim newBoneMatrixColumn2 As New SourceVector()
	'					'	Dim newBoneMatrixColumn3 As New SourceVector()
	'					'	MathModule.R_ConcatTransforms(adjmatrixColumn0, adjmatrixColumn1, adjmatrixColumn2, adjmatrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3, newBoneMatrixColumn0, newBoneMatrixColumn1, newBoneMatrixColumn2, newBoneMatrixColumn3)
	'					'	'MatrixAngles( bonematrix, panim->sanim[j+iStartFrame][k].rot, panim->sanim[j+iStartFrame][k].pos );
	'					'	oPosition.x = newBoneMatrixColumn3.x
	'					'	oPosition.y = newBoneMatrixColumn3.y
	'					'	oPosition.z = newBoneMatrixColumn3.z
	'					'End If

	'					'beforeMovementFrameRotation.x = oRotation.x
	'					'beforeMovementFrameRotation.y = oRotation.y
	'					'beforeMovementFrameRotation.z = oRotation.z

	'					''If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LXR) > 0 AndAlso (previousFrameRotation.x <> oRotation.x) Then
	'					''	oRotation.x += scale * aMovement.vector.x
	'					''End If
	'					''If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LYR) > 0 AndAlso (previousFrameRotation.y <> oRotation.y) Then
	'					''	oRotation.y += scale * aMovement.vector.y
	'					''End If
	'					''If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZR) > 0 AndAlso (previousFrameRotation.z <> oRotation.z) Then
	'					''	oRotation.z += scale * aMovement.vector.z
	'					''End If
	'					''------
	'					'If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZR) > 0 AndAlso (previousFrameRotation.z <> oRotation.z) Then
	'					'	oRotation.z += t * MathModule.DegreesToRadians(aMovement.angle)
	'					'End If

	'					'previousFrameRotation.x = beforeMovementFrameRotation.x
	'					'previousFrameRotation.y = beforeMovementFrameRotation.y
	'					'previousFrameRotation.z = beforeMovementFrameRotation.z
	'					'------
	'					'TEST: Works for the test cases, except for: ani_3axis_multirotate_zigzag, ani_3axis_rotate_zigzag, ani_rotate_zigzag
	'					Dim t As Double
	'					Dim scale As Double
	'					Dim differencePosition As New SourceVector()
	'					t = (frameIndex - startFrameIndex) / (aMovement.endframeIndex - startFrameIndex)
	'					scale = aMovement.v0 + 0.5 * (aMovement.v1 - aMovement.v0)
	'					If scale = 0 Then
	'						differencePosition.x = 0
	'						differencePosition.y = 0
	'						differencePosition.z = 0
	'					Else
	'						differencePosition.x = aMovement.position.x / scale
	'						differencePosition.y = aMovement.position.y / scale
	'						differencePosition.z = aMovement.position.z / scale
	'					End If

	'					scale = aMovement.v0 * t + 0.5 * (aMovement.v1 - aMovement.v0) * t * t

	'					'RadianEuler	rot( 0, 0, 0 );
	'					Dim rot As New SourceVector()
	'					rot.x = 0
	'					rot.y = 0
	'					rot.z = 0
	'					If (aMovement.motionFlags And SourceMdlMovement.STUDIO_LXR) > 0 OrElse (aMovement.motionFlags And SourceMdlMovement.STUDIO_LYR) > 0 OrElse (aMovement.motionFlags And SourceMdlMovement.STUDIO_LZR) > 0 Then
	'						'TODO: Fill-in other stuff here.

	'						rot.z = MathModule.DegreesToRadians(aMovement.angle)
	'					End If

	'					'VectorScale( rot, t, adjangle );
	'					Dim adjangle As New SourceVector()
	'					adjangle.x = rot.x * t
	'					adjangle.y = rot.y * t
	'					adjangle.z = rot.z * t

	'					'AngleMatrix( adjangle, adjpos, adjmatrix );
	'					Dim adjmatrixColumn0 As New SourceVector()
	'					Dim adjmatrixColumn1 As New SourceVector()
	'					Dim adjmatrixColumn2 As New SourceVector()
	'					Dim adjmatrixColumn3 As New SourceVector()
	'					'MathModule.AngleMatrix(adjangle.x, adjangle.y, adjangle.z, adjmatrixColumn0, adjmatrixColumn1, adjmatrixColumn2, adjmatrixColumn3)
	'					MathModule.AngleMatrix(adjangle.y, adjangle.z, adjangle.x, adjmatrixColumn0, adjmatrixColumn1, adjmatrixColumn2, adjmatrixColumn3)
	'					adjmatrixColumn3.x = differencePosition.x * scale
	'					adjmatrixColumn3.y = differencePosition.y * scale
	'					adjmatrixColumn3.z = differencePosition.z * scale

	'					'AngleMatrix( panim->sanim[j+iStartFrame][k].rot, panim->sanim[j+iStartFrame][k].pos, bonematrix );
	'					Dim boneMatrixColumn0 As New SourceVector()
	'					Dim boneMatrixColumn1 As New SourceVector()
	'					Dim boneMatrixColumn2 As New SourceVector()
	'					Dim boneMatrixColumn3 As New SourceVector()
	'					'MathModule.AngleMatrix(oRotation.x, oRotation.y, oRotation.z, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
	'					MathModule.AngleMatrix(oRotation.y, oRotation.z, oRotation.x, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3)
	'					boneMatrixColumn3.x = oPosition.x
	'					boneMatrixColumn3.y = oPosition.y
	'					boneMatrixColumn3.z = oPosition.z

	'					'ConcatTransforms( adjmatrix, bonematrix, bonematrix );
	'					Dim newBoneMatrixColumn0 As New SourceVector()
	'					Dim newBoneMatrixColumn1 As New SourceVector()
	'					Dim newBoneMatrixColumn2 As New SourceVector()
	'					Dim newBoneMatrixColumn3 As New SourceVector()
	'					MathModule.R_ConcatTransforms(adjmatrixColumn0, adjmatrixColumn1, adjmatrixColumn2, adjmatrixColumn3, boneMatrixColumn0, boneMatrixColumn1, boneMatrixColumn2, boneMatrixColumn3, newBoneMatrixColumn0, newBoneMatrixColumn1, newBoneMatrixColumn2, newBoneMatrixColumn3)

	'					'MatrixAngles( bonematrix, panim->sanim[j+iStartFrame][k].rot, panim->sanim[j+iStartFrame][k].pos );
	'					'MathModule.MatrixAnglesInRadians(newBoneMatrixColumn0, newBoneMatrixColumn1, newBoneMatrixColumn2, newBoneMatrixColumn3, oRotation.x, oRotation.y, oRotation.z)
	'					MathModule.MatrixAnglesInRadians(newBoneMatrixColumn0, newBoneMatrixColumn1, newBoneMatrixColumn2, newBoneMatrixColumn3, oRotation.y, oRotation.z, oRotation.x)
	'					oPosition.x = newBoneMatrixColumn3.x
	'					oPosition.y = newBoneMatrixColumn3.y
	'					oPosition.z = newBoneMatrixColumn3.z

	'					'TEST: 
	'					If oRotation.z > Math.PI Then
	'						oRotation.z -= 2 * Math.PI
	'					End If
	'					If oRotation.z < -Math.PI Then
	'						oRotation.z += 2 * Math.PI
	'					End If
	'				Else
	'					'TODO: Use the last calculated movement for remaining frames?
	'					Dim debug As Integer = 4242
	'				End If

	'				startFrameIndex = aMovement.endframeIndex + 1
	'			Next
	'		End If
	'	End If
	'End Sub
	'FROM: [48] SourceEngine2007_source se2007_src\src_main\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: calculate changes in position and angle between two points in a sequences cycle
	'// Output:	updated position and angle, relative to CycleFrom being at the origin
	'//			returns false if sequence is not a movement sequence
	'//-----------------------------------------------------------------------------
	'
	'bool Studio_SeqMovement( const CStudioHdr *pStudioHdr, int iSequence, float flCycleFrom, float flCycleTo, const float poseParameter[], Vector &deltaPos, QAngle &deltaAngles )
	'{
	'	mstudioanimdesc_t *panim[4];
	'	float	weight[4];
	'
	'	mstudioseqdesc_t &seqdesc = pStudioHdr->pSeqdesc( iSequence );
	'
	'	Studio_SeqAnims( pStudioHdr, seqdesc, iSequence, poseParameter, panim, weight );
	'	
	'	deltaPos.Init( );
	'	deltaAngles.Init( );
	'
	'	bool found = false;
	'
	'	for (int i = 0; i < 4; i++)
	'	{
	'		if (weight[i])
	'		{
	'			Vector localPos;
	'			QAngle localAngles;
	'
	'			localPos.Init();
	'			localAngles.Init();
	'
	'			if (Studio_AnimMovement( panim[i], flCycleFrom, flCycleTo, localPos, localAngles ))
	'			{
	'				found = true;
	'				deltaPos = deltaPos + localPos * weight[i];
	'				// FIXME: this makes no sense
	'				deltaAngles = deltaAngles + localAngles * weight[i];
	'			}
	'			else if (!(panim[i]->flags & STUDIO_DELTA) && panim[i]->nummovements == 0 && seqdesc.weight(0) > 0.0)
	'			{
	'				found = true;
	'			}
	'		}
	'	}
	'	return found;
	'}
	'bool Studio_AnimMovement( mstudioanimdesc_t *panim, float flCycleFrom, float flCycleTo, Vector &deltaPos, QAngle &deltaAngle )
	'{
	'	if (panim->nummovements == 0)
	'		return false;
	'
	'	Vector startPos;
	'	QAngle startA;
	'	Studio_AnimPosition( panim, flCycleFrom, startPos, startA );
	'
	'	Vector endPos;
	'	QAngle endA;
	'	Studio_AnimPosition( panim, flCycleTo, endPos, endA );
	'
	'	Vector tmp = endPos - startPos;
	'	deltaAngle.y = endA.y - startA.y;
	'	VectorYawRotate( tmp, -startA.y, deltaPos );
	'
	'	return true;
	'}
	'// Output:	updated position and angle, relative to the origin
	'//			returns false if animation is not a movement animation
	'//-----------------------------------------------------------------------------
	'
	'bool Studio_AnimPosition( mstudioanimdesc_t *panim, float flCycle, Vector &vecPos, QAngle &vecAngle )
	'{
	'	float	prevframe = 0;
	'	vecPos.Init( );
	'	vecAngle.Init( );
	'
	'	if (panim->nummovements == 0)
	'		return false;
	'
	'	//ZM: flCycle is frame fraction of an animation's full count of frames. 
	'	      For example, 0.5 means middle frame (not always an integer) of the animation; 1 means ending frame of the animation.
	'	//ZM: iLoops = 0 if flCycle is between 0 and 1.
	'	//ZM: Thus, for Crowbar, iLoops = 0 and flFrame = frameIndex.
	'	int iLoops = 0;
	'	if (flCycle > 1.0)
	'	{
	'		iLoops = (int)flCycle;
	'	}
	'	else if (flCycle < 0.0)
	'	{
	'		iLoops = (int)flCycle - 1;
	'	}
	'	flCycle = flCycle - iLoops;
	'
	'	float	flFrame = flCycle * (panim->numframes - 1);
	'
	'	for (int i = 0; i < panim->nummovements; i++)
	'	{
	'		mstudiomovement_t *pmove = panim->pMovement( i );
	'
	'		if (pmove->endframe >= flFrame)
	'		{
	'			float f = (flFrame - prevframe) / (pmove->endframe - prevframe);
	'
	'			float d = pmove->v0 * f + 0.5 * (pmove->v1 - pmove->v0) * f * f;
	'
	'			vecPos = vecPos + d * pmove->vector;
	'			vecAngle.y = vecAngle.y * (1 - f) + pmove->angle * f;
	'			if (iLoops != 0)
	'			{
	'				mstudiomovement_t *pmove = panim->pMovement( panim->nummovements - 1 );
	'				vecPos = vecPos + iLoops * pmove->position; 
	'				vecAngle.y = vecAngle.y + iLoops * pmove->angle; 
	'			}
	'			return true;
	'		}
	'		else
	'		{
	'			prevframe = pmove->endframe;
	'			vecPos = pmove->position;
	'			vecAngle.y = pmove->angle;
	'		}
	'	}
	'
	'	return false;
	'}
	'FROM: [48] SourceEngine2007_source se2007_src\src_main\public\mathlib\mathlib.h
	'// rotate a vector around the Z axis (YAW)
	'void VectorYawRotate( const Vector& in, float flYaw, Vector &out);
	Private Sub AdjustPositionAndRotationByPiecewiseMovement(ByVal frameIndex As Integer, ByVal boneIndex As Integer, ByVal movements As List(Of SourceMdlMovement), ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)
		Dim aBone As SourceMdlBone
		aBone = Me.theMdlFileData.theBones(boneIndex)

		oPosition.x = iPosition.x
		oPosition.y = iPosition.y
		oPosition.z = iPosition.z
		oPosition.debug_text = iPosition.debug_text
		oRotation.x = iRotation.x
		oRotation.y = iRotation.y
		oRotation.z = iRotation.z
		oRotation.debug_text = iRotation.debug_text

		If aBone.parentBoneIndex = -1 Then
			If movements IsNot Nothing AndAlso frameIndex > 0 Then
				Dim previousEndFrameIndex As Integer
				Dim vecPos As SourceVector
				Dim vecAngle As SourceVector

				previousEndFrameIndex = 0
				vecPos = New SourceVector()
				vecAngle = New SourceVector()

				For Each aMovement As SourceMdlMovement In movements
					If frameIndex <= aMovement.endframeIndex Then
						Dim f As Double
						Dim d As Double
						f = (frameIndex - previousEndFrameIndex) / (aMovement.endframeIndex - previousEndFrameIndex)
						d = aMovement.v0 * f + 0.5 * (aMovement.v1 - aMovement.v0) * f * f
						vecPos.x = vecPos.x + d * aMovement.vector.x
						vecPos.y = vecPos.y + d * aMovement.vector.y
						vecPos.z = vecPos.z + d * aMovement.vector.z
						vecAngle.y = vecAngle.y * (1 - f) + aMovement.angle * f

						Exit For
					Else
						previousEndFrameIndex = aMovement.endframeIndex
						vecPos.x = aMovement.position.x
						vecPos.y = aMovement.position.y
						vecPos.z = aMovement.position.z
						vecAngle.y = aMovement.angle
					End If
				Next

				'TEST: Does not work for various rotations.
				'Dim tmp As New SourceVector()
				'tmp.x = iPosition.x + vecPos.x
				'tmp.y = iPosition.y + vecPos.y
				'tmp.z = iPosition.z + vecPos.z
				'oRotation.z = iRotation.z + vecAngle.y
				'oPosition = MathModule.VectorYawRotate(tmp, -vecAngle.y)
				'------
				'Dim tmp As New SourceVector()
				'tmp.x = iPosition.x + vecPos.x
				'tmp.y = iPosition.y + vecPos.y
				'tmp.z = iPosition.z + vecPos.z
				'oRotation.z = iRotation.z + vecAngle.y
				'oPosition = MathModule.VectorYawRotateXandYSwap(tmp, -vecAngle.y)
				'------
				'tmp.x = iPosition.y + vecPos.y
				'tmp.y = -(iPosition.x + vecPos.x)
				'tmp.z = iPosition.z + vecPos.z
				'tmp.x = iPosition.y + vecPos.x
				'tmp.y = -(iPosition.x + vecPos.y)
				'tmp.z = iPosition.z + vecPos.z
				'tmp.x = -(iPosition.x + vecPos.y)
				'tmp.y = iPosition.y + vecPos.x
				'tmp.z = iPosition.z + vecPos.z
				'oPosition.x = iPosition.x + vecPos.x
				'oPosition.y = iPosition.y + vecPos.y
				'oPosition.z = iPosition.z + vecPos.z
				'oRotation.z = iRotation.z + vecAngle.y
				'oPosition = MathModule.VectorYawRotate(tmp, -vecAngle.y)
				'------
				'TEST: Works for translation and maybe rotation on Z, but ignores other axis rotations because the above does not work for rotations.
				'oPosition.x = iPosition.x + vecPos.x
				'oPosition.y = iPosition.y + vecPos.y
				'oPosition.z = iPosition.z + vecPos.z
				'oRotation.z = iRotation.z + vecAngle.y
				'------
				Dim tmp As New SourceVector()
				oRotation.z = MathModule.DegreesToRadians(MathModule.RadiansToDegrees(iRotation.z) + vecAngle.y)
				tmp = MathModule.VectorYawRotate(iPosition, MathModule.DegreesToRadians(vecAngle.y))
				oPosition.x = tmp.x + vecPos.x
				oPosition.y = tmp.y + vecPos.y
				oPosition.z = tmp.z + vecPos.z
			End If
		End If
	End Sub

	Private Sub AdjustPositionAndRotationForDelta(ByVal iPosition As SourceVector, ByVal iRotation As SourceVector, ByRef oPosition As SourceVector, ByRef oRotation As SourceVector)

	End Sub

	Private Sub GetFrameInfo(ByVal frameIndex As Integer, ByVal boneIndex As Integer)
		'Me.theAnimationFrameLines.Clear()
		'If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
		'	If ((anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_FRAMEANIM) <> 0) Then
		'	Else
		'		Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
		'	End If
		'End If

		'boneIndex = Me.theAnimationFrameLines.Keys(0)
		'aFrameLine = Me.theAnimationFrameLines.Values(0)

		'If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 Then
		'	'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		'	'NOTE: Only adjust position if bone is a root bone. Do not know why.

		'	If aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.position.debug_text = "anim+bone" Then
		'		'TEST: Try this because of "sequence_blend from Game Zombie" model.
		'		position.x = aFrameLine.position.y
		'		position.y = -aFrameLine.position.x
		'		position.z = aFrameLine.position.z
		'	Else
		'		position.x = aFrameLine.position.x
		'		position.y = aFrameLine.position.y
		'		position.z = aFrameLine.position.z
		'	End If

		'	rotation.x = aFrameLine.rotation.x
		'	rotation.y = aFrameLine.rotation.y
		'	If aFrameLine.rotation.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone" Then
		'		'TEST: Try this because of "sequence_blend from Game Zombie" model.
		'		rotation.z = aFrameLine.rotation.z + MathModule.DegreesToRadians(-90)
		'	Else
		'		rotation.z = aFrameLine.rotation.z
		'	End If
		'End If
	End Sub

	Private Function WriteVertexLine(ByVal aStripGroup As SourceVtxStripGroup07, ByVal aVtxIndexIndex As Integer, ByVal lodIndex As Integer, ByVal meshVertexIndexStart As Integer, ByVal bodyPartVertexIndexStart As Integer, ByVal extraUvChannelIndex As Integer) As String
		Dim aVtxVertexIndex As UShort
		Dim aVtxVertex As SourceVtxVertex07
		Dim aVertex As SourceVertex
		Dim vertexIndex As Integer
		Dim line As String

		line = ""
		Try
			aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
			aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
			vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
			If Me.theVvdFileData.fixupCount = 0 Then
				aVertex = Me.theVvdFileData.theVertexes(vertexIndex)
			Else
				'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
				'      Maybe the listing by lodIndex is only needed internally by graphics engine.
				'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				aVertex = Me.theVvdFileData.theFixedVertexesByLod(0)(vertexIndex)
				'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
			End If

			line = "  "
			line += aVertex.boneWeight.bone(0).ToString(AppConstants.InternalNumberFormat)

			line += " "
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				'NOTE: This does not work for L4D2 w_models\weapons\w_minigun.mdl.
				line += aVertex.positionY.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += (-aVertex.positionX).ToString("0.000000", AppConstants.InternalNumberFormat)
			Else
				'NOTE: This works for L4D2 w_models\weapons\w_minigun.mdl.
				line += aVertex.positionX.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.positionY.ToString("0.000000", AppConstants.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.positionZ.ToString("0.000000", AppConstants.InternalNumberFormat)

			line += " "
			If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				line += aVertex.normalY.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += (-aVertex.normalX).ToString("0.000000", AppConstants.InternalNumberFormat)
			Else
				line += aVertex.normalX.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.normalY.ToString("0.000000", AppConstants.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.normalZ.ToString("0.000000", AppConstants.InternalNumberFormat)

			If extraUvChannelIndex = -1 Then
				line += " "
				line += aVertex.texCoordX.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += (1 - aVertex.texCoordY).ToString("0.000000", AppConstants.InternalNumberFormat)
			Else
				Dim u As Double = Me.theVvdFileData.theExtraDatas(extraUvChannelIndex).theTextureCoordinates(vertexIndex).X
				Dim v As Double = Me.theVvdFileData.theExtraDatas(extraUvChannelIndex).theTextureCoordinates(vertexIndex).Y
				line += " "
				line += u.ToString("0.000000", AppConstants.InternalNumberFormat)
				line += " "
				line += (1 - v).ToString("0.000000", AppConstants.InternalNumberFormat)
			End If

			line += " "
			line += aVertex.boneWeight.boneCount.ToString(AppConstants.InternalNumberFormat)
			For boneWeightBoneIndex As Integer = 0 To aVertex.boneWeight.boneCount - 1
				line += " "
				line += aVertex.boneWeight.bone(boneWeightBoneIndex).ToString(AppConstants.InternalNumberFormat)
				line += " "
				line += aVertex.boneWeight.weight(boneWeightBoneIndex).ToString("0.000000", AppConstants.InternalNumberFormat)
			Next
		Catch ex As Exception
			line = "// " + line
		End Try

		Return line
	End Function

	Private Sub CalculateFirstAnimDescFrameLinesForPhysics(ByRef aFirstAnimationDescFrameLine As AnimationFrameLine)
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine
		Dim frameIndex As Integer
		Dim frameLineIndex As Integer
		Dim aSequenceDesc As SourceMdlSequenceDesc
		Dim anAnimationDesc As SourceMdlAnimationDesc49

		aSequenceDesc = Nothing
		anAnimationDesc = Me.theMdlFileData.theAnimationDescs(0)

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		frameIndex = 0
		Me.theAnimationFrameLines.Clear()
		'If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
		Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
		'End If

		frameLineIndex = 0
		boneIndex = Me.theAnimationFrameLines.Keys(frameLineIndex)
		aFrameLine = Me.theAnimationFrameLines.Values(frameLineIndex)

		aFirstAnimationDescFrameLine.rotation = New SourceVector()
		aFirstAnimationDescFrameLine.position = New SourceVector()

		aFirstAnimationDescFrameLine.rotation.x = aFrameLine.rotation.x
		aFirstAnimationDescFrameLine.rotation.y = aFrameLine.rotation.y
		'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		'	Dim z As Double
		'	z = aFrameLine.rotation.z
		'	z += MathModule.DegreesToRadians(-90)
		'	aFirstAnimationDescFrameLine.rotation.z = z
		'Else
		aFirstAnimationDescFrameLine.rotation.z = aFrameLine.rotation.z
		'End If

		''NOTE: Only adjust position if bone is a root bone. Do not know why.
		''If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
		''TEST: Try this version, because of "sequence_blend from Game Zombie" model.
		'If Me.theMdlFileData.theBones(boneIndex).parentBoneIndex = -1 AndAlso (aFrameLine.position.debug_text.StartsWith("raw") OrElse aFrameLine.rotation.debug_text = "anim+bone") Then
		'	aFirstAnimationDescFrameLine.position.x = aFrameLine.position.y
		'	aFirstAnimationDescFrameLine.position.y = (-aFrameLine.position.x)
		'	aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
		'Else
		aFirstAnimationDescFrameLine.position.x = aFrameLine.position.x
		aFirstAnimationDescFrameLine.position.y = aFrameLine.position.y
		aFirstAnimationDescFrameLine.position.z = aFrameLine.position.z
		'End If
	End Sub

	Private Sub ProcessTransformsForPhysics()
		If Me.thePhyFileData.theSourcePhyCollisionDatas.Count = 1 Then
			Dim aFirstAnimationDescFrameLine As New AnimationFrameLine()
			Me.CalculateFirstAnimDescFrameLinesForPhysics(aFirstAnimationDescFrameLine)

			Dim position As SourceVector
			Dim rotation As SourceVector
			position = aFirstAnimationDescFrameLine.position
			rotation = aFirstAnimationDescFrameLine.rotation

			'MathModule.AngleMatrix(rotation.y, rotation.z, rotation.x, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'Me.worldToPoseColumn3.x = position.x
			'Me.worldToPoseColumn3.y = position.y
			'Me.worldToPoseColumn3.z = position.z
			'------
			'MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
			'poseToWorldColumn3.x = position.x
			'poseToWorldColumn3.y = position.y
			'poseToWorldColumn3.z = position.z
			'MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
			'------
			MathModule.AngleMatrix(rotation.x, rotation.y, rotation.z + MathModule.DegreesToRadians(-90), poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
			poseToWorldColumn3.x = position.y
			poseToWorldColumn3.y = -position.x
			poseToWorldColumn3.z = position.z
			MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
		End If
	End Sub

	Private Function TransformPhyVertex(ByVal aBone As SourceMdlBone, ByVal vertex As SourceVector, ByVal aSourcePhysCollisionModel As SourcePhyPhysCollisionModel) As SourceVector
		Dim aVectorTransformed As New SourceVector
		Dim aVector As New SourceVector()

		'NOTE: Too small.
		'aVectorTransformed.x = vertex.x
		'aVectorTransformed.y = vertex.y
		'aVectorTransformed.z = vertex.z
		'------
		'NOTE: Rotated for:
		'      simple_shape
		'      L4D2 w_models\weapons\w_minigun
		'aVectorTransformed.x = 1 / 0.0254 * vertex.x
		'aVectorTransformed.y = 1 / 0.0254 * vertex.y
		'aVectorTransformed.z = 1 / 0.0254 * vertex.z
		'------
		'NOTE: Works for:
		'      simple_shape
		'      L4D2 w_models\weapons\w_minigun
		'      L4D2 w_models\weapons\w_smg_uzi
		'      L4D2 props_vehicles\van
		'aVectorTransformed.x = 1 / 0.0254 * vertex.z
		'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
		'aVectorTransformed.z = 1 / 0.0254 * -vertex.y
		'------
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_minigun
		'aVectorTransformed.x = 1 / 0.0254 * vertex.x
		'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
		'aVectorTransformed.z = 1 / 0.0254 * vertex.z
		'------
		'NOTE: Rotated for:
		'      L4D2 props_vehicles\van
		'aVectorTransformed.x = 1 / 0.0254 * vertex.z
		'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
		'aVectorTransformed.z = 1 / 0.0254 * vertex.x
		'------
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_minigun
		'aVector.x = 1 / 0.0254 * vertex.x
		'aVector.y = 1 / 0.0254 * vertex.y
		'aVector.z = 1 / 0.0254 * vertex.z
		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'------
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_minigun
		'aVector.x = 1 / 0.0254 * vertex.x
		'aVector.y = 1 / 0.0254 * -vertex.y
		'aVector.z = 1 / 0.0254 * vertex.z
		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'------
		'NOTE: Works for:
		'      L4D2 w_models\weapons\w_minigun
		'      L4D2 w_models\weapons\w_smg_uzi
		'NOTE: Rotated for:
		'      simple_shape
		'      L4D2 props_vehicles\van
		'NOTE: Each mesh piece rotated for:
		'      L4D2 survivors\survivor_producer
		'aVector.x = 1 / 0.0254 * vertex.z
		'aVector.y = 1 / 0.0254 * -vertex.y
		'aVector.z = 1 / 0.0254 * vertex.x
		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'------
		'NOTE: Works for:
		'      simple_shape
		'      L4D2 props_vehicles\van
		'      L4D2 survivors\survivor_producer
		'      L4D2 w_models\weapons\w_autoshot_m4super
		'      L4D2 w_models\weapons\w_desert_eagle
		'      L4D2 w_models\weapons\w_minigun
		'      L4D2 w_models\weapons\w_rifle_m16a2
		'      L4D2 w_models\weapons\w_smg_uzi
		'NOTE: Rotated for:
		'      L4D2 w_models\weapons\w_desert_rifle
		'      L4D2 w_models\weapons\w_shotgun_spas
		'If Me.thePhyFileData.theSourcePhyIsCollisionModel Then
		'	aVectorTransformed.x = 1 / 0.0254 * vertex.z
		'	aVectorTransformed.y = 1 / 0.0254 * -vertex.x
		'	aVectorTransformed.z = 1 / 0.0254 * -vertex.y
		'Else
		'	'NOTE: Correct:
		'	'      Team Fortress 2\tf2_misc_dir\models\player\demo.mdl
		'	aVector.x = 1 / 0.0254 * vertex.x
		'	aVector.y = 1 / 0.0254 * vertex.z
		'	aVector.z = 1 / 0.0254 * -vertex.y
		'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'End If
		'------
		'TODO: [TransformPhyVertex] Merge the various code blocks (separated by MDL version) into one code block.
		If Me.theMdlFileData.version >= 44 AndAlso Me.theMdlFileData.version <= 47 Then
			' This works for various weapons and vehicles in HL2.
			If Me.thePhyFileData.theSourcePhyCollisionDatas.Count = 1 Then
				aVectorTransformed.x = 1 / 0.0254 * vertex.z
				aVectorTransformed.y = 1 / 0.0254 * -vertex.x
				aVectorTransformed.z = 1 / 0.0254 * -vertex.y
			Else
				aVector.x = 1 / 0.0254 * vertex.x
				aVector.y = 1 / 0.0254 * vertex.z
				aVector.z = 1 / 0.0254 * -vertex.y
				aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			End If
		Else
			If Me.thePhyFileData.theSourcePhyCollisionDatas.Count = 1 Then
				'Dim copyOfVector As New SourceVector()
				''copyOfVector.x = 1 / 0.0254 * vertex.x
				''copyOfVector.y = 1 / 0.0254 * vertex.y
				''copyOfVector.z = 1 / 0.0254 * vertex.z
				''copyOfVector.x = 1 / 0.0254 * vertex.x
				''copyOfVector.y = 1 / 0.0254 * vertex.z
				''copyOfVector.z = 1 / 0.0254 * -vertex.y
				'copyOfVector.x = 1 / 0.0254 * vertex.z
				'copyOfVector.y = 1 / 0.0254 * -vertex.x
				'copyOfVector.z = 1 / 0.0254 * -vertex.y
				'aVector = MathModule.VectorTransform(copyOfVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				''aVector.x = 1 / 0.0254 * vertex.z
				''aVector.y = 1 / 0.0254 * -vertex.x
				''aVector.z = 1 / 0.0254 * -vertex.y
				''aVectorTransformed.x = 1 / 0.0254 * vertex.z
				''aVectorTransformed.y = 1 / 0.0254 * -vertex.x
				''aVectorTransformed.z = 1 / 0.0254 * -vertex.y

				'Dim debug As Integer = 4242

				''Dim temp As Double
				''temp = aVector.y
				''aVector.y = aVector.z
				''aVector.z = -temp
				''------
				''aVector.y = -aVector.y
				''------
				''Dim temp As Double
				''temp = aVector.x
				''aVector.x = aVector.z
				''aVector.z = -aVector.y
				''aVector.y = -temp
				''------
				''Dim temp As Double
				''temp = aVector.x
				''aVectorTransformed.x = aVector.z
				''aVectorTransformed.z = -aVector.y
				''aVectorTransformed.y = -temp

				'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

				'[2017-12-22]
				' Correct for cube_like_mesh
				'aVectorTransformed.x = 1 / 0.0254 * vertex.z
				'aVectorTransformed.y = 1 / 0.0254 * -vertex.x
				'aVectorTransformed.z = 1 / 0.0254 * -vertex.y
				'------
				' Correct for L4D2 w_models/weapons/w_desert_rifle.mdl
				'aVectorTransformed.x = 1 / 0.0254 * vertex.z
				'aVectorTransformed.y = 1 / 0.0254 * -vertex.y
				'aVectorTransformed.z = 1 / 0.0254 * vertex.x
				'------
				'aVector.x = 1 / 0.0254 * vertex.z
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * -vertex.z
				'aVector.z = 1 / 0.0254 * vertex.y
				'aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVector.x = 1 / 0.0254 * vertex.z
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVectorTransformed = MathModule.VectorITransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * vertex.x  
				'aVector.y = 1 / 0.0254 * vertex.z  
				'aVector.z = 1 / 0.0254 * -vertex.y 
				'aVector.x = 1 / 0.0254 * -vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * vertex.y
				'aVector.x = 1 / 0.0254 * vertex.y
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * -vertex.y
				'aVector.y = 1 / 0.0254 * vertex.x
				'aVector.z = 1 / 0.0254 * vertex.z
				'aVector.x = 1 / 0.0254 * vertex.y
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * -vertex.z   
				'aVector.x = 1 / 0.0254 * vertex.y
				'aVector.y = 1 / 0.0254 * vertex.x
				'aVector.z = 1 / 0.0254 * -vertex.z
				' Correct for cube_like_mesh
				' Correct for L4D2 w_models/weapons/w_desert_rifle.mdl
				' Incorrect for L4D2 w_models/weapons/w_rifle_m16a2.mdl
				'aVector.x = 1 / 0.0254 * -vertex.y
				'aVector.y = 1 / 0.0254 * -vertex.x
				'aVector.z = 1 / 0.0254 * -vertex.z
				'aVectorTransformed = MathModule.VectorITransform(aVector, Me.poseToWorldColumn0, Me.poseToWorldColumn1, Me.poseToWorldColumn2, Me.poseToWorldColumn3)
				'======
				''FROM: collisionmodel.cpp ConvertToWorldSpace()
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				''Dim worldToBoneColumn0 As New SourceVector()
				''Dim worldToBoneColumn1 As New SourceVector()
				''Dim worldToBoneColumn2 As New SourceVector()
				''Dim worldToBoneColumn3 As New SourceVector()
				''MathModule.AngleMatrix(aBone.rotation.x, aBone.rotation.y, aBone.rotation.z, worldToBoneColumn0, worldToBoneColumn1, worldToBoneColumn2, worldToBoneColumn3)
				''worldToBoneColumn3.x = aBone.position.x
				''worldToBoneColumn3.y = aBone.position.y
				''worldToBoneColumn3.z = aBone.position.z
				''aVector.x = aVectorTransformed.x
				''aVector.y = aVectorTransformed.y
				''aVector.z = aVectorTransformed.z
				''aVectorTransformed = MathModule.VectorTransform(aVector, worldToBoneColumn0, worldToBoneColumn1, worldToBoneColumn2, worldToBoneColumn3)
				'aVector.x = aVectorTransformed.x
				'aVector.y = aVectorTransformed.y
				'aVector.z = aVectorTransformed.z
				'aVectorTransformed = MathModule.VectorTransform(aVector, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
				'======
				''FROM: collisionmodel.cpp ConvertToWorldSpace()
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * -vertex.y
				'aVectorTransformed = MathModule.VectorTransform(aVector, poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3)
				'aVector.x = aVectorTransformed.x
				'aVector.y = aVectorTransformed.y
				'aVector.z = aVectorTransformed.z
				'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				'======
				''FROM: collisionmodel.cpp ConvertToWorldSpace()
				' ''NOTE: These 3 lines work for airport_fuel_truck, ambulance, and army_truck, but not w_desert_file and w_rifle_m16a2.
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.z
				'aVector.z = 1 / 0.0254 * -vertex.y
				' ''NOTE: These 3 lines work for w_desert_file and w_rifle_m16a2, but not ambulance and army_truck.
				''aVector.x = 1 / 0.0254 * vertex.x
				''aVector.y = 1 / 0.0254 * -vertex.y
				''aVector.z = 1 / 0.0254 * vertex.z
				'aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
				'aVector.x = aVectorTransformed.x
				'aVector.y = aVectorTransformed.y
				'aVector.z = aVectorTransformed.z
				''aVector.x = aVectorTransformed.x
				''aVector.y = aVectorTransformed.z
				''aVector.z = -aVectorTransformed.y
				'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				'======
				'FROM: collisionmodel.cpp ConvertToWorldSpace()
				If (Me.theMdlFileData.flags And SourceMdlFileData.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
					'NOTE: These 3 lines do not work for airport_fuel_truck, ambulance, and army_truck.
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * vertex.z
					'aVector.z = 1 / 0.0254 * -vertex.y
					'aVector.x = 1 / 0.0254 * vertex.y
					'aVector.y = 1 / 0.0254 * -vertex.x
					'aVector.z = 1 / 0.0254 * vertex.z
					'aVector.x = 1 / 0.0254 * -vertex.y
					'aVector.y = 1 / 0.0254 * -vertex.x
					'aVector.z = 1 / 0.0254 * vertex.z
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * vertex.y
					'aVector.z = 1 / 0.0254 * vertex.z
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * vertex.z
					'aVector.z = 1 / 0.0254 * vertex.y
					'' Still need a rotate 90 on the z.
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * -vertex.y
					'aVector.z = 1 / 0.0254 * vertex.z
					'aVector.x = 1 / 0.0254 * -vertex.y
					'aVector.y = 1 / 0.0254 * vertex.x
					'aVector.z = 1 / 0.0254 * vertex.z
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * -vertex.z
					'aVector.z = 1 / 0.0254 * vertex.y
					'aVector.x = 1 / 0.0254 * vertex.z
					'aVector.y = 1 / 0.0254 * -vertex.x
					'aVector.z = 1 / 0.0254 * -vertex.y

					'aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
					'aVector.x = aVectorTransformed.x
					'aVector.y = aVectorTransformed.y
					'aVector.z = aVectorTransformed.z
					'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

					'------

					'TEST: Works for props_vehciles that use $staticprop.
					aVector.x = 1 / 0.0254 * vertex.z
					aVector.y = 1 / 0.0254 * -vertex.x
					aVector.z = 1 / 0.0254 * -vertex.y
					aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
					aVector.x = aVectorTransformed.x
					aVector.y = aVectorTransformed.z
					aVector.z = -aVectorTransformed.y
					aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				Else
					'NOTE: These 3 lines work for w_desert_file and w_rifle_m16a2, but not ambulance and army_truck.
					'TEST: Did not work for 50_cal. Rotated 180. Original model has phys mesh rotated oddly, anyway.
					'TEST: Incorrect. Need to 180 on the Y and -1 on scale Z. Noticable on w_minigun.
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * -vertex.y
					'aVector.z = 1 / 0.0254 * vertex.z
					'TEST: Incorrect. Need to 180 on the Y. Noticable on w_minigun.
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * vertex.y
					'aVector.z = 1 / 0.0254 * vertex.z
					'TEST: Incorrect. Need to -1 on scale Z. Noticable on w_minigun.
					'aVector.x = 1 / 0.0254 * vertex.x
					'aVector.y = 1 / 0.0254 * vertex.y
					'aVector.z = 1 / 0.0254 * -vertex.z
					'TEST: Works for w_minigun.
					'TEST: Works for 50cal, but be aware that the phys mesh does not look right for the model. It does look like original model, though.
					'TEST: Does not work for Garry's Mod addon "dodge_daytona" 236224475.
					aVector.x = 1 / 0.0254 * vertex.x
					aVector.y = 1 / 0.0254 * -vertex.y
					aVector.z = 1 / 0.0254 * -vertex.z

					aVectorTransformed = MathModule.VectorTransform(aVector, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
					aVector.x = aVectorTransformed.x
					aVector.y = aVectorTransformed.y
					aVector.z = aVectorTransformed.z
					aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
				End If
			Else
				'FROM: collisionmodel.cpp ConvertToBoneSpace()
				'aVector.x = 1 / 0.0254 * vertex.x
				'aVector.y = 1 / 0.0254 * vertex.y
				'aVector.z = 1 / 0.0254 * vertex.z
				aVector.x = 1 / 0.0254 * vertex.x
				aVector.y = 1 / 0.0254 * vertex.z
				aVector.z = 1 / 0.0254 * -vertex.y
				aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
			End If
		End If

		'If Me.theMdlFileData.theBoneTransforms IsNot Nothing Then
		'	Dim transform As SourceMdlBoneTransform
		'	Dim boneIndex As Integer
		'	Dim preToBoneColumn0 As New SourceVector()
		'	Dim preToBoneColumn1 As New SourceVector()
		'	Dim preToBoneColumn2 As New SourceVector()
		'	Dim preToBoneColumn3 As New SourceVector()
		'	Dim boneToPostColumn0 As New SourceVector()
		'	Dim boneToPostColumn1 As New SourceVector()
		'	Dim boneToPostColumn2 As New SourceVector()
		'	Dim boneToPostColumn3 As New SourceVector()

		'	'TODO: Find the real boneIndex based on boneName
		'	boneIndex = 0
		'	transform = Me.theMdlFileData.theBoneTransforms(boneIndex)

		'	'MathModule.MatrixInvert(poseToWorldColumn0, poseToWorldColumn1, poseToWorldColumn2, poseToWorldColumn3, Me.worldToPoseColumn0, Me.worldToPoseColumn1, Me.worldToPoseColumn2, Me.worldToPoseColumn3)
		'	MathModule.R_ConcatTransforms(transform.preTransformColumn0, transform.preTransformColumn1, transform.preTransformColumn2, transform.preTransformColumn3, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3, preToBoneColumn0, preToBoneColumn1, preToBoneColumn2, preToBoneColumn3)
		'	MathModule.R_ConcatTransforms(preToBoneColumn0, preToBoneColumn1, preToBoneColumn2, preToBoneColumn3, transform.postTransformColumn0, transform.postTransformColumn1, transform.postTransformColumn2, transform.postTransformColumn3, boneToPostColumn0, boneToPostColumn1, boneToPostColumn2, boneToPostColumn3)
		'	aVectorTransformed = MathModule.VectorITransform(aVector, boneToPostColumn0, boneToPostColumn1, boneToPostColumn2, boneToPostColumn3)
		'Else
		'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'End If



		'------
		'NOTE: Works for:
		'      survivor_producer
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.x
		'phyVertex.y = 1 / 0.0254 * aVector.z
		'phyVertex.z = 1 / 0.0254 * -aVector.y
		'------
		'NOTE: These two lines match orientation for cstrike it_lampholder1 model, 
		'      but still doesn't compile properly.
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * -aVector.x
		'phyVertex.z = 1 / 0.0254 * -aVector.y
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.y
		'phyVertex.y = 1 / 0.0254 * aVector.x
		'phyVertex.z = 1 / 0.0254 * -aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.x
		'phyVertex.y = 1 / 0.0254 * aVector.y
		'phyVertex.z = 1 / 0.0254 * -aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * -aVector.y
		'phyVertex.y = 1 / 0.0254 * aVector.x
		'phyVertex.z = 1 / 0.0254 * aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * -aVector.y
		'phyVertex.y = 1 / 0.0254 * aVector.x
		'phyVertex.z = 1 / 0.0254 * aVector.z
		'------
		'NOTE: Does not work for:
		'      w_smg_uzi()
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * aVector.y
		'phyVertex.z = 1 / 0.0254 * aVector.x
		'------
		'NOTE: Works for:
		'      w_smg_uzi()
		'NOTE: Does not work for:
		'      survivor_producer
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * -aVector.y
		'phyVertex.z = 1 / 0.0254 * aVector.x
		'------
		'phyVertex.x = 1 / 0.0254 * aVector.z
		'phyVertex.y = 1 / 0.0254 * -aVector.y
		'phyVertex.z = 1 / 0.0254 * -aVector.x
		'------
		'If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyIsCollisionModel Then
		'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
		'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'	'TEST:  Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
		'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
		'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.z
		'	'aVector.y = 1 / 0.0254 * -phyVertex.vertex.y
		'	'aVector.z = 1 / 0.0254 * phyVertex.vertex.x
		'	'TEST: Does not rotate L4D2's van phys mesh correctly.
		'	'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'	'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
		'	'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
		'	'TEST: Works for L4D2's van phys mesh.
		'	'      Does not work for L4D2 w_model\weapons\w_minigun.mdl.
		'	aVector.x = 1 / 0.0254 * vertex.z
		'	aVector.y = 1 / 0.0254 * -vertex.x
		'	aVector.z = 1 / 0.0254 * -vertex.y
		'Else
		'	'TEST: Does not work for L4D2 w_model\weapons\w_minigun.mdl.
		'	aVector.x = 1 / 0.0254 * vertex.x
		'	aVector.y = 1 / 0.0254 * vertex.z
		'	aVector.z = 1 / 0.0254 * -vertex.y

		'	aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
		'End If
		'------
		'TEST: Does not rotate L4D2's van phys mesh correctly.
		'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
		'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'TEST: Does not rotate L4D2's van phys mesh correctly.
		'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
		'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
		'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
		'TEST: works for survivor_producer; matches ref and phy meshes of van, but both are rotated 90 degrees on z-axis
		'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
		'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
		'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y

		'aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)

		Return aVectorTransformed
	End Function

	'Private Sub CreateAnimationFrameLines(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal frameIndex As Integer)

	'End Sub

	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'	int					i;
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	mstudioanim_t *panim = animdesc.pAnim( );
	'
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pbone->flags & boneMask))
	'			{
	'				CalcBoneQuaternion( iFrame, s, pbone, panim, q[i] );
	'				CalcBonePosition  ( iFrame, s, pbone, panim, pos[i] );
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pbone->flags & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'		}
	'	}
	'}
	'======
	'FROM: SourceEngine2007_source\src_main\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: Find and decode a sub-frame of animation
	'//-----------------------------------------------------------------------------
	'
	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'	pStudioHdr->m_nPerfAnimationLayers++;
	'#endif
	'
	'	virtualmodel_t *pVModel = pStudioHdr->GetVirtualModel();
	'
	'	if (pVModel)
	'	{
	'		CalcVirtualAnimation( pVModel, pStudioHdr, pos, q, seqdesc, sequence, animation, cycle, boneMask );
	'		return;
	'	}
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	const mstudiolinearbone_t *pLinearBones = pStudioHdr->pLinearBones();
	'
	'	int					i;
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	int iLocalFrame = iFrame;
	'	float flStall;
	'	mstudioanim_t *panim = animdesc.pAnim( &iLocalFrame, flStall );
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	// if the animation isn't available, look for the zero frame cache
	'	if (!panim)
	'	{
	'		// Msg("zeroframe %s\n", animdesc.pszName() );
	'		// pre initialize
	'		for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				if (animdesc.flags & STUDIO_DELTA)
	'				{
	'					q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'					pos[i].Init( 0.0f, 0.0f, 0.0f );
	'				}
	'				else
	'				{
	'					q[i] = pbone->quat;
	'					pos[i] = pbone->pos;
	'				}
	'			}
	'		}
	'
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, 1.0 );
	'
	'		return;
	'	}
	'
	'	// BUGBUG: the sequence, the anim, and the model can have all different bone mappings.
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				CalcBoneQuaternion( iLocalFrame, s, pbone, pLinearBones, panim, q[i] );
	'				CalcBonePosition  ( iLocalFrame, s, pbone, pLinearBones, panim, pos[i] );
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'				pStudioHdr->m_nPerfAnimatedBones++;
	'				pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'			pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'		}
	'	}
	'
	'	// cross fade in previous zeroframe data
	'	if (flStall > 0.0f)
	'	{
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, flStall );
	'	}
	'
	'	if (animdesc.numlocalhierarchy)
	'	{
	'		matrix3x4_t *boneToWorld = g_MatrixPool.Alloc();
	'		CBoneBitList boneComputed;
	'
	'		int i;
	'		for (i = 0; i < animdesc.numlocalhierarchy; i++)
	'		{
	'			mstudiolocalhierarchy_t *pHierarchy = animdesc.pHierarchy( i );
	'
	'			if ( !pHierarchy )
	'				break;
	'
	'			if (pStudioHdr->boneFlags(pHierarchy->iBone) & boneMask)
	'			{
	'				if (pStudioHdr->boneFlags(pHierarchy->iNewParent) & boneMask)
	'				{
	'					CalcLocalHierarchyAnimation( pStudioHdr, boneToWorld, boneComputed, pos, q, pbone, pHierarchy, pHierarchy->iBone, pHierarchy->iNewParent, cycle, iFrame, s, boneMask );
	'				}
	'			}
	'		}
	'
	'		g_MatrixPool.Free( boneToWorld );
	'	}
	'
	'}
	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc49, ByVal frameIndex As Integer, Optional onlyWriteCorrectiveAnimationRootBones As Boolean = False)
		Dim s As Double
		Dim animIndex As Integer
		Dim aBone As SourceMdlBone
		Dim aWeight As Double
		Dim anAnimation As SourceMdlAnimation
		Dim rot As SourceVector
		Dim pos As SourceVector
		Dim aFrameLine As AnimationFrameLine
		Dim sectionFrameIndex As Integer

		s = 0

		animIndex = 0

		'If anAnimationDesc.theAnimations Is Nothing OrElse animIndex >= anAnimationDesc.theAnimations.Count Then
		'	anAnimation = Nothing
		'Else
		'	anAnimation = anAnimationDesc.theAnimations(animIndex)
		'End If
		'------
		Dim sectionIndex As Integer
		Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		If anAnimationDesc.sectionFrameCount = 0 Then
			sectionIndex = 0
			sectionFrameIndex = frameIndex
		Else
			sectionIndex = CInt(Math.Truncate(frameIndex / anAnimationDesc.sectionFrameCount))
			sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount)
		End If
		'aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
		'If anAnimationDesc.theSectionsOfAnimations Is Nothing OrElse animIndex >= aSectionOfAnimation.Count Then
		'	anAnimation = Nothing
		'Else
		'	anAnimation = aSectionOfAnimation(animIndex)
		'End If
		anAnimation = Nothing
		aSectionOfAnimation = Nothing
		If anAnimationDesc.theSectionsOfAnimations IsNot Nothing Then
			aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
			If animIndex >= 0 AndAlso animIndex < aSectionOfAnimation.Count Then
				anAnimation = aSectionOfAnimation(animIndex)
			End If
		End If

		For boneIndex As Integer = 0 To Me.theMdlFileData.theBones.Count - 1
			aBone = Me.theMdlFileData.theBones(boneIndex)

			If onlyWriteCorrectiveAnimationRootBones AndAlso aBone.parentBoneIndex <> -1 Then
				Continue For
			End If

			If aSequenceDesc IsNot Nothing Then
				aWeight = aSequenceDesc.theBoneWeights(boneIndex)
			Else
				'NOTE: This should only be needed for a delta $animation.
				'      Arbitrarily assign 1 so that the following code will add frame lines for this $animation.
				aWeight = 1
			End If

			If anAnimation IsNot Nothing AndAlso anAnimation.boneIndex = boneIndex Then
				If aWeight > 0 Then
					If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
						aFrameLine = Me.theAnimationFrameLines(boneIndex)
					Else
						aFrameLine = New AnimationFrameLine()
						Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
					End If

					aFrameLine.rotationQuat = New SourceQuaternion()
					'rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation)
					rot = CalcBoneRotation(sectionFrameIndex, s, aBone, anAnimation, aFrameLine.rotationQuat)
					aFrameLine.rotation = New SourceVector()

					'NOTE: z = z puts head-foot axis horizontally
					'      facing viewer
					aFrameLine.rotation.x = rot.x
					aFrameLine.rotation.y = rot.y
					aFrameLine.rotation.z = rot.z
					'------
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.y - 1.570796
					'aFrameLine.rotation.z = rot.z
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.x
					'aFrameLine.rotation.z = rot.z
					'------
					'------
					'NOTE: x = z puts head-foot axis horizontally
					'      facing away from viewer
					'aFrameLine.rotation.x = rot.z
					'aFrameLine.rotation.y = rot.y
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = rot.z
					'aFrameLine.rotation.y = rot.x
					'aFrameLine.rotation.z = rot.y
					'------
					'------
					'NOTE: y = z  : head-foot axis vertically correctly
					'      x = -x : upside-down
					'      z = y  : 
					' facing to window right
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					' facing to window left
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'NOTE: Upside-down; facing to window right
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					' facing to window left
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'NOTE: Upside-down; facing to window right
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.x

					aFrameLine.rotation.debug_text = rot.debug_text

					'pos = Me.CalcBonePosition(frameIndex, s, aBone, anAnimation)
					pos = Me.CalcBonePosition(sectionFrameIndex, s, aBone, anAnimation)
					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = pos.x
					aFrameLine.position.y = pos.y
					aFrameLine.position.z = pos.z
					aFrameLine.position.debug_text = pos.debug_text
				End If

				animIndex += 1
				'If animIndex >= anAnimationDesc.theAnimations.Count Then
				'	anAnimation = Nothing
				'Else
				'	anAnimation = anAnimationDesc.theAnimations(animIndex)
				'End If
				If animIndex >= aSectionOfAnimation.Count Then
					anAnimation = Nothing
				Else
					anAnimation = aSectionOfAnimation(animIndex)
				End If
			ElseIf aWeight > 0 Then
				If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
					aFrameLine = Me.theAnimationFrameLines(boneIndex)
				Else
					aFrameLine = New AnimationFrameLine()
					Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
				End If

				'NOTE: Changed in v0.25.
				'If (anAnimationDesc.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_DELTA) > 0 Then
					aFrameLine.rotation = New SourceVector()
					aFrameLine.rotation.x = 0
					aFrameLine.rotation.y = 0
					aFrameLine.rotation.z = 0
					aFrameLine.rotation.debug_text = "desc_delta"

					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = 0
					aFrameLine.position.y = 0
					aFrameLine.position.z = 0
					aFrameLine.position.debug_text = "desc_delta"
				Else
					aFrameLine.rotation = New SourceVector()
					aFrameLine.rotation.x = aBone.rotation.x
					aFrameLine.rotation.y = aBone.rotation.y
					aFrameLine.rotation.z = aBone.rotation.z
					aFrameLine.rotation.debug_text = "desc_bone"

					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = aBone.position.x
					aFrameLine.position.y = aBone.position.y
					aFrameLine.position.z = aBone.position.z
					aFrameLine.position.debug_text = "desc_bone"
				End If
			End If
		Next
	End Sub

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame rotation for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBoneQuaternion( int frame, float s, 
	'						const Quaternion &baseQuat, const RadianEuler &baseRot, const Vector &baseRotScale, 
	'						int iBaseFlags, const Quaternion &baseAlignment, 
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if ( panim->flags & STUDIO_ANIM_RAWROT )
	'	{
	'		q = *(panim->pQuat48());
	'		Assert( q.IsValid() );
	'		return;
	'	} 

	'	if ( panim->flags & STUDIO_ANIM_RAWROT2 )
	'	{
	'		q = *(panim->pQuat64());
	'		Assert( q.IsValid() );
	'		return;
	'	}

	'	if ( !(panim->flags & STUDIO_ANIM_ANIMROT) )
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			q.Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'		}
	'		else
	'		{
	'			q = baseQuat;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pValuesPtr = panim->pRotV();

	'	if (s > 0.001f)
	'	{
	'		QuaternionAligned	q1, q2;
	'		RadianEuler			angle1, angle2;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle1.x, angle2.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle1.y, angle2.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle1.z, angle2.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle1.x = angle1.x + baseRot.x;
	'			angle1.y = angle1.y + baseRot.y;
	'			angle1.z = angle1.z + baseRot.z;
	'			angle2.x = angle2.x + baseRot.x;
	'			angle2.y = angle2.y + baseRot.y;
	'			angle2.z = angle2.z + baseRot.z;
	'		}

	'		Assert( angle1.IsValid() && angle2.IsValid() );
	'		if (angle1.x != angle2.x || angle1.y != angle2.y || angle1.z != angle2.z)
	'		{
	'			AngleQuaternion( angle1, q1 );
	'			AngleQuaternion( angle2, q2 );

	'	#ifdef _X360
	'			fltx4 q1simd, q2simd, qsimd;
	'			q1simd = LoadAlignedSIMD( q1 );
	'			q2simd = LoadAlignedSIMD( q2 );
	'			qsimd = QuaternionBlendSIMD( q1simd, q2simd, s );
	'			StoreUnalignedSIMD( q.Base(), qsimd );
	'	#else
	'			QuaternionBlend( q1, q2, s, q );
	'#End If
	'		}
	'		else
	'		{
	'			AngleQuaternion( angle1, q );
	'		}
	'	}
	'	else
	'	{
	'		RadianEuler			angle;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle.x = angle.x + baseRot.x;
	'			angle.y = angle.y + baseRot.y;
	'			angle.z = angle.z + baseRot.z;
	'		}

	'		Assert( angle.IsValid() );
	'		AngleQuaternion( angle, q );
	'	}

	'	Assert( q.IsValid() );

	'	// align to unified bone
	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	{
	'		QuaternionAlign( baseAlignment, q, q );
	'	}
	'}
	'
	'inline void CalcBoneQuaternion( int frame, float s, 
	'						const mstudiobone_t *pBone,
	'						const mstudiolinearbone_t *pLinearBones,
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if (pLinearBones)
	'	{
	'		CalcBoneQuaternion( frame, s, pLinearBones->quat(panim->bone), pLinearBones->rot(panim->bone), pLinearBones->rotscale(panim->bone), pLinearBones->flags(panim->bone), pLinearBones->qalignment(panim->bone), panim, q );
	'	}
	'	else
	'	{
	'		CalcBoneQuaternion( frame, s, pBone->quat, pBone->rot, pBone->rotscale, pBone->flags, pBone->qAlignment, panim, q );
	'	}
	'}
	'Private Function CalcBoneQuaternion(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceQuaternion
	'	Dim rot As New SourceQuaternion()
	'	Dim angleVector As New SourceVector()

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
	'		rot.x = anAnimation.theRot48.x
	'		rot.y = anAnimation.theRot48.y
	'		rot.z = anAnimation.theRot48.z
	'		rot.w = anAnimation.theRot48.w
	'		Return rot
	'	ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
	'		rot.x = anAnimation.theRot64.x
	'		rot.y = anAnimation.theRot64.y
	'		rot.z = anAnimation.theRot64.z
	'		rot.w = anAnimation.theRot64.w
	'		Return rot
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
	'			rot.x = 0
	'			rot.y = 0
	'			rot.z = 0
	'			rot.w = 1
	'		Else
	'			rot.x = aBone.quat.x
	'			rot.y = aBone.quat.y
	'			rot.z = aBone.quat.z
	'			rot.w = aBone.quat.w
	'		End If
	'		Return rot
	'	End If

	'	Dim rotV As SourceMdlAnimationValuePointer

	'	rotV = anAnimation.theRotV

	'	If rotV.animValueOffset(0) <= 0 Then
	'		angleVector.x = 0
	'	Else
	'		angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(0), aBone.rotationScaleX)
	'	End If
	'	If rotV.animValueOffset(1) <= 0 Then
	'		angleVector.y = 0
	'	Else
	'		angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(1), aBone.rotationScaleY)
	'	End If
	'	If rotV.animValueOffset(2) <= 0 Then
	'		angleVector.z = 0
	'	Else
	'		angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(2), aBone.rotationScaleZ)
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
	'		angleVector.x += aBone.quat.x
	'		angleVector.y += aBone.quat.y
	'		angleVector.z += aBone.quat.z
	'	End If

	'	rot = MathModule.AngleQuaternion(angleVector)

	'	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	'	{
	'	'		QuaternionAlign( baseAlignment, q, q );
	'	'	}

	'	Return rot
	'End Function
	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation, ByRef rotationQuat As SourceQuaternion) As SourceVector
		Dim rot As New SourceQuaternion()
		Dim angleVector As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
			rot.x = anAnimation.theRot48bits.x
			rot.y = anAnimation.theRot48bits.y
			rot.z = anAnimation.theRot48bits.z
			rot.w = anAnimation.theRot48bits.w
			rotationQuat.x = rot.x
			rotationQuat.y = rot.y
			rotationQuat.z = rot.z
			rotationQuat.w = rot.w
			angleVector = MathModule.ToEulerAngles(rot)

			angleVector.debug_text = "raw48"
			Return angleVector
		ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
			'angleVector.x = anAnimation.theRot64.xRadians
			'angleVector.y = anAnimation.theRot64.yRadians
			'angleVector.z = anAnimation.theRot64.zRadians
			'------
			rot.x = anAnimation.theRot64bits.x
			rot.y = anAnimation.theRot64bits.y
			rot.z = anAnimation.theRot64bits.z
			rot.w = anAnimation.theRot64bits.w
			rotationQuat.x = rot.x
			rotationQuat.y = rot.y
			rotationQuat.z = rot.z
			rotationQuat.w = rot.w
			angleVector = MathModule.ToEulerAngles(rot)

			''TEST: Rotate z by -90 degrees.
			''TEST: Rotate y by -90 degrees.
			'angleVector.y += MathModule.DegreesToRadians(-90)

			angleVector.debug_text = "raw64 (" + rot.x.ToString() + ", " + rot.y.ToString() + ", " + rot.z.ToString() + ", " + rot.w.ToString() + ")"
			Return angleVector
		End If

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				angleVector.x = 0
				angleVector.y = 0
				angleVector.z = 0
				rotationQuat.x = 0
				rotationQuat.y = 0
				rotationQuat.z = 0
				rotationQuat.w = 0
				angleVector.debug_text = "delta"
			Else
				angleVector.x = aBone.rotation.x
				angleVector.y = aBone.rotation.y
				angleVector.z = aBone.rotation.z
				rotationQuat.x = 0
				rotationQuat.y = 0
				rotationQuat.z = 0
				rotationQuat.w = 0
				angleVector.debug_text = "bone"
			End If
			Return angleVector
		End If

		Dim rotV As SourceMdlAnimationValuePointer

		rotV = anAnimation.theRotV

		If rotV.animXValueOffset <= 0 Then
			angleVector.x = 0
		Else
			angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimXValues, aBone.rotationScale.x)
		End If
		If rotV.animYValueOffset <= 0 Then
			angleVector.y = 0
		Else
			angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimYValues, aBone.rotationScale.y)
		End If
		If rotV.animZValueOffset <= 0 Then
			angleVector.z = 0
		Else
			angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimZValues, aBone.rotationScale.z)
		End If

		angleVector.debug_text = "anim"

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
			angleVector.x += aBone.rotation.x
			angleVector.y += aBone.rotation.y
			angleVector.z += aBone.rotation.z
			angleVector.debug_text += "+bone"
		End If

		rotationQuat = MathModule.EulerAnglesToQuaternion(angleVector)
		Return angleVector
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame position for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBonePosition(	int frame, float s,
	'						const Vector &basePos, const Vector &baseBoneScale, 
	'						const mstudioanim_t *panim, Vector &pos	)
	'{
	'	if (panim->flags & STUDIO_ANIM_RAWPOS)
	'	{
	'		pos = *(panim->pPos());
	'		Assert( pos.IsValid() );

	'		return;
	'	}
	'	else if (!(panim->flags & STUDIO_ANIM_ANIMPOS))
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			pos.Init( 0.0f, 0.0f, 0.0f );
	'		}
	'		else
	'		{
	'			pos = basePos;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pPosV = panim->pPosV();
	'	int					j;

	'	if (s > 0.001f)
	'	{
	'		float v1, v2;
	'		for (j = 0; j < 3; j++)
	'		{
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], v1, v2 );
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			pos[j] = v1 * (1.0 - s) + v2 * s;
	'		}
	'	}
	'	else
	'	{
	'		for (j = 0; j < 3; j++)
	'		{
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], pos[j] );
	'		}
	'	}

	'	if (!(panim->flags & STUDIO_ANIM_DELTA))
	'	{
	'		pos.x = pos.x + basePos.x;
	'		pos.y = pos.y + basePos.y;
	'		pos.z = pos.z + basePos.z;
	'	}

	'	Assert( pos.IsValid() );
	'}
	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceVector
		Dim pos As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
			'If aBone.parentBoneIndex = -1 Then
			'	pos.x = anAnimation.thePos.y
			'	pos.y = -anAnimation.thePos.x
			'	pos.z = anAnimation.thePos.z
			'Else
			pos.x = anAnimation.thePos.x
			pos.y = anAnimation.thePos.y
			pos.z = anAnimation.thePos.z
			'	'------
			'	'pos.y = anAnimation.thePos.z
			'	'pos.z = anAnimation.thePos.y
			'	'------
			'	'pos.x = anAnimation.thePos.y
			'	'pos.y = -anAnimation.thePos.x
			'	'pos.z = anAnimation.thePos.z
			'End If

			pos.debug_text = "raw"
			Return pos
		ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) = 0 Then
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				pos.x = 0
				pos.y = 0
				pos.z = 0
				pos.debug_text = "delta"
			Else
				pos.x = aBone.position.x
				pos.y = aBone.position.y
				pos.z = aBone.position.z
				'pos.y = aBone.positionZ
				'pos.z = -aBone.positionY
				pos.debug_text = "bone"
			End If
			Return pos
		End If

		Dim posV As SourceMdlAnimationValuePointer

		posV = anAnimation.thePosV

		If posV.animXValueOffset <= 0 Then
			pos.x = 0
		Else
			pos.x = Me.ExtractAnimValue(frameIndex, posV.theAnimXValues, aBone.positionScale.x)
		End If

		If posV.animYValueOffset <= 0 Then
			pos.y = 0
		Else
			pos.y = Me.ExtractAnimValue(frameIndex, posV.theAnimYValues, aBone.positionScale.y)
		End If

		If posV.animZValueOffset <= 0 Then
			pos.z = 0
		Else
			pos.z = Me.ExtractAnimValue(frameIndex, posV.theAnimZValues, aBone.positionScale.z)
		End If

		pos.debug_text = "anim"

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
			pos.x += aBone.position.x
			pos.y += aBone.position.y
			pos.z += aBone.position.z
			pos.debug_text += "+bone"
		End If

		Return pos
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
	'{
	'	if ( !panimvalue )
	'	{
	'		v1 = 0;
	'		return;
	'	}

	'	int k = frame;

	'	while (panimvalue->num.total <= k)
	'	{
	'		k -= panimvalue->num.total;
	'		panimvalue += panimvalue->num.valid + 1;
	'		if ( panimvalue->num.total == 0 )
	'		{
	'			Assert( 0 ); // running off the end of the animation stream is bad
	'			v1 = 0;
	'			return;
	'		}
	'	}
	'	if (panimvalue->num.valid > k)
	'	{
	'		v1 = panimvalue[k+1].value * scale;
	'	}
	'	else
	'	{
	'		// get last valid data block
	'		v1 = panimvalue[panimvalue->num.valid].value * scale;
	'	}
	'}
	Public Function ExtractAnimValue(ByVal frameIndex As Integer, ByVal animValues As List(Of SourceMdlAnimationValue), ByVal scale As Double) As Double
		Dim v1 As Double
		' k is frameCountRemainingToBeChecked
		Dim k As Integer
		Dim animValueIndex As Integer

		Try
			k = frameIndex
			animValueIndex = 0
			While animValues(animValueIndex).total <= k
				k -= animValues(animValueIndex).total
				animValueIndex += animValues(animValueIndex).valid + 1
				If animValueIndex >= animValues.Count OrElse animValues(animValueIndex).total = 0 Then
					'NOTE: Bad if it reaches here. This means maybe a newer format of the anim data was used for the model.
					v1 = 0
					Return v1
				End If
			End While

			If animValues(animValueIndex).valid > k Then
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + k + 1).value * scale
			Else
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + animValues(animValueIndex).valid).value * scale
			End If
		Catch ex As Exception
		End Try

		Return v1
	End Function

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter
	Private theMdlFileData As SourceMdlFileData49
	Private thePhyFileData As SourcePhyFileData
	Private theVvdFileData As SourceVvdFileData04

	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

	Private worldToPoseColumn0 As New SourceVector()
	Private worldToPoseColumn1 As New SourceVector()
	Private worldToPoseColumn2 As New SourceVector()
	Private worldToPoseColumn3 As New SourceVector()
	Private poseToWorldColumn0 As New SourceVector()
	Private poseToWorldColumn1 As New SourceVector()
	Private poseToWorldColumn2 As New SourceVector()
	Private poseToWorldColumn3 As New SourceVector()

	Private previousFrameRotation As New SourceVector()
	Private frame0Position As New SourceVector()

#End Region

End Class
