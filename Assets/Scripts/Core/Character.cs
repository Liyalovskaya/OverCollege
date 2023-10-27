using System;
using OC.ConfigData;

namespace OC.Core
{
    public class Character : GameEntity
    {
        public string FirstName;
        public string LastName;
        public string CalledName;
        public string CallPlayerName;

        public string flavor;

        private CharacterConfig _config;
        
        
        private Location _location;

        public Location Location
        {
            get => _location;
            set
            {
                if (_location == value) return;
                _location?.Characters.Remove(this);
                _location = value;
                _location.Characters.Add(this);
            }
        }
        
        

    }
}
