using System.Collections.Generic;
using System.Linq;

namespace CodeLuau
{
    internal class Email
    {
        private static readonly List<string> UnacceptableDomains
            = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };

        public string Address { get; set; }
        public string Domain => Address.Split('@').Last();
        public bool IsEmpty => string.IsNullOrWhiteSpace(Address);

        public bool HasAcceptableDomain()
        {
            var domain = Domain;
            return !UnacceptableDomains.Contains(domain);
        }
    }
}
