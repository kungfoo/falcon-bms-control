using System;
using System.IO;
using System.Text;
using System.Xml;

namespace F4Utils.Speech
{
    public class CommFile
    {
        public CommFileHeaderRecord[] Headers;

        private CommFile()
        {
        }

        public static CommFile LoadFromBinary(string commFilePath)
        {
            var fi = new FileInfo(commFilePath);
            if (!fi.Exists) throw new FileNotFoundException(commFilePath);

            var commFile = new CommFile();
            var bytes = new byte[fi.Length];
            using (var fs = new FileStream(commFilePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(bytes, 0, (int) fi.Length);
            }


            var firstHeader = ReadHeader(bytes, 0);
            var numHeaders = firstHeader.commOffset/14;
            commFile.Headers = new CommFileHeaderRecord[numHeaders];
            commFile.Headers[0] = firstHeader;
            for (var i = 1; i < numHeaders; i++)
            {
                commFile.Headers[i] = ReadHeader(bytes, i);
            }

            return commFile;
        }

        private static CommFileHeaderRecord ReadHeader(byte[] bytes, int headerNum)
        {
            var pCommHeader = headerNum*14;
            var thisHeader = new CommFileHeaderRecord
                                 {
                                     commHdrNbr = BitConverter.ToUInt16(bytes, pCommHeader)
                                 };
            pCommHeader += 2;
            thisHeader.warp = BitConverter.ToUInt16(bytes, pCommHeader);
            pCommHeader += 2;
            thisHeader.priority = bytes[pCommHeader];
            pCommHeader++;
            thisHeader.positionElement = (sbyte) bytes[pCommHeader];
            pCommHeader++;
            thisHeader.bullseye = BitConverter.ToInt16(bytes, pCommHeader);
            pCommHeader += 2;
            thisHeader.totalElements = bytes[pCommHeader];
            pCommHeader++;
            thisHeader.totalEvals = bytes[pCommHeader];
            pCommHeader++;
            thisHeader.commOffset = BitConverter.ToUInt32(bytes, pCommHeader);
            pCommHeader += 4;
            thisHeader.data = new CommFileDataRecord[thisHeader.totalElements];
            var pData = thisHeader.commOffset;
            for (var i = 0; i < thisHeader.totalElements; i++)
            {
                var thisData = new CommFileDataRecord
                                   {
                                       fragIdOrEvalId = BitConverter.ToInt16(bytes, (int) pData)
                                   };
                pData += 2;
                thisHeader.data[i] = thisData;
            }
            return thisHeader;
        }

        public void FixupOffsets()
        {
            if (Headers == null) return;
            var offset = (uint) Headers.Length*14;
            for (ushort i = 0; i < Headers.Length; i++)
            {
                var thisHeader = Headers[i];
                thisHeader.commOffset = offset;
                Headers[i] = thisHeader;
                offset += (uint) 2*thisHeader.totalElements;
            }
        }

        public void SaveAsBinary(string commFilePath)
        {
            var fi = new FileInfo(commFilePath);

            using (var fs = new FileStream(commFilePath, FileMode.Create))
            {
                //write headers
                if (Headers != null)
                {
                    Array.Sort(Headers,
                               delegate(CommFileHeaderRecord hdr1, CommFileHeaderRecord hdr2) { return hdr1.commHdrNbr.CompareTo(hdr2.commHdrNbr); });
                    for (var i = 0; i < Headers.Length; i++)
                    {
                        var thisHeader = Headers[i];
                        fs.Write(BitConverter.GetBytes(thisHeader.commHdrNbr), 0, 2);
                        fs.Write(BitConverter.GetBytes(thisHeader.warp), 0, 2);
                        fs.WriteByte(thisHeader.priority);
                        fs.WriteByte((byte) thisHeader.positionElement);
                        fs.Write(BitConverter.GetBytes(thisHeader.bullseye), 0, 2);
                        fs.WriteByte(thisHeader.totalElements);
                        fs.WriteByte(thisHeader.totalEvals);
                        fs.Write(BitConverter.GetBytes(thisHeader.commOffset), 0, 4);
                    }

                    //write data
                    for (var i = 0; i < Headers.Length; i++)
                    {
                        var thisHeader = Headers[i];
                        if (thisHeader.data != null)
                        {
                            for (var j = 0; j < thisHeader.data.Length; j++)
                            {
                                var thisDataRecord = thisHeader.data[j];
                                fs.Write(BitConverter.GetBytes(thisDataRecord.fragIdOrEvalId), 0, 2);
                            }
                        }
                    }
                }
                fs.Flush();
                fs.Close();
            }
        }

        public void SaveAsXml(string commXmlFilePath)
        {
            var fi = new FileInfo(commXmlFilePath);
            var xws = new XmlWriterSettings
                          {
                              Indent = true,
                              NewLineOnAttributes = false,
                              OmitXmlDeclaration = false,
                              IndentChars = "\t",
                              CheckCharacters = true,
                              Encoding = Encoding.UTF8
                          };


            using (var fs = new FileStream(commXmlFilePath, FileMode.Create))
            using (var xw = XmlWriter.Create(fs, xws))
            {
                xw.WriteStartDocument();
                xw.WriteStartElement("CommFile");
                //xw.WriteStartAttribute("numComms");
                //xw.WriteValue(this.headers.Length);
                //xw.WriteEndAttribute();
                if (Headers != null)
                {
                    Array.Sort(Headers,
                               delegate(CommFileHeaderRecord hdr1, CommFileHeaderRecord hdr2) { return hdr1.commHdrNbr.CompareTo(hdr2.commHdrNbr); });
                    for (var i = 0; i < Headers.Length; i++)
                    {
                        var thisHeader = Headers[i];
                        if (thisHeader.data != null)
                        {
                            xw.WriteStartElement("Comm");
                            xw.WriteStartAttribute("id");
                            xw.WriteValue(thisHeader.commHdrNbr);
                            xw.WriteEndAttribute();
                            xw.WriteStartAttribute("warp");
                            xw.WriteValue(thisHeader.warp);
                            xw.WriteEndAttribute();
                            xw.WriteStartAttribute("priority");
                            xw.WriteValue(thisHeader.priority);
                            xw.WriteEndAttribute();
                            xw.WriteStartAttribute("positionElement");
                            xw.WriteValue(thisHeader.positionElement);
                            xw.WriteEndAttribute();
                            xw.WriteStartAttribute("bullseye");
                            xw.WriteValue(thisHeader.bullseye);
                            xw.WriteEndAttribute();
                            //xw.WriteStartAttribute("totalElements");
                            //xw.WriteValue(thisHeader.totalElements);
                            //xw.WriteEndAttribute();
                            //xw.WriteStartAttribute("totalEvals");
                            //xw.WriteValue(thisHeader.totalEvals);
                            //xw.WriteEndAttribute();

                            for (var j = 0; j < thisHeader.data.Length; j++)
                            {
                                var thisDataRecord = thisHeader.data[j];
                                xw.WriteStartElement("CommElement");
                                xw.WriteStartAttribute("index");
                                xw.WriteValue(j);
                                xw.WriteEndAttribute();
                                if (thisDataRecord.fragIdOrEvalId < 0)
                                {
                                    xw.WriteStartAttribute("evalId");
                                    xw.WriteValue(-thisDataRecord.fragIdOrEvalId);
                                    xw.WriteEndAttribute();
                                }
                                else
                                {
                                    xw.WriteStartAttribute("fragId");
                                    xw.WriteValue(thisDataRecord.fragIdOrEvalId);
                                    xw.WriteEndAttribute();
                                }
                                xw.WriteEndElement();
                            }
                            xw.WriteEndElement();
                        }
                    }
                }
                xw.WriteEndElement();
                xw.WriteEndDocument();
                xw.Flush();
                fs.Flush();
                xw.Close();
            }
        }

        public static CommFile LoadFromXml(string commXmlFilePath)
        {
            var toReturn = new CommFile();
            var headers = new CommFileHeaderRecord[0];
            using (var fs = new FileStream(commXmlFilePath, FileMode.Open, FileAccess.Read))
            using (XmlReader xr = new XmlTextReader(fs))
            {
                var thisHeader = new CommFileHeaderRecord();
                var dataRecords = new CommFileDataRecord[0];
                while (xr.Read())
                {
                    long val;
                    string attribValString;
                    bool parsed;
                    if (xr.NodeType == XmlNodeType.Element && xr.Name == "CommFile")
                    {
                        //attribValString = xr.GetAttribute("numComms");
                        //parsed = Int64.TryParse(attribValString, out val);
                        //if (parsed)
                        //{
                        // headers = new CommFileHeaderRecord[val];
                        //}
                        //else
                        //{
                        //    throw new IOException(string.Format("Could not parse {0}, bad or missing @numComms attribute in /CommFile root element.", commXmlFilePath));
                        //}
                        headers = new CommFileHeaderRecord[0];
                    }
                    if (xr.NodeType == XmlNodeType.Element && xr.Name == "Comm")
                    {
                        thisHeader = new CommFileHeaderRecord();

                        attribValString = xr.GetAttribute("id");
                        parsed = Int64.TryParse(attribValString, out val);
                        if (parsed)
                        {
                            thisHeader.commHdrNbr = (ushort) val;
                        }
                        else
                        {
                            throw new IOException(
                                string.Format(
                                    "Could not parse {0}, bad or missing @id attribute in /CommFile/Comm element.",
                                    commXmlFilePath));
                        }

                        attribValString = xr.GetAttribute("warp");
                        parsed = Int64.TryParse(attribValString, out val);
                        if (parsed)
                        {
                            thisHeader.warp = (ushort) val;
                        }
                        else
                        {
                            throw new IOException(
                                string.Format(
                                    "Could not parse {0}, bad or missing @warp attribute in /CommFile/Comm element.",
                                    commXmlFilePath));
                        }

                        attribValString = xr.GetAttribute("priority");
                        parsed = Int64.TryParse(attribValString, out val);
                        if (parsed)
                        {
                            thisHeader.priority = (byte) val;
                        }
                        else
                        {
                            throw new IOException(
                                string.Format(
                                    "Could not parse {0}, bad or missing @priority attribute in /CommFile/Comm element.",
                                    commXmlFilePath));
                        }

                        attribValString = xr.GetAttribute("positionElement");
                        parsed = Int64.TryParse(attribValString, out val);
                        if (parsed)
                        {
                            thisHeader.positionElement = (sbyte) val;
                        }
                        else
                        {
                            throw new IOException(
                                string.Format(
                                    "Could not parse {0}, bad or missing @positionElement attribute in /CommFile/Comm element.",
                                    commXmlFilePath));
                        }

                        attribValString = xr.GetAttribute("bullseye");
                        parsed = Int64.TryParse(attribValString, out val);
                        if (parsed)
                        {
                            thisHeader.bullseye = (short) val;
                        }
                        else
                        {
                            throw new IOException(
                                string.Format(
                                    "Could not parse {0}, bad or missing @bullseye attribute in /CommFile/Comm element.",
                                    commXmlFilePath));
                        }

                        //attribValString = xr.GetAttribute("totalElements");
                        //parsed = Int64.TryParse(attribValString, out val);
                        //if (parsed)
                        //{
                        //    thisHeader.totalElements = (byte)val;
                        //    dataRecords = new CommFileDataRecord[thisHeader.totalElements];
                        //}
                        //else
                        //{
                        //    throw new IOException(string.Format("Could not parse {0}, bad or missing @totalElements attribute in /CommFile/Comm element.", commXmlFilePath));
                        //}
                        dataRecords = new CommFileDataRecord[0];

                        //attribValString = xr.GetAttribute("totalEvals");
                        //parsed = Int64.TryParse(attribValString, out val);
                        //if (parsed)
                        //{
                        //    thisHeader.totalEvals = (byte)val;
                        //}
                        //else
                        //{
                        //    throw new IOException(string.Format("Could not parse {0}, bad or missing @totalEvals attribute in /CommFile/Comm element.", commXmlFilePath));
                        //}
                    }
                    else if (xr.NodeType == XmlNodeType.Element && (xr.Name == "CommElement"))
                    {
                        var thisElementDataRecord = new CommFileDataRecord();
                        attribValString = xr.GetAttribute("fragId");
                        if (!string.IsNullOrEmpty(attribValString))
                        {
                            parsed = Int64.TryParse(attribValString, out val);
                            if (parsed)
                            {
                                thisElementDataRecord.fragIdOrEvalId = (short) val;
                            }
                            else
                            {
                                throw new IOException(
                                    string.Format(
                                        "Could not parse {0}, bad or missing @fragId attribute in /CommFile/Comm/CommElement element.",
                                        commXmlFilePath));
                            }
                        }
                        else
                        {
                            attribValString = xr.GetAttribute("evalId");
                            if (string.IsNullOrEmpty(attribValString))
                            {
                                throw new IOException(
                                    string.Format(
                                        "Could not parse {0}, missing @fragId or @evalId attribute in /CommFile/Comm/CommElement element.",
                                        commXmlFilePath));
                            }
                            parsed = Int64.TryParse(attribValString, out val);
                            if (parsed)
                            {
                                thisHeader.totalEvals++;
                                thisElementDataRecord.fragIdOrEvalId = (short) -val;
                            }
                            else
                            {
                                throw new IOException(
                                    string.Format(
                                        "Could not parse {0}, bad or missing @evalId attribute in /CommFile/Comm/CommElement element.",
                                        commXmlFilePath));
                            }
                        }

                        attribValString = xr.GetAttribute("index");
                        parsed = Int64.TryParse(attribValString, out val);
                        if (parsed)
                        {
                            var index = (int) val;
                            if (index > dataRecords.Length - 1)
                            {
                                //throw new IOException(string.Format("Could not parse {0}, @index attribute value in /CommFile/Comm/CommElement element exceeds (@totalElements-1) value declared in parent /CommFile/Comm element.", commXmlFilePath));
                                Array.Resize(ref dataRecords, index + 1);
                            }
                            dataRecords[index] = thisElementDataRecord;
                            thisHeader.totalElements++;
                        }
                        else
                        {
                            throw new IOException(
                                string.Format(
                                    "Could not parse {0}, bad or missing @index attribute in /CommFile/Comm/CommElement element.",
                                    commXmlFilePath));
                        }
                    }
                    else if (xr.NodeType == XmlNodeType.EndElement && xr.Name == "Comm")
                    {
                        thisHeader.data = dataRecords;
                        if (thisHeader.commHdrNbr > headers.Length - 1)
                        {
                            //throw new IOException(string.Format("Could not parse {0}, @id attribute value in /CommFile/Comm element exceeds (@numComms-1) attribute value declared in /CommFile root element.", commXmlFilePath));
                            Array.Resize(ref headers, thisHeader.commHdrNbr + 1);
                        }
                        headers[thisHeader.commHdrNbr] = thisHeader;
                    }
                }
            }
            toReturn.Headers = headers;
            toReturn.FixupOffsets();
            return toReturn;
        }
    }
}