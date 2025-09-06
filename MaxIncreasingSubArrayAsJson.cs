using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Xml.Linq;

public static class MaxIncreasingSubArrayAsJson
{
    /*
    public static void Main()
    {
        List<int> nums = new List<int> { };
        Console.WriteLine(MaxIncreasingSubArrayAsJsonFunction(nums));
    }*/
    public static string MaxIncreasingSubArrayAsJsonFunction(List<int> numbers)
    {
        if(numbers == null || numbers.Count == 0)
        {
            return JsonSerializer.Serialize(new List<int>());
        }
        List<int> temp = new List<int>(); 
        List<int> maxTemp = new List<int>();
        int count = 0;
        int maxCount= 0;
        int i = 0;
        while (i+1 < numbers.Count)
        {
            count = 0;//reset the count and temp because you are will check for the other subarrays now
            temp = new List<int>();
            while (numbers[i] <= numbers[i + 1]) // check each sub array
            {
                temp.Add(numbers[i]);
                count += numbers[i];
                i++;
                
                if (i + 1>= numbers.Count - 1 ) { //break the loop to not get out of index error
                    break;
                }
            }
                if ( (temp.Count == 0 || numbers[i] > temp[temp.Count-1]))//handle the last element
                {
                    temp.Add(numbers[i]);
                    count += numbers[i];
                    i++;
                }

            if (count >= maxCount) // check if subarray is larger then the max
            {
                maxCount = count;
                maxTemp = new List<int>(temp);
            }

        }


        return JsonSerializer.Serialize(maxTemp);
    }
}
