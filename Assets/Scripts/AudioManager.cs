using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

// Structs for the Audio Channels
[System.Serializable]
public struct Audio2DData
{
	public AudioSource Background2D;
    public AudioSource[] SFX2D;
    public AudioSource[] Channels2D;
}

[System.Serializable]
public struct Audio3DData {
    public AudioSource Background3D;
    public AudioSource[] SFX3D;
    public AudioSource[] Channels3D;
}

public class AudioManager : MonoBehaviour {

    // Singleton Instance

    private static AudioManager _Instance;

    public static AudioManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new AudioManager();
            }
            return _Instance;
        }
    }

    // Audio Channels
    public Audio2DData _Audio2DData;
    public Audio3DData _Audio3DData;

    List<AudioSource> ActiveChannels = new List<AudioSource>();

    // Audio Clips
    public AudioClip[] audioBackground;
    public AudioClip[] audioSFX;
    public AudioClip[] audioOther;

    // Used to determine whether 2D or 3D channels are used
    public bool Play3DBackground = true;

    // Used to determine whether random background music is played
    public bool PlayBackground = false;

    public void Start()
    {
        PlayAudioClip(_Audio3DData.Background3D, audioBackground[0], 0, false, 1);  // Example of music playing
    }

    public void Update()
    {
        // Removes channels that aren't playing
        foreach(AudioSource Channel in ActiveChannels)
        {
            if (Channel.isPlaying == false)
                ActiveChannels.Remove(Channel);
        }
        // Plays background music if it isn't already playing
        if (PlayBackground)
            PlayRandomBackgroundClip();
    }

    // Creates a 2D channel - MUST be in the 2D channels lists
    void Create2DChannel(AudioSource[] Channels) {
        Channels[Channels.Length] = gameObject.AddComponent<AudioSource>();
        RemoveSpatialBlend();
    }

    // Makes channels 2D
    void RemoveSpatialBlend() {
        _Audio2DData.Background2D.spatialBlend = 0;

        foreach (AudioSource channel in _Audio2DData.SFX2D)
            channel.spatialBlend = 0;

        foreach (AudioSource channel in _Audio2DData.Channels2D)
            channel.spatialBlend = 0;
    }

    // Takes volume as a percentage
    public void PlayAudioClip(AudioSource Channel, AudioClip Clip, int LoopAmount, bool DoesDopler, float Volume)
    {
        if (Volume > 1)
            Volume = 1;
        else if (Volume < 0)
            Volume = 0;

        if (!DoesDopler) 
        {
           for (int index = 0; index < LoopAmount; ++index)
            {
                if (!Channel.isPlaying)
                {
                    Channel.PlayOneShot(Clip, Volume);
                }
            } 
        }
        else
        {
            
        }
    }

    public void PlayRandomBackgroundClip()
    {
        // Plays background music if the background channel is empty and a background music clip exists
        if (audioBackground.Length > 0) {
            if (_Audio3DData.Background3D == null && Play3DBackground)
            {
                _Audio3DData.Background3D = gameObject.AddComponent<AudioSource>();
                ActiveChannels.Add(_Audio3DData.Background3D);
                _Audio3DData.Background3D.PlayOneShot(audioBackground[Random.Range(0, audioBackground.Length - 1)], 0.5f);
            }
            else if (_Audio2DData.Background2D == null)
            {
                _Audio2DData.Background2D = gameObject.AddComponent<AudioSource>();
                ActiveChannels.Add(_Audio2DData.Background2D);
                RemoveSpatialBlend();
                _Audio2DData.Background2D.PlayOneShot(audioBackground[Random.Range(0, audioBackground.Length - 1)], 0.5f);
            }
        }
    }

}
