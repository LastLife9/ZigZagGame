using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action OnStartMove { get; set; }
    public static Action OnFallDown { get; set; }
    public static Action OnChangeTurn { get; set; }

    [SerializeField] 
    private AudioClip _failClip;
    [SerializeField]
    private AudioClip _switchTurnClip;
    private Transform _transform;
    private Rigidbody _rigidbody;

    private bool _canInput = true;
    private bool _canMove = false;
    private bool _moveRight = true;

    private int _groundLayer = 1 << 6;
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private float _minYValue = -3f;
    private float _checkGroundDist = 2f;
    public float Speed { get => _speed; }
    private Vector3 _moveDirection => _moveRight ? Vector3.right : Vector3.forward;
    private bool _checkGround => Physics.Raycast(_transform.position, Vector3.down, _checkGroundDist, _groundLayer);

    private void Awake()
    {
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
        if (_canInput && Input.GetMouseButtonDown(0) && !Helpers.IsOverUI()) 
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

        SoundManager.Instance.Play(_failClip);
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

        ScoreCounter.Instance.IncreaseCurrentScore();
        SoundManager.Instance.Play(_switchTurnClip);
        OnChangeTurn?.Invoke();
    }

    public void StartMove()
    {
        _canMove = true;

        ScoreCounter.Instance.IncreasePlayedGames();
        OnStartMove?.Invoke();
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
