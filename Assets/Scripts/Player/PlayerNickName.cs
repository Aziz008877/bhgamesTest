using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNickName : MonoBehaviour
{
    [SerializeField] private TextMeshPro _nickNameText;
    private void OnEnable()
    {
        _nickNameText.text = PlayerData.NickName;
    }
}
