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

public class GameExit : BaseUI
{
    [Header("테스트용")]
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
            Debug.Log($"유저 로그인 성공! {result.User.DisplayName} ({result.User.UserId})");

            // 이메일 인증 여부 
            if (!isTest && result.User.IsEmailVerified.Equals(false))
            {
                Debug.Log("이메일 인증 필요");
                return;
            }

            CheckUserInfo();
            SceneManager.LoadScene("StartScene");
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
                    Debug.LogWarning("값 가져오기 취소됨");
                    return;
                }
                else if (task.IsFaulted)
                {
                    Debug.LogWarning($"값 가져오기 실패 : {task.Exception.Message}");
                    return;
                }

                DataSnapshot snapshot = task.Result;

                if (snapshot.Value is null)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.name = FirebaseManager.Auth.CurrentUser.DisplayName;
                    playerData.email = FirebaseManager.Auth.CurrentUser.Email;
                    playerData.slots = new Dictionary<string, SlotData>();


                    string json = JsonUtility.ToJson(playerData);
                    userDataRef.SetRawJsonValueAsync(json);
                }
                else
                {
                    if (snapshot.HasChild("slots"))
                    {

                        foreach (DataSnapshot slotSnapshot in snapshot.Child("slots").Children)
                        {
                            string slotKey = slotSnapshot.Key;
                            int money = int.Parse(slotSnapshot.Child("money").Value.ToString());
                            int day = int.Parse(slotSnapshot.Child("day").Value.ToString());

                            Debug.Log($"슬롯 {slotKey} - 돈: {money}, 날짜: {day}");
                        }
                    }
                }

            });

    }

    void _JoinButton()
    {
        id = idField.text;
        password = passwordField.text;

        NullCheck();
        FirebaseManager.Auth.CreateUserWithEmailAndPasswordAsync(id, password).ContinueWith(task =>
        {
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
            Debug.Log("ID를 입력해주세요.");
            return;
        }
        if (passwordField.text == "")
        {
            Debug.Log("비밀번호를 입력해주세요.");
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
        JoinButton.onClick.AddListener(_JoinButton);
    }
}
