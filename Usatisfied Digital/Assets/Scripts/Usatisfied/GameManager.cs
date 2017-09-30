using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Usatisfied
{

    public class GameManager : IDontDestroy<GameManager>
    {
        public enum Resiliences { Mental, Phisycs, Emotional, Social, Satisfaction}
        public TActions.ActionsType actualAction;
        public List<TActions> actionsPreset;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}