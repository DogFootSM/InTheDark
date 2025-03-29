using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoorOnOff : MonoBehaviourPun
{
    [SerializeField] List<GameObject> lockDoorList;
    [SerializeField] List<GameObject> openDoorList;

    private void Start()
    {
        //��
        if (openDoorList.Count > 0)
        {
            CreateDoor(lockDoorList, openDoorList);
        }
        //����
        else
        {
            if (PhotonNetwork.IsMasterClient)
                CreateDoor(lockDoorList);
        }
    }

    /// <summary>
    /// �� �� ��� �̰ɷ�
    /// </summary>
    /// <param name="lockList"></param>
    /// <param name="openList"></param>
    void CreateDoor(List<GameObject> lockList, List<GameObject> openList)
    {

        for (int i = 0; i < lockList.Count - 2; i++)
        {
            openList[i].SetActive(!lockList[i].activeSelf);
        }

    }

    /// <summary>
    /// ������ ��� �̰ɷ�
    /// </summary>
    /// <param name="lockList"></param>
    void CreateDoor(List<GameObject> lockList)
    {
        int howManyLocked;

        howManyLocked = Random.Range(0, lockList.Count);
        for (int i = 0; i < howManyLocked; i++)
        {
            lockList[i].SetActive(true);
        }

        photonView.RPC(nameof(RPCSyncDoors), RpcTarget.Others, howManyLocked);

    }

    [PunRPC]
    void RPCSyncDoors(int howManyLocked)
    {
        for (int i = 0; i < howManyLocked; i++)
        {
            lockDoorList[i].SetActive(true);
        }
    }

}
