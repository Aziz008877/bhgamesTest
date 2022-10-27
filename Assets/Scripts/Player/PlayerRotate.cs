using Mirror;
using UnityEngine;

public class PlayerRotate : NetworkBehaviour
{
    private void Awake()
    {
        PlayerMove.OnPlayerMoved += Rotate;
    }

    private void Rotate(Vector3 moveVector)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (moveVector != Vector3.zero)
        {
            transform.forward = moveVector;
        }
    }

    private void OnDestroy()
    {
        PlayerMove.OnPlayerMoved -= Rotate;
    }
}
