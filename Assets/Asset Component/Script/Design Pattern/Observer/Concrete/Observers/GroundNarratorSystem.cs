using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundNarratorSystem : MonoBehaviour, IObserver
{
    [Header("Main Component")]
    [SerializeField] private List<ObserverSubject> observerSubjects;

    [Header("Item Component")] 
    [SerializeField] private List<GameObject> itemObjects;

    [Header("Reference")]
    private AudioClip itemOneClip;

    #region MonoBehaviour Callbacks
    
    private void OnEnable()
    {
        InitializeAddObserver();
    }

    private void OnDisable()
    {
        InitializeRemoveObserver();
    }

    #endregion

    #region Observers Callbacks

    public void OnNotify(ItemAction itemAction)
    {
        switch (itemAction)
        {
            case ItemAction.ItemOne:

                var spawnPosition = observerSubjects[0].transform.position;
                Instantiate(itemObjects[0], spawnPosition, Quaternion.identity);
                break;
            
            case ItemAction.ItemTwo:

                var spawnPosition2 = observerSubjects[1].transform.position;
                Instantiate(itemObjects[1], spawnPosition2, Quaternion.identity);
                break;
            
            case ItemAction.ItemThree:
            
                var spawnPosition3 = observerSubjects[2].transform.position;
                Instantiate(itemObjects[2], spawnPosition3, Quaternion.identity);
                break;
            
            case ItemAction.Clock:
                // Some Logic
                break;
            default:
                Debug.Log("Item Not Found");
                break;
        }
    }

    #endregion

    #region Another Methods

    private void InitializeAddObserver()
    {
        for (int i = 0; i < observerSubjects.Count; i++)
        {
            observerSubjects[i].AddObserver(this);
        }
    }
    
    private void InitializeRemoveObserver()
    {
        for (int i = 0; i < observerSubjects.Count; i++)
        {
            observerSubjects[i].RemoveObserver(this);
        }
    }

    #endregion
    
    
}