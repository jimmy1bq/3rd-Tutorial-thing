using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isred;
    private bool iscueball = false;
    private bool is8ball = false;
    bool firstContactWithBall = false;
    public bool hitRedBall = false;
    public bool hitBlueBall = false;

    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.y = 0f;
        rb.linearVelocity = newVelocity;
    }
    public bool isBallRed()
    {
        return isred;
    }
    public bool is8Ball()
    {
        return is8ball;
    }

    public bool isCueBall()
    {
        return iscueball;
    }


    public void BallSetup(bool red)
    {
        isred = red;
        if (isred)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void makeCueBall()
    {
        iscueball = true;
    }
    public void make8Ball()
    {
        is8ball = true;
        GetComponent<Renderer>().material.color = Color.black;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isCueBall()) {
            if (collision.gameObject.tag == "Ball" && !firstContactWithBall) {
                if (collision.gameObject.GetComponent<Ball>().isred) {
                    firstContactWithBall = true;
                    hitRedBall = true;


                } else if (!collision.gameObject.GetComponent<Ball>().isred) {
                    firstContactWithBall = true;
                    hitBlueBall = true;

                }
            }
        
        }
    }
}
