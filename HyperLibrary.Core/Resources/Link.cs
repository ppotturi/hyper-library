using System;

namespace HyperLibrary.Core.Resources
{
    public class Link
    {
        public Uri Href { get; set; }
        public string Rel { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
    }
}