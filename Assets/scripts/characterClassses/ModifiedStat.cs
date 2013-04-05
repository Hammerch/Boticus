using System.Collections.Generic;
using UnityEngine;

public class ModifiedStat : BaseStat {
	private List<ModifyingAttribute> _mods; // A list of Attributes that modify this stat
	private int _modValue; // The amount added to the base value of the modifiers
	
	public ModifiedStat(){
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	public void AddModifier(ModifyingAttribute mod){
		_mods.Add(mod);
	}
	
	private void CalculateModValue(){
		_modValue = 0;
		if(_mods.Count>0){
			foreach(ModifyingAttribute att in _mods){
				_modValue += (int)(att.attribute.AdjustedValue * att.ratio);
			}
		}
	}
	
	public new int AdjustedValue {
		get{return BaseValue + BuffValue + _modValue; }
	}
	
	public void Update(){
		CalculateModValue();
	}
	
	public string GetModifyingAttributeString(){
		string temp = "";
		for(int cnt=0; cnt<_mods.Count; cnt++){
			temp += _mods[cnt].attribute.Name;
			temp += ",";
			temp += _mods[cnt].ratio;
			
			if(cnt<_mods.Count-1){
				temp += "|";
			}
		}
		return temp;
	}
}

public struct ModifyingAttribute{
	public Attribute attribute;
	public float ratio;	
}