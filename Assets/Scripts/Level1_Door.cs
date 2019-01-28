using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level1_Door : MonoBehaviour
{

    Color32 initColor;


    // Start is called before the first frame update
    void Start()
    {
        initColor = GetComponent<SpriteRenderer>().color;        
    }

    // Update is called once per frame
    void Update()
    {


        if (PlayerManager.instance.m_playerState==PlayerState.hasShell)
        {
            StartCoroutine(Door_Open());
        }
    }

    IEnumerator Door_Open()
    {
        GetComponent<SpriteRenderer>().DOColor(new Color32(initColor.r, initColor.g, initColor.b, 0), 0.9f);
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
        yield return null;
    }
}
