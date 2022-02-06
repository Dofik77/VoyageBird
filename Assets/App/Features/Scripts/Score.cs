using System;

namespace App.Features.Scripts
{
    public class Score
    {
        public event Action<int> Changed;
        
        public int Value
        {
            get => _value;
            set
            {
                if(value.Equals(_value))
                    return;
                
                _value = value;
                Changed?.Invoke(_value);
            }
        }

        private int _value;
    }
}