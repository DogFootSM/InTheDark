using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : BaseUI
{
    GameObject pausePannel;
    GameObject optionPannel;

    GameObject exitPopUp;

    Button continueButton;
    Button optionButton;
    Button exitButton;

    Button yesButton;
    Button noButton;

    TMP_Text exitText;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if(Time.timeScale > 0)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;


            pausePannel.SetActive(!pausePannel.activeSelf);
        }
    }

    /// <summary>
    /// �Ͻ����� �ݱ�
    /// </summary>
    void ContinueButton()
    {
        //�ӽ�
        Time.timeScale = 1f;
        pausePannel.SetActive(false);
    }

    void OptionButton()
    {
        optionPannel.SetActive(true);
    }

    /// <summary>
    /// �������� �˾� ȣ��
    /// </summary>
    void ExitPopUp()
    {
        if (SceneManager.GetActiveScene().name == "WaitingScene")
        {
            exitText.text = "���� ȭ������ ���ư��ðڽ��ϱ�?";
        }
        else
        {
            exitText.text = "�ٴٷ� ���ư��ðڽ��ϱ�?";
        }


            exitPopUp.SetActive(true);
    }
    /// <summary>
    /// ���Ӿ��ϰ�� > �������� �̵�
    /// </summary>
    void ReturnGame()
    {
        if (SceneManager.GetActiveScene().name == "WaitingScene")
            return;

        Time.timeScale = 1f;
        SceneManager.LoadScene("WaitingScene");
    }

    /// <summary>
    /// �����ϰ�� > ��������
    /// </summary>
    void ExitGame()
    {
        if (SceneManager.GetActiveScene().name != "WaitingScene")
            return;

        Database.instance.SaveData(FirebaseManager.Auth.CurrentUser.UserId, "Slot_1", IngameManager.Instance.money, IngameManager.Instance.days);
        
        PhotonNetwork.LeaveRoom();
    }

    void ExitNo()
    {
        exitPopUp.SetActive(false);
    }


    void Init()
    {
        pausePannel = GetUI("PausePannel");
        optionPannel = GetUI("SettingCanvas");

        exitPopUp = GetUI("ExitPopUp");
        exitText = GetUI<TMP_Text>("ExitText");


        continueButton = GetUI<Button>("ContinueButton");
        optionButton = GetUI<Button>("OptionButton");
        exitButton = GetUI<Button>("ExitButton");

        yesButton = GetUI<Button>("YesButton");
        noButton = GetUI<Button>("NoButton");

        continueButton.onClick.AddListener(() => ContinueButton());
        optionButton.onClick.AddListener(() => OptionButton());
        exitButton.onClick.AddListener(() => ExitPopUp());

        noButton.onClick.AddListener(() => ExitNo());
        yesButton.onClick.AddListener
        (() =>
            {
                ReturnGame();
                ExitGame();
            }
        );
    }



}
