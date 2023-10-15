using Highborne.Common.EventBus;
using Highborne.Common.EventBus.Events.UI;
using Highborne.Common.Helpers;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Highborne.Presentation.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class DevConsole : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private StyleSheet _styleSheet;

        private IEventBus _eventBus;
        private VisualElement _devConsole;
        private TextField _inputField;
        private Label _outputLabel;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<ShowDevConsoleEvent>(ShowDevConsoleEventHandler);
            _eventBus.Subscribe<HideDevConsoleEvent>(HideDevConsoleEventHandler);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ShowDevConsoleEvent>(ShowDevConsoleEventHandler);
            _eventBus.Unsubscribe<HideDevConsoleEvent>(HideDevConsoleEventHandler);
        }

        private void ShowDevConsoleEventHandler(ShowDevConsoleEvent showDevConsoleEvent)
        {
            if (_devConsole == null)
            {
                GenerateDevConsole();
            }

            _document.rootVisualElement.Add(_devConsole);
        }

        private void HideDevConsoleEventHandler(HideDevConsoleEvent hideDevConsoleEvent)
        {
            _document.rootVisualElement.Remove(_devConsole);
        }

        private void GenerateDevConsole()
        {
            _devConsole = VisualElementBuilder.Create("dev-console");
            _devConsole.styleSheets.Add(_styleSheet);

            _outputLabel = VisualElementBuilder.Create<Label>("dev-console-output");
            _devConsole.Add(_outputLabel);

            _inputField = VisualElementBuilder.Create<TextField>("dev-console-input");
            _inputField.RegisterCallback<KeyDownEvent>(OnInputFieldKeyDown);
            _devConsole.Add(_inputField);

            _document.rootVisualElement.Add(_devConsole);
        }

        private void OnInputFieldKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
            {
                ProcessCommand(_inputField.value);
                _inputField.value = "";
            }
        }

        private void ProcessCommand(string command)
        {
            _outputLabel.text += "> " + command + "\n";
        }
    }
}