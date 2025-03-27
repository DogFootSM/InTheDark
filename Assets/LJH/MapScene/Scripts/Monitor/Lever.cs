using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Stage
{
    startLand,
    middleLand,
    endLand,
    sellLand
}
public class Lever : MonoBehaviourPun
{
    public Stage Stage;
    PopUp popUp;

    [SerializeField] TMP_Text posName;

    private void Start()
    {
        popUp = GetComponent<PopUp>();
    }
    private void Update()
    {
        GoingMap();
    }

    public void posNameisWhat(Stage stage)
    {
        switch (stage)
        {
            case Stage.startLand:
                posName.text = "������ ��";
                break;

            case Stage.middleLand:
                posName.text = "�߰���";
                break;

            case Stage.endLand:
                posName.text = "���� ��";
                break;

            case Stage.sellLand:
                posName.text = "������ ����";
                break;
        }
    }

    void GoingMap()
    {
        if(popUp.HitMe && Input.GetKeyDown(KeyCode.E))
        {
            switch(Stage)
            {
                case Stage.startLand:
                    GameManager.Instance.SceneBGM(SceneType.INGAME);
                    SceneManager.LoadScene("MapScene1");
                    break;

                case Stage.middleLand:
                    //Todo : �������� ä������ SceneManager.LoadScene("");
                    Debug.Log("�߰� ������ ���ϴ�");
                    break;

                case Stage.endLand:
                    //Todo : �������� ä������ SceneManager.LoadScene("");
                    Debug.Log("���� ������ ���ϴ�");
                    break;

                case Stage.sellLand:
                    SceneManager.LoadScene("StoreScene");
                    Debug.Log("���� ������ ���ϴ�");
                    break;
            }
        }
    }

}
