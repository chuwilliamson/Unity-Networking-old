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
	void Start()
	{
		foreach (var v in CardMonos) {
			int randomGold = UnityEngine.Random.Range (100, 1000);
			v.Gold = randomGold;
		}
	}
}
