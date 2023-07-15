using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public static Action OnPassedPlatform { get; set; }

    private Rigidbody _rigidbody;

    private bool _passed = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_passed) return;

        if(other.tag == "Player")
        {
            _passed = true;
            _rigidbody.isKinematic = false;
            Invoke(nameof(Deactivate), 2f);
            OnPassedPlatform?.Invoke();
        }
    }

    private void Deactivate()
    {
        _passed = false;
        gameObject.SetActive(false);
    }
}
