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



	// Update is called once per frame
	void Update () {
		if (ai_turn) {
			Invoke ("Play_Past_Notes", 2f);
			Invoke("AI_MakeSound", 4f);
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

	IEnumerator Wait() {
		yield return new WaitForSeconds(1);
	}

	void Play_Past_Notes() {
		//should not be called on first round. (Checked: good)
		if (ai_order.Count != 0) {
			foreach (int eachNum in ai_order) {
				StartCoroutine (Wait ());
				Debug.Log ("Currently in ai_order: " + eachNum.ToString ());
				if (eachNum == 1) {
					Invoke ("Play_Note_Delayed1", 2f);
					Debug.Log ("Playing A");
				} else if (eachNum == 2) {
					Invoke ("Play_Note_Delayed2", 2f);
					Debug.Log ("Playing B");
				} else if (eachNum == 3) {
					Invoke ("Play_Note_Delayed3", 2f);
					Debug.Log ("Playing C");
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
	//For some reason, the first time this method is called, it is playing all 3 notes at the same time... I think?
	void AI_MakeSound() {
		rand = UnityEngine.Random.Range (1, 4);
		Debug.Log ("rand = " + rand.ToString ());
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

	//Play note given number
	void Play_Note(int num) {
		if (num == 1) {
			buttonA_audio.Play ();
		} else if (num == 2) {
			buttonB_audio.Play ();
		} else if (num == 3) {
			buttonC_audio.Play ();
		}
	}

	//Play method used in invoke
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
