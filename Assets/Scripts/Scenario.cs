using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour {

	public GameObject permaWallObject;
	public GameObject weakWallObject;
	public GameObject floorObject;
	public GameObject humanObject;
	public GameObject playerObject;
	public GameObject goalObject;
	public GameObject bridgeObject;
	public GameObject dogObject;
	public GameObject wallExplosion;

	public int width;
	public int height;
	public GameObject playerO;
	public PlayerController player;

	// Should all objects be on the same list?
	public List<GameObject> Walls = new List<GameObject>(); // Normal objects
	public List<GameObject> People = new List<GameObject>(); // possible actors
	public List<GameObject> Floors = new List<GameObject>(); // floor level objects

    private bool justChangedEmotion = false;
    private AudioSource audioPlayer;
    public AudioClip breakClip;
    public AudioClip dogClip;
    public AudioClip buildClip;
    public AudioClip speedClip;

    void Start() {
        audioPlayer = gameObject.GetComponent<AudioSource>();
		wallExplosion = Resources.Load ("WallExplosion") as GameObject;
    }

	GameObject makeObject(GameObject toadd, int i, int j){
		return (GameObject)Instantiate (toadd, new Vector3 (i, 0, j), Quaternion.identity);
	}

	private bool isInSquare(GameObject item, int i, int j){
		int x = Mathf.RoundToInt (item.transform.position.x);
		int z = Mathf.RoundToInt (item.transform.position.z);

		if (i == x && j == z) {
//			Debug.Log (i);
//			Debug.Log (x);
			return true;
		}
		return false;
	}
	 
	public void myDestroy(GameObject item){
		Walls.Remove(item);
		Destroy(item);
	}

	public int isPassable(int i, int j){
		foreach(GameObject item in Walls){
			if (isInSquare (item, i, j)) {
				//item.GetComponent<Renderer>().material.color = new Color (1, 0, 0);
				return 0;
			}
		}
		foreach(GameObject item in People){
			if (isInSquare (item, i, j)) {
				return 0;
			}
		}
		foreach(GameObject item in Floors){
			if (isInSquare (item, i, j)) {
				return 1;
			}
		}
		return 0;
	}

	public void Act(int x, int z){
		for(int k=0;k<Walls.Count;k++){
			GameObject item = Walls [k];
			if (isInSquare (item, x, z)) {
				//Debug.Log ("Act");
				if (item.GetComponent<Cake> ()) {
					Debug.Log ("You grabbed the cake, you naughty cake grabber!");
				}
				if (item.GetComponent<WeakWall>() && player.destroys > 0) {
					Instantiate (wallExplosion, new Vector3 (x, 0, z), Quaternion.identity);
					Debug.Log ("Weak wall destroyed");
                    audioPlayer.clip = breakClip;
                    audioPlayer.Play();
					player.destroys--;
					myDestroy (item);
				}
            }
		}
        for (int k = 0; k < People.Count; k++)
        {
            GameObject item = People[k];
            if (isInSquare(item, x, z))
            {
                if (item.GetComponent<Human>() && justChangedEmotion == false)
                {
                    Debug.Log("Human Collision");
                    if (item.GetComponent<Human>().mood != 'B' && player.emotion == 'B' )
                    {
                        player.emotion = item.GetComponent<Human>().mood;
                        item.GetComponent<Human>().mood = 'B';
                        Debug.Log("Player Got emotion" + player.emotion);
                        StartCoroutine(emotionHasChanged());
                    }
                    else if (item.GetComponent<Human>().mood == 'B' && player.emotion != 'B')
                    {
                        item.GetComponent<Human>().mood = player.emotion;
                        player.emotion = 'B';
                        Debug.Log("Player used emotion" + player.emotion);
                        StartCoroutine(emotionHasChanged());
                    }
                }
            }
        }
        for (int k = 0; k < Floors.Count; k++) {
			GameObject item = Floors [k];
			if (isInSquare (item, x, z)) {
				return; // floor ok
				//Debug.Log ("Act");
			}
		}
		if (player.builds > 0) {
			createBridge (x, z);
			player.builds--;
		}

	}

    IEnumerator emotionHasChanged() {
        justChangedEmotion = true;
        yield return new WaitForSeconds(0.5f);
        justChangedEmotion = false;
    }

	public void createBridge(int i, int j){
		Floors.Add(GameObject.Instantiate(bridgeObject, new Vector3(i,-0.5f,j), Quaternion.identity));
	}

	public void createPermaWall(int i, int j){
		GameObject newWall = makeObject (permaWallObject, i, j);
		Walls.Add (newWall);
	}

	public void createWeakWall(int i, int j){
		GameObject newWall = makeObject (weakWallObject, i, j);
		Walls.Add (newWall);
	}

	public void createPeople(int i, int j, char mood){
		GameObject newHuman = makeObject (humanObject, i, j);
		Human temp = newHuman.GetComponent<Human>();
		temp.mood=mood;
		People.Add(newHuman);
	}

	public void createGoal(int i, int j){
		GameObject newWall = makeObject (goalObject, i, j);
		Walls.Add (newWall);
	}

	public void createDog(int i, int j){
		GameObject newWall = makeObject (dogObject, i, j);
		Walls.Add (newWall);
	}

	public void createPlayer(int i, int j){
		playerO = makeObject (playerObject, i, j);
		player = playerO.GetComponent<PlayerController>();
		(playerO.GetComponent (typeof(PlayerController)) as PlayerController).setScenario(this);
	}

	public void createFloor(int i, int j){
		Floors.Add(GameObject.Instantiate(floorObject, new Vector3(i,-0.5f,j), Quaternion.identity));
	}
}
