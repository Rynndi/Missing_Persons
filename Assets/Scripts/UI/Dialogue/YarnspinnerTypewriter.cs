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

    public YarnspinnerTypewriter()
    {
    }

    public YarnspinnerTypewriter(float speed = 0.3f, string className = "")
    {
        this.speed = speed;
        if (className != "")
            AddToClassList(className);
    }

    public async YarnTask RunTypewriter(MarkupParseResult line, CancellationToken cancellationToken)
    {
        int visibleCharacters = 0;
        string sourceText = line.Text;
        double accumulatedDelay = speed;
        int totalCharCount = sourceText.Length;

        for (int i = 0; i < totalCharCount; i++)
        {
            while (!cancellationToken.IsCancellationRequested && accumulatedDelay < speed)
            {
                double timeBeforeYield = Time.timeAsDouble;
                await YarnTask.Yield();
                double timeAsDouble = Time.timeAsDouble;
                accumulatedDelay += timeAsDouble - timeBeforeYield;
            }

            if (cancellationToken.IsCancellationRequested)
                break;

            visibleCharacters++;
            text = sourceText.Substring(0, visibleCharacters) + "<alpha=#00>" + sourceText.Substring(visibleCharacters);
            accumulatedDelay -= speed;
        }
        text = sourceText;
    }

    public void PrepareForContent(MarkupParseResult line)
    {
        text = line.Text.Substring(0, 0) + "<alpha=#00>"+line.Text.Substring(0);
    }

}