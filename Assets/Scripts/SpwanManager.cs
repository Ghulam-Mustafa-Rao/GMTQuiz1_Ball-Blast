using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    GameObject spawnedObject;
    public GameObject stone;
    public GameObject rockHolder;
    public float xBounds;
    int lifeIncrement = 0;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //spwan rocks if all rocks are destroyed
        if (rockHolder.transform.childCount == 0)
        {
            SpwanRocks();
        }
    }


    void SpwanRocks()
    {
        int life = 1;
        //Spwan rocks according to waveno
        for (int i = 0; i < GameManager.gameManager.waveNo; i++)
        {

            if (GameManager.gameManager.waveNo > 3)
            {
                lifeIncrement = GameManager.gameManager.waveNo + 1;
            }
            life = GameManager.gameManager.waveNo + lifeIncrement;

            if (i == 3)
            {
                break;
            }

            //create rock
            spawnedObject = Instantiate(stone, new Vector3(Random.Range(-xBounds, xBounds + 1), rockHolder.gameObject.transform.position.y, 80), Quaternion.identity);
            spawnedObject.transform.SetParent(rockHolder.transform);
            spawnedObject.GetComponent<Rocks>().spwanManager = this;
            spawnedObject.GetComponent<Rocks>().life = life;
            spawnedObject.GetComponent<Rocks>().initialLife = life;


        }
        GameManager.gameManager.waveNo++;
    }

}
