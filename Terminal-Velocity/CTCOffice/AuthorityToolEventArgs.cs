using System;

namespace CTCOffice
{
    public class AuthorityToolEventArgs : EventArgs
    {
        public AuthorityToolEventArgs(int authority)
        {
            Authority = authority;
        }

        public int Authority { get; set; }
    }
}