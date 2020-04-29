using System;
using System.Xml.Serialization;

namespace Common.MacroProgramming
{
    [Serializable]
    [XmlInclude(typeof(Counter))]
    [XmlInclude(typeof(Delay))]
    [XmlInclude(typeof(ExecuteCommand))]
    [XmlInclude(typeof(FlipFlop))]
    [XmlInclude(typeof(ForcedPriorityTask))]
    [XmlInclude(typeof(MathOperation))]
    [XmlInclude(typeof(Inverter))]
    [XmlInclude(typeof(Join))]
    [XmlInclude(typeof(KeyPress))]
    [XmlInclude(typeof(LogicGate))]
    [XmlInclude(typeof(Macro))]
    [XmlInclude(typeof(MouseClick))]
    [XmlInclude(typeof(MouseDoubleClick))]
    [XmlInclude(typeof(MouseMovement))]
    [XmlInclude(typeof(MouseRotator))]
    [XmlInclude(typeof(MouseWheelMovement))]
    [XmlInclude(typeof(Pulser))]
    [XmlInclude(typeof(RangeEvaluator))]
    [XmlInclude(typeof(Repeater))]
    [XmlInclude(typeof(Split))]
    [XmlInclude(typeof(Timeout))]
    [XmlInclude(typeof(Timer))]
    [XmlInclude(typeof(Toggle))]
    [XmlInclude(typeof(AnalogPassthrough))]
    [XmlInclude(typeof(DigitalPassthrough))]
    [XmlInclude(typeof(TextPassthrough))]
    [XmlInclude(typeof(AnalogSignalNormalizer))]
    [XmlInclude(typeof(BinaryCodedDecimalDigitDecoder))]
    [XmlInclude(typeof(BinaryCodedDecimalDigitEncoder))]
    [XmlInclude(typeof(AnalogSignalNormalizer))]
    public class Chainable
    {
        public string FriendlyName { get; set; }
        public string Id { get; set; }
        public object Tag { get; set; }

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
            return Serialization.Util.SerializeToXml(this, typeof(Chainable));
        }
    }
}