using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// Instantiate() '생성자' -> 내부적으로 메모리 할당, 컴포넌트 초기화, 씬 트리 갱신 등 비용 큼
// Destroy() '파괴자' -> Unity는 실제로는 즉시 파괴하지 않고, GC(가비지 컬렉션)에서 처리함

// '언터페이스'란? - "이렇게 생긴 함수랑 변수는 꼭 있어야 해!" 라고 약속해주는 틀



public class Object_Pool : IPool
{
    public Transform parentTransform { get; set; }

    // Queue -> FIFO (First In First Out) -> 선입선출
    // DeQueue (먼저 들어온 오브젝트를 내보낸다)
    // EnQueue (오브젝트를 Queue내부에 집어 넣는다)
    // Stack -> LIFO (Last In First Out) -> 후입선출
    public Queue<GameObject> pool { get; set; } = new Queue<GameObject>();

    public GameObject Get(Action<GameObject> action = null)
    {
        GameObject obj = pool.Dequeue();
        obj.SetActive(true); // 오브젝트를 활성화

        if(action != null)
        {
            action?.Invoke(obj); // 액션이 있다면 실행
        }
        return obj; // 활성화된 오브젝트를 반환
    }

    public void Return(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);
        obj.transform.parent = parentTransform;
        obj.SetActive(false); // 오브젝트를 비활성화

        if (action != null)
        {
            action?.Invoke(obj); // 액션이 있다면 실행
        }
    }
}
public class Pool_Mng : MonoBehaviour
{
    public Dictionary<string, IPool> m_pool_Dictionary = new Dictionary<string, IPool>(); // 풀을 저장할 딕셔너리

    Transform base_Obj = null;

    public void Start()
    {
        base_Obj = this.transform; // 풀의 부모 트랜스폼을 현재 스크립트가 붙어있는 게임 오브젝트로 설정
    }

    public IPool pooling_OBJ(string path)
    {
        if (m_pool_Dictionary.ContainsKey(path) == false)
        {
            Add_Pool(path); // 딕셔너리에 풀을 추가
        }
        if (m_pool_Dictionary[path].pool.Count <= 0)
        {
            Add_Queue(path); // 풀에 오브젝트를 추가
        }

        return m_pool_Dictionary[path]; // 해당 경로의 풀을 반환
    }

    private GameObject Add_Pool(string path)
    {
        GameObject obj = new GameObject(path + "##POOL");
        obj.transform.SetParent(base_Obj); // 풀의 부모 트랜스폼을 설정
        Object_Pool T_Pool = new Object_Pool(); // Object_Pool 인스턴스 생성

        m_pool_Dictionary.Add(path, T_Pool); // 딕셔너리에 풀을 추가
        T_Pool.parentTransform = obj.transform; // 풀의 부모 트랜스폼 설정
        return obj;
    }

    private void Add_Queue(string path)
    {
        var obj = Instantiate(Resources.Load<GameObject>("POOL/" + path)); // 리소스에서 게임 오브젝트를 로드

        m_pool_Dictionary[path].Return(obj); // 게임 오브젝트를 풀에 반환
    }
}
