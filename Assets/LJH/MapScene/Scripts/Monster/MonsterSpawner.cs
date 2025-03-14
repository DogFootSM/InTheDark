using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviourPun
{
    [SerializeField] List<GameObject> monsterList;
    List<string> monsterNames = new List<string>();

    //���� �ý����� �ΰ��� Ÿ�̸ӿ� �����Ǿ�� �� ���� �ӽ÷� 10 �ھƵξ���
    public int Timer = 10;

    private void Start()
    {
        monsterNames.Add("Enemy1");

        //StartCoroutine(MonsterSpawnCoroutine());
        MonsterSpawn();
    }

    /// <summary>
    /// Ư�� �ð��� �� ������ ���� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator MonsterSpawnCoroutine()
    {
        while (true)
        {
            if(Timer % 2 == 1)
            {
                MonsterSpawn();
            }
            yield return null;
        }
    }

    void MonsterSpawn()
    {
        List<GameObject> list = new List<GameObject>();

        Vector3 monPos = gameObject.transform.position + new Vector3(Random.Range(0, 11), 0, Random.Range(0, 11));
        if (Random.Range(0, 2) > 0.5f)
        {
            //list.Add(Instantiate(monsterList[Random.Range(0, monsterList.Count)], monPos, Quaternion.identity));
            list.Add(PhotonNetwork.Instantiate(monsterNames[Random.Range(0, monsterList.Count)], monPos, Quaternion.identity));
        }
        //0���� ��ȯ�Ǿ��� ��� �ٽ� ��ȯ
        if (list.Count == 0)
            MonsterSpawn();
    }
}
