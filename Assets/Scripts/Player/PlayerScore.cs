using System;
using System.Threading.Tasks;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScore : MonoBehaviour
    {
        private int _scoreCount = 0;
        public static Action<string> OnPlayerWin;
        [SerializeField] private TextMeshPro _nickname;

        private void Awake()
        {
            PlayerHit.OnPlayerHit += IncreaseScoreCount;
        }

        private async void IncreaseScoreCount()
        {
            if (_scoreCount + 1 < 3)
            {
                _scoreCount++;
            }
            else
            {
                OnPlayerWin?.Invoke(_nickname.text);
                await Task.Delay(5000);
            
                NetworkManager.singleton.ServerChangeScene(SceneManager.GetActiveScene().name);
            }
        }

        private void OnDestroy()
        {
            PlayerHit.OnPlayerHit -= IncreaseScoreCount;
        }
    }
}
