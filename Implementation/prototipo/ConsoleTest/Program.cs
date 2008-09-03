using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Usuario: " + System.Web.Security.Membership.GetUser().UserName);
            SharepointUtilities.Utilities util = new SharepointUtilities.Utilities();
            //Console.Out.WriteLine("Se encontraron " +util.Test().ToString() + " listas");
            Console.In.ReadLine();
        }
    }
}
