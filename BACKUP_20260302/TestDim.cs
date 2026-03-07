using System;
using System.Reflection;
using CSiAPIv1;

class Program
{
    static void Main()
    {
        try
        {
            cOAPI myETABSObject = null;
            try
            {
                myETABSObject = (cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.ETABS.API.ETABSObject");
            }
            catch (Exception)
            {
                Console.WriteLine("ETABS API aktıf değil");
                return;
            }

            cSapModel model = myETABSObject.SapModel;

            int numNames = 0;
            string[] myName = null;
            model.FrameObj.GetNameList(ref numNames, ref myName);

            if (numNames > 0)
            {
                int colCount = 0;
                foreach (string tName in myName)
                {
                    eFrameDesignOrientation propType = eFrameDesignOrientation.Null;
                    model.FrameObj.GetDesignOrientation(tName, ref propType);

                    if (propType == eFrameDesignOrientation.Column && colCount < 20)
                    {
                        string propName = "", sAuto = "";
                        model.FrameObj.GetSection(tName, ref propName, ref sAuto);

                        eFramePropType sType = default(eFramePropType);
                        model.PropFrame.GetTypeOAPI(propName, ref sType);

                        double t3 = 0.4, t2 = 0.4;
                        string fileName = "", matProp = "";
                        int color = 0;
                        string notes = "", guid = "";

                        int ret = 0;
                        if ((int)sType == 1) // Rectangular
                        {
                            ret = model.PropFrame.GetRectangle(propName, ref fileName, ref matProp, ref t3, ref t2, ref color, ref notes, ref guid);
                        }
                        else if ((int)sType == 2) // Circular
                        {
                            ret = model.PropFrame.GetCircle(propName, ref fileName, ref matProp, ref t3, ref color, ref notes, ref guid);
                            t2 = t3;
                        }

                        Console.WriteLine(string.Format("Col: {0}, Prop: {1}, sType: {2}, ret: {3}, t3: {4}, t2: {5}", tName, propName, (int)sType, ret, t3, t2));
                        colCount++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
