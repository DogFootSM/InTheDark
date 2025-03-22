using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    public static IngameManager Instance;

    public float time {get; set;}
    public float minute { get; set; }

    public int days { get; set; }

    float randomMinute;

    float elapsedTime = 1f;

    public int money { get; set; }

    private void Awake()
    {
        Init();
        SingletonInit();
    }


    //�ð��� ��� 10�ʸ��� 5 ~10���� �帣��
    //am8�÷� �����ؼ�
    //pm8�õǸ� �� ħ��

    /// <summary>
    /// Ÿ�̸� ���� - �ʾ� ���۵� �� ȣ��
    /// </summary>
    public void TimerReset()
    {
        Debug.Log("�ð� �ʱ�ȭ�Ǿ���");
        time = 8;
        minute = 0;
    }

    /// <summary>
    /// �ð� ���� - �ʾ� ���۵� �� ȣ��
    /// </summary>
    /// <returns></returns>
    public IEnumerator TimerCount()
    {
        Debug.Log("Ÿ�̸� ī��Ʈ ����");
        while (true) 
        {
            Debug.Log("�ð� �帧");
            randomMinute = Random.Range(5, 10);
            minute += randomMinute;

            if (minute >= 60)
            {
                time++;
                minute = minute - 60;
            }

            if(time >= 20)
            {
                TimeOver();
            }
            yield return new WaitForSeconds(elapsedTime);
        }
    }

    void TimeOver()
    {
        //Todo: ����߽�Ŵ
    }    

    void Init()
    {
        Database.instance.LoadData(FirebaseManager.Auth.CurrentUser.UserId, "Slot_1");
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
