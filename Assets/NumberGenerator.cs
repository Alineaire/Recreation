using System.Collections.Generic;
using UnityEngine;

class NumberGenerator
{
    List<int> buttonColors = new List<int>();

    public NumberGenerator(int minInclusive, int maxExclusive)
    {
        for (int i = minInclusive; i < maxExclusive; ++i)
            buttonColors.Add(i);
    }

    public int Draw()
    {
        int index = Random.Range(0, buttonColors.Count - 1);
        int value = buttonColors[index];
        buttonColors.RemoveAt(index);
        buttonColors.Add(value);
        return value;
    }
}
