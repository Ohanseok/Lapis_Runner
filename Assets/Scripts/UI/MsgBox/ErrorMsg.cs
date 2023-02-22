using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorMsg : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Attention_Text;

    public void SetErrorMsg(ErrorType type)
    {
        gameObject.SetActive(true);

        switch(type)
        {
            case ErrorType.LackOfCondition_Archer:
                Attention_Text.text = "��ó�� ȹ���� �� �����ϴ�.";
                break;

            case ErrorType.LackOfCondition_DarkMage:
                Attention_Text.text = "�米�� ȹ���� �� �����ϴ�.";
                break;

            case ErrorType.LackOfCondition_Monk:
                Attention_Text.text = "�·��� ȹ���� �� �����ϴ�.";
                break;

            default:
                return;
        }

        transform.GetComponent<Animator>().Rebind();

        transform.GetComponent<Animator>().Play("NonTouchPopUp");

        StartCoroutine(CheckAnimator());
    }

    IEnumerator CheckAnimator()
    {
        while (transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
