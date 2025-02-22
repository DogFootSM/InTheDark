using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using WebSocketSharp;
using TMPro;

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

    [Header("�ؽ�Ʈ")]
    [SerializeField] TMP_Text dayText;
    [SerializeField] TMP_Text moneyText;

    DatabaseReference database;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        //SaveData("1���÷��̾�", 1, 100, 1);
        LoadData("1���÷��̾�", 1);
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

        //������ Key�� ������ ����
        Dictionary<string, object> slotData = new Dictionary<string, object>
    {
        {"������", money},
        {"���� ��¥", day}
    };

        // ���� �����͸� �ش� ������ Ư�� ���� ��ġ�� ����
        database.Child("Players").Child(playerId).Child("Slots").Child("slot_" + slot)
            .SetValueAsync(slotData)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("������ ���� ����: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("������ ���� ����! �÷��̾� ID: " + playerId + " | ����: " + slot);
                }
            });
    }

    /// <summary>
    /// ������ �ҷ����� �Լ�/
    /// ���� ��ư�� �ش� �Լ� �־�ΰ� �÷��̾� ���̵�� ���� �־��ִ� ������ ���
    /// </summary>
    /// <param name="playerId">������ �ҷ����� ���� Key��</param>
    /// <param name="slot">��� ��������</param>
    void LoadData(string playerId, int slot)
    {
        database.Child("Players").Child(playerId).Child("Slots").Child("slot_" + slot)
        .GetValueAsync()
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("������ �ҷ����� ����: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    Dictionary<string, object> slotData = snapshot.Value as Dictionary<string, object>;

                    // money�� day �� ��������
                    int money = int.Parse(slotData["������"].ToString());
                    int day = int.Parse(slotData["���� ��¥"].ToString());

                    moneyText.text = money + "��";
                    dayText.text = day + "����";

                    Debug.Log($"������ �ҷ����� ����! �÷��̾� ID: {playerId}, ����: {slot}, ������: {money}, ��¥: {day}");
                }
                else
                {
                    Debug.Log("����� ���� �����Ͱ� �����ϴ�.");
                }
            }
        });
    }

   


    void Init()
    {

        database = FirebaseDatabase.DefaultInstance.RootReference;

        dayText.text = "0����";
        moneyText.text = "0��";
    }




}
