using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;

    public Vector3 velocity;
    private Vector3 previousPosition;
    public float rotation;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
         rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        // Calculate velocity
        velocity = (transform.position - previousPosition) / Time.deltaTime;

        previousPosition = transform.position;
    }
}