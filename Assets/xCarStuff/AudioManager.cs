using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]
	public static AudioManager instance;

    public float pitchSlowT;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

            s.source.pitch = s.pitch;
            s.source.volume = s.volume;
        }
	}

    void Start()
    {
        Play("theme");
    }

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}
		if (s.pitchVariance > 0)
		{
			s.pitchVariance += UnityEngine.Random.Range(-s.pitchVariance, s.pitchVariance);
		}
		if (s.volumeVariance > 0)
		{
			s.volumeVariance += UnityEngine.Random.Range(-s.volumeVariance, s.volumeVariance);
		}

		if ((!s.source.isPlaying && s.dontRepeat) || !s.dontRepeat) s.source.Play();
	}

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + sound + " not found!");
			return;
		}
		s.source.Stop();
	}

    public void slowSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.pitch = pitchSlowT;
        }
    }

    public void normalSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.pitch = s.pitch;
        }
    }

}
