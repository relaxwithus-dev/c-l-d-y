using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SmoothZoomCamera : MonoBehaviour
{
    #region Variable
        
    [SerializeField] private PixelPerfectCamera pixelPerfectCamera;
    [SerializeField] private int zoomLevel;

    #endregion

    #region MonoBehaviour Callbacks

    private void Start()
    {
        zoomLevel = 1;
    }

    private void Update() 
    {
        ZoomPixelPerfectCamera();
    }

    #endregion
    
    #region Relax With Us Callbacks
        
    private void ZoomPixelPerfectCamera()
    {
        var scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
            
        if (scrollWheelInput != 0) {
            zoomLevel += Mathf.RoundToInt(scrollWheelInput * 10);
            zoomLevel = Mathf.Clamp(zoomLevel, 1, 5);
            pixelPerfectCamera.refResolutionX = Mathf.FloorToInt(Screen.width / zoomLevel);
            pixelPerfectCamera.refResolutionY = Mathf.FloorToInt(Screen.height / zoomLevel);
        }
    }

    #endregion
}

