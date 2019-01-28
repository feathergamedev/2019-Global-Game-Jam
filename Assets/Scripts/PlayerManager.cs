using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{



    private Rigidbody2D m_rigid;
    private Vector2 m_velocity;

    [SerializeField]
    [Header("速度的倍率")]
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));        
    }

    private void FixedUpdate()
    {
        m_rigid.velocity = m_velocity * moveSpeed;
    }
}
