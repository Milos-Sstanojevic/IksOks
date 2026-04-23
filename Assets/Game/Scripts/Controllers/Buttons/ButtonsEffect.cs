using UnityEngine;

namespace Game
{
    public sealed class ButtonsEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip xAudio;
        [SerializeField] private AudioClip oAudio;
        
        private AudioService _audioService;

        private void Awake()
        {
            _audioService = OR.Get<AudioService>();
        }
        
        public void PlaySoundEffect(AudioClip audioClip)
        {
            _audioService.PlaySfx(audioClip);
        }

        public void PlayPlacingEffect(BoardState.CellValue value)
        {
            _audioService.PlaySfx(value==BoardState.CellValue.X ? xAudio : oAudio);
        }
    }
}