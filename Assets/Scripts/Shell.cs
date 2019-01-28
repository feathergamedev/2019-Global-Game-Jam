using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shell : MonoBehaviour
{


    public int shieldProvided;
    public int speedDebuff;

    public float fadeOut_Time;

    private SpriteRenderer m_renderer;
    private Rigidbody2D m_rigid;

    [SerializeField]
    private Color color_dumped;

    [SerializeField]
    private float pushAway_force;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<SpriteRenderer>();
        m_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Get_Dumped()
    {
        StartCoroutine(Pass_Away());
    }

    IEnumerator Pass_Away()
    {
        Vector2 push_dir = Random.insideUnitCircle * pushAway_force;
        m_rigid.AddForce(push_dir);
        m_rigid.AddTorque(Random.Range(30f, 150f));

        m_renderer.DOColor(color_dumped, fadeOut_Time);


        yield return new WaitForSeconds(fadeOut_Time);
        Destroy(this.gameObject);
    }

}
