using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;
using System.Timers;
using Markov;


namespace Twittor
{
    class Program
    {
        private static string customer_key = "";
        private static string customer_secret_key = "";
        private static string acess_token = "";
        private static string acess_token_secret = "";

        private  static TwitterService _twitterService = new TwitterService(
            customer_key, 
            customer_secret_key, 
            acess_token, 
            acess_token_secret);

        static void Main(string[] args)
        {
            markovTest();
            //testTwittor();
        }

        private static void testTwittor()
        {
            for (int i = 0; i < 10; i++)
            {
                Random rand = new Random(DateTime.Now.Millisecond);
                int rnd = rand.Next(1, 100);
                SendSimpleTestTweet("Maa!:" + rnd);
                wait1hour();
            }
        }

        public static void SendSimpleTestTweet(string _status)
        {
            _twitterService.SendTweet(new SendTweetOptions {Status = _status}, (status, response) => { });
        }

        public static void wait1hour()
        {
            Thread.Sleep(3600000);
        }

        // в ответе должно быть # + Hexagram + The Name + The Judgment (прямо из соотв колонок включая значок из колонки Hexagram)


        public static void markovTest()
        {
            var chain = new MarkovChain<string>(1);

            chain.Add(new[] { "1", "䷀", "The Creative", "The Creative works sublime success,\r\nFurthering through perseverance..\n" }, 1);
            chain.Add(new[] { "2", "䷁", "The Receptive", "The Receptive brings about sublime success, \r\nFurthering through the perseverance of a mare. \r\nIf the superior man undertakes something and tries to lead, \r\nHe goes astray; \r\nBut if he follows, he finds guidance. \r\nIt is favorable to find friends in the west and south, to forego friends in the east and north.\r\nQuiet perseverance brings good fortune.\n"}, 1);
            chain.Add(new[] { "3", "䷂", "Difficulty at the Beginning", "Difficulty at the Beginning works supreme success,\r\nFurthering through perseverance.\r\nNothing should be undertaken. \r\nIt furthers one to appoint helpers..\n" }, 1);

            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                var sentence = string.Join(" ", chain.Chain(rand));
                Console.WriteLine(sentence);
            }

            Console.ReadKey(true);
        }

    }
}
