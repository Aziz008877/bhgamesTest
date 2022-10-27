using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIMove : MonoBehaviour
    {
        [SerializeField] private Ease _easeType;
        [SerializeField] private float _moveDuration;
        [SerializeField] private bool _isGoingBack = false;
        [SerializeField] private Vector2 _startPosition, _activePosition;
        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            //MoveToActive();
        }

        public void MoveToActive()
        {
            MovePanel(_activePosition);
        }

        public void MoveBack()
        {
            MovePanel(_startPosition);
        }
        
        private void MovePanel(Vector2 movePos)
        {
            var sequence = DOTween.Sequence();
            
            sequence.Append(
                _rect
                    .DOAnchorPos(movePos, _moveDuration)
                    .SetEase(_easeType));
        }
    }
}
