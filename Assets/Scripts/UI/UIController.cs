using System;
using Common.Extentions;
using Events;
using Settings;
using UI.EndBattlePanel;
using UI.TopHUDPanel;
using UniTaskPubSub;

namespace UI
{
    public class UIController : IDisposable
    {
        private readonly AsyncMessageBus _messageBus;
        private readonly GameSettings _gameSettings;
        private readonly TopHUDModel _topHUD;
        private readonly EndBattlePanelModel _endBattlePanelModel;
        
        private CompositeDisposable _subscriptions;

        public UIController( AsyncMessageBus messageBus,
            GameSettings gameSettings,
            TopHUDModel topHUD,
            EndBattlePanelModel endBattlePanelModel)
        {
            _messageBus = messageBus;
            _gameSettings = gameSettings;
            _topHUD = topHUD;
            _endBattlePanelModel = endBattlePanelModel;
        }

        public void Initialize()
        {
            _subscriptions = new CompositeDisposable
            {
                _messageBus.Subscribe<PlayerHealthWasChangedEvent>(PlayerHealthWasChangedHandler),
            };
            
            _topHUD.Show(_gameSettings.PlayerInitialHealth);
        }

        public void InitGameUI()
        {
            _topHUD.Show(_gameSettings.PlayerInitialHealth);
        }

        public void ShowEndBattlePanelHandler(LastGameResultProvider lastGameResultProvider, Action callback)
        {
            _endBattlePanelModel.Show(lastGameResultProvider, callback);
        }

        private void PlayerHealthWasChangedHandler(PlayerHealthWasChangedEvent eventData)
        {
            _topHUD.Show(eventData.CurrentPlayerHealth);
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }
    }
}