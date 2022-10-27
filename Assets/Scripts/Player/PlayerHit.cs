using System;
using System.Threading.Tasks;
using DG.Tweening;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerMove))]
public class PlayerHit : NetworkBehaviour
{
    [SerializeField] private float _colorTransitionDuration;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private float _sprintForce = 750;
    
    public static Action OnPlayerHit;
    private Rigidbody _rigidBody;
    
   
    private PlayerMove _playerMove;
    private void Awake()
    {
        Init();
        PlayerMove.OnPlayerSprinted += Sprint;
    }

    private void Init()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerMove = GetComponent<PlayerMove>();
    }
    private void Sprint()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        _rigidBody.AddForce(transform.forward * _sprintForce);
        
    }

    private void OnDestroy()
    {
        PlayerMove.OnPlayerSprinted -= Sprint;
    }

    public async void GetHit()
    {
        GetComponent<BoxCollider>().enabled = false;
        _mesh.material.DOColor(Color.red, _colorTransitionDuration)
            .SetEase(Ease.Linear);
        
        await Task.Delay(3000);
        GetComponent<BoxCollider>().enabled = true;
        _mesh.material.DOColor(Color.blue, _colorTransitionDuration)
            .SetEase(Ease.Linear);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out PlayerHit anotherPlayer))
        {
            if (_playerMove.IsSprinting)
            {
                OnPlayerHit?.Invoke();
                anotherPlayer.GetHit();
            }
        }
    }
}
