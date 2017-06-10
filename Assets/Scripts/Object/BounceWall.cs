using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : Objects
{
    
    public Animator m_anim;

    private bool animating = false;

    void Start()
    {
        //m_PowerEnable = false;
        m_rotateObject = this.transform;
    }

    void Update()
    {
        FollowToFinger(this.transform);
        RotateObject(m_rotateObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (CanActive(coll))
        {
            Vector3 velocity = coll.relativeVelocity * (1f - coll.collider.sharedMaterial.bounciness);
            velocity = new Vector3(-velocity.x, velocity.y);
            coll.rigidbody.AddForce(velocity, ForceMode2D.Impulse);
            BGMManager.Instance.PlaySound(m_effectSound);

            if(!animating)
                StartCoroutine(Scale(velocity.y));

            //m_anim.SetBool("play", true);
        }
    }

    //void OnCollisionExit2D(Collision2D coll)
    //{
    //    //if (CanActive(coll))
    //        //m_anim.SetBool("play", false);
    //}

    IEnumerator Scale(float _velocity)
    {
        float y_scale = this.transform.localScale.y;
        float y_size = Mathf.Clamp(y_scale - (_velocity * 0.1f), 0.5f, 1.0f);

        Vector3 target = new Vector3(transform.localScale.x, y_size, transform.localScale.z);
        Vector3 original = transform.localScale;

        animating = true;

        float time = 0f;
        while (time <= 1)
        {
            time += Time.deltaTime / 0.1f;
            transform.localScale = Vector3.Lerp(original, target, time);
            yield return new WaitForFixedUpdate();
        }
        time = 0;
        while (time <= 1)
        {
            time += Time.deltaTime / 0.1f;
            transform.localScale = Vector3.Lerp(target, original, time);
            yield return new WaitForFixedUpdate();
        }

        animating = false;
    }
}
