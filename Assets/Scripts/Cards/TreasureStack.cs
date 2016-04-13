using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Treasure stack.
/// This is attached to the GameObject in the scene.
/// </summary>
public class TreasureStack : CardStack<TreasureCard, TreasureCardMono>
{
	protected override void Setup()
	{
		base.Setup ();
		foreach (var v in CardMonos) {
			int randomGold = UnityEngine.Random.Range (100, 1000);
			v.Gold = randomGold;
			v.Description = "This is a default Treasure card...";
		}
	}

	void Awake()
	{
		if (this.m_cards.Count < 1) {
			this.Setup ();
		}
	}

	[ContextMenu("DO IT DO IT")]
	public void EditorInit()
	{
		this.Setup();
	}

	public void EditorClear()
	{
		this.Clear();
	}
}
