using System;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public event Action OnAttack;
    public void AnimationEventHandler(string eventName)
    {
        //Debug.Log($"Event triggered: {eventName}");
    }

    public void OnAttackFinished(AnimationEvent evt)
    {
        if (evt.animatorClipInfo.weight > 0.5f)
        OnAttack?.Invoke();
    }
}
