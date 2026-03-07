using System;
using System.Reflection;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {
            var asm = Assembly.LoadFile(@"C:\Program Files\Computers and Structures\ETABS 22\CSiAPIv1.dll");
            var type = asm.GetType("CSiAPIv1.cPropFrame");
            if (type == null) { Console.WriteLine("Type not found"); return; }
            var methods = type.GetMethods().ToList();
                
            foreach(var m in methods)
            {
                var prm = m.GetParameters().Select(p => 
                    (p.IsOut ? "out " : (p.ParameterType.IsByRef ? "ref " : "")) + 
                    p.ParameterType.Name.Replace("&", "") + " " + p.Name).ToArray();
                Console.WriteLine(string.Format("{0}({1})", m.Name, string.Join(", ", prm)));
            }
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
