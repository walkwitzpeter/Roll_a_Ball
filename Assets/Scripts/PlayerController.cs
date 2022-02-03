using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public int numberOfCubes;

    public GameObject pickupObjects;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    private bool touchingTank = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= pickupObjects.transform.childCount)
        {
            //winTextObject.text = "You beat level " + SceneManager.GetActiveScene().buildIndex.ToString();
            //Debug.LogError(winTextObject);
            //Debug.LogError(winTextObject.GetComponent<TextMeshPro>());
            //Debug.LogError(winTextObject.GetComponent<Text>().text);

            //winTextObject.GetComponent<Text>().text = "You beat level ";
            winTextObject.SetActive(true);
            Invoke("LoadNextScene", 2.0f);
        }
    }

    //void Update()
    //{
    //    if (touchingTank)
    //    {
    //        rb.AddForce(-movementX * 3 * speed, 0f, -movementY * 3 * speed);
    //        Debug.LogError(movementY + "tank3" + movementX);
    //    }
    //}

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX * speed, 0.0f, movementY * speed);

        rb.velocity = movement;
        //rb.AddForce(movement);

        //if (touchingTank)
        //{
        //    rb.AddForce(-movementX * 3 * speed, 0f, -movementY * 3 * speed);
        //    Debug.LogError(movementY + "tank2" + movementX);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
        //if (other.gameObject.CompareTag("Tank"))
        //{
        //    // This stuff doesn't work (if the on trigger is clicked then it can run thro the tank)
        //    Debug.LogError(movementY + "tank" + movementX);
        //    rb.AddForce(-movementX * 3 * speed, 0f, -movementY * 3 * speed);
        //    Debug.LogError(movementY + "tank" + movementX);
        //}
        //if (other.gameObject.CompareTag("Star"))
        //{
        //    rb.AddForce(-movementX, 0f, -movementY);
        //    Debug.LogError("star");
        //}
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Tank")
    //    {
    //        touchingTank = false;
    //    }
    //}

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
