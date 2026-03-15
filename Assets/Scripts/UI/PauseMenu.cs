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

        if (resume != null)
            resume.clicked -= GlobalEvents.TriggerResumeButtonClicked;

        if (exit != null)
        {
            exit.clicked -= Application.Quit;
            exit.clicked -= stopEditor;
        }
    }

    protected override void generateContent()
    {
        if (root == null)
        {
            Debug.LogError("PauseMenu root is null");
            return;
        }
        Debug.Log("xdd");

        root.AddToClassList("PauseRoot");

        VisualElement pauseMenu = Create("ChecklistRoot", "PausePopup");
        VisualElement textContainer = Create("TextContainer");
        Label titleText = Create<Label>("TitleText");
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

        if (pauseMenu == null || textContainer == null || titleText == null || buttonContainer == null || exit == null || resume == null)
        {
            Debug.LogError("PauseMenu failed to create one or more UI elements");
            return;
        }

        titleText.text = "MISSING PERSONS";

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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}