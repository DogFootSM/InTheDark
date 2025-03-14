using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> monsterList;

    //���� �ý����� �ΰ��� Ÿ�̸ӿ� �����Ǿ�� �� ���� �ӽ÷� 10 �ھƵξ���
    public int Timer = 10;

    private void Start()
    {
        //StartCoroutine(MonsterSpawnCoroutine());
        MonsterSpawn();
    }

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
            list.Add(Instantiate(monsterList[Random.Range(0, monsterList.Count)], monPos, Quaternion.identity));
        }
        //0���� ��ȯ�Ǿ��� ��� �ٽ� ��ȯ
        if (list.Count == 0)
            MonsterSpawn();
    }
}
