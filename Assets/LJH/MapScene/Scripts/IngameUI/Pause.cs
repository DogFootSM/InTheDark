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
        exitPopUp.SetActive(true);
    }
    /// <summary>
    /// ���Ӿ��ϰ�� > �������� �̵�
    /// </summary>
    void ReturnGame()
    {
        if (SceneManager.GetActiveScene().name == "WaitingScene")
            return;

        SceneManager.LoadScene("WaitingScene");
    }

    /// <summary>
    /// �����ϰ�� > ��������
    /// </summary>
    void ExitGame()
    {
        if (SceneManager.GetActiveScene().name != "WaitingScene")
            return;

        Debug.Log(IngameManager.Instance.money);
        Database.instance.SaveData(FirebaseManager.Auth.CurrentUser.UserId, "Slot_1", IngameManager.Instance.money, IngameManager.Instance.days);

#if UNITY_EDITOR
        //Comment : ����Ƽ �����ͻ󿡼� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : ���� �󿡼� ����
        Application.Quit();
#endif
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
