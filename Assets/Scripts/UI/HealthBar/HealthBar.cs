using UnityEngine;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

namespace UI.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private Image _fill;

        private IHealth _healthController;
        private Transform _mainCamera;

        private void Start()
        {
            _slider.interactable = false;
        }

        public void Initialize(IHealth healthController)
        {
            _healthController = healthController;
            _healthController.HealthChanged += SetHealthValue;

            UpdatePosition();
            UpdateScale();

            if (_healthController.IsInitialized)
            {
                SetHealthValue(_healthController.Health, _healthController.MaxHealth);
            }
        }

        private void OnDisable()
        {
            if (_healthController != null)
            {
                _healthController.HealthChanged -= SetHealthValue;
            }
        }

        private void Update()
        {
            var isInitialized = _healthController != null;
            if (!isInitialized)
            {
                return;
            }

            if (!_healthController.IsDead)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            if (_healthController.HealthBarAnchor != null)
            {
                transform.position = _healthController.HealthBarAnchor.position;
            }
        }

        private void UpdateScale()
        {
            if (_healthController.HealthBarAnchor != null)
            {
                transform.localScale = _healthController.HealthBarAnchor.localScale;
            }
        }

        private void SetHealthValue(float newHealth, float maxHealth)
        {
            _slider.value = newHealth / maxHealth;

            if (_fill == null) return;
            var color = _gradient.Evaluate(_slider.value);
            _fill.color = color;
        }
    }
}