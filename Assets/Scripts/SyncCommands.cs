using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCommands : MonoBehaviour
{
    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += Yee;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= Yee;
    }

    private void Yee(EventData obj)
    {
        Debug.Log("It works");
    }

    
    public GameObject target;

    public void CallToggleTarget()
    {
        Debug.Log("The call of the wild");
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(0, null, raiseEventOptions, sendOptions);
    }

    [PunRPC]
    private void ToggleTarget()
    {
        if (target != null)
        {
            target.SetActive(!target.activeSelf);
        }
    }
}
