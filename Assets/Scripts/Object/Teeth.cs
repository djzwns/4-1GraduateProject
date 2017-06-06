using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : Objects
{
    public ParticleSystem m_effect;
    public Transform m_direction;
    public Transform[] m_teeth;
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
        if (coll.tag == "ball" && m_spitFunc != null)
        {
            spit(m_spitTime);
            StopCoroutine(m_spitFunc);
        }
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

        bite(_time);

        yield return new WaitForSeconds(_time);

        spit(_time);

        m_effect.Play();
        BGMManager.Instance.PlaySound(m_effectSound);
        rigid.gravityScale = 1.2f;
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.AddForce(this.transform.up * GetPower(), ForceMode2D.Impulse);
    }

    private void bite(float _time)
    {
        Quaternion target;

        target = Quaternion.Euler(0, 0, -50);
        StartCoroutine(bite_spitCoroutine(m_teeth[0], _time * 0.125f, target));

        target = Quaternion.Euler(0, 0, 50);
        StartCoroutine(bite_spitCoroutine(m_teeth[1], _time * 0.125f, target));
    }

    private void spit(float _time)
    {
        Quaternion target;

        target = Quaternion.Euler(0, 0, 0);
        StartCoroutine(bite_spitCoroutine(m_teeth[0], _time * 0.05f, target));
        StartCoroutine(bite_spitCoroutine(m_teeth[1], _time * 0.05f, target));
    }

    // 수정 필요.
    private IEnumerator bite_spitCoroutine(Transform _transform, float _time, Quaternion _target)
    {
        Quaternion original = _transform.rotation;

        float time = 0;
        while (time <= 1)
        {
            time += Time.deltaTime / _time;
            _transform.localRotation = Quaternion.Lerp(original, _target, time);
            yield return new WaitForFixedUpdate();
        }
    }

    private float GetPower()
    {
        float power = 0;
        if (m_const_power == 0)
            power = m_max_power * (m_power + 1);
        else
            power = m_const_power * m_max_power;

        return power;
    }
}
