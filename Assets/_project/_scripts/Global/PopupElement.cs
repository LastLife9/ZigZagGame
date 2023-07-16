using System.Collections;
using TMPro;
using UnityEngine;

public class PopupElement : MonoBehaviour
{
    [SerializeField]
    private Vector2 _offset;
    [SerializeField]
    private float _duration = 2f;

    private TextMeshProUGUI _messageTxt;
    private RectTransform _rectTransform;
    private Transform _target;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _messageTxt = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(_target != null)
        {
            _rectTransform.anchoredPosition = Helpers.GetPositionOnCanvas(_target.position);
        }
    }

    public void SetTarget(Transform newTarget, string message)
    {
        _target = newTarget;
        _messageTxt.text = message;

        StartCoroutine(DisableAfterTime(_duration));
    }

    public IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        _target = null;
        gameObject.SetActive(false);
    }
}