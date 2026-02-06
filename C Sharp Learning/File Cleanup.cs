/*This program is part of my push to learn a bit of C# before work needs me to.
 * This one time run program will get my wordle text base trimmed down, since its
 * based on multiple sources, that will of course have repeated words
 * It shows basic File I/O, and an understanding of data structures
 * (specifically Dictionaries). Using a dictionary is a bit overkill (a 10,000 index array
 * will work fine on my pc) but I felt getting a better understanding on 
 * certain data structures in C# was worth the extra effort and overkill.
 */


//TODO: DONE
using System;
using System.Collections.Generic;
using System.IO;

namespace One_Time_File_Cleanup
{
    internal class File_Cleanup
    {
        static void Main(string[] args)
        {
            prepareGuessList();
            prepareAnswerList();
        }

        static void prepareGuessList()
        {
            var baseListName = "Word List.txt"; //project settings are set to include word list in output file. 
                                                //Fixes the relative path. This is a Visual Studio Community setting.
            if (!File.Exists(baseListName))
            {
                Console.WriteLine("Base wordlist was not found");
                Console.ReadKey();
                return;
            }

            StreamReader baseFile = new StreamReader(baseListName);
            HashSet<String> wordList = new HashSet<String>();

            //Read file and build HashSet of all words
            var deletionCounter = 0;
            deletionCounter = ReadFileAndBuildSet(baseFile, wordList);
            Console.WriteLine(deletionCounter);

            //Write to new file
            var trimmedFileName = "C:/Users/minec/source/repos/C Sharp Learning/C Sharp Learning/trimmed wordle list.txt";
            StreamWriter trimmedFile = new StreamWriter(trimmedFileName);

            foreach (String key in wordList)
            {
                trimmedFile.WriteLine(key);
            }
            trimmedFile.Close();
            baseFile.Close();
        }
        static void prepareAnswerList()
        {
            var baseListName = "C:\\Users\\minec\\source\\repos\\C Sharp Learning\\C Sharp Learning\\trimmed wordle list.txt";
            if (!File.Exists(baseListName))
            {
                Console.WriteLine("Trimmed wordle list does not exist");
                Console.ReadKey();
            }

            StreamReader baseFile = new StreamReader(baseListName);
            List<String> validAnswerList = new List<string>();
           
            String word;
            while (!baseFile.EndOfStream)
            {
                word = baseFile.ReadLine().Trim();
                
                if (word.EndsWith("s")) //if word is plural DON'T ADD TO VALID ANSWERS LIST.
                {
                    continue;
                }

                validAnswerList.Add(word);
            }
            baseFile.Close();

            string finalAnswerFileName = "C:\\Users\\minec\\source\\repos\\C Sharp Learning\\C Sharp Learning\\Trimmed Answer List.txt";
            StreamWriter finalAnswerFile = new StreamWriter(finalAnswerFileName);

            foreach(string validAnswer in validAnswerList)
            {
                finalAnswerFile.WriteLine(validAnswer);
            }
        }
        //returns false if null, returns true otherwise
        static bool WordCleanup(String word)
        {
            if(word.Equals(null))
            {
                return false;
            }
            word.ToLower();
            word.Trim();
            
            return true;
        }
        //returns the amount of lines that were repeated / deleted.
        static int ReadFileAndBuildSet(StreamReader file, HashSet<String> wordList)
        {
            String word;
            var deletionCounter = 0;

            while (!file.EndOfStream)
            {
                word = file.ReadLine();

                if (!WordCleanup(word))
                {
                    Console.WriteLine("Error! Null word encountered. Terminating...");
                    Environment.Exit(-1);
                }

                if (!wordList.Add(word)) //HashSet Add method doesn't allow duplicates. So we automatically don't need to check for them.
                {
                    //Console.WriteLine(word); writes all DUPLICATE words
                    deletionCounter++;
                }
            }

            return deletionCounter;
        }
    }
}
