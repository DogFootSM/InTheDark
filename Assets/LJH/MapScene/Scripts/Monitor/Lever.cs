using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Stage
{
    startLand,
    middleLand,
    endLand
}
public class Lever : MonoBehaviourPun
{
    public Stage Stage;



    private void Update()
    {
        GoingMap();
    }

    void GoingMap()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            switch(Stage)
            {
                case Stage.startLand:
                    //Todo : �������� ä������ SceneManager.LoadScene("");
                    Debug.Log("������ ������ ���ϴ�");
                    break;

                case Stage.middleLand:
                    //Todo : �������� ä������ SceneManager.LoadScene("");
                    Debug.Log("�߰� ������ ���ϴ�");
                    break;

                case Stage.endLand:
                    //Todo : �������� ä������ SceneManager.LoadScene("");
                    Debug.Log("���� ������ ���ϴ�");
                    break;
            }
        }
    }
}
