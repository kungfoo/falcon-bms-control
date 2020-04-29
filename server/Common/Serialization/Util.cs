using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using log4net;

namespace Common.Serialization
{
    public static class Util
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Util));

        /// <summary>
        ///     Creates a deep clone of an object graph using a MemoryStream and a BinaryFormatter
        ///     to serialize an object graph to memory and then deserialize it back to an object.
        ///     Only serializable objects are supported, and only their serializable variables will
        ///     be cloned.
        /// </summary>
        /// <param name="toClone">an object which is the root of the object graph to clone</param>
        /// <returns>an object which is the root of the cloned object graph</returns>
        public static T DeepClone<T>(T toClone)
        {
            T cloned;
            using (var ms = new MemoryStream(1000))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, toClone);
                ms.Seek(0, SeekOrigin.Begin);
                cloned = (T) bf.Deserialize(ms);
                ms.Close();
            }
            return cloned;
        }

        /// <summary>
        ///     Compares two objects to determine their equality by
        ///     serializing both objects and comparing the serializations,
        ///     on the principal that two objects that serialize to the same
        ///     image must be identical as far as their serializable properties
        ///     are concerned.  This method uses MemoryStreams and BinaryFormatters
        ///     to do serialization in-memory and is not suited for huge object graphs.
        /// </summary>
        /// <param name="x">An object</param>
        /// <param name="y">An object to compare to "x"</param>
        /// <returns>true if both x and y are identical, or false if they are not</returns>
        public static bool DeepEquals(object x, object y)
        {
            string objXString = null;
            string objYString = null;
            if (x != null)
            {
                objXString = ToRawBytes(x);
            }
            if (y != null)
            {
                objYString = ToRawBytes(y);
            }
            return Equals(objXString, objYString);
        }

        public static object DeserializeFromXml(string xml, Type type)
        {
            if (string.IsNullOrWhiteSpace(xml)) return null;
            object toReturn;
            var serializer = XmlSerializer.FromTypes(new[] {type})[0];
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                ms.Seek(0, SeekOrigin.Begin);
                sw.Write(xml);
                sw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                toReturn = serializer.Deserialize(ms);
            }
            return toReturn;
        }

        public static T DeserializeFromXmlFile<T>(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException();
            if (new FileInfo(fileName).Exists)
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var toReturn = DeserializeFromXmlStream<T>(fs);
                    fs.Close();
                    return toReturn;
                }
            }
            return default(T);
        }

        public static T DeserializeFromXmlStream<T>(Stream xmlStream)
        {
            if (xmlStream == null) throw new ArgumentNullException(nameof(xmlStream));
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(xmlStream);
        }

        public static T DeserializeFromXmlString<T>(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return default(T);
            var serializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms, Encoding.UTF8))
            {
                sw.Write(xml);
                sw.Flush();
                sw.Close();
                ms.Seek(0, SeekOrigin.Begin);
                return (T) serializer.Deserialize(ms);
            }
        }

        /// <summary>
        ///     Deserializes an object from a string.
        /// </summary>
        /// <param name="x">a String containing the raw bytes of the object to deserialize.</param>
        /// <returns>the deserialized object.</returns>
        public static object FromRawBytes(string x)
        {
            object toReturn = null;
            if (!string.IsNullOrEmpty(x))
            {
                var bytes = Convert.FromBase64String(x);
                using (var ms = new MemoryStream(1000))
                {
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    var bf = new BinaryFormatter();
                    toReturn = bf.Deserialize(ms);
                    ms.Close();
                }
            }
            return toReturn;
        }

        public static string SerializeToXml(object x, Type type)
        {
            if (x == null) return null;
            string toReturn;
            var serializer = new XmlSerializer(type);
            using (var ms = new MemoryStream())
            {
                ms.Seek(0, SeekOrigin.Begin);
                try
                {
                    serializer.Serialize(ms, x);
                }
                catch (Exception e)
                {
                    _log.Debug(e.Message, e);
                    throw;
                }
                ms.Seek(0, SeekOrigin.Begin);
                using (var str = new StreamReader(ms))
                {
                    toReturn = str.ReadToEnd();
                }
            }
            return toReturn;
        }

        public static void SerializeToXmlFile<T>(T toSerialize, string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                SerializeToXmlStream(toSerialize, fs);
                fs.Flush();
                fs.Close();
            }
        }

        public static void SerializeToXmlStream<T>(T toSerialize, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, toSerialize);
            stream.Flush();
        }

        public static string SerializeToXmlString<T>(T toSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, toSerialize);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     Serializes an object to a string.
        /// </summary>
        /// <param name="toSerialize">The object to serialize.</param>
        /// <returns>a String containing the raw bytes of the supplied object.</returns>
        public static string ToRawBytes(object toSerialize)
        {
            string toReturn = null;
            byte[] bytes = null;
            if (toSerialize != null)
            {
                using (var ms = new MemoryStream(1000))
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(ms, toSerialize);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    bytes = new byte[ms.Length];
                    ms.Read(bytes, 0, bytes.Length);
                    ms.Close();
                }
            }
            if (bytes != null)
            {
                toReturn = Convert.ToBase64String(bytes);
            }
            return toReturn;
        }
    }
}