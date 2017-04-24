using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyReferences : MonoBehaviour {

    private bool destroyObj = false;

    private GameObject parent;

    void Awake()
    {
        parent = transform.parent.gameObject;
        parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        if (destroyObj)
        {
            Destroy(parent);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            destroyObj = true;
        }
    }

    

}
