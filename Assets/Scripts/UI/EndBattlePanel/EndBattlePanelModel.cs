using System;
using UI.Controllers;
using UnityEngine;

namespace UI.EndBattlePanel
{
    public class EndBattlePanelModel : MonoBehaviour
    {
        [SerializeField] private EndBattlePanelView _view;
        
        public void Show(LastGameResultProvider lastGameResult, Action callback)
        {
            var simpleClickController = new SimpleClickController();
            simpleClickController.OnClick += callback;
            _view.SetControllers(simpleClickController);
            _view.SetData(lastGameResult.IsWin);
            _view.Show();
        }
    }
}