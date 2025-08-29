using System.Collections;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public Card[] cards; // 카드 배열
    bool isReady = true;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Initalize()
    {
        if (isReady == false) return;
        isReady = false;
        animator.Play("Selector_Open");
        for(int i = 0; i < cards.Length; i++)
        {
            cards[i].Initalize();
        }
    }

    public void SelectCard(int value)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (i == value)
            {
                cards[i].SetAnimation("Card_Select");
            }
            else 
            {
                cards[i].SetAnimation("Card_NonSelect");
            }

            cards[i].isSelected = true;
        }
        StartCoroutine(GameStartCoroutine());
    }

    IEnumerator GameStartCoroutine()
    {
        // Time.TimeScale == 0
        yield return new WaitForSecondsRealtime(1f);
        animator.Play("Selector_Close");
        Time.timeScale = 1f;
    }
}
