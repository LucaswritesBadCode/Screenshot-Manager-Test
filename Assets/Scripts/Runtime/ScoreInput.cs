using UnityEngine;
using LucasWritesBadCode.BowlingScoreCounter.Runtime;

namespace Runtime
{
    public class ScoreInput : MonoBehaviour
    {
        [SerializeField] string[] scores;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ScoreHandler.CalculateScore(scores);
            }
        }
    }
}
