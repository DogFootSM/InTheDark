using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void Update()
    {
        if (PhotonNetwork.InRoom && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(GameObject.Find(nameof(Camera)).gameObject);

            PhotonNetwork.LeaveRoom();
            //�ΰ��� �Ŵ��� ����
            Destroy(transform.parent.parent.gameObject);

            //���� ������ ������ �ʱ�ȭ
            Database.instance.ResetData(FirebaseManager.Auth.CurrentUser.UserId, "Slot_1");

        }
    }
}
