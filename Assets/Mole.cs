using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Mole : MonoBehaviour, OnTouch3D
{
    private GameLogic game;
    private const float UP_TIME = 1.0f;
    private const float UP_Y = 1.5f;
    private const float DOWN_Y = -1f;
    private const float ANIM_SPEED = 0.2f;
    // status 
    // 0: waiting
    // 1: rising
    // 2: up
    // 3: recovering
    private int status;
    private float ellapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<GameLogic>();
        status = 3;
        ellapsedTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        ellapsedTime -= Time.deltaTime;
        var position = gameObject.transform.localPosition;
        switch (status)
        {
            case 0:
                {
                    // time to get its head up
                    if (ellapsedTime <= 0)
                    {
                        status = 1;
                    }
                    break;
                }
            case 1:
                {
                    if (position.y >= UP_Y)
                    {
                        gameObject.transform.localPosition = new Vector3(position.x, UP_Y, position.z);
                        status = 2;
                        // hold this position for 0.8-1.2 second
                        ellapsedTime = Random.Range(0.8f, 1.2f);
                    } else
                    {
                        gameObject.transform.Translate(new Vector3(0, ANIM_SPEED * Time.deltaTime, 0));
                    }
                    break;
                }
            case 2:
                {
                    // havn't get caught in time, miss
                    if (ellapsedTime <= 0)
                    {
                        game.miss++;
                        status = 3;
                    }
                    break;
                }
            case 3:
                {
                    // go back to the hole
                    if (position.y <= DOWN_Y)
                    {
                        gameObject.transform.localPosition = new Vector3(position.x, DOWN_Y, position.z);
                        // timeout, set to next status 
                        status = 0;
                        ellapsedTime = Random.Range(1.5f, 5.0f);
                    } else
                    {
                        gameObject.transform.Translate(new Vector3(0, -ANIM_SPEED * Time.deltaTime, 0));
                    }
                    break;
                }
        }

    }

    public int getStatus()
    {
        return status;
    }

    public void OnTouch()
    {
        // get caught
        if (status == 2 || status == 1)
        {
            game.count++;
            status = 3;
        }
    }
}
