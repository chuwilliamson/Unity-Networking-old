
using System.Collections.Generic;
using UnityEngine;

public class DiscardStack : Stack
{
    public static DiscardStack singleton;

    public GameObject CardPrefab;
    public void Setup()
    {
        if (singleton == null)
            singleton = this;
        Cards = new List<GameObject>();
    }

    public override void Shuffle(GameObject go)
    {
        base.Shuffle(go);
    }

}