namespace F4Utils.Campaign.F4Structs
{
    public class SquadronStoresDataType {
        public byte[] Stores = new byte[CampLibConstants.MAXIMUM_WEAPTYPES];	// Weapon stores (only has meaning for squadrons)
        public byte infiniteAG;					// One AG weapon we've chosen to always have available
        public byte infiniteAA;					// One AA weapon we've chosen to always have available
        public byte infiniteGun;				// Our main gun weapon, which we will always have available
    };
}