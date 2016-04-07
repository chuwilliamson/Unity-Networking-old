using UnityEngine;
using System.Collections;

namespace Eric
{
    public interface iPlayer
    {
        int GainGold(int a_gold);
        int LevelUp(int a_levels);
        int MoveCardTo(Transform a_location);
        int GainExperience(int a_experience);
    }
}
