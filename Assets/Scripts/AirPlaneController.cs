using UnityEngine;

public class AirPlaneController : MonoBehaviour
{
    public float speed = 10.0f;  // Adjust this to control the airplane's speed.
    public float rotationSpeed = 2.0f;  // Adjust this to control the airplane's rotation speed.

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component.
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle user input for airplane controls.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate forward movement and apply it.
        Vector3 forwardMovement = transform.forward * verticalInput * speed;
        rb.AddForce(forwardMovement);

        // Calculate rotation and apply it.
        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * rotation);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
