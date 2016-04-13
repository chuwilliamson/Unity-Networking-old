using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MysteryStack),true)]
public class CustomMysteryStackEditor : Editor {

	public override void OnInspectorGUI()
	{
		
		MysteryStack script = (MysteryStack)target;

		if (GUILayout.Button ("Generate Deck")) {
			script.EditorInit ();
		} 
		if (GUILayout.Button ("Clear Deck")) {
			script.EditorClear ();	
		} 

		DrawDefaultInspector ();
	}
}

[CustomEditor(typeof(TreasureStack),true)]
public class CustomTreasureStackEditor : Editor {

	public override void OnInspectorGUI()
	{
		
		TreasureStack script = target as TreasureStack;
		if (GUILayout.Button ("Generate Deck")) {
			script.EditorInit ();	
		}
		if (GUILayout.Button ("Clear Deck")) {
			script.EditorClear ();	
		}

		DrawDefaultInspector ();



	}
}
