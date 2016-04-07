using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource audio_manager;
    public AudioClip shuffle;
    public AudioClip win;
    public AudioClip hover_over_card;
    public AudioClip button_click;
    public AudioClip draw_card;
    public AudioClip card_flip;

    void Start()
    {
        AudioPlay.manager = audio_manager;
        AudioPlay.shuffle = shuffle;
        AudioPlay.win = win;
        AudioPlay.hover_on_card = hover_over_card;
        AudioPlay.button_click = button_click;
        AudioPlay.draw_card = draw_card;
        AudioPlay.card_flip = card_flip;
    }
}
