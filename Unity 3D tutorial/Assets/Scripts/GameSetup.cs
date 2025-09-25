using UnityEngine;

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
        ballRadius = BallPrefab.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
