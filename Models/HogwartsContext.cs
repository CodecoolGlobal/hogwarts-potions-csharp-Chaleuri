using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;
        public DbSet<Student> Students { get; private set; }
        public DbSet<Room> Rooms { get; private set; }

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<Room>().ToTable("rooms");
        }

        public async Task AddRoom(Room room)
        {
            if (room != null && !this.Rooms.Contains(room))
            {
                this.Rooms.Add(room);
            }
        }

        public Task<Room> GetRoom(long roomId)
        {
            var room = Rooms.FirstOrDefaultAsync(room => room.ID.Equals(roomId));
            return room;
        }

        public Task<List<Room>> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRoom(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            throw new NotImplementedException();
        }
    }
}
