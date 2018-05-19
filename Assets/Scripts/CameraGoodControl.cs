using UnityEngine;
using System.Collections;
 
public class CameraGoodControl : MonoBehaviour {
  
  float scrollSpeed = 5;
  float distance = 0;
  float fprotation = 0;
  bool firstPerson = false;
  bool started = false;

    // Use this for initialization
  void Start () {
  }
 
    // Update is called once per frame
  void Update () {
    MoveCamera();
    RotateCamera();
  }

  
  private void MoveCamera(){
    /*if(firstPerson){
      Vector3 pos = hahmot.GiveLocation();
      pos.y+=2.3F;
      Camera.main.transform.position = pos+distance*Camera.main.transform.forward;
    }
    */
    float wheel = Input.GetAxis("Mouse ScrollWheel");
    //Zoom on mousewheel
    if (firstPerson){
      distance += wheel*3;
    } else {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit;
      Physics.Raycast(ray, out hit);
   //   if( Camera.main.transform.position.y + (hit.point.y-Camera.main.transform.position.y)*wheel > 200 ){
   //   } else if( Camera.main.transform.position.y + (hit.point.y-Camera.main.transform.position.y)*wheel < 100 ){
   //   } else {
        Camera.main.transform.position += (hit.point-Camera.main.transform.position)*wheel;
    //  }
    }
    // Movement vectors X and Z
    Vector3 camMoveX = Camera.main.transform.forward;
    camMoveX.y=0;
    camMoveX.Normalize();
    Vector3 camMoveZ = new Vector3(camMoveX.z, 0, -camMoveX.x);
    // scroll map on middlebutton
    if(Input.GetMouseButton(2)){
      float moveX = Input.GetAxisRaw ("Mouse X");
      float moveY = Input.GetAxisRaw ("Mouse Y");
      Camera.main.transform.position += camMoveX*moveY*25;
      Camera.main.transform.position += camMoveZ*moveX*25; 
    }
/*    
    // Keyboard movement
    if(Input.GetKey("left")) { //(xpos >= 0 && xpos < ResourceManager.ScrollWidth) || 
       Camera.main.transform.position-= camMoveZ*scrollSpeed;
    } else
    if(Input.GetKey("right")) { //(xpos <= Screen.width && xpos > Screen.width - ResourceManager.ScrollWidth) || 
      Camera.main.transform.position += camMoveZ*scrollSpeed;
    }
    
    //vertical camera movement
    if(Input.GetKey("down")) {//(ypos >= 0 && ypos < ResourceManager.ScrollWidth) || 
      Camera.main.transform.position -= camMoveX*scrollSpeed;
    } else if( Input.GetKey("up")) { //(ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth) ||
      Camera.main.transform.position += camMoveX*scrollSpeed;
    }
		*/
 /*   
    // Don't go underground
    if (Camera.main.transform.position.y < Terrain.activeTerrain.SampleHeight(Camera.main.transform.position)+1F){
      Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 
        Terrain.activeTerrain.SampleHeight(Camera.main.transform.position)+1F, Camera.main.transform.position.z);
    }
		*/
  }
 
  private void RotateCamera() {
    // right mouse button down -> rotate around camera
/*    if (firstPerson){
      Vector3 pos = hahmot.GiveLocation();
      Camera.main.transform.rotation = hahmot.GiveRotation();
      Camera.main.transform.RotateAround ( pos, Vector3.up, 90 * fprotation*10F);
    }
*/

    if(Input.GetMouseButton(1)){
      float rotationX = Input.GetAxisRaw ("Mouse X");
      float rotationY = Input.GetAxisRaw ("Mouse Y");
      //     Ray ray = Camera.main.ScreenPointToRay( new Vector3((Screen.width/2), (Screen.height/2)) );
      //     RaycastHit hit;
      //      Physics.Raycast(ray, out hit);
      
      //      Camera.main.transform.position = Camera.main.transform.position + (Camera.main.transform.forward)*-zooming*10;
      // Horizontal
      if (firstPerson){
        fprotation += rotationX*Time.deltaTime;
      } else {
        Camera.main.transform.RotateAround ( Camera.main.transform.position, Vector3.up, 90 * Time.deltaTime*rotationX*10F);
      
        // Vertical
        Vector3 oldForward = Camera.main.transform.forward;
        Quaternion oldCameraDirection=Camera.main.transform.rotation;
        Vector3 CameraLeftAxis= new Vector3(-oldForward.z,0,oldForward.x);
        Camera.main.transform.RotateAround ( Camera.main.transform.position, CameraLeftAxis, 90 * Time.deltaTime*rotationY*10F);
        // We have a problem if camera points directly up or down, undo
        Vector3 testForward=Camera.main.transform.forward;
        testForward.y = 0;
   //     if(testForward.magnitude < 0.5){
  //        Camera.main.transform.rotation = oldCameraDirection;
  //      }
      }
    }    
  }
}