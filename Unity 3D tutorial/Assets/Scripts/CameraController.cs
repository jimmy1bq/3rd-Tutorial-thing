using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downangle;
    [SerializeField] float power;
    [SerializeField] GameObject cueStick;
    float horizontalInput; 
    Transform cueBall;
    GameManager gameManager;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {  gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) {
          if(ball.GetComponent<Ball>().isCueBall()) {
              cueBall = ball.transform;
                break;
            }
        }
        resetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(cueBall != null) {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            resetCamera();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&& gameObject.GetComponent<Camera>().enabled ) { 
            
            Vector3 hitdirectoin = transform.forward;
         hitdirectoin= new Vector3(hitdirectoin.x,0,hitdirectoin.z).normalized;
        cueBall.GetComponent<Rigidbody>().AddForce(hitdirectoin*power,ForceMode.Impulse);
        cueStick.SetActive(false);
            gameManager.switchCamera();



        }
    }
    public void resetCamera(){
        cueStick.SetActive(true);
        transform.position = cueBall.position + offset;
    transform.LookAt(cueBall.position);
    transform.localEulerAngles = new Vector3(downangle, transform.localEulerAngles.y, 0);


    }
}
