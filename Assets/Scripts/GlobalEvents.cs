using System;

public class GlobalEvents
{
    public static event Action OnPipeScaleChanged;

    public static void InvokeOnPipeScaleChanged()
    {
        OnPipeScaleChanged?.Invoke();
    }
}