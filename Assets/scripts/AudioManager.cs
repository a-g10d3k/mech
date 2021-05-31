using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;
    public SMech Mech;

    [System.Serializable]
    public struct SMech
    {
        public AudioClip LegSlide;
        public AudioClip LegStomp;
    }
    
    private void Awake()
    {
        if(I != null)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
    }
}
