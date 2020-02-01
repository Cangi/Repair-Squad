using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCommands : MonoBehaviour
{
    public GameObject target;

    public void CallToggleTarget()
    {
        GetComponent<PhotonView>().RPC("ToggleTarget", RpcTarget.OthersBuffered);
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
