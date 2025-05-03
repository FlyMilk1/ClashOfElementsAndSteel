using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueMaker : MonoBehaviour
{
    public Unit[] queuedUnits = new Unit[10];
    public Unit[] enemyQueuedUnits = new Unit[10];
    public int levelIndex;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    bool released = false;
    public void ReleaseQueue()
    {
        if(queuedUnits.Length > 0 )
        {
            Debug.Log("Released Queue");
            released = true;
            foreach( Unit unit in queuedUnits )
            {
                if(unit != null)
                {
                    unit.isReleased = true;
                }
                
            }
            foreach ( Unit unit in enemyQueuedUnits)
            {
                if (unit != null)
                {
                    unit.isReleased = true;
                }
            }
        }
    }
    private void Update()
    {
        if(released)
        {
            bool temp = false;
            foreach ( Unit unit in queuedUnits)
            {
                if(unit != null)
                {
                    temp = true;
                }
            }
            if (!temp)
            {
                defeatScreen.SetActive(true);
            }
            else
            {
                bool temp2 = false;
                foreach( Unit unit in enemyQueuedUnits)
                {
                    if(unit != null)
                    {
                        temp2 = true;
                    }
                }
                if (!temp2)
                {
                    victoryScreen.SetActive(true);
                }
            }
        }
    }
}
