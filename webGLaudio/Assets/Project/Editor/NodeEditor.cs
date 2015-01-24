using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		Node myTarget = (Node)target;

		if(GUILayout.Button("Generate id"))
		{
			myTarget.GenerateId ();
		}

		if(GUILayout.Button("Autoconnect nearest"))
		{
			myTarget.AutoConnectNearest();
		}

		if(GUILayout.Button("Update connections"))
		{
			myTarget.UpdateConnected ();
		}

		if(GUILayout.Button("Clear all connections"))
		{
			myTarget.ClearConnected ();
		}
	}

	public void OnSceneGUI()
	{
		Node myTarget = (Node)target;
		myTarget.DrawDebugs ();
	}
}
