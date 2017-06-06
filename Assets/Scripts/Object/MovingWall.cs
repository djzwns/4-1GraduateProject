using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingWall : Objects
{
    enum MOVESTATE
    {
        WAIT = 0,
        LEFT = 1,
        RIGHT = 2,
    }

    private MOVESTATE m_moveState = MOVESTATE.WAIT;
    private Vector3 m_beginPosition;

    public float m_max_power = 3f;
    [Range(0f, 2f)]
    public float m_const_power = 0;

    void Start()
    {
        m_PowerEnable = true;
        m_rotateObject = this.transform;
        m_moveState = MOVESTATE.LEFT;
        GameManager.Instance.gamestart_callback += Play;
        GameManager.Instance.gamereset_callback += Stop;
    }

    void Update()
    {
        FollowToFinger(this.transform);
        RotateObject(m_rotateObject);
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.m_IsPlaying)
        {
            Move();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ball") return;
        if (!GameManager.Instance.m_IsPlaying) return;

        StartCoroutine(MoveChange(0.5f));
    }

    private IEnumerator MoveChange(float _time)
    {
        MOVESTATE lastState = m_moveState;
        m_moveState = MOVESTATE.WAIT;

        yield return new WaitForSeconds(_time);

        switch (lastState)
        {
            case MOVESTATE.LEFT:
                m_moveState = MOVESTATE.RIGHT;
                break;

            case MOVESTATE.RIGHT:
                m_moveState = MOVESTATE.LEFT;
                break;

            default:
                break;
        }
    }

    private Vector3 GetMoveDirection()
    {
        switch (m_moveState)
        {
            case MOVESTATE.WAIT:
                return Vector3.zero;

            case MOVESTATE.LEFT:
                return Vector3.left;

            case MOVESTATE.RIGHT:
                return Vector3.right;

            default: return Vector3.zero;
        }
    }

    private void Move()
    {
        Vector3 direction = GetMoveDirection();

        this.transform.Translate(direction * GetPower());
    }

    private void Stop()
    {
        Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.zero;
        m_moveState = MOVESTATE.WAIT;
        transform.position = m_beginPosition;
    }

    private void Play()
    {
        m_beginPosition = transform.position;
        m_moveState = MOVESTATE.RIGHT;
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

    void OnDestroy()
    {
        GameManager.Instance.gamestart_callback -= Play;
        GameManager.Instance.gamereset_callback -= Stop;
    }
}
