using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour {

  public int playerNum;
  public float bumpMagnitude, falloff, bumpScale;

  private Vector3 bumpForce, bumpLocation;

  void Start(){
    Quaternion bumpRotation = Quaternion.identity;
    bumpRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    transform.rotation = bumpRotation;
    transform.localScale *= bumpScale;
  }

  void OnTriggerEnter(Collider other){
    if(other.gameObject.tag == "Player"){
      Player p = other.transform.parent.GetComponent<Player>();
      bumpPlayer(p);
    }
  }

  void bumpPlayer(Player player){
    if(playerNum != player.playerNum ){
      Vector3 bumpDirection = new Vector3 (player.transform.position.x - transform.position.x,
      transform.position.y, player.transform.position.z - transform.position.z);
      bumpForce = bumpDirection / bumpDirection.magnitude;
      bumpForce *= bumpMagnitude - bumpDirection.magnitude * falloff;
      print(bumpMagnitude - bumpDirection.magnitude * falloff);
      player.moveScript.wasBumped(bumpForce);
    }
  }

  void OnDrawGizmos(){
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, bumpForce);
  }

}
