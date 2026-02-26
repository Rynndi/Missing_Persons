using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

[UxmlElement]
public partial class Row : VisualElement
{

    VisualElement imageContainer;
    Image image;
    Label textbox;
    Texture2D altSprite;


    public Row()
    {
        initRow();
    }

    public Row(string text)
    {
        initRow(text);
    }

    public void setText(string text)
    {
        textbox.text = text;
    }

    public void checkCompleted()
    {
        image.style.unityBackgroundImageTintColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        image.style.backgroundImage = altSprite;
        image.style.opacity = 1;
    }

    public void initRow(string text = "Default Text")
    {
        textbox = Create<Label>("ChecklistLabel");
        textbox.text = text;

        imageContainer = Create<VisualElement>("ImageContainer");
        image = Create<Image>("Checkbox");

        imageContainer.Add(image);

        Add(textbox);
        Add(imageContainer);

        altSprite = Resources.Load<Texture2D>("checkbox completed");
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