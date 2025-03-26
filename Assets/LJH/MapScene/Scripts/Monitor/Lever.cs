using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        popUp = GetComponent<PopUp>();
    }
    private void Update()
    {
        GoingMap();
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
