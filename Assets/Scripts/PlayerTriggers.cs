using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTriggers : MonoBehaviour
{
    public Rigidbody rb;
    public TextMeshProUGUI deathText;

    private int deathCount;
    private bool isOut;
    // Start is called before the first frame update

    public int GetDeathCount()
    {
        return deathCount;
    }
    void Start()
    {
        deathCount = 0;
        isOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOut)
        {
            deathCount += 1;
            rb.transform.position = new Vector3(0.0f, 2.0f, 0.0f);
            rb.velocity = Vector3.zero;
            rb.transform.rotation = Quaternion.identity;
            isOut = false;
        }
        deathText.text = string.Format("Death: {0:D}", deathCount);
    }

    void OnTriggerEnter(Collider o)
    {
        if(o.gameObject.tag == "Boundary")
        {
            isOut = true;
        }
        if(o.gameObject.tag == "Goal")
        {
            FindObjectOfType<StageManager>().OnTriggerGoal();
            rb.velocity = Vector3.zero;
        }
    }
}
