using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Character_Mei mainCharacter = null;
    public Character_Mei MainCharacter
    {
        get
        {
            return mainCharacter;
        }
    }

    private static GameManager instance = null;
    public static GameManager Inst
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            instance.Initialize();
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void Initialize()
    {
        mainCharacter = FindObjectOfType<Character_Mei>();
    }
}
