using System;
using System.Reflection;

class Program
{
    static void Main()
    {
        try
        {
            Type t = Type.GetTypeFromProgID("CSI.ETABS.API.ETABSObject");
            if (t == null) return;
            var etabsObj = Activator.CreateInstance(t);
            var sapModel = t.GetMethod("GetObject").Invoke(etabsObj, new object[] { "SapModel" });
            if (sapModel == null) return;

            string[] fkeys = null;
            int tblVer = 0, numRecs = 0;
            string[] fields = null, tblData = null;

            object[] args = new object[] { "Frame Assignments - Summary", fkeys, "", tblVer, fields, numRecs, tblData };
            
            var db = sapModel.GetType().GetProperty("DatabaseTables").GetValue(sapModel);
            var m = db.GetType().GetMethod("GetTableForDisplayArray");
            m.Invoke(db, args);

            numRecs = (int)args[5];
            Console.WriteLine("Records with '': " + numRecs);

            args[2] = "All";
            m.Invoke(db, args);
            Console.WriteLine("Records with 'All': " + (int)args[5]);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
