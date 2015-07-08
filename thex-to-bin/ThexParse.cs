using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace thex_to_bin
{
    // Parse either simple hex (ie: 1A 03 80 etc) or Intel hex to binary.

    public enum EDataType { DATA_RAWHEX, DATA_INTELHEX, DATA_UNKNOWN } ;

    public class ThexParse
    {

        static public EDataType detectDataType (string text)
        {
            // StringReader parses \r and \r\n equally for us
            using (StringReader sr = new StringReader (text))
            {
                string line = sr.ReadLine();

                if (line != null)
                {

                }
            }
            return EDataType.DATA_UNKNOWN;
        }


        static public Byte[] convertToBin (string text)
        {

        }


    }

}
