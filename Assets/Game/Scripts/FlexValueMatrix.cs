using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlexValueMatrix
{
    public static int[] CONST_VALUE = {0, 100, 20, 30, 40, 50, 45, 5};      // Default value of pieces

    public static int[,] SOLDIER_FLEX_VAL =                 // Flex value of Soldier base on posiotion
    {       
        {7,10,12,15,20,15,12,10,7},
        {7,10,12,15,15,15,12,10,7},
        {5, 7,10,10,10,10,10, 7,5},
        {5, 5, 5, 5, 5, 5 ,5, 5,5},
        {5, 5, 5, 5, 5, 5 ,5, 5,5},

        {5, 5, 5, 5, 5, 5 ,5, 5,5},
        {5, 5, 5, 5, 5, 5 ,5, 5,5},
        {5, 7,10,10,10,10,10, 7,5},
        {7,10,12,15,15,15,12,10,7},
        {7,10,12,15,20,15,12,10,7}
    };

    public static int[,] GENERAL_FLEX_VAL =                 // Flex value of General base on posiotion
    {
        {0, 0, 0, 50, 100, 50, 0, 0, 0},
        {0, 0, 0, 50, 75, 50, 0, 0, 0},
        {0, 0, 0, 40, 50, 40, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},

        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 40, 50, 40, 0, 0, 0},
        {0, 0, 0, 50, 75, 50, 0, 0, 0},
        {0, 0, 0, 50, 100, 50, 0, 0, 0},
    };

    public static int[,] RED_PLUS_FLEX_VAL =                // Flex plus value of Red Chariot, Horse, Cannon
    {
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},

        {30, 30, 40, 50, 50, 50, 40, 30, 30},
        {30, 40, 40, 50, 50, 50, 40, 40, 30},
        {30, 40, 40, 50, 50, 50, 40, 40, 30},
        {40, 40, 40, 50, 50, 50, 40, 40, 40},
        {50, 50, 50, 50, 50, 50, 50, 50, 50},
    };

    public static int[,] BLACK_PLUS_FLEX_VAL =              // Flex plus value of Black Chariot, Horse, Cannon
    {
        {50, 50, 50, 50, 50, 50, 50, 50, 50},
        {40, 40, 40, 50, 50, 50, 40, 40, 40},
        {30, 40, 40, 50, 50, 50, 40, 40, 30},
        {30, 40, 40, 50, 50, 50, 40, 40, 30},
        {30, 30, 40, 50, 50, 50, 40, 30, 30},

        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0},
    };

}
