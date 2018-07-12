using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MonoBehaviour {

    Vector3 lastPosition;
    Vector3 nextPosition;
    public float speed;
    bool walking = false;

    void Start () {
		
	}
	
	void Update () {
        if(!walking){
            walking = true;
            Vector2 rand = Random.insideUnitCircle.normalized;
            nextPosition = this.transform.position + new Vector3(rand.x, 0, rand.y);
            lastPosition = this.transform.position;
            StartCoroutine(Walk());
        }
	}

    IEnumerator Walk(){
       
        float count = 0;
        while(count<speed){
            count += Time.deltaTime;
            this.transform.position = Vector3.Lerp(lastPosition, nextPosition, count / speed);
            yield return null;
        }
        walking = false;


    }
}
