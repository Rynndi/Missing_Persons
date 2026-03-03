using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using Yarn.Markup;
using Yarn.Unity;

public abstract class ToolkitMarkupHandler : MonoBehaviour, IToolkitMarkupHandler
{
    public abstract void OnPrepareForLine(MarkupParseResult line, Label text);

    public abstract void OnLineDisplayBegin(MarkupParseResult line, Label text);

    public abstract YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken);

    public abstract void OnLineDisplayComplete();

    public abstract void OnLineWillDismiss();
}