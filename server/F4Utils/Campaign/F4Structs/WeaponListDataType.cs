namespace F4Utils.Campaign.F4Structs
{
    public class WeaponListDataType {
        public sbyte[] Name=new sbyte[16];
        public short[] WeaponID=new short[WeaponsConstants.MAX_WEAPONS_IN_LIST];
        public byte[] Quantity=new byte[WeaponsConstants.MAX_WEAPONS_IN_LIST];
    };
}