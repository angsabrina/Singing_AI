using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Play_Sounds : MonoBehaviour {
	public AudioSource buttonA_audio;
	public AudioSource buttonB_audio;
	public AudioSource buttonC_audio;

	bool ai_turn = true;
	int rand;
	List<int> ai_order = new List<int> ();
	List<int> player_order = new List<int>();


	void Start() {
		List<String> buttonHolder = new List<String>();
		buttonHolder.Add("ButtonA");
		buttonHolder.Add("ButtonB");
		buttonHolder.Add("ButtonC");
	}
	// Update is called once per frame

	//need to tell player their order was correct and make it go back to ai turn
	void Update () {
		if (ai_turn) {
			AI_MakeSound ();
			ai_turn = false;
			player_order = new List<int>();
		} else {
			if (Input.GetMouseButtonDown (0)) {
				MakeSound ();
				int compareNum = player_order.Count - 1;
				if ((player_order [compareNum]) != ai_order [compareNum]) {
					Debug.Log ("You lose!");
				}
				if (player_order.Count == ai_order.Count) {
					Debug.Log ("You win! Let's make it harder!");
					ai_turn = true;
				}
			}







		}
	}

	//Makes sound when player clicks on object
	void MakeSound() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.name == "ButtonA") {
				Debug.Log ("Hit button A");
				player_order.Add (1);
				Play_Note (1);
			}
			else if (hit.transform.name == "ButtonB") {
				Debug.Log ("Hit button B");
				player_order.Add (2);
				Play_Note (2);
			}
			else if (hit.transform.name == "ButtonC") {
				Debug.Log ("Hit button C");
				player_order.Add (3);
				Play_Note (3);
			}
		}
	}

	//AI randomizes which sound it will make next
	void AI_MakeSound() {
		rand = UnityEngine.Random.Range (1, 4);

		if (ai_order.Count != 0) {
			foreach (int eachNum in ai_order) {
				Debug.Log ("Currently in ai_order: " + eachNum.ToString ());
				if (eachNum == 1) {
					Invoke ("Play_Note_Delayed1", 2f);
				} else if (eachNum == 2) {
					Invoke ("Play_Note_Delayed2", 2f);
				} else if (eachNum == 3) {
					Invoke ("Play_Note_Delayed3", 2f);
				}

			}
		}
		if (rand == 1) {
			Debug.Log ("AI played: buttonA");
			Play_Note_Delayed1();
		} else if (rand == 2) {
			Debug.Log ("AI played: buttonB");
			Play_Note_Delayed2();
		} else if (rand == 3) {
			Debug.Log ("AI played: buttonC");
			Play_Note_Delayed3();
		}
		ai_order.Add (rand);
	}

	void Play_Note(int num) {
		if (num == 1) {
			buttonA_audio.Play ();
		} else if (num == 2) {
			buttonB_audio.Play ();
		} else if (num == 3) {
			buttonC_audio.Play ();
		}
	}

	void Play_Note_Delayed1() {
		buttonA_audio.Play ();
	}
	void Play_Note_Delayed2() {
		buttonB_audio.Play ();
	}
	void Play_Note_Delayed3() {
		buttonC_audio.Play ();
	}
		

}
