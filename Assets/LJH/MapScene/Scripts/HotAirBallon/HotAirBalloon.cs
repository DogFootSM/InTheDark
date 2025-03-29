using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HotAirBalloon : MonoBehaviourPun
{
    Rigidbody rigid;

    [HideInInspector] public GameObject spawnPoint;
    Vector3 spawnPos;
    [HideInInspector] public GameObject destinationPoint;
    Vector3 destinationPos;

    List<Item> itemList = new List<Item>();

    GameObject basket;

    float speed = 15f;
    float boomDelayTime = 60f;

    public Coroutine boomCo;
    // ���� ��ġ -110
    // ��ž ��ġ 220

    private void Start()
    {
        spawnPos = spawnPoint.transform.position;
        destinationPos = destinationPoint.transform.position;
        
    }
    private void FixedUpdate()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, destinationPos, Time.deltaTime * speed);
        float distance = Vector3.Distance(spawnPos, destinationPos) / 2;

        if(Vector3.Distance(transform.position, destinationPos) <= distance)
        {
            DropBasket();
        }    
    }


    void DropBasket()
    {
        if (basket == null)
        {
            //���ⱸ���� �и��ϰ� ������ٵ� �߰��Ͽ� �ڿ������� �������� ��
            basket = transform.GetChild(0).gameObject;
            basket.transform.parent = null;
            basket.AddComponent<Rigidbody>();

            boomCo = StartCoroutine(BoomAirBalloon(boomDelayTime));
        }
    }

    public IEnumerator BoomAirBalloon(float boomDelayTime)
    {
        yield return new WaitForSeconds(boomDelayTime);
        basket.transform.parent = gameObject.transform;
        Destroy(gameObject);
        //Todo : ���� �ִϸ��̼� �߰��ؾ���
    }
}
