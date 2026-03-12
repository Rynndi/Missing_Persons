using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Yarn.Markup;
using Yarn.Unity;

public class AdvancerMarkupTranslator : ToolkitMarkupHandler
{
    [SerializeField]
    LineAdvancer actionMarkupHandler;


    
    public override void OnPrepareForLine(MarkupParseResult line, Label text)
    {
        actionMarkupHandler.OnPrepareForLine(line, null);    
    }

    public override void OnLineDisplayBegin(MarkupParseResult line, Label text)
    {
        actionMarkupHandler.OnLineDisplayBegin(line, null);    
    }

    public override YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken)
    {
        return actionMarkupHandler.OnCharacterWillAppear(currentCharacterIndex, line, cancellationToken);
    }

    public override void OnLineDisplayComplete()
    {
        actionMarkupHandler.OnLineDisplayComplete();
    }

    public override void OnLineWillDismiss()
    {
        actionMarkupHandler.OnLineWillDismiss();
    }
}