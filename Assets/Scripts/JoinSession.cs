using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class JoinSession : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public PhotonView sparkyView;
    public PhotonView plumberView;
    // Start is called before the first frame update
    void Start()
    {
        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable();
        switch (SceneManager.GetActiveScene().name)
        {
            case "HoloLens":
                hash.Add("Type","HoloLens");
                break;
            case "Sparky":
                hash.Add("Type", "Sparky");
                break;
            case "Plumber":
                hash.Add("Type", "Plumber");
                break;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["Type"]);
        ConnectNow();
    }

    public void ConnectNow()
    {
        Debug.Log("ConnectAndJoinRandom.ConnectNow() will now call: PhotonNetwork.ConnectUsingSettings().");
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1.0";
    }

    public void Disconect()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnJoinedLobby()
    {
        if (PhotonNetwork.CountOfRooms == 0)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 0;
            PhotonNetwork.CreateRoom("RepairSquadSession", roomOptions, PhotonNetwork.CurrentLobby);
            Debug.Log("Created session");
        }
        else
        {
            PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "");
            PhotonNetwork.JoinRoom("RepairSquadSession");
            Debug.Log("Joining session");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Just joined: " + PhotonNetwork.CurrentRoom.Name);
        switch(SceneManager.GetActiveScene().name)
        {
            case "HoloLens":
                GetComponent<PhotonView>().ViewID = 1;
                //sparkyView.TransferOwnership(PhotonNetwork.PhotonViews[1].OwnerActorNr);
                //plumberView.TransferOwnership(PhotonNetwork.PhotonViews[2].OwnerActorNr);
                break;
            case "Sparky":
                GetComponent<PhotonView>().ViewID = 2;
                break;
            case "Plumber":
                GetComponent<PhotonView>().ViewID = 3;
                break;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.ActorNumber + " joined the room");
        Debug.Log(newPlayer.CustomProperties["Type"]);
        if (SceneManager.GetActiveScene().name == "HoloLens")
        {
            switch (newPlayer.CustomProperties["Type"])
            {
                case "HoloLens":
                    break;
                case "Sparky":
                    Debug.Log("Passed it to Sparky");
                    sparkyView.ViewID = 2;
                    sparkyView.TransferOwnership(newPlayer);
                    break;
                case "Plumber":
                    Debug.Log("Passed it to Plumber");
                    plumberView.ViewID = 3;
                    plumberView.TransferOwnership(newPlayer);
                    break;
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.ActorNumber + " left the room");
    }

    public void OnLeftLobby()
    {
        throw new System.NotImplementedException();
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        throw new System.NotImplementedException();
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo x in roomList)
        {
            Debug.Log(x.Name);
            Debug.Log(x.PlayerCount);
        }
    }

}
