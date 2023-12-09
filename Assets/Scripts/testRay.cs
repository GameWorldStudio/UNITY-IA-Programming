using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRay : MonoBehaviour
{

    Vector3 Target;

    [Range(1f, 360f)] public float AngleVu = 30f;
    [Range(1f, 100f)] public float distanceVu = 10f;
    [Range(1f, 500f)] public float distancePoursuite;

    Vector3 playerDirection;

    [HideInInspector] public bool detection = false;



    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Receiver").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //On met à jour les coordonnées utilitaires des objets
        Target = GameObject.Find("Receiver").transform.position;
        playerDirection = (Target - transform.position).normalized;

        

        //Si D(Joueur, Toupie) < X
        if(Vector3.Distance(transform.position, Target) < distanceVu)
        {
            //Si le joueur entre dans l'angle de vu de l'ennemi
            if (Vector3.Angle(transform.forward, playerDirection) < AngleVu / 2.0f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, playerDirection, out hit))
                {            
                    if (hit.transform == GameObject.Find("Receiver").transform)
                    {
                        Debug.DrawLine(transform.position, Target, Color.green);
                        detection = true;
                        print("joueur detecté");
                    }
                }
            }
        }
        else if(Vector3.Distance(transform.position, Target) > distancePoursuite)
        {
            detection = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Dessine l'angle de détéction
        float halfFOV = AngleVu / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * distanceVu);
        Gizmos.DrawRay(transform.position, rightRayDirection * distanceVu);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, distanceVu);
        Gizmos.DrawWireSphere(transform.position, distancePoursuite);
    }

}
