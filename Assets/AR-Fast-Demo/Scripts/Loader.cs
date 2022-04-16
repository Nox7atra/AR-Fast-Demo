using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private GameObject _editorPrefab;
    [SerializeField] private GameObject _arPrefab;

    private void Awake()
    {
        if (Application.isEditor)
        {
            Instantiate(_editorPrefab);
        }
        else
        {
            Instantiate(_arPrefab);
        }
    }
}
