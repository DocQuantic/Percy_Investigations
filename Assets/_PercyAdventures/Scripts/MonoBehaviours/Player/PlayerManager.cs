using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    protected Transform player;
    public bool isInteracting;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public Transform GetPlayer()
    {
        return player;
    }

}
