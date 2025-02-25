using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

/// <summary>
/// ���̾�̽� �����ϴ� �޴���
/// </summary>
public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    private FirebaseApp app;
    private FirebaseAuth auth;
    private FirebaseDatabase database;

    public static FirebaseApp App => Instance.app;
    public static FirebaseAuth Auth => Instance.auth;
    public static FirebaseDatabase Database => Instance.database;

    private void Awake()
    {
        // �̱��� ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // ȣȯ�� ����
        CheckDependency();
    }

    private void CheckDependency()
    {
        // ȣȯ���� ���ؼ� ������ ���ִ� �κ�
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;
                database = FirebaseDatabase.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                Debug.Log("Firebase ������ Ȯ�� ����!");
            }
            else
            {
                Debug.LogError($"Firebase ������ Ȯ�� ����! ����: {task.Result}");
                // Firebase Unity SDK is not safe to use here.
                app = null;
                auth = null;
                database = null;
            }
        });
    }

}
