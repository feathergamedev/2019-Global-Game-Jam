using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FinishLine : MonoBehaviour
{
    public Image finalBlackMask;
    public GameObject UI_credit;

    [SerializeField]
    float cur_alpha;

    // Start is called before the first frame update
    void Start()
    {
        cur_alpha = 0;
        UI_credit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
//        finalBlackMask.color = new Color32(0, 0, 0, (byte)(cur_alpha * 255));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            PlayerManager.instance.m_playerState = PlayerState.Finished;
            StartCoroutine(Go_To_The_End());
        }
    }

    IEnumerator Go_To_The_End()
    {
        finalBlackMask.DOColor(new Color32(0, 0, 0, 255), 5.0f);

        yield return new WaitForSeconds(5.0f);

        UI_credit.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        finalBlackMask.DOColor(new Color32(0, 0, 0, 0), 2.5f);


    }
}
