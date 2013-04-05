using UnityEngine;
using System.Collections;
using System;		// Added to access the enum class

public class BaseCharacter:MonoBehaviour  {
	private string _name;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vitals[] _vital;
	private Skill[] _skill;
	
	public void Awake(){
		_name = String.Empty;
		_level = 0;
		_freeExp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vitals[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		setupAttributes();
		setupVitals();
		setupSkills();
		
	}
	
	#region setters and getters
	public string Name{
		get{ return _name; }
		set{ _name = value; }
	}
	
	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}
	
	public uint FreeExp{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}
	#endregion
	
	public void AddExp(uint exp){
		_freeExp += exp;
		CalculateLevel();
	}
	
	public void CalculateLevel(){
		
	}
	
	private void setupAttributes(){
		for(int cnt=0; cnt<_primaryAttribute.Length; cnt++){
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	
	private void setupVitals(){
		for(int cnt=0; cnt<_vital.Length; cnt++){
			_vital[cnt] = new Vitals();
		}
		setupVitalModifiers();
	}
	
	private void setupSkills(){
		for(int cnt=0; cnt<_skill.Length; cnt++){
			_skill[cnt] = new Skill();
		}
		setupSkillModifiers();
	}
	
	public Attribute getPrimaryAttribute(int index){
		return _primaryAttribute[index];
	}
	
	public Skill getSkill(int index){
		return _skill[index];
	}
	
	public Vitals getVital(int index){
		return _vital[index];
	}
	
	private void setupVitalModifiers(){		
		// Health
		ModifyingAttribute Health1 = new ModifyingAttribute();
		ModifyingAttribute Health2 = new ModifyingAttribute();
		Health1.attribute = getPrimaryAttribute((int)AttributeName.Durability);
		Health1.ratio = .75f;
		Health2.attribute = getPrimaryAttribute((int)AttributeName.Endurance);
		Health2.ratio = .25f;
		
		//getVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(getPrimaryAttribute((int)AttributeName.Durability),.75f));
		getVital((int)VitalName.Health).AddModifier(Health1);
		getVital((int)VitalName.Health).AddModifier(Health2);
		
		// Energy -- Voltage?
		ModifyingAttribute Energy = new ModifyingAttribute();
		Energy.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		Energy.ratio = 0.5f;
		getVital((int)VitalName.Energy).AddModifier(Energy);
		
		// Power -- Horse Power
		ModifyingAttribute Power = new ModifyingAttribute();
		Power.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		Power.ratio = 1.0f;
		getVital((int)VitalName.Power).AddModifier(Power);
	}
	
	private void setupSkillModifiers(){
		#region Setup of the Skills Modifiers
		// stab
		ModifyingAttribute Stab1 = new ModifyingAttribute();
		ModifyingAttribute Stab2 = new ModifyingAttribute();
		ModifyingAttribute Stab3 = new ModifyingAttribute();
		ModifyingAttribute Stab4 = new ModifyingAttribute();
		Stab1.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		Stab2.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		Stab3.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		Stab4.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		Stab1.ratio = 0.25f;
		Stab2.ratio = 0.25f;
		Stab3.ratio = 0.25f;
		Stab4.ratio = 0.25f;
		getSkill((int)SkillName.stab).AddModifier(Stab1);
		getSkill((int)SkillName.stab).AddModifier(Stab2);
		getSkill((int)SkillName.stab).AddModifier(Stab3);
		getSkill((int)SkillName.stab).AddModifier(Stab4);
		
		// slash
		ModifyingAttribute Slash1 = new ModifyingAttribute();
		ModifyingAttribute Slash2 = new ModifyingAttribute();
		ModifyingAttribute Slash3 = new ModifyingAttribute();
		Slash1.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		Slash2.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		Slash3.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		Slash1.ratio = 0.5f;
		Slash2.ratio = 0.25f;
		Slash3.ratio = 0.25f;
		getSkill((int)SkillName.slash).AddModifier(Slash1);
		getSkill((int)SkillName.slash).AddModifier(Slash2);
		getSkill((int)SkillName.slash).AddModifier(Slash3);
		
		// parry
		ModifyingAttribute Parry1 = new ModifyingAttribute();
		ModifyingAttribute Parry2 = new ModifyingAttribute();
		Parry1.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		Parry2.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		Parry1.ratio = 0.75f;
		Parry2.ratio = 0.25f;
		getSkill((int)SkillName.parry).AddModifier(Parry1);
		getSkill((int)SkillName.parry).AddModifier(Parry1);
		
		// throwing
		ModifyingAttribute Throw1 = new ModifyingAttribute();
		ModifyingAttribute Throw2 = new ModifyingAttribute();
		Throw1.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		Throw2.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		Throw1.ratio = 0.75f;
		Throw2.ratio = 0.25f;
		getSkill((int)SkillName.throwing).AddModifier(Throw1);
		getSkill((int)SkillName.throwing).AddModifier(Throw2);
		
		// kick
		ModifyingAttribute Kick1 = new ModifyingAttribute();
		ModifyingAttribute Kick2 = new ModifyingAttribute();
		ModifyingAttribute Kick3 = new ModifyingAttribute();
		Kick1.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		Kick2.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		Kick3.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		Kick1.ratio = 0.5f;
		Kick2.ratio = 0.25f;
		Kick3.ratio = 0.25f;
		getSkill((int)SkillName.kick).AddModifier(Kick1);
		getSkill((int)SkillName.kick).AddModifier(Kick2);
		getSkill((int)SkillName.kick).AddModifier(Kick3);
		
		// jump_kick
		ModifyingAttribute JumpKick1 = new ModifyingAttribute();
		ModifyingAttribute JumpKick2 = new ModifyingAttribute();
		ModifyingAttribute JumpKick3 = new ModifyingAttribute();
		ModifyingAttribute JumpKick4 = new ModifyingAttribute();
		JumpKick1.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		JumpKick2.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		JumpKick3.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		JumpKick4.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		JumpKick1.ratio = 0.3f;
		JumpKick2.ratio = 0.2f;
		JumpKick3.ratio = 0.3f;
		JumpKick4.ratio = 0.2f;
		getSkill((int)SkillName.jump_kick).AddModifier(JumpKick1);
		getSkill((int)SkillName.jump_kick).AddModifier(JumpKick2);
		getSkill((int)SkillName.jump_kick).AddModifier(JumpKick3);
		getSkill((int)SkillName.jump_kick).AddModifier(JumpKick4);
		
		// spin_slash
		ModifyingAttribute SpinSlash1 = new ModifyingAttribute();
		ModifyingAttribute SpinSlash2 = new ModifyingAttribute();
		ModifyingAttribute SpinSlash3 = new ModifyingAttribute();
		ModifyingAttribute SpinSlash4 = new ModifyingAttribute();
		SpinSlash1.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		SpinSlash2.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		SpinSlash3.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		SpinSlash4.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		SpinSlash1.ratio = 0.2f;
		SpinSlash2.ratio = 0.2f;
		SpinSlash3.ratio = 0.4f;
		SpinSlash4.ratio = 0.2f;
		getSkill((int)SkillName.spin_slash).AddModifier(SpinSlash1);
		getSkill((int)SkillName.spin_slash).AddModifier(SpinSlash2);
		getSkill((int)SkillName.spin_slash).AddModifier(SpinSlash3);
		getSkill((int)SkillName.spin_slash).AddModifier(SpinSlash4);
		
		// whirlwind
		ModifyingAttribute WhirlWind1 = new ModifyingAttribute();
		ModifyingAttribute WhirlWind2 = new ModifyingAttribute();
		ModifyingAttribute WhirlWind3 = new ModifyingAttribute();
		ModifyingAttribute WhirlWind4 = new ModifyingAttribute();
		WhirlWind1.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		WhirlWind2.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		WhirlWind3.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		WhirlWind4.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		WhirlWind1.ratio = 0.3f;
		WhirlWind2.ratio = 0.3f;
		WhirlWind3.ratio = 0.3f;
		WhirlWind4.ratio = 0.1f;
		getSkill((int)SkillName.whirlwind).AddModifier(WhirlWind1);
		getSkill((int)SkillName.whirlwind).AddModifier(WhirlWind2);
		getSkill((int)SkillName.whirlwind).AddModifier(WhirlWind3);
		getSkill((int)SkillName.whirlwind).AddModifier(WhirlWind4);
		
		// block
		ModifyingAttribute Block1 = new ModifyingAttribute();
		ModifyingAttribute Block2 = new ModifyingAttribute();
		Block1.attribute = getPrimaryAttribute((int)AttributeName.Durability);
		Block2.attribute = getPrimaryAttribute((int)AttributeName.Endurance);
		Block1.ratio = 0.75f;
		Block2.ratio = 0.25f;
		getSkill((int)SkillName.block).AddModifier(Block1);
		getSkill((int)SkillName.block).AddModifier(Block2);
		
		// block_attack
		ModifyingAttribute BlockAttack1 = new ModifyingAttribute();
		ModifyingAttribute BlockAttack2 = new ModifyingAttribute();
		ModifyingAttribute BlockAttack3 = new ModifyingAttribute();
		ModifyingAttribute BlockAttack4 = new ModifyingAttribute();
		BlockAttack1.attribute = getPrimaryAttribute((int)AttributeName.Speed);
		BlockAttack2.attribute = getPrimaryAttribute((int)AttributeName.Agility);
		BlockAttack3.attribute = getPrimaryAttribute((int)AttributeName.Durability);
		BlockAttack4.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		BlockAttack1.ratio = 0.3f;
		BlockAttack2.ratio = 0.3f;
		BlockAttack3.ratio = 0.3f;
		BlockAttack4.ratio = 0.1f;
		getSkill((int)SkillName.block_attack).AddModifier(BlockAttack1);
		getSkill((int)SkillName.block_attack).AddModifier(BlockAttack2);
		getSkill((int)SkillName.block_attack).AddModifier(BlockAttack3);
		getSkill((int)SkillName.block_attack).AddModifier(BlockAttack4);
		
		// mega_shove
		ModifyingAttribute Shove1 = new ModifyingAttribute();
		ModifyingAttribute Shove2 = new ModifyingAttribute();
		Shove1.attribute = getPrimaryAttribute((int)AttributeName.Strength);
		Shove2.attribute = getPrimaryAttribute((int)AttributeName.Capacity);
		Shove1.ratio = 0.75f;
		Shove2.ratio = 0.25f;
		getSkill((int)SkillName.mega_shove).AddModifier(Shove1);
		getSkill((int)SkillName.mega_shove).AddModifier(Shove2);
		#endregion
	}
	
	public void StatUpdate(){
		for(int cnt = 0; cnt < _vital.Length; cnt++){
			_vital[cnt].Update();
		}
		
		for(int cnt = 0; cnt < _skill.Length; cnt++){
			_skill[cnt].Update();
		}
	}
}
