using System;
namespace ServerChat
{
    public class Channel
    {
        public string _name;
        public bool _locked;

        public Channel(string name)
        {
            this._name = name;
        }
    }
}
