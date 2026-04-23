using System;
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
        private BoardState _boardState;
        private ThemeState _themeState;

        private void Awake()
        {
            _boardState=OR.Get<BoardState>();
            _themeState=OR.Get<ThemeState>();
            _buttonsEffects=new List<ButtonsEffect>();
            for(int i=0;i<fields.Count;i++)
                _buttonsEffects.Add(fields[i].GetComponent<ButtonsEffect>());
            
            Assert.IsTrue(fields.Count == _boardState.OneDimension*_boardState.OneDimension);
        }

        private void OnEnable()
        {
            _boardState.OnCellChanged += RenderCell;
            _boardState.OnReset += RenderAll;
        }

        private void OnDisable()
        {
            _boardState.OnCellChanged -= RenderCell;
            _boardState.OnReset -= RenderAll;
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
                return;
            }

            var selectedTheme = _themeState.SelectedTheme;
            image.sprite = pieceThemeSettings.GetThemeSprite(selectedTheme,value);
            image.enabled = true;
            _buttonsEffects[index].PlayPlacingEffect(value);
        }

        private void RenderAll()
        {
            for (int i = 0; i < _boardState.OneDimension * _boardState.OneDimension; i++)
                RenderCell(i);
        }
    }
}