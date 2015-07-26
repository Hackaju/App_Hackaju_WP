using System.Collections.Generic;

namespace AppStudio.Data
{
    public static class OAuthTokensRepository
    {
        private static Dictionary<long, OAuthTokens> Tokens { get; set; }

        static OAuthTokensRepository()
        {
            Tokens = new Dictionary<long, OAuthTokens>();


            Tokens.Add(1690, new OAuthTokens
                            {
                                { "ClientId", "b8171b021b8449a1881d0bfaf3c419af" }
                            });

            Tokens.Add(1691, new OAuthTokens
                            {
                                { "ConsumerKey", "P1kr359wlZ5Q8oal6ZuMJDPjU" },
                                { "ConsumerSecret", "kqLJCGQtIDpJ6tj3qbEpa8WEejghsoZ9s12LOSqQ7gHhm9qlqE" },
                                { "AccessToken", "398047744-DHfPpNyk15zV7plzCsHqbCVg1wBx8Zw3iF5DMbId" },
                                { "AccessTokenSecret", "HX9eKYCgGWbcFmJ0N1TghLp2aLn6HubbdlmUUokPamB6q" }
                            });

        }

        public static OAuthTokens GetTokens(long key)
        {
            if (Tokens.ContainsKey(key))
            {
                return Tokens[key];
            }
            return null;
        }

    }
}
