using Services;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private BoundaryPointsProvider _boundaryPointsProvider;
        private InputHandler _inputHandler;
        private float _movingSpeed;

        public void Initialize(
            InputHandler inputHandler,
            BoundaryPointsProvider boundaryPointsProvider,
            float movingSpeed)
        {
            _boundaryPointsProvider = boundaryPointsProvider;
            _inputHandler = inputHandler;
            _movingSpeed = movingSpeed;

            _inputHandler.LeftButtonPressed += MoveLeft;
            _inputHandler.RightButtonPressed += MoveRight;
            _inputHandler.ForwardButtonPressed += MoveForward;
            _inputHandler.BackButtonPressed += MoveBack;
            ResetPosition();
        }

        public void Dispose()
        {
            if (_inputHandler == null) return;
            _inputHandler.LeftButtonPressed -= MoveLeft;
            _inputHandler.RightButtonPressed -= MoveRight;
            _inputHandler.ForwardButtonPressed -= MoveForward;
            _inputHandler.BackButtonPressed -= MoveBack;
        }

        private void Move(Vector2 direction)
        {
            var move = new Vector3(direction.x, direction.y, 0) * _movingSpeed * Time.deltaTime;
            var newPosition = transform.position + move;
            
            newPosition.x = Mathf.Clamp(newPosition.x, _boundaryPointsProvider.LeftBoundary.position.x, _boundaryPointsProvider.RightBoundary.position.x);
            newPosition.y = Mathf.Clamp(newPosition.y, _boundaryPointsProvider.BottomBoundary.position.y, _boundaryPointsProvider.TopBoundary.position.y);

            transform.position = newPosition;
        }

        private void ResetPosition()
        {
            var centerX = (_boundaryPointsProvider.LeftBoundary.position.x + _boundaryPointsProvider.RightBoundary.position.x) / 2;
            var centerY = (_boundaryPointsProvider.BottomBoundary.position.y + _boundaryPointsProvider.TopBoundary.position.y) / 2;
            transform.position = new Vector3(centerX, centerY, transform.position.z);
        }

        private void MoveLeft()
        {
            Move(Vector2.left);
        }

        private void MoveRight()
        {
            Move(Vector2.right);
        }

        private void MoveForward()
        {
            Move(Vector2.up);
        }

        private void MoveBack()
        {
            Move(Vector2.down);
        }
    }
}