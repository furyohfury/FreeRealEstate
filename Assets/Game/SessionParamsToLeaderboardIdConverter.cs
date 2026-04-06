namespace Game
{
    public static class SessionParamsToLeaderboardIdConverter
    {
        private const string EASY = "EasyLB";
        private const string MEDIUM = "MediumLB";
        private const string HARD = "HardLB";
        
        public static string Convert(string paramsId)
        {
            switch (paramsId)
            {
                case "Mock":
                    return "MockLB";
                case "Easy":
                    return EASY;
                case "Medium":
                    return MEDIUM;
                case "Hard":
                    return HARD;
                default:
                    return null;
            }
        }
    }
}
