using UnityEngine;
using UnityEngine.UI;

namespace TennisMatch
{
    /// <summary>
    /// ARD
    /// </summary>
    public class MatchSettings : MonoBehaviour
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
                match.score.MatchSetNumber = 3;
            }
            else
            if (MatchInTwoSet.isOn)
            {
                match.score.MatchSetNumber = 2;
            }
            else
            if (MatchInOneSet.isOn)
            {
                match.score.MatchSetNumber = 1;
            }
        }
    }
}
