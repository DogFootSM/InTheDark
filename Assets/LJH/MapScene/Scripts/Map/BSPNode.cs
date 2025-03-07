using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPNode
{
    public Bounds Area; //��尡 �����ϴ� ����
    public BSPNode Left, Right; //������ ���� ����
    public Bounds Room; //���� ���� ũ��

    public BSPNode(Bounds area)
    {
        Area = area;
    }
}
