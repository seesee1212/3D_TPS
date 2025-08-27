using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Instantiate() '������' -> ���������� �޸� �Ҵ�, ������Ʈ �ʱ�ȭ, �� Ʈ�� ���� �� ��� ŭ
// Destroy() '�ı���' -> Unity�� �����δ� ��� �ı����� �ʰ�, GC(������ �÷���)���� ó����

// '�������̽�'��? - "�̷��� ���� �Լ��� ������ �� �־�� ��!" ��� ������ִ� Ʋ



public class Object_Pool : IPool
{
    public Transform parentTransform { get; set; }

    // Queue -> FIFO (First In First Out) -> ���Լ���
    // DeQueue (���� ���� ������Ʈ�� ��������)
    // EnQueue (������Ʈ�� Queue���ο� ���� �ִ´�)
    // Stack -> LIFO (Last In First Out) -> ���Լ���
    public Queue<GameObject> pool { get; set; } = new Queue<GameObject>();

    public GameObject Get(Action<GameObject> action = null)
    {
        GameObject obj = pool.Dequeue();
        obj.SetActive(true); // ������Ʈ�� Ȱ��ȭ

        if(action != null)
        {
            action?.Invoke(obj); // �׼��� �ִٸ� ����
        }
        return obj; // Ȱ��ȭ�� ������Ʈ�� ��ȯ
    }

    public void Return(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);
        obj.transform.parent = parentTransform;
        obj.SetActive(false); // ������Ʈ�� ��Ȱ��ȭ

        if (action != null)
        {
            action?.Invoke(obj); // �׼��� �ִٸ� ����
        }
    }
}
public class Pool_Mng : MonoBehaviour
{
    public Dictionary<string, IPool> m_pool_Dictionary = new Dictionary<string, IPool>(); // Ǯ�� ������ ��ųʸ�

    Transform base_Obj = null;

    public void Start()
    {
        base_Obj = this.transform; // Ǯ�� �θ� Ʈ�������� ���� ��ũ��Ʈ�� �پ��ִ� ���� ������Ʈ�� ����
    }

    public IPool pooling_OBJ(string path)
    {
        if (m_pool_Dictionary.ContainsKey(path) == false)
        {
            Add_Pool(path); // ��ųʸ��� Ǯ�� �߰�
        }
        if (m_pool_Dictionary[path].pool.Count <= 0)
        {
            Add_Queue(path); // Ǯ�� ������Ʈ�� �߰�
        }

        return m_pool_Dictionary[path]; // �ش� ����� Ǯ�� ��ȯ
    }

    private GameObject Add_Pool(string path)
    {
        GameObject obj = new GameObject(path + "##POOL");
        obj.transform.SetParent(base_Obj); // Ǯ�� �θ� Ʈ�������� ����
        Object_Pool T_Pool = new Object_Pool(); // Object_Pool �ν��Ͻ� ����

        m_pool_Dictionary.Add(path, T_Pool); // ��ųʸ��� Ǯ�� �߰�
        T_Pool.parentTransform = obj.transform; // Ǯ�� �θ� Ʈ������ ����
        return obj;
    }

    private void Add_Queue(string path)
    {
        var obj = Instantiate(Resources.Load<GameObject>("POOL/" + path)); // ���ҽ����� ���� ������Ʈ�� �ε�

        m_pool_Dictionary[path].Return(obj); // ���� ������Ʈ�� Ǯ�� ��ȯ
    }
}
