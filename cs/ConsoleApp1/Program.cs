using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
	    public static void Main()
	    {
		    var vocabulary = GetSortedWords(
			    "Hello, hello, hello, how low",
			    "",
			    "With the lights out, it's less dangerous",
			    "Here we are now; entertain us",
			    "I feel stupid and contagious",
			    "Here we are now; entertain us",
			    "A mulatto, an albino, a mosquito, my libido...",
			    "Yeah, hey"
		    );
		    foreach (var word in vocabulary)
			    Console.WriteLine(word);
	    }

	    public static string[] GetSortedWords( string textLines)
	    {
			
			
		    var list = textLines
			    .Split(new string[] { " ", ".", ",", "'" }, StringSplitOptions.RemoveEmptyEntries)

			    //.Select(sel=>sel.Replace(".","").Replace(",",""))
			    .Select(st => st.ToLower())
			    .Distinct()
			    .Select(se => Tuple.Create(se.Length, se))
			    .OrderBy(e => e)
			    .Select(v => v.Item2)
			    .ToList();
            ;
			    
		    return null;
	    }
    }
}
