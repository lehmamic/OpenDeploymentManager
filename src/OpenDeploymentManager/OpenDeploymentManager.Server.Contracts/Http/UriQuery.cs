using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenDeploymentManager.Server.Contracts.Http
{
    public class UriQuery : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>();

        public UriQuery()
        {
        }

        public UriQuery(string query)
        {
            if (query != null)
            {
                int num = query.Length;
                for (int i = ((query.Length > 0) && (query[0] == '?')) ? 1 : 0; i < num; i++)
                {
                    int startIndex = i;
                    int num4 = -1;
                    while (i < num)
                    {
                        char ch = query[i];
                        if (ch == '=')
                        {
                            if (num4 < 0)
                            {
                                num4 = i;
                            }
                        }
                        else if (ch == '&')
                        {
                            break;
                        }

                        i++;
                    }

                    string str = null;
                    string str2 = null;
                    if (num4 >= 0)
                    {
                        str = query.Substring(startIndex, num4 - startIndex);
                        str2 = query.Substring(num4 + 1, (i - num4) - 1);
                    }
                    else
                    {
                        str2 = query.Substring(startIndex, i - startIndex);
                    }

                    this.Add(str != null ? Uri.UnescapeDataString(str) : null, Uri.UnescapeDataString(str2));
                    if ((i == (num - 1)) && (query[i] == '&'))
                    {
                        this.Add(null, string.Empty);
                    }
                }
            }
        }

        public string this[string key]
        {
            get
            {
                foreach (var kvp in this.entries)
                {
                    if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                    {
                        return kvp.Value;
                    }
                }

                return null;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(string key, string value)
        {
            this.entries.Add(new KeyValuePair<string, string>(key, value));
        }

        public void Add(string key, object value)
        {
            string itemValue = value != null ? value.ToString() : string.Empty;
            this.Add(key, itemValue);
        }

        public override string ToString()
        {
            var queryBuilder = new StringBuilder();

            if (this.entries.Count > 0)
            {
                queryBuilder.Append('?');
                var first = true;

                foreach (var kvp in this.entries)
                {
                    if (!first)
                    {
                        queryBuilder.Append('&');
                    }
                    else
                    {
                        first = false;
                    }

                    if (kvp.Key.StartsWith("$"))
                    {
                        queryBuilder.Append("$");
                        queryBuilder.Append(Uri.EscapeDataString(kvp.Key.Substring(1)));
                    }
                    else
                    {
                        queryBuilder.Append(Uri.EscapeDataString(kvp.Key));
                    }

                    queryBuilder.Append('=');
                    queryBuilder.Append(Uri.EscapeDataString(kvp.Value));
                }
            }

            return queryBuilder.ToString();
        }
    }
}
