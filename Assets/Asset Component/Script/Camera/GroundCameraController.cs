using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GroundCameraController : MonoBehaviour
{
    #region Variable

    [Header("Main Component")]
    [SerializeField] private int groundNumber;
    [SerializeField] private int targetPixelPerUnitCamera;
    
    private int currentPixelPerUnitCamera;
    private int normalPixelPerUnitCamera;
    private bool isMoveGround;
    
    // Constant Parameter
    private const string IS_GROUND = "isGround";
    private const string GROUND_NUM = "groundNum";
    
    [Header("Reference")]
    [SerializeField] private PixelPerfectCamera pixelPerfectCamera;
    private Animator cameraAnim;
    
    #endregion
    
    #region MonoBehaviour Callbacks

    private void Awake()
    {
        cameraAnim = GameObject.Find("CameraController").GetComponent<Animator>();
    }

    private void Start()
    {
        normalPixelPerUnitCamera = pixelPerfectCamera.assetsPPU;
        currentPixelPerUnitCamera = normalPixelPerUnitCamera;
    }

    private void Update()
    {
        pixelPerfectCamera.assetsPPU = currentPixelPerUnitCamera;
        
        if (isMoveGround)
        {
            ResizeCameraGround();
        }
        else
        {
            ResizeCameraPlayer();
        }
    }
    
    #endregion

    #region Collider Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch (groundNumber)
            {
                case 1:
                    
                    cameraAnim.SetBool(IS_GROUND, true);
                    cameraAnim.SetInteger(GROUND_NUM,1);
                    break;
                case 2:
                    cameraAnim.SetBool(IS_GROUND, true);
                    cameraAnim.SetInteger(GROUND_NUM,2);
                    break;
                case 3:
                    cameraAnim.SetBool(IS_GROUND, true);
                    cameraAnim.SetInteger(GROUND_NUM,3);
                    break;
                case 4:
                    cameraAnim.SetBool(IS_GROUND, true);
                    cameraAnim.SetInteger(GROUND_NUM,4);
                    break;
                default:
                    Debug.Log("Ground numberx salah kang");
                    break;
            }
            
            isMoveGround = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraAnim.SetBool(IS_GROUND, false);
            isMoveGround = false;
        }
    }

    #endregion
    
    #region Relax With Us Callbacks

    private void ResizeCameraGround()
    {
        if (currentPixelPerUnitCamera > targetPixelPerUnitCamera)
        {
            currentPixelPerUnitCamera--;
            Debug.Log($"Camera ke ground is {currentPixelPerUnitCamera}");
        }
    }
    
    private void ResizeCameraPlayer()
    {
        if (currentPixelPerUnitCamera < normalPixelPerUnitCamera)
        {
            currentPixelPerUnitCamera++;
            Debug.Log($"Camera balek player is {currentPixelPerUnitCamera}");
        }
    }

    #endregion

}
