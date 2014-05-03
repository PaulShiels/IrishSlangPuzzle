using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrishSlangPuzzle
{
    public class User
    {
        public string name { get; set; }
        public int points { get; set; }
        

        public User()
        {

        }

        public User(string name, int points)
        {
            this.name = name;
            this.points = points;
        }

        public override string ToString()
        {
            return String.Format("{0} \t {1}", name, points);
        }
    }
}
