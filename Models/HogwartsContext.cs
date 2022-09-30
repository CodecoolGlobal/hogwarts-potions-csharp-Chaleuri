using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models
{
    public class HogwartsContext : DbContext
    {
        public const int MaxIngredientsForPotions = 5;
        public DbSet<Student> Students { get; private set; }
        public DbSet<Room> Rooms { get; private set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Potion> Potions { get; set; }

        public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<Room>().ToTable("rooms");

            modelBuilder.Entity<Ingredient>().ToTable("ingredients");
            modelBuilder.Entity<Recipe>().ToTable("recipes");
            modelBuilder.Entity<Potion>().ToTable("potions");
        }

        public async Task AddRoom(Room room)
        {
            this.Rooms.Add(room);

            await SaveChangesAsync();
        }

        public Task<Room> GetRoom(long roomId)
        {
            return Rooms.FirstOrDefaultAsync(room => room.ID.Equals(roomId));
        }

        public Task<List<Room>> GetAllRooms()
        {
            return Rooms.ToListAsync();
        }

        public async Task UpdateRoom(Room room)
        {
            Rooms.Update(room);

            await SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var deleteRoom = await GetRoom(id);
            if (!deleteRoom.Equals(null))
            {
                Rooms.Remove(deleteRoom);
                await SaveChangesAsync();
            }
        }

        public Task<List<Room>> GetRoomsForRatOwners()
        {
            return Rooms.Where(room =>
                    room.Residents.Any(student => student.PetType != PetType.Cat || student.PetType != PetType.Owl))
                .ToListAsync();
        }

        public Task<List<Potion>> GetAllPotions()
        {
            return Potions.ToListAsync();
        }

        public Task<Potion> BrewPotion()
        {
            throw new NotImplementedException();
        }

        public Task<List<Potion>> GetAllPotionsOfStudent()
        {
            throw new NotImplementedException();
        }

        public async Task<Potion> AddIngredientToPotion()
        {
            throw new NotImplementedException();
        }

        public List<Ingredient> GetIngredientlistByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
