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
    [SerializeField] Camera cam;
    private int coins;
    private float min_y_pos;
    private ReadPort read;
    private bool lock_lights = false;
    // Start is called before the first frame update
    void Start()
    {

        rigi = GetComponent<Rigidbody2D>();
        pos_def = transform.position;
        global_light_lum = global_light.GetComponent<Light2D>();
        lantern_lum = lantern.GetComponent<Light2D>();
        global_light_color = global_light_lum.color;
        min_y_pos = -25;
        read = gameObject.GetComponent<ReadPort>();
    }

    // Update is called once per frame
    void Update()
    {
        mouvementVector = (Vector3.up * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal")).normalized;
        if (read.buttonPress1 > 0 || read.buttonPress2 > 0 ||read.buttonPress3 > 0 ||read.buttonPress3 > 0 || read.buttonPress4 > 0)
        {
            mouvementVector = (Vector3.left * read.buttonPress4 + Vector3.right * read.buttonPress3 + Vector3.up * read.buttonPress1).normalized;
        }
        if (!can_jump)
        {
            mouvementVector.y = 0;
        }
        if (transform.position.y < min_y_pos)
        {
            rigi.velocity = Vector2.zero;
            rigi.angularVelocity = 0;
            transform.position = pos_def;
            cam.transform.position = new Vector3(pos_def.x, cam.transform.position.y, cam.transform.position.z);
        }

        if (Input.GetKey(KeyCode.E))
        {
            lantern_lum.intensity += 0.01f;
        }

        if (Input.GetKey(KeyCode.A))
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
        }
        if (Input.GetKey(KeyCode.C))
        {
            global_light_lum.intensity += 0.005f;
            global_light_color.r += 0.005f;
            global_light_color.g += 0.005f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lock_lights = !lock_lights;
            print("Lock lights : " + lock_lights);
        }

        if (!lock_lights)
        {
            global_light_lum.intensity = read.sensor;
            lantern_lum.intensity = read.power;
            global_light_color.g = read.sensor;
            global_light_color.r = read.sensor;
        }

        global_light_color.r = global_light_color.r > 1 ? 1 : global_light_color.r;
        global_light_color.g = global_light_color.g > 1 ? 1 : global_light_color.g;
        global_light_color.r = global_light_color.r < 0.2f ? 0.2f : global_light_color.r;
        global_light_color.g = global_light_color.g < 0.2f ? 0.2f : global_light_color.g;
        global_light_lum.intensity = global_light_lum.intensity > 1 ? 1 : global_light_lum.intensity;
        global_light_lum.intensity = global_light_lum.intensity < 0.2f ? 0.2f : global_light_lum.intensity;
        global_light_lum.color = global_light_color;


    }

    private void FixedUpdate()
    {
        motionVector = new Vector3(mouvementVector.x * maxVelocity, 1.65f * mouvementVector.y * maxVelocity * (1 + Mathf.Abs(mouvementVector.x) * 0.65f), 0);
        if (motionVector.x < 0)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);
            if (transform.position.x < cam.transform.position.x)
            {
                if (Mathf.Abs(transform.position.x - cam.transform.position.x) > 1)
                {
                    cam.transform.position = new Vector3(transform.position.x + 1, cam.transform.position.y, cam.transform.position.z);
                }

            }
        }
        else if (motionVector.x > 0)
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
            if (transform.position.x > cam.transform.position.x)
            {
                if (Mathf.Abs(transform.position.x - cam.transform.position.x) > 1)
                {
                    cam.transform.position = new Vector3(transform.position.x - 1, cam.transform.position.y, cam.transform.position.z);
                }
            }
        }
        float lerpSmooth = rigi.velocity.magnitude < motionVector.magnitude ? acceleration : decceleration;
        if (!can_jump)
        {
            motionVector = new Vector3(motionVector.x, rigi.velocity.y, motionVector.z);
        }
        rigi.velocity = Vector3.Lerp(rigi.velocity, motionVector, lerpSmooth / 20);
        rigi.velocity += new Vector2(0, mouvementVector.y);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Coin"))
        {
            coins++;
            print("win");
            switch (coins)
            {
                case 1:
                    min_y_pos = -50;
                    transform.position = new Vector3(-25.6f, -46.08f, 0);
                    pos_def = transform.position;
                    cam.transform.position = new Vector3(-25.6f, -46.08f, -10);
                    break;
                case 2:
                    min_y_pos = -70;
                    transform.position = new Vector3(-25.6f, -66.08f, 0);
                    pos_def = transform.position;
                    cam.transform.position = new Vector3(-25.6f, -66.08f, -10);
                    break;

            }
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (rigi.velocity.y != 0)
    //    {
    //        can_jump = false;
    //    }
    //}


}
