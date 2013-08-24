using UnityEngine;
using System.Collections.Generic;

public class GameManagerBehaviour : Singleton<GameManagerBehaviour>
{
	private bool[] _correct;
	private bool[] _guesses;
	private bool _win;
	private int _curIdx;
	private TimerBehaviour _timer;
	private string[] _questions;
	private GUIText _questionField;
	private int _sideMargins; //in pixels
	private bool _mainInit;
	private bool _answeredAll;
	
	private void Start()
	{
		GameObject.DontDestroyOnLoad(this.gameObject);
		GUIText intro = ((GameObject)GameObject.FindGameObjectWithTag("intro_text")).GetComponent<GUIText>();
		intro.text = 
				"Please remain calm as any outbursts in the test\n" +
				"environment will be viewed unfavorably.\n" +
				"\n" +
				"You will have ten (10) seconds\n" +
				"to answer each question.\n" +
				"\n" +
				"Successful completion is\n" +
				"mandatory for admittance.\n" +
				"\n" +
				"Press [Enter] when ready";
		_mainInit = false;
	}
	
	private void Update()
	{
		if(Application.loadedLevelName == "opening")
		{
			if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp (KeyCode.KeypadEnter))
				Application.LoadLevel("main_scene");
		}
		else if(Application.loadedLevelName == "main_scene" && !_mainInit)
		{
			InitQuestions();
		}
		else if(Application.loadedLevelName == "result_scene")
		{
			DisplayResults();
		}
	}
	
	public void Clicked(bool yesClicked)
	{
		_guesses[_curIdx] = yesClicked;
		if(_guesses[_curIdx] != _correct[_curIdx])
			_win = false;
		_curIdx++;
		
		if(_curIdx == _correct.Length)
			GameOver();
		else
		{
			_timer.Reset();
			DisplayQuestion();
		}
	}
	
	public void GameOver()
	{
		Application.LoadLevel("result_scene");
	}
	
	private void InitQuestions()
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
		_questionField = ((GameObject)GameObject.FindGameObjectWithTag ("question_field")).GetComponent<GUIText>();
		ShuffleQuestions((TextAsset)Resources.Load("questions"));
		_sideMargins = Mathf.FloorToInt(Screen.width*0.1f);
		DisplayQuestion();
		_mainInit = true;
		_answeredAll = false;
	}
	
	private void ShuffleQuestions(TextAsset src)
	{
		string[] raw = src.text.Split(new char[1]{'\n'});
		List<string> qList = new List<string>();
		qList.AddRange(raw);
		
		int idx = 0;
		_questions = new string[30];
		while(qList.Count > 0)
		{
			int rnd = Random.Range(0, qList.Count);
			_questions[idx++] = qList[rnd];
			qList.RemoveAt(rnd);
		}
	}
	
	private void WrapLine(string line)
	{
		string[] words = line.Split(new char[1]{' '});
		string res = "";
		for(int i=0;i<words.Length;++i)
		{
			string word = words[i].Trim();
			if(i == 0)
			{
				res = words[i];
				_questionField.text = res;
			}
			else
			{
				res += " " + word;
				_questionField.text = res;
			}
			
			Rect fieldSize = _questionField.GetScreenRect();
			if(fieldSize.width > Screen.width - _sideMargins)
			{
				res = res.Substring(0, res.Length-word.Length);
				res += "\n" + word;
				_questionField.text = res;
			}
		}
	}
	
	private void DisplayQuestion()
	{
		WrapLine(_questions[_curIdx]);
	}
	
	private void DisplayResults()
	{
		string msg;
		
		if(_win && _answeredAll)
			msg = "You have passed the exam.";
		else
			msg = "You have failed the exam.";
		
		GUIText text = ((GameObject)GameObject.FindGameObjectWithTag("result_text")).GetComponent<GUIText>();
		text.text = msg;
	}
}
