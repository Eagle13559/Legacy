using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{

    public int force = 100;

    public int enemyWorth = 5;

    [SerializeField]
    private GameObject moneyObj;

    private Rigidbody2D[] instantiatedObj;

    [SerializeField]
    private string[] animationNames;

    private float _physicsTimer = 0;
    private float _physicsMaxTime = 1.5f;

    private bool instantiated = false;

    void Start()
    {
        
    }

    void Update()
    { 
        if (instantiated)
        {
            if (_physicsTimer > _physicsMaxTime)
            {
                //StartCoroutine(FreezeAllObj());
                instantiated = false;
            }
            else
            {
                _physicsTimer += Time.deltaTime;
            }
        }
        
    }

    /// <summary>
    /// Instatiates money where enemy died. 
    /// </summary>
    public void DropMoney(Vector3 locationToInstantiate)
    {
        instantiatedObj = new Rigidbody2D[enemyWorth];
        for (int i = 0; i < enemyWorth; i++)
        {
            GameObject tempObj = Instantiate(moneyObj, locationToInstantiate, Quaternion.identity) as GameObject;

            // set animation to random animation of jelly money
            tempObj.GetComponent<AnimationController2D>().setAnimation(animationNames[i % animationNames.Length]);

            tempObj.SetActive(true);

            float angle = Random.Range(Mathf.PI / 6, Mathf.PI * (5 / 3));

            Vector2 forceVect = new Vector2(1, Mathf.Sin(angle) * force);

            Rigidbody2D physics = tempObj.GetComponent<Rigidbody2D>();

            physics.bodyType = RigidbodyType2D.Dynamic;

            // apply a force to move the money
            tempObj.GetComponent<Rigidbody2D>().AddForce(forceVect * force);

            instantiatedObj[i] = physics;
        }

        instantiated = true;
    }

    public IEnumerator FreezeAllObj ()
    {
        foreach(Rigidbody2D objPhysics in instantiatedObj)
        {
            if (objPhysics != null)
                objPhysics.bodyType = RigidbodyType2D.Static;
            yield return new WaitForEndOfFrame();
        }
    }
}
