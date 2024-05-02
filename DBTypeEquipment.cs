using inventory.Interfaces;
using inventory.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace inventory
{
    public class DBTypeEquipment : IEquipmentType
    {



        public IEnumerable<TypeEquipment> AllTypeEquipment

        {
            get
            {




                List<TypeEquipment> typeequipment = new List<TypeEquipment>();


                MySqlDataReader TypeEquipmentData = Connection.SqlConnection("SELECT * FROM DataBase.TypeEquipment");


                while (TypeEquipmentData.Read())
                {
                    typeequipment.Add(new TypeEquipment()
                    {
                        Id = TypeEquipmentData.GetInt32(0),
                        Type = TypeEquipmentData.GetString(1),

                    });


                }
                TypeEquipmentData.Close();
                return typeequipment;

            }
        }
        public int Add(TypeEquipment typeequipment)
        {

            MySqlDataReader TypeEquipmentData = Connection.SqlConnection(
                $"insert into TypeEquipment (Type) values ('{typeequipment.Type}');");

            int IdUser = -1;

            TypeEquipmentData.Close();
            return IdUser;
        }
        public int Edit(TypeEquipment typeequipment)
        {
            MySqlDataReader TypeEquipmentData = Connection.SqlConnection(
                $"update TypeEquipment set Type = '{typeequipment.Type}' where id = {typeequipment.Id};");

            int IdUser = -1;
            TypeEquipmentData.Close();
            return IdUser;
        }

        public int Delete(TypeEquipment typeequipment)
        {

            MySqlDataReader TypeEquipmentData = Connection.SqlConnection(
                $"Delete from TypeEquipment where id = {typeequipment.Id}");
            TypeEquipmentData.Close();
            return -1;
        }
    }
}