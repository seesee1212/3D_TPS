using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this; // �̱��� �ν��Ͻ� ����
        }
    }


    public float detectionRadius;
    public LayerMask monsterLayer; // ���� ���̾�
    public Transform target
    {
        get { return GetNearestMonster(); } // target ������Ƽ�� ���� ����� ���͸� ��ȯ�մϴ�.
    }

    public Vector3 Direction()
    {
        Vector3 dirToMonster = (target.position - transform.position).normalized;
        return dirToMonster;
    }


    public Transform GetNearestMonster()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, monsterLayer); // "Monster" ���̾ �ش��ϴ� ���͸� ã���ϴ�.
        
        Transform nearest = null;
        float minDist = Mathf.Infinity; // �ּ� �Ÿ� �ʱ�ȭ

        foreach (Collider col in hits)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position); // ���� ��ġ�� ���� ��ġ ���� �Ÿ� ���
            if (dist < minDist) // �ּ� �Ÿ����� ������
            {
                minDist = dist; // �ּ� �Ÿ� ������Ʈ
                nearest = col.transform; // ���� ����� ���ͷ� ����
            }
        }
        return nearest; // ���� ����� ������ Transform ��ȯ
    }
}



