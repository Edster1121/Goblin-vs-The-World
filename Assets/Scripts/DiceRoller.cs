using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] float rollForce = 10f;
    [SerializeField] float torque = 10f;

    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Debug.Log(transform.position);
        if (rb.velocity.magnitude < 0.05f && rb.angularVelocity.magnitude < 0.05f)
        {
            rb.Sleep(); // stops all movement and rotation
        }   
    }

    public void Roll()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Make sure dice is in front of camera (z = 0 or close)
        transform.position = new Vector3(0, 2, 0);

        Vector3 randomDirection = new Vector3(
            Random.Range(-0.5f, 0.5f),
            1f,
            Random.Range(-0.5f, 0.5f)
        ).normalized;

        rb.AddForce(randomDirection * rollForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * torque, ForceMode.Impulse);
    }

}
