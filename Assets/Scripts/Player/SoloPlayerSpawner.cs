using Photon.Pun;
using UnityEngine;

public class SoloPlayerSpawner : MonoBehaviourPun
{
    public GameObject PlayerSpawner;
    void Start()
    {
        //Instantiate(PlayerSpawner, new Vector3(0, 0, 0), Quaternion.identity);
        PhotonNetwork.Instantiate("Player", new Vector3(0, 0, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
