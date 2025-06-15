using System;
using PrimeTween;
using UnityEngine;

namespace CompanyName.Game
{

    [CreateAssetMenu(fileName = "GameAnimationsScriptable", menuName = "Scriptable Objects/GameAnimationsScriptable")]
    public class GameAnimationsScriptable : ScriptableObject
    {
        [SerializeField] private CellHighlightData _cellHighlight;
        [SerializeField] private PuzzleSelectionData _puzzleSelection;
        [SerializeField] private PuzzlePlaceOnGridData _puzzlePlaceOnGrid;

        public CellHighlightData CellHighlightData => _cellHighlight;
        public PuzzleSelectionData SelectionData => _puzzleSelection;
        public PuzzlePlaceOnGridData PlaceOnGridData => _puzzlePlaceOnGrid;

        public static float GetAnimationDuration(float duration)
        {
#if DEBUG_UI_ANIMATIONS
            return duration * 10f;
#else
            return duration;
#endif
        }
    }

    public enum GameAnimationType
    {
        PuzzleSelection,
        PuzzlePlaceOnGrid,
    }

    [Serializable]
    public class CellHighlightData
    {
        public float offsetY = 0.2f;
        public float duration = 0.1f;
        public float Duration => GameAnimationsScriptable.GetAnimationDuration(duration);
        public Ease ease = Ease.OutCubic;
    }
    
    [Serializable]
    public class PuzzleSelectionData
    {

    }

    [Serializable]
    public class PuzzlePlaceOnGridData
    {
        public float flyDuration = 0.15f;
        public float FlyDuration => GameAnimationsScriptable.GetAnimationDuration(flyDuration);
        public Ease flyEase = Ease.InCubic;
        public float hitOffestY = 0.1f;
        public float hitDuration = 0.1f;
        public float HitDuration => GameAnimationsScriptable.GetAnimationDuration(hitDuration);
        public int hitFrequency = 1;


    }
}
