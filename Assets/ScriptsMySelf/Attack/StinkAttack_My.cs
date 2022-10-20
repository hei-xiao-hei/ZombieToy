//此脚本处理臭味攻击。恶臭攻击是一种效果范围（AoE）攻击，需要较长的冷却时间。开火后，这次攻击

//发射一枚会爆炸并以半径内的所有敌人为目标的炮弹，迫使他们逃跑。仅此攻击

//释放触发器时触发。
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StinkAttack_My : MonoBehaviour
{
	[Header("Weapon Specs")]
	public float Cooldown = 5f;                             //攻击冷却时间

	[SerializeField] float range = 5f;                      //攻击范围

	[Header("Weapon References")]
	[SerializeField] StinkProjectile_My stinkProjectile;     
	[SerializeField] Renderer targetReticule;               

	[Header("Reticule Colors")]
	[SerializeField] Color invalidTargetTint = Color.red;   //攻击目标错误的颜色      
	[SerializeField] Color notReadyTint = Color.yellow;     //攻击未准备好的颜色        
	[SerializeField] Color readyTint = Color.green;         //攻击准备好的颜色

	float timeOfLastAttack = -10f;                          
	Vector3 targetPosition;                                 //目标
	bool inRange = false;                                   //是否在范围内

	
	public void Fire()
	{
		//If the target is in range, launch the projectile
		if (inRange)
			LaunchProjectile();
	}

	void Update()
	{
		//Assume the target isn't in range
		inRange = false;
		//If we don't have a MouseLocation script in the scene or if the position of the mouse isn't valid, leave Update()
		if (MouseLocation_My.Instance == null || !MouseLocation_My.Instance.IsValid)
			return;

		//Grab the current position of the mouse
		targetPosition = MouseLocation_My.Instance.MousePosition;
		//Find the distance between the mouse and the player
		float distance = Vector3.Distance(targetPosition, transform.position);
		//If the distance is smaller than the range, then the attack is in range
		if (distance <= range)
			inRange = true;

		UpdateReticule();
	}

	//This method updates the position and color of the reticule
	void UpdateReticule()
	{
		//Place the reticule where the mouse is
		targetReticule.transform.position = targetPosition;

		//If the attack isn't in range, set invalid tint
		if (!inRange)
			targetReticule.material.SetColor("_TintColor", invalidTargetTint);
		//If attack is on cooldown, set not ready tint
		else if (timeOfLastAttack + Cooldown > Time.time)
			targetReticule.material.SetColor("_TintColor", notReadyTint);
		//Otherwise, set ready tint
		else
			targetReticule.material.SetColor("_TintColor", readyTint);
	}

	//This method launches the projectile
	void LaunchProjectile()
	{
		//record the time of the attack
		timeOfLastAttack = Time.time;
		//Turn the projectile on
		stinkProjectile.gameObject.SetActive(true);
		//Send the projectile on its way towards the target
		stinkProjectile.StartPath(transform.position, targetPosition);
	}
}
