using System;
using System.Reflection;
using System.Linq;

class Program
{
    static void Main()
    {
        try
        {
            Assembly asm = Assembly.LoadFrom(@"C:\Program Files\Computers and Structures\ETABS 22\CSiAPIv1.dll");
            var types = asm.GetTypes();
            var propFrameType = types.FirstOrDefault(t => t.Name == "cDatabaseTables");
            
            if (propFrameType != null)
            {
                var methods = propFrameType.GetMethods().Where(m => m.Name.Contains("SetLoad")).ToList();
                foreach(var m in methods)
                {
                    Console.Write(m.Name + "(");
                    var parameters = m.GetParameters();
                    for(int i=0; i<parameters.Length; i++)
                    {
                        var p = parameters[i];
                        Console.Write(p.IsOut ? "out " : (p.ParameterType.IsByRef ? "ref " : ""));
                        Console.Write(p.ParameterType.Name.Replace("&", "") + " " + p.Name);
                        if (i < parameters.Length - 1) Console.Write(", ");
                    }
                    Console.WriteLine(")");
                }
            }
            else
            {
                Console.WriteLine("cDatabaseTables type not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
