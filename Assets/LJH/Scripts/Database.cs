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


    [Header("������ ����")]
    public int money;
    public int days;
    ObjectInfo obj;

    [Header("�ؽ�Ʈ")]
    public TMP_Text dayText;
    public TMP_Text moneyText;

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
        database.Child("UserData").Child(playerId).Child(slot)
            .SetValueAsync(slotData)
            .ContinueWithOnMainThread(task =>
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
    /// 
    public void LoadData(string playerId, string slot)
    {
        database.Child("UserData").Child(playerId).Child(slot)
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
                    Debug.Log($"������ �ҷ����� ����! ��ü ������: {snapshot.GetRawJsonValue()}");

                    // ���� ���� �� ��������
                    int money = 150;
                    int days = 1;

                    if (snapshot.HasChild("Money") && snapshot.Child("Money").Value is long moneyValue)
                    {
                        money = (int)moneyValue;
                    }
                    if (snapshot.HasChild("Days") && snapshot.Child("Days").Value is long daysValue)
                    {
                        days = (int)daysValue;
                    }

                    // UI ������Ʈ
                    moneyText.text = money.ToString();
                    dayText.text = days.ToString();

                    Debug.Log($"������ �ҷ����� ����! �÷��̾� ID: {playerId}, ����: {slot}, ������: {money}, ��¥: {days}");
                }
                else
                {
                    Debug.Log("����� ���� �����Ͱ� �����ϴ�. ���ο� ���� �����͸� �ҷ��ɴϴ�.");
                    moneyText.text = "150";
                    dayText.text = "1";
                }
            }
        });
    }
    public void LoadData1(string playerId, string slot)
    {
        database.Child("UserData").Child(playerId).Child(slot)
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

                if (snapshot != null)
                {
                    Debug.Log($"������ �ҷ����� ����! ��ü ������: {snapshot.GetRawJsonValue()}");

                    // Dictionary ��ȯ �õ�
                    Dictionary<string, object> slotData = snapshot.Value as Dictionary<string, object>;

                    if (slotData != null)
                    {
                        // Money �� ��������
                        int money = slotData.ContainsKey("Money") && slotData["Money"] is long moneyValue ? (int)moneyValue : 150;

                        // Days �� ��������
                        int days = slotData.ContainsKey("Days") && slotData["Days"] is long daysValue ? (int)daysValue : 1;

                        // UI ������Ʈ
                        moneyText.text = money.ToString();
                        dayText.text = days.ToString();

                        Debug.Log($"������ �ҷ����� ����! �÷��̾� ID: {playerId}, ����: {slot}, ������: {money}, ��¥: {days}");
                    }
                    else
                    {
                        Debug.Log("slotData ��ȯ ����! �⺻������ ����");
                        moneyText.text = "150";
                        dayText.text = "1";
                    }
                }
                else
                {
                    Debug.Log("����� ���� �����Ͱ� �����ϴ�. ���ο� ���� �����͸� �ҷ��ɴϴ�.");
                    moneyText.text = "150";
                    dayText.text = "1";
                }
            }
        });
    }

    void Init()
    {

        database = FirebaseDatabase.DefaultInstance.RootReference;


        if (dayText != null)
            if (moneyText != null)
            {
                dayText.text = "1";
                moneyText.text = "150";
            }
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
