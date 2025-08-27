using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f; // �Ѿ� �ӵ�
    public float lifetime = 5.0f; // �Ѿ� ���� �ð�
    public GameObject ExplosionParticle;
    public GameObject DamageObject; // ������ ������Ʈ (�ʿ�� ���)
    private Vector3 direction; // Ÿ��(����) Ʈ������


    public void Initalize(Vector3 dir)
    {
        direction = dir; // Ÿ���� ����
        StartCoroutine(DestroyCoroutine(5));
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime; // �Ѿ� �̵�
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
            Instantiate(ExplosionParticle, transform.position, Quaternion.identity); // �浹 �� ���� ����Ʈ ����

            other.gameObject.GetComponent<MONSTER>().GetDamage(MANAGER.SESSION.Damage);

            StopAllCoroutines(); // ������ �ڷ�ƾ ����
            MANAGER.POOL.m_pool_Dictionary["Projectile"].Return(this.gameObject);
        }
    }
}
