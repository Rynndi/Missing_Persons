using System.Threading;
using UnityEngine.UIElements;
using Yarn.Markup;
using Yarn.Unity;

public interface IToolkitMarkupHandler
{
    void OnPrepareForLine(MarkupParseResult line, Label text);

    void OnLineDisplayBegin(MarkupParseResult line, Label text);

    YarnTask OnCharacterWillAppear(int currentCharacterIndex, MarkupParseResult line, CancellationToken cancellationToken);

    void OnLineDisplayComplete();

    void OnLineWillDismiss();
}
