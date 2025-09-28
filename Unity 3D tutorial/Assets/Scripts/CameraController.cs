using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downangle;
    [SerializeField] float power;
    [SerializeField] GameObject cueStick;
    [SerializeField] TextMeshProUGUI powerText;

    private bool istakingshot = false;
    [SerializeField] float maxDrawDistance;
    private float savedMousePosition;
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
        if(cueBall != null && !istakingshot) {
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }

        shoot();
    }
    public void resetCamera(){
        cueStick.SetActive(true);
        transform.position = cueBall.position + offset;
    transform.LookAt(cueBall.position);
    transform.localEulerAngles = new Vector3(downangle, transform.localEulerAngles.y, 0);


    }
    void shoot() {
        if (gameObject.GetComponent<Camera>().enabled) {

            if (Input.GetKeyDown(KeyCode.LeftShift) && !istakingshot) {
                istakingshot = true;
                savedMousePosition = 0f;

            } else if (istakingshot) {
                if (savedMousePosition + Input.GetAxis("Mouse Y") <= 0) { 

                savedMousePosition += Input.GetAxis("Mouse Y");

                    if (savedMousePosition <= maxDrawDistance) { 

                        savedMousePosition = maxDrawDistance;
                    }
                    float powerVal = ((savedMousePosition - 0) / (maxDrawDistance - 0)) * (100 - 0) + 0;
                    int powerInt = Mathf.RoundToInt(powerVal);
                    powerText.text = "Power: "+ powerVal.ToString() + "%";
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Vector3 hitdirectoin = transform.forward;
                    hitdirectoin = new Vector3(hitdirectoin.x, 0, hitdirectoin.z).normalized;
                    cueBall.GetComponent<Rigidbody>().AddForce(hitdirectoin * power * Mathf.Abs(savedMousePosition), ForceMode.Impulse);
                    cueStick.SetActive(false);
                    gameManager.switchCamera();
                    istakingshot = false;
                }
            }
        }
    }
}
