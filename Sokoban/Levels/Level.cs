namespace Sokoban.Levels
{
    public enum ElementType
    {
        Wall,
        Box,
        Site,
        Player,
        BoxOnSite,
        Space
    }

    public struct Element
    {
        public ElementType type;
        public int x, y;
    }

    public class Level
    {
        private string _levelMapPath;

        public Level(string levelMapPath)
        {
            _levelMapPath = levelMapPath;
        }

        public Element[] ReadMap()
        {
            if (!File.Exists(_levelMapPath))
            {
                Console.WriteLine("File does not exist {0}", _levelMapPath);
                return Array.Empty<Element>();
            }

            string[] fileContent = File.ReadAllLines(_levelMapPath);
            List<Element> elements = new List<Element>();

            for (int y = 0; y < fileContent.Length; y++)
            {
                for (int x = 0; x < fileContent[y].Length; x++)
                {
                    Element tmp = new Element()
                    {
                        x = x * 50,
                        y = y * 50
                    };

                    switch (fileContent[y][x])
                    {
                        case '#':
                            {
                                tmp.type = ElementType.Wall;
                                elements.Add(tmp);
                                break;
                            }
                        case '.':
                            {
                                tmp.type = ElementType.Site;
                                elements.Add(tmp);
                                break;
                            }
                        case '!':
                            {
                                tmp.type = ElementType.Box;
                                elements.Add(tmp);
                                break;
                            }
                        case '@':
                            {
                                tmp.type = ElementType.Player;
                                elements.Add(tmp);
                                break;
                            }
                        case '^':
                            {
                                tmp.type = ElementType.BoxOnSite;
                                elements.Add(tmp);
                                break;
                            }
                    }
                    tmp.type = ElementType.Space;
                    elements.Add(tmp);
                }
            }

            return elements.ToArray();
        }
    }
}

