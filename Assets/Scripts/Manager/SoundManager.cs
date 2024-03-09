using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private GameObject _soundRoot = null;

    public void Init()
    {
        if (_soundRoot == null)
        {
            _soundRoot = GameObject.Find("@SoundRoot");
            
            if (_soundRoot == null)
            {
                _soundRoot = new GameObject { name = "@SoundRoot" };
                Object.DontDestroyOnLoad(_soundRoot);

                string[] soundTypeNames = System.Enum.GetNames(typeof(Define.Sound));
                
                for (int count = 0; count < soundTypeNames.Length - 1; count++)
                {
                    GameObject go = new GameObject { name = soundTypeNames[count] };
                    _audioSources[count] = go.AddComponent<AudioSource>();
                    go.transform.parent = _soundRoot.transform;
                }

                _audioSources[(int)Define.Sound.Bgm].loop = true;
            }
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        
        _audioClips.Clear();
    }

    /// <summary>
    /// 음악 속도 조절?
    /// </summary>
    /// <param name="type"> 음악 타입</param>
    /// <param name="pitch"> 속도 </param>
    public void SetPitch(Define.Sound type, float pitch = 1.0f)
    {
        AudioSource audioSource = _audioSources[(int)type];

        if (audioSource == null)
            return;

        audioSource.pitch = pitch;
    }

    public bool Play(Define.Sound type, string path, float volume = 1.0f, float pitch = 1.0f)
    {
        if (string.IsNullOrEmpty(path) == null)
            return false;

        AudioSource audioSource = _audioSources[(int)type];

        if (path.Contains("Sound/") == false)
            path = string.Format("Sound/{0}", path);

        audioSource.volume = volume;

        if (type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resource.Load<AudioClip>(path);

            if (audioClip == null)
                return false;
            
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();

            return true;
        }
        else if (type == Define.Sound.Effect)
        {
            AudioClip audioClip = GetAudioClip(path);

            if (audioClip == null)
                return false;

            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);

            return true;
        }
        else if (type == Define.Sound.Speech)
        {
            AudioClip audioClip = GetAudioClip(path);

            if (audioClip == null)
                return false;
            
            if(audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.pitch = pitch;
            audioSource.Play();

            return true;
        }

        return false;
    }

    private AudioClip GetAudioClip(string path)
    {
        AudioClip audioClip = null;

        if (_audioClips.TryGetValue(path, out audioClip))
            return audioClip;

        audioClip = Managers.Resource.Load<AudioClip>(path);
        _audioClips.Add(path, audioClip);
        return audioClip;
    }
}
