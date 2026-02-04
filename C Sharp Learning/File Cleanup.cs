/*This program is part of my push to learn a bit of C# before work needs me to.
 * This one time run program will get my wordle text base trimmed down, since its
 * based on multiple sources, that will of course have repeated words
 * It shows basic File I/O, and an understanding of data structures
 * (specifically Dictionaries). Using a dictionary is a bit overkill (a 10,000 index array
 * will work fine on my pc) but I felt getting a better understanding on 
 * certain data structures in C# was worth the extra effort and overkill.
 */


//TODO: Add standardization to input stream.
using System;
using System.Collections.Generic;
using System.IO;

namespace One_Time_File_Cleanup
{
    internal class File_Cleanup
    {
        static void Main(string[] args)
        {
            var fileName = "Word List.txt";
            if (!File.Exists(fileName)) 
            {
                Console.WriteLine("File was not found");
                Console.ReadKey();
                return;
            }
            StreamReader file = new StreamReader(fileName);
            HashSet<String> wordList = new HashSet<String>();
            
            String word;
            var deletionCounter = 0;
            while (!file.EndOfStream)
            {
                word = file.ReadLine();

                if(!wordList.Add(word))
                {
                    deletionCounter++;
                }
            }
            


        }
        //static StreamReader OpenTextFile(string name)
        //{
        //    StreamReader file = new StreamReader(name);
        //    return file;
        //}
    }
}
