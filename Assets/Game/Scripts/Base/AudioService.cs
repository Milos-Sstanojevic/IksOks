using System;
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

        public event Action<float> OnMusicVolumeDbChanged;
        public event Action<float> OnSfxVolumeDbChanged;
        
        private AudioSettingsState _audioSettingsState;
        private AudioClip _currentMusicClip;

        public void Init(AudioSettingsState audioSettingsState, float musicVolumeDb, float sfxVolumeDb)
        {
            Assert.IsNotNull(audioSettingsState);
            Assert.IsNotNull(musicAudioMixer);
            Assert.IsNotNull(sfxAudioMixer);
            Assert.IsNotNull(musicSource);
            Assert.IsNotNull(sfxSource);

            _audioSettingsState=audioSettingsState;

            _audioSettingsState.OnMusicChanged += OnMusicChanged;
            _audioSettingsState.OnSfxChanged += OnSfxChanged;

            SetMusicVolume(musicVolumeDb, false);
            SetSfxVolume(sfxVolumeDb, false);
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
            SetMusicVolume(enabled ? enabledDb : mutedDb);
        }

        private void OnSfxChanged(bool enabled)
        {
            SetSfxVolume(enabled ? enabledDb : mutedDb);
        }

        private void SetMusicVolume(float valueDb, bool notify = true)
        {
            musicAudioMixer.audioMixer.SetFloat(musicParamether, valueDb);
            musicSource.mute = valueDb <= mutedDb;

            if (!notify)
                return;

            OnMusicVolumeDbChanged?.Invoke(valueDb);
        }

        private void SetSfxVolume(float valueDb, bool notify = true)
        {
            sfxAudioMixer.audioMixer.SetFloat(sfxParamether, valueDb);
            sfxSource.mute = valueDb <= mutedDb;

            if (!notify)
                return;

            OnSfxVolumeDbChanged?.Invoke(valueDb);
        }

        private void OnDestroy()
        {
            if (_audioSettingsState == null)
                return;

            _audioSettingsState.OnMusicChanged -= OnMusicChanged;
            _audioSettingsState.OnSfxChanged -= OnSfxChanged;
        }
        

    }
}
