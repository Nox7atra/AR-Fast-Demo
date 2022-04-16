using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacer : MonoBehaviour
{
    private GameObject _gameObject;

    public void Load(GameObject go)
    {
        if (_gameObject != null)
        {
            Destroy(_gameObject);
        }
        _gameObject = Instantiate(go);
        _gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.touchCount == 0 || !ARManager.Instance.ARSession.enabled)
            return;
        
        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;
        var ray = ARManager.Instance.SessionOrigin.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (ARManager.Instance.RaycastManager.Raycast(ray, ARHitResults, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = ARHitResults[0].pose;
            _gameObject.SetActive(true);
            _gameObject.transform.position = hitPose.position;
        }
    }

    public List<ARRaycastHit> ARHitResults { get; set; }
}
