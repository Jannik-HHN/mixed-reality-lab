using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBall : MonoBehaviour
{
    public PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ball"))
        {
            player.grabbedBall = true;
            player.GrabBall();
            Debug.Log("Triggered");
        }
    }
}
