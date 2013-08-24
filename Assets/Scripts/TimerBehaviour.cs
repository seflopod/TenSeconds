using UnityEngine;
using System.Collections;

public class TimerBehaviour : MonoBehaviour {
	private float _timeRemaining;
	private GUIText _guiText;
	// Use this for initialization
	void Start ()
	{
		_timeRemaining = 10.0f;
		_guiText = gameObject.GetComponent<GUIText>();
	}
	
	void FixedUpdate()
	{
		_timeRemaining-=Time.fixedDeltaTime;
		int dispTime = Mathf.CeilToInt(_timeRemaining);
		_guiText.text = string.Format("0:{0:00}",dispTime);
		if(_timeRemaining <= 0.0f)
			GameManagerBehaviour.Instance.GameOver();
	}
	
	public void Reset()
	{
		_timeRemaining = 10.0f;
	}
}
