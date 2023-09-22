namespace OrthoGraphEngine
{
   public static class OBJ
   {
        public static Mesh Read(string path)
        {
            Mesh resault;
            List<Vector3> verties = new List<Vector3>();
            List<Point> lines = new List<Point>();
            string str;

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() > -1)
                {
                    str = sr.ReadLine().Trim();
                    while (str.Contains("  ")) { str = str.Replace("  ", " "); }
                    string[] tmp = str.Split(' ');
                    
                    if (tmp[0] == "v")
                    {  
                        verties.Add(ParseVector3(tmp));
                    }
                    if (tmp[0] == "f")
                    {
                        for (int i = 1; i < tmp.Length - 1; i++)
                        {
                            lines.Add(new Point(ParseNumber(tmp[i]) - 1, ParseNumber(tmp[i + 1]) - 1));
                        }
                        lines.Add(new Point(ParseNumber(tmp[tmp.Length - 1]) - 1, ParseNumber(tmp[1]) - 1));
                    }                    
                }
            }

            resault = new Mesh(verties.ToArray(), lines.ToArray());
            return resault;
        }

        public static Vector3 ParseVector3(string[] array)
        {
            return new Vector3(float.Parse(array[1].Replace(".", ",")), float.Parse(array[2].Replace(".", ",")), float.Parse(array[3].Replace(".", ",")));
        }

        public static int ParseNumber(string str)
        {
            string[] tmp = str.Split('/');
            return int.Parse(tmp[0]);            
        }
   }
}