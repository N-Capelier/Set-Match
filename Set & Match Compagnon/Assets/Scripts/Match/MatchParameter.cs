using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class MatchParameter : MonoBehaviour
    {
        [Header("Variable")]
        [SerializeField] private MatchData match;

        [SerializeField] private Toggle simpleMatch, doubleMatch;
        [SerializeField] private Toggle MatchInOneSet, MatchInTwoSet, MatchInThreeSet;


        private void Update()
        {
            //Party Simple ou Double
            if (simpleMatch.isOn)
            {
                match.doubleMatch = false;
            }
            else
            if (doubleMatch.isOn)
            {
                match.doubleMatch = true;
            }

            //Nombre de Set
            if (MatchInThreeSet.isOn)
            {
                match.MatchSetNumber = 3;
            }
            else
            if (MatchInTwoSet.isOn)
            {
                match.MatchSetNumber = 2;
            }
            else
            if (MatchInOneSet.isOn)
            {
                match.MatchSetNumber = 1;
            }
        }
    }
}
