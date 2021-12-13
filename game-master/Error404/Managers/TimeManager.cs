using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Error404
{
    public class TimeManager
    {
        private static TimeManager Instance;

        private GameTime time;

        public GameTime Time
        {
            get
            {
                return time;
            }
        }

        private TimeManager()
        {

        }

        public void UpdateTimeManager(GameTime gt)
        {
            time = gt;
        }

        public static TimeManager GetInstance()
        {
            if(Instance == null)
            {
                Instance = new TimeManager();
            }
            return Instance;
        }
    }
}
