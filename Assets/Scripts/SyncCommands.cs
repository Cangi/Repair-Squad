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
    public GameObject plumbTool;
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
            if(plumbTool != null)
            {
                plumbTool.SetActive(true);
            }
            if(underwaterVar != null)
            {
                underwaterVar.waterRising = true;
            }
        }
    }

    [PunRPC]
    public void ToiletFixed()
    {
        underwaterVar.waterRising = false;
    }
    }
