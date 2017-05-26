using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : Objects
{
    public ParticleSystem m_effect;
    public Transform m_direction;
    public float m_spitTime = 1f;

    private float m_max_power = 10f;
    [Range(0f, 2f)]
    public float m_const_power = 0;

    private IEnumerator m_spitFunc;

    void Start()
    {
        m_PowerEnable = true;
        m_rotateObject = this.transform;
    }

    void Update()
    {
        FollowToFinger(this.transform);
        RotateObject(m_rotateObject);
        PowerRenewal();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (!GameManager.Instance.m_IsPlaying) return;
        if (coll.tag != "ball") return;

        m_spitFunc = Spit(coll.gameObject, m_spitTime);
        StartCoroutine(m_spitFunc);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.tag == "ball" && m_spitFunc != null)
            StopCoroutine(m_spitFunc);
    }

    /// <summary>
    /// 오브젝트를 잡았다가 _time 시간이 지난 후에 발사함
    /// </summary>
    /// <param name="_go"></param>
    /// <param name="_time"></param>
    /// <returns></returns>
    private IEnumerator Spit(GameObject _go, float _time)
    {
        Rigidbody2D rigid = _go.GetComponent<Rigidbody2D>();

        if (rigid == null) yield return null;

        //m_effect.Play();
        BGMManager.Instance.PlaySound(m_effectSound);
        _go.transform.position = m_direction.position;

        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 0;
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(_time);

        float power = 0;
        if (m_const_power == 0)
            power = m_max_power * (m_power + 1);
        else
            power = m_const_power * m_max_power;

        m_effect.Play();
        BGMManager.Instance.PlaySound(m_effectSound);
        rigid.gravityScale = 1.2f;
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.AddForce(this.transform.up * power, ForceMode2D.Impulse);
    }
}
