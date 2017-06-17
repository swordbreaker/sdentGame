using System.Collections.Generic;
using System.Linq;
using MoonSharp.Interpreter.DataStructs;
using UnityEditor.iOS.Xcode;

namespace Assets.Scripts.Console
{
    public class AutoCompleteManger
    {
        private readonly Dictionary<string, List<string>> _autoCompleteDictronary = new Dictionary<string,List<string>>();

        public IEnumerable<string> GetAwaibleCommands(string s)
        {
            var splitt = s.Split('.');
            if (_autoCompleteDictronary.ContainsKey(splitt[0]))
            {
                return _autoCompleteDictronary[s];
            }
            else
            {
                if (splitt.Length > 1)
                {
                    var cmd = splitt[0] + ".";
                    if (_autoCompleteDictronary.ContainsKey(cmd))
                    {
                        return Filter(_autoCompleteDictronary[cmd], splitt[1]).Select(s1 => cmd + s1);
                    }
                    return new List<string>();
                }
                return Filter(_autoCompleteDictronary.Keys, splitt[0]);
            }
        }

        public void Add(string value, IEnumerable<string> l)
        {
            if(l == null) l = new List<string>();
            _autoCompleteDictronary[value] = l.ToList();
        }

        public void Add(string key, string value)
        {
            if (_autoCompleteDictronary.ContainsKey(key))
            {
                _autoCompleteDictronary[key].Add(value);
            }
            else
            {
                _autoCompleteDictronary.Add(key, new List<string>(new[]{value}));
            }
        }

        public void Add(string value)
        {
            Add(value, new List<string>());
        }

        private IEnumerable<string> Filter(IEnumerable<string> list, string value)
        {
            return list.Where(s => s.Length >= value.Length && s.Substring(0, value.Length) == value);
        }
    }
}
