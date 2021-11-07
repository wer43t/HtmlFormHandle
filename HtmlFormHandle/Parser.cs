using System.Collections.Generic;

namespace Parser
{
    public class ParserQuery
    {
        public Dictionary<string, string> fields;

        public ParserQuery()
        {
            fields = new Dictionary<string, string>();
        }

        public void GetFields(string query)
        {
            foreach (string f in query.Split('&'))
            {
                string[] field = f.Split('=');
                fields.Add(field[0], field[1]);
            }
        }
    }
}
