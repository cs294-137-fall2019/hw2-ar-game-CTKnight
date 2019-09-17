using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private const int MISS_THRESHOLD = 5;

    public int count;
    public int miss;
    // 0: ready
    // 1: started
    // 2: finished
    // 3: not ready
    private int status;
    public GameObject gameBoard;
    public Text messageText;
    public Button gameControlButton;
    // Start is called before the first frame update
    void Start()
    {
        status = 3;
        gameBoard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (miss >= MISS_THRESHOLD)
        {
            status = 2;
        }
        updateUI();
    }

    public void init()
    {
        count = miss = 0;
    }

    private void updateUI()
    {
        switch (status)
        {
            case 0:
                {
                    gameControlButton.gameObject.SetActive(true);
                    gameControlButton.GetComponentInChildren<Text>().text = "Start";
                    updateText("Click on Start!");
                    break;
                }
            case 1:
                {
                    gameControlButton.gameObject.SetActive(false);
                    updateText(formatScore());
                    break;
                }
            case 2:
                {
                    gameControlButton.GetComponentInChildren<Text>().text = "Restart";
                    updateText("Click on Restart!");
                    break;
                }
            case 3:
                {
                    updateText("Finding a plane to place game board");
                    gameControlButton.gameObject.SetActive(false);
                    break;
                }
        }
    }

    public void ready()
    {
        status = 0;
    }

    public void gameStart()
    {
        gameBoard.SetActive(true);
        init();
    }

    private string formatScore()
    {
        return $"{count} hit {miss} miss!";
    }

    private void updateText(string content)
    {
        messageText.text = content;
    }

    public void onControlClicked()
    {
        switch (status)
        {
            case 0:
                {
                    status = 1;
                    gameStart();
                    break;
                }
            case 2:
                {
                    status = 0;
                    break;
                }
        }
    }
}
