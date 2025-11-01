using System;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public event Action OnAttack;

    private string _lastEventName;
    private float _lastEventTime;

    public void AnimationEventHandler(string eventName)
    { 
        //Debug.Log($"Event triggered: {eventName}");
    }

    public void OnAttackFinished(AnimationEvent evt)
    {
        string clipName = evt.animatorClipInfo.clip != null
            ? evt.animatorClipInfo.clip.name
            : "Unknown";

        float time = Time.time;

        if (_lastEventName == clipName && time - _lastEventTime < 0.01f)
            return;

        _lastEventName = clipName;
        _lastEventTime = time;

        OnAttack?.Invoke();
    }
}
