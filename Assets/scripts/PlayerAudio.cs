using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public Player Player { get => gameObject.GetComponent<Player>(); }
    private AudioSource _audioSource;
    private Dictionary<string, Sound> _sounds;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();

        _sounds = new Dictionary<string, Sound>
        {
            { "LLegSlide", new Sound(AudioManager.I.Mech.LegSlide, new Vector3(-0.5f, -2f, 0f)) },
            { "LLegStomp", new Sound(AudioManager.I.Mech.LegStomp, new Vector3(-0.5f, -3f, 0f)) },
            { "RLegSlide", new Sound(AudioManager.I.Mech.LegSlide, new Vector3(0.5f, -2f, 0f)) },
            { "RLegStomp", new Sound(AudioManager.I.Mech.LegStomp, new Vector3(0.5f, -3f, 0f)) },
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string str)
    {
        var sound = _sounds[str];
        AudioSource.PlayClipAtPoint(sound.AudioClip, transform.TransformPoint(sound.PositionOffset));
    }

    private class Sound
    {
        public AudioClip AudioClip;
        public Vector3 PositionOffset;

        public Sound (AudioClip audioClip, Vector3 positionOffset)
        {
            AudioClip = audioClip;
            PositionOffset = positionOffset;
        }
    }
}
