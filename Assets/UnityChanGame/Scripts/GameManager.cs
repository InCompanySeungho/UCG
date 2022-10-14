using System;
using System.Collections;
using System.Collections.Generic;
using UC;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public static GameManager instance
    {
        get
        {
            return _Instance;
        }
        set
        {
            _Instance = value;
            DontDestroyOnLoad(_Instance);
        }
    }
    private void Awake()
    {
        instance = this;
    }

    // 스테이지 정보
}
