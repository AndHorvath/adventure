using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal interface IPlayable
    {
        public Player Player { get; }

        public void Start();
        public void Play();
        public void Stop();
    }
}