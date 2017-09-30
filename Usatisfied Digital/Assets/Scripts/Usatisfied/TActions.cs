using System.Collections;
using System.Collections.Generic;
using Utils.Localization;
using UnityEngine;

namespace Usatisfied
{
    [System.Serializable]
    public class TActions
    {
        public enum ActionsType { Calendar, Education, Feed, Fun, Health, Program, Rest, Sports}
        public string name;

        public ActionsType actionType;
        
        [Header("Resiliences")]
        [Range(0f,1f)]
        public float actionMental;
        [Range(0f, 1f)]
        public float actionPhisycs;
        [Range(0f, 1f)]
        public float actionEmotional;
        [Range(0f, 1f)]
        public float actionSocial;
        public float actionSatisfation;
        [Header("Time")]
        public float actionStart;
        public float actionFinish;

        public float stress;
        public float maxStress;

        public bool notdelete = true;

        public Sprite actionIcons;
     
        public TActions()
        { }
        public TActions(TActions template)
        {
            if (template != null)
            {
                this.name = LocalizationManager.GetText(template.name);
                this.actionType = template.actionType;
                this.actionMental = template.actionMental;
                this.actionPhisycs = template.actionPhisycs;
                this.actionEmotional = template.actionEmotional;
                this.actionSocial = template.actionSocial;
                this.actionSatisfation = template.actionSatisfation;
                this.actionStart = template.actionStart;
                this.actionFinish = template.actionFinish;

                this.actionIcons = ActionsManager.GetInstance().actionIcones[(int)actionType];
            }
        }

        public Sprite GetIcone(ActionsType type)
        {
            return ActionsManager.GetInstance().actionIcones[(int)actionType];
        }
    }
}
