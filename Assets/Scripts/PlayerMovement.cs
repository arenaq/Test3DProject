using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody rb;
    public float forwardForce;
    public float backwardForce;
    public float sidewaysForce;
    public float jumpForce;

    private const int MOVEMENT_NONE = -1;
    private const int MOVEMENT_FORWARD = 0;
    private const int MOVEMENT_BACKWARD = 1;
    private const int MOVEMENT_LEFT = 2;
    private const int MOVEMENT_RIGHT = 3;
    private const int MOVEMENT_JUMP = 4;
    private const int MOVEMENT_STOP = 5;
    private int movement = MOVEMENT_NONE;

    private void Update() {
        if (Input.GetKey("w")) {
            movement = MOVEMENT_FORWARD;
        }

        if (Input.GetKey("s")) {
            movement = MOVEMENT_BACKWARD;
        }

        if (Input.GetKey("a")) {
            movement = MOVEMENT_LEFT;
        }

        if (Input.GetKey("d")) {
            movement = MOVEMENT_RIGHT;
        }

        if (Input.GetKey("space") && rb.velocity.y == 0) {
            // TODO add raycast condition for jumping when near the ground https://www.youtube.com/watch?v=EINgIoTG8D4
            movement = MOVEMENT_JUMP;
        }

        if (Input.GetKey("x")) {
            movement = MOVEMENT_STOP;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        switch (movement) {
            case MOVEMENT_FORWARD:
                rb.AddForce(0, 0, forwardForce * Time.deltaTime, ForceMode.VelocityChange);
                break;
            case MOVEMENT_BACKWARD:
                rb.AddForce(0, 0, -backwardForce * Time.deltaTime, ForceMode.VelocityChange);
                break;
            case MOVEMENT_LEFT:
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                break;
            case MOVEMENT_RIGHT:
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                break;
            /*case MOVEMENT_JUMP:
                rb.AddForce(0, jumpForce * Time.deltaTime, 0, ForceMode.VelocityChange);
                break;
            case MOVEMENT_STOP:
                rb.velocity = Vector3.zero;
                break;*/
        }
        movement = MOVEMENT_NONE;

        if (rb.position.y < -1f) {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
