using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position == Vector3.zero)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }


}
