using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MysteryStack),true)]
public class CustomMysteryStackEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
		MysteryStack script = (MysteryStack)target;
		if (GUILayout.Button ("Generate Deck")) {
			script.Test ();	
		} 
		if (GUILayout.Button ("Clear Deck")) {
			script.Clear ();	
		} 
	}
}

[CustomEditor(typeof(TreasureStack),true)]
public class CustomTreasureStackEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
		TreasureStack script = (TreasureStack)target;
		if (GUILayout.Button ("Generate Deck")) {
			script.Test ();	
		}
		if (GUILayout.Button ("Clear Deck")) {
			script.Clear ();	
		}



	}
}
