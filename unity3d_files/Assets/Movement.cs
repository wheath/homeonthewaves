using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public KeyCode pressUp;
    public KeyCode pressDown;
    public KeyCode pressLeft;
    public KeyCode pressRight;
    public KeyCode loadLevel;
    public string currentLevel;

    private Rigidbody rb;
    public float speed = 1.0f;
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   //the player's right side will be considered its "front"
        Vector3 forward = gameObject.transform.right;

        //use these keys to navigate around the map
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);

        //the player will now turn to face its direction of movement. These are necessary for picking up and moving the ramps and blocks
        if (Input.GetKeyDown(pressUp)) GetComponent<Transform>().eulerAngles = new Vector3(0, -90, 0);

        if (Input.GetKeyDown(pressDown)) GetComponent<Transform>().eulerAngles = new Vector3(0, 90, 0);

        if (Input.GetKeyDown(pressLeft)) GetComponent<Transform>().eulerAngles = new Vector3(0, 180, 0);

        if (Input.GetKeyDown(pressRight)) GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(loadLevel)) SceneManager.LoadScene(currentLevel);
        if (Input.GetKeyDown("escape")) Application.Quit();
    }
}