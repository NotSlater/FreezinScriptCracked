using System;

namespace Steam
{
    public class api
    {
        private string name;
        private string ownerid;
        private string secret;
        private string version;

        public api(string name, string ownerid, string secret, string version)
        {
            this.name = name;
            this.ownerid = ownerid;
            this.secret = secret;
            this.version = version;
        }
    }
}