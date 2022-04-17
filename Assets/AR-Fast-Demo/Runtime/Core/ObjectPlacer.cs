using UnityEngine;

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
    private void Update()
    {
        if (Input.touchCount == 0 || !ARManager.Instance.ARSession.enabled)
            return;
    
        var ray = ARManager.Instance.SessionOrigin.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out var hit))
        {
            _gameObject.SetActive(true);
            _gameObject.transform.position = hit.point;
        }
    }

}
