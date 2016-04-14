using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Mystery stack.
/// This is attached to the GameObject in the scene.
/// </summary>
public class MysteryStack : CardStack<MysteryCard, MysteryCardMono>
{
    void Awake()
    {
        if (this.m_cards.Count < 1)
        {
            this.Setup();
        }
    }
    protected override void Setup()
    {
        base.Setup();
    }


    public void EditorInit()
    {
        this.Setup();
    }

    public void EditorClear()
    {
        this.Clear();
    }
}

