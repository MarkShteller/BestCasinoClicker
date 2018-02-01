using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class Casino : AssetType
{
    private List<Building> buildings;
    private List<Achievement> achievements;

    public Casino(int id, string name, string description, string icon) : base(id, name, description, icon)
    {
        
    }
}

