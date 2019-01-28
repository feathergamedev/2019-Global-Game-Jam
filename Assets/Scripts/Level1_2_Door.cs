using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level1_2_Door : MonoBehaviour
{

    public GameObject targetShell;
    Color32 initColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.instance.cur_home == targetShell)
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
