using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float xBoundaries = 38;
    public Vector3 mousePosition;
    public Vector2 position;
    public bool gameOver = false;
    public GameObject bulletPrefab;
    public GameObject bulletSpwaner;
    public float bulletYOffset = 10;
    GameObject bullet;
    public float playerSpeed = 1;
    public static GameManager gameManager;
    public int waveNo = 1;
    public GameObject CoinPrefab;
    public GameObject CoinHolder;
    public int coinsGahtered = 0;
    public TextMeshProUGUI coinsText;
    public ParticleSystem gameOverEffect;

    public AudioClip gameOverClip;
    public AudioClip coinCollectedClip;
    public AudioClip stoneDestroyedClip;
    public AudioSource playerAudioSource;

    public Slider slider;
    public TextMeshProUGUI previousScoreText;
    public TextMeshProUGUI nextScoreText;
    public GameObject gameOverTextObject;

    int playerlevel = 1;
    public TextMeshProUGUI playerlevelText;
    int previousScore = 0;
    int nextScore = 5;

    public int score = 0;
    bool gameOverCalled = false;
    private void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanBullets());
        slider.GetComponent<Slider>().maxValue = nextScore;
        slider.GetComponent<Slider>().minValue = previousScore;
        previousScoreText.text = previousScore.ToString();
        nextScoreText.text = nextScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * playerSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
        /*if(Input.GetMouseButton(0))
        {
            mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = Vector2.Lerp(transform.position, mousePosition,1f);
            GetComponent<Rigidbody2D>().MovePosition(position);
        }*/

        
        coinsText.text = "Coins : " + coinsGahtered;


        //set slider value
        if (score > nextScore)
        {
            previousScore = nextScore;
            nextScore *= 2;
            playerlevel++;

        }


        slider.maxValue = nextScore;
        slider.minValue = previousScore;
        slider.value = score;
        previousScoreText.text = previousScore.ToString();
        nextScoreText.text = nextScore.ToString();
        playerlevelText.text = "Level : " + playerlevel;

        if (gameOver)
        {
            if (!gameOverCalled)
            {
                gameOverEffect.Play();
                gameOverCalled = true;
                StartCoroutine(gameOverCo());
            }

            gameOverTextObject.SetActive(true);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftBound"))
        {
            transform.position = new Vector3(transform.position.x + 2, transform.position.y, 80);
        }
        if (collision.gameObject.CompareTag("RightBound"))
        {
            transform.position = new Vector3(transform.position.x - 2, transform.position.y, 80);
        }
    }
    private void FixedUpdate()
    {

    }

    IEnumerator SpwanBullets()
    {
        while (!gameOver)
        {
            bullet = Instantiate(bulletPrefab, new Vector3(bulletSpwaner.transform.position.x, bulletSpwaner.transform.position.y, 0), bulletPrefab.transform.rotation);

            //bullet.transform.SetParent(bulletSpwaner.transform);

            bullet.transform.position = new Vector3(bulletSpwaner.transform.position.x, bulletSpwaner.transform.position.y + bulletYOffset, 0);
            bullet.transform.localScale = new Vector3(5, 5, 1);
            bullet.GetComponent<MoveBullet>().moveBulletUp = true;

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            playerAudioSource.PlayOneShot(coinCollectedClip);
            coinsGahtered++;
            Destroy(collision.gameObject);
        }
    }


    IEnumerator gameOverCo()
    {
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
    }

}
