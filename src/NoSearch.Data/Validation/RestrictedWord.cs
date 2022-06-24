using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSearch.Data.Validation
{
    internal enum RestrictedWordReason
    {
        Offensive = 0
    }

    public class RestrictedWord
    {
        public RestrictedWord(string word, int id)
        {
            Id = id;
            Word = word;            
        }

        public int Id { get; set; }
        public string Word { get; set; }
        public int Reason { get; set; } = (int)RestrictedWordReason.Offensive;
    }
}
