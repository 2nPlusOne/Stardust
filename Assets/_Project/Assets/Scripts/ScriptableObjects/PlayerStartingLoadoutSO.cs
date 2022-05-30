using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    [CreateAssetMenu(fileName = "PlayerStartingLoadout", menuName = "Scriptable Objects/Player Starting Loadout")]
    public class PlayerStartingLoadoutSO : ScriptableObject
    {
        public BodyDetailsSO startingBodyDetails;
        public int startingMass;
        public int startingMetal;
        public EngineDetailsSO startingEngineDetails;
    }
}
