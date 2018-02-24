using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField] private Text status;
    [SerializeField] private GameObject roomListItemPrefab;
    [SerializeField] private Transform roomListParent;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
    }

    public void RefreshRoomList ()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";
        if (matchList == null)
        {
            status.text = "Couldn't get room list.";
            return;
        }

        foreach (MatchInfoSnapshot match in matchList)
        {
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem roomListItem = roomListItemGO.GetComponent<RoomListItem>();
            if (roomListItem != null)
            {
                roomListItem.Setup(match, JoinRoom);
            }

            roomList.Add(roomListItemGO);
        }

        if (roomList.Count == 0)
        {
            status.text = "No rooms available at the moment";
        }
    }

    void  ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom (MatchInfoSnapshot match)
    {
        networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining";
    }
}
