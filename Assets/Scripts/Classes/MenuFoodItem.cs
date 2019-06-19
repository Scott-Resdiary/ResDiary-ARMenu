using System;

public class MenuFoodItem{
    public int MenuItemId {get; set;}
    public string Category {get; set;}

    public double Cost {get; set;}

    public string Description {get; set;}

    public string Name{get; set;}

    public MenuFoodItem(int id, string name, double cost, string category, string description = null){
        this.MenuItemId = id;
        this.Name = name;
        this.Cost = cost;
        this.Category = category;
        this.Description = description;
    }
}