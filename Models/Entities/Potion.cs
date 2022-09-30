using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models.Entities;

public class Potion
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ID { get; set; }
    public string Name { get; set; }

    public List<Ingredient> Ingredients { get; set; }
    public Student BrewerStudent { get; set; }
    public Recipe Recipe { get; set; }
    public BrewingStatus BrewingStatus { get; set; }
}