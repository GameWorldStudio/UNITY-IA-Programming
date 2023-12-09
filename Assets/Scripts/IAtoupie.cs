using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class IAtoupie : MonoBehaviour
{
    /*

    private RaycastHit Hit;

    private float Vitesse = 0.9f;

    public bool Detection = false;

    public Transform Target;

    public CharacterController controller;

    public NavMeshAgent Agent;

    private float backValue = 23.94861f;
    private float newValue = 175f;

    static public float degat = 35;

   // private playerStats player;

    public CapsuleCollider col;

    public Image uiDetec;

    //A rajouter : Trouver Target automatiquement

    void Awake()
    {
       // player = GameObject.FindObjectOfType<playerStats>();
       // Agent = gameObject.GetComponent<NavMeshAgent>();
      //  CapsuleCollider col = gameObject.GetComponent<CapsuleCollider>();
    }

	
	// Update is called once per frame
	void Update ()
    {

        if(!Detection)
        {
            Agent.enabled = false;

            transform.Translate(Vector3.forward * Vitesse * Time.deltaTime);
            /*
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 0.5f))
            {
                transform.Rotate(Vector3.up * Random.Range(50, 200));
            }
            */
   //     }

     //   if (Agent.enabled)
       // {
         //   Agent.destination = Target.position;
            /*
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 0.5f))
            {
                transform.Rotate(Vector3.up * Random.Range(50, 200));
            }
            *//*
        }
    }

    /*
    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            Detection = true;
            Agent.enabled = true;
            col.radius = newValue;
            uiDetec.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            Detection = false;
            col.radius = backValue;
            uiDetec.GetComponent<CanvasGroup>().alpha = 0;
        }
       
       
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (player.armPoint != 0 && (player.armPoint - degat) <= 0)
            {
                float reste = player.armPoint - degat;
                player.armPoint = 0;
                player.lifePoint = player.lifePoint + reste;

            }
            else if (player.armPoint != 0 && player.armPoint > degat)
            {
                player.armPoint -= degat;
            }
            else if (player.armPoint == 0)
            {
                player.lifePoint -= degat;

            }
            Destroy(gameObject);
            uiDetec.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "AP")
        {
            uiDetec.GetComponent<CanvasGroup>().alpha = 0;
            Destroy(gameObject);
        }
        
    }
    */
}
