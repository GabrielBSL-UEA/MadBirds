using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;

    private Dictionary<string, Sound> sfxs = new Dictionary<string, Sound>();
    private Dictionary<string, Sound> musics = new Dictionary<string, Sound>();

    private string m_CurrentMusic = "-empty-";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                AddToDictionary(s, musics);
                continue;
            }

            AddToDictionary(s, sfxs);
        }
    }

    public void PlaySFX(string name)
    {
        if (!sfxs.ContainsKey(name))
        {
            Debug.LogWarning("Efeito sonoro de nome '" + name + "' não encontrado");
            return;
        }

        sfxs[name].source.Play();
    }

    public void PlayMusic(string name)
    {
        if (m_CurrentMusic.Equals(name))
        {
            Debug.LogWarning("Música '" + name + "' já está tocando");
            return;
        }

        if (!musics.ContainsKey(name))
        {
            Debug.LogWarning("Música de nome '" + name + "' não encontrado");
            return;
        }

        if (m_CurrentMusic.Equals("-empty-"))
        {
            m_CurrentMusic = name;
        }

        musics[m_CurrentMusic].source.Stop();
        musics[name].source.Play();

        m_CurrentMusic = name;
    }

    private void AddToDictionary(Sound newSound, Dictionary<string, Sound> audioDictionary, bool loop = false)
    {
        newSound.source = gameObject.AddComponent<AudioSource>();
        newSound.source.clip = newSound.clip;

        newSound.source.volume = newSound.volume;
        newSound.source.pitch = newSound.pitch;

        newSound.source.loop = loop;

        audioDictionary[newSound.name] = newSound;
    }
}
