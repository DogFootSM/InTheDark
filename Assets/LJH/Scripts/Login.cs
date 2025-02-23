using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : BaseUI
{
    [Header("�׽�Ʈ��")]
    [SerializeField] bool isTest;

    Button LoginButton;
    Button JoinButton;

    [SerializeField] TMP_InputField idField;
    [SerializeField] TMP_InputField passwordField;

    DatabaseReference database;
    FirebaseAuth auth;

    string id;
    string password;

    private void Awake()
    {
        Bind();
        Init();
    }

    void _LoginButton()
    {
        id = idField.text;
        password = passwordField.text;

        Debug.Log("�α��ι�ư ����");
        NullCheck();
        FirebaseManager.Auth.SignInWithEmailAndPasswordAsync(id, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            AuthResult result = task.Result;
            Debug.Log($"���� �α��� ����! {result.User.DisplayName} ({result.User.UserId})");

            // �̸��� ���� ���� 
            if (!isTest && result.User.IsEmailVerified.Equals(false))
            {
                Debug.Log("�̸��� ���� �ʿ�");
                return;
            }
            try
            {
                Debug.Log("���̵�����");
                SceneManager.LoadScene("DataBaseScene");
                Debug.Log("���̵�����");
            }
            catch (Exception ex)
            {
                Debug.LogError($"�� ��ȯ �� ���� �߻�: {ex.Message}");
            }
        });
    }

    private void CheckUserInfo()
    {
        string uid = FirebaseManager.Auth.CurrentUser.UserId;
        DatabaseReference userDataRef = FirebaseManager.Database.RootReference.Child("UserData").Child(uid);

        userDataRef.GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogWarning("�� �������� ��ҵ�");
                    return;
                }
                else if (task.IsFaulted)
                {
                    Debug.LogWarning($"�� �������� ���� : {task.Exception.Message}");
                    return;
                }

                Debug.Log("�� �������� ����!");
                DataSnapshot snapshot = task.Result;

                if (snapshot.Value is null)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.name = FirebaseManager.Auth.CurrentUser.DisplayName;
                    playerData.email = FirebaseManager.Auth.CurrentUser.Email;


                    string json = JsonUtility.ToJson(playerData);
                    userDataRef.SetRawJsonValueAsync(json);
                }
                else
                {
                    Debug.Log(snapshot.Child("name").Value);
                    Debug.Log(snapshot.Child("email").Value);

                    foreach (DataSnapshot data in snapshot.Child("record").Children)
                    {
                        Debug.Log($"Record's {data} : {data.Value}");
                    }
                }

            });

        

    }

    void _JoinButton()
    {
        id = idField.text;
        password = passwordField.text;

        Debug.Log("ȸ�����Թ�ư ����");
        NullCheck();
        FirebaseManager.Auth.CreateUserWithEmailAndPasswordAsync(id, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    void NullCheck()
    {
        if (idField.text == "")
        {
            Debug.Log("ID�� �Է����ּ���.");
            return;
        }
        if (passwordField.text == "")
        {
            Debug.Log("��й�ȣ�� �Է����ּ���.");
            return;
        }
    }









    void Init()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        database = FirebaseDatabase.DefaultInstance.RootReference;
        
        LoginButton = GetUI<Button>("Login");
        JoinButton = GetUI<Button>("Join");

        idField = GetUI<TMP_InputField>("Id");
        passwordField = GetUI<TMP_InputField>("Password");

        LoginButton.onClick.AddListener(_LoginButton);
        JoinButton .onClick.AddListener(_JoinButton);
    }
}
