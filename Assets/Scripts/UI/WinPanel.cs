using System;
using System.Threading.Tasks;
using Mirror;
using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UIMove))]
public class WinPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winPlayerText;
    private UIMove _moveAnimation;
    private void Awake()
    {
        _moveAnimation = GetComponent<UIMove>();
        
        PlayerScore.OnPlayerWin += DisplayPlayerName;
    }

    private async void DisplayPlayerName(string playerName)
    {
        _moveAnimation.MoveToActive();
        _winPlayerText.text = playerName;

        await Task.Delay(5000);
        //NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        PlayerScore.OnPlayerWin -= DisplayPlayerName;
    }
}
