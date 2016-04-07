using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace DndHelper.HelperClasses
{
    public class RemotePost
    {
        private NameValueCollection Inputs = new NameValueCollection();

        public string Url = "";
        public string Method = "post";
        public string FormName;

        public void Add(string name, string val)
        {
            Inputs.Add(name, val);
        }

        public void Post(List<string> names, List<String> values)
        {
            HttpContext current = HttpContext.Current;
            current.Items.Clear();

            for(int i = 0; i < names.Count; i++)
            {
                if(i < values.Count)
                {
                    current.Items.Add(names[i], values[i]);
                }
            }
            
        }
    }
}