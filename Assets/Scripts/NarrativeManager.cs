using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityAtoms;
using TMPro;
using DG.Tweening;

public class NarrativeManager : MonoBehaviour
{
    public static NarrativeManager instance;

    [SerializeField]
    private TextMeshProUGUI Text_narrative;

    [SerializeField]
    private Image Narrative_frame;

    public StringVariable cur_narrative;

    private float cur_alpha;

    Coroutine coroutine;
    Tween tween;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cur_narrative.Value = "";
        cur_alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Text_narrative.color = new Color32(255, 255, 255, (byte)(cur_alpha * 255));
        Narrative_frame.color = new Color32(0, 0, 0, (byte)(cur_alpha * 255));

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
        tween = DOTween.To(() => cur_alpha, x => cur_alpha = x, 0, 0.25f);

        yield return new WaitForSeconds(0.5f);

        Text_narrative.text = cur_narrative.Value;

        tween = DOTween.To(() => cur_alpha, x => cur_alpha = x, 1, 2.0f);


        yield return new WaitForSeconds(7.0f);

        tween = DOTween.To(() => cur_alpha, x => cur_alpha = x, 0, 2.0f);

        yield return null;
    }

}
