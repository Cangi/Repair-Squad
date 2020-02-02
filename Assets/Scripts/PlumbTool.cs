using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlumbTool : MonoBehaviour
{
    private int fixRoom = 0;
    public int maxToiletHits = 3;
    private bool fixedToilet = false;
    void OnCollisionEnter(Collision collision)
    {
        int remainingHits = maxToiletHits - fixRoom;
        Debug.Log("You have to hit the toilet " + remainingHits + "to fix it");
        if (collision.gameObject.name == "toilet")
        {
            fixRoom++;
        }if (collision.gameObject.name == "roomsimple")
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
