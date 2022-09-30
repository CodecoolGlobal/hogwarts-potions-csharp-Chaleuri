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

        public Task<Student> GetStudentById(long studentId)
        {
            return Students.FirstAsync(student => student.ID.Equals(studentId)) ?? null;
        }

        public Task<List<Potion>> GetAllPotions()
        {
            return Potions.ToListAsync();
        }

        public async Task<Potion> BrewPotion(long studentId, Potion newPotion)
        {
            Student? brewStudent = await this.GetStudentById(studentId);
            if (brewStudent == null)
            {
                return null;
            }
            else
            {
                newPotion.BrewerStudent = brewStudent;
                foreach (Ingredient newPotionIngredient in newPotion.Ingredients)
                {
                    if (!Ingredients.Any(ingredient => ingredient.Name.Equals(newPotionIngredient.Name)))
                    {
                        Ingredients.Add(newPotionIngredient);
                    }
                }

                if (newPotion.Ingredients.Count == 5)
                {
                    if (Recipes.Any(recipe => recipe.Ingredients.Any(newPotion.Ingredients.Contains)))
                    {
                        newPotion.BrewingStatus = BrewingStatus.Replica;
                    }
                    else
                    {
                        newPotion.BrewingStatus = BrewingStatus.Discovery;
                        var newRecipe = new Recipe { Ingredients = newPotion.Ingredients, Student = newPotion.BrewerStudent, Name = $"{newPotion.BrewerStudent}'s discovery"};
                        Recipes.Add(newRecipe);
                        newPotion.Recipe = newRecipe;
                    }
                }

                await Potions.AddAsync(newPotion);
                await SaveChangesAsync();
                return newPotion;
            }
        }

        public async Task<List<Potion>> GetAllPotionsOfStudent(long studentId)
        {
            Student student = await this.GetStudentById(studentId);
            return await Potions.Where(potion => potion.BrewerStudent.Equals(student)).ToListAsync();
        }

        public async Task<Potion> AddIngredientToPotion()
        {
            throw new NotImplementedException();
        }

        public List<Ingredient> GetIngredientlistByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Recipe> GetRecipe(Potion potion)
        {
            List<Ingredient> ingredients = potion.Ingredients;
            Recipe returnRecipe;
            foreach (Recipe recipe in Recipes)
            {
                if (recipe.Ingredients.All(ingredients.Contains))
                {
                    potion.BrewingStatus = BrewingStatus.Replica;
                    returnRecipe = new Recipe
                    {
                        Ingredients = ingredients,
                        Student = potion.BrewerStudent,
                        Name = $"{potion.BrewerStudent.Name}'s discovery #{potion.DiscoveryCount}"
                    };
                    await SaveChangesAsync();
                    return returnRecipe;
                }
            }

            if (potion.Ingredients.Count < 5)
            {
                potion.BrewingStatus = BrewingStatus.Brew;
                returnRecipe = new Recipe
                {
                    Ingredients = ingredients,
                    Student = potion.BrewerStudent,
                    Name = $"{potion.BrewerStudent.Name}'s discovery #{potion.DiscoveryCount}"
                };
            }


        }
    }
}
