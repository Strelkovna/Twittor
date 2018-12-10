﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace Twittor
{
    public class TwitterHelper
    {
        public static bool SendTweetinReplyToStatusId(TwitterService service, string status, long inReplyToStatusId)
        {
            var sendoptions = new SendTweetOptions();
            sendoptions.Status = status;
            sendoptions.InReplyToStatusId = inReplyToStatusId;
            var response = service.SendTweet(sendoptions);
            if (response == null)
            {
                Console.WriteLine("ERROR SENDING TWEET! Possible duplicate!");
                return false;
            }
            return true;
        }
        // retrieves all post of the user
        public static IEnumerable<TwitterStatus> GetUserTimeline(TwitterService service, long userId, bool includeRts, bool excludeReplies)
        {
            ListTweetsOnUserTimelineOptions options = new ListTweetsOnUserTimelineOptions();
            options.UserId = userId;
            options.IncludeRts = includeRts;
            options.ExcludeReplies = excludeReplies;
            return service.ListTweetsOnUserTimeline(options);
        }

        public static TwitterUser GetUserIdFromUsername(TwitterService service, string screenname)
        {       
            GetUserProfileForOptions options = new GetUserProfileForOptions();
            options.ScreenName = screenname;
            return service.GetUserProfileFor(options);
        }

    }
}