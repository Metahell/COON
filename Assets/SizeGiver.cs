using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeGiver : MonoBehaviour
{
    Collider2D m_Collider;
    Vector3 m_Size;

    void Start()
    {
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider2D>();

        //Fetch the size of the Collider volume
        m_Size = m_Collider.bounds.size;

        //Output to the console the size of the Collider volume
        Debug.Log("Collider Size : " + m_Size);
    }
}
