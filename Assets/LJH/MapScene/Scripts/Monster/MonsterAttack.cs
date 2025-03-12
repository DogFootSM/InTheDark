using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] GameObject pirate;
    Monster monster;

    private void Start()
    {
        monster = pirate.GetComponent<Monster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(monster.state == monsterState.attack)
            if(other.CompareTag(Tag.Player))
            {
                //Todo : �÷��̾��� ü���� �����ؾ���
                //other.GetComponent<PlayerStat>().hp -= 1;
                Debug.Log("���Ͱ� �÷��̾� ����");
            }
    }
}
