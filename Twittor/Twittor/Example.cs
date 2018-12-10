//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net;
//using System.IO;
//using TweetSharp;

//namespace ConsoleApplication1
//{
//    class Program
//    {
//        // vars
//        static Random ran = new Random();
//        static int textNum = 0;
//        static bool readFileSucceeds = false;
//        static string uri = "";
//        static string content = "";
//        static string[] contentWords;
//        static int numberOfSourceWords = 1;
//        static Dictionary<string, Dictionary<string, int>> markovDict = new Dictionary<string, Dictionary<string, int>>();
//        static string resultText = "";
//        static string tweetText = "";
//        static int linkLength = 0;
//        static string tweetLink = "";

//        static void Main(string[] args)
//        {
//            /*
//            FileStream filestream = new FileStream("out.txt", FileMode.Create);
//            var streamWriter = new StreamWriter(filestream);
//            streamWriter.AutoFlush = true;
//            Console.SetOut(streamWriter);
//            Console.SetError(streamWriter);
//             */

//            do
//            {
//                textNum = RandomInt(1, 50000); // choose the book to use
//                ReadFileFromWeb(textNum); // find this book
//            } while (readFileSucceeds == false); // if 404, program will loop until valid
//            contentWords = SplitContent(content);
//            numberOfSourceWords = contentWords.Length;
//            markovDict = PopulateMarkovChain(contentWords);
//            resultText = CreateText(markovDict, RandomInt(3, 50));
//            tweetLink = " http://www.gutenberg.org/ebooks/" + textNum;
//            linkLength = tweetLink.Length;
//            if (resultText.Length > 140 - linkLength)
//            {
//                tweetText = resultText.Substring(0, 140 - linkLength) + tweetLink;
//            }
//            else
//            {
//                tweetText = resultText + tweetLink;
//            }
//            Console.WriteLine("Tweet: {0}", tweetText);
//            PostTweet(tweetText);
//            Console.WriteLine("End of program. Press any key to exit.");
//            Console.ReadKey();
//        } // end Main

//        static int RandomInt(int min = 1, int max = 5000) // choose random number
//        {
//            int result = 0;
//            result = ran.Next(min, max);
//            return result;
//        } // end RandomInt

//        static void ReadFileFromWeb(int num = 1228) // fetch the book from Gutenburg, default is Darwin's Origin of Species
//        {
//            uri = "http://www.gutenberg.org/cache/epub/" + num + "/pg" + num + ".txt"; // path to book

//            try // will throw exception if 404
//            {
//                WebClient client = new WebClient();
//                Stream stream = client.OpenRead(uri); // streams the content
//                readFileSucceeds = true; // if the above worked, this will happen and break the loop
//                StreamReader reader = new StreamReader(stream); // read the stream
//                content = reader.ReadToEnd(); // string set to content of the stream
//                Console.WriteLine(content); // write to console for debug purposes
//                Console.WriteLine(num); // // write to console the book ID for debug purposes
//            }
//            catch // if something fucks up, which it will if the book ID is invalid
//            {
//                readFileSucceeds = false; // will force the program to loop until a valid book ID is chosen
//                Console.WriteLine("read failed"); // debug purposes
//            }
//        } // end ReadFileFromWeb

//        static string[] SplitContent(string input) // splits the string, easy shit yo
//        {
//            string[] output = input.Split();
//            return output;
//        }

//        static Dictionary<string, Dictionary<string, int>> PopulateMarkovChain(string[] inputWords) // uwotm9
//        // but seriously, this is a Dictionary that holds a word, and its value is another dictionary that holds
//        // the second word and how many times this pair appears.
//        {
//            // make this keypair dictionary
//            Dictionary<string, Dictionary<string, int>> returnDict = new Dictionary<string, Dictionary<string, int>>();
//            string currentWord = ""; // current word
//            string nextWord = ""; // next word
//            for (int i = 0; i < inputWords.Length; i++) // run through every word in the array
//            {
//                if (i != inputWords.Length - 1) // make sure we don't try and take a pair of the last word
//                {
//                    currentWord = inputWords[i]; // this word
//                    nextWord = inputWords[i + 1]; // the next one
//                    if (returnDict.ContainsKey(currentWord) && returnDict[currentWord].ContainsKey(nextWord)) // check if both the current and next word exist in the dict
//                    {
//                        returnDict[currentWord][nextWord]++; // number of instances increment
//                    }
//                    else if (returnDict.ContainsKey(currentWord)) // if the current word exists only
//                    {
//                        returnDict[currentWord].Add(nextWord, 1); // add the next word and set instances to 1
//                    }
//                    else // current word is new
//                    {
//                        Dictionary<string, int> secondDict = new Dictionary<string, int>(); // second dictionary just to hold the next word and the number of instances (1)
//                        secondDict.Add(nextWord, 1); // add this sole value
//                        returnDict.Add(currentWord, secondDict); // add this to the main dictionary
//                    }

//                }
//                // Console.WriteLine("{0}: {1}", wordPair, returnDict[wordPair]);
//                // code

//            }

//            // prints the entire dictionary for debug purposes
//            foreach (var pair1 in returnDict)
//            {
//                foreach (var pair2 in pair1.Value)
//                {
//                    Console.WriteLine("{0}, {1}: {2}", pair1.Key, pair2.Key, pair2.Value);
//                }
//            } // end debug

//            return returnDict;
//        } // end PopulateMarkovChain

//        static string CreateText(Dictionary<string, Dictionary<string, int>> inputDict, int numOfWords)
//        {
//            string outText = "";
//            string randomWord = contentWords[ran.Next(0, contentWords.Length)];
//            string nextWord = "";


//            for (int i = 0; i < numOfWords; i++)
//            {
//                outText += randomWord + " ";

//                Dictionary<string, int> filterDict = new Dictionary<string, int>();

//                foreach (var pair1 in inputDict)
//                {
//                    foreach (var pair2 in pair1.Value)
//                    {
//                        if (pair1.Key == randomWord)
//                        {
//                            filterDict.Add(pair2.Key, pair2.Value);
//                        }
//                    }
//                }

//                if (filterDict.Count == 0)
//                {
//                    randomWord = contentWords[ran.Next(0, contentWords.Length)];
//                }
//                else
//                {
//                    bool wordChosen = false;
//                    int total = 0;

//                    foreach (var pair in filterDict)
//                    {
//                        total += pair.Value;
//                    }

//                    do
//                    {
//                        foreach (var pair in filterDict)
//                        {
//                            if (ran.Next(0, total) <= pair.Value)
//                            {
//                                nextWord = pair.Key;
//                                wordChosen = true;
//                            }
//                        }
//                    } while (wordChosen == false);

//                    randomWord = nextWord;
//                }
//            }

//            Console.WriteLine(outText);
//            return outText;
//        } // end CreateText

//        static void PostTweet(string textToTweet)
//        {

//            var service = new TwitterService("<<consumerKey>>", "<<consumerSecret>>");
//            Console.WriteLine("set up new TwitterService");
//            service.AuthenticateWith("<<token>>", "<<tokenSecret>>");
//            Console.WriteLine("authenticated");
//            SendTweetOptions tweet = new SendTweetOptions();
//            Console.WriteLine("created tweet");
//            Console.WriteLine("{0}", textToTweet);
//            tweet.Status = textToTweet;
//            Console.WriteLine("assigned status to tweet");
//            service.SendTweet(tweet);
//            Console.WriteLine("tweet sent!");

//        } // end PostTweet
//    }
//}