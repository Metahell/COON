using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControls : MonoBehaviour
{
    private Rigidbody2D rigi;
    private Vector3 mouvementVector = Vector3.zero;
    private Vector3 motionVector = Vector3.zero;
    [Header("Movement Parameters")]
    [SerializeField]
    public float maxVelocity;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float decceleration;

    private bool can_jump;
    private Vector3 direction;
    private Vector3 pos_def;
    // Start is called before the first frame update
    void Start()
    {

        rigi = GetComponent<Rigidbody2D>();
        pos_def = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouvementVector = (Vector3.up * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal")).normalized;
        if (!can_jump)
        {
            mouvementVector.y = 0;
        }
        if (transform.position.y < -50)
        {
            rigi.velocity = Vector2.zero;
            rigi.angularVelocity = 0;
            transform.position = pos_def;
        }
        
    }

    private void FixedUpdate()
    {
        motionVector = new Vector3(mouvementVector.x * maxVelocity, mouvementVector.y * maxVelocity, 0);
        if (motionVector.x < 0)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);
        }
        else if (motionVector.x > 0)
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
        }
        float lerpSmooth = rigi.velocity.magnitude < motionVector.magnitude ? acceleration : decceleration;
        if (!can_jump)
        {
            motionVector = new Vector3(motionVector.x,rigi.velocity.y,motionVector.z);
        }
        rigi.velocity = Vector3.Lerp(rigi.velocity, motionVector, lerpSmooth / 20);
        rigi.velocity += new Vector2(0,mouvementVector.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("test");
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.normalized == new Vector2(0,1))
            {
                can_jump = true;
            }
            if (contact.normal.normalized == new Vector2(1, 0) || contact.normal.normalized == new Vector2(-1, 0))
            {
            }
            Debug.DrawRay(contact.point, contact.normal, Color.white,10);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        can_jump = false;
    }

}
