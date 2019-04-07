using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractTutorial : MonoBehaviour {

    public GameObject Player;
    public Text TutorialText;
    public bool shouldShow;
    public float Timer;
    public string textString;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Vector3.Distance(Player.transform.position, transform.position) <= 10.0f)
        {
            TutorialText.text = textString;
            shouldShow = false;
        }
        if (!shouldShow)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                TutorialText.text = "";
                Destroy(gameObject);
            }
        }
	}
}
