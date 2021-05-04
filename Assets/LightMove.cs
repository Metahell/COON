using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightMove : MonoBehaviour
{
    public Vector3 start;
    public Vector3 dest;
    [Range(0.1f, 10)]
    [SerializeField]
    private float speed=3;
    public Vector3 cur;
    public GameObject lantern;
    private Light2D lantern_lum;
    private Collider2D col;
    private Color tmpm;
    private Color tmps;
    private Transform lantern_pos;
    public GameObject global_light;
    private Light2D global_light_lum;
    [Range(-2, 2)]
    [SerializeField] float min_lantern_intensity = 0;
    [Range(-2, 2)]
    [SerializeField] float max_lantern_intensity = 0.5f;
    [Range(0.2f, 1)]
    [SerializeField] float min_global_intensity = 0.6f;
    [SerializeField] GameObject moon;
    [SerializeField] GameObject sun;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        tmpm = moon.GetComponent<SpriteRenderer>().color;
        tmps = sun.GetComponent<SpriteRenderer>().color;
        global_light_lum = global_light.GetComponent<Light2D>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cur = transform.position;
        if (global_light_lum.intensity < min_global_intensity&&cur!=start)
        {
            transform.position = Vector3.Lerp(transform.position, start, speed * Time.deltaTime);
        }
        if (global_light_lum.intensity > min_global_intensity && cur != dest)
        {
            transform.position = Vector3.SmoothDamp(transform.position, dest,ref velocity, 10);
        }
        tmpm.a = 1f - (global_light_lum.intensity);
        moon.GetComponent<SpriteRenderer>().color = tmpm;
        tmps.a = (global_light_lum.intensity);
        sun.GetComponent<SpriteRenderer>().color = tmps;
    }
}
