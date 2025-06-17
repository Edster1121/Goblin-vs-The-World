using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private float velocityDamp = 0.5f;
    [SerializeField] private float angularVelocityDamp = 0.5f;
    [SerializeField, Tooltip("the range should be all +ve")] private Vector2 xVelocityRange = new Vector2(10, 20);
    [SerializeField, Tooltip("the range should be all +ve")] private Vector2 yVelocityRange = new Vector2(10, 20);
    [SerializeField, Tooltip("the range should be all +ve")] private Vector2 xAngularVelocityRange = new Vector2(15, 30);
    [SerializeField, Tooltip("the range should be all +ve")] private Vector2 yAngularVelocityRange = new Vector2(15, 30);
    [SerializeField] private float velocityStoppageThreshold = 0.1f;
    [SerializeField] private float angularVelocityStoppageThreshold = 0.1f;

    [SerializeField, Tooltip("pulls dice onto rolling plane using gravity")]
    private float zGravity = 0.5f;
    
    
    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Debug.Log(transform.position);
        Debug.Log(rb.velocity);
        
        rb.AddForce(Vector3.forward * zGravity, ForceMode.Acceleration);

        rb.velocity *= velocityDamp;
        rb.angularVelocity *= angularVelocityDamp;

        if (rb.velocity.magnitude <= velocityStoppageThreshold &&
            rb.angularVelocity.magnitude <= angularVelocityStoppageThreshold)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            SnapToClosestFace();
            rb.Sleep();
        }
    }

    public void Roll()
    {
        //reawaken dice
        rb.WakeUp();
        
        //reset velocities to 0 before rolling
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        //make dice roll
        rb.velocity = new Vector3(
            Random.Range(xVelocityRange.x, xVelocityRange.y) * RandomSign(),
            Random.Range(yVelocityRange.x, yVelocityRange.y) * RandomSign(),
            0.5f
            );
        
        rb.angularVelocity = new Vector3(
            Random.Range(xAngularVelocityRange.x, xAngularVelocityRange.y) * RandomSign(),
            Random.Range(yAngularVelocityRange.x, yAngularVelocityRange.y) * RandomSign(),
            0.5f
        );

    }
    
    private readonly Quaternion[] _validRotations = new Quaternion[]
    {
        Quaternion.Euler(0, 0, 0),      // 1
        Quaternion.Euler(0, 0, 180),    // 6
        Quaternion.Euler(90, 0, 0),     // 2
        Quaternion.Euler(-90, 0, 0),    // 5
        Quaternion.Euler(0, 0, 90),     // 3
        Quaternion.Euler(0, 0, -90),    // 4
    };

    private void SnapToClosestFace()
    {
        Quaternion current = transform.rotation;
        float minAngle = float.MaxValue;
        Quaternion closest = _validRotations[0];

        foreach (var rot in _validRotations)
        {
            float angle = Quaternion.Angle(current, rot);
            if (angle < minAngle)
            {
                minAngle = angle;
                closest = rot;
            }
        }

        transform.rotation = closest;
    }

    private int RandomSign()
    {
        return Random.value < 0.5f ? 1 : -1;
    }

}
