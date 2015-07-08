using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions ;

namespace thex_to_bin
{

    
    public class ThexParseException : Exception
    {
        public ThexParseException() { }
        public ThexParseException(string msg) : base(msg) { }
        public ThexParseException(string msg, Exception inner) : base(msg, inner) { }
    }


    // Parse either simple hex (ie: 1A 03 80 etc) or Intel hex to binary.

    public enum EHexType { DATA_RAWHEX, DATA_INTELHEX, DATA_UNKNOWN } ;

    public class ThexParse
    {
        const string commentPattern = @"^\s*(#.*)$";
        const string doubleHexPattern = @"\s*([0,a-f,A-F][0-9]|[0-9][a-f,A-F]|\d\d|[a-f,A-F][a-f,A-F])\s*";
        static bool isComment(string text)
        {
            Regex re = new Regex(commentPattern);
            MatchCollection matches = re.Matches(text);

            if (matches.Count > 0)
                return true;

            return false;
        }

        static List<byte> getRawHexLine(string text)
        {
            // abort if comment
            if (isComment(text))
                return null;

            List<byte> bytes = new List<byte>();

            // look for double-hex digit patterns
            Regex re = new Regex(doubleHexPattern);
            MatchCollection matches = re.Matches(text);

            foreach (Match m in matches)
            {
                string value = m.Groups[0].Value;

                bytes.Add(byte.Parse(value, System.Globalization.NumberStyles.HexNumber));
            }

            if (bytes.Count == 0)
                return null;

            return bytes ;
        }

        static byte[] getRawHexLineBytes(string text)
        {
            List<byte> bytes = getRawHexLine(text);

            if (bytes == null || bytes.Count == 0)
                return null;

            byte[] data = bytes.ToArray();

            return data;
        }


        static public EHexType detectHexType (string text)
        {
            // StringReader parses \r and \r\n equally for us
            using (StringReader sr = new StringReader (text))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        byte[] data = getRawHexLineBytes(line);

                        if (data != null && data.Length > 0)
                            return EHexType.DATA_RAWHEX;
                    }
                    catch (ThexParseException e)
                    {
                        // Well, must not be a raw hex file
                    }
                }
            }
            return EHexType.DATA_UNKNOWN;
        }


        static public byte[] convertToBin (string text)
        {
            if (detectHexType(text) == EHexType.DATA_UNKNOWN)
                throw new ThexParseException("Unknown type of hex data!");

            List<byte> bytes = new List<byte>();

            // StringReader parses \r and \r\n equally for us
            using (StringReader sr = new StringReader(text))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    List<byte> bytesToAdd = getRawHexLine(line);
                    if (bytesToAdd != null)
                        bytes.AddRange (bytesToAdd);
                }
            }

            return bytes.ToArray();
        }


    }

}
