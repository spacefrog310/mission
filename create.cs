using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Windows.Forms;

namespace MissionTest
{
    class create : Script
    {

        public ColShape shape(Vector3 pos)
        {       
            var shape = API.createSphereColShape(pos, 10);
            return shape;
        }


        public NetHandle veh(VehicleHash model, Vector3 pos, Vector3 rot,int color1,int color2)
         {
            var blipIcon = 318;
            var veh = API.createVehicle(model, pos, rot,color1,color2,0);
            var vehpos = API.getEntityPosition(veh);
            var create = new create();
            create.blip(veh, vehpos, blipIcon);
            return veh;
         }


        public void blip(NetHandle obj,Vector3 pos,int blipIcon )
        {
            var blip = API.createBlip(obj);
            API.setBlipSprite(blip, blipIcon);
            API.setBlipPosition(blip, pos);
        }

        public void blipAtPos(Vector3 pos, int icon)
        {
           var blip = API.createBlip(pos);
           API.setBlipSprite(blip, icon);

        }

        public void mission(Client player, PedHash newPlayerModel, Vector3 missionStartCoord,Vector3 missionDesCoord,VehicleHash missionVeh,Vector3 vehRot, int missionVehInt, PedHash oldModel)
        {
            var playerObj = new set();
            var create = new create();
            playerObj.playerModel(player, newPlayerModel); // player to garbage model
            var shape = create.shape(missionDesCoord); // create shape at destination
            var veh = create.veh(missionVeh, missionStartCoord, vehRot, 255, 133); // veh create
            
            shape.onEntityEnterColShape += (s, ent) =>
            {

                var curVeh = API.getEntityModel(player.CurrentVehicle); 

                if (curVeh == missionVehInt) // not sure if this will work   //  missionvehint = 1917016601  (trash truck)
                {
                     
                    playerObj.playerModel(player, oldModel); // not sure about this // oldModel = API.pedNameToModel(Convert.ToString(curModel));
                    API.warpPlayerOutOfVehicle(player, veh); // warp player out of vehicle
                    API.deleteEntity(veh); // Delete the vehicle
                    API.deleteColShape(shape); // Delete the mission destination shape
                    API.sendChatMessageToPlayer(player, "Mission complete");
                }
                else
                {
                    API.sendChatMessageToPlayer(player, "Wrong Vehicle");
                }

            };

        }

    }

 }

