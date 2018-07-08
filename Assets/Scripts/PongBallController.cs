using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallController : MonoBehaviour {

    public Vector2 bounds = Vector2.one;

    public float speed;

    Vector3 nextPosition;
    Vector3 lastPosition;

    float counter;
   
	void Start () {
        GenerateNextPosition(Random.value < .5f ? true : false);
        lastPosition = this.transform.localPosition;
	}
	
	void Update () {
        LerpToNextPosition();
	}

    void LerpToNextPosition()
    {
        counter += Time.deltaTime * speed;
        this.transform.localPosition = Vector3.Lerp(lastPosition, nextPosition, counter);
        if (counter > 1)
        {
            counter = 0;
            GenerateNextPosition(this.transform.localPosition.z < 0 ? true : false);
            lastPosition = this.transform.position;
        }

    }
    void GenerateNextPosition(bool upDown){
        nextPosition = upDown ? new Vector3(0, 0, bounds.y) : new Vector3(0, 0, -bounds.y);
    }
}
