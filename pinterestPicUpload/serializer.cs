using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace PinBot
{
    public class serializer
    {
        public serializer()
        {
        }

        public void SerializeObject(string filename, account objectToSerialize)
        {
            if (objectToSerialize == null)
                return;
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public account DeSerializeObject(string filename)
        {
            account objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (account)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
