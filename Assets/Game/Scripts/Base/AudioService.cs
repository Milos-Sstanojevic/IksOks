using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Audio;

namespace Game
{
    public sealed class AudioService : MonoBehaviour
    {
        private const float enabledDb = 0f;
        private const float mutedDb = -80f;
        private const string sfxParamether = "SFXVolume";
        private const string musicParamether = "MusicVolume";
        [SerializeField] private AudioMixerGroup musicAudioMixer;
        [SerializeField] private AudioMixerGroup sfxAudioMixer;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        
        private AudioSettingsState _audioSettingsState;
        private AudioClip _currentMusicClip;

        public void Init(AudioSettingsState audioSettingsState)
        {
            Assert.IsNotNull(audioSettingsState);
            _audioSettingsState=audioSettingsState;

            _audioSettingsState.OnMusicChanged += OnMusicChanged;
            _audioSettingsState.OnSFXChanged += OnSfxChanged;

            ApplySettings();
        }

        public void PlaySfx(AudioClip clip, float volumeScale = 1f)
        {
            Assert.IsNotNull(clip);
            sfxSource.PlayOneShot(clip, volumeScale);
        }

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            Assert.IsNotNull(clip);

            if (_currentMusicClip == clip && musicSource.isPlaying) return;
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }

        private void OnMusicChanged(bool enabled)
        {
            musicAudioMixer.audioMixer.SetFloat(musicParamether, enabled ? enabledDb : mutedDb);
        }

        private void OnSfxChanged(bool enabled)
        {
            sfxAudioMixer.audioMixer.SetFloat(sfxParamether,enabled ? enabledDb : mutedDb);
        }
        
        private void ApplySettings()
        {
            OnMusicChanged(_audioSettingsState.MusicEnabled);
            OnSfxChanged(_audioSettingsState.SFXEnabled);
        }

        private void OnDestroy()
        {
            _audioSettingsState.OnMusicChanged -= OnMusicChanged;
            _audioSettingsState.OnSFXChanged -= OnSfxChanged;
        }
        

    }
}