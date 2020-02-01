using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformsSharing : MonoBehaviour, IPunObservable
{
    public bool syncPosition;
    public bool syncRotation;
    public bool syncScale;
    private bool firstTake = false;
    private Vector3 networkPosition;
    private Vector3 storedPosition;
    private Vector3 direction;
    private Quaternion networkRotation;
    private float distance;
    private float angle;
    [SerializeField]
    private Transform sceneTransform;
    [SerializeField]
    private PhotonView photonView;

    public void Awake()
    {
        storedPosition = transform.position;
        networkPosition = Vector3.zero;
        networkRotation = Quaternion.identity;
        if (sceneTransform == null)
        {
            sceneTransform = transform;
        }
    }

    void OnEnable()
    {
        firstTake = true;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, networkPosition, distance * (1.0f / PhotonNetwork.SerializationRate));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, networkRotation, angle * (1.0f / PhotonNetwork.SerializationRate));
        }
    }
    [PunRPC]
    public void RequestLatestPosition()
    {
        GetComponent<PhotonView>().RPC("UpdatePositionOnLoad", RpcTarget.Others, transform);
    }

    [PunRPC]
    public void UpdatePositionOnLoad(Transform latestUpdate)
    {
        transform.localPosition = latestUpdate.localPosition;
        transform.localRotation = latestUpdate.localRotation;
        transform.localScale = latestUpdate.localScale;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (syncPosition)
            {
                direction = transform.localPosition - storedPosition;
                storedPosition = transform.localPosition;

                stream.SendNext(transform.localPosition);
                stream.SendNext(direction);
            }
            if (syncRotation)
            {
                stream.SendNext(transform.rotation);
            }
            if (syncScale)
            {
                stream.SendNext(transform.localScale);
            }
        }
        else
        {
            if (syncPosition)
            {
                networkPosition = (Vector3)stream.ReceiveNext();
                direction = (Vector3)stream.ReceiveNext();
                if (firstTake)
                {
                    transform.position = networkPosition;
                    distance = 0f;
                }
                else
                {
                    float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                    networkPosition += direction * lag;
                    distance = Vector3.Distance(transform.localPosition, networkPosition);
                }
            }
            if (syncRotation)
            {
                networkRotation = (Quaternion)stream.ReceiveNext();

                if (firstTake)
                {
                    angle = 0f;
                    transform.rotation = networkRotation;
                }
                else
                {
                    angle = Quaternion.Angle(transform.rotation, networkRotation);
                }
            }
            if (syncScale)
            {
                transform.localScale = (Vector3)stream.ReceiveNext();
            }
            if (firstTake)
            {
                firstTake = false;
            }
        }

        
    }
}

