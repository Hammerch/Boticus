using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject PlayerCharacter;
	public Camera MainCamera;
	public GameObject GameSettings;
	
	public float zOffSet;
	public float yOffSet;
	public float xRotOffSet;
	
	private GameObject pc;
	private PlayerCharacter pcScript;
	
	// Use this for initialization
	void Start () {
		pc = Instantiate(PlayerCharacter, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "pc";
		pcScript = pc.GetComponent<PlayerCharacter>();
		zOffSet = -2.5f;
		yOffSet = 2.5f;
		xRotOffSet = 22.5f;
		
		MainCamera.transform.position = new Vector3(pc.transform.position.x, pc.transform.position.y+yOffSet, pc.transform.position.z+zOffSet); 
		MainCamera.transform.Rotate(xRotOffSet, 0, 0);	
		
		LoadCharacter();
	}
	
	public void LoadCharacter(){
		GameObject gs = GameObject.Find("GameSettings");
		
		if(gs == null){
			Instantiate(GameSettings, Vector3.zero, Quaternion.identity);
			gs.name = "gameSettings";
		}
		gameSettings gsScript = gs.GetComponent<gameSettings>();
		gsScript.LoadCharacterData();
	}
}