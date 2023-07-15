using System;
using UnityEngine;

public class Autopilot : MonoBehaviour
{
    public static Action<bool> OnChangeState { get; set; }
    private PlayerController _playerController;

    [SerializeField] 
    private Transform _rightObserver;
    [SerializeField] 
    private Transform _leftObserver;

    private int _groundLayer = 1 << 6;
    private float _checkGroundDist = 2f;
    private bool _active = false;

    private bool _checkRight => Physics.Raycast(_rightObserver.position, Vector3.down, _checkGroundDist, _groundLayer);
    private bool _checkLeft => Physics.Raycast(_leftObserver.position, Vector3.down, _checkGroundDist, _groundLayer);

    private void OnEnable()
    {
        OnChangeState += ChangeState;
    }

    private void OnDisable()
    {
        OnChangeState -= ChangeState;
    }

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_active) 
            TakeControll();
        else 
            _playerController.EnableInput();
    }

    public void ChangeState(bool state)
    {
        _active = state;
    }

    private void TakeControll()
    {
        _playerController.DisableInput();

        if(_checkRight && !_checkLeft && !_playerController.IsMoveRight())
        {
            _playerController.ChangeTurn();
        }
        else if (_checkLeft && !_checkRight && _playerController.IsMoveRight())
        {
            _playerController.ChangeTurn();
        }
    }
}
