using System.Collections;
using TMPro;
using UnityEngine;

public class DamageTMP : MonoBehaviour
{
    private TextMeshProUGUI m_Text;
    private RectTransform rectTransform;

    private Vector2 velocity; //�ʱ� �ӵ�(������ �)
    private float gravity = -1000f; // �߷� ȿ��(UI �̵��̹Ƿ� �� ���� �ʿ�)
    private float lifetime = 1.0f; // ���� �ð�

    private Color textColor;

    private void Awake()
    {
        m_Text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void Initalize(Transform parent, Vector3 pos, string temp)
    {
        transform.SetParent(parent);

        m_Text.text = temp;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);
        rectTransform.position = screenPosition;

        velocity = new Vector2(Random.Range(-50f, 50f), Random.Range(150f, 250f)); // �ʱ� �ӵ� ����

        textColor = m_Text.color; // �ؽ�Ʈ ���� ����

        StartCoroutine(MoveAndFade()); // �ڷ�ƾ ����
    }

    IEnumerator MoveAndFade()
    {
        float elapsedTime = 0.0f;

        while(elapsedTime < lifetime)
        {
            velocity.y += gravity * Time.deltaTime; // �߷� ����

            rectTransform.anchoredPosition += velocity * Time.deltaTime; // ��ġ ������Ʈ

            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / lifetime); // ���� ����
            m_Text.color = textColor;

            elapsedTime += Time.deltaTime; // ��� �ð� ������Ʈ
            yield return null; // ���� �����ӱ��� ���
        }

        MANAGER.POOL.m_pool_Dictionary["DamageTMP"].Return(this.gameObject);
    }
}
