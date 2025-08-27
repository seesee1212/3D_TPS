using UnityEngine;

[RequireComponent(typeof(CharacterController))] //물리적용 방식 rigidbody 대신 CharacterController 사용
public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public Vector3 cameraDir = Vector3.zero;

    private Camera m_Camera; // 카메라 컴포넌트
    private Vector3 moveDir;
    private CharacterController controller; // CharacterController 컴포넌트
    private Animator animator; // Animator 컴포넌트

    private void Start()
    {
        m_Camera = Camera.main; // 메인 카메라를 가져옵니다.
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move(); // 이동 함수 호출
        Rotate(); // 회전 함수 호출
        Animate(); // 애니메이션 함수 호출
        CameraMove(); // 카메라 이동 함수 호출
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); // 좌우 입력
        float v = Input.GetAxisRaw("Vertical"); // 상하 입력
                                                // 
        moveDir = new Vector3(h, 0, v).normalized; // 입력 방향 벡터 생성
        // SimpleMove() 는 Time.deltaTime 을 자동으로 적용하여 이동 속도를 조정합니다.
        controller.SimpleMove(moveDir * moveSpeed); // 이동
    }

    void Rotate()
    {
        if (Player.instance.target != null)
        {
            Vector3 dirToMonster = Player.instance.Direction(); // 플레이어에서 몬스터 방향 벡터를 가져옵니다.
            dirToMonster.y = 0f; // y축 방향은 무시합니다.

            RotateToQuaternion(dirToMonster); // 이동 방향으로 회전
        }

        //magnitide(상대적으로 느림), sqrMagnitude(빠름)
        else if (moveDir.sqrMagnitude > 0.01f)
        {
            RotateToQuaternion(moveDir); // 이동 방향으로 회전
        }
    }

    void RotateToQuaternion(Vector3 direction)
    {
        Quaternion targetRot = Quaternion.LookRotation(direction); // 몬스터 방향으로 회전
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime); // 부드러운 회전
    }

    void Animate()
    {
        animator.SetFloat("SPEED", moveDir.magnitude); // 애니메이션 속도 설정   
    }

    void CameraMove()
    {
        m_Camera.transform.position = Vector3.Lerp(
            m_Camera.transform.position,
            transform.position + cameraDir,
            2.0f * Time.deltaTime);
    }
}
