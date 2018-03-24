using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour {

	public static Camera activeCamera {get; set;}

	public static void setActiveCamera(int cameraIndex)
	{
		for(int i = 0; i < Camera.allCameras.Length; i++){
			if(i == cameraIndex){
				Camera.allCameras[i].enabled = true;
				activeCamera = Camera.allCameras[i];
			}else{
				Camera.allCameras[i].enabled = false;
			}
		}
	}

}
