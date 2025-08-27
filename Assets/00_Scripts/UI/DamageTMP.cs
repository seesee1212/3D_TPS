using System.Collections;
using TMPro;
using UnityEngine;

public class DamageTMP : MonoBehaviour
{
    private TextMeshProUGUI m_Text;
    private RectTransform rectTransform;

    private Vector2 velocity; //초기 속도(포물선 운동)
    private float gravity = -1000f; // 중력 효과(UI 이동이므로 값 조정 필요)
    private float lifetime = 1.0f; // 지속 시간

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

        velocity = new Vector2(Random.Range(-50f, 50f), Random.Range(150f, 250f)); // 초기 속도 설정

        textColor = m_Text.color; // 텍스트 색상 저장

        StartCoroutine(MoveAndFade()); // 코루틴 시작
    }

    IEnumerator MoveAndFade()
    {
        float elapsedTime = 0.0f;

        while(elapsedTime < lifetime)
        {
            velocity.y += gravity * Time.deltaTime; // 중력 적용

            rectTransform.anchoredPosition += velocity * Time.deltaTime; // 위치 업데이트

            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / lifetime); // 투명도 감소
            m_Text.color = textColor;

            elapsedTime += Time.deltaTime; // 경과 시간 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        MANAGER.POOL.m_pool_Dictionary["DamageTMP"].Return(this.gameObject);
    }
}
