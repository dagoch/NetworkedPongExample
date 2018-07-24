using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallController : MonoBehaviour {

    public Vector2 bounds = Vector2.one;
    Vector2 direction = Vector2.zero;

    public float speed;

   // Vector3 nextPosition;
    //Vector3 lastPosition;

    float counter;

    public PongPlayerController[] paddles;

    public int scoreA = 0;
    public int scoreB = 0;
   
	void Start () {
        direction = new Vector2(0, 1);
        paddles = FindObjectsOfType<PongPlayerController>();
//        GenerateNextPosition(Random.value < .5f ? true : false);
        //lastPosition = this.transform.localPosition;
	}
	
	void Update () {
        MovePuck();
        CheckDirection();
	}

    void MovePuck()
    {
//        float dist = Vector3.Distance(lastPosition, nextPosition);
        counter += Time.deltaTime * speed;
        this.transform.Translate(new Vector3(direction.x,0,direction.y)*speed );
       

    }

    void CheckDirection(){
        if (this.transform.localPosition.x > bounds.x)
            HitWall();
        if (this.transform.localPosition.x < -bounds.x)
            HitWall();
        if (this.transform.localPosition.z > bounds.y)
        {
            HitPaddle();

        }
        if (this.transform.localPosition.z < -bounds.y)
        {
            HitPaddle();
        }
    }
   
    void HitWall(){
        direction = new Vector2(direction.x * -1, direction.y);
    }
    void HitPaddle(){

        int whichPaddle =
            Vector3.Distance(this.transform.localPosition, paddles[0].transform.localPosition) >
            Vector3.Distance(this.transform.localPosition, paddles[1].transform.localPosition) ?
                   1 : 0;
        
        direction = new Vector2((this.transform.localPosition.x-paddles[whichPaddle].transform.localPosition.x)*.5f, direction.y * -1);
        if (Vector3.Distance(this.transform.localPosition, paddles[whichPaddle].transform.localPosition) > 1.5f)
        {
            if (whichPaddle == 0) scoreA += 1; else scoreB += 1;
            direction = new Vector2(0, direction.y);

            //Destroy(this.gameObject);
        }
    }
}
