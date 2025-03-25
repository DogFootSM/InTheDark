using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ReturnState : IMonsterState
{
    
    Monster monster;
    

    public void Enter(Monster monster)
    {
        this.monster = monster;
        Debug.Log("���� ���� ����");

        monster.ReturnToSpawn();
    }

    public void Update()
    {
        if(monster.HasPlayers())
        {
            monster.stateMachine.ChangeState(new M_ChaseState(), monster);
        }

        if (monster.IsAtSpawnPoint())
        {
            monster.stateMachine.ChangeState(new M_IdleState(), monster);
        }

        if (monster.Hp <= 0)
        {
            monster.stateMachine.ChangeState(new M_DeadState(), monster);
        }
    }

    public void Exit()
    {
        Debug.Log("���� ���� ����");
    }
}
