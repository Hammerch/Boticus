using UnityEngine;
public class BaseStat:MonoBehaviour {
	private int _baseValue; 		// Base Value of the Stat
	private int _buffValue;			// Modified Value of the Stat
	private int _expToLevel;		// Experience needed to get to next level
	private float _levelModifier;	// 0-1 modifier to next level
	
	public BaseStat(){
		_baseValue		= 0;
		_buffValue		= 0;
		_expToLevel 	= 1000;
		_levelModifier 	= 1.1f;	
		
	}
	#region basic getters and setters
	public int BaseValue{
		get{ return _baseValue; }
		set{ _baseValue=value; }
	}
	
	public int BuffValue{
		get{ return _buffValue; }
		set{ _buffValue=value; }
	}
	
	public int ExpToLevel{
		get{ return _expToLevel; }
		set{ _expToLevel=value; }
	}
	
	public float LevelModifier{
		get{ return _levelModifier; }
		set{ _levelModifier=value; }
	}
	#endregion
	
	private int _CalculateExpToLevel(){
		return (int)(_expToLevel*_levelModifier);
	}
	
	public void LevelUp(){
		_expToLevel = _CalculateExpToLevel();
		_baseValue++;	
	}
	
	public int AdjustedValue{
		
		get{return _baseValue + _buffValue; }
		
	}
}
