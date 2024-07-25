using Common.MVC.View;
using TMPro;
using UI.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EndBattlePanel
{
    public class EndBattlePanelView : UiDataView<bool>
    {
        private const string WIN_SING = "WIN";
        private const string LOOSE_SING = "LOOSE";
        
        [SerializeField] private TextMeshProUGUI _tittle;
        [SerializeField] private Button _resetButton;
        
        protected override void Init()
        {
        }

        public override void Show()
        {
            base.Show();
            _resetButton.onClick.AddListener(() => Interact<SimpleClickController>(s => s.OnClickInvoke()));
        }

        public override void Hide()
        {
            base.Hide();
            _resetButton.onClick.RemoveAllListeners();
        }

        public override void Refresh()
        {
            _tittle.text = Data ? WIN_SING : LOOSE_SING;
            _tittle.color = Data ? Color.green : Color.red;
        }
    }
}