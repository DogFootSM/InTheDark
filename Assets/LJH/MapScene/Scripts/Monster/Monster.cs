using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum monsterState
{
    idle,
    chase,
    attack,
    returnMove
}
public class Monster : MonoBehaviourPun
{
    public monsterState state;

    [SerializeField] List<GameObject> playerList = new List<GameObject>();
    List<Collider> playerColl = new List<Collider>();
    NavMeshAgent agent;

    [SerializeField] GameObject spawnPoint;
    Vector3 spawnPointPos;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPointPos = GameObject.FindWithTag(Tag.MonsterSpawner).transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag.Player))
        {
            Debug.Log("�ν� �Ϸ�");
            playerList.Add(other.gameObject);
            playerColl.Add(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //�÷��̾� �߰�
        if(other.CompareTag(Tag.Player))
        {
            Debug.Log("�߰���");
            agent.SetDestination(other.gameObject.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(Tag.Player))
        {
            Debug.Log("�߰� ����");
            playerList.Remove(other.gameObject);
            playerColl.Remove(other);

            //�÷��̾ �������� �ٸ� �÷��̾ �� �ε����� �����ִ� �ڵ�
            for (int i = 0; i < playerList.Count; i++)
            {
                if(playerList[i] == null)
                {
                    playerList.Insert(i, playerList[i + 1]);
                    playerList.Remove(playerList[i+1]);
                }
            }
            //���� ��ġ�� ���ư�
            agent.SetDestination(spawnPointPos);
        }
    }
}
