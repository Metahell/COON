using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lanternManager : MonoBehaviour
{
    public GameObject lantern;
    private Light2D lantern_lum;
    private Collider2D col;
    private Color tmp;
    private Transform lantern_pos;
    public GameObject global_light;
    private Light2D global_light_lum;
    [Range(-2, 2)]
    [SerializeField] float min_lantern_intensity = 0;
    [Range(-2, 2)]
    [SerializeField] float max_lantern_intensity = 0.5f;
    [Range(0.2f, 1)]
    [SerializeField] float min_global_intensity = 0.4f;
    [Range(0.2f, 1)]
    [SerializeField] float max_global_intensity = 0.8f;
    [SerializeField] bool fade_with_light;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        tmp = col.transform.gameObject.GetComponent<SpriteRenderer>().color;
        lantern_lum = lantern.GetComponent<Light2D>();
        lantern_pos = lantern.transform;
        global_light_lum = global_light.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = true;

        if (fade_with_light)
        {
            if ((Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) < 2.58f))
            {
                if (lantern_lum.intensity > max_lantern_intensity)
                {
                    if (global_light_lum.intensity < max_global_intensity)
                    {
                        tmp.a = (Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) - 0.60f) / 2.58f;
                    }
                    if (Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) < 1)
                    {
                        col.enabled = false;
                    }
                }
                else if (lantern_lum.intensity < min_lantern_intensity)
                {
                    if (global_light_lum.intensity > min_global_intensity)
                    {
                        tmp.a = 1 - (Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) - 0.60f) / 2.58f;
                    }
                    col.enabled = true;
                }
                else
                {

                    tmp.a = 1.2f - (global_light_lum.intensity);
                    if (global_light_lum.intensity > max_global_intensity)
                    {
                        col.enabled = false;
                    }
                }
            }
            else
            {
                tmp.a = 1.2f - (global_light_lum.intensity);
                if (global_light_lum.intensity > max_global_intensity)
                {
                    col.enabled = false;
                }

            }
        } else
        {
            if ((Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) < 2.58f))
            {
                if (lantern_lum.intensity < min_lantern_intensity)
                {
                    if (global_light_lum.intensity > min_global_intensity)
                    {
                        tmp.a = (Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) - 0.60f) / 2.58f;
                    }
                    if (Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) < 1)
                    {
                        col.enabled = false;
                    }
                }
                else if (lantern_lum.intensity > max_lantern_intensity)
                {
                    if (global_light_lum.intensity < max_global_intensity)
                    {
                        tmp.a = 1 - (Vector3.Distance(lantern_pos.position, col.ClosestPoint(lantern_pos.position)) - 0.60f) / 2.58f;
                    }
                    col.enabled = true;
                }
                else
                {

                    tmp.a = 0.2f + (global_light_lum.intensity);
                    if (global_light_lum.intensity < min_global_intensity)
                    {
                        col.enabled = false;
                    }
                }
            }
            else
            {
                tmp.a = 0.2f + (global_light_lum.intensity);
                if (global_light_lum.intensity < min_global_intensity)
                {
                    col.enabled = false;
                }

            }
        }

        col.transform.gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

}
