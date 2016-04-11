using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Eric.Player))]
public class CustomPlayerEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
		Eric.Player script = (Eric.Player)target;
		if (GUILayout.Button ("Draw Mystery")) {
			script.Test ();
		}
		if (GUILayout.Button ("Draw Treasure")) {
			script.TestTreasure ();
		}
	}
}
