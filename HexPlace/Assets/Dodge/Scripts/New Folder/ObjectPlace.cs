using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace MagicKit
{

    public class ObjectPlace : MonoBehaviour
    {
        private SpawnPoint _spawnPoint;
        // Use this for initialization
        void Start()
        {
            GetSpawnPoint(ref _spawnPoint, this.transform);
        }

        //// Update is called once per frame
        //void Update()
        //{

        //}
        private bool GetSpawnPoint(ref SpawnPoint spawnPoint, Transform occupant)
        {
            // Finds the spawn point that is the furthest from the player and currently unoccupied.
            List<SpawnPoint> availableSpawnPoints = new List<SpawnPoint>();
            foreach (var sp in SpawnPointManager.Instance.spawnPoints)
            {
                if (sp.Available && sp != spawnPoint)
                {
                    availableSpawnPoints.Add(sp);
                }
            }
            if (availableSpawnPoints.Count > 0)
            {
                
                if (availableSpawnPoints[0] != null)
                {
                    if (availableSpawnPoints[0].RequestSpawnPoint(occupant))
                    {
                        if (spawnPoint != null)
                        {
                            spawnPoint.ReleaseSpawnPoint();
                        }
                        spawnPoint = availableSpawnPoints[0];
                        return true;
                    }
                }
                else if (spawnPoint != null)
                {
                    // If there are no other valid spawn points, re-select the current one.
                    spawnPoint.RequestSpawnPoint(occupant);
                    return true;
                }
            }
            else if (spawnPoint != null)
            {
                // If there are no other available spawn points, re-select the current one.
                spawnPoint.RequestSpawnPoint(occupant);
                return true;
            }
            return false;
        }
    }
}