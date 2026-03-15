using UnityEngine;
using UnityEngine.UIElements;

public class InGameMenu : UIElementTemplate
{
    VisualElement container;
    Button settings;
    Button pause;

    protected override void deinitListeners()
    {
        if (pause != null)
            pause.clicked -= GlobalEvents.TriggerPauseButtonClicked;

        GlobalEvents.pauseButtonClicked -= toggleVisibility;
        GlobalEvents.resumeButtonClicked -= toggleVisibility;
    }

    protected override void generateContent()
    {
        if (root == null)
        {
            Debug.LogError("InGameMenu root is null");
            return;
        }

        container = Create("MenuContainer");
        settings = Create<Button>("Settings");
        pause = Create<Button>("Pause");

        if (container == null || settings == null || pause == null)
        {
            Debug.LogError("InGameMenu failed to create one or more UI elements");
            return;
        }

        container.Add(settings);
        container.Add(pause);
        root.Add(container);

        pause.clicked += GlobalEvents.TriggerPauseButtonClicked;
        GlobalEvents.pauseButtonClicked += toggleVisibility;
        GlobalEvents.resumeButtonClicked += toggleVisibility;
    }
}