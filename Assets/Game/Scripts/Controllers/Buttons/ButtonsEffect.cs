using UnityEngine;

namespace Game
{
    public sealed class ButtonsEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip buttonAudio;
        private AudioService _audioService;

        private void Awake()
        {
            _audioService = OR.Get<AudioService>();
        }
        
        public void PlaySoundEffect()
        {
            _audioService.PlaySfx(buttonAudio);
        }
    }
}