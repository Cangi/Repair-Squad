using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCommands : MonoBehaviour
{

    public void OnEvent(EventData phtonEvent)
    {
        Debug.Log("There is an event in the session");
    }
    public GameObject target;

    public void CallToggleTarget()
    { 

        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others};
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
