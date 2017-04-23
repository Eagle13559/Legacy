using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{

    public int force = 100;

    public int enemyWorth = 5;

    [SerializeField]
    private GameObject moneyObj;

    [SerializeField]
    private string[] animationNames;

    void Start()
    {
    }

    /// <summary>
    /// Instatiates money where enemy died. 
    /// </summary>
    public void DropMoney(Vector3 locationToInstantiate)
    {
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
        }
    }
}
