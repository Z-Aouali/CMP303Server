using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public int CurrentTick = 0;

    public GameObject playerPrefab;

    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = Constants.TPS;

        

        Server.Start(4, 23554);


    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }


    private void FixedUpdate()
    {
        if(CurrentTick % (Constants.TPS * 1) == 0)
        {
            PlayerSync();
        }

        CurrentTick++;
    }


    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefab, new Vector3(0, 0.5f, 0), Quaternion.identity).GetComponent<Player>();
    }


    private void PlayerSync()
    {
        ServerSend.SendSync(CurrentTick);
    }

}


