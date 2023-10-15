using UnityEngine.UIElements;

namespace Highborne.Common.Helpers
{
    public static class VisualElementBuilder
    {
        public static VisualElement Create(params string[] classNames) => Create<VisualElement>(classNames);

        public static T Create<T>(params string[] classNames) where T : VisualElement, new()
        {
            var element = new T();
            foreach (var className in classNames)
            {
                element.AddToClassList(className);
            }
            return element;
        }
    }
}