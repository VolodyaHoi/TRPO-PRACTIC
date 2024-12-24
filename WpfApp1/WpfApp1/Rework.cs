using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Rework
    {
        public int[] ReworkArray(int[] array)
        {
            int[] sec_array = new int[array.Length];
            int minValue = array[0];
            int maxValue = array[0];

            for (int i = 0; i < array.Length - 1; i++) 
            {
                if (minValue > array[i+1])
                {
                    minValue = array[i+1];
                }

            }
            for (int i = 0; i < array.Length - 1;i++)
            {
                if (maxValue < array[i + 1])
                {
                    maxValue = array[i + 1];
                }
                
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == minValue)
                {
                    array[i] = maxValue;
                } else if (array[i] ==  maxValue) { array[i] = minValue; }
            }

            sec_array = array;

            return sec_array;
        }
    }
}
