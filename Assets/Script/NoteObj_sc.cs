using UnityEngine;
using System.Collections;

public class NoteObj_sc : MonoBehaviour {

    public int speed;
    public bool isStart = false;

    public int channel;
    public float noteTime;

    public float destroyPositionZ;
    public float destroyDelayTime;

    // Use this for initialization
    void Start () {

    }

    void OnTriggerEnter(Collider col) {
        if (col.transform.tag == "Cut_Line") {
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart == true) {
            StartCoroutine( move() );
        }
    }

    IEnumerator move() {

        //Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (transform.position.z > destroyPositionZ) {
            transform.Translate(Vector3.back * speed * Time.smoothDeltaTime);
        } else {
            Destroy(gameObject);
        }

        yield return null;
    }

    public void setPosition(float x, float y, float z) {
        transform.position = new Vector3(x, y, z);
    }

}
