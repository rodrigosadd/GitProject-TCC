using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
     public Vector3 targetPlayer;
     public float speed;

     void Start()
     {
          GetPositionPlayer();
     }

     void Update()
     {
          MoveToPlayerPosition();
     }

     public void GetPositionPlayer()
     {
          targetPlayer = PlayerController.instance.transform.position;
     }

     public void MoveToPlayerPosition()
     {
          transform.position = Vector3.MoveTowards(transform.position, targetPlayer, speed * Time.deltaTime);

          if (transform.position.x == targetPlayer.x && transform.position.y == targetPlayer.y && transform.position.z == targetPlayer.z)
          {
               Destroy(gameObject);
          }
     }

     void OnTriggerEnter(Collider other)
     {
          if (other.transform.tag == "Player")
          {
               PlayerController.instance.TakeHit();
          }
     }
}
