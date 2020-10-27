using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class HeadAnimTrig2 : MonoBehaviour
{
    public GameObject HeadObj;
    public GameObject Player;
    public GameObject Target;
    public AudioSource FxHitSound;
    private Vector3 ScreenPoint;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(HeadAnimStart());
    }

    IEnumerator HeadAnimStart()
    {
        ScreenPoint = Camera.main.WorldToScreenPoint(Target.transform.position);

        if (ScreenPoint.x < 0 || ScreenPoint.y < 0 || ScreenPoint.z < 0)
        {
            Debug.Log("해당 오브젝트가 카메라 영역안에 존재하지 않는다");
            Player.GetComponent<FirstPersonController>().enabled = false;
            Player.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            Debug.Log("해당 오브젝트가 카메라 영역안에 존재한다");
        }

        HeadObj.GetComponent<Animation>().Play("HeadMoveAnim2");
        Player.GetComponent<FirstPersonController>().PlayerMoveStop();
        yield return new WaitForSeconds(0.1f);
        FxHitSound.Play();
        yield return new WaitForSeconds(2);
        Player.GetComponent<FirstPersonController>().PlayerMoveStart();
        Player.GetComponent<FirstPersonController>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
