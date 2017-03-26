using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    [SerializeField]
    private GameObject _foreLayer;
    [SerializeField]
    private GameObject _backLayer;

    private float _deltaFore;
    private float _deltaBack;

    private GameObject _camera;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.position.Set(_camera.transform.position.x, _camera.transform.position.y, gameObject.transform.position.z);
    }
}
