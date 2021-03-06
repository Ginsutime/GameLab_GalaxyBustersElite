﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehavior contains Instantiate fucntion
//cannot be static AND monobehavior?
public class PoolUtility : MonoBehaviour
{
    /// <summary> Instantiates Projectiles from Pool
    /// <para>  Takes most recent inactive reference in hierarchy to set active and enable. If no availalbe inactive reference, instantiates a new reference and enables. </para>
    /// <para>   Abstracted to work with Any projectile type. </para>
    /// </summary>
    /// <param name="pool"> Pool of references in hierarchy </param>
    /// <param name="spawnPoint"> Transform of position and direction to fire projectile from </param>
    /// <param name="projectileReference"> Projectile Prefab to instantiate, if needed </param>
    public static GameObject InstantiateFromPool(List<GameObject> pool, Transform spawnPoint, GameObject projectileReference)
    {
        //Static Function to call from any script

        //First checks for any available, inactive references that already exist
        foreach (GameObject projectile in pool)
        {
            if (projectile != null && projectile.activeInHierarchy == false)
            {
                //Enables and positions projectile, to reuse
                projectile.SetActive(true);
                projectile.transform.position = spawnPoint.position;
                projectile.transform.rotation = spawnPoint.rotation;
                return projectile;
            }
        }

        //Worst Case Scenario, instantiates for current and future use
        GameObject newProjectile = Instantiate(projectileReference, spawnPoint.position, spawnPoint.rotation, null);
        pool.Add(newProjectile);
        return newProjectile;
    }
}