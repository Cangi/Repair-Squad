using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlumbTool : MonoBehaviour
{
    private int fixRoom = 0;
    public int maxToiletHits = 3;
    private bool fixedToilet = false;
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        int remainingHits = maxToiletHits - fixRoom;
        if (collision.gameObject.name == "toilet")
        {
            fixRoom++; 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "roomsimple" && SceneManager.GetActiveScene().name == "HoloLens")
        {
            Player target = null;
            transform.localScale = transform.localScale / 4;
            foreach (Player x in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (x.CustomProperties["Type"] == "Plumber")
                {
                    target = x;
                } 
            }
            PhotonNetwork.GetPhotonView(900).TransferOwnership(target);
        }
    }

    public void Update()
    {
        if (fixRoom >= 3 && fixedToilet == false)
        {
            fixedToilet = true;
            PhotonNetwork.GetPhotonView(99).RPC("ToiletFixed",RpcTarget.All);
        }
    }
}
