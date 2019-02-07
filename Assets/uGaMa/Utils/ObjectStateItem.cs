using System;
using System.Collections.Generic;
using UnityEngine;

namespace uGaMa.Utils
{
    [Serializable]
    public class ObjectStateItem
    {
        [SerializeField] private List<GameObject> gameObjects;

        private int state;

        public int State
        {
            get { return state; }
            set
            {
                if (value < gameObjects.Count)
                {
                    state = value;

                    for (int i = 0; i < gameObjects.Count; i++)
                    {
                        if(value == i)
                            gameObjects[value].SetActive(true);
                        else
                            gameObjects[i].SetActive(false);
                    }
                }

                state = -1;
            }
        }
    }
}