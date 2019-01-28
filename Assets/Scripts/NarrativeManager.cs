using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;
using TMPro;
using DG.Tweening;

public class NarrativeManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI Text_narrative;



    [SerializeField]
    private StringVariable cur_narrative;

    private float cur_alpha;

    Coroutine coroutine;
    Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        cur_alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Text_narrative.color = new Color32(0, 0, 0, (byte)(cur_alpha * 255));

        if (Input.GetKeyDown(KeyCode.Q))
        {
            cur_narrative.Value = "Hi! Testing.";
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            cur_narrative.Value = "You can go anywhere.\nHave nowhere can go as well.";
        }
    }

    public void Narrative_OnChange()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        if (tween != null)
            tween.Kill();

        coroutine = StartCoroutine(Narrative_UI_Perform());
    }

    IEnumerator Narrative_UI_Perform()
    {
        tween = DOTween.To(() => cur_alpha, x => cur_alpha = x, 0, 0.5f);

        yield return new WaitForSeconds(0.5f);

        Text_narrative.text = cur_narrative.Value;

        tween = DOTween.To(() => cur_alpha, x => cur_alpha = x, 1, 3.0f);


        yield return null;
    }

}
