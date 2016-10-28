using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Windows.Forms;

namespace MissionTest
{

    public class MissionTest : Script
    {
        public MissionTest()
        {
            API.onResourceStart += myResourceStart;
            
        }

        public void myResourceStart( )
        {
            API.consoleOutput("Starting mission script!");

        }

        [Command("start")]
        public void startMission(Client sender) 
        {
             
            var curModel = API.getEntityModel(sender.CharacterHandle); // current player model
            var oldModel = API.pedNameToModel(Convert.ToString(curModel)); // the old player model (not sure)
            PedHash model = PedHash.GarbageSMY; // garbadge man model
            var create = new create();
            VehicleHash missionVeh = VehicleHash.Trash;
            Vector3 vehRot = new Vector3(0, 0, 0); // Vehicle rotation

            Vector3 missionStartCoord = sender.Position; // player position
            Vector3 missionDesCoord = new Vector3(50, 50, 50); // destination coords 
            var missionStartIcon = 78;
            var startPoint = create.shape(missionStartCoord);

            create.blipAtPos(missionStartCoord, missionStartIcon); // create shape to start mission

            startPoint.onEntityEnterColShape += (s, ent) =>
            {
                create.mission(sender, model, missionStartCoord, missionDesCoord, missionVeh, vehRot, oldModel);
            };

        }


   }

}
