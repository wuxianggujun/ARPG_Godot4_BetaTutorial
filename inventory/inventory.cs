using Godot;
using System;
using System.Collections.Generic;
using Array = Godot.Collections.Array;

public partial class inventory : Resource
{
    [Export] public inventoryItem[] Items { get; set; }
    
    

}