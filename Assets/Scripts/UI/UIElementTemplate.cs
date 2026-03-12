using System.Collections;
using NUnit.Framework.Constraints;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class UIElementTemplate : MonoBehaviour
{
    [SerializeField]
    private UIDocument document;

    [SerializeField]
    private StyleSheet masterStyleSheet;

    [SerializeField]
    private StyleSheet individualStyleSheet;

    [SerializeField]
    private bool initialVisibility = true;

    public VisualElement root;
    public bool contentCompleted = false;
    private bool visible;
    

    void Start()
    {
        Debug.Log("new template opened");
        StartCoroutine(Generate());
    }

    void OnValidate()
    {
        if(Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    void OnEnable()
    {
        //add listeners & stuff
    }

    void OnDisable()
    {
        deinitListeners();
        //add listeners & stuff
    }

    protected abstract void deinitListeners();

    private IEnumerator Generate()
    {
        yield return null;

        document.rootVisualElement.Clear();
        if (masterStyleSheet != null)
            document.rootVisualElement.styleSheets.Add(masterStyleSheet);
        if (individualStyleSheet != null)
            document.rootVisualElement.styleSheets.Add(individualStyleSheet);

        visible = initialVisibility;
        if (!visible)
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
        }

        VisualElement extContainer = new VisualElement();

        extContainer.AddToClassList("ext-container");
        document.rootVisualElement.Add(extContainer);

        root = extContainer;
        generateContent();
        Debug.Log("progressed");
    }

    public void toggleVisibility()
    {
        if (visible)
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
        }
        else
        {
            document.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        visible = !visible;
    }


    public void clear()
    {
        document.rootVisualElement.Clear();
    }

    //override this method to do the funny
    protected abstract void generateContent();

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

    
}
