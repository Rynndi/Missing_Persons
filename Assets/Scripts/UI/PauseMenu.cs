using UnityEngine.UIElements;

public class PauseMenu : UIElementTemplate
{
    protected override void deinitListeners()
    {
        throw new System.NotImplementedException();
    }

    protected override void generateContent()
    {
        root.AddToClassList("PauseRoot");
        VisualElement pauseMenu = Create("ChecklistRoot", "PausePopup");
        VisualElement textContainer = Create("TextContainer");
        Label titleText = Create<Label>("TitleText");
        titleText.text = "MISSING PERSONS";
        VisualElement buttonContainer = Create("BottomContainer");
        Button exit = Create<Button>("Settings", "ExitButton");
        Button resume = Create<Button>("Pause", "ResumeButton");

        buttonContainer.Add(exit);
        buttonContainer.Add(resume);
        textContainer.Add(titleText);
        pauseMenu.Add(textContainer);
        pauseMenu.Add(buttonContainer);
        root.Add(pauseMenu);
    }
}