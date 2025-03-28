using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuidlingRoom : MonoBehaviourPun
{
    // Room2 
    [SerializeField] List<GameObject> roomList_Depth1 = new List<GameObject>();
    [SerializeField] List<GameObject> roomList_Depth2 = new List<GameObject>();
    [SerializeField] List<GameObject> roomList_Depth3 = new List<GameObject>();

    int depth1Check;
    int depth2Check;
    int depth3Check;

    private void Start()
    {
        Init();
        Coroutine finderCo = StartCoroutine(PhotonFinder());


    }

    IEnumerator PhotonFinder()
    {
        yield return new WaitForSeconds(5f);

        if (PhotonNetwork.IsMasterClient)
                    RPCCreaTeRandomToTal();
    }


    [PunRPC]
    void RPCCreaTeRandomToTal()
    {
        
        photonView.RPC("CreateRandomDepth1", RpcTarget.AllViaServer);
        photonView.RPC("CreateRandomDepth2", RpcTarget.AllViaServer);
        photonView.RPC("CreateRandomDepth3", RpcTarget.AllViaServer);
    }


    void CreateRandomDepth1()
    {
        Debug.Log("��");
        //depth1Check�� 0�� ��� == ��� ���� ���� ��� �̱� ������ doWhile�� 0�� �ƴҶ����� �ݺ���Ŵ
        do
        {
            depth1Check = roomList_Depth1.Count;
            foreach (GameObject room in roomList_Depth1)
            {
                room.SetActive(0.5f < Random.Range(0, 2));
                if (room.activeSelf)
                {
                    depth1Check--;
                }
            }
        } while (depth1Check < 2);
    }

    void CreateRandomDepth2()
    {
        //depth1Check�� 0�� ��� == ��� ���� ���� ��� �̱� ������ doWhile�� 0�� �ƴҶ����� �ݺ���Ŵ
        do
        {
            depth2Check = roomList_Depth2.Count;
            foreach (GameObject room in roomList_Depth2)
            {
                room.SetActive(0.5f < Random.Range(0, 2));
                if (room.activeSelf)
                {
                    depth2Check--;
                }
            }
        } while (depth2Check == 0);

        AutoTFCheck(roomList_Depth1[0], roomList_Depth2[0]);
        AutoTFCheck(roomList_Depth1[1], roomList_Depth2[2]);
        AutoTFCheck(roomList_Depth1[2], roomList_Depth2[4]);

        AutoTFCheck(roomList_Depth1[0], roomList_Depth1[1], roomList_Depth2[1]);
        AutoTFCheck(roomList_Depth1[1], roomList_Depth1[2], roomList_Depth2[3]);

    }
    /// <summary>
    /// �ڵ����� �޹� Ȱ��ȭ ó��
    /// fromRoom�� 2�� �ʿ��� ���
    /// </summary>
    /// <param name="fromRoom1"></param>
    /// <param name="fromCheck1"></param>
    /// <param name="fromRoom2"></param>
    /// <param name="fromCheck2"></param>
    /// <param name="toRoom"></param>
    /// <param name="toCheck"></param>
    static void AutoTFCheck(GameObject fromRoom1, GameObject fromRoom2, GameObject toRoom)
    {
        bool fromCheck1 = fromRoom1.activeSelf;
        bool fromCheck2 = fromRoom2.activeSelf;

        if(!fromCheck1 && !fromCheck2)
            toRoom.SetActive(false);

        if(fromCheck1 && fromCheck2)
            toRoom.SetActive(true);
    }

    /// <summary>
    /// �ڵ����� �޹� Ȱ��ȭ ó��
    /// fromRoom�� 1���� �ʿ��� ���
    /// </summary>
    /// <param name="fromRoom"></param>
    /// <param name="toRoom"></param>
    static void AutoTFCheck(GameObject fromRoom, GameObject toRoom)
    {
        bool check = fromRoom.activeSelf;
        toRoom.SetActive(check);
    }

    void CreateRandomDepth3()
    {
        //depth1Check�� 0�� ��� == ��� ���� ���� ��� �̱� ������ doWhile�� 0�� �ƴҶ����� �ݺ���Ŵ
        do
        {
            depth3Check = roomList_Depth3.Count;
            foreach (GameObject room in roomList_Depth3)
            {
                room.SetActive(0.5f < Random.Range(0, 2));
                if (room.activeSelf)
                {
                    depth3Check--;
                }
            }
        } while (depth3Check == 0);
    }



    void Init()
    {
        depth1Check = roomList_Depth1.Count;
        depth2Check = roomList_Depth2.Count;
        depth3Check = roomList_Depth3.Count;
    }
}
