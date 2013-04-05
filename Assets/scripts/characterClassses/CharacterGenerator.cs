using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {
	
	private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int _pointsLeft;
	
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASE_LABEL_WIDTH = 30;
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HEIGHT = 20;
	private const int STAT_STARTING_POS = 40;
	
	public GUISkin Boticus_Skin;
	
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () {
		GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		pc.name = "pc";
		_toon = pc.GetComponent<PlayerCharacter>();
		_toon.Awake();
		_pointsLeft = STARTING_POINTS;
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++  ){ // loop through the player attributes
			_toon.getPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			_pointsLeft -=(STARTING_VALUE-MIN_STARTING_ATTRIBUTE_VALUE);
		}
		_toon.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		GUI.skin = Boticus_Skin;
		
		displayName();
		displayAttributes();
		displayVitals();
		displaySkills();
		displayRemainingPoints();
		
		if(_pointsLeft>0 || _toon.Name==""){
			displayCreateLabel();
		}
		else{
			displayCreateButton();
		}
	}
	
	private void displayName(){
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");
		_toon.Name = GUI.TextField(new Rect(65, 10, 200, 25), _toon.Name);
	}
	
	private void displayAttributes(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++  ){ // loop through the player attributes
			GUI.Label(new Rect(OFFSET, 
				STAT_STARTING_POS+(cnt*LINE_HEIGHT), 
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT), ((AttributeName)cnt).ToString()); // Attribute Name
			
			GUI.Label(new Rect(STAT_LABEL_WIDTH+OFFSET, 
				STAT_STARTING_POS+(cnt*LINE_HEIGHT), 
				BASE_LABEL_WIDTH, 
				LINE_HEIGHT), _toon.getPrimaryAttribute(cnt).AdjustedValue.ToString()); // Attribute Value
			
			if(GUI.Button(new Rect(OFFSET+STAT_LABEL_WIDTH+BASE_LABEL_WIDTH, 
				STAT_STARTING_POS+(cnt*LINE_HEIGHT), 
				BUTTON_WIDTH, 
				BUTTON_HEIGHT), "+")){ // Add Button
				if(_pointsLeft > 0){
					_toon.getPrimaryAttribute(cnt).BaseValue++;
					_pointsLeft--;
					_toon.StatUpdate();
				}
			}
			if(GUI.Button(new Rect(OFFSET+STAT_LABEL_WIDTH+BASE_LABEL_WIDTH+BUTTON_WIDTH, 
				STAT_STARTING_POS+(cnt*LINE_HEIGHT), 
				BUTTON_WIDTH, 
				BUTTON_HEIGHT), "-")){// Minus Button
				if(_toon.getPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE){
					_toon.getPrimaryAttribute(cnt).BaseValue--;
					_pointsLeft++;
					_toon.StatUpdate();
				}
				
			}
		}
	}
	
	private void displayVitals(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++  ){ // loop through the player Vitals
			GUI.Label(new Rect(OFFSET, 
				STAT_STARTING_POS+((cnt+7)*LINE_HEIGHT), 
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT), ((VitalName)cnt).ToString()); // Vital Name
			GUI.Label(new Rect(STAT_LABEL_WIDTH+OFFSET, STAT_STARTING_POS+((cnt+7)*LINE_HEIGHT), BASE_LABEL_WIDTH, LINE_HEIGHT), _toon.getVital(cnt).AdjustedValue.ToString()); // Vital Value
		}
	}
	
	private void displaySkills(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++  ){ // loop through the player Skills
			GUI.Label(new Rect(OFFSET*5+STAT_LABEL_WIDTH+BASE_LABEL_WIDTH+BUTTON_WIDTH*2, 
				STAT_STARTING_POS+(cnt*LINE_HEIGHT), 
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT), ((SkillName)cnt).ToString()); // Skill Name
			
			GUI.Label(new Rect(OFFSET*5+STAT_LABEL_WIDTH+BASE_LABEL_WIDTH+BUTTON_WIDTH*2+OFFSET+STAT_LABEL_WIDTH, 
				STAT_STARTING_POS+(cnt*LINE_HEIGHT), 
				30, 
				LINE_HEIGHT), _toon.getSkill(cnt).AdjustedValue.ToString()); // Skill Value
		}
	}
	
	private void displayRemainingPoints(){
		GUI.Label(new Rect(275, 10, 100, 25), "Points Left: "+_pointsLeft.ToString());
	}
	
	private void displayCreateButton(){
		if(GUI.Button(new Rect(Screen.width/2-BUTTON_WIDTH*5, 
			STAT_STARTING_POS+(Enum.GetValues(typeof(SkillName)).Length*LINE_HEIGHT)+OFFSET*2, 
			BUTTON_WIDTH*5, 
			BUTTON_HEIGHT), "Create")){
			
			//Change Curr value of Vitals
			
			//Save the character
			GameObject gs = GameObject.Find("GameSettings");
			gameSettings gsScript = gs.GetComponent<gameSettings>();
			
			UpdateCurrentVitalValues();
			gsScript.SaveCharacterData();
			
			// Go to the first game level
			Application.LoadLevel(1);	
			
		};
	}
	private void displayCreateLabel(){
		GUI.Label(new Rect(Screen.width/2-BUTTON_WIDTH*5, 
			STAT_STARTING_POS+(Enum.GetValues(typeof(SkillName)).Length*LINE_HEIGHT)+OFFSET*2, 
			BUTTON_WIDTH*5, 
			BUTTON_HEIGHT),
			"Creating...", "Button");
	}
	
	private void UpdateCurrentVitalValues(){
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			_toon.getVital(cnt).CurrentValue = _toon.getVital(cnt).AdjustedValue;	
		}
	}
}
