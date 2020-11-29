using System.Collections;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    [System.Serializable]
    public struct AudioNode
    {
        public string name;
        public AudioClip sound;
    }

    public static float SfxVolume
    {
        get { return _sfxVolume; }

        set
        {
            _sfxVolume = value;
            foreach (AudioSource src in Instance._soundSources)
                src.volume = _sfxVolume;
        }
    }
    public AudioNode[] AudioNodes;

    private static float _sfxVolume = 1f;

    private ArrayList _soundSources = new ArrayList();
    private static Hashtable _audios = new Hashtable();


    public void PlaySoundUI(string name)
    {
        PlaySound((AudioClip)_audios[name], false, 1f);
    }

    public static void PlaySound(string name, bool loop = false, float volume = 1f, float pitch = 1f)
    {
        PlaySound((AudioClip)_audios[name], loop, volume, pitch);
    }

    public static void PlaySound(AudioClip sound, bool loop = false, float volume = 1f, float pitch = 1f)
    {
        if (sound == null) return;
        foreach (AudioSource src in Instance._soundSources)
        {
            if (!src.isPlaying)
            {
                src.name = sound.name;
                src.loop = loop;
                src.volume = SfxVolume * volume;
                src.clip = sound;
                src.pitch = pitch;
                src.Play();
                return;
            }
        }

        AudioSource newSrc = CreateNewSource();
        newSrc.loop = loop;
        newSrc.volume = _sfxVolume * volume;
        newSrc.PlayOneShot(sound);
    }

    public static void SetChannelVolume(string channel, float volume)
    {
        Instance.transform.Find(channel).GetComponent<AudioSource>().volume = volume;
    }

    private static AudioSource CreateNewSource()
    {
        GameObject temp = new GameObject();
        temp.transform.parent = Instance.transform;
        temp.transform.localPosition = Vector3.zero;

        AudioSource src = temp.AddComponent<AudioSource>();
        Instance._soundSources.Add(src);

        return src;
    }

    private void Awake()
    {
        GetAudioSources();
        FillAudioDictionary();

        SfxVolume = 1f;

        //DontDestroyOnLoad(gameObject);
    }

    private void GetAudioSources()
    {
        foreach (Transform child in transform)
        {
            AudioSource src = child.GetComponent<AudioSource>();
            _soundSources.Add(src);
        }
    }

    private void FillAudioDictionary()
    {
        foreach (AudioNode node in AudioNodes)
            if (!_audios.ContainsKey(node.name))
                _audios.Add(node.name, node.sound);
    }
}