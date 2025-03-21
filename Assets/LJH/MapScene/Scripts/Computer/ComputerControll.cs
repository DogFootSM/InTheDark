using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

enum Text
{
    menu = 1,
    land,
    store,
    check
}
public class ComputerControll : BaseUI
{
    const string land = "������ ����";
    const string store = "���� ����";
    const string land_Start = "������ ��";
    const string land_Middle = "�߰���";
    const string land_End = "���� ��";
    const string flashlight = "������";
    const string stick = "�����";
    const string exit = "������";
    const string and = "���";
    const string buy = "�Ϸ�";

    TMP_Text menuText;
    TMP_Text landText;
    TMP_Text storeText;
    TMP_Text checkText;

    List<GameObject> textObjList = new List<GameObject>();
    GameObject curPage;

    TMP_InputField inputField;

    //������ ���ſ� ����Ʈ
    List<Item> items = new List<Item>();
    float itemsPrice;

    [Header("������ ������ �־�δ� ����Ʈ")]
    [SerializeField] List<Item> itemList = new List<Item>();
    [Header("���ⱸ ���� ��ġ")]
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject destinationPoint;


    private void Awake()
    {
        Bind();
        Init();
    }


    private void OnEnable()
    {
        TextSetActive((int)Text.menu);

        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField();


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
                            inputField.ActivateInputField();
                            break;

                        case store:
                            TextSetActive((int)Text.store);
                            //���� �������� ������ ��� �ʱ�ȭ
                            items.Clear();
                            inputField.ActivateInputField();
                            break;

                        case exit:
                            gameObject.SetActive(false);
                            break;

                        default:
                            inputField.text = "";
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���_Menu");
                            inputField.ActivateInputField();
                            break;
                    }
                    break;

                case "SubMenuText_Land":
                    switch (inputText)
                    {
                        case land_Start:
                            //Todo : ������ �����Ǽ� ����
                            inputField.ActivateInputField();
                            break;

                        case land_Middle:
                            //Todo : ������ �߰��� ����
                            inputField.ActivateInputField();
                            break;

                        case land_End:
                            //Todo : ������ ���� �� ����
                            inputField.ActivateInputField();
                            break;

                        case exit:
                            TextSetActive((int)Text.menu);
                            inputField.ActivateInputField();
                            break;

                        default:
                            inputField.text = "";
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���");
                            inputField.ActivateInputField();
                            break;
                    }
                    break;

                case "SubMenuText_Store":
                    switch (inputText)
                    {
                        case flashlight:
                            //Todo : ���� ����Ʈ�� ������ �߰�
                            AddItemList(itemList[0]);
                            inputField.ActivateInputField();
                            break;

                        case stick:
                            //Todo : ���� ����Ʈ�� ����� �߰�
                            AddItemList(itemList[1]);
                            inputField.ActivateInputField();
                            break;

                        case exit:
                            items.Clear();
                            TextSetActive((int)Text.menu);
                            inputField.ActivateInputField();
                            break;

                        default:
                            inputField.text = "";
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���");
                            inputField.ActivateInputField();
                            break;
                    }
                    break;

                case "SubMenuText_Check":
                    switch (inputText)
                    {
                        case and:
                            //Todo : ���� ����Ʈ�� ������ �߰�
                            if (itemsPrice >= IngameManager.Instance.money)
                            {
                                Debug.Log("�������� �����Ͽ� '���'�� ������ �� �����ϴ�.");
                                TextSetActive((int)Text.check);
                            }
                            else
                            {
                                TextSetActive((int)Text.store);
                            }
                            //���Ŀ� �߰� ����? ���� �Ϸ�? ȭ�� �־����
                            inputField.ActivateInputField();
                            break;

                        case buy:
                            //���� �Ϸ�
                            CallAirBalloon();
                            gameObject.SetActive(false);
                            break;

                        default:
                            inputField.text = "";
                            //Todo �÷��̽�Ȧ�� ���� �ٲ���ҵ�?
                            Debug.Log("�ٽ� �Է����ּ���");
                            inputField.ActivateInputField();
                            break;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="item"></param>
    void AddItemList(Item item)
    {
       ////���� ���Ŀ� �ּ������Ͽ� �׽�Ʈ
       if(itemPriceCheck(item))
       {
           items.Add(item);
           TextSetActive((int)Text.check);
        }
       else
       {
            TextSetActive((int)Text.store);
            //Todo :������ �ؽ�Ʈ ����
            Debug.Log("�������� �����մϴ�.");
       }
        inputField.text = "";
    }

    bool itemPriceCheck(Item item)
    {
        InteractableItemData itemData;
        Debug.Log(item.name);

        switch (item.name)
        {
            case "FlashLight":
                itemData = ItemManager.Instance.GetItemData(4); // ������ Id
                break;

            case "FishingRod":
                itemData = ItemManager.Instance.GetItemData(2); // ���˴� Id
                break;

            default:
                itemData = null;
                return false;
        }

        Debug.Log($"������ ������ {itemData.name}");
        float itemPrice = itemData.ItemBuyPrice;
        Debug.Log($"{item.name}�� ���� {itemPrice}");

        if(itemsPrice + itemPrice > IngameManager.Instance.money)
        { 
            return false;
        }
        
        itemsPrice += itemPrice;
        Debug.Log($"���� �������� ���� {itemsPrice}1");
        return true;
    }

    /// <summary>
    /// ��ǻ�Ϳ��� ���� ���� �Ϸ�� �ش� �Լ� ȣ���Ͽ� ���ⱸ ����
    /// </summary>
    public void CallAirBalloon()
    {
        Debug.Log($"���� �������� ���� {itemsPrice}2");
        IngameManager.Instance.money -= itemsPrice;
        Debug.Log($"������ �� {IngameManager.Instance.money}");

        GameObject airBallonPrefab = PhotonNetwork.Instantiate("HotAirBalloon", spawnPoint.transform.position, Quaternion.identity);
        airBallonPrefab.GetComponent<HotAirBalloon>().spawnPoint = spawnPoint;
        airBallonPrefab.GetComponent<HotAirBalloon>().destinationPoint = destinationPoint;
        airBallonPrefab.transform.GetChild(0).GetComponent<Basket>().itemList = items;
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
        checkText = GetUI<TMP_Text>("SubMenuText_Check");

        textObjList.Add(menuText.gameObject);
        textObjList.Add(landText.gameObject);
        textObjList.Add(storeText.gameObject);
        textObjList.Add(checkText.gameObject);

        curPage = textObjList[0];

        inputField = GetUI<TMP_InputField>("InputField (TMP)");
    }
}
