using UnityEngine;

[RequireComponent(typeof(CharacterController))] //�������� ��� rigidbody ��� CharacterController ���
public class Player_Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public Vector3 cameraDir = Vector3.zero;

    private Camera m_Camera; // ī�޶� ������Ʈ
    private Vector3 moveDir;
    private CharacterController controller; // CharacterController ������Ʈ
    private Animator animator; // Animator ������Ʈ

    private void Start()
    {
        m_Camera = Camera.main; // ���� ī�޶� �����ɴϴ�.
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move(); // �̵� �Լ� ȣ��
        Rotate(); // ȸ�� �Լ� ȣ��
        Animate(); // �ִϸ��̼� �Լ� ȣ��
        CameraMove(); // ī�޶� �̵� �Լ� ȣ��
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); // �¿� �Է�
        float v = Input.GetAxisRaw("Vertical"); // ���� �Է�
                                                // 
        moveDir = new Vector3(h, 0, v).normalized; // �Է� ���� ���� ����
        // SimpleMove() �� Time.deltaTime �� �ڵ����� �����Ͽ� �̵� �ӵ��� �����մϴ�.
        controller.SimpleMove(moveDir * moveSpeed); // �̵�
    }

    void Rotate()
    {
        if (Player.instance.target != null)
        {
            Vector3 dirToMonster = Player.instance.Direction(); // �÷��̾�� ���� ���� ���͸� �����ɴϴ�.
            dirToMonster.y = 0f; // y�� ������ �����մϴ�.

            RotateToQuaternion(dirToMonster); // �̵� �������� ȸ��
        }

        //magnitide(��������� ����), sqrMagnitude(����)
        else if (moveDir.sqrMagnitude > 0.01f)
        {
            RotateToQuaternion(moveDir); // �̵� �������� ȸ��
        }
    }

    void RotateToQuaternion(Vector3 direction)
    {
        Quaternion targetRot = Quaternion.LookRotation(direction); // ���� �������� ȸ��
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime); // �ε巯�� ȸ��
    }

    void Animate()
    {
        animator.SetFloat("SPEED", moveDir.magnitude); // �ִϸ��̼� �ӵ� ����   
    }

    void CameraMove()
    {
        m_Camera.transform.position = Vector3.Lerp(
            m_Camera.transform.position,
            transform.position + cameraDir,
            2.0f * Time.deltaTime);
    }
}
