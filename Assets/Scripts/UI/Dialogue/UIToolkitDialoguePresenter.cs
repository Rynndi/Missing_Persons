using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Yarn.Markup;
using Yarn.Unity;

public class UIToolkitDialoguePresenter : DialoguePresenterBase
{
    [SerializeField]
    private UIDocument document;

    [SerializeField]
    private StyleSheet masterStyleSheet;

    [SerializeField]
    private StyleSheet individualStyleSheet;

    [SerializeField]
    private bool hideWhenNotDisplaying = false;

    [SerializeField]
    private bool typewriterEffect = true;

    [SerializeField]
    private bool nameDisplay = false;

    [SerializeField]
    private bool autoAdvance = false;

    [SerializeField]
    private int autoAdvanceDelay = 1;

    [SerializeField]
    private float typewriterSpeed = 0.03f;

    [SerializeField]
    private bool debugText = false;
    

    public VisualElement root;

    public YarnspinnerTypewriter typewriter;
    Label charName;
    bool generated = false;



    [SerializeField] List<ToolkitMarkupHandler> eventHandlers = new List<ToolkitMarkupHandler>();
        private List<IToolkitMarkupHandler> ActionMarkupHandlers
        {
            get
            {
                var pauser = new PauseEventProcessor();
                List<IToolkitMarkupHandler> ActionMarkupHandlers = new()
                {
                    
                };
                ActionMarkupHandlers.AddRange(eventHandlers);
                return ActionMarkupHandlers;
            }
        }

    void Start()
    {
        Debug.Log("new template opened");
        StartCoroutine(Generate());
    }

    void OnValidate()
    {
        if (Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    void Awake()
    {
        if (!typewriterEffect)
            typewriterSpeed = 0;
        if (!autoAdvance)
            autoAdvanceDelay = 0;
    }

    


    private void generateContent()
    {
        generated = false;
        charName = Create<Label>("charName");
        typewriter = new YarnspinnerTypewriter(ActionMarkupHandlers, typewriterSpeed, "dialogueBox");
        if(debugText)
        {
            typewriter.text = "default text for testing";
        }
        root.Add(charName);
        root.Add(typewriter);
        generated = true;
    }



    public override YarnTask OnDialogueCompleteAsync()
    {
        return YarnTask.CompletedTask;
    }

    public override YarnTask OnDialogueStartedAsync()
    {
        return YarnTask.CompletedTask;
    }

    public override async YarnTask RunLineAsync(LocalizedLine line, LineCancellationToken token)
    {
        while (!generated)
        {
            await YarnTask.Yield();
        }

        Debug.Log("Attempting to run a line");
         if (gameObject.activeInHierarchy == false)
        {
            // This line view isn't active; it should immediately report that
            // it's finished presenting.
            Debug.LogWarning($"Can't show line '{line.Text.Text}': not active");
            //return
        }

        if (hideWhenNotDisplaying)
        {
            root.style.display = DisplayStyle.Flex;
        }
        

        MarkupParseResult text = line.TextWithoutCharacterName;
        if (string.IsNullOrWhiteSpace(line.CharacterName) || !nameDisplay)
        {
            charName.style.display = DisplayStyle.None;
        }
        else
        {
            charName.style.display = DisplayStyle.Flex;
            charName.text = line.CharacterName;
        }

        typewriter.PrepareForContent(text);

        //some animations?
        await typewriter.RunTypewriter(text, token.HurryUpToken).SuppressCancellationThrow();

        if (autoAdvance)
        {
            await YarnTask.Delay((int)(autoAdvanceDelay * 1000), token.NextContentToken).SuppressCancellationThrow();
        }
        else
        {
            await YarnTask.WaitUntilCanceled(token.NextContentToken).SuppressCancellationThrow();
        }
    }



    public VisualElement Create(params string[] classnames)
    {
        return Create<VisualElement>(classnames);
    }

    public T Create<T>(params string[] classnames) where T : VisualElement, new()
    {
        var ele = new T();
        foreach (var classname in classnames)
        {
            ele.AddToClassList(classname);
        }
        return ele;
    }

    private IEnumerator Generate()
    {
        yield return null;

        document.rootVisualElement.Clear();
        if (masterStyleSheet != null)
            document.rootVisualElement.styleSheets.Add(masterStyleSheet);
        if (individualStyleSheet != null)
            document.rootVisualElement.styleSheets.Add(individualStyleSheet);

        VisualElement extContainer = new VisualElement();

        extContainer.AddToClassList("ext-container");
        document.rootVisualElement.Add(extContainer);

        root = extContainer;
        generateContent();
        Debug.Log("progressed");
    }
}