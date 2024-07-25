using Common.MVC.View;
using TMPro;
using UnityEngine;

namespace UI.TopHUDPanel
{
    public class TopHUDView : UiDataView<int>
    {
        [SerializeField] private TextMeshProUGUI _playerHPText;
        
        protected override void Init()
        {
        }

        public override void Refresh()
        {
            _playerHPText.text = $"Current Health - <color=#ACC5EC>{Data}</color>";
        }
    }
}