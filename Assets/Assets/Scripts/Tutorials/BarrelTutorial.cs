using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrelTutorial : MonoBehaviour {

    public GameObject Player;
    public Text TutorialText;
    public bool shouldShow;
    public float Timer;
    public GameObject Camera;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) <= 10.0f)
        {
            TutorialText.text = "Shoot those barrels so they explode!";
            Camera.SetActive(true);
            shouldShow = false;
        }
        if (!shouldShow)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                Camera.SetActive(false);
                TutorialText.text = "";
                Destroy(gameObject);
            }
        }
    }
}
