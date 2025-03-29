using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using WebSocketSharp;
using TMPro;
using System;

struct ObjectInfo
{
    string objName;
    int objPrice;
}
public class Database : BaseUI
{
    public static Database instance = null;

    const int DefaultMoney = 250;
    const int DefaultDays = 0;

    [Header("�ؽ�Ʈ")]
    //public TMP_Text dayText;
    //public TMP_Text moneyText;

    DatabaseReference database;

    private void Awake()
    {
        SingletonInit();
        Bind();
        Init();
    }

    private void Start()
    {
    }

    /// <summary>
    /// ������ ����� �Լ� (���� ������ �� ȣ���)
    /// </summary>
    /// <param name="playerId">���̵�</param>
    /// <param name="slot">���̺� ����</param>
    /// <param name="money">���� �ݾ�</param>
    /// <param name="day">���� ��¥</param>
    public void SaveData(string playerId, string slot, int money, int day)
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
        {"Money", money},
        {"Days", day}
    };

        // ���� �����͸� �ش� ������ Ư�� ���� ��ġ�� ����
        database.Child("UserData").Child(playerId).Child(slot.ToString())
            .SetValueAsync(slotData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("������ ���� ����: " + task.Exception);
                }
            });
    }

    public void ResetData(string playerId, string slot)
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
        {"Money", DefaultMoney},
        {"Days", DefaultDays}
    };



        // ���� �����͸� �ش� ������ Ư�� ���� ��ġ�� ����
        database.Child("UserData").Child(playerId).Child(slot.ToString())
            .SetValueAsync(slotData)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("������ ���� ����: " + task.Exception);
                }
            });
    }

    /// <summary>
    /// ������ �ҷ����� �Լ�/
    /// ���� ��ư�� �ش� �Լ� �־�ΰ� �÷��̾� ���̵�� ���� �־��ִ� ������ ���
    /// </summary>
    /// <param name="playerId">������ �ҷ����� ���� Key��</param>
    /// <param name="slot">��� ��������</param>
    /// 
    public void LoadData(string playerId, string slot)
    {

        database.Child("UserData").Child(playerId).Child(slot.ToString())
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

                Debug.Log($"snapshot.Exists: {snapshot.Exists}");

                if (snapshot.Exists)
                {
                    if (snapshot.HasChild("Money"))
                    {
                        object moneyValue = snapshot.Child("Money").Value;
                        if (moneyValue != null)
                        {
                            IngameManager.Instance.money = Convert.ToInt32(moneyValue);
                        }
                    }
                    else
                    {
                        IngameManager.Instance.money = DefaultMoney;
                    }


                    if (snapshot.HasChild("Days"))
                    {
                        object daysValue = snapshot.Child("Days").Value;
                        if (daysValue != null)
                        {
                            IngameManager.Instance.days = Convert.ToInt32(daysValue);
                        }
                    }
                    else
                    {
                        IngameManager.Instance.days = DefaultDays;
                    }

                }
                else
                {
                    Debug.Log("����� ���� �����Ͱ� �����ϴ�. ���ο� ���� �����͸� �ҷ��ɴϴ�.");
                    //�׽�Ʈ �ڵ�
                    //moneyText.text = "150";
                    //dayText.text = "1";

                    IngameManager.Instance.money = 250;
                    IngameManager.Instance.days = 1;

                }
            }
        });
    }

    void Init()
    {

        database = FirebaseDatabase.DefaultInstance.RootReference;

       //�׽�Ʈ �ڵ�
       // if (dayText != null)
       //     if (moneyText != null)
       //     {
       //         dayText.text = "1";
       //         moneyText.text = "150";
       //     }
    }

    void SingletonInit()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
