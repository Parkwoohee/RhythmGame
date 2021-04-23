using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchJudgment : MonoBehaviour
{
    public GameObject hitParticle, noteFragments, dropNote;
    public Rigidbody rigidbody;
    private 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = noteFragments.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.z < 0.5f)
        {
            LifeBar.LifeBarControll -= 3;
            this.gameObject.SetActive(false);
        }
    }
    
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "glove")
        {
            //맞은 coll의 glove(왼손, 오른손)의 ControolerVelocity()를 가져와서 판단
            if (coll.gameObject.GetComponent<Controller>().ControllerVelocity())
            {
                //파티클생성 및 삭제
                Instantiate(hitParticle, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f), transform.rotation);
                //맞은 노트들은 파편을 남기며 떨어진다.
                dropNote = Instantiate(noteFragments, new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f), transform.rotation);
                rigidbody.AddRelativeForce(new Vector3(0, 3, 8), ForceMode.Impulse); //앞으로 튀어나가며 떨어짐
                //점수를 올리고 lifeBar제어
                ScoreUI.Score++;
                if (LifeBar.LifeBarControll <= 15)
                {
                    LifeBar.LifeBarControll++;
                }
                //맞은 NoteObj 비활성화
                this.gameObject.SetActive(false);
            }
        }
    }
}
