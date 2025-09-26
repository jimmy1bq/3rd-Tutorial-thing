using UnityEngine;
using UnityEngine.UIElements;

public class GameSetup : MonoBehaviour
{
    [SerializeField] GameObject BallPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform HeadBallPosition;
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Placing nuts");
        ballRadius = BallPrefab.GetComponent<SphereCollider>().radius;
        ballDiameter = ballRadius * 2f;
        Debug.Log("Placing nuts");
        placeAllBalls();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //places every ball 
    void placeAllBalls() {
      
        placeCueBalls();
         placeRandomBalls();
    }
    void placeCueBalls()
    {
        GameObject ball = Instantiate(BallPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().makeCueBall();
    }
    void place8Balls(Vector3 Position)
    {
        GameObject ball = Instantiate(BallPrefab, Position, Quaternion.identity);
        ball.GetComponent<Ball>().make8Ball();
    }
    //place every ball that isn't the cue ball or 8 ball
    void placeRandomBalls()
    { Debug.Log("Placing Random Balls");
        int NumInThisRow = 1;
        int rand;
        Vector3 FirstInRowPosition = HeadBallPosition.position;
        Vector3 currentPosition = FirstInRowPosition;

    void PlaceRedBall(Vector3 position) {
            GameObject ball = Instantiate(BallPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;


        }
     void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(BallPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;


        }
        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < NumInThisRow; j++) 
            {
                if (i == 2 && j == 1) {
                    place8Balls(currentPosition);

                } else if (redBallsRemaining>0 && blueBallsRemaining>0) { 
                        rand = Random.Range(0, 2);
                    Debug.Log(rand);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPosition);
                    }
                    else
                    {
                        PlaceBlueBall(currentPosition);
                    }
                    } else if (redBallsRemaining > 0) {

                        PlaceRedBall(currentPosition);

                    } else if (blueBallsRemaining > 0)

                {        PlaceBlueBall(currentPosition);
                }
                currentPosition += new Vector3(1, 0, 0).normalized * ballDiameter;
            }
           FirstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPosition = FirstInRowPosition;
            NumInThisRow++;
        }
    }
}
