public class Attribute :BaseStat {
	private string _name;
	public Attribute(){
		int ExpToLevel;
		float LevelModifier;
		_name = "";
		ExpToLevel = 50;
		LevelModifier = 1.5f;
	}
	
	public string Name {
		get{return _name;}
		set{_name = value;}
	}
}

public enum AttributeName{
	Speed,
	Durability,
	Agility,
	Capacity,
	Endurance, 
	Strength
	
}
