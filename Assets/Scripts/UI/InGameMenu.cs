using UnityEngine.UIElements;

public class InGameMenu : UIElementTemplate
{
    VisualElement container;
    Button settings;
    Button pause;
    protected override void deinitListeners()
    {
        settings.clicked -= GlobalEvents.TriggerSettingsClicked;
        pause.clicked -= GlobalEvents.TriggerPauseClicked;
    }

    protected override void generateContent()
    {
        container = Create("MenuContainer");
        settings = Create<Button>("Settings");
        pause = Create<Button>("Pause");

        container.Add(settings);
        container.Add(pause);

        root.Add(container);

        settings.clicked += GlobalEvents.TriggerSettingsClicked;
        pause.clicked += GlobalEvents.TriggerPauseClicked;
    }
}