public class Vitals : ModifiedStat {
	private int _currentValue;
	
	public Vitals(){
		_currentValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}
	
	public int CurrentValue{
		get{ 
			if( _currentValue > AdjustedValue ){
				_currentValue = AdjustedValue;
			}
			return _currentValue; 
		}
		set{ _currentValue = value; }
	}
}
public enum VitalName{
	Health,
	Energy,
	Power
	
}