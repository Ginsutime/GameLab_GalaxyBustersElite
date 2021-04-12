﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeBase : MonoBehaviour
{
	[SerializeField] public bool challengeCompleted = false;
	[SerializeField] public bool challengeFailed = false;
	[SerializeField] public string challengeText;
	[SerializeField] int scoreValue = 0;
	[SerializeField] public float threshold;
	[SerializeField] public float progress = 0;

	private void Update()
	{
		if (GameManager.gm.currentState == GameState.Win && !challengeCompleted && !challengeFailed)
			failure();
	}

	public void victory()
	{
		if (!challengeFailed)
		{
			Debug.Log("<color=green>" + gameObject.name + " Completed!</color>");
			ScoreSystem.IncreaseScoreFlat("Challenge", scoreValue);
			challengeCompleted = true;
		}
	}

	public void failure()
	{
		if (!challengeCompleted)
		{
			Debug.Log("<color=red>" + gameObject.name + " Failed.</color>");
			challengeFailed = true;
		}
	}

	public virtual string GetProgress()
	{
		return progress + "/" + threshold;
	}
}