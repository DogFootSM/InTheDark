using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInit : BaseUI
{
    //  �׽�Ʈ�� ��ũ��Ʈ��
    //
    //
    //
    //
    //

    [Header("�ؽ�Ʈ")]
    public TMP_Text dayText;
    public TMP_Text moneyText;

    [Header("��ư")]
    public Button exitButton;
    public Button PlusDayButton;
    public Button PlusMoneyButton;

    void Awake()
    {
        Bind();
        Init();
    }

    private void Start()
    {
        //�׽�Ʈ �ڵ�
        //Database.instance.dayText = dayText;
        //Database.instance.moneyText = moneyText;
    }

    void ExitButton()
    {
#if UNITY_EDITOR
        //Comment : ����Ƽ �����ͻ󿡼� ����
        Database.instance.SaveData(FirebaseManager.Auth.CurrentUser.UserId, "Slot_1", int.Parse(moneyText.text), int.Parse(dayText.text));
        UnityEditor.EditorApplication.isPlaying = false;
#else
        //Comment : ���� �󿡼� ����
        Application.Quit();
#endif
    }

    void PlusDay()
    {
            //Database.instance.days += 1;
            //dayText.text = Database.instance.days.ToString();
    }
    void PlusMoney()
    {
            //Database.instance.money += 100;
            //moneyText.text = Database.instance.money.ToString();
    }
    void Init()
    {
        dayText = GetUI<TMP_Text>("Day");
        moneyText = GetUI<TMP_Text>("Money");

        exitButton = GetUI<Button>("ExitButton");
        exitButton.onClick.AddListener(ExitButton);

        PlusDayButton = GetUI<Button>("PlusDay");
        PlusMoneyButton = GetUI<Button>("PlusMoney");

        PlusDayButton.onClick.AddListener(PlusDay);
        PlusMoneyButton.onClick.AddListener(PlusMoney);
    }
}
