using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAirBalloon : MonoBehaviourPun
{
    GameObject airBallonPrefab;


    /// <summary>
    /// ��ǻ�Ϳ��� ���� ���� �Ϸ�� �ش� �Լ� ȣ���Ͽ� ���ⱸ ����
    /// </summary>
    public void CallAirBalloon()
    {
        airBallonPrefab = PhotonNetwork.Instantiate("HotAirBalloon", transform.position, Quaternion.identity);
    }

}
