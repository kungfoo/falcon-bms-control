namespace F4Utils.Campaign.F4Structs
{
    public enum Classtable_Types
    {
        TYPE_NOTHING = 1,

        //Abstract/weapons
        TYPE_MINESWEEP = 2,
        TYPE_GUN = 3,
        TYPE_RACK = 4,
        TYPE_LAUNCHER = 5,
        TYPE_ABSTRACT_WEAPONS,

        // air/class manager/airtasking manager
        TYPE_ATM = 1,

        // air/unit
        TYPE_FLIGHT = 1,
        TYPE_PACKAGE = 2,
        TYPE_SQUADRON = 3,
        TYPE_AIR_UNITS,

        // air/vehicle
        TYPE_AIRPLANE = 1,
        TYPE_BOMB = 2,
        //TYPE_ELECTRONICS            = 3,
        TYPE_POD = 3,
        TYPE_FUEL_TANK = 4,
        TYPE_HELICOPTER = 5,
        TYPE_MISSILE = 6,
        TYPE_RECON = 7,
        TYPE_ROCKET = 8,
        TYPE_AIR_VEHICLES,

        // land/objective
        TYPE_AIRBASE = 1,
        TYPE_AIRSTRIP = 2,
        TYPE_ARMYBASE = 3,
        TYPE_BEACH = 4,
        TYPE_BORDER = 5,
        TYPE_BRIDGE = 6,
        TYPE_CHEMICAL = 7,
        TYPE_CITY = 8,
        TYPE_COM_CONTROL = 9,
        TYPE_DEPOT = 10,
        TYPE_FACTORY = 11,
        TYPE_FORD = 12,
        TYPE_FORTIFICATION = 13,
        TYPE_HILL_TOP = 14,
        TYPE_INTERSECT = 15,
        TYPE_NAV_BEACON = 16,
        TYPE_NUCLEAR = 17,
        TYPE_PASS = 18,
        TYPE_PORT = 19,
        TYPE_POWERPLANT = 20,
        TYPE_RADAR = 21,
        TYPE_RADIO_TOWER = 22,
        TYPE_RAIL_TERMINAL = 23,
        TYPE_RAILROAD = 24,
        TYPE_REFINERY = 25,
        TYPE_ROAD = 26,
        TYPE_SEA = 27,
        TYPE_TOWN = 28,
        TYPE_VILLAGE = 29,
        TYPE_HARTS = 30,
        TYPE_SAM_SITE = 31,
        TYPE_LAND_OBJECTIVES,

        // land/features
        TYPE_CRATER = 1, // uncertain
        TYPE_CTRL_TOWER = 2,
        TYPE_BARN = 3,
        TYPE_BUNKER = 4,
        TYPE_BUSH = 5,
        TYPE_FACTORYS = 6,
        TYPE_CHURCH = 7,
        TYPE_CITY_HALL = 8,
        TYPE_DOCK = 9,
        //TYPE_DEPOT                  = 10, // already in the enum under objectives
        TYPE_RUNWAY = 11,
        TYPE_WAREHOUSE = 12,
        TYPE_HELIPAD = 13,
        TYPE_FUEL_TANKS = 14,
        TYPE_NUKE_PLANT = 15,
        TYPE_BRIDGES = 16,
        TYPE_PIER = 17,
        TYPE_PPOLE = 18,
        TYPE_SHOP = 19,
        TYPE_PTOWER = 20,
        TYPE_APARTMENT = 21,
        TYPE_HOUSE = 22,
        TYPE_PPLANT = 23,
        TYPE_TAXI_SIGN = 24,
        TYPE_NAV_BEAC = 25,
        TYPE_RADAR_SITE = 26,
        TYPE_CRATERS = 27,
        TYPE_RADARS = 28,
        TYPE_RTOWER = 29,
        TYPE_TAXIWAY = 30,
        TYPE_RAIL_TERMINALS = 31,
        TYPE_REFINERYS = 32,
        TYPE_SAM = 33,
        TYPE_SHED = 34,
        TYPE_BARRACKS = 35,
        TYPE_TREE = 36,
        TYPE_WTOWER = 37,
        TYPE_TWNHALL = 38,
        TYPE_AIR_TERMINAL = 39, // used by "Terminal"
        // TYPE_YARD                   = 39, // ???
        TYPE_SHRINE = 40,
        TYPE_PARK = 41,
        TYPE_OFF_BLOCK = 42,
        TYPE_TVSTN = 43,
        TYPE_HOTEL = 44,
        TYPE_HANGAR = 45,
        TYPE_LIGHTS = 46,
        TYPE_VASI = 47,
        TYPE_TANK = 48,
        TYPE_FENCE = 49,
        TYPE_PARKINGLOT = 50,
        TYPE_SMOKESTACK = 51,
        TYPE_BUILDING = 52,
        TYPE_COOL_TWR = 53,
        TYPE_CONT_DOME = 54,
        TYPE_GUARDHOUSE = 55,
        TYPE_TRANSFORMER = 56,
        TYPE_AMMO_DUMP = 57,
        TYPE_ART_SITE = 58,
        TYPE_OFFICE = 59,
        TYPE_CHEM_PLANT = 60,
        TYPE_TOWER = 61,
        TYPE_HOSPITAL = 62,
        TYPE_SHOPBLK = 63,
        //TYPE_RUNWAY_NUM             = 64, // ???
        TYPE_STATIC = 64, // used by weapon trailer
        TYPE_RUNWAY_MARKER = 65,
        TYPE_STADIUM = 66,
        TYPE_MONUMENT = 67,
        TYPE_LAND_FEATURES,

        // land/classmanager
        TYPE_GTM = 1,

        // land/unit
        TYPE_BATTALION = 1,
        TYPE_BRIGADE = 2,
        TYPE_LAND_UNITS,

        // land/vehicle
        TYPE_FOOT = 1,
        TYPE_TOWED = 2,
        TYPE_TRACKED = 3,
        TYPE_WHEELED = 4,
        TYPE_LAND_VEHICLES,

        // sea/classmanager
        TYPE_NTM = 1,

        // sea/unit
        TYPE_TASKFORCE = 1,
        TYPE_SEA_UNITS,

        // sea/vehicle
        TYPE_ASSAULT = 1,
        TYPE_BOAT = 2,
        TYPE_BUOY = 3,
        TYPE_CAPITAL_SHIP = 4,
        TYPE_CARGO = 5,
        TYPE_CRUISER = 6,
        TYPE_DEPTHCHARGE = 7,
        TYPE_DESTROYER = 8,
        TYPE_FRIGATE = 9,
        TYPE_PATROL = 10,
        TYPE_RAFT = 11,
        TYPE_SHIP = 12,
        TYPE_TANKER = 13,
        TYPE_TORPEDO = 14,
        TYPE_SEA_VEHICLES,

        // undersea/unit
        TYPE_WOLFPACK = 1,
        TYPE_UNSERSEA_UNITS,

        // undersea/vehicle
        TYPE_SUBMARINE = 1,
        TYPE_UNSERSEA_VEHICLES,

        // abs/classmanager
        TYPE_TEAM = 1,

        // air/sfx
        TYPE_CHAFF = 5,
        TYPE_FLARE = 6,
        TYPE_EJECT = 9, // domain and class seem to not matter

        // abstract/abstract
        TYPE_FLYING_EYE = 1,

        // these are all unknown and unreferenced
        TYPE_ARMADA = 1,
        TYPE_END_MISSION = 1,
        TYPE_TEST = 15,
        TYPE_COCKPIT = 3,
        TYPE_DEBRIS = 1,
        TYPE_AEXPLOSION = 1,
        TYPE_CANOPY = 7,
        TYPE_EXPLOSION = 1,
        TYPE_FIRE = 2,
        TYPE_DUST = 3,
        TYPE_SMOKE = 4,
        TYPE_CLOUD = 1,
        TYPE_HULK = 2,
    }
}