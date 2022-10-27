using System;
using System.Threading.Tasks;
using Cinemachine;
using Mirror;
using UnityEngine;

public class PlayerMove : NetworkBehaviour, IMovable
{
    
    [SerializeField] private float _baseSpeed = 5;
    private Vector3 _moveVector;
    
    public static Action<Vector3> OnPlayerMoved;
    
    public static Action OnPlayerSprinted;
    private Camera _camera;
    private bool _isSprinting = false;
    public bool IsSprinting => _isSprinting;

    [SerializeField] private CinemachineFreeLook _cameraFreeLook;
    [SerializeField] private Transform _cameraTracking;
    private void Awake()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnStartLocalPlayer()
    {
        _cameraFreeLook = CinemachineFreeLook.FindObjectOfType<CinemachineFreeLook>();
        _cameraFreeLook.LookAt = _cameraTracking;
        _cameraFreeLook.Follow = _cameraTracking;
    }

    public void Move()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;

        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 forwardRelativeInput = verticalMove * forward;
        Vector3 rightRelativeInput = horizontalMove * right;

        Vector3 cameraRelativeMovement = forwardRelativeInput + rightRelativeInput;

        Vector3 clampedPosition = transform.position;
        
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -8, 8);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -8, 8);
        
        transform.position = clampedPosition;
        
        OnPlayerMoved?.Invoke(cameraRelativeMovement);
        transform.Translate(cameraRelativeMovement * Time.deltaTime * _baseSpeed, Space.World);

    }

    public async void Sprint()
    {
        if (Input.GetMouseButtonDown(0) && !_isSprinting)
        {
            _isSprinting = true;
            OnPlayerSprinted?.Invoke();
            await Task.Delay(1500);
            _isSprinting = false;
        } 
        
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Move();
        Sprint();
    }
}
