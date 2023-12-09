using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCam : MonoBehaviour
{

    [Range(1.0f, 360.0f)]
    public float angleVu = 30.5f;

    [Range(2.0f, 50f)]
    public float distanceVu = 10f;

    Vector3 playerDirection;

    Vector3 Target;


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
        playerDirection = (Target - transform.GetChild(0).position).normalized;

        transform.Rotate(Vector3.up, 0.5f);

        //Si D(Joueur, Toupie) < X
        if (Vector3.Distance(transform.transform.GetChild(0).position, Target) < distanceVu)
        {


            //Si le joueur entre dans l'angle de vu de l'ennemi
            if (Vector3.Angle(transform.transform.GetChild(0).forward, playerDirection) < angleVu / 2.0f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.transform.GetChild(0).position, playerDirection, out hit))
                {
                    if (hit.transform == GameObject.Find("Receiver").transform)
                    {
                        Debug.DrawLine(transform.transform.GetChild(0).position, Target, Color.green);
                        print("joueur detecté");
                    }


                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Dessine l'angle de détéction
        float halfFOV = angleVu / 2.0f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.transform.GetChild(0).forward;
        Gizmos.DrawRay(transform.transform.GetChild(0).position, leftRayDirection * distanceVu);
        Gizmos.DrawRay(transform.transform.GetChild(0).position, rightRayDirection * distanceVu);

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.transform.GetChild(0).position, distanceVu);
       
    }

    
}
