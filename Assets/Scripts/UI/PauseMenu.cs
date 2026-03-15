using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class PauseMenu : UIElementTemplate
{
    Button resume, exit;
    Slider volume;
    
    protected override void deinitListeners()
    {
        GlobalEvents.pauseButtonClicked -= toggleVisibility;
        GlobalEvents.resumeButtonClicked -= toggleVisibility;
        resume.clicked -= GlobalEvents.TriggerResumeButtonClicked;
        exit.clicked -= Application.Quit;
        exit.clicked -= stopEditor;
    }

    protected override void generateContent()
    {
        root.AddToClassList("PauseRoot");
        VisualElement pauseMenu = Create("ChecklistRoot", "PausePopup");
        VisualElement textContainer = Create("TextContainer");
        Label titleText = Create<Label>("TitleText");
        titleText.text = "MISSING PERSONS";
        VisualElement buttonContainer = Create("BottomContainer");
        exit = Create<Button>("Settings", "ExitButton");
        resume = Create<Button>("Pause", "ResumeButton");
        volume = new Slider("Volume", 0, 1);
        volume.value = AudioManager.Instance.currentVol;
        volume.direction = SliderDirection.Horizontal;
        volume.AddToClassList("Volume");
        volume.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            AudioManager.Instance.SetMasterVolume(evt.newValue);
        });

        buttonContainer.Add(exit);
        buttonContainer.Add(resume);
        textContainer.Add(titleText);
        pauseMenu.Add(textContainer);
        pauseMenu.Add(buttonContainer);
        root.Add(pauseMenu);

        GlobalEvents.pauseButtonClicked += toggleVisibility;
        GlobalEvents.resumeButtonClicked += toggleVisibility;
        resume.clicked += GlobalEvents.TriggerResumeButtonClicked;
        exit.clicked += Application.Quit;
        exit.clicked += stopEditor;
    }

    

    void stopEditor()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}