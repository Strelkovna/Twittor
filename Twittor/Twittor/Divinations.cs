using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twittor
{
    class Divinations
    {
    }
}

public class Rootobject
{
    public Divination[] Divinations { get; set; }
}

public class Divination
{
    public string Id { get; set; }
    public string Hexagram { get; set; }
    public string Name { get; set; }
    public string Judgement { get; set; }
}
