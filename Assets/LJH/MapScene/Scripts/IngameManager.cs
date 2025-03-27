using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class IngameManager : MonoBehaviourPun
{
    public static IngameManager Instance;

    public float time { get; set; }
    public float minute { get; set; }

    public int days { get; set; }

    float randomMinute;

    float elapsedTime = 1f;

    public int money { get; set; }

    public string masterID;

    //������ ��ġ ����� ItemSave ��ũ��Ʈ�� ����
    public Dictionary<int, Vector3> posDict = new Dictionary<int, Vector3>();
    public Dictionary<Vector3, string> nameDict = new Dictionary<Vector3, string>();
    [HideInInspector]  public List<int> keyList = new List<int>();


    //���ӿ��� ��
    [SerializeField] GameObject gameOverPopup;
    int playerCount;


    private void Awake()
    {
        Init();
        SingletonInit();
    }

    private void Update()
    {
        //Todo : ������Ʈ�� �� �δ�÷���.. ���ɷ� �ٲٸ� ������ ����
        if (gameOverPopup.activeSelf) return;

        if (GameManager.Instance.PlayerObjects.Where(x => x.IsDeath).Count() ==
                GameManager.Instance.PlayerObjects.Count)
        {
            Invoke(nameof(GameOver), 3f);
        }
    }

    public void PlayerCheck()
    {
        playerCount = GameManager.Instance.PlayerObjects.Count;
    }

    


    //�ð��� ��� 10�ʸ��� 5 ~10���� �帣��
    //am8�÷� �����ؼ�
    //pm8�õǸ� �� ħ��

    /// <summary>
    /// Ÿ�̸� ���� - �ʾ� ���۵� �� ȣ��
    /// </summary>
    public void TimerReset()
    {
        time = 8;
        minute = 0;
    }

    /// <summary>
    /// �ð� ���� - �ʾ� ���۵� �� ȣ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimerCount()
    {
        while (true) 
        {
            randomMinute = Random.Range(5, 10);
            minute += randomMinute;

            if (minute >= 60)
            {
                time++;
                minute = minute - 60;
            }

            if(time >= 12)
            {
                TimeOver();
            }
            yield return new WaitForSeconds(elapsedTime);
        }
    }

    void TimeOver()
    {
        photonView.RPC("RPCPlayerAllDie", RpcTarget.AllViaServer);

    }

    [PunRPC]
    void RPCPlayerAllDie()
    {
        List<GameObject> playerS = new List<GameObject>();

        playerS = GameObject.FindGameObjectsWithTag(Tag.Player).ToList();

        foreach (GameObject player in playerS)
        {
            player.GetComponent<PlayerController>().ChangeState(PState.DEATH);
        }
    }

    public void GameOver()
    {
        gameOverPopup.SetActive(true);
    }

    void Init()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log($"�����̶� ������ ����");
            Database.instance.LoadData(FirebaseManager.Auth.CurrentUser.UserId, "Slot_1");

        }
        else
        {
            Debug.Log($"{masterID}�� ����");
            Database.instance.LoadData(masterID, "Slot_1");
        }
    }


    void SingletonInit()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
