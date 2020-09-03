using UnityEngine;

namespace TennisMatch
{
    public class DataVisualizer : MonoBehaviour
    {
        public static string IntPointIntoString(int allPoints, int advPoints)
        {
            if (allPoints <= 3)
            {
                switch (allPoints)
                {
                    case 0:
                        return "0";
                    case 1:
                        return "15";
                    case 2:
                        return "30";
                    case 3:
                        return "40";
                    default:
                        return "XX";
                }

            }
            else
            {
                if (allPoints > advPoints)
                {
                    return "40A";
                }
                else
                {
                    return "40";
                }
            }
        }

    }
}
