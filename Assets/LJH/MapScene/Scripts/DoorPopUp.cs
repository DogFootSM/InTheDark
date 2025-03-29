using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorPopUp : MonoBehaviour
{
    public GameObject player;
    public Collider playerCol;

    [SerializeField] GameObject keyInfo;
    DoorController controller;

    public bool hitMe;
    private void Start()
    {
        controller = GetComponent<DoorController>();
    }

    private void Update()
    {
        if (controller.doorOpened)
            keyInfo.transform.GetChild(0).GetComponent<TMP_Text>().text = "���ݱ�";
        else if (!controller.doorOpened)
            keyInfo.transform.GetChild(0).GetComponent<TMP_Text>().text = "������";

        KeyInfoOnOff(hitMe);


    }

    void KeyInfoOnOff(bool tf)
    {
        keyInfo.SetActive(tf);
    }

}
