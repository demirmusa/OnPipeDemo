﻿using System;

public class GlobalEvents
{
    public static event Action OnPipeScaleChanged;
    public static void InvokeOnPipeScaleChanged()
    {
        OnPipeScaleChanged?.Invoke();
    }
    
    public static event Action OnGameCollectedItemCountChanged;
    public static void InvokeOnGameCollectedItemCountChanged()
    {
        OnGameCollectedItemCountChanged?.Invoke();
    }
    
    public static event Action OnGameStart;
    public static void InvokeOnGameStart()
    {
        OnGameStart?.Invoke();
    }
    
    public static event Action OnGameEnd;
    public static void InvokeOnGameEnd()
    {
        OnGameEnd?.Invoke();
    }
    
    public static event Action OnSetMenu;
    public static void InvokeOnSetMenu()
    {
        OnSetMenu?.Invoke();
    }
    
    public static event Action OnCollectableTypeChange;
    public static void InvokeOnCollectableTypeChange()
    {
        OnCollectableTypeChange?.Invoke();
    }
}