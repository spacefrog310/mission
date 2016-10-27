using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Windows.Forms;

namespace MissionTest
{

    public class MissionTest : Script
    {
        public MissionTest(Client player)
        {
            API.onResourceStart += myResourceStart;

            var curModel = API.getEntityModel(player.CharacterHandle); // current player model
            var oldModel = API.pedNameToModel(Convert.ToString(curModel)); // the old player model (not sure)
            PedHash model = PedHash.GarbageSMY; // garbadge man model
            var create = new create();
            VehicleHash missionVeh = VehicleHash.Trash;
            int missionVehInt = 1917016601; // 1917016601 = trash truck model
            Vector3 vehRot = new Vector3(0, 0, 0); // Vehicle rotation

            Vector3 missionStartCoord = player.Position; // player position
            Vector3 missionDesCoord = new Vector3(50, 50, 50); // destination coords 
            var missionStartIcon = 78;
            var startPoint = create.shape(missionStartCoord);

            create.blipAtPos(missionStartCoord, missionStartIcon); // create shape to start mission

            startPoint.onEntityEnterColShape += (s, ent) =>
            {
                create.mission(player, model, missionStartCoord, missionDesCoord, missionVeh, vehRot, missionVehInt, oldModel);
            };
        }

        public void myResourceStart()
        {
            API.consoleOutput("Starting script!");
        }



   }

}
