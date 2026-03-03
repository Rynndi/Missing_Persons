using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Yarn;
using Yarn.Markup;
using Yarn.Unity;

[UxmlElement]
public partial class YarnspinnerTypewriter : Label
{
    float speed = 0.3f;
    public List<IToolkitMarkupHandler> ActionMarkupHandlers { get; set; } = new List<IToolkitMarkupHandler>();


    public YarnspinnerTypewriter()
    {
        
    }

    public YarnspinnerTypewriter(List<IToolkitMarkupHandler> handler, float speed = 0.3f, string className = "")
    {
        this.speed = speed;
        if (className != "")
            AddToClassList(className);
        ActionMarkupHandlers = handler;
    }

    public async YarnTask RunTypewriter(MarkupParseResult line, CancellationToken cancellationToken)
    {
        Debug.Log("Typewriter attempting to update");
        int visibleCharacters = 0;
        string sourceText = line.Text;
        double accumulatedDelay = speed;
        int totalCharCount = sourceText.Length;

        foreach (IToolkitMarkupHandler actionMarkupHandler in ActionMarkupHandlers)
        {
                actionMarkupHandler.OnLineDisplayBegin(line, this);
        }


        for (int i = 0; i < totalCharCount; i++)
        {
            while (!cancellationToken.IsCancellationRequested && accumulatedDelay < speed)
            {
                double timeBeforeYield = Time.timeAsDouble;
                await YarnTask.Yield();
                double timeAsDouble = Time.timeAsDouble;
                accumulatedDelay += timeAsDouble - timeBeforeYield;
            }

            foreach (IToolkitMarkupHandler actionMarkupHandler2 in ActionMarkupHandlers)
            {
                await actionMarkupHandler2.OnCharacterWillAppear(i, line, cancellationToken).SuppressCancellationThrow();
            }

            visibleCharacters++;
            text = sourceText.Substring(0, visibleCharacters) + "<alpha=#00>" + sourceText.Substring(visibleCharacters);
            accumulatedDelay -= speed;
        }

        text = sourceText;

        foreach (IToolkitMarkupHandler actionMarkupHandler3 in ActionMarkupHandlers)
        {
            actionMarkupHandler3.OnLineDisplayComplete();
        }
    }

    public void PrepareForContent(MarkupParseResult line)
    {
        text = line.Text.Substring(0, 0) + "<alpha=#00>" + line.Text.Substring(0);
        foreach (IToolkitMarkupHandler actionMarkupHandler in ActionMarkupHandlers)
        {
            actionMarkupHandler.OnPrepareForLine(line, this);
        }
    }

    public void ContentWillDismiss()
    {
        foreach (IToolkitMarkupHandler actionMarkupHandler in ActionMarkupHandlers)
        {
            actionMarkupHandler.OnLineWillDismiss();
        }
    }

}