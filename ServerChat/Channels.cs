using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ServerChat
{
    public class Channels
    {
        public List<Channel> _channels;

        public Channels()
        {
            _channels = new List<Channel>();
            _channels.Add(new Channel("general"));
            _channels.Add(new Channel("Radio sexe"));
            _channels.Add(new Channel("NitroGaming"));
        }

        public void serialize(Channels channels)
        {
            IFormatter formatter = new BinaryFormatter();
            //We create a new file called "Channels.bin"
            FileStream stream = new FileStream("Channels.bin", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, channels);
            stream.Close();
        }

        public static Channels deserialize()
        {
            IFormatter formatter = new BinaryFormatter();
            //We call the .bin file 
            FileStream stream = new FileStream("Channels.bin", FileMode.Open, FileAccess.Read);
            Channels c = new Channels();
            c = (Channels)formatter.Deserialize(stream);
            stream.Close();
            return c;
        }


    }
}
