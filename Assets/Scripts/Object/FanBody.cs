using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBody : Objects
{
    public Transform m_joint;
    public Transform m_fan;
    public GameObject m_effect;

    private float m_max_power = 30f;
    [Range(0f, 2f)]
    public float m_const_power = 0;
    //private ParticleSystem ps;

    void Start()
    {
        m_PowerEnable = true;
        m_rotateObject = m_joint;
        //ps = m_effect.GetComponent<ParticleSystem>();        
    }

    void Update()
    {
        FollowToFinger(this.transform);
        RotateObject(m_rotateObject);
        PowerRenewal();        
    }

    void FixedUpdate()
    {
        EffectOnOff();
        Rotate(m_fan);
    }

    public void Blow(GameObject _go)
    {
        Rigidbody2D rigid = _go.GetComponent<Rigidbody2D>();

        if (rigid == null) return;
        

        rigid.AddForce(m_joint.up * GetPower(), ForceMode2D.Force);
    }

    private void EffectOnOff()
    {
        if (!m_UIEnable) return;
        if (GetPower() != 0) m_effect.SetActive(true);
        else m_effect.SetActive(false);
        //ps.startLifetime = 1 / GetPower() * 3f;//(m_power - 1) * -1 + 1;
        //ps.startSpeed = GetPower() * 3f;

        //ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        //int count = ps.particleCount;
        //for (int i = 0; i < count; ++i)
        //{
        //    float velocity = GetPower() * 100;
        //    particles[i].startLifetime = (1 / GetPower()) * 3f;
        //    particles[i].velocity = particles[i].velocity.normalized * velocity;
        //}

        //ps.SetParticles(particles, count);
    }

    private void Rotate(Transform _transform)
    {
        if (GetPower() != 0)
        {
            _transform.Rotate(Vector3.up * GetPower());
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
