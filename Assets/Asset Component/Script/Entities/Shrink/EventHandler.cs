using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action ChangeBodyEvent;
    public static void CallChangeBodyEvent()
    {
        if (ChangeBodyEvent != null)
            ChangeBodyEvent();
    }

    public static event Action<Vector3, float> ShrinkEvent;
    public static void CallShrinkEvent(Vector3 target, float moveDuration)
    {
        if (ShrinkEvent != null)
            ShrinkEvent(target, moveDuration);
    }
}
