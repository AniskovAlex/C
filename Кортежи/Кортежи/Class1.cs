using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Кортежи
{
    public class RoadMap
    {
        private List<(string,List<(string,int)>)> list= new List<(string, List<(string, int)>)>();
        List<(string, int, string, Boolean)> road;
        private List<(string, string, int)> towns = new List<(string, string, int)>
        {
            ("a","b",7),
            ("a","c",9),
            ("a","f",14),
            ("b","a",7),
            ("b","c",10),
            ("b","d",15),
            ("c","a",9),
            ("c","b",10),
            ("c","d",11),
            ("c","f",2),
            ("d","b",15),
            ("d","c",11),
            ("d","f",9),
            ("e","d",6),
            ("e","f",9),
            ("f","a",14),
            ("f","c",2),
            ("f","e",9)
        };

        public void fill(string from)
        {
            if (towns != null)
            {
                foreach ((string, string, int) x in towns){
                    if (list == null)
                    {
                        (string,int) b= (x.Item2,x.Item3);
                        List<(string, int)> bl=new List<(string, int)>();
                        bl.Add(b);
                        (string, List<(string, int)>) bbl = (x.Item1, bl);
                        list.Add(bbl);
                    }
                    else
                    {
                        Boolean f = false;
                        int i = 0;
                        foreach ((string, List<(string, int)>) y in list)
                        {  
                            if (x.Item1 == y.Item1)
                            {
                                (string, int) b = (x.Item2, x.Item3);
                                list[i].Item2.Add(b);
                                f = true;
                            }
                            i++;
                            if (f) break;
                        }
                        if (!f)
                        {
                            (string, int) b = (x.Item2, x.Item3);
                            List<(string, int)> bl = new List<(string, int)>();
                            bl.Add(b);
                            (string, List<(string, int)>) bbl = (x.Item1, bl);
                            list.Add(bbl);
                        }
                    }
                }
                calulate(list,from);
            }
        }
        private void calulate(List<(string, List<(string, int)>)> list, string a)
        {
            string town_now = a;
            road = new List<(string, int, string, Boolean)>();
            (string, List<(string, int)>) buf = (null, null);
            foreach ((string, List<(string, int)>) x in list)
            {
                if (x.Item1 == town_now)
                {
                    buf = x;
                }
            }
            (string, int, string, Boolean) buffer = (town_now, 0, town_now, true);
            road.Add(buffer);
            while (true)
            {
                (string, int, string, Boolean) set;
                foreach ((string, int) x in buf.Item2)
                {
                    int sum = buffer.Item2 + x.Item2;
                    (string, int, string, Boolean) compare = search_way_by_name(road,x.Item1);
                    string name = buffer.Item3 + "-" + x.Item1;
                    set = (x.Item1, sum, name, true);
                    if (compare!= (null, 0, null, false))
                    {
                        if (compare.Item2 > sum)
                        {
                            int index = road.IndexOf(compare);
                            road[index] = set;
                        }
                    }
                    else road.Add(set);
                }
                int i = road.IndexOf(buffer);
                buffer.Item4 = false;
                road[i] = buffer;
                town_now = search_min(road);
                if (town_now != null)
                {
                    buf = search_name(list, town_now = search_min(road));
                    buffer = search_way_by_name(road, town_now);
                }
                else break;
            }
            foreach ((string, int, string, Boolean) x in road)
            {
                Console.Out.WriteLine(x);
            }
        }
        private (string, List<(string, int)>) search_name(List<(string, List<(string, int)>)> list, string a)
        {
            (string, List<(string, int)>) ret = (null, null);
            foreach ((string, List<(string, int)>) x in list)
            {
                if (x.Item1 == a)
                {
                    ret = x;
                }
            }
            return ret;
        }
        private (string, int, string, Boolean) search_way_by_name(List<(string, int, string, Boolean)> list, string a)
        {
            (string, int, string, Boolean) ret = (null, 0,null,false);
            foreach ((string, int, string, Boolean) x in list)
            {
                if (x.Item1 == a)
                {
                    ret = x;
                }
            }
            return ret;
        }

        private string search_min(List<(string, int, string, Boolean)> list)
        {
            //(string, int, string, Boolean) ret = (null, 0, null, true);
            long sum = Int64.MaxValue;
            string ret=null;
            foreach ((string, int, string, Boolean) x in list)
            {
                if(x.Item4)
                if (x.Item2 < sum)
                {
                    ret = x.Item1;
                }
            }
            return ret;
        }

        public List<(string, string, int)> print_towns()
        {
            return towns;
        }
        public List<(string, int, string, Boolean)> print_roads()
        {
            return road;
        }
    }
}
