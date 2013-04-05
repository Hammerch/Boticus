using UnityEngine;
using System.Collections;
using System;

public class gameSettings : MonoBehaviour {
	
	void Awake(){
		DontDestroyOnLoad(this);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SaveCharacterData(){
		GameObject pc = GameObject.Find("pc");
		PlayerCharacter pcClass = pc.GetComponent<PlayerCharacter>();
		
		PlayerPrefs.SetString("PlayerName", pcClass.Name);
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++){
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString()+" - Base Value",pcClass.getPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString()+" - Exp to Level",pcClass.getPrimaryAttribute(cnt).ExpToLevel);

		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++){
			PlayerPrefs.SetInt(((VitalName)cnt).ToString()+" - Base Value",pcClass.getVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString()+" - Exp to Level",pcClass.getVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString()+" - Curr Value",pcClass.getVital(cnt).CurrentValue);
			
			PlayerPrefs.SetString(((VitalName)cnt).ToString()+" - Mods",pcClass.getVital(cnt).GetModifyingAttributeString());
		}
		
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++){
			PlayerPrefs.SetInt(((SkillName)cnt).ToString()+" - Base Value",pcClass.getSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString()+" - Exp to Level",pcClass.getSkill(cnt).ExpToLevel);
			
			PlayerPrefs.SetString(((SkillName)cnt).ToString()+" - Mods",pcClass.getSkill(cnt).GetModifyingAttributeString());
		}
	}
	
	public void LoadCharacterData(){
		
	}
}
