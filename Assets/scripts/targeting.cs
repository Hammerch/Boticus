using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class targeting : MonoBehaviour {
	public List<Transform> targets;
	private Transform selectedTarget;
	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		targets = new List<Transform>();
		selectedTarget = null;
		AddAllEnemies();
		myTransform = transform;
	}
	
	//Get all enemey targets
	public void AddAllEnemies(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject enemy in enemies){
			AddTarget(enemy.transform);
		}
	}
	
	public void AddTarget(Transform enemy){
		targets.Add(enemy);
	}
	
	private void TargetEnemy(){
		if(selectedTarget == null){
			sortTargetsByDistance();
			selectedTarget = targets[0];
		}
		else{
			int index = targets.IndexOf(selectedTarget);
			if(index<targets.Count-1){
				index++;
			}
			else{
				index = 0;
			}
			deselectTarget();
			selectedTarget = targets[index];
		}
		selectTarget();
	}
	
	private void sortTargetsByDistance(){
		targets.Sort(delegate(Transform t1, Transform t2){ 
			return (Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position)));
		});
	}
	
	private void selectTarget(){
		/// Hilight the selected enemy.... Halo?
		PlayerAttack pa = (PlayerAttack)GetComponent("PlayerAttack");
		pa.target = selectedTarget.gameObject;
	}
	
	private void deselectTarget(){
		/// unHighlight the enemy when no longer selected.
		selectedTarget = null;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			TargetEnemy();
		}
	}
}
