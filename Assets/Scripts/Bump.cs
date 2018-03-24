using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bump : MonoBehaviour {

  public int playerNum;
  public float bumpMagnitude, falloff, bumpScale;
  public AudioClip[] bumpAudio;

  private Vector3 bumpForce, bumpLocation;
  private AudioSource audioSource;

  void Start(){
    Quaternion bumpRotation = Quaternion.identity;
    bumpRotation.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    transform.rotation = bumpRotation;
    transform.localScale *= bumpScale;
    audioSource = GetComponent<AudioSource>();
    audioSource.PlayOneShot(bumpAudio[Random.Range(0, bumpAudio.Length)], bumpMagnitude * 0.6f);
  }

  void OnTriggerEnter(Collider other){
    if(other.gameObject.tag == "Player"){
      Player p = other.transform.parent.GetComponent<Player>();
      bumpPlayer(p);
    }
  }

  void bumpPlayer(Player player){
    if(playerNum != player.playerNum ){
      Vector3 playerHeading = new Vector3 (player.transform.position.x - transform.position.x,
      transform.position.y, player.transform.position.z - transform.position.z);
      float playerDistance = playerHeading.magnitude * 0.95f;
      Vector3 bumpDirection = playerHeading / playerDistance;
      float bumpScalar = Mathf.Clamp(bumpMagnitude - playerDistance * playerDistance, 0, bumpMagnitude);
      bumpForce = bumpDirection * bumpScalar;
      print(bumpScalar);
      player.wasBumped(bumpForce);
    }
  }

  void OnDrawGizmos(){
    Gizmos.color = Color.red;
    Gizmos.DrawLine(transform.position, bumpForce);
  }

}
