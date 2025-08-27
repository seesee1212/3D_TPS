using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f; // 총알 속도
    public float lifetime = 5.0f; // 총알 생존 시간
    public GameObject ExplosionParticle;
    public GameObject DamageObject; // 데미지 오브젝트 (필요시 사용)
    private Vector3 direction; // 타겟(몬스터) 트랜스폼


    public void Initalize(Vector3 dir)
    {
        direction = dir; // 타겟을 설정
        StartCoroutine(DestroyCoroutine(5));
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // 총알 이동
    }
    
    IEnumerator DestroyCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        MANAGER.POOL.m_pool_Dictionary["Projectile"].Return(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity); // 충돌 시 폭발 이펙트 생성

            other.gameObject.GetComponent<MONSTER>().GetDamage(MANAGER.SESSION.Damage);

            StopAllCoroutines(); // 기존의 코루틴 중지
            MANAGER.POOL.m_pool_Dictionary["Projectile"].Return(this.gameObject);
        }
    }
}
