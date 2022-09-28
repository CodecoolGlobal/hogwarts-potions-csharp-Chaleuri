using System.Linq;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Data;

public class DbInitializer
{
    public static void Initialize(HogwartsContext context)
    {

        context.Database.EnsureCreated();

        if (context.Students.Any() || context.Rooms.Any())
        {
            return;
        }

        var rooms = new Room[]
        {
            new Room { Capacity = 5 },
            new Room { Capacity = 5 }
        };

        foreach (Room room in rooms)
        {
            context.Rooms.Add(room);
        }

        context.SaveChanges();

        var students = new Student[]
        {
            new Student{Name="Carson Alexander",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = rooms[0]},
            new Student{Name="Meredith Alonso",HouseType = HouseType.Gryffindor, PetType = PetType.Owl, Room = rooms[0]},
            new Student{Name="Arturo Anand",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = rooms[0]},
            new Student{Name="Gytis Barzdukas",HouseType = HouseType.Gryffindor, PetType = PetType.Cat, Room = rooms[0]},
            new Student{Name="Yan Li",HouseType = HouseType.Gryffindor, PetType = PetType.Rat, Room = rooms[0]},
            new Student{Name="Peggy Justice",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = rooms[1]},
            new Student{Name="Laura Alexander",HouseType = HouseType.Slytherin, PetType = PetType.None, Room = rooms[1]},
            new Student{Name="Nino Alexander",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = rooms[1]},
            new Student{Name="Arturo Olivetto",HouseType = HouseType.Slytherin, PetType = PetType.Owl, Room = rooms[1]},
            new Student{Name="Carson Norman",HouseType = HouseType.Slytherin, PetType = PetType.Cat, Room = rooms[1]},
        };

        foreach (Student s in students)
        {
            context.Students.Add(s);

        }

        context.SaveChanges();

    }
}