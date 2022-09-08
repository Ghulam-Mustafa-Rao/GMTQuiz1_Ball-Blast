using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Rocks : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int life = 5;
    GameObject spawnedObject;
    public int initialLife = 0;
    public SpwanManager spwanManager;
    Vector3 initialSize;
    public GameObject animationImage;
    public GameObject rockImage;

    bool destroyercalled = false;
    private void Awake()
    {
        initialSize = new Vector3(84, 84, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().rotation = Random.Range(-5000, 5000);
    }

    // Update is called once per frame
    void Update()
    {
        if (life > 10)
        {
            transform.localScale = initialSize + (new Vector3(10, 10, 1)) * 3;
        }
        else
        {
            transform.localScale = initialSize + (new Vector3(life, life, 1)) * 3;
        }


        scoreText.text = life.ToString();
        //check to destroy rock and give player points
        if (life <= 0 && !destroyercalled)
        {
            //coin spwan
            spawnedObject = Instantiate(GameManager.gameManager.CoinPrefab, new Vector3(transform.position.x, GameManager.gameManager.CoinHolder.transform.position.y, 80), GameManager.gameManager.CoinPrefab.transform.rotation);
            spawnedObject.transform.SetParent(GameManager.gameManager.CoinHolder.transform);
            spawnedObject.transform.position = transform.position;




            //give player points
            GameManager.gameManager.score += initialLife;


            StartCoroutine(DestroyTheRock());

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!destroyercalled)
        {
            /*if (collision.gameObject.CompareTag("Ground"))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 30, ForceMode2D.Impulse);
            }
            if (collision.gameObject.CompareTag("Stone"))
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            }*/
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
                life--;
                if (life > 6 && life < 13 && life % 2 == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        spawnedObject = Instantiate(spwanManager.stone, transform.position, Quaternion.identity);
                        spawnedObject.transform.SetParent(spwanManager.rockHolder.transform);
                        spawnedObject.GetComponent<Rocks>().spwanManager = spwanManager;
                        spawnedObject.GetComponent<Rocks>().life = life;
                        spawnedObject.GetComponent<Rocks>().initialLife = life;
                        /*if(i==0)
                            spawnedObject.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 30, ForceMode2D.Impulse);
                        else
                            spawnedObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 30, ForceMode2D.Impulse);*/
                    }
                    StartCoroutine(DestroyTheRock());

                }
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.gameManager.playerAudioSource.PlayOneShot(GameManager.gameManager.gameOverClip);
                GameManager.gameManager.gameOver = true;
            }
            /*GetComponent<Rigidbody2D>().AddTorque(2000);

            if (GetComponent<Rigidbody2D>().angularVelocity < 0)
                GetComponent<Rigidbody2D>().rotation = Random.Range(-5000, -1000);
            else
                GetComponent<Rigidbody2D>().rotation = Random.Range(1000, 5000);*/
        }

    }

    IEnumerator DestroyTheRock()
    {
        destroyercalled = true;
        GameManager.gameManager.playerAudioSource.PlayOneShot(GameManager.gameManager.stoneDestroyedClip);
        rockImage.SetActive(false);
        scoreText.gameObject.SetActive(false);
        animationImage.SetActive(true);
        yield return new WaitForSeconds(0.30f);
        
        Destroy(gameObject);
    }



}
