using UnityEngine.UIElements;

public class InGameMenu : UIElementTemplate
{
    VisualElement container;
    Button settings;
    Button pause;
    protected override void deinitListeners()
    {
        pause.clicked -= GlobalEvents.TriggerPauseButtonClicked;
        GlobalEvents.pauseButtonClicked -= toggleVisibility;
        GlobalEvents.resumeButtonClicked -= toggleVisibility;
    }

    protected override void generateContent()
    {
        container = Create("MenuContainer");
        pause = Create<Button>("Pause");

        container.Add(settings);
        container.Add(pause);

        root.Add(container);

        pause.clicked += GlobalEvents.TriggerPauseButtonClicked;
        GlobalEvents.pauseButtonClicked += toggleVisibility;
        GlobalEvents.resumeButtonClicked += toggleVisibility;
    }
}