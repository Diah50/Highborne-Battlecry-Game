using Highborne.Common.EventBus;
using Highborne.Common.EventBus.Events.UI;
using Highborne.Common.Helpers;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Highborne.Presentation.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private StyleSheet _styleSheet;

        private IEventBus _eventBus;
        private VisualElement _mainMenu;

        [Inject]
        public void Construct(IEventBus eventBus)     
        {
            _eventBus = eventBus;

            _eventBus.Subscribe<ShowMainMenuEvent>(ShowMainMenuEventHandler);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ShowMainMenuEvent>(ShowMainMenuEventHandler);
        }
        
        private void ShowMainMenuEventHandler(ShowMainMenuEvent showMainMenuEvent)
        {
            if (_mainMenu == null) GenerateMainMenu();

            _document.rootVisualElement.Clear();
            _document.rootVisualElement.Add(_mainMenu);
        }

        private void GenerateMainMenu()
        {
            _mainMenu = VisualElementBuilder.Create("main-menu");
            _mainMenu.styleSheets.Add(_styleSheet);

            _document.rootVisualElement.Add(_mainMenu);

            var navbar = VisualElementBuilder.Create("menu-navbar");
            _mainMenu.Add(navbar);

            var logoImage = VisualElementBuilder.Create<Image>("menu-logo-image");
            navbar.Add(logoImage);

            var singlePlayerButton = VisualElementBuilder.Create<Button>("menu-button");
            singlePlayerButton.text = "Singleplayer";
            singlePlayerButton.clickable.clicked += () => Debug.Log("Singleplayer button clicked");
            navbar.Add(singlePlayerButton);

            var multiplayerButton = VisualElementBuilder.Create<Button>("menu-button");
            multiplayerButton.text = "Multiplayer";
            multiplayerButton.clickable.clicked += () => Debug.Log("Multiplayer button clicked");
            navbar.Add(multiplayerButton);

            var settingsButton = VisualElementBuilder.Create<Button>("menu-button");
            settingsButton.text = "Settings";
            settingsButton.clickable.clicked += () => Debug.Log("Options button clicked");
            navbar.Add(settingsButton);

            var extrasButton = VisualElementBuilder.Create<Button>("menu-button");
            extrasButton.text = "Extras";
            extrasButton.clickable.clicked += () => Debug.Log("Extras button clicked");
            navbar.Add(extrasButton);

            var creditsButton = VisualElementBuilder.Create<Button>("menu-button");
            creditsButton.text = "Credits";
            creditsButton.clickable.clicked += () => Debug.Log("Credits button clicked");
            navbar.Add(creditsButton);

            var quitButton = VisualElementBuilder.Create<Button>("menu-button");
            quitButton.text = "Quit";
            quitButton.clickable.clicked += () => Debug.Log("Quit button clicked");
            navbar.Add(quitButton);
        }
    }
}