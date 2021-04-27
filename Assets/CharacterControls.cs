using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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

    [SerializeField]
    private Vector2 test;

    public GameObject lantern;
    private Light2D lantern_lum;
    public GameObject global_light;
    private Light2D global_light_lum;
    public Color global_light_color;
    // Start is called before the first frame update
    void Start()
    {

        rigi = GetComponent<Rigidbody2D>();
        pos_def = transform.position;
        global_light_lum = global_light.GetComponent<Light2D>();
        lantern_lum = lantern.GetComponent<Light2D>();
        global_light_color = global_light_lum.color;
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

        if (Input.GetKey(KeyCode.A))
        {
            lantern_lum.intensity += 0.01f;
        }

        if (Input.GetKey(KeyCode.E))
        {
            lantern_lum.intensity -= 0.01f;
        }

        lantern_lum.intensity = lantern_lum.intensity > 2 ? 2 : lantern_lum.intensity;
        lantern_lum.intensity = lantern_lum.intensity < -2 ? -2 : lantern_lum.intensity;
        can_jump = rigi.velocity.y == 0;

        if (Input.GetKey(KeyCode.W))
        {
            global_light_lum.intensity -= 0.005f;
            global_light_color.r -= 0.005f;
            global_light_color.g -= 0.005f;
            global_light_color.r = global_light_color.r < 0.2f ? 0.2f : global_light_color.r;
            global_light_color.g = global_light_color.g < 0.2f ? 0.2f : global_light_color.g;
        }
        if (Input.GetKey(KeyCode.C))
        {
            global_light_lum.intensity += 0.005f;
            global_light_color.r += 0.005f;
            global_light_color.g += 0.005f;
            global_light_color.r = global_light_color.r > 1 ? 1 : global_light_color.r;
            global_light_color.g = global_light_color.g > 1 ? 1 : global_light_color.g;
        }

        global_light_lum.intensity = global_light_lum.intensity > 1 ? 1 : global_light_lum.intensity;
        global_light_lum.intensity = global_light_lum.intensity < 0.2f ? 0.2f : global_light_lum.intensity;
        global_light_lum.color = global_light_color;
    }

    private void FixedUpdate()
    {
        motionVector = new Vector3(mouvementVector.x * maxVelocity,1.65f* mouvementVector.y * maxVelocity *(1+ Mathf.Abs(mouvementVector.x)*0.65f), 0);
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

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    foreach (ContactPoint2D contact in collision.contacts)
    //    {
    //        test = contact.normal.normalized;
    //        if (contact.normal.normalized == new Vector2(0,1))
    //        {
    //            can_jump = true;
    //        }
    //        if (contact.normal.normalized == new Vector2(1, 0) || contact.normal.normalized == new Vector2(-1, 0))
    //        {
    //        }
    //        Debug.DrawRay(contact.point, contact.normal, Color.white,10);
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (rigi.velocity.y != 0)
    //    {
    //        can_jump = false;
    //    }
    //}


}
