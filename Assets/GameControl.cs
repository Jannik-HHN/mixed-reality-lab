using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject ball;

    private Rigidbody rb_ball;
    private bool isPaused = false;
    private bool isSlowedDown = false;
    // Start is called before the first frame update
    void Start()
    {
        rb_ball = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // https://stackoverflow.com/questions/43714781/proper-way-to-move-rigidbody-gameobject
            Vector3 tempVect = new Vector3(-4.75f, 2.93f, 3.96f);
            //tempVect = tempVect.normalized * speed * Time.deltaTime;
            rb_ball.MovePosition(tempVect);
            rb_ball.velocity = Vector3.zero;
            rb_ball.angularVelocity = Vector3.zero;
            rb_ball.useGravity = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(isPaused)
            {
                Time.timeScale = 1;
            } else
            {
                Time.timeScale = 0;
            }
            
            isPaused = !isPaused;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (isSlowedDown)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0.1f;
            }

            isSlowedDown = !isSlowedDown;
        }
    }
}
