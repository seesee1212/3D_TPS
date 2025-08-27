using System.Collections;
using UnityEngine;

public class Monster_Movement : MONSTER
{
    public float speed = 3.0f; // 몬스터 이동 속도

    private Rigidbody rb; // Rigidbody 컴포넌트
    private Animator animator; // Animator 컴포넌트 (추가 가능성 있음)

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져옴
        animator = GetComponentInChildren<Animator>(); // Animator 컴포넌트를 가져옴 (필요시)
    }


    public override void Initalize(Transform player)
    {
        base.Initalize(player); // 부모 클래스의 Initalize 호출

        Rotate(direction(), false); // 타겟 방향으로 회전

        StartCoroutine(SpawnStartCouroutine(new Vector3(15,15,15)));
    }

    IEnumerator SpawnStartCouroutine(Vector3 scaleEnd)
    {
        Vector3 ScaleStart = Vector3.zero;
        Vector3 ScaleEnd = scaleEnd;
        float duration = 0.5f; // 생성 애니메이션 지속 시간
        float timer = 0.0f;
        while ( timer < duration)
        {
            float t = timer / duration;
            transform.localScale = Vector3.Lerp(ScaleStart, ScaleEnd, t);
            timer += Time.deltaTime; // 타이머 업데이트
            yield return null; // 다음 프레임까지 대기
        }
        isSpawned = true; // 몬스터가 생성되었음을 표시
        animator.SetTrigger("MOVE"); // 생성 애니메이션이 끝나면 이동 애니메이션 트리거
    }

    private void FixedUpdate()
    {
        if (isDead) return; // 몬스터가 죽었으면 이동하지 않음
        if (!isSpawned) return;

        MoveAndRotate(); // 타겟을 향해 이동 및 회전
        
    }

    void MoveAndRotate()
    {
        Rotate(direction()); // 타겟 방향으로 회전
        rb.MovePosition(rb.position + direction() * speed * Time.fixedDeltaTime);
    }

    Vector3 direction()
    {
        Vector3 direction = (target.position - transform.position).normalized; // 타겟 방향 벡터
        direction.y = 0; // 수평만 회적하도록 Y축 제거
        return direction; // 방향 벡터 반환
    }
    void Rotate(Vector3 direction, bool Lerp = true)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction); // 타겟 방향으로 회전
            if(Lerp)
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            else
            {
                 transform.rotation = targetRotation; // 즉시 회전  
            }
        }
    }
}
