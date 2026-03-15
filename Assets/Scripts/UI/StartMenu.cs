using Unity.VisualScripting;
using UnityEngine.UIElements;

public class StartMenu : UIElementTemplate
{
    SceneLoader loader;
    Button start;
    Button settings;

    protected override void deinitListeners()
    {
        start.clicked -= loader.transitionScene;
        settings.clicked -= GlobalEvents.TriggerPauseButtonClicked;
    }

    protected override void generateContent()
    {
        root.AddToClassList("PauseRoot");
        VisualElement buttonContainer = Create("BottomContainer");
        start = Create<Button>("Pause", "StartButton");
        settings = Create<Button>("Settings");

        root.Add(start);
        root.Add(settings);

        loader = GetComponentInChildren<SceneLoader>();
        start.clicked += loader.transitionScene;
        settings.clicked += GlobalEvents.TriggerPauseButtonClicked;
    }
}