using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageManager : MonoBehaviour
{
    public GameObject player;
    public GameObject UI;
    public GameObject pausedUI;

    public bool is3D; // true if game mode is 3d, false if game mode is 2d
    public bool isFirst;

    private int stageNumber = 0;
    private bool isPaused = false;

    float[] minRangeXZ = { 2.0f, 2.5f, 2.0f, 2.5f, 2.5f };
    float[] maxRangeXZ = { 3.0f, 3.5f, 3.0f, 3.5f, 3.5f };
    float[] minRangeY = { -1.0f, -2.0f, -1.0f, -2.0f, -2.0f };
    float[] maxRangeY = { 0.5f, 1.0f, 0.5f, 1.0f, 1.0f };

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void OnTriggerGoal()
    {
        if (stageNumber == 4)
        {
            float time = UI.GetComponentInChildren<RecordTime>().time;
            int death = player.GetComponent<PlayerTriggers>().GetDeathCount();
            GameObject.Find("Message").GetComponent<MessageScript>().level++;
            if (is3D)
            {
                GameObject.Find("Message").GetComponent<MessageScript>().time3d = time;
                GameObject.Find("Message").GetComponent<MessageScript>().death3d = death;
                if(isFirst)
                    SceneManager.LoadScene(2);
            }
            else
            {
                GameObject.Find("Message").GetComponent<MessageScript>().time2d = time;
                GameObject.Find("Message").GetComponent<MessageScript>().death2d = death;
                if(isFirst)
                    SceneManager.LoadScene(1);
            }

            if (!isFirst)
            {
                Debug.Log("Game Over");
                MessageScript msg = GameObject.Find("Message").GetComponent<MessageScript>();
                Debug.Log(string.Format("2dtime: {0:f} 2ddeath:{1:d} 3dtime: {2:f} 3ddeath:{3:d}",
                    msg.time2d, msg.death2d, msg.time3d, msg.death3d));
                SceneManager.LoadScene(3);
            }
        }
        else
        {
            stageNumber++;
            Restart();
        }
    }

    void Restart()
    {
        InitiateStage();
    }

    void GenerateBlocks()
    {
        GameObject block = Resources.Load("Prefabs/Block", typeof(GameObject)) as GameObject;
        GameObject goal = Resources.Load("Prefabs/GoalBlock", typeof(GameObject)) as GameObject;

        float randX, randY, randZ, dist, randTheta;

        Instantiate(block, Vector3.zero, Quaternion.identity);
        Vector3 position = Vector3.zero;
        for (int i = 0; i < stageNumber/2; i++)
        {
            dist = Random.Range(minRangeXZ[stageNumber], maxRangeXZ[stageNumber]);
            if (is3D)
            {
                randTheta = Random.Range(-(Mathf.PI / 4), Mathf.PI / 2);
                randX = dist * Mathf.Sin(randTheta);
                randZ = dist * Mathf.Cos(randTheta);
            }
            else
            {
                randX = dist;
                randZ = 0;
            }
            randY = Random.Range(minRangeY[stageNumber], maxRangeY[stageNumber]);
            position += new Vector3(randX, randY, randZ);
            Instantiate(block, position, Quaternion.identity);
        }
        dist = Random.Range(minRangeXZ[stageNumber], maxRangeXZ[stageNumber]);
        if (is3D)
        {
            randTheta = Random.Range(-(Mathf.PI / 4), Mathf.PI / 2);
            randX = dist * Mathf.Sin(randTheta);
            randZ = dist * Mathf.Cos(randTheta);
        }
        else
        {
            randX = dist;
            randZ = 0;
        }
        randY = Random.Range(minRangeY[stageNumber], maxRangeY[stageNumber]);
        position += new Vector3(randX, randY, randZ);
        Instantiate(goal, position, Quaternion.identity);
    }
    void ClearBlocks()
    {
        foreach (GameObject block in GameObject.FindGameObjectsWithTag("Block"))
        {
            GameObject.Destroy(block);
        }
    }
    void InitiateStage()
    {
        player.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ClearBlocks();
        GenerateBlocks();
        Pause();
        
    }

    void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        pausedUI.SetActive(true);
        TextMeshProUGUI stageText = pausedUI.GetComponentsInChildren<TextMeshProUGUI>()[1];
        if (is3D)
        {
            stageText.text = string.Format("3D - {0:D}", stageNumber + 1);
        }
        else
        {
            stageText.text = string.Format("2D - {0:D}", stageNumber + 1);
        }

        UI.SetActive(false);

    }

    void Resume()
    {
        isPaused = false;
        Time.timeScale = 1;
        pausedUI.SetActive(false);
        UI.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Message").GetComponent<MessageScript>().level == 1)
        {
            isFirst = true;
        }
        else
        {
            isFirst = false;
        }

        InitiateStage();
    }
    // Update is called once per frame
    void Update()
    {
        if(isPaused && Input.GetKeyDown(KeyCode.Space))
        {
            Resume();
        }
    }
}
