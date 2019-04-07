using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    //The animator for this gameobject
    public Animator Animation;

    //the player gameobject
    public GameObject PlayerGameObject;

    public float RoamArea;
    static public float RoamInterval;

    public float RoamTimer;

    public float Speed;

    public float AggroDistance;

    public float AttackRange;

    public float Health;
    public float Damage;

    public float DespawnTimer;

    public bool RoamableZombie;

    //New roam destination
    public Vector3 NewDestination;

    public void Awake()
    {
        
        //float that will dictate when to start roaming.
        RoamInterval = Random.Range(10.0f, 30.0f);

        RoamTimer = RoamInterval;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (Animation.GetBool("isAlive"))
        {
            if (Vector3.Distance(PlayerGameObject.transform.position, gameObject.transform.position) <= AggroDistance)
                Animation.SetBool("isAggro", true);

            if (!Animation.GetBool("isAggro"))
            {
                NotAggroed();
            }
            else
                Aggroed();
        }
        else
        {
            DespawnTimer -= Time.deltaTime;

            if (DespawnTimer <= 0.0f)
                Destroy(gameObject);
        }
	}

    //The main function that will be run w hile the zombie is not aggroed,
    //this will be the function that decides when to roam, and do other stuff
    void NotAggroed()
    {
        if (RoamableZombie)
        {
            RoamTimer -= Time.deltaTime;

            if (RoamTimer <= 0.0f)
            {
                //if roam is false then set it to true
                if (!Animation.GetBool("isRoaming"))
                    Roam(true);

                //if we havent got a new destination
                if (NewDestination == Vector3.zero)
                {
                    //this will set the x and y of newdestination. We only want to move in
                    // the x and z directions so we need to move the values around a bit to get this
                    NewDestination = Random.insideUnitCircle * RoamArea;
                    NewDestination += gameObject.transform.position;
                    NewDestination.z = NewDestination.y;
                    NewDestination.y = 0.0f;
                }

                //Move our zombie towards the newdestination.
                float step = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, NewDestination, step);
                transform.LookAt(NewDestination);

                //if we are basically at our target destination then set roam to false and reset the timer
                if (Vector3.Distance(transform.position, NewDestination) <= 0 && !Animation.GetBool("atDestination"))
                {
                    //Start agonizing animation
                    Agonizing(true);

                    //we reached our destination
                    AtDestination(true);

                    //we are no longer roaming
                    Roam(false);

                    //reset the roam timer
                    RoamTimer = RoamInterval;

                    //reset the destination ready for next one
                    NewDestination = Vector3.zero;
                }
            }
            //Give the timer 2 secs so the animator has enough time to change animation
            else if (RoamTimer <= RoamInterval - 2.0f)
            {
                AtDestination(false);
                Agonizing(false);
            }
        }
    }

    //This is the main function that will be run while the zombie is aggroed
    void Aggroed()
    {
        if (Animation.GetCurrentAnimatorStateInfo(0).IsName("Zombie Scream"))
            return;

        if (Animation.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
        {
            transform.LookAt(PlayerGameObject.transform.position);
            Animation.SetBool("canAttack", false);
            return;
        }

        //Move our zombie towards the newdestination.
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, PlayerGameObject.transform.position, step);
        transform.LookAt(PlayerGameObject.transform.position);

        //if the zombie is close enough to attack the player and its not already attacking the player then attack them.
        if (Vector3.Distance(PlayerGameObject.transform.position, gameObject.transform.position) <= AttackRange && !Animation.GetCurrentAnimatorStateInfo(0).IsName("Zombie Attack"))
        {
            PlayerGameObject.GetComponent<PlayerController>().Health -= 25;
            Animation.SetBool("canAttack", true);
        }
    }

    void AtDestination(bool boolean)
    {
        Animation.SetBool("atDestination", boolean);
    }

    void Roam(bool boolean)
    {
        Animation.SetBool("isRoaming", boolean);
    }

    void Agonizing(bool boolean)
    {
        Animation.SetBool("isAgonizing", boolean);
    }

    public void Hit(float damage)
    {
        Health -= damage;

        if (Health <= 0)
            Dead();
    }

    void Dead()
    {
        Animation.SetBool("isAlive", false);
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
