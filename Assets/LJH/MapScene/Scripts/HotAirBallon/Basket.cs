using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviourPun, IHitMe
{
    public List<Item> itemList = new List<Item>();

    public bool HitMe { get; set; }
    PopUp popUp;

    void Start()
    {
        popUp = GetComponent<PopUp>();
    }

    private void Update()
    {

        if(popUp.HitMe && Input.GetKeyDown(KeyCode.E))
        {
            ShowItem(itemList);
        }
    }

    /// <summary>
    /// ��ǻ�Ϳ��� �ش� �Լ� ȣ���Ͽ� ������ ����Ʈ�� ������ ���� �����
    /// </summary>
    /// <param name="itemName"></param>
    public void BuyItem(List<Item> itemList, Item itemName)
    {
        itemList.Add(itemName);
        this.itemList = itemList;
        //Todo : ���� ��� �� �Ҹ�
        //GameManager.Instance.money -= itemName.price;
    }

    /// <summary>
    /// ����� �������� �ѷ��ִ� �Լ�
    /// </summary>
    /// <param name="itemList"></param>
    void ShowItem(List<Item> itemList)
    {
        //������ �������� ����Ʈ�� �־��ְ� �� �����۵��� �ٽ����� ������ ��, �ֺ��� �ѷ�����

    }
}
