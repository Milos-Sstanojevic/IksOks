using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public sealed class BoardRenderer : MonoBehaviour
    {
        [SerializeField] private List<FieldView> fields;
        [SerializeField] private PieceThemeSettings pieceThemeSettings;
        private List<ButtonsEffect> _buttonsEffects;
        private List<ParticleSystem> _placingEffects;
        private BoardState _boardState;
        private ThemeState _themeState;
        private GameplayState _gameplayState;
        private int _boardCellsCount;

        private void Awake()
        {
            _boardState=OR.Get<BoardState>();
            _themeState=OR.Get<ThemeState>();
            _gameplayState=OR.Get<GameplayState>();
            _boardCellsCount=_boardState.OneDimension * _boardState.OneDimension;

            Assert.IsTrue(fields.Count == _boardCellsCount);

            _buttonsEffects=new List<ButtonsEffect>(_boardCellsCount);
            _placingEffects=new List<ParticleSystem>(_boardCellsCount);
            for (var i = 0; i < _boardCellsCount; i++)
            {
                var field = fields[i];
                Assert.IsNotNull(field);

                var buttonsEffect = field.GetComponent<ButtonsEffect>();
                Assert.IsNotNull(buttonsEffect);
                Assert.IsNotNull(field.PlacingPfx);

                _buttonsEffects.Add(buttonsEffect);
                _placingEffects.Add(field.PlacingPfx);
            }
            
        }

        private void OnEnable()
        {
            _boardState.OnCellChanged += RenderCell;
            _boardState.OnReset += RenderAll;
            _gameplayState.OnGameOver += OnGameOver;
        }

        private void OnDisable()
        {
            _boardState.OnCellChanged -= RenderCell;
            _boardState.OnReset -= RenderAll;
            _gameplayState.OnGameOver -= OnGameOver;
        }

        private void Start()
        {
            RenderAll();
        }

        private void RenderCell(int index)
        {
            var value = _boardState.Get(index);
            var image = fields[index].Image;

            if (value == BoardState.CellValue.Empty)
            {
                image.sprite = null;
                image.enabled = false;
                StopPlacingEffect(index);
                return;
            }

            var selectedTheme = _themeState.SelectedTheme;
            image.sprite = pieceThemeSettings.GetThemeSprite(selectedTheme,value);
            image.enabled = true;
            _buttonsEffects[index].PlayPlacingEffect(value);
        }

        private void OnGameOver(GameplayState.ResultType result)
        {
            if (result == GameplayState.ResultType.Draw)
                return;

            for (var i = 0; i < _boardCellsCount; i++)
            {
                if (!_gameplayState.IsWinningCell(i))
                    continue;

                PlayWinningEffect(i);
            }
        }

        private void PlayWinningEffect(int index)
        {
            var placingEffect = _placingEffects[index];
            placingEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            placingEffect.Play(true);
        }

        private void StopPlacingEffect(int index)
        {
            _placingEffects[index].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void RenderAll()
        {
            for (var i = 0; i < _boardCellsCount; i++)
                RenderCell(i);
        }
    }
}
