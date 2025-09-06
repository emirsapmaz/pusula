using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pusula
{
    public static class LongestVowelSubsequenceAsJson
    {
        /*
        public static void Main()
        {
            List<string> words = new List<string> { "algorithm", "education", "idea", "strength" };
            Console.WriteLine(LongestVowelSubsequenceAsJsonFunction(words));
        }*/
        public static string LongestVowelSubsequenceAsJsonFunction(List<string> words)
        {
            if (words == null || words.Count == 0)
            {
                return JsonSerializer.Serialize(new List<string>());
            }
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

            List<record> result = new List<record>();

            foreach (string word in words)
            {
                string sequence = string.Empty;
                int length = 0;
                int maxlength = 0;
                string maxsequence = string.Empty;
                for (int i = 0; i < word.Length; i++)//traverse between the letters of the word
                {
                    if (vowels.Contains(word[i]))//check if the word has vowels
                    {
                        sequence += word[i];
                        length++;
                    }
                    else
                    {
                        sequence = string.Empty;
                        length = 0;
                    }
                    if (length > maxlength)//record the longest subsequence
                    {
                        maxlength = length;
                        maxsequence = sequence;
                    }

                }
                    record r = new record(word, maxsequence, maxlength);
                    result.Add(r);
            }

            return JsonSerializer.Serialize(result);

        }
        public class record
        {
            public string word {  get; set; }
            public string sequence { get; set; }
            public int length { get; set; }
            public record(string word,string sequence,int length){
                this.length = length;
                this.word = word;
                this.sequence = sequence;
            }
        }

    }
}
