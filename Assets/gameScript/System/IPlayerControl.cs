using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerControl//interface to prevent the player's current HP exceeding its max.
{
     void PreventHPOver();
}
