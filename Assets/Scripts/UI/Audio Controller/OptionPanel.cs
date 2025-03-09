using UnityEngine;
using UnityEngine.Audio;

public class OptionPanel : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;

    private string _musicVolume = "MusicVolume";
    private string _masterVolume = "MasterVolume";
    private string _soundVolume = "SoundVolume";
    private float _maxVolume = 1f;
    private float _minVolume = 0.0001f;

    public void ToggelMusic(bool enabled)
    {
        if (enabled)
            _audioMixer.audioMixer.SetFloat(_musicVolume, Mathf.Log10(_maxVolume) * 20);
        else
            _audioMixer.audioMixer.SetFloat(_musicVolume, Mathf.Log10(_minVolume) * 20);
    }

    public void ToggelSound(bool enabled)
    {
        if (enabled)
            _audioMixer.audioMixer.SetFloat(_soundVolume, Mathf.Log10(_maxVolume) * 20);
        else
            _audioMixer.audioMixer.SetFloat(_soundVolume, Mathf.Log10(_minVolume) * 20);
    }

    public void ChangeVolume(float volume)
    {
        if (volume < _minVolume)
            volume = _minVolume;

        _audioMixer.audioMixer.SetFloat(_masterVolume, Mathf.Log10(volume) * 20);
    }
}
