using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Apprentissage : MonoBehaviour
{
    /*
       Ce programme a pour but de faire en sorte le l'ia suive un chemin tout tracer, puis, en fonction de nos envie, fait des tour, ou refais le chemin inverse une fois arrivé au bout de sa course.
       A note que lorsque l'ia nous poursuivra, il reconstruira un chemin, celui de la poursuite afin de pouvoir le refaire par après, si jamais on lui échappe.
       Si cela arrive, il effectura automatiquement le chemin en sens sans faire de tour. (Ou ajouter une bool pour lui dire de revenir à son point de départ mais ce n'est pas le but ici)
    */

        /// <summary>
        /// Ce qu'il reste à ajouter -> Suppression des noeux qui n'ont pas été utiliser pour le nouveau chemin 
        /// Ajouter une fonctionnalité très complexe : L'IA est capable d'effectuer une analyse sur chaque point (après reconstruction d'un chemi) et de déterminer si le chemin devant elle n'est pas a empreinté
        /// Ainsi, elle décidera de créer des noeux elle même et de se créer seul un chemin afin de le rendre plus "réaliste"
        /// </summary>
    
    public GameObject WaysPoint; // Liste de tout les noeux que l'ia dois rejoindre *
    public List<GameObject> noeux; 

    NavMeshAgent agent;

    int currentNodes = 0; // Noeux en cours -> Celui que l'ia est en train de rejoindre

    public bool boucle = true; // Si true, parcours son tour en boucle
    bool sens = true; // Si true, va dans le bon sens, sinon va dans le sens inverse, afin de refaire un chemin tracer

    bool detection;

    float CurrentTime = 0;
    float interTime = 5; // Intervalle de temps modulable 
    int i = 0;
    int NbnewNoeux = 0;

    bool peux; // Vérifie si l'ia peux placer un nouveau noeux à sa position actuelle


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        WaysPoint = GameObject.Find("WaysPoints"); // *On stock une variable mais si il y a plusieur garde avec des chemin différents on dois stocker nous même le parcours
        
        for(int i = 0; i < WaysPoint.transform.childCount; i++)
        {
            noeux.Add(WaysPoint.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        detection = GetComponent<testRay>().detection;

        if (!detection)
        {
            if (currentNodes != noeux.Count && sens)
            {
                agent.destination = noeux[currentNodes].transform.position;

                if (Vector3.Distance(gameObject.transform.position, noeux[currentNodes].transform.position) < 2)
                {
                    currentNodes++;
                }
            }
            else
            {
                if (boucle)
                {
                    currentNodes = 0;
                }
                else
                {
                    sens = false;

                    if (currentNodes != 0 && !sens)
                    {
                        agent.destination = noeux[currentNodes - 1].transform.position;
                    

                        if (Vector3.Distance(gameObject.transform.position, noeux[currentNodes - 1].transform.position) < 2)
                        {
                            currentNodes--;

                        }
                    }
                    else
                    {
                        sens = true;
                    }
                }
            }
        }
        else
        {
            currentNodes = noeux.Count;
            boucle = false;
            sens = false;


            agent.destination = GameObject.Find("Receiver").transform.position;

            CurrentTime += Time.deltaTime;

            if(CurrentTime >= interTime)
            {

                peux = Verif();
                CurrentTime = 0;

                if (peux)
                {
                    if (i != noeux.Count - NbnewNoeux)
                    {
/*                        Debug.Log("On déplace le noeux " + i);*/
                        noeux[i].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        i++;
                    }
                    else
                    {
/*                        Debug.Log("On créer un nouveau noeux ");*/
                        var newNoeux = GameObject.Instantiate(noeux[0].gameObject,
                            transform.position,
                            transform.rotation);

                        noeux.Add(newNoeux);
                        NbnewNoeux++;
                    }
                }
                
            }
        }
        
    }

    /// <summary>
    /// Vérifie si chaque noeux est assez loins de l'ia afin de ne pas en recréer assez prêt
    /// </summary>
    /// <returns></returns>
    public bool Verif()
    {
        bool isGood = true;
        RaycastHit Hit;

        for (int i = 0; i < noeux.Count && isGood; i++)
        {
            if (Vector3.Distance(transform.position, noeux[i].transform.position) < 5f)
            {
                if (Physics.Linecast(transform.position, noeux[i].transform.position, out Hit))
                {
                    if(Hit.transform.tag != "Noeux")
                    {
                        isGood = false;
                    }
                    
                }
            }
        }

        return isGood;
    }
}
