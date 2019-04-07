using UnityEngine;
using System.Collections;

public class SirenEffect : MonoBehaviour
{

    public Light redLight;
    public Light blueLight;

    private Vector3 redLightTemp;
    private Vector3 blueLightTemp;

    public float speed;

    public int isClockwise;

    private void Awake()
    {
        //Setting the x rotation for the light
        redLightTemp.x = redLight.transform.eulerAngles.x;
        blueLightTemp.x = blueLight.transform.eulerAngles.x;

        //Setting the y rotation for the light
        redLightTemp.y = redLight.transform.eulerAngles.y;
        blueLightTemp.y = blueLight.transform.eulerAngles.y;

        speed = Random.Range(135.0f, 200.0f);

        isClockwise = Random.Range(0, 2);
        //reverse the speed 
        if (isClockwise == 0)
            speed *= -1;
    }

    void FixedUpdate()
    {
        redLightTemp.y += speed * Time.deltaTime;
        blueLightTemp.y += speed * Time.deltaTime;

        redLight.transform.eulerAngles = redLightTemp;
        blueLight.transform.eulerAngles = blueLightTemp;
    }
}
