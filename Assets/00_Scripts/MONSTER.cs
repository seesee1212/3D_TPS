using UnityEngine;

public class MONSTER : MonoBehaviour
{
    public int HP;
    public int MaxHP;

    public Transform target;
    public string monsterId;

    public bool isDead = false;
    public bool isSpawned = false; // ���Ͱ� �����Ǿ����� ����

    private IFactory<MONSTER> factory;

    public virtual void Initalize(Transform player)
    {
        MANAGER.SESSION.AddMonster();
        isSpawned = false;
        HP = 10;
        MaxHP = HP;

        isDead = false;

        monsterId = Random.Range(0, 2) == 1 ? "Skeleton_01" : "Skeleton_02"; // ���� ID�� �������� ����

        factory = new GenericPartFactory<MONSTER>(MANAGER.DB.Monster);
        target = player;
        factory.Build(this, monsterId);
    }

    public void GetDamage(int dmg)
    {
        HP -= dmg;

        var damageFont = MANAGER.POOL.pooling_OBJ("DamageTMP").Get((value) =>
        {
            value.GetComponent<DamageTMP>().Initalize(
                Base_Canvas.instance.HOLDERLAYER,
                transform.position,
                dmg.ToString());
        });

        if (HP <= 0)
        {
            isDead = true;
            MANAGER.SESSION.RemoveMonster();

            var deadEffect = MANAGER.POOL.pooling_OBJ("DeadEffect").Get((value) =>
            {
                value.transform.position = transform.position;
            });

            MANAGER.Instance.Run(Util_Coroutine.Delay(0.5f,
                () => MANAGER.POOL.m_pool_Dictionary["DeadEffect"].Return(deadEffect)));


            MANAGER.POOL.m_pool_Dictionary["Monster"].Return(this.gameObject);

            DropEXP(transform.position, Random.Range(10.0f, 50.0f));
        }
    }

    private void DropEXP(Vector3 deathPosition, float exp = 1.0f)
    {
        // ���࿡ ����ġ�� 3.8f�� ��� ���� ��ü = 0.25f, �߰���ü = 1.0f, ū ��ü = 3.0f
        // ȹ���ϴ� ����ġ �� + ��ü�� ���ؼ� ȹ���ϴ� ����ġ �� + 

        float[] units = {50.0f, 30.0f, 10.0f };

        foreach(float unit in units)
        {
            while(exp >= unit)
            {
                exp -= unit;

                OrbMake(deathPosition, unit);
            }
        }

        if(exp > 0.01f)
        {
            OrbMake(deathPosition, exp);
        }
    }

    private void OrbMake(Vector3 deathPosition, float exp)
    {
        Vector3 spawnPos = deathPosition + Utils_World.GetRandomCircleOffset(1.5f);
        spawnPos.y += 0.5f;
        var orb = MANAGER.POOL.pooling_OBJ("Orb").Get((value) =>
        {
            value.transform.position = transform.position;
            value.GetComponent<Orb>().Initalize(exp, spawnPos);
        });
    }
}

    
