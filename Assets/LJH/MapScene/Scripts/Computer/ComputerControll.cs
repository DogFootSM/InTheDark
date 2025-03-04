using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

enum Text
{
    menu = 1,
    land,
    store
}
public class ComputerControll : BaseUI
{
    const string land = "������ ����";
    const string store = "���� ����";
    const string land_Start = "������ ��";
    const string land_Middle = "�߰���";
    const string land_End = "���� ��";
    const string light = "������";
    const string stick = "�����";
    const string exit = "������";

    TMP_Text menuText;
    TMP_Text landText;
    TMP_Text storeText;

    List<GameObject> textObjList = new List<GameObject>();
    GameObject curPage;

    TMP_InputField inputField;

    private void Awake()
    {
        Bind();
        Init();
    }

    private void OnEnable()
    {
        TextSetActive((int)Text.menu);

        inputField.gameObject.SetActive(true);
    }

    private void Update()
    {
        TextInput(inputField.text);
    }

    /// <summary>
    /// ���� �Է�
    /// </summary>
    /// <param name="inputText"></param>
    void TextInput(string inputText)
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (curPage.name)
            {
                //�޴� �������� ��
                case "MenuText":
                    switch (inputText)
                    {
                        case land:
                            TextSetActive((int)Text.land);
                            break;

                        case store:
                            TextSetActive((int)Text.store);
                            break;

                        case exit:
                            gameObject.SetActive(false);
                            break;

                        default:
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���_Menu");
                            break;
                    }
                    break;

                case "SubMenuText_Land":
                    switch (inputText)
                    {
                        case land_Start:
                            //Todo : ������ �����Ǽ� ����
                            Debug.Log("������ �� ���� ����");
                            break;

                        case land_Middle:
                            //Todo : ������ �߰��� ����
                            Debug.Log("�߰������� ����");
                            break;

                        case land_End:
                            //Todo : ������ ���� �� ����
                            Debug.Log("���� ������ ����");
                            break;

                        case exit:
                            TextSetActive((int)Text.menu);
                            break;

                        default:
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���");
                            break;
                    }
                    break;

                case "SubMenuText_Store":
                    switch (inputText)
                    {
                        case light:
                            //Todo : ���� ����Ʈ�� ������ �߰�
                            Debug.Log("������ �߰�");
                            break;

                        case stick:
                            //Todo : ���� ����Ʈ�� ����� �߰�
                            Debug.Log("����� �߰�");
                            break;

                        case exit:
                            TextSetActive((int)Text.menu);
                            break;

                        default:
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���");
                            break;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// �ؽ�Ʈ Ȱ��ȭ/��Ȱ��ȭ ����
    /// </summary>
    /// <param name="index">1 = menu, 2 = land, 3 = store</param>
    void TextSetActive(int index)
    {
        foreach (GameObject go in textObjList)
        {
            go.SetActive(false);
        }
        textObjList[index - 1].gameObject.SetActive(true);
        curPage = textObjList[index - 1];
        inputField.text = "";
    }

    void Init()
    {
        menuText = GetUI<TMP_Text>("MenuText");
        landText = GetUI<TMP_Text>("SubMenuText_Land");
        storeText = GetUI<TMP_Text>("SubMenuText_Store");

        textObjList.Add(menuText.gameObject);
        textObjList.Add(landText.gameObject);
        textObjList.Add(storeText.gameObject);

        curPage = textObjList[0];

        inputField = GetUI<TMP_InputField>("InputField (TMP)");
    }
}
