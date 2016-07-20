/*
 * Credits: Stephen Walther, http://stephenwalther.com
 * The source code has been taken from his MVC Tip blog series http://stephenwalther.com/blog/archive/2008/07/02/asp-net-mvc-tip-13-unit-test-your-custom-routes.aspx
 */

using System;
using System.Security.Principal;

namespace Nuts.Web.Test.Fakes
{
    public class FakeIdentity : IIdentity
    {
        private readonly string name;

        public FakeIdentity(string userName)
        {
            this.name = userName;
        }

        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.name); }
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}
