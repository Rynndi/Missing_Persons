using Unity.VisualScripting;
using UnityEngine.UIElements;

public class StartMenu : UIElementTemplate
{
    SceneLoader loader;
    Button start;

    protected override void deinitListeners()
    {
        start.clicked -= loader.transitionScene;
    }

    protected override void generateContent()
    {
        root.AddToClassList("PauseRoot");
        VisualElement pauseMenu = Create("ChecklistRoot", "PausePopup");
        VisualElement textContainer = Create("TextContainer");
        Label titleText = Create<Label>("TitleText");
        titleText.text = "MISSING PERSONS";
        VisualElement buttonContainer = Create("BottomContainer");
        start = Create<Button>("Pause", "StartButton");

        buttonContainer.Add(start);
        textContainer.Add(titleText);
        pauseMenu.Add(textContainer);
        pauseMenu.Add(buttonContainer);
        root.Add(pauseMenu);

        loader = GetComponentInChildren<SceneLoader>();
        start.clicked += loader.transitionScene;
    }
}