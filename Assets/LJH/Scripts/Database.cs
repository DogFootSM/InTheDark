using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

struct ObjectInfo
{
    string objName;
    int objPrice;
}
public class Database : MonoBehaviour
{
    [Header("������ ����")]
    int money;
    int days;
    ObjectInfo obj;

    DatabaseReference database;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        SaveData("1���÷��̾�", 1, 100, 1);
    }

    /// <summary>
    /// ������ ����� �Լ� (���� ������ �� ȣ���)
    /// </summary>
    /// <param name="playerId">���̵�</param>
    /// <param name="slot">���̺� ����</param>
    /// <param name="money">���� �ݾ�</param>
    /// <param name="day">���� ��¥</param>
    void SaveData(string playerId, int slot, int money, int day)
    {
        //
        // �������� ������ 1 ~ 5 ����
        // ���� 1�� �������� ��, �ش��ϴ� ���Ե����Ϳ� ���� ��¥�� ����Ǿ�� ��
        // �÷��̾�� ������ �ҷ�����
        // ���Կ� ���� �ش� �����͸� �ҷ����� ���
        //
        // �����Ҷ� ���Կ� �����͸� �����ϰ�
        // ������ �÷��̾ �ش� ���� ��ġ�� ����
        // ����iD�� Key�� ���� ����

        List<Dictionary<string, object>> slots = new List<Dictionary<string, object>>();

        //������ Key�� ������ ����
        Dictionary<string, object> slotData = new Dictionary<string, object>
        {
            {"������", money},
            {"���� ��¥", day}
        };

        if (database == null)
        {
            Debug.Log("�����ͺ��̽�����־�");
        }
        database.Child("PlayerID").SetValueAsync(playerId);
        database.Child(playerId).Child("Slot").SetValueAsync(slots);
    }

    /// <summary>
    /// ������ �ҷ����� �Լ�
    /// </summary>
    void LoadData()
    {

    }


    void Init()
    {
        
        database = FirebaseDatabase.DefaultInstance.RootReference;
    }
    

    

}
