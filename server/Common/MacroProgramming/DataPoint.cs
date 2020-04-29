using System;
using System.Xml.Serialization;

namespace Common.MacroProgramming
{
    [Serializable]
    [XmlInclude(typeof(Range))]
    public abstract class DataPoint
    {
        /// <summary>
        ///     Compares this object to another one to determine if they are equal.  Equality for this type of object simply means
        ///     that the other object must be of the same type and must be monitoring the same DirectIn device.
        /// </summary>
        /// <param name="obj">An object to compare this object to</param>
        /// <returns>
        ///     a boolean, set to true, if the this object is equal to the specified object, and set to false, if they are not
        ///     equal.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode() && Serialization.Util.DeepEquals(this, obj);
        }

        /// <summary>
        ///     Gets an integer "hash" representation of this object, for use in hashtables.
        /// </summary>
        /// <returns>
        ///     an integer containing a numeric hash of this object's variables.  When two objects are Equal, their hashes
        ///     should be equal as well.
        /// </returns>
        public override int GetHashCode()
        {
            return Serialization.Util.ToRawBytes(this).GetHashCode();
        }

        /// <summary>
        ///     Gets a string representation of this object.
        /// </summary>
        /// <returns>a String containing a textual representation of this object.</returns>
        public override string ToString()
        {
            return Serialization.Util.SerializeToXml(this, typeof(DataPoint));
        }
    }
}