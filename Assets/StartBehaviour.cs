using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartBehaviour : MonoBehaviour
{
    [SerializeField]
    private ReadPort read;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (read.buttonPress1 > 0 || Input.GetKey(KeyCode.Z))
        {
            SceneManager.LoadScene(0);
        }
    }
}
