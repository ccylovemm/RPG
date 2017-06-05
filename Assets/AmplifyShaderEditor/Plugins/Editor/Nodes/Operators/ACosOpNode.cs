// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>

using System;

namespace AmplifyShaderEditor
{
	[Serializable]
	[NodeAttributes( "ACos", "Trigonometry", "Arccosine of scalars and vectors" )]
	public sealed class ACosOpNode : SingleInputOp
	{
		protected override void CommonInit( int uniqueId )
		{
			base.CommonInit( uniqueId );
			m_opName = "acos";
			m_previewId = 73;
			m_inputPorts[ 0 ].CreatePortRestrictions(	WirePortDataType.OBJECT, 
														WirePortDataType.FLOAT , 
														WirePortDataType.FLOAT2, 
														WirePortDataType.FLOAT3, 
														WirePortDataType.FLOAT4, 
														WirePortDataType.COLOR, 
														WirePortDataType.INT );
		}
	}
}
