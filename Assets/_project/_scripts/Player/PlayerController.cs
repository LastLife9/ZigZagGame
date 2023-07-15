using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public static Action OnStartMove { get; set; }
    public static Action OnFallDown { get; set; }
    public static Action OnChangeTurn { get; set; }

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _minYValue = -3f;
    public float Speed { get => _speed;}

    private int _groundLayer = 1 << 6;
    private float _checkGroundDist = 2f;

    private Transform _transform;
    private Rigidbody _rigidbody;
    private PointerEventData _eventDataCurrentPosition;
    private List<RaycastResult> _results;

    private bool _canInput = true;
    private bool _canMove = false;
    private bool _moveRight = true;

    private Vector3 _moveDirection => _moveRight ? Vector3.right : Vector3.forward;
    private bool _checkGround => Physics.Raycast(_transform.position, Vector3.down, _checkGroundDist, _groundLayer);

    private void Awake()
    {
        Instance = this;
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CatchFalling();

        if (!_canMove) 
            return;
        if (!_checkGround) 
            FallDown();
        if (_canInput && Input.GetMouseButtonDown(0) && !IsOverUI()) 
            ChangeTurn();

        Move();
    }

    private void Move()
    {
        _transform.position += _moveDirection * Speed * Time.deltaTime;
    }

    private void FallDown()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(_moveDirection * Speed, ForceMode.Impulse);
        _canMove = false;

        OnFallDown?.Invoke();
    }

    private void CatchFalling()
    {
        if (_transform.position.y <= _minYValue) 
            _rigidbody.isKinematic = true;
    }

    public void ChangeTurn()
    {
        _moveRight = !_moveRight;

        OnChangeTurn?.Invoke();
    }

    public void StartMove()
    {
        _canMove = true;

        OnStartMove?.Invoke();
    }

    private bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    public bool IsMoveRight()
    {
        return _moveRight;
    }
    public void EnableInput()
    {
        _canInput = true;
    }
    public void DisableInput()
    {
        _canInput = false;
    }
}
