using System;
using UnityEngine;

public class SimpleTimer : MonoBehaviour
{
    private static GameObject _obj;
    private float _targetTimeStart = 0f;
    private float _targetTime = 0f;
    private Action _endAction;
    private bool stopped = false;

    void Update()
    {
        if (stopped)
        {
            return;
        }

        if (_targetTime <= 0)
        {
            return;
        }

        _targetTime -= Time.deltaTime;

        if (_targetTime <= 0.0f)
        {
            _endAction();
            Destroy(this);
        }
    }

    public void StartTimer(float targetTime, Action endAction)
    {
        _targetTime = targetTime;
        _targetTimeStart = targetTime;
        _endAction = endAction;
    }

    public void Restart()
    {
        _targetTime = _targetTimeStart;
    }

    public void Stop()
    {
        stopped = true;
        _targetTime = 0;
        Destroy(this);
    }

    public void SetTimer(float targetTime)
    {
        _targetTime = targetTime;
    }

    public static SimpleTimer Create(float targetTime, Action endAction)
    {
        if (!_obj)
        {
            _obj = new GameObject("Simple Timer");
        }

        var timerComponent = _obj.AddComponent<SimpleTimer>();
        timerComponent.StartTimer(targetTime, endAction);
        return timerComponent;
    }
}