using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TweetSharp;
using System.Timers;
using Markov;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Twittor
{
    class Program
    {
        private static string customer_key = "W4Y7W3DHB9fhyFKa5GI9b2C10";
        private static string customer_secret_key = "14BiDO4nTUsXmhYNxCh590hZXzdalICqPhiUyvgTikTuLuKsFg";
        private static string acess_token = "1035452087683956737-rUQBiaGeJv6dx5pXrnHC3RWbfCmf5M";
        private static string acess_token_secret = "F3ZgZkvD7w3z8lZ6AFAvyzsX2oYnsCumhd97Q1gXBWSQO";

        private static TwitterService _twitterService = new TwitterService(
            customer_key,
            customer_secret_key,
            acess_token,
            acess_token_secret);

        static void Main(string[] args)
        {
            //generateRandomDailyPhraseUsingMarkov();
            //markovTest();
            generateRandomPrivateReply();
            //testTwittor();
            Console.ReadKey(true);
        }

        private static void testTwittor()
        {
            for (int i = 0; i < 10; i++)
            {
                SendSimpleTestTweet(generateRandomPrivateReply());
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


        public static void markovTest2()
        {
            var chain = new MarkovChain<string>(1);

            chain.Add(
                new[]
                {
                    "1", "䷀", "The Creative",
                    "The Creative works sublime success,\r\nFurthering through perseverance..\n"
                }, 1);
            chain.Add(
                new[]
                {
                    "2", "䷁", "The Receptive",
                    "The Receptive brings about sublime success, \r\nFurthering through the perseverance of a mare. \r\nIf the superior man undertakes something and tries to lead, \r\nHe goes astray; \r\nBut if he follows, he finds guidance. \r\nIt is favorable to find friends in the west and south, to forego friends in the east and north.\r\nQuiet perseverance brings good fortune.\n"
                }, 1);
            chain.Add(
                new[]
                {
                    "3", "䷂", "Difficulty at the Beginning",
                    "Difficulty at the Beginning works supreme success,\r\nFurthering through perseverance.\r\nNothing should be undertaken. \r\nIt furthers one to appoint helpers..\n"
                }, 1);

            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                var sentence = string.Join(" ", chain.Chain(rand));
                Console.WriteLine(sentence);
            }

            Console.ReadKey(true);
        }

        public static void markovTest()
        {
            var chain = new MarkovChain<string>(1);

            chain.Add(
                new[]
                {
                    "1", "䷀", "The Creative",
                    "The Creative works sublime success,\r\nFurthering through perseverance..\n"
                }, 1);
            chain.Add(
                new[]
                {
                    "2", "䷁", "The Receptive",
                    "The Receptive brings about sublime success, \r\nFurthering through the perseverance of a mare. \r\nIf the superior man undertakes something and tries to lead, \r\nHe goes astray; \r\nBut if he follows, he finds guidance. \r\nIt is favorable to find friends in the west and south, to forego friends in the east and north.\r\nQuiet perseverance brings good fortune.\n"
                }, 1);
            chain.Add(
                new[]
                {
                    "3", "䷂", "Difficulty at the Beginning",
                    "Difficulty at the Beginning works supreme success,\r\nFurthering through perseverance.\r\nNothing should be undertaken. \r\nIt furthers one to appoint helpers..\n"
                }, 1);

            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                var sentence = string.Join(" ", chain.Chain(rand));
                Console.WriteLine(sentence);
            }

            Console.ReadKey(true);
        }

        public static void generateRandomDailyPhraseUsingMarkov()
        {
            var json = File.ReadAllText("C:\\Files\\Dev\\Twittor\\Twittor\\Twittor\\dbSimple.json");

            Rootobject divination = JsonConvert.DeserializeObject<Rootobject>(json);

            var chain = new MarkovChain<string>(1);
            for (int i = 1; i < 4; i++)
            {
                chain.Add(new[] {divination.Divinations[i].Id, divination.Divinations[i].Hexagram}, 1);
            }

            var rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                var sentence = string.Join(" ", chain.Chain(rand));
                Console.WriteLine("Sentence#" + i + " " + sentence);
            }

        }

        public static string generateRandomPrivateReply()
        {
            var json = File.ReadAllText("C:\\Files\\Dev\\Twittor\\Twittor\\Twittor\\dbSimple.json");

            Rootobject divination = JsonConvert.DeserializeObject<Rootobject>(json);

                Random rand = new Random(DateTime.Now.Millisecond);
                int rnd = rand.Next(1, 4);
                var phrase = divination.Divinations[rnd].Id + " " + divination.Divinations[rnd].Hexagram + " " + divination.Divinations[rnd].Name + " " +divination.Divinations[rnd].Judgement;
                Console.WriteLine(phrase);
            return phrase;
        }
    }
}
