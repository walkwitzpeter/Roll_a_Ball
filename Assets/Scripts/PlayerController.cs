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
            Debug.LogError(winTextObject);
            Debug.LogError(winTextObject.GetComponent<TextMeshPro>());
            Debug.LogError(winTextObject.GetComponent<Text>().text);

            winTextObject.GetComponent<Text>().text = "You beat level ";
            winTextObject.SetActive(true);
            Invoke("LoadNextScene", 2.0f);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX * speed, 0.0f, movementY * speed);

        rb.velocity = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
