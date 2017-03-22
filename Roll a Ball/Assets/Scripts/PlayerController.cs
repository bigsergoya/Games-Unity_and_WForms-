using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float speed; //Паблик позволяет редактировать ее значение из юнити
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    private int count;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countText.text = getCountText(count);
        winText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement*speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            countText.text = getCountText(count);
            if(count == 4)
            {
                winText.text = "U win";
            }
        }
    }
    private string getCountText(int count)
    {
        return "Count: " + count.ToString();
    }
}
//Destroy(other.gameObject);