using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviourPun, IHitMe
{
    public List<Item> itemList = new List<Item>();

    HotAirBalloon balloon;

    public bool HitMe { get; set; }
    PopUp popUp;

    void Start()
    {
        popUp = GetComponent<PopUp>();
        balloon = transform.parent.GetComponent<HotAirBalloon>();
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
        for (int i = 0; i <= itemList.Count; i++)
        {
            if (i == itemList.Count)
            {
                StopCoroutine(balloon.boomCo);
                StartCoroutine(balloon.BoomAirBalloon(1f));
            }
            else
            {
                Vector3 itemPos = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                string itemName = itemList[i].name;

                PhotonNetwork.Instantiate(itemName, itemPos, Quaternion.identity);
            }
        }
    }
}
