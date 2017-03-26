using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour {

    [SerializeField]
    private GameObject _foreLayer;
    [SerializeField]
    private GameObject _backLayer;

    private float _deltaFore = 0.1f;
    private float _deltaBack = 0.05f;

    private Vector3 _prevLocation;

	// Use this for initialization
	void Start () {
        _prevLocation = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 currentPos = gameObject.transform.position;
        Vector3 diff = _prevLocation - currentPos;
        _foreLayer.transform.position += (diff * _deltaFore);
        _backLayer.transform.position += (diff * _deltaBack);
        _prevLocation = currentPos;
    }
}
