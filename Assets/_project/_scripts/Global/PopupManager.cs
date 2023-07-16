using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; set; }

    [SerializeField] 
    private Transform parent;

    private string _popupTag = "Popup";

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnText(Transform spawner, string txt)
    {
        GameObject element = ObjectPooling.Instance.SpawnFromPool(_popupTag, Vector3.zero, Quaternion.identity);
        element.transform.SetParent(parent, true);
        element.GetComponent<PopupElement>().SetTarget(spawner, txt);
    }
}
