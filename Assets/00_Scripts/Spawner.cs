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
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
            SpawnMonsterAtEdge(); // ���͸� ���� �����ڸ��� ��ȯ
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
        // �� �ѷ��� 360���� �������� ��, ������ ���� (0~2 ����)�� ���

        // � �߽����� �������� ������ �Ÿ���ŭ ������ ���� �����ڸ��� �����ϰ� ���͸� ��ȯ
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        return new Vector3(center.x + x, center.y, center.z + z);
    }
}
