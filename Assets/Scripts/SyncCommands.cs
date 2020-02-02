using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCommands : MonoBehaviour
{
    public GameObject target;
    public underwater underwaterVar;

    public void Start()
    {
        underwaterVar = FindObjectOfType<underwater>();
    }

    public void CallToggleTarget()
    {
        Debug.Log("The call of the wild");
        PhotonNetwork.GetPhotonView(99).RPC("TheGrandHack",RpcTarget.All);
    }

    [PunRPC]
    public void TheGrandHack() 
    {
        Debug.Log("Event Blyat no, RPC!!!!");
        if(target != null)
        {
            target.SetActive(!target.activeSelf);
            if(underwaterVar != null)
            {
                underwaterVar.waterRising = true;
            }
        }
    }
    }
