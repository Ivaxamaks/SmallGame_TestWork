using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class BoundaryPointsProvider
    {
        [SerializeField] private Transform _leftBoundary;
        [SerializeField] private Transform _rightBoundary;
        [SerializeField] private Transform _topBoundary;
        [SerializeField] private Transform _bottomBoundary;

        public Transform LeftBoundary => _leftBoundary;
        public Transform RightBoundary => _rightBoundary;
        public Transform TopBoundary => _topBoundary;
        public Transform BottomBoundary => _bottomBoundary;
    }
}