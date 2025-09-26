using UnityEngine;

public class Ball : MonoBehaviour
{   private bool isred;
    private bool iscueball = false;
    private bool is8ball = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isBallRed() {
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
       isred= red;
        if (isred) { 
        GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
        GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void makeCueBall() { 
          iscueball = true;
    }
    public void make8Ball()
    {
        is8ball = true;
        GetComponent<Renderer>().material.color = Color.black;
    }
}
