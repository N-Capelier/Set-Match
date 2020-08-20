using UnityEngine;
using System.Collections.Generic;

namespace TennisMatch
{
    /// <summary>
    /// ARD script 
    /// <para> 
    /// Rally allant de -3 à 3 
    /// </para>
    /// </summary>
    public class RallyScorer : MonoBehaviour
    {
        [Header("Variable")]
        [Range(-3, 3)] public int value = 0;

        public void MovedTo(int pos, bool pointMarked)
        {
            //Save la nouvelle position jeton
            value = pos;

            //Event du point si il y en a un
            if (pointMarked)
            {
                ResetRally();
            }
        }
        public void ResetRally()
        {
            value = 0;
        }
    }
}
