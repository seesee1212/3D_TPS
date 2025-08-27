using System.Collections;
using UnityEngine;

public class Player_Attacker : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹

    private void Start()
    {
        StartCoroutine(FireCoroutine()); // 코루틴 시작
    }
    IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // 0.5초 대기
        FireProjectile(); // 총알 발사
        StartCoroutine(FireCoroutine()); // 다음 발사 예약
    }

    private void FireProjectile()
    {
        Vector3 fireDir;
        if (Player.instance.target != null)
        {
            fireDir = Player.instance.Direction(); // 플레이어에서 몬스터 방향 벡터를 가져옵니다.
        }
        else
        {
            fireDir = transform.forward; // 몬스터가 없으면 플레이어의 현재 방향으로 발사
        }


        var bullet = MANAGER.POOL.pooling_OBJ("Projectile").Get((value) =>
        {
            Vector3 pos = transform.position + new Vector3(0, 1.0f, 0) + fireDir * 1.0f; // 플레이어 위치에서 약간 위로 발사
            value.transform.position = pos; // 플레이어 위치에서 총알 발사
            value.GetComponent<Bullet>().Initalize(fireDir); // 총알 초기화
        });

    }
}
