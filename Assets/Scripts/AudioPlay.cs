using UnityEngine;
using System.Collections;

public class AudioPlay
{
    static public AudioSource manager;
    static public AudioClip shuffle;
    static public AudioClip win;
    static public AudioClip button_click;
    static public AudioClip draw_card;
    static public AudioClip card_flip;
    static public AudioClip hover_on_card;

    static public void Shuffle()
    {
        manager.clip = shuffle;
        manager.Play();
    }

    static public void Win()
    {
        manager.volume = 0.3f; 
        manager.clip = win;
        manager.Play();
        manager.volume = 1;
    }

    static public void CardFlip()
    {
        manager.clip = card_flip;
        manager.Play();
    }

    static public void HoverOnCard()
    {
        manager.clip = hover_on_card;
        manager.Play();
    }

    static public void DrawCard()
    {
        manager.clip = draw_card;
        manager.Play();
    }

    public void ButtonClick()
    {
        manager.clip = button_click;
        manager.Play();
    }
}
