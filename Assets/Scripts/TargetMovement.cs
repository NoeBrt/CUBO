using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;
    private float speedX = 1, speedZ, speedY;
    private int frames;
    private int frameInterval = 200;
    private PlayerRayManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerRayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate at random location, each 200 frame, the rotation change
        transform.Rotate(new Vector3(speedX, speedY, speedZ) * speed * Time.deltaTime);
        if (Time.frameCount % frameInterval == 0)
        {
            if (!player.getGameOver())
            {
                changeRotationSpeed();
            }
        }

    }
    void changeRotationSpeed()
    {
        speedX = Random.Range(-speed, speed);
        speedY = Random.Range(-speed, speed);
        speedZ = Random.Range(-speed, speed);
    }
}
