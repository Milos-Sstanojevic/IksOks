using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    
    [CreateAssetMenu(menuName = "Create PieceTheme", fileName = "PieceTheme", order = 0)]
    public sealed class PieceThemeSettings : ScriptableObject
    {
        [Serializable]
        public struct PieceThemeData
        {
            public ThemeState.ThemeId themeId;
            public Sprite spriteX;
            public Sprite spriteO;
        }

        [SerializeField] private PieceThemeData[] pieceThemeData;
        
        public Sprite GetThemeSprite(ThemeState.ThemeId themeId, BoardState.CellValue value)
        {
            Assert.IsNotNull(pieceThemeData);
            for(int i=0;i<pieceThemeData.Length;i++)
                if (pieceThemeData[i].themeId == themeId)
                    return value==BoardState.CellValue.X ? pieceThemeData[i].spriteX : pieceThemeData[i].spriteO;
                
            return null;
        }
    }
}