using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public bool moveBulletUp = false;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(5, 5, 1);

        //Move object upWards
        if (moveBulletUp)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        //Destroy object if out of screen
        if (transform.position.y > 60)
        {
            Destroy(this.gameObject);
        }

    }
}
