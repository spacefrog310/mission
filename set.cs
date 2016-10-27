using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Windows.Forms;

namespace MissionTest
{
   class set : Script
    {

        public  void playerModel(Client player,PedHash model)  
        {
            
          API.sendNativeToPlayer(player, 0x00A1CADD00108836, model); // change player model
            
        }

    }
}
