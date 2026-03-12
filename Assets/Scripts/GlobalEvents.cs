using UnityEngine;
using System;

public static class GlobalEvents
{
    public static event Action onGameStart;
    public static void TriggerGameStart() => onGameStart?.Invoke();

    public static event Action onPauseInvoked;
    public static void TriggerPauseClicked() => onPauseInvoked?.Invoke();

    public static event Action onResumeInvoked;
    public static void TriggerResumeClicked() => onResumeInvoked?.Invoke();

    public static event Action pauseButtonClicked;
    public static void TriggerPauseButtonClicked() => pauseButtonClicked?.Invoke();

    public static event Action resumeButtonClicked;
    public static void TriggerResumeButtonClicked() => resumeButtonClicked?.Invoke();

    public static event Action onCollected;
    public static void TriggerPersonKilled() => onCollected?.Invoke();
}