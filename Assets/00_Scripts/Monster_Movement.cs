using System.Collections;
using UnityEngine;

public class Monster_Movement : MONSTER
{
    public float speed = 3.0f; // ���� �̵� �ӵ�

    private Rigidbody rb; // Rigidbody ������Ʈ
    private Animator animator; // Animator ������Ʈ (�߰� ���ɼ� ����)

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� ������
        animator = GetComponentInChildren<Animator>(); // Animator ������Ʈ�� ������ (�ʿ��)
    }


    public override void Initalize(Transform player)
    {
        base.Initalize(player); // �θ� Ŭ������ Initalize ȣ��

        Rotate(direction(), false); // Ÿ�� �������� ȸ��

        StartCoroutine(SpawnStartCouroutine(new Vector3(15,15,15)));
    }

    IEnumerator SpawnStartCouroutine(Vector3 scaleEnd)
    {
        Vector3 ScaleStart = Vector3.zero;
        Vector3 ScaleEnd = scaleEnd;
        float duration = 0.5f; // ���� �ִϸ��̼� ���� �ð�
        float timer = 0.0f;
        while ( timer < duration)
        {
            float t = timer / duration;
            transform.localScale = Vector3.Lerp(ScaleStart, ScaleEnd, t);
            timer += Time.deltaTime; // Ÿ�̸� ������Ʈ
            yield return null; // ���� �����ӱ��� ���
        }
        isSpawned = true; // ���Ͱ� �����Ǿ����� ǥ��
        animator.SetTrigger("MOVE"); // ���� �ִϸ��̼��� ������ �̵� �ִϸ��̼� Ʈ����
    }

    private void FixedUpdate()
    {
        if (isDead) return; // ���Ͱ� �׾����� �̵����� ����
        if (!isSpawned) return;

        MoveAndRotate(); // Ÿ���� ���� �̵� �� ȸ��
        
    }

    void MoveAndRotate()
    {
        Rotate(direction()); // Ÿ�� �������� ȸ��
        rb.MovePosition(rb.position + direction() * speed * Time.fixedDeltaTime);
    }

    Vector3 direction()
    {
        Vector3 direction = (target.position - transform.position).normalized; // Ÿ�� ���� ����
        direction.y = 0; // ���� ȸ���ϵ��� Y�� ����
        return direction; // ���� ���� ��ȯ
    }
    void Rotate(Vector3 direction, bool Lerp = true)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Ÿ�� �������� ȸ��
            if(Lerp)
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            else
            {
                 transform.rotation = targetRotation; // ��� ȸ��  
            }
        }
    }
}
