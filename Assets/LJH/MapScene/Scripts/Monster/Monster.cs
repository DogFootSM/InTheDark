using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviourPun
{
    public M_StateMachine stateMachine;

    //�÷��̾� ������ ���ӿ�����Ʈ, �ݶ��̴� ������ ����
    [SerializeField] List<GameObject> playerList = new List<GameObject>();
    List<Collider> playerColl = new List<Collider>();
    
    public NavMeshAgent agent;

    [SerializeField] GameObject spawnPoint;
    Vector3 spawnPointPos;

    float attackDistance = 1f;

    private void Start()
    {
        Init();
        StateInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if(other.CompareTag(Tag.Player))
        {
            Debug.Log("�ν� �Ϸ�");
            SearchingPlayer(other);
        }
    }

    void SearchingPlayer(Collider player)
    {
        playerList.Add(player.gameObject);
        playerColl.Add(player);
    }


    private void OnTriggerStay(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        //�÷��̾� �߰�
        if (other.CompareTag(Tag.Player))
        {
            Debug.Log("�߰���");
            agent.SetDestination(other.gameObject.transform.position);
            agent.SetDestination(playerList[0].transform.position);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (other.CompareTag(Tag.Player))
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

    /// <summary>
    /// ������ �÷��̾ ���� ��� True
    /// </summary>
    /// <returns></returns>
    public bool HasPlayers()
    {
        return playerList.Count > 0;
    }

    /// <summary>
    /// ���Ϳ� �÷��̾� �Ÿ��� ���� ���Ϸ� ��������� True ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool PlayerInRange()
    {
        if (playerList.Count == 0) 
            return false;

        //�ӽ÷� 1f�� �� ���Ŀ� �������
        return Vector3.Distance(transform.position, playerList[0].transform.position) < attackDistance;
    }

    /// <summary>
    /// ���Ͱ� ��������Ʈ�� ���� True ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool IsAtSpawnPoint()
    {
        return Vector3.Distance(transform.position, spawnPointPos) < 1f;
    }

    /// <summary>
    /// �÷��̾� �߰�
    /// </summary>
    public void ChasePlayer()
    {
        agent.SetDestination(playerList[0].transform.position);
    }

    /// <summary>
    /// ��������Ʈ�� ����
    /// </summary>
    public void ReturnToSpawn()
    {
        agent.SetDestination(spawnPointPos);
    }

    /// <summary>
    /// ���� �ϴ� �Լ�
    /// </summary>
    public void Attack()
    {
        Debug.Log("����");
    }


    void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnPointPos = GameObject.FindWithTag(Tag.MonsterSpawner).transform.position;
    }

    void StateInit()
    {
        stateMachine = new M_StateMachine();
        stateMachine.ChangeState(new M_IdleState(), this);
    }
}
