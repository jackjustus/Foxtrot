using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterousMole : Boss
{


    public MonsterousMole() : base() {
        // Constructor for the MonsterousMole class
        bossName = "Monsterous Mole";
        bossDescription = "A giant mole that burrows underground and attacks from below";
        
    }


    #region Targeting Methods
    #endregion

    #region Movement Methods
    #endregion

    #region Attack Methods
    #endregion

    #region Misc Methods
    void OnDrawGizmos()
    {
        // Draw the boss's attack range
    }
    #endregion


}
