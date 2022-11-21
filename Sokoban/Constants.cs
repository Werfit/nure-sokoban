namespace Sokoban
{

    public static class Constants
    {
        public struct AssetsPaths
        {
            public static readonly string Player = "./assets/images/player.png";
            public static readonly string BoxInactive = "./assets/images/crate_04.png";
            public static readonly string BoxActive = "./assets/images/crate_14.png";
            public static readonly string Site = "./assets/images/environment_08.png";
            public static readonly string Wall = "./assets/images/block_01.png";
            public static readonly string Space = "./assets/images/ground_04.png";
        };

        public static readonly int BlockSize = 50;

        public static readonly string[] LevelPaths = new string[] { "./assets/levels/level1.txt", "./assets/levels/level2.txt" };
    }
}

