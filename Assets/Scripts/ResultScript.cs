using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ResultScript : MonoBehaviour
{
    private const string error = "error";
    public TextMeshProUGUI resultText;
    // Start is called before the first frame update

    public void GoBackToFirstMenu()
    {
        Debug.Log("click");
        if (GameObject.Find("Message"))
        {
            Destroy(GameObject.Find("Message"));
        }
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        GameObject msgObject = GameObject.Find("Message");
        if (msgObject)
        {
            MessageScript msg = msgObject.GetComponent<MessageScript>();
            string text = string.Format("2D�ð�: {0:F}\t2D���: {1:D}\n3D�ð�: {2:F}\t3D���: {3:D}\n", msg.time2d, msg.death2d, msg.time3d, msg.death3d);
            if (msg.is2DFirst)
            {
                text += "2D����\n";
            }
            else
            {
                text += "3D����\n";
            }
            resultText.text = text;
        }
        else
        {
            resultText.text = error;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
