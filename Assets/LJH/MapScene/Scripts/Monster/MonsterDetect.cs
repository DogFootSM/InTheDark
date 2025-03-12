using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{
    Monster monster;

    private void Start()
    {
        monster = GetComponentInParent<Monster>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tag.Player))
        {
            //ToDo: ���� ���·� ��ȯ
            monster.state = monsterState.attack;
        }
    }
}
