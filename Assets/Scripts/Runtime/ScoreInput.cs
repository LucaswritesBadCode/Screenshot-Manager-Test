using UnityEngine;
using LucasWritesBadCode.BowlingScoreCounter.Runtime;

namespace Runtime
{
    public class ScoreInput : MonoBehaviour
    {
[SerializeField] int[] scores;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ScoreHandler.GetScore(scores);
            }
        }
    }
}
