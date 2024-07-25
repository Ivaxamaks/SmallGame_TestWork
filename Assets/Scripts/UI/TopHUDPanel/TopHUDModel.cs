using UnityEngine;

namespace UI.TopHUDPanel
{
    public class TopHUDModel : MonoBehaviour
    {
        [SerializeField] private TopHUDView _view;
        
        public void Show(int currentHealth)
        {
            _view.SetData(currentHealth);
            _view.Show();
        }
        
    }
}