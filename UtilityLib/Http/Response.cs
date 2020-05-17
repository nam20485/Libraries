using System.Text;

namespace Libraries.UtilityLib.Http
{
    public class Response
    {
        public readonly byte[] Bytes = null;

        public string String
        { 
            get { return Bytes != null? Encoding.Default.GetString(Bytes) : null; }
        }

        public Response(byte[] bytes)
        {
            Bytes = bytes;
        }

        public Response(string bytes)
            : this(bytes.ToByteArray())
        {
        }

        public override string ToString()
        {
            return String;
        }
    }

}

