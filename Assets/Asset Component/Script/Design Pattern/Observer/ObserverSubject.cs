using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObserverSubject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observerAction)
    {
        observers.Add(observerAction);
    }
    
    public void RemoveObserver(IObserver observerAction)
    {
        observers.Remove(observerAction);
    }
    
    protected void NotifyObservers(ItemAction itemAction)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(itemAction);
        }
    }
}
