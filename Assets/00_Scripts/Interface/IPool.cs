using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPool
{
    Transform parentTransform { get; set; } // Ǯ�� �θ� Ʈ������
    Queue<GameObject> pool { get; set; } // Ǯ�� ����� ���� ������Ʈ��
    GameObject Get(Action<GameObject> action = null); // Ǯ���� ���� ������Ʈ�� �������� �޼���

    void Return(GameObject obj, Action<GameObject> action = null); // ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
}