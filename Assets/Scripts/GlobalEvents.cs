using UnityEngine;
using System;

public static class GlobalEvents
{
    public static event Action onGameStart;
    public static void TriggerGameStart() => onGameStart?.Invoke();

    public static event Action onPauseClicked;
    public static void TriggerPauseClicked() => onPauseClicked?.Invoke();

    public static event Action onPersonKilled;
    public static void TriggerPersonKilled() => onPersonKilled?.Invoke();
}