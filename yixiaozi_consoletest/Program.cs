using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yixiaozi;
using yixiaozi.Media.Audio;

namespace yixiaozi_consoletest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(yixiaozi.Media.Audio.Audio.GetCurrentSpeakerVolume()); 
            Console.WriteLine(yixiaozi.Media.Audio.Audio.ISTheMute().ToString()); 
        }
    }
}
