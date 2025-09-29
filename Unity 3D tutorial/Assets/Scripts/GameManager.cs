using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI player1BallsText;
    [SerializeField] TextMeshProUGUI player2BallsText;
    [SerializeField] TextMeshProUGUI currentPlayerTurnText;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] float movementThereshold;
   

    [SerializeField] GameObject restartButton;
    [SerializeField] Transform headPosition;

    [SerializeField] Camera cueStickCamera;
    [SerializeField] Camera overheadCamera;
    [SerializeField] float shotTimer = 3f;
    private float currentShotTimer;
    Camera currentCamera;

    enum currentPlayer {
        Player1,
        Player2
    }

    currentPlayer player;
    bool isWinningShotPlayer1 = false;
    bool isWinningShotPlayer2 = false;
    bool iswaitingforballstostop = false;
    bool isGameOver = false;
    bool willSawpPLayer = false;
    bool ballPocketed = false;
    int player1ballsremaining = 7;
    int player2ballsremaining = 7;
    //for some reason unity counts the first  3 frame of my cueballs veloctiy as 2.5e-6 and would automatically switch over to the next player so Im placing this here to stop it
    int ignoreIntialFrameCount = 3;
    void Start()
    {
        player = currentPlayer.Player1;
        currentCamera = cueStickCamera;
    }
    
    // Update is called once per frame
    void Update()
    {//just here because the restart button is not working


        if (iswaitingforballstostop && !isGameOver)
        {
            currentShotTimer -= Time.deltaTime;
            if (currentShotTimer > 0) {
                return;
            }
            bool allballsarestopped = true;
            //does switch camera but takes a decent amount of time as if I try to do 5e-5 it just switch camera right away after the cue ball gets hit
            foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
            {
                //if (ball.GetComponent<Ball>().isCueBall())
                //{
                //     Debug.Log(ball.GetComponent<Rigidbody>().linearVelocity.magnitude);
                // }

                if (ignoreIntialFrameCount > 0 || ball.GetComponent<Rigidbody>().linearVelocity.magnitude >= movementThereshold)
                {

                    ignoreIntialFrameCount--;
                    allballsarestopped = false;
                    break;
                }
            }
           
            if (allballsarestopped)
            {

                iswaitingforballstostop = false;
                if (willSawpPLayer || !ballPocketed)
                {
                    nextPlayerTurn();
                }
                else
                {
                    switchCamera();
                }

                currentShotTimer = shotTimer;
                ballPocketed = false;
            }
        }
    }
    public void switchCamera() {
        if (currentCamera == cueStickCamera)
        {
            overheadCamera.enabled = true;
            cueStickCamera.enabled = false;
            currentCamera = overheadCamera;
            iswaitingforballstostop = true;
        }
        else {
            overheadCamera.enabled = false;
            cueStickCamera.enabled = true;
            currentCamera = cueStickCamera;
            currentCamera.GetComponent<CameraController>().resetCamera();
        }

    }
    public void RestartTheGame() {
        Debug.Log("Restarting");
        SceneManager.LoadScene(0);

    }
    public void HoverTest() {
        Debug.Log("HI");
    }
    bool scratch() {

        if (player == currentPlayer.Player1) {
            if (isWinningShotPlayer1)
            {
                scratchonwinningshot("Player 1");
                return true;
            }
            else {
                if (isWinningShotPlayer2) {
                    scratchonwinningshot("Player 2");
                    return true;
                }
               
            }
        }
       
        //nextPlayerTurn();
        willSawpPLayer = true;
        return false;
    
    }
    void Early8Ball() {
        if (player==currentPlayer.Player1) {
            lose("Player 1 has hit the 8 ball too early in - skill issue");
        }
        else lose("Player 2 has hit the 8 ball too early in - skill issue");
    }
    void scratchonwinningshot(string playername) {
        lose(playername +" " + "Scratched THEIR WINNING SHOT AND HAS LOST");
    }
    


    bool CheckBall(Ball ball) {
        if (ball.isCueBall())
        {
 
            if (scratch())
            {
                return true;
            }

            else {return false;}


        }
        else if (ball.is8Ball())
        {
            if (isWinningShotPlayer1)
            {

                win("Player 1 ");
                return true;
            }
            else if(isWinningShotPlayer2) {
                win("Player 1 ");
                return true;
            }
            Early8Ball();
        
        }
        else {
            if (ball.isBallRed())
            {
                player1ballsremaining--;
                player1BallsText.text = "Player 1 Balls Remaining: " + player1ballsremaining;
                if (player1ballsremaining <= 0)
                {   
                    isWinningShotPlayer1 = true;
                }
                if (player != currentPlayer.Player1)
                {  // iswaitingforballstostop = true;
                   willSawpPLayer = true;
                    // nextPlayerTurn();
                }

            }
            else{
                player2ballsremaining--;
                player2BallsText.text = "Player 2 Balls Remaining: " + player2ballsremaining;
                if (player2ballsremaining <= 0)
                {
                    isWinningShotPlayer2 = true;
                }
                if (player != currentPlayer.Player2)
                {   willSawpPLayer = true;
                    //iswaitingforballstostop = true;
                    //nextPlayerTurn();
                }
            }
        
        
        
  }
           return true;
    }
    void lose(string message) { 
        isGameOver = true;
        messageText.GameObject().SetActive(true);
        messageText.text = message;
        restartButton.SetActive(true);
    }
    void win(string player)
    {   isGameOver = true;
        messageText.GameObject().SetActive(true);
        messageText.text = player + "HAS WON!!!!!!!!??!??!?!!";
        restartButton.SetActive(true);
    }
    void nextPlayerTurn() {
        if (player == currentPlayer.Player1)
        {
            player = currentPlayer.Player2;
            currentPlayerTurnText.text = "Player 2's Turn";
        }
        else {

            player = currentPlayer.Player1;
            currentPlayerTurnText.text = "Player 1's Turn";
        
    }   willSawpPLayer = false;
        switchCamera();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Ball") {
                ballPocketed = true;
            if (CheckBall(other.gameObject.GetComponent<Ball>()))
            {
                Destroy(other.gameObject);
            }
            else { 
            other.gameObject.transform.position = headPosition.position;
            other.gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

          }
      }
    }

}
