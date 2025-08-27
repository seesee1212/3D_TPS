using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRadius = 10.0f;
    public GameObject monsterPrefab;
    public Transform player;
    public float spawnInterval = 3.0f;

    public float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f; // 타이머 초기화
            SpawnMonsterAtEdge(); // 몬스터를 원의 가장자리에 소환
        }
    }

    void SpawnMonsterAtEdge()
    {
        Vector3 spawnPos = GetRandomPointOnCircleEdge(player.position, spawnRadius);

        var monster = MANAGER.POOL.pooling_OBJ("Monster").Get((value) =>
        {
            value.transform.position = spawnPos;
            value.GetComponent<MONSTER>().Initalize(player);
        });

    }

    Vector3 GetRandomPointOnCircleEdge(Vector3 center, float radius)
    {
        float angle = Random.Range(0.0f, Mathf.PI * 2f);
        // 원 둘레를 360도로 생각했을 때, 무작위 각도 (0~2 라디안)를 골라

        // 어떤 중심점을 기준으로 반지름 거리만큼 떨어진 원의 가장자리에 랜덤하게 몬스터를 소환
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return new Vector3(center.x + x, center.y, center.z + z);
    }
}
