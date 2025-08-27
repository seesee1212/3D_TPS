using System.Collections;
using UnityEngine;

public class Player_Attacker : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ� ������

    private void Start()
    {
        StartCoroutine(FireCoroutine()); // �ڷ�ƾ ����
    }
    IEnumerator FireCoroutine()
    {
        yield return new WaitForSeconds(1.0f); // 0.5�� ���
        FireProjectile(); // �Ѿ� �߻�
        StartCoroutine(FireCoroutine()); // ���� �߻� ����
    }

    private void FireProjectile()
    {
        Vector3 fireDir;
        if (Player.instance.target != null)
        {
            fireDir = Player.instance.Direction(); // �÷��̾�� ���� ���� ���͸� �����ɴϴ�.
        }
        else
        {
            fireDir = transform.forward; // ���Ͱ� ������ �÷��̾��� ���� �������� �߻�
        }


        var bullet = MANAGER.POOL.pooling_OBJ("Projectile").Get((value) =>
        {
            Vector3 pos = transform.position + new Vector3(0, 1.0f, 0) + fireDir * 1.0f; // �÷��̾� ��ġ���� �ణ ���� �߻�
            value.transform.position = pos; // �÷��̾� ��ġ���� �Ѿ� �߻�
            value.GetComponent<Bullet>().Initalize(fireDir); // �Ѿ� �ʱ�ȭ
        });

    }
}
