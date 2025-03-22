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
                app = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.DefaultInstance;
                database = FirebaseDatabase.DefaultInstance;

            }
            else
            {
                Debug.LogError($"Firebase ������ Ȯ�� ����! ����: {task.Result}");
                app = null;
                auth = null;
                database = null;
            }
        });
    }


}
