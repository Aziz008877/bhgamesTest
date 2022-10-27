using System;
using Mirror;
using UnityEngine;

public class PlayerData: NetworkBehaviour
{
    public static string NickName;
    private const string _defaultName = "PlayerName";

    public void SetNickName(string newNickName)
    {
        
            if (string.IsNullOrEmpty(newNickName))
            {
                NickName = _defaultName;
            }
            NickName = newNickName;
            //PlayerPrefs.SetString("NickName", NickName);
        
    }

    public void StartHost()
    {
        
    }
}
