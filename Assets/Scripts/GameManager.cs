using UnityEngine;
using System.Collections;

public class GameManager
{
	private static GameManager _instance;
	
	public static GameManager Instance
	{
		get
		{
			if(_instance == null)
				_instance = new GameManager();
			return _instance;
		}
	}
	
	private bool[] _correct;
	private bool[] _guesses;
	private bool _win;
	private int _curIdx;
	private TimerBehaviour _timer;
	private string[] _questions;
	
	private GameManager()
	{
		_correct = new bool[30]{true, true, false, false, true,
								true, false, true, false, true,
								true, false, false, false, true,
								false, false, false, true, true,
								false, false, true, true, true,
								true, false, false, true, true };
		_guesses = new bool[30];
		_win = true;
		_curIdx = 0;
		_timer = ((GameObject)GameObject.FindGameObjectWithTag("timer")).GetComponent<TimerBehaviour>();
		TextAsset src = (TextAsset)Resources.Load("questions");
		_questions = src.text.Split("\n");
		
	}
	
	public void Clicked(bool yesClicked)
	{
		_guesses[_curIdx] = yesClicked;
		if(!(_guesses[_curIdx] && _correct[_curIdx]))
			_win = false;
		_curIdx++;
		if(_curIdx == 2)
			GameOver(true);
		else
			_timer.Reset();
	}
	
	public void GameOver(bool answeredAll)
	{
		string msg;
		if(_win && answeredAll)
			msg = "Winner!";
		else
			msg = "Loser!";
		Debug.Log(msg);
	}
	
	private void ShuffleQuestions()
	{
		
	}
}
