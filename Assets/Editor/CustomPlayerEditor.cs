using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Character.Player))]
public class CustomPlayerEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
		Character.Player script = (Character.Player)target;
		if (GUILayout.Button ("Draw Mystery")) {
			script.Test ();
		}
		if (GUILayout.Button ("Draw Treasure")) {
			script.TestTreasure ();
		}
	}
}
