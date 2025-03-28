using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviourPun
{
    [SerializeField] List<GameObject> monsterList;
    List<string> monsterNames = new List<string>();


    private void Start()
    {
        monsterNames.Add("Enemy1");

        if(PhotonNetwork.IsMasterClient)
        MonsterSpawn();
    }


    void MonsterSpawn()
    {
        // ���� �ּ� ���� ���� ����ϱ� ���� ����Ʈ
        List<GameObject> list = new List<GameObject>();

        Vector3 monPos = gameObject.transform.position + new Vector3(Random.Range(0, 11), 0, Random.Range(0, 11));
        
        if (Random.Range(0, 2) > 0.5f)
        {
            list.Add(PhotonNetwork.Instantiate(monsterNames[Random.Range(0, monsterList.Count)], monPos, Quaternion.identity));
            //���� ������ �� ���� Ŭ������ ������ġ�� ����
            list[list.Count - 1].GetComponent<Monster>().spawnPointPos = monPos;
        }
        //0���� ��ȯ�Ǿ��� ��� �ٽ� ��ȯ
        if (list.Count == 0)
            MonsterSpawn();
    }
}
