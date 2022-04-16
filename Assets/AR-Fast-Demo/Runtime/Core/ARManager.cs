
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    [SerializeField] private ARSession _arSession;
    [SerializeField] private ARCameraManager _arCameraManager;
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private ARPlaneManager _planeManager;
    [SerializeField] private ARSessionOrigin _sessionOrigin;

    public static ARManager Instance;
    public ARSession ARSession => _arSession;
    public ARCameraManager ARCameraManager => _arCameraManager;
    public ARRaycastManager RaycastManager => _raycastManager;
    public ARPlaneManager PlaneManager => _planeManager;
    public ARSessionOrigin SessionOrigin => _sessionOrigin;

    private void Start()
    {
        Instance = this;
    }
}