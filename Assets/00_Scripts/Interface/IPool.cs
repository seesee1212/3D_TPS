using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPool
{
    Transform parentTransform { get; set; } // 풀의 부모 트랜스폼
    Queue<GameObject> pool { get; set; } // 풀에 저장된 게임 오브젝트들
    GameObject Get(Action<GameObject> action = null); // 풀에서 게임 오브젝트를 가져오는 메서드

    void Return(GameObject obj, Action<GameObject> action = null); // 게임 오브젝트를 풀에 반환하는 메서드
}