using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [SerializeField] TMP_Text gasText;

    float gas = 0f;
    Coroutine gasCo;

    Door doorScriptL;
    Door doorScriptR;
    public Coroutine DoorCoL;
    public Coroutine DoorCoR;

    public bool doorOpend = true;
    private void Start()
    {
        doorScriptL = leftDoor.GetComponent<Door>();
        doorScriptR = rightDoor.GetComponent<Door>();

    }

    private void Update()
    {
        DoorOpen();
        GasCheck();
    }


    /// <summary>
    /// �� ����Ǵ� �Լ�
    /// </summary>
    void DoorOpen()
    {
        //���� 0�Ǹ� ������ �� �����
        if (gas <= 0)
        {
            DoorCoL = StartCoroutine(doorScriptL.DoorOpenCoroutine());
            DoorCoR = StartCoroutine(doorScriptR.DoorOpenCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (doorOpend)
            {
                doorOpend = false;
                DoorCoL = StartCoroutine(doorScriptL.DoorOpenCoroutine());
                DoorCoR = StartCoroutine(doorScriptR.DoorOpenCoroutine());
            }
        }
    }

    void GasCheck()
    {
        if (gasCo == null && gas < 100f)
        {
            gasCo = StartCoroutine(DoorGasCoroutine());
        }

        else if(gasCo != null && gas >= 100f)
        {
            StopCoroutine(gasCo);
            gasCo = null;
        }
    }

    /// <summary>
    /// �� �������� ��, ���� �����Ǵ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DoorGasCoroutine()
    {
        while (gas < 100f)
        {
            gas += 10f * Time.deltaTime;

            if (gas >= 100f)
            {
                gas = 100f;
            }
            gasText.text = $"{gas.ToString("F2")}%";

            yield return null;
        }
    }

    /// <summary>
    /// �� �������� �� ���� ���ҵǴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    IEnumerator DoorGasDeCoroutine()
    {
        while (gas > 0)
        {
            gas -= 10f * Time.deltaTime;

            if (gas <= 0)
            {
                gas = 0f;
            }
            gasText.text = $"{gas.ToString("F2")}%";

            yield return null;
        }
    }
}
